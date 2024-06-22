using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeLab
{
    static class HelperClass
    {

        //Проверка корректности ввода
        public static int checkingTheInput(int columns, int rows)
        {
            if (columns > 16)
            {
                MessageBox.Show("Число переменных не должно превышать 16\n");
                return 0;
            }
            else if (rows > 16)
            {
                MessageBox.Show("Число ограничений не должно превышать 16\n");
                return 0;
            }
            else if (columns < 1)
            {
                MessageBox.Show("Число переменных не должно быть меньше 1\n");
                return 0;
            }
            else if (rows < 1)
            {
                MessageBox.Show("Число ограничений не должно быть меньше 1\n");
                return 0;
            }

            else if (columns < rows)
            {
                MessageBox.Show("Число переменных не должно быть меньше числа ограничений\n");
                return 0;
            }
            else
            {
                return 1;
            }


        }
        //Создание таблицы целевой ф-и 
        public static void completionFuncTable(int columns, int rows, DataGridView table)
        {
            for (int i = 1; i < columns + 1; ++i)
            {
                table.Columns.Add("x" + i, "x" + i);
            }
            
            string[] funcRow = new string[16];
            Array.Resize(ref funcRow, columns);
            for (int i = 0; i < columns; ++i)
            {
                funcRow[i] = "";
            }
            table.Rows.Add(funcRow);
            table.Rows[0].HeaderCell.Value = "f(x)";
        }
        //Создание таблицы ограничений
        public static void completionRestrictionsTable(int columns, int rows, DataGridView table)
        {

            //Заполнение таблицы ограничений
            for (int i = 1; i < columns + 1; ++i)
            {
                table.Columns.Add("c" + i, "c" + i);
            }
            table.Columns.Add("b", "b");

            string[] limitRow = new string[18];
            Array.Resize(ref limitRow, columns);
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < columns; ++j)
                {
                    limitRow[j] = "";
                }
                table.Rows.Add(limitRow);
                table.Rows[i].HeaderCell.Value = "f" + (i+1) + "(x)";
            }


        }
        //Инвертируем значения в списке
       public static List<CompositeNumber> OppositeListOfNumber(List<CompositeNumber> list)
        {
            if (list[list.Count - 1] < CompositeNumber.Null)
            {
                for (int i = 0; i < list.Count; ++i)
                    list[i] = -list[i];
            }
            return list;

        }
        //Инициализация матрицы по заданным значениям
        public static List<List<CompositeNumber>> InitMatrix(int rows, int columns)
        {
            List<List<CompositeNumber>> matrix = new List<List<CompositeNumber>>();
            for(int i = 0; i < rows; ++i)
            {
                List<CompositeNumber> list = new List<CompositeNumber>();
                for( int j = 0; j < columns; ++j)
                {
                    list.Add(CompositeNumber.Null);
                }
                matrix.Add(list);
            }
            return matrix;
        }
        //Удалить столбец
        public static List<List<CompositeNumber>> deleteColumn(int column, List<List<CompositeNumber>> matrix )
        {
            foreach (var list in matrix) 
            {
                list.RemoveAt(column);
            }
            return matrix;
        }
        //Содержит ли базис в себе искуственную переменную
        public static bool checkingForContentElement(List<Int32> listTags, List<Int32> listBasis, int index)
        {
            int flag = 0;
            for(int i=0; i < listTags.Count; ++i)
            {
                if (listBasis[index] == listTags[i])
                    flag = 1;
            }
            if (flag == 1)
                return true;
            else
                return false;
        }
        //Нахождение строки с неположительными элементами
        public static bool checkingForInvalidColumn(List<List<CompositeNumber>> matrix)
        {
            int flag_0 = 0;
            for (int i = 0; i < matrix[0].Count; ++i)
            {
                if (matrix[matrix.Count - 1][i] == CompositeNumber.Null)
                {
                    ++flag_0;
                }
            }
            for (int column = 0; column < matrix[0].Count - 1; ++column)
            {
                int count = 0;
                for (int row = 0; row < matrix.Count; ++row)
                {
                    if (matrix[row][column] < CompositeNumber.Null || matrix[row][column] == CompositeNumber.Null)
                    {
                        ++count;
                    }

                }
                if (count == matrix.Count && flag_0 != matrix[0].Count)
                    return true;
            }


            return false;
        }

        public static bool checkingForInvalidRow(List<List<CompositeNumber>> matrix)
        {
            
            for(int row = 0; row < matrix.Count; ++row)
            {
                int count = 0;
                for (int column = 0; column < matrix[0].Count; ++column)
                {
                    if (matrix[row][column] < CompositeNumber.Null)
                        ++count;

                }
                if (count == matrix[0].Count)
                    return true;
            }
            return false;
        }
        //Нахождение max-element в матрице
        public static CompositeNumber findMaxInMatrix(List<List<CompositeNumber>> matrix)
        {
            CompositeNumber max = new CompositeNumber(-1, 1);
            CompositeNumber ten = new CompositeNumber(10, 1);
            for (int i=0; i < matrix.Count; ++i)
            {
                for(int j=0; j < matrix[0].Count; ++j)
                {
                    if(max < matrix[i][j])
                    {
                        max = matrix[i][j];
                    }
                }
               
            }
            return max + ten;
        }
        //алгоритм Евклида
        public static long gcd(long a, long b)
        {
            while (b != 0)
                b = a % (a = b);
            return a;

        }
    }
}
