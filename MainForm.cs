using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeLab
{
    public partial class MainForm : Form
    {
        public const int SimplexMethod = 0, GraphicalMethod = 1; // Константы для способа решения задачи
        public const int Maximize = 0, Minimize = 1;// Константы для того, что мы хотим сделать с целевой функцией
        public const int Regular = 0, Decimal = 1;// Константы для типа дробей
        public const int Selective = 1, Artificial = 0;// Константы для типа базиса

        List<Int32> listOfParams = new List<Int32>();//Список кол-ва переменных
        List<List<Int32>> listOfHistoryParams = new List<List<Int32>>();//История списка параметров

        List<Int32> listOfBasis = new List<Int32>(); //Список базисных переменных 
        List<Int32> listOfCoef = new List<Int32>();//Список коэффициентов у базисных переменных
        List<List<Int32>> listOfHistoryBasis = new List<List<Int32>>();// История списка базисных переменных
        List<Int32> listOfTags = new List<Int32>();// Список хранящий номера введенных базисных элементов
        List<List<List<CompositeNumber>>> listOfHistoryTable = new List<List<List<CompositeNumber>>>();//История всей таблицы
        List<(int, int)> listOfSupElements = new List<(int, int)>();//Список для опорных элементов

        List<CompositeNumber> listOfFuncFraction = new List<CompositeNumber>();// Список, содержащий коэффициенты  f(x)
        List<List<CompositeNumber>> MainTableFraction = new List<List<CompositeNumber>>();//Основная таблица таблица с коэфициентами ограничений
        //Вспомогательные переменные
        int typeOfTask = Maximize, typeOfBasis = Artificial, typeOfFraction = Regular, solutionMethod = SimplexMethod;
        int columns = 3, rows = 2, mostRow = 0, mostCol = 0, choosedRow = 0, choosedCol = 0;

        public MainForm()
        {
            InitializeComponent();
            GomoryBtn.Visible = false;
        }
        //---------------------------------------------------------------------------------
        //Обработчики для taskConditionPage
        //Метод, задающий конфигурацию задачи
        private void SetParametersBtn_Click(object sender, EventArgs e)
        {
            typeOfTask = OptimizationComboBox.SelectedIndex;
            typeOfBasis = BasisComboBox.SelectedIndex;
            typeOfFraction = TypeOfFractionsComboBox.SelectedIndex;
            solutionMethod = TypeOfTaskcomboBox.SelectedIndex;
            columns = Int32.Parse(NumOfVariablesTextBox.Text);
            rows = Int32.Parse(NumOfRestrictionsTextBox.Text);
            if (HelperClass.checkingTheInput(columns, rows) == 1)
            {
                //Очищаем таблицу функции
                FunctionDisplay.Rows.Clear();
                FunctionDisplay.Columns.Clear();
                //Очищаем таблицу ограничений
                RestrictionsDisplay.Rows.Clear();
                RestrictionsDisplay.Columns.Clear();
                //Заполнение таблицы функции 

                HelperClass.completionFuncTable(columns, rows, FunctionDisplay);
                HelperClass.completionRestrictionsTable(columns, rows, RestrictionsDisplay);
            }

        }
        //Метод, запускающий решение
        private void DecideBtn_Click(object sender, EventArgs e)
        {
            labelForResponse.Text = "";
            if (solutionMethod == SimplexMethod)
            {
                TransitionBtn.Visible = false;
                MainTableFraction.Clear();
                listOfBasis.Clear();
                listOfParams.Clear();


                CompositeNumber.typeOfFraction = typeOfFraction;



                if (CreateTable())
                {
                    if (typeOfBasis == Selective)
                    {

                        BasisVariables form = new BasisVariables(columns, rows);
                        form.ShowDialog();

                        listOfBasis = form.getListOfBasis();
                        listOfParams = form.getListOfParams();


                        CreateTableForSimplexMethod();
                       // supElementForSimplex();
                        nextStepSM();

                    }
                    else
                    {
                        CreateTableForBasisMethod();
                        supElementForBasis();
                    }
                }

            }
            if(solutionMethod == GraphicalMethod)
            {
                typeOfBasis = Selective;
                MainTableFraction.Clear();
                listOfBasis.Clear();
                listOfParams.Clear();

                CompositeNumber.typeOfFraction = typeOfFraction;
                if (CreateTable())
                {
                    BasisVariables form = new BasisVariables(columns, rows);
                    form.ShowDialog();
                    listOfBasis = form.getListOfBasis();
                    listOfParams = form.getListOfParams();
                    Gauss matrix = new Gauss(MainTableFraction, listOfBasis, listOfParams, listOfFuncFraction);
                    matrix.changeMatrix();
                    matrix.GaussMethod();
                    MainTableFraction = matrix.getFinalMatrix();
                }
                GraphicalMethod form_graph = new GraphicalMethod(MainTableFraction, typeOfTask, listOfBasis, listOfParams,listOfFuncFraction);
                form_graph.ShowDialog();
            }
  
        }
        //---------------------------------------------------------------------------------

        //---------------------------------------------------------------------------------
        //Методы создания таблиц
        bool CreateTable()
        {
            try
            {
                int index = 0;

                DataGridViewRow row = FunctionDisplay.Rows[0];
                List<CompositeNumber> tmp_ListOfRow = new List<CompositeNumber>();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    CompositeNumber tmp_Number = CompositeNumber.Parse((String)cell.Value);
                    tmp_ListOfRow.Add(tmp_Number);
                }
                listOfFuncFraction = tmp_ListOfRow;
                if (typeOfTask == Maximize)
                {
                    for (int i = 0; i < listOfFuncFraction.Count; ++i)
                    {
                        listOfFuncFraction[i] = -listOfFuncFraction[i];
                    }
                }
                for (int i = 1; i <= RestrictionsDisplay.Columns.Count - 1; ++i)
                    listOfParams.Add(i);

                foreach (DataGridViewRow elementRow in RestrictionsDisplay.Rows)
                {
                    List<CompositeNumber> tmp_RowList = new List<CompositeNumber>();
                    listOfBasis.Add(RestrictionsDisplay.Columns.Count + index);
                    listOfTags.Add(RestrictionsDisplay.Columns.Count + index);
                    foreach (DataGridViewCell cell in elementRow.Cells)
                    {
                        CompositeNumber tmp_Number = CompositeNumber.Parse((String)cell.Value);
                        tmp_RowList.Add(tmp_Number);
                    }
                    MainTableFraction.Add(HelperClass.OppositeListOfNumber(tmp_RowList));
                    ++index;
                }
                if (typeOfBasis == Artificial)
                {
                    List<CompositeNumber> sumRow = new List<CompositeNumber>();
                    for (int i = 0; i < RestrictionsDisplay.Columns.Count; ++i)
                    {
                        CompositeNumber sum = new CompositeNumber(0, 1);
                        for (int j = 0; j < RestrictionsDisplay.Rows.Count; ++j)
                        {
                            sum += MainTableFraction.ElementAt(j).ElementAt(i);
                        }
                        sumRow.Add(-sum);
                    }
                    MainTableFraction.Add(sumRow);
                }
            }
            catch (Exception e) { MessageBox.Show(e.Message); return false; }
            return true;
        }

        void CreateTableForBasisMethod()
        {
            NextStepBtn.Visible = true;
            PreviousStepBtn.Visible = true;
            mainTabControl.SelectedTab = artificialBasisPage;

            artificialBasisVisualization.Columns.Clear();
            artificialBasisVisualization.Rows.Clear();

            updateTable(artificialBasisVisualization);


        }
        void CreateTableForSimplexMethod()
        {
            mainTabControl.SelectedTab = simplexMethodPage;
            nextStepSMBtn.Visible = true;
            prevStepSMBtn.Visible = true;

            simplexMethodVisualization.Columns.Clear();
            simplexMethodVisualization.Rows.Clear();
            Gauss matrix = new Gauss(MainTableFraction, listOfBasis, listOfParams, listOfFuncFraction);
            matrix.changeMatrix();
            matrix.GaussMethod();
            MainTableFraction = matrix.getFinalMatrix(); ;

            updateTable(simplexMethodVisualization);
        }
        //---------------------------------------------------------------------------------

        //---------------------------------------------------------------------------------
        //Обновить значения в таблице
        void updateTable(DataGridView dataGridView)
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
        //Вывести ответ
        void CreateAnswer()
        {
            string ans = "Ответ: x = (";
            List<CompositeNumber> answer = new List<CompositeNumber>();
            for (int i = 0; i < MainTableFraction[0].Count + MainTableFraction.Count - 2; i++)
            {
                answer.Add(CompositeNumber.Null);
            }
            for (int i = 0; i < listOfBasis.Count; i++)
            {
                answer[listOfBasis[i] - 1] = MainTableFraction[i][MainTableFraction[i].Count - 1];
            }
            for (int i = 0; i < answer.Count; i++)
            {
                ans += answer[i];
                if (i != (answer.Count - 1))
                {
                    ans += ", ";
                }
            }
            if(typeOfTask == Maximize) // Если изначально задача стояла в максимизации
            {
                ans += "), f(x) = " + (MainTableFraction[MainTableFraction.Count - 1][MainTableFraction[0].Count - 1]);
                labelForResponse.Text = ans;
            }
            else // Если изначально задача стояла в минимизации
            {
                ans += "), f(x) = " + (-MainTableFraction[MainTableFraction.Count - 1][MainTableFraction[0].Count - 1]);
                labelForResponse.Text = ans;
            }
            GomoryBtn.Visible = true;

        }

        private void LoadBtn_Click(object sender, EventArgs e)
        {
            listOfFuncFraction.Clear();
            MainTableFraction.Clear();

            var fileContent = string.Empty;
            var filePath = string.Empty;


            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    String line = "";
                    try
                    {


                       //---------------------------------------------------------------
                        StreamReader sr = new StreamReader(filePath);
                        line = sr.ReadLine();//1
                        NumOfVariablesTextBox.Text = line;
                        line = sr.ReadLine();//2
                        NumOfRestrictionsTextBox.Text = line;
                        line = sr.ReadLine();
                        TypeOfTaskcomboBox.Text = line;
                        line = sr.ReadLine();
                        OptimizationComboBox.Text = line;
                        line = sr.ReadLine();
                        BasisComboBox.Text = line;
                        line = sr.ReadLine();
                        TypeOfFractionsComboBox.Text = line;
                        //---------------------------------------------------------------
                        line = sr.ReadLine();
                        String[] lineFunc = line.Split(' ');
                        for (int i = 0; i < lineFunc.Length - 1; ++i)
                        {
                            listOfFuncFraction.Add(CompositeNumber.Parse(lineFunc[i]));
                        }
                        //Continue to read until you reach end of file
                        line = sr.ReadLine();
                        int index = 0;
                        while (line != null)
                        {

                            String[] lineSimp = line.Split(' ');
                            MainTableFraction.Add(new List<CompositeNumber>());
                            for (int i = 0; i < lineSimp.Length - 1; ++i)
                            {
                                MainTableFraction[index].Add(CompositeNumber.Parse(lineSimp[i]));
                            }
                            ++index;
                            line = sr.ReadLine();
                        }
                        
                        sr.Close();
                        Console.ReadLine();
                    }
                    catch (Exception eexc)
                    {
                        MessageBox.Show("Exception: " + eexc.Message);
                    }
                }



            }

            // Целевая функция
            FunctionDisplay.Rows.Clear();
            FunctionDisplay.Columns.Clear();
            for (int i = 1; i < listOfFuncFraction.Count + 1; ++i)
            {
                FunctionDisplay.Columns.Add("x" + i, "x" + i);
            }

            string[] funcRow = new string[16];
            Array.Resize(ref funcRow, MainTableFraction[0].Count);
            for (int i = 0; i < listOfFuncFraction.Count; ++i)
            {
                funcRow[i] = listOfFuncFraction[i].ToString();
            }
            FunctionDisplay.Rows.Add(funcRow);
            FunctionDisplay.Rows[0].HeaderCell.Value = "f(x)";

            // Ограничения
            RestrictionsDisplay.Rows.Clear();
            RestrictionsDisplay.Columns.Clear();
            for (int i = 1; i < MainTableFraction[0].Count; ++i)
            {
                RestrictionsDisplay.Columns.Add("c" + i, "c" + i);
            }
            RestrictionsDisplay.Columns.Add("b", "b");

            string[] limitRow = new string[18];
            Array.Resize(ref limitRow, MainTableFraction[0].Count);
            for (int i = 0; i < MainTableFraction.Count; ++i)
            {
                for (int j = 0; j < MainTableFraction[i].Count; ++j)
                {
                    limitRow[j] = MainTableFraction[i][j].ToString();
                }
                RestrictionsDisplay.Rows.Add(limitRow);
                RestrictionsDisplay.Rows[i].HeaderCell.Value = "f" + i + "(x)";
            }


        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (CreateTable())
            {

                Stream myStream;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                                           
                        myStream.Close();
                    }
                }

                try
                {
                    
                    StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                    sw.WriteLine(NumOfVariablesTextBox.Text);
                    sw.WriteLine(NumOfRestrictionsTextBox.Text);
                    sw.WriteLine(TypeOfTaskcomboBox.Text);
                    sw.WriteLine(OptimizationComboBox.Text);
                    sw.WriteLine(BasisComboBox.Text);
                    sw.WriteLine(TypeOfFractionsComboBox.Text);
                    for (int i = 0; i < listOfFuncFraction.Count; i++)
                    {
                        if (typeOfTask == Maximize)
                        {
                            sw.Write(-listOfFuncFraction[i] + " ");
                        }
                        else
                        {
                            sw.Write(listOfFuncFraction[i] + " ");
                        }
                        
                    }
                    sw.WriteLine("");
                    for (int i = 0; i < MainTableFraction.Count-1; i++)
                    {
                        for (int j = 0; j < MainTableFraction[i].Count; j++)
                        {
                            sw.Write(MainTableFraction[i][j] + " ");
                        }
                        sw.WriteLine("");
                    }
                    
                    sw.Close();
                }
                catch (Exception eexc)
                {
                    MessageBox.Show("Exception: " + eexc.Message);
                }
            }
        }

        private void InfoBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Эта программа – результат лабораторной работы по Методам Оптимизации.\n" +
                                   " В ней реализовано следующее:\n" +
                               " 1)Возможность диалогового ввода размерности задачи и матрицы коэффициентов целевой функции в канонической форме.Размерность не более 16 * 16.\n" +
                               " 2)Сохранение введённой задачи в файл и чтение из файла.\n" +
                               " 3)Возможность решения задачи с использованием заданных базисных переменных.\n" +
                               " 4)Реализация метода искусственного базиса.\n" +
                               " 5)Выбор автоматического и пошагового режима решения задачи.\n" +
                               " 6)В пошаговом режиме возможность возврата назад.\n" +
                               " 7)В пошаговом режиме возможность выбора опорного элемента.\n" +
                               " 8)Работа с обыкновенными и десятичными дробями.\n" +
                               " 9)Справка.\n" +
                               " 10)Контекстно - зависимая помощь.\n" +
                               " 11)Поддержка мыши.\n" +
                               " 12)Контроль данных(защита от «дурака».", "Справка" );
        }

        private void GomoryBtn_Click(object sender, EventArgs e)
        {
            Gomory gomoryForm = new Gomory(MainTableFraction, typeOfTask, listOfBasis, listOfParams);
            gomoryForm.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ToolTip t1 = new ToolTip();
            t1.SetToolTip(SaveBtn, "Сохранение");
            ToolTip t2 = new ToolTip();
            t2.SetToolTip(LoadBtn, "Загрузка");
            ToolTip t3 = new ToolTip();
            t3.SetToolTip(InfoBtn, "Справка");
        }

       

        //Создание нового базиса
        void nextTableBasis(int n, int m)
        {

            int columns = MainTableFraction[0].Count;
            int rows = MainTableFraction.Count;

            
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


            // обновляет таблицу
            if(typeOfBasis == Artificial && HelperClass.checkingForContentElement(listOfTags,listOfParams, m))
            {
                MainTableFraction = HelperClass.deleteColumn(m, MainTableFraction);
                listOfParams.RemoveAt(m);
                
            }
           
        }
        //---------------------------------------------------------------------------------



        //---------------------------------------------------------------------------------
        //Обработчики для simplexMethodPage
        //Предыдущий шаг Симплекс метода
        private void prevStepSMBtn_Click(object sender, EventArgs e)
        {
          
            if (listOfHistoryTable.Count == 0)
            {
                MessageBox.Show("Предыдущий шаг отсутствует");
                return;
            }
            MainTableFraction = listOfHistoryTable[listOfHistoryTable.Count - 1];
            listOfHistoryTable.RemoveAt(listOfHistoryTable.Count - 1);

            listOfParams = listOfHistoryParams[listOfHistoryParams.Count - 1];
            listOfHistoryParams.RemoveAt(listOfHistoryParams.Count - 1);

            listOfBasis = listOfHistoryBasis[listOfHistoryBasis.Count - 1];
            listOfHistoryBasis.RemoveAt(listOfHistoryBasis.Count - 1);

            updateTable(simplexMethodVisualization);
            supElementForSimplex();
            if (supElementForSimplex() == 1)
            {
                MessageBox.Show("Решение найдено");
                nextStepSMBtn.Visible = false;
                CreateAnswer();
                return;
            }
            nextStepSMBtn.Visible = true;

        }
        //Следующий шаг Симплес метода
        private void nextStepSMBtn_Click(object sender, EventArgs e)
        {
            checkingForNullRow();
            updateTable(simplexMethodVisualization);
            nextTableBasis(choosedRow, choosedCol);
            updateTable(simplexMethodVisualization);
            nextStepSM();

        }
        //Выбор нужного нам опорного элемента
        private void simplexMethodVisualization_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == MainTableFraction.Count - 1 || e.ColumnIndex == MainTableFraction[0].Count - 1
                || MainTableFraction[e.RowIndex][e.ColumnIndex] < CompositeNumber.Null
                || !(MainTableFraction[MainTableFraction.Count - 1][e.ColumnIndex] < CompositeNumber.Null))
            {
                MessageBox.Show("Данную ячейку нельзя выбрать");
                return;
            }
            choosedRow = e.RowIndex;
            choosedCol = e.ColumnIndex;
        }
        //Показывает все оптимальные опорные элементы
        int supElementForSimplex()

        {
            CompositeNumber supplement = new CompositeNumber(100, 1);
            if (HelperClass.checkingForInvalidColumn(MainTableFraction))
            {
                
                return -1;
            }
            if (HelperClass.checkingForInvalidRow(MainTableFraction))
            {
                return -2;
            }
            int globalMinRow = -1, globalMinColumn = -1;
            int count = 0;
            listOfSupElements.Clear();
            CompositeNumber globalMin = HelperClass.findMaxInMatrix(MainTableFraction) * supplement;

            int columns = MainTableFraction[0].Count - 1;
            int rows = MainTableFraction.Count - 1;

            
            foreach (CompositeNumber elemOfRow in MainTableFraction[rows])
            {
                if (elemOfRow > CompositeNumber.Null|| elemOfRow == CompositeNumber.Null)
                {
                    ++count;
                }
            }

            if (count == MainTableFraction[rows].Count) ///???? может быть так, что посл. стр. пол., а своб. член отр.
            {
                return 1;
            }

            for (int i = 0; i < columns; ++i)
            {
                CompositeNumber currElement = MainTableFraction[rows][i];
                if (currElement < CompositeNumber.Null)
                {
                    CompositeNumber localMin = HelperClass.findMaxInMatrix(MainTableFraction);
                    int localMinRow = -1, localMinColumn = -1;
                    for (int j = 0; j < rows; ++j)
                    {
                        CompositeNumber rightElem = MainTableFraction[j][columns];
                        CompositeNumber applicantElem = MainTableFraction[j][i];
                        if (applicantElem > CompositeNumber.Null
                            && localMin > (rightElem / applicantElem))
                        {
                            localMinRow = j;
                            localMinColumn = i;
                            localMin = MainTableFraction[j][columns] /
                            MainTableFraction[j][i];
                        }
                        if (globalMin > localMin)
                        {
                            globalMin = localMin;
                           globalMinRow = localMinRow;
                            globalMinColumn = localMinColumn;
                        }
                    }
                    if (localMinRow != -1 && localMinColumn != -1)
                    {
                        listOfSupElements.Add((localMinRow, localMinColumn));
                    }
                    else
                    {
                        
                        labelForResponse.Text = "Функция неограничена снизу"; //???
                    }
                }
            }

            this.mostRow = globalMinRow;
            this.mostCol = globalMinColumn;

            this.choosedCol = this.mostCol;
            this.choosedRow = this.mostRow;

            simplexMethodVisualization.CurrentCell.Selected = false;

            if (choosedCol == -1 || choosedRow == -1)
            {
                return 1;
            }
            foreach ((int, int) curr in listOfSupElements)
            {
                simplexMethodVisualization.Rows[curr.Item1].Cells[curr.Item2].Style.BackColor = Color.Green;
            }
            simplexMethodVisualization.Rows[mostRow].Cells[mostCol].Style.BackColor = Color.Yellow;
            return 0;
        }
        //---------------------------------------------------------------------------------


        //---------------------------------------------------------------------------------
        //Обработчики для artificialBasisPage
        //Показывает все оптимальные опорные элементы
        int supElementForBasis()
        {
            CompositeNumber supplement = new CompositeNumber(100, 1);
            if (HelperClass.checkingForInvalidColumn(MainTableFraction))
            {
                
                return -1;
            }
            int globalMinRow = -1, globalMinColumn = -1;
            listOfSupElements.Clear();
            CompositeNumber globalMin = HelperClass.findMaxInMatrix(MainTableFraction) * supplement;
            int count = 0;
            int columns = MainTableFraction[0].Count - 1;
            int rows = MainTableFraction.Count - 1;



            foreach (CompositeNumber elemOfRow in MainTableFraction[rows])
            {
                if (elemOfRow == CompositeNumber.Null)
                {
                    ++count;
                }
            }

            if (count == MainTableFraction[rows].Count)
            {
                
                return 1;
            }

            for (int i = 0; i < columns; ++i)
            {
                CompositeNumber currElement = MainTableFraction[rows][i];
                if (currElement < CompositeNumber.Null)
                {
                    CompositeNumber localMin = HelperClass.findMaxInMatrix(MainTableFraction);
                    int localMinRow = -1, localMinColumn = -1;
                    for (int j = 0; j < rows; ++j)
                    {
                        CompositeNumber rightElem = MainTableFraction.ElementAt(j).ElementAt(columns);
                        CompositeNumber applicantElem = MainTableFraction.ElementAt(j).ElementAt(i);
                        if (applicantElem > CompositeNumber.Null
                            && localMin > (rightElem / applicantElem)
                            )
                        {
                            localMinRow = j;
                            localMinColumn = i;
                            localMin = MainTableFraction.ElementAt(j).ElementAt(columns) /
                            MainTableFraction.ElementAt(j).ElementAt(i);
                        }
                        if (globalMin > localMin)
                        {
                            globalMin = localMin;
                            globalMinRow = localMinRow;
                            globalMinColumn = localMinColumn;
                        }
                    }
                    if (localMinRow != -1 && localMinColumn != -1)
                    {
                        listOfSupElements.Add((localMinRow, localMinColumn));
                    }
                    else
                    {
                        MessageBox.Show("Система несовместна");
                    }
                }
                
            }

            if (globalMinRow == -1 && globalMinColumn == -1)
            {

                
                return 1;
            }

            this.mostRow = globalMinRow;
            this.mostCol = globalMinColumn;

            this.choosedCol = this.mostCol;
            this.choosedRow = this.mostRow;

            artificialBasisVisualization.CurrentCell.Selected = false;

            foreach ((int, int) curr in listOfSupElements)
            {
                artificialBasisVisualization.Rows[curr.Item1].Cells[curr.Item2].Style.BackColor = Color.Green;
            }
            artificialBasisVisualization.Rows[mostRow].Cells[mostCol].Style.BackColor = Color.Yellow;
            return 0;
        }

        private void TransitionBtn_Click(object sender, EventArgs e)
        {
            checkingForNullRow ();
            listOfHistoryTable.Clear();
            labelForResponse.Text = "";
            mainTabControl.SelectedTab = simplexMethodPage;
            nextStepSMBtn.Visible = true;
            prevStepSMBtn.Visible = true;

            simplexMethodVisualization.Columns.Clear();
            simplexMethodVisualization.Rows.Clear();
            Gauss matrix = new Gauss(MainTableFraction, listOfBasis, listOfParams, listOfFuncFraction);
            MainTableFraction = matrix.getTransitMatrix(); ;
            updateTable(simplexMethodVisualization);
            supElementForSimplex();
            if (supElementForSimplex() == 1)
            {
                MessageBox.Show("Решение найдено");
                nextStepSMBtn.Visible = false;
                CreateAnswer();
                return;
            }
        }
       

        private void artificialBasisVisualization_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == MainTableFraction.Count - 1 || e.ColumnIndex == MainTableFraction[0].Count - 1
                || MainTableFraction[e.RowIndex][e.ColumnIndex] < CompositeNumber.Null
                || !(MainTableFraction[MainTableFraction.Count - 1][e.ColumnIndex] < CompositeNumber.Null))
            {
                MessageBox.Show("Данную ячейку нельзя выбрать");
                return;
            }

            choosedRow = e.RowIndex;
            choosedCol = e.ColumnIndex;
        }
        //Следующий шаг
        private void NextStepBtn_Click(object sender, EventArgs e)
        {
            checkingForNullRow ();
            updateTable(artificialBasisVisualization);
            nextTableBasis(choosedRow, choosedCol);
            updateTable(artificialBasisVisualization);
            if (supElementForBasis()== 1)
            {
               
                MessageBox.Show("Поиск базисного решения завершен");
                NextStepBtn.Visible = false;
                TransitionBtn.Visible = true;
                return;
            }
            if(supElementForBasis() == -1)
            {
                NextStepBtn.Visible = false;
                MessageBox.Show("Система несовместна");
                return;
            }
            
            
        }
        //Предыдущий шаг
        private void PreviousStepBtn_Click(object sender, EventArgs e)
        {
            NextStepBtn.Visible = true;
            if (listOfHistoryTable.Count == 0)
            {
                MessageBox.Show("Предыдущий шаг отсутствует");
                return;
            }
            MainTableFraction = listOfHistoryTable[listOfHistoryTable.Count - 1];
            listOfHistoryTable.RemoveAt(listOfHistoryTable.Count - 1);

            listOfParams = listOfHistoryParams[listOfHistoryParams.Count - 1];
            listOfHistoryParams.RemoveAt(listOfHistoryParams.Count - 1);

            listOfBasis = listOfHistoryBasis[listOfHistoryBasis.Count - 1];
            listOfHistoryBasis.RemoveAt(listOfHistoryBasis.Count - 1);

            updateTable(artificialBasisVisualization);
            supElementForBasis();
        }

       //Метод, удаляющий нулевую строку
        void checkingForNullRow ()
        {
            for (int row = 0; row < MainTableFraction.Count - 1; ++row)
            {
                if (listOfBasis[row] >= MainTableFraction.Count - 1)
                {
                    bool check = true;
                    for (int column = 0; column < MainTableFraction[row].Count - 1; ++column)
                    {
                        if (listOfParams[column] < MainTableFraction.Count - 1 && MainTableFraction[row][column] != CompositeNumber.Null)
                        {
                            check = false;
                        }
                    }
                    if (MainTableFraction[row][MainTableFraction[row].Count - 1] != CompositeNumber.Null)
                    {
                        check = false;
                    }
                    if (check)
                    {
                        int column = MainTableFraction[row].Count;
                        for (int elemntOfColumn = 0; elemntOfColumn < column; ++elemntOfColumn)
                        {
                            MainTableFraction[row].RemoveAt(0);
                        }
                        MainTableFraction.RemoveAt(row);
                        listOfBasis.RemoveAt(row);
                    }
                }
            }
        }
        void nextStepSM()
        {
            switch (supElementForSimplex())
            {
                case 1:
                    MessageBox.Show("Решение найдено");
                    nextStepSMBtn.Visible = false;
                    CreateAnswer();
                    return;
                case -1:
                    if (typeOfTask == Maximize)
                    {
                        MessageBox.Show("Функция неограничена сверху");
                        nextStepSMBtn.Visible = false;
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Функция неограничена снизу");
                        nextStepSMBtn.Visible = false;
                        return;
                    }
                case -2:
                    MessageBox.Show("Система несовместна");
                    nextStepSMBtn.Visible = false;
                    labelForResponse.Text = "Система несовместна";
                    return;
                default:
                    return;
            }

        }
        
        }
        //---------------------------------------------------------------------------------
    }
    
