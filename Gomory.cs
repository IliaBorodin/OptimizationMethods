using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeLab
{
    public partial class Gomory : Form
    {

        private List<List<CompositeNumber>> MainTableFraction = new List<List<CompositeNumber>>();
        private List<List<List<CompositeNumber>>> listOfHistoryTable = new List<List<List<CompositeNumber>>>();
        private int typeOfTask;
        private List<Int32> listOfParams = new List<Int32>();//Список кол-ва переменных
        private List<List<Int32>> listOfHistoryParams = new List<List<Int32>>();//История списка параметров
        List<Int32> listOfBasis = new List<Int32>(); //Список базисных переменных 
        List<List<Int32>> listOfHistoryBasis = new List<List<Int32>>();// История списка базисных переменных
        public Gomory(List<List<CompositeNumber>> list, int typeOfTask, List<Int32> listOfBasis, List<Int32> listOfParams)
        {
            this.MainTableFraction = list;
            this.typeOfTask = typeOfTask;
            this.listOfBasis = listOfBasis;
            this.listOfParams = listOfParams;
            this.listOfHistoryBasis.Add(listOfBasis);
            this.listOfHistoryParams.Add(listOfParams);
            
            InitializeComponent();
            startAlgo();

        }
        public List<CompositeNumber> getRowWithMaxFraction()
        {
            int index = 0;
            CompositeNumber maxNumber = CompositeNumber.Null;
            for(int i = 0; i< MainTableFraction.Count - 1; ++i)
            {
                CompositeNumber tmp = MainTableFraction[i][MainTableFraction[i].Count - 1].getFractionalPart();//получили дробную часть
                if(maxNumber < tmp)
                {
                    maxNumber = tmp;
                    index = i;
                }
            }
            return MainTableFraction[index];
        }

        public void createRowGomory(List<CompositeNumber> row)
        {
            List<CompositeNumber> newRow = new List<CompositeNumber>();
            List<CompositeNumber> tmpRow = MainTableFraction[MainTableFraction.Count - 1]; // строка целевой ф-и в Симплекс-таблице
            CompositeNumber minusOne = new CompositeNumber(-1, 1);
            for(int i = 0; i< row.Count; ++i)
            {
                CompositeNumber number = row[i].getFractionalPart() * minusOne;
                newRow.Add(number);
            }
            this.MainTableFraction.Insert(MainTableFraction.Count - 1, newRow);
           // this.MainTableFraction.Add(tmpRow);
            this.listOfBasis.Add(listOfBasis.Count + listOfParams.Count + 1);
            //this.listOfHistoryTable.Add(this.MainTableFraction);
        }



        public void nextSimplexStep()
        {
            int columns = MainTableFraction[0].Count;
            int rows = MainTableFraction.Count;
            int n = this.listOfBasis.Count - 1;
            int m = this.listOfParams.Count - 1;


            List<List<CompositeNumber>> curr = new List<List<CompositeNumber>>();
            int index = 0;
            foreach (List<CompositeNumber> row in MainTableFraction)
            {
                curr.Add(new List<CompositeNumber>());
                foreach (CompositeNumber cell in row)
                {
                    curr[index].Add(cell);
                }
                ++index;
            }
            listOfHistoryTable.Add(curr);

            List<Int32> currParam = new List<Int32>();
            foreach (Int32 par in listOfParams)
            {
                currParam.Add(par);
            }
            listOfHistoryParams.Add(currParam);

            List<Int32> currBas = new List<Int32>();
            foreach (Int32 basi in listOfBasis)
            {
                currBas.Add(basi);
            }
            listOfHistoryBasis.Add(currBas);


            Int32 tempFraction = listOfBasis[n];
            listOfBasis[n] = listOfParams[m];
            listOfParams[m] = tempFraction;


            MainTableFraction[n][m] = new CompositeNumber(1, 1) / MainTableFraction[n][m];


            for (int i = 0; i < columns; ++i)
            {
                if (i != m)
                {
                    MainTableFraction[n][i] = MainTableFraction[n][i] * MainTableFraction[n][m];
                }
            }


            for (int i = 0; i < rows; ++i)
            {
                if (i != n)
                {
                    MainTableFraction[i][m] = -MainTableFraction[i][m] * MainTableFraction[n][m];
                }
            }


            for (int i = 0; i < rows; ++i)
            {
                if (i != n)
                {
                    for (int j = 0; j < columns; ++j)
                    {
                        if (j != m)
                        {
                            CompositeNumber previter = -MainTableFraction[i][m] / MainTableFraction[n][m];
                            MainTableFraction[i][j] = MainTableFraction[i][j] - previter * MainTableFraction[n][j];
                        }
                    }
                }
            }
        }


        public void updateGomoryTable(DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            int columns = MainTableFraction[1].Count;

            for (int a = 0; a < listOfParams.Count; a++)
            {
                dataGridView.Columns.Add("x" + a, "x" + listOfParams[a]);
            }
            dataGridView.Columns.Add("b", "b");

            for (int i = 0; i < MainTableFraction.Count; i++)
            {
                string[] limitRow = new string[18];
                Array.Resize(ref limitRow, columns + 1);
                for (int j = 0; j < MainTableFraction[i].Count; j++)
                {
                    limitRow[j] = MainTableFraction[i][j].ToString();
                }

                dataGridView.Rows.Add(limitRow);
                if (i != MainTableFraction.Count - 1)
                {
                    dataGridView.Rows[i].HeaderCell.Value = "x" + listOfBasis[i];
                }

            }
        }

        public void inverseLastRow()
        {
            CompositeNumber minusOne = new CompositeNumber(-1, 1);
            for(int i = 0; i< MainTableFraction[0].Count; ++i)
            {
                MainTableFraction[MainTableFraction.Count - 1][i] = minusOne * MainTableFraction[MainTableFraction.Count - 1][i];
            }
        }

        public bool checkOptimalityCondition()
        {
            bool flag = true;
             for (int i = 0; i < MainTableFraction.Count; ++i)
            {
                if (MainTableFraction[i][MainTableFraction[0].Count-1].getDenominator() != 1)
                    flag = false;
            }
            return flag;
        }

        public void startAlgo()
        {
            if(typeOfTask == 1)
            {
                // inverseColumnMatrix();
                //inverseLastRow();
            }
            this.listOfHistoryTable.Add(MainTableFraction);
            if (checkOptimalityCondition())
            {
                //Вывод ответа
                return;
            }
            inverseLastRow(); // умножаем на -1 последнюю строку F
            while (true)
            {
                createRowGomory(getRowWithMaxFraction());
                nextSimplexStep();
                if (checkOptimalityCondition())
                    break;
            }
            updateGomoryTable(GomoryVisualization);
            CreateAnswer();

        }


        public void CreateAnswer()
        {
            for(int i = 0; i< listOfHistoryTable.Count;++i)
            {
                HistoryBox.Items.Add(i + 1);
                int x = 1;
            }
        }

        private void HistoryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = HistoryBox.SelectedIndex;
            MainTableFraction = listOfHistoryTable[index];
            listOfParams = listOfHistoryParams[index];
            listOfBasis = listOfHistoryBasis[index];
            updateGomoryTable(GomoryVisualization);
        }

        //public void inverseColumnMatrix()
        //{ CompositeNumber minusOne = new CompositeNumber(-1, 1);
        //    for(int i = 0; i < MainTableFraction.Count - 1; ++i)
        //    {
        //        MainTableFraction[i][MainTableFraction[0].Count - 1] = minusOne * MainTableFraction[i][MainTableFraction[0].Count - 1];

        //    }
        //}
    }
}
