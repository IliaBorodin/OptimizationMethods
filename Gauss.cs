using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeLab
{
    class Gauss
    {
        private List<List<CompositeNumber>> matrix;
        private List<Int32> listOfBasis = new List<Int32>();
        private List<Int32> listOfParams = new List<Int32>();
        private List<CompositeNumber> listOfFuncFraction = new List<CompositeNumber>();
        

       
        public Gauss(List<List<CompositeNumber>> matrix, List<Int32> listOfBasis, List<Int32> listOfParams, List<CompositeNumber> listOfFuncFraction)
        {
            this.matrix = matrix;
            this.listOfBasis = listOfBasis;
            this.listOfParams = listOfParams;
            this.listOfFuncFraction = listOfFuncFraction;
            
        }

        public List<List<CompositeNumber>> GetMatrix()
        {
            return this.matrix;
        }
        public void swapLines(int line1, int line2)
        {
            for (int index = 0; index < matrix[line1].Count; ++index)
            {
                CompositeNumber tmp_number = matrix[line1][index];
                matrix[line1][index] = matrix[line2][index];
                matrix[line2][index] = tmp_number;
            }
        }
        public void swapColumns(int column1, int column2)
        {
            for(int index = 0; index< matrix.Count; ++index)
            {
                CompositeNumber tmp_number = matrix[index][column1];
                matrix[index][column1] = matrix[index][column2];
                matrix[index][column2] = tmp_number;
            }
        }
      public  void minusLines(int line1, int line2, CompositeNumber coef)
        {
            for (int index = 0; index < matrix[line1].Count; ++index)
            {
                matrix[line1][index] -= matrix[line2][index] * coef;
            }
        }
      public  void plusLines(int line1, int line2, CompositeNumber coef)
        {
            for (int index = 0; index< matrix[line1].Count; ++index)
            {
                matrix[line1][index] += matrix[line2][index] * coef;
            }
        }
       public void divideLines(int line, CompositeNumber coef)
        {
            for (int index = 0; index < matrix[line].Count; ++index)
            {
                matrix[line][index] /= coef;
            }
        }
        public void changeMatrix()
        {
            List<Int32> list = new List<Int32>(listOfBasis.Concat(listOfParams).ToList());
            CompositeNumber[,] tmp_list = new CompositeNumber[matrix.Count, matrix[0].Count];
            int rows = matrix.Count;
            int columns = matrix[0].Count;
            for (int i=0; i < rows; ++i)
            {
                for (int j = 0; j < columns-1; ++j)
                {
                    tmp_list[i,j] = matrix[i][list[j]-1];
                }

                tmp_list[i, columns - 1] = matrix[i][columns-1]; ;

            }
            
            for(int i = 0; i <rows; ++i)
            {
                for(int j=0;j< columns; ++j)
                {
                    matrix[i][j] = tmp_list[i, j];
                }
            }
        }

        public void GaussMethod()
        {
            // прямой ход
            for (int row = 0; row < matrix.Count; ++row)
            {
                if (matrix[row][row] != CompositeNumber.Null)
                {
                    divideLines( row, matrix[row][row]);
                    for (int j = row + 1; j < matrix.Count; ++j)
                    {
                        minusLines( j, row, matrix[j][row]);
                    }
                }
                else
                {
                    for (int j = row + 1; j < matrix.Count; ++j)
                    {
                        if (matrix[j][row] != CompositeNumber.Null)
                        {
                            swapLines(row, j);
                            break;
                        }
                    }
                }
            }

            // обратный ход
            for (int i = matrix.Count - 1; i > 0; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    minusLines(j, i, matrix[j][i]);
                }
            }
            return;
        }

        public List<List<CompositeNumber>> getFinalMatrix()
        {
            
            int rows = matrix.Count + 1;
            int columns = matrix[0].Count - listOfBasis.Count();
            List<List<CompositeNumber>> finalMatrix = HelperClass.InitMatrix(rows, columns);
            CompositeNumber sum = CompositeNumber.Null;
            CompositeNumber[,] tmp_list = new CompositeNumber[rows, columns];
            CompositeNumber minus_one = new CompositeNumber(-1, 1);
            for(int i = 0; i < matrix.Count; ++i)
            {
                for(int j=0; j< matrix[0].Count - listOfBasis.Count(); ++j)
                {
                    tmp_list[i, j] = matrix[i][j + listOfBasis.Count()];
                }
            }
            for(int i = 0; i < columns-1; ++i)
            {
                for(int j=0; j < matrix.Count; ++j)
                {
                    sum += (minus_one * tmp_list[j, i]) * listOfFuncFraction[listOfBasis[j]-1]; 

                }
                sum += listOfFuncFraction[listOfParams[i] - 1];
                tmp_list[rows-1, i] = sum;
                sum = CompositeNumber.Null;
            }
            for(int i=0;i< matrix.Count; ++i)
            {
                sum += tmp_list[i, columns - 1] * listOfFuncFraction[listOfBasis[i] - 1];
            }
            tmp_list[rows - 1, columns - 1] = -sum;
            
            for(int i = 0; i < rows; ++i)
            {
                for(int j = 0; j < columns; ++j)
                {
                    finalMatrix[i][j] = tmp_list[i, j];
                }
            }
            return finalMatrix;
        }


        public List<List<CompositeNumber>> getTransitMatrix()
        {
            int rows = matrix.Count;
            int columns = matrix[0].Count;
            List<List<CompositeNumber>> finalMatrix = HelperClass.InitMatrix(rows, columns);
            CompositeNumber sum = CompositeNumber.Null;
            CompositeNumber minus_one = new CompositeNumber(-1, 1);
            for(int i =0; i < rows - 1; ++i)
            {
                for(int j=0;j<columns; ++j)
                {
                    finalMatrix[i][j]= matrix[i][j]; 
                }
            }
            for (int i = 0; i < columns - 1; ++i)
            {
                for (int j = 0; j < rows-1; ++j)
                {
                    sum += (minus_one * finalMatrix[j][i]) * listOfFuncFraction[listOfBasis[j] - 1];

                }
                sum += listOfFuncFraction[listOfParams[i] - 1];
                finalMatrix[rows - 1][i] = sum;
                sum = CompositeNumber.Null;
            }
            for (int i = 0; i < rows-1; ++i)
            {
                sum += finalMatrix[i][columns - 1] * listOfFuncFraction[listOfBasis[i] - 1];
                
            }
            finalMatrix[rows - 1][columns - 1] = -sum;

            
            return finalMatrix;
        }

    }

        
    }


