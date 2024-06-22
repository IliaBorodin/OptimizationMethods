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
    public partial class BasisVariables : Form
    {
        private List<Int32> listOfParams = new List<Int32>();
        private List<Int32> listOfBasis = new List<Int32>();
        private int countOfVariables;
        private int countOfRestrictions;

        public BasisVariables(int countOfVariables, int countOfRestrictions)
        {
            this.countOfVariables = countOfVariables;
            this.countOfRestrictions = countOfRestrictions;
            InitializeComponent();

        }
        public List<Int32> getListOfParams()
        {
            return listOfParams;
        }

        public List<Int32> getListOfBasis()
        {
            return listOfBasis;
        }

       



        private void BasisVariables_Load_1(object sender, EventArgs e)
        {
            for (int i = 0; i < countOfVariables; ++i)
            {

                int x = i + 1;
                BasisVariablesGriedView.Rows.Add(x.ToString());
            }
        }

        private void SetBasisBtn_Click(object sender, EventArgs e)
        {
            //Очистим списки
            listOfParams.Clear();
            listOfBasis.Clear();
            int count = BasisVariablesGriedView.Rows.Cast<DataGridViewRow>().Where(p => Convert.ToBoolean(p.Cells["Column1"].Value) == true).Count();
            if (count == countOfRestrictions)
            {
                for (int i = BasisVariablesGriedView.RowCount - 1; i >= 0; --i)
                {
                    DataGridViewRow row = BasisVariablesGriedView.Rows[i];
                    if (Convert.ToBoolean(row.Cells["Column1"].Value) == true)
                    {
                        int basis_variable = int.Parse((string)row.Cells["Variable"].Value);
                        listOfBasis.Add(basis_variable);
                    }
                    else
                    {
                        int variable = int.Parse((string)row.Cells["Variable"].Value);
                        listOfParams.Add(variable);
                    }

                }
                listOfBasis.Sort();
                listOfParams.Sort();
                //--------------------------------------------------------------------
               

                //--------------------------------------------------------------------


                string message = "Успешно задан базис";
                MessageBox.Show(message, "Message", MessageBoxButtons.OK);
                this.Close();
            }
            else
            {
                string message1 = "Неверно задан базис";
                MessageBox.Show(message1, "Message", MessageBoxButtons.OK);
            }
        }
    }
}
