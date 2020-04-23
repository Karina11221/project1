using System;
using System.IO;
using System.Text;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;
using simplex.Classes;
using Mehroz;

namespace simplex.Classes
{
    class SimplexTable
    {
        // Кол-во уравнений
        private int n;
        // Кол-во ограничений
        private int m;
        // Кол-во строк симплекс-таблицы
        int rows;           
        // Кол-во столбцов симплекс-таблицы
        int cols;
        // Разрешающая строка
        int pivotRow = - 1;         
        // Разрешающий столбец
        int pivotCol = - 1;         
        // Тип решаемой задачи (Максимизация '0' или минимизация '1')
        private int tasktype;
        // Флаг ручного пересчета таблицы
        private bool manuallyRecalcFlag;

        // Флаг разрешимой задачи
        private bool solved = false;
        // Флаг оптимального нецелочисленного плана
        private bool nonintegersolved = false;        
        // Флаг бесконечного кол-во решений задачи
        private bool infinity = false;
        // Флаг отсутствие допустимого решения задачи
        private bool unsolvable = false;         
        
        // Симплекс-таблица
        //double[,] table = new double[100, 100];

        // Симплекс-таблица в виде обыкновенных дробей
        Fraction[,] table = new Fraction[100, 100];

        // Вектор базисных переменных
        private ArrayList baseVector = new ArrayList();

        // Вектор оценочных ограничений
        private double[] mrVector = new double[100];

        // Вектор корней задачи
        private double[] FRootVector = new double[100];

        // Вектор корней задачи в виде обыкновенных дробей
        private Fraction[] FRootVectorFraction = new Fraction[100];

        public double FunctionValue()
        {
            return table[m, 0].ToDouble();
        }

        public Fraction FunctionValueFraction()
        {
            return table[m, 0];
        }

        public double Roots(int index)
        {
            return FRootVector[index];
        }

        public Fraction RootsFraction(int index)
        {
            return FRootVectorFraction[index];
        }

        public SimplexTable(int an, int am)
        {
            manuallyRecalcFlag = false;
            for (int i = 0; i < 100; i++)
            {
                FRootVectorFraction[i] = 0;
            }

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    table[i, j] = 0;
                }
            }
            ResizeTable(an, am);
        }

        public double MRVector(int index)
        {
            return mrVector[index];
        }

        public void SetMRVector(int index, double value)
        {
            mrVector[index] = value;
        }

        public void FillMatrixA(Fraction[,] SourceMatrixA)
        {
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    SourceMatrixA[i, j] = table[i, j + 1].ToDouble();
                }
            }

        }

        public void FillVectorb(Fraction[] SourceVectorb)
        {
            for (int i = 0; i < m; i++)
            {
                SourceVectorb[i] = table[i, 0].ToDouble();
            }
        }

        public void FillVectorc(Fraction[] SourceVectorc)
        {
            for (int i = 1; i < n + 1; i++)
            {
                SourceVectorc[i - 1] = table[m, i].ToDouble();
            }
        }

        public void ResizeTable(int an, int am)
        {
            n = an;
            m = am;
            rows = m + 1;
            cols = 1 + n + m;
            tasktype = 0;
            ClearTable();

            baseVector.Clear();
            for (int i = 0; i < m; i++)
            {
                baseVector.Add(n + i + 1);
            }
        }

        // Добавляем новое ограничение с максимальным дробным числом
        public void AddNewRest()
        {
            double sign = 0;
            double value = 0;
            int index = MaxDoubleIndex();
            m = m + 1;
            rows = m + 1;
            cols = 1 + n + m;
            baseVector.Add(n + m);

            table[m - 1, cols - 1] = 0;
            for (int i = 0; i < cols; i++)
            {
                table[m, i] = table[m - 1, i];
            }

            if (table[index, 0].ToDouble() != 0)
            {
                sign = table[index, 0].ToDouble() / table[index, 0].ToDouble();
            }
            else
            {
                sign = 1;
            }

            value = Math.Truncate(table[index, 0].ToDouble());
            table[m - 1, 0] = table[index, 0] - (long)value;
            table[m - 1, 0] = table[m - 1, 0] * sign * -1;

            table[m - 1, cols - 1] = 1;
            for (int i = 1; i < cols - 1; i++)
            {
                if (table[index, i] >= 0)
                {
                    value = Math.Truncate(table[index, i].ToDouble());
                    table[m - 1, i] = - (table[index, i] - value);
                }
                else
                {
                    value = Math.Abs(Math.Truncate(table[index, i].ToDouble()));
                    table[m - 1, i] = -(table[index, i] + value + 1);
                }
            }
        }

        public int Сols
        {
            get { return cols; }
        }

        public int Rows
        {
            get { return rows; }
        }

        public int N
        {
            get { return n; }
            set { n = value; }
        }

        public int M
        {
            get { return m; }
            set { m = value; }
        }

        public int PivotRow
        {
            get { return pivotRow; }
            set { pivotRow = value; }
        }

        public int PivotCol
        {
            get { return pivotCol; }
            set { pivotCol = value; }
        }

        public int TaskType
        {
            get { return tasktype; }
            set { tasktype = value; }
        }

        public bool Solved
        {
            get { return solved; }
            set { solved = value; }
        }

        public bool NonIntegerSolved
        {
            get { return nonintegersolved; }
            set { nonintegersolved = value; }
        }

        public bool Unsolvable
        {
            get { return unsolvable; }
            set { unsolvable = value; }
        }

        public bool Infinity
        {
            get { return infinity; }
            set { infinity = value; }
        }

        public bool ManuallyRecalcFlag
        {
            get { return manuallyRecalcFlag; }
            set { manuallyRecalcFlag = value; }
        }

        public int BaseVector(int index)
        {
            return (int)baseVector[index];
        }

        // Функция находит максимальное дробное число среди свободных членов
        public int MaxDoubleIndex()
        {
            int result = -1;
            double value = 0;
            double MaxValue = 0; 
            for (int j = 0; j < M; j++)
            {
                value = Math.Abs(table[j, 0].ToDouble()) - Math.Abs(Math.Floor(table[j, 0].ToDouble()));
                if (MaxValue < value)
                {
                    MaxValue = value;
                    result = j;
                }
            }

            return result;
        }

        public void SwapObjFuncSigns()
        {
            for (int i = 1; i < n + m + 1; i++)
            {
                table[rows - 1, i] = table[rows - 1, i] * -1;
            }
        }

        public int CheckObjFuncSigns()
        {
            int result = 0;

            if (tasktype == 0)
            {
                for (int i = 1; i < n + m + 1; i++)
                {
                    if (table[rows - 1, i] != 0)
                    {
                        result = 1;
                    }
                }

                for (int i = 1; i < n + m + 1; i++)
                {
                    if (table[rows - 1, i] < 0)
                    {
                        result = 0;
                    }
                }
            }
            
            if (tasktype == 1)
            {
                for (int i = 1; i < n + m + 1; i++)
                {
                    if (table[rows - 1, i] != 0)
                    {
                        result = 2;
                    }
                }

                for (int i = 1; i < n + m + 1; i++)
                {
                    if (table[rows - 1, i] > 0)
                    {
                        result = 0;
                    }
                }
            }

            return result;
        }

        // Функция проверяет, имеет ли задача бесконечное множество решений
        public bool TestForInfinity()
        {
          int LPivotRow = 0;
          bool Result = false;

          // Проверяем свободные члены,
          // если среди них есть отрицательные значения,
          // то решение недопустимое, и сначала нужно перейти к допустимому решению
          double LMaxMinValue = table[LPivotRow, 0].ToDouble();
          for (int i = 0; i < m; i++)
          {
            // Находим среди свободных членов максимальное отрицательное число по модулю
            // Это число будет задавать разрешающую (ведущую) строку
              if ((Math.Abs(LMaxMinValue) < Math.Abs(table[i, 0].ToDouble())) & (table[i, 0] < 0))
            {
                 LMaxMinValue = table[i, 0].ToDouble();
              LPivotRow = i;
            }
          }

          // В этой строке так же находим максимальный по модулю отрицательный элемент,
          // который будет разрешающим (ведущим) столбцом.
          if (LMaxMinValue >= 0) 
          {
            for (int i = 1; i <= n + m; i++)   
            {
              if ((!BaseVariable(i)) & (table[m, i] < 0))
              {
                Result = true;
                for (int j = 0; j < m; j++) 
                {
                  if (table[j, i] > 0) 
                  {
                      Result = false;
                  }
                }

                if (Result) 
                {
                    pivotRow = - 1;
                    pivotCol = i;
                    return Result;
                }
              }
            }
          }

          return Result;
        }

        public void ResetbaseVector()
        {
            baseVector.Clear();
            for (int i = 0; i < m; i++)
            {
                baseVector.Add(n + i + 1);
            }
        }

        public void SetBaseVector(int index, int value)
        {
            baseVector[index] = value;
        }

        public void Assign(SimplexTable smt)
        {
            pivotCol = smt.PivotCol;
            PivotRow = smt.pivotRow;
            tasktype = smt.TaskType;
            for (int i = 0; i < m; i++)
            {
                baseVector[i] = smt.BaseVector(i);
            }

            /*
            for (int i = 0; i < 100; i++)
            for (int j = 0; j < 100; j++)
            {
                table[i, j] = smt.table[i, j];
            }
            */
        }

        public double Table(int RowIndex, int ColIndex)
        {
            return table[RowIndex, ColIndex].ToDouble();
        }

        public Fraction FractionTable(int RowIndex, int ColIndex)
        {
            return table[RowIndex, ColIndex];
        }

        public bool ColValidToCalc(double value)
        {
            return value < 0;
        }

        // Функция проверяет является ли заданная переменная базисной
        public bool BaseVariable(int variable)
        {
            bool result = false;
            for (int i = 0; i < m; i++)
            {
                if ((int)baseVector[i] == variable)
                {
                    result = true;
                }
            }
            return result;
        }

        //  Процедура расчета оценочных ограничений
        public void CalcMeritRestrictions(int lpivotCol)
        {
            if (lpivotCol != - 1)
            {
                for (int i = 0; i < m; i++)
                {
                    if ((table[i, 0] * table[i, lpivotCol] < 0) | ((table[i, 0] == 0) & (table[i, lpivotCol] < 0)) | (table[i, lpivotCol] == 0)) 
                    {
                        mrVector[i] = - 1;
                    } else
                    {
                        mrVector[i] = Math.Abs(table[i, 0].ToDouble() / table[i, lpivotCol].ToDouble());
                    }
                }
            }
        }

        //  Функция находит разрешающий столбец и строку в заданной симплекс-таблице,
        //  которые обеспечивают максимальное возрастание или уменьшение целевой функции.
        public bool FindOptimalStep()
        {
            double ltemp = 0;
            int lpivotRow = - 1;
            int lpivotCol = - 1;
            int LCurrentCol = 0;
            bool result = false;
            double LMaxMinValue = 0;
            double ObjFuncValue = 0;
            double LPivotElement = 0;
            double minMeritRestrictions;
            
            // Проверяем свободные члены,
            // если среди них есть отрицательные значения,
            // то решение недопустимое, и сначала нужно перейти к допустимому решению

            // Ищем среди свободных членов отрицательное число
            for (int i = 0; i < m; i++) 
            {
              if (table[i, 0] < 0)
              {
                  LMaxMinValue = table[i, 0].ToDouble();
                lpivotRow = i;
              }
            };

            // Если такое число найдено, то ищем среди свободных членов,
            // максимальное отрицательное число по модулю
            if (lpivotRow != - 1) 
            {
              for (int i = 0; i < m; i++) 
              {
                // Находим среди свcободных членов максимальное отрицательное число по модулю
                // Это число будет задавать разрешающую (ведущую) строку
                  if ((Math.Abs(LMaxMinValue) < Math.Abs(table[i, 0].ToDouble())) & (table[i, 0] < 0))
                {
                    LMaxMinValue = table[i, 0].ToDouble();
                  lpivotRow = i;
                }
              }

              // В этой строке так же находим максимальный по модулю отрицательный элемент,
              // который будет разрешающим (ведущим) столбцом.
              if (LMaxMinValue < 0)
              {
                LMaxMinValue = 0;
                for (int i = 1; i < cols; i++) 
                {
                    if ((Math.Abs(LMaxMinValue) < Math.Abs(table[lpivotRow, i].ToDouble())) & (table[lpivotRow, i].ToDouble() < 0))
                  {
                      LMaxMinValue = table[lpivotRow, i].ToDouble();
                    lpivotCol = i;
                  }
                }
              }
            }

            if (lpivotCol > 0) 
            {
              CalcMeritRestrictions(lpivotCol);
              pivotRow = lpivotRow;
              pivotCol = lpivotCol;
              result = true;
              return result;
            } else
            {
              lpivotRow = - 1;
              lpivotCol = - 1;
            }

            switch (tasktype)
            {
              case 0:
                ObjFuncValue = -1.7e308;
                break;
              case 1:
                ObjFuncValue = 1.7e308;
                break;
            }

            for (int i = 1; i < cols; i++) 
            {
              LCurrentCol = i;
              if ((ColValidToCalc(table[m, LCurrentCol].ToDouble())) & (!BaseVariable(LCurrentCol)))
              {
                lpivotCol = LCurrentCol;
                CalcMeritRestrictions(lpivotCol);

                // Выбирается наименьшее отрицательное число при отыскании max,
                // либо наибольшее положительное при задачи на min.
            
                //  Ищем минимальное оценочное ограничение.

                minMeritRestrictions = - 1;
                for (int j = 0; j < m; j++) 
                {
                    if (mrVector[j] >= 0)
                  {
                    if (minMeritRestrictions < 0) 
                    {
                      lpivotRow = j;
                      minMeritRestrictions = mrVector[lpivotRow];
                    } else
                    {
                        if (minMeritRestrictions > mrVector[j])
                      {
                        lpivotRow = j;
                        minMeritRestrictions = mrVector[lpivotRow];
                      };
                    };
                  };
                };

                if ((minMeritRestrictions >= 0) & (lpivotRow != - 1)  & (lpivotCol != - 1))
                {
                    LPivotElement = table[lpivotRow, lpivotCol].ToDouble();
                    ltemp = table[m, 0].ToDouble() + ((table[lpivotRow, 0].ToDouble() / LPivotElement) * (-1 * table[m, lpivotCol].ToDouble()));

                  if (tasktype == 1) 
                  {
                    ltemp = - ltemp;
                  }

                  if (((ObjFuncValue < ltemp) & (tasktype == 0)) | ((ObjFuncValue > ltemp) & (tasktype == 1)))
                  {
                    result = true;
                    pivotRow = lpivotRow;
                    pivotCol = lpivotCol;
                    ObjFuncValue = ltemp;
                  };
                } else
                {
                  pivotRow = - 1;
                  pivotCol = - 1;
                  result = false;
                  return result;
                };
              };
            };

            if (pivotCol != - 1)
            {
              CalcMeritRestrictions(pivotCol);
            }

            return result;
        }

        // Сохранение текущей таблицы в файл
        public void SaveToFile(string FileName)
        {
            FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write);

            byte[] filesign = { Convert.ToByte('S'), Convert.ToByte('M'), Convert.ToByte('T') };
            byte[] tasktype = { 0, 0 };
            byte[] version = { 0, 0 };

            fs.Write(filesign, 0, 3);
            fs.Write(tasktype, 0, 2);
            fs.Write(version, 0, 2);

            fs.Write(BitConverter.GetBytes(n), 0, 4);
            fs.Write(BitConverter.GetBytes(m), 0, 4);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    fs.Write(BitConverter.GetBytes(table[i, j].ToDouble()), 0, 8);
                }
            }

            fs.Close();
        }

        // Загрузка таблицы из файла
        public bool LoadFromFile(string FileName)
        {
            FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);

            byte[] doublebuf = new byte[8];
            byte[] filesign = new byte[3];
            byte[] tasktype = new byte[2];
            byte[] version = new byte[2];
            byte[] intbuf = new byte[4];

            fs.Read(filesign, 0, 3);

            if (!((filesign[0] == Convert.ToByte('S')) &(filesign[1] == Convert.ToByte('M')) & (filesign[2] == Convert.ToByte('T'))))
            {
                MessageBox.Show("Неподдерживаемый формат файла!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                fs.Close();
                return false;
            }

            fs.Read(tasktype, 0, 2);
            fs.Read(version, 0, 2);

            if (!((BitConverter.ToUInt16(tasktype, 0) == 0) & (BitConverter.ToUInt16(version, 0) == 0)))
            {
                MessageBox.Show("Неправильный формат файла!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                fs.Close();
                return false;
            }
           
            fs.Read(intbuf, 0, 4);
            n = BitConverter.ToInt32(intbuf, 0);
            fs.Read(intbuf, 0, 4);
            m = BitConverter.ToInt32(intbuf, 0);

            ResizeTable(n, m);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    fs.Read(doublebuf, 0, 8);
                    table[i, j] = BitConverter.ToDouble(doublebuf, 0);
                }
            }

            fs.Close();

            return true;
        }

        private void ClearTable()
        {
            // Заполнение матрицы нулями
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    table[i, j] = 0;
                }
            // Вписывание единичной матрицы в ячейки базисных переменных
            for (int i = 0; i < rows; i++)
                for (int j = n + 1; j < cols; j++)
                {
                    if (i + n + 1 == j)
                    {
                        table[i, j] = 1;
                    }
                }
        }

        public void PrintTable(DataGridView SimplexGrid) 
        {
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    //SimplexGrid[j, i].Value = "12";
                    SimplexGrid[j, i].Value = table[i, j].ToString();
                }
        }

        public bool FillTable(DataGridView SimplexGrid)
        {
            bool result = true;
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    try
                    {
                        
                        //table[i, j] = (double)Convert.ToDouble(SimplexGrid[j, i].Value);
                        table[i, j] = SimplexGrid[j, i].Value.ToString();
                    }
                    catch
                    {
                        result = false;
                        MessageBox.Show("Неправильный формат числа: \"" + SimplexGrid[j, i].Value.ToString() + "\"", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            return result;
        }

        // Функция проверяет, имеет ли задача допустимые решения
        public bool TestForUnsolvable()
        {
          bool Result = false;
          int LPivotRow = - 1;
          double LMaxMinValue = 0;

          // Проверяем свободные члены,
          // если среди них есть отрицательные значения,
          // то решение недопустимое, и сначала нужно перейти к допустимому решению

          // Ищем среди свободных членов отрицательное число
          for (int i = 0; i < m; i++)
          {
            if (table[i, 0] < 0) 
            {
                LMaxMinValue = table[i, 0].ToDouble();
              LPivotRow = i;
            }
          }

          // Если такое число найдено, то ищем среди свободных членов,
          // максимальное отрицательное число по модулю
          if (LPivotRow != - 1)
          {
            for (int i = 0; i < m; i++)
            {
              // Находим среди свободных членов максимальное отрицательное число по модулю
              // Это число будет задавать разрешающую (ведущую) строку
                if ((Math.Abs(LMaxMinValue) < Math.Abs(table[i, 0].ToDouble())) & (table[i, 0] < 0))
              {
                  LMaxMinValue = table[i, 0].ToDouble();
                LPivotRow = i;
              }
            }

            // В этой строке так же находим максимальный по модулю отрицательный элемент,
            // который будет разрешающим (ведущим) столбцом.
            if (LMaxMinValue < 0)
            {
              Result = true;
              LMaxMinValue = 0;
              pivotRow = pivotRow + 1;
              for (int i = 1; i < cols; i++)
              {
                  if ((Math.Abs(LMaxMinValue) < Math.Abs(table[LPivotRow, i].ToDouble())) & (table[LPivotRow, i] < 0))
                {
                  pivotRow = - 1;
                  Result = false;
                  break;
                }
              }
            }
          }

          return Result;
        }

        public bool TestForInteger()
        {
            bool result = true;

            for (int i = 0; i < m; i++)
            {
                if ((int)baseVector[i] <= n)
                {
                    if (table[i, 0].Denominator != 1)
                    {
                        result = false;
                    }
                }
            }

            return result;
        }

        public bool TestForSolved()
        {
          bool result = true;

          for (int i = 1; i < cols; i++)
          {
            if (table[m, i] < 0)
            {
              result = false;
            }
          }

          for (int i = 0; i < m; i++)
          {
            if (table[i, 0] < 0) 
            {
                result = false;
            }
          }

          return result;
        }

        public void RecalcTable()
        {
            // Каждый элемент разрешающей строки (строки в которой находится разрешающий элемент),
            // делим на разрешающий элемент
            Fraction relement = new Fraction(table[pivotRow - 1, pivotCol]);
            //double relement = table[pivotRow - 1, pivotCol].ToDouble();
            for (int i = 0; i < cols; i++)
            {
                table[pivotRow - 1, i] = table[pivotRow - 1, i] / relement;
            }

            // Все остальные элементы расчитываются по формуле:
            // Для этого мы должны сделать равными 0
            // все остальные элементы разрешающего столбца таблицы 1,
            // кроме стоящего на пересечении с разрешающей строкой.
            // Домножаем на необходимое число разрешающую строку таблицы 1 и
            // прибавлеям к другим строкам. Получаем симплекс-таблицу 2.
            for (int i = 0; i < rows; i++)
            {
                if (i != pivotRow - 1) 
                {
                    //double temp = table[i, pivotCol].ToDouble();
                    Fraction temp = new Fraction(table[i, pivotCol]);
                    for (int j = 0; j < cols; j++)
                    {
                        table[i, j] = table[i, j] - table[pivotRow - 1, j] * temp;
                    }
                }
            }
          
            baseVector[pivotRow - 1] = pivotCol;

            // Находим вектор корней
            for (int i = 0; i < n; i++) 
            {
                FRootVector[i] = 0;
                FRootVectorFraction[i] = 0;
            }

            for (int i = 0; i < m; i++)
            {
                if ((int)baseVector[i] <= n) 
                {
                    FRootVector[(int)baseVector[i] - 1] = table[i, 0].ToDouble();
                    FRootVectorFraction[(int)baseVector[i] - 1] = table[i, 0];
                }
            }
        }

        public void GetTable(Fraction[,] matrixA, Fraction[] vectorb, Fraction[] vectorc)
        {
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    table[i, j + 1] = matrixA[i, j];
                }
            }

            for (int i = 1; i < n + 1; i++)
            {
                table[m, i] = vectorc[i - 1];
            }

            for (int i = 0; i < m; i++)
            {
                table[i, 0] = vectorb[i];
            }
        }
    }
}
