using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using simplex.Classes;
using simplex.forms.NewTaskForm;
using Mehroz;

namespace simplex
{
    public partial class MainForm : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool LockWindowUpdate(IntPtr hWndLock);

        // Кол-во уравнений исходной задачи
        private int SourceM = 2;
        // Кол-во ограничений исходной задачи
        private int SourceN = 2;
        // Тип исходной задачи (max/min)
        private int SourceTaskType = 0;
        // Флаг новой задачи, если false - то создана новая таблица
        private bool IsNewTask = false;
        // Знаки ограничений исходной задачи
        private int[] SourceSign = new int[100];
        // Вектор свободных членов исходной задачи
        private Fraction[] SourceVectorb = new Fraction[100];
        // вектор коэффициентов целевой функции исходной задачи
        private Fraction[] SourceVectorc = new Fraction[100];
        // Коэффициенты ограничений исходной задачи
        private Fraction[,] SourceMatrixA = new Fraction[100, 100];

        // Кол-во уравнений задачи, приведенной к канонической форме
        private int CanonicalM = 2;
        // Кол-во ограничений задачи, приведенной к канонической форме
        private int CanonicalN = 2;
        // Тип исходной задачи, приведенной к канонической форме
        private int CanonicalTaskType = 0;
        // Знаки ограничений задачи, приведенной к канонической форме
        private int[] CanonicalSign = new int[100];
        // Вектор свободных членов задачи, приведенной к канонической форме
        private Fraction[] CanonicalVectorb = new Fraction[100];
        // вектор коэффициентов целевой функции задачи, приведенной к канонической форме
        private Fraction[] CanonicalVectorc = new Fraction[100];
        // Коэффициенты ограничений задачи, приведенной к канонической форме
        private Fraction[,] CanonicalMatrixA = new Fraction[100, 100];

        int currentSimplexTable = -1;

        List<SimplexTable> SimplexTables = new List<SimplexTable>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void PrintRest(TextBox txtBox, Fraction[,] Matrix, int[] Signs, Fraction[] Vectorb, int AIndex, int AN)
        {
            string LLineStr = "";
            for (int j = 0; j < AN; j++)
            {
                if (j > 0)
                {
                    if (Matrix[AIndex, j] >= 0)
                    {
                        LLineStr = LLineStr + String.Format(" + {0}*x{1}", Matrix[AIndex, j], j + 1);
                    }
                    else
                    {
                        LLineStr = LLineStr + String.Format(" - {0}*x{1}", Math.Abs(Matrix[AIndex, j].ToDouble()), j + 1);
                    }
                }
                else
                {
                    if (Matrix[AIndex, j] >= 0) 
                    {
                        LLineStr = LLineStr + String.Format("   {0}*x{1}", Matrix[AIndex, j], j + 1);
                    } else
                    {
                        LLineStr = LLineStr + String.Format(" - {0}*x{1}", Math.Abs(Matrix[AIndex, j].ToDouble()), j + 1);
                    }
                }
            }

            switch (Signs[AIndex])
            {
                case 0:
                    LLineStr = LLineStr + " = ";
                    break;
                case 1:
                    LLineStr = LLineStr + " >= ";
                    break;
                case 2:
                    LLineStr = LLineStr + " <= ";
                    break;
            }

            if (Vectorb[AIndex] >= 0)
            {
                LLineStr = LLineStr + String.Format("{0}", Vectorb[AIndex]);
            }
            else
            {
                LLineStr = LLineStr + String.Format("-{0}", Math.Abs(Vectorb[AIndex].ToDouble()));
            }

            txtBox.AppendText(LLineStr);
            txtBox.AppendText(Environment.NewLine);
        }

        private void PrintF(TextBox txtBox, Fraction[] Vectorc, int AN, int TaskType)
        {
            string LLineStr = "   F(x) =";
            for (int j = 0; j < AN; j++)
            {
                if (j > 0)
                {
                    if (Vectorc[j] >= 0)
                    {
                        LLineStr = LLineStr + String.Format(" + {0}*x{1}", Vectorc[j], j + 1);
                    }
                    else
                    {
                        LLineStr = LLineStr + String.Format(" - {0}*x{1}", Math.Abs(Vectorc[j].ToDouble()), j + 1);
                    }
                }
                else
                {
                    if (Vectorc[j] >= 0)
                    {
                        LLineStr = LLineStr + String.Format("   {0}*x{1}", Vectorc[j], j + 1);
                    }
                    else
                    {
                        LLineStr = LLineStr + String.Format(" - {0}*x{1}", Math.Abs(Vectorc[j].ToDouble()), j + 1);
                    }
                }

            }

            switch (TaskType)
            {
                case 0:
                    LLineStr = LLineStr + " -> max";
                    break;
                case 1:
                    LLineStr = LLineStr + " -> min";
                    break;
            }

            txtBox.AppendText(LLineStr);
            txtBox.AppendText(Environment.NewLine);
        }

        private void PrintSolveLog(TextBox txtBox)
        {
            try
            {
                LockWindowUpdate(TaskInfotextBox.Handle);

                TaskInfotextBox.Clear();

                if (IsNewTask)
                {
                    CanonicalN = SourceN;
                    CanonicalM = SourceM;
                    CanonicalTaskType = SourceTaskType;
                    for (int i = 0; i < SourceM; i++)
                    {
                        CanonicalSign[i] = SourceSign[i];
                        CanonicalVectorb[i] = SourceVectorb[i];
                        for (int j = 0; j < SourceN; j++)
                        {
                            CanonicalVectorc[j] = SourceVectorc[j];
                            CanonicalMatrixA[i, j] = SourceMatrixA[i, j];
                        }
                    }

                    txtBox.AppendText("Исходная задача:");
                    txtBox.AppendText(Environment.NewLine);
                    txtBox.AppendText(Environment.NewLine);

                    for (int i = 0; i < SourceM; i++)
                    {
                        PrintRest(txtBox, SourceMatrixA, SourceSign, SourceVectorb, i, SourceN);
                    }

                    txtBox.AppendText(Environment.NewLine);
                    PrintF(txtBox, SourceVectorc, SourceN, SourceTaskType);
                    txtBox.AppendText(Environment.NewLine);
                    txtBox.AppendText("Приводим к каноническому виду: ");

                    int LStepCount = 1;
                    bool LCanonicalForm = true;
                    for (int i = 0; i < SourceM; i++)
                    {
                        switch (SourceSign[i])
                        {
                            case 0: // '='
                                LCanonicalForm = false;

                                txtBox.AppendText(Environment.NewLine);
                                txtBox.AppendText(String.Format("{0}. Избавляемся от знака равенства в {1} - ограничении. Что бы поменять знак '=' на неравенство '<=', вводим в задачу еще одно такое же ограничение, но с противоположенными знаками: ", LStepCount, i + 1));
                                txtBox.AppendText(Environment.NewLine);

                                CanonicalM++;
                                for (int j = 0; j < CanonicalN; j++)
                                {
                                    CanonicalMatrixA[CanonicalM - 1, j] = -CanonicalMatrixA[i, j];
                                }

                                CanonicalVectorb[CanonicalM - 1] = -CanonicalVectorb[i];
                                CanonicalSign[i] = 2;
                                CanonicalSign[CanonicalM - 1] = 2;

                                txtBox.AppendText(Environment.NewLine);
                                PrintRest(txtBox, CanonicalMatrixA, CanonicalSign, CanonicalVectorb, CanonicalM - 1, CanonicalN);

                                LStepCount++;
                                break;
                            case 1: // '>='
                                LCanonicalForm = false;

                                txtBox.AppendText(Environment.NewLine);
                                txtBox.AppendText(String.Format("{0}. Избавляемся от знака больше-равно в {1} - ограничении. Что бы поменять неравенство '>=' на неравенство '<=', меняем знаки в этом ограничении на противоположенные: ", LStepCount, i + 1));
                                txtBox.AppendText(Environment.NewLine);

                                CanonicalVectorb[i] = -CanonicalVectorb[i];
                                for (int j = 0; j < CanonicalN; j++)
                                {
                                    CanonicalMatrixA[i, j] = -CanonicalMatrixA[i, j];
                                }

                                CanonicalSign[i] = 2;

                                txtBox.AppendText(Environment.NewLine);
                                PrintRest(txtBox, CanonicalMatrixA, CanonicalSign, CanonicalVectorb, i, CanonicalN);

                                LStepCount++;
                                break;
                        }
                    }

                    // Меняем знаки при целевой функции
                    if (SourceTaskType == 0)
                    {
                        for (int i = 0; i < CanonicalN; i++)
                        {
                            CanonicalVectorc[i] = -CanonicalVectorc[i];
                        }

                        txtBox.AppendText(Environment.NewLine);
                        txtBox.AppendText(Environment.NewLine);
                        txtBox.AppendText(String.Format("{0}. Т.к. задача на максимизацию, то меняем знаки при целевой функции:", LStepCount));
                        txtBox.AppendText(Environment.NewLine);
                        txtBox.AppendText(Environment.NewLine);
                        PrintF(txtBox, CanonicalVectorc, CanonicalN, CanonicalTaskType);
                        LStepCount++;
                    }

                    if (LCanonicalForm)
                    {
                        txtBox.AppendText(Environment.NewLine);
                        txtBox.AppendText("Задача уже записана в каноническом виде, и не требует дополнительных преобразований.");
                        txtBox.AppendText(Environment.NewLine);
                    }
                    else
                    {
                        txtBox.AppendText(Environment.NewLine);
                        txtBox.AppendText("Записываем канонический вид задачи, после преобразований: ");
                        txtBox.AppendText(Environment.NewLine);
                        txtBox.AppendText(Environment.NewLine);

                        for (int i = 0; i < CanonicalM; i++)
                        {
                            PrintRest(txtBox, CanonicalMatrixA, CanonicalSign, CanonicalVectorb, i, CanonicalN);
                        }

                        txtBox.AppendText(Environment.NewLine);
                        PrintF(txtBox, CanonicalVectorc, CanonicalN, CanonicalTaskType);
                        txtBox.AppendText(Environment.NewLine);
                    }
                }
                else
                {
                    txtBox.AppendText("Исходная задача:");
                    txtBox.AppendText(Environment.NewLine);
                    txtBox.AppendText(Environment.NewLine);

                    for (int i = 0; i < CanonicalM; i++)
                    {
                        PrintRest(txtBox, CanonicalMatrixA, CanonicalSign, CanonicalVectorb, i, CanonicalN);
                    }

                    txtBox.AppendText(Environment.NewLine);
                    PrintF(txtBox, CanonicalVectorc, CanonicalN, CanonicalTaskType);
                    txtBox.AppendText(Environment.NewLine);

                    txtBox.AppendText(Environment.NewLine);
                    txtBox.AppendText("Задача уже записана в каноническом виде, и не требует дополнительных преобразований.");
                    txtBox.AppendText(Environment.NewLine);
                }

                for (int i = 0; i < SimplexTables.Count; i++)
                {
                    SimplexTable smptbl = SimplexTables[i];
                    txtBox.AppendText(Environment.NewLine);
                    txtBox.AppendText("----------------------------");

                    if (smptbl.Solved)
                    {
                        txtBox.AppendText(Environment.NewLine);
                        txtBox.AppendText("Среди значений коэффициентов целевой функции нет отрицательных. Поэтому задача решена!");
                        txtBox.AppendText(Environment.NewLine);
                        txtBox.AppendText(Environment.NewLine);

                        for (int j = 0; j < smptbl.N; j++)
                        {
                            txtBox.AppendText(String.Format("x{0} = {1}", j + 1, smptbl.Roots(j)));
                            txtBox.AppendText(Environment.NewLine);
                        }

                        txtBox.AppendText(Environment.NewLine);
                        if (smptbl.TaskType == 1)
                        {
                            txtBox.AppendText("Так как исходной задачей был поиск минимума, оптимальное решение есть свободный член строки F, взятый с противоположным знаком.");
                            txtBox.AppendText(Environment.NewLine);
                            txtBox.AppendText(String.Format("Значение целевой функции: Fmin = {0}", -smptbl.FunctionValue()));
                        }
                        else
                        {
                            txtBox.AppendText(String.Format("Значение целевой функции: Fmin = {0}", smptbl.FunctionValue()));
                        }

                        return;
                    }

                    if (smptbl.Infinity)
                    {
                        txtBox.AppendText(Environment.NewLine);
                        txtBox.AppendText("Оптимального решения не существует! Т.к. данная задача не ограничена.");
                        return;
                    }

                    if (smptbl.Unsolvable)
                    {
                        txtBox.AppendText(Environment.NewLine);
                        txtBox.AppendText("Так как в строке с отрицательным свободным членом нет отрицательных элементов, то система ограничений не совместна и задача не имеет решения.");
                        return;
                    }

                    if (smptbl.NonIntegerSolved)
                    {
                        txtBox.AppendText(Environment.NewLine);
                        txtBox.AppendText("Среди значений коэффициентов целевой функции нет отрицательных. Поэтому найдено оптимальное решение.");
                        txtBox.AppendText(Environment.NewLine);
                        txtBox.AppendText(Environment.NewLine);

                        for (int j = 0; j < smptbl.N; j++)
                        {
                            txtBox.AppendText(String.Format("x{0} = {1}", j + 1, smptbl.RootsFraction(j).ToString()));
                            txtBox.AppendText(Environment.NewLine);
                        }

                        txtBox.AppendText(Environment.NewLine);
                        if (smptbl.TaskType == 1)
                        {
                            txtBox.AppendText("Так как исходной задачей был поиск минимума, оптимальное решение есть свободный член строки F, взятый с противоположным знаком.");
                            txtBox.AppendText(Environment.NewLine);
                            txtBox.AppendText(String.Format("Значение целевой функции: Fmin = {0}", (-smptbl.FunctionValueFraction()).ToString()));
                        }
                        else
                        {
                            txtBox.AppendText(String.Format("Значение целевой функции: Fmin = {0}", smptbl.FunctionValueFraction().ToString()));
                        }

                        txtBox.AppendText(Environment.NewLine);
                        txtBox.AppendText(Environment.NewLine);
                        txtBox.AppendText("Получившееся оптимальное решение нецелочисленное. Среди свободных членов находим переменную с максимальным дробным числом:");
                        txtBox.AppendText(Environment.NewLine);

                        double value = 0;
                        int MaxIndex = 0;
                        double MaxValue = 0;
                        txtBox.AppendText(Environment.NewLine);
                        for (int j = 0; j < smptbl.M; j++)
                        {
                            if (smptbl.BaseVector(j) <= smptbl.N)
                            {
                                value = Math.Abs(smptbl.Table(j, 0)) - Math.Abs(Math.Floor(smptbl.Table(j, 0)));
                                if (MaxValue < value)
                                {
                                    MaxValue = value;
                                    MaxIndex = j;
                                }
                                txtBox.AppendText(String.Format("x{0} = {1} = {2}", smptbl.BaseVector(j), smptbl.FractionTable(j, 0).ToString(), smptbl.Table(j, 0)));
                                txtBox.AppendText(Environment.NewLine);
                            }

                        }

                        txtBox.AppendText(Environment.NewLine);
                        txtBox.AppendText("x" + smptbl.BaseVector(MaxIndex).ToString() + " - свободный член с максимальным дробным числом. Поэтому вводим дополнительное ограничение по " + (MaxIndex + 1) + " строке:");
                        txtBox.AppendText(Environment.NewLine);

                        string LLineStr = "";
                        txtBox.AppendText(Environment.NewLine);

                        double sign = smptbl.Table(MaxIndex, 0) / smptbl.Table(MaxIndex, 0);
                        Fraction FractionValue = new Fraction(smptbl.FractionTable(MaxIndex, 0));

                        if (FractionValue < 0)
                        {
                            FractionValue = FractionValue * -1;
                        }

                        FractionValue = FractionValue - Math.Truncate(Math.Abs(smptbl.FractionTable(MaxIndex, 0).ToDouble()));

                        bool isfirst = true;
                        FractionValue = FractionValue * sign * -1;
                        LLineStr = LLineStr + FractionValue.ToString() + " = ";
                        for (int j = 1; j < smptbl.Сols; j++)
                        {
                            if (smptbl.FractionTable(MaxIndex, j) >= 0)
                            {
                                value = Math.Truncate(smptbl.FractionTable(MaxIndex, j).ToDouble());
                                FractionValue = -(smptbl.FractionTable(MaxIndex, j) - value);
                            }
                            else
                            {
                                value = Math.Abs(Math.Truncate(smptbl.FractionTable(MaxIndex, j).ToDouble()));
                                FractionValue = -(smptbl.FractionTable(MaxIndex, j) + value + 1);
                            }

                            if (FractionValue > 0)
                            {
                                if (isfirst)
                                {
                                    LLineStr = LLineStr + FractionValue.ToString() + "x" + j;
                                }
                                else
                                {
                                    LLineStr = LLineStr + " + " + FractionValue.ToString() + "x" + j;
                                }
                                isfirst = false;
                            }
                            else
                            {
                                LLineStr = LLineStr + " - " + (FractionValue * -1).ToString() + "x" + j;
                                isfirst = false;
                            }
                        }

                        LLineStr = LLineStr + " + " + "x" + (smptbl.N + smptbl.M + 1);

                        txtBox.AppendText(LLineStr);
                        txtBox.AppendText(Environment.NewLine);
                        txtBox.AppendText(Environment.NewLine);

                        txtBox.AppendText("Пересчитываем получившуюся таблицу:");

                        continue;
                    }

                    if ((smptbl.PivotCol != -1) & (smptbl.PivotRow != -1))
                    {
                        txtBox.AppendText(Environment.NewLine);
                        txtBox.AppendText(String.Format("Итерация {0}:", i + 1));
                        txtBox.AppendText(Environment.NewLine);
                        txtBox.AppendText(Environment.NewLine);
                        txtBox.AppendText(String.Format("Ведущий столбец: {0}", SimplexTables[i].PivotCol));
                        txtBox.AppendText(Environment.NewLine);
                        txtBox.AppendText(String.Format("Ведущая строка: {0}", SimplexTables[i].PivotRow + 1));
                    }

                }
            }
            finally
            {
                LockWindowUpdate(IntPtr.Zero);
            }
            
        }

        // Обновление UpdateGrid в соответствии с текущей симплекс таблицей
        private void UpdateSimplexGrid()
        {
            if ((currentSimplexTable >= 0) & (currentSimplexTable < SimplexTables.Count)) 
            {
                SimplexTable smptbl = SimplexTables[currentSimplexTable];

                Mainpanel.Visible = true;

                SimplexGrid.RowCount = smptbl.M + 1;
                SimplexGrid.ColumnCount = smptbl.N + smptbl.M + 2;
                
                SimplexGrid.Columns[0].HeaderText = "b";
                
                for (int i = 1; i < SimplexGrid.ColumnCount - 1; i++)
                {
                    SimplexGrid.Columns[i].HeaderText = "x" + i.ToString();
                }
                
                SimplexGrid.Columns[SimplexGrid.ColumnCount - 1].HeaderText = "O.O";

                for (int i = 0; i < SimplexGrid.RowCount - 1; i++)
                {
                    SimplexGrid.Rows[i].HeaderCell.Value = "x" + smptbl.BaseVector(i);
                }

                if (TaskTypeComboBox.SelectedIndex == 0)
                {
                    SimplexGrid.Rows[SimplexGrid.RowCount - 1].HeaderCell.Value = "Fmax";
                }
                else
                {
                    SimplexGrid.Rows[SimplexGrid.RowCount - 1].HeaderCell.Value = "Fmin";
                }

                for (int i = 0; i < SimplexGrid.RowCount; i++)
                {
                    for (int j = 0; j < SimplexGrid.ColumnCount; j++)
                    {
                        SimplexGrid[j, i].Style.BackColor = Color.White;
                    }
                }

                if (smptbl.PivotRow != -1)
                {
                    for (int i = 0; i < SimplexGrid.ColumnCount; i++)
                    {
                        SimplexGrid[i, smptbl.PivotRow].Style.BackColor = Color.LightYellow;
                    }
                }

                if (smptbl.PivotCol != -1)
                {
                    for (int i = 0; i < SimplexGrid.RowCount; i++)
                    {
                        SimplexGrid[smptbl.PivotCol, i].Style.BackColor = Color.LightYellow;
                    }

                    smptbl.CalcMeritRestrictions(smptbl.PivotCol);
                }

                if ((smptbl.PivotRow != -1) & (smptbl.PivotCol != -1))
                {
                    for (int i = 0; i < smptbl.M; i++)
                    {
                        if (smptbl.MRVector(i) < 0)
                        {
                            SimplexGrid[SimplexGrid.ColumnCount - 1, i].Value = "Не огр.";
                        }
                        else
                        {
                            SimplexGrid[SimplexGrid.ColumnCount - 1, i].Value = smptbl.MRVector(i);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < smptbl.M; i++)
                    {
                        SimplexGrid[SimplexGrid.ColumnCount - 1, i].Value = "";
                    }
                }

                smptbl.PrintTable(SimplexGrid);
            }

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CloseTableMode();
            TaskTypeComboBox.SelectedIndex = 0;
        }

        private void CloseTableMode()
        {
            TaskTypeComboBox.Enabled = false;
            RecalTablebutton.Enabled = false;
        }

        private void OpenTableMode()
        {
            TaskTypeComboBox.Enabled = true;
            RecalTablebutton.Enabled = true;
        }

        public bool ValidPivotColRow(int pivotCol, int pivotRow)
        {
            bool result = true;

            if ((currentSimplexTable >= 0) & (currentSimplexTable < SimplexTables.Count))
            {
                SimplexTable currentsmptbl = SimplexTables[currentSimplexTable];

                currentsmptbl.CalcMeritRestrictions(pivotCol);

                result = currentsmptbl.MRVector(pivotRow) >= 0;
            }

            return result;
        }

        public void SetPivotColRow(int col, int row)
        {
            if (SimplexGrid.Visible)
            {
                for (int i = 0; i < SimplexGrid.RowCount; i++)
                {
                    for (int j = 0; j < SimplexGrid.ColumnCount; j++)
                    {
                        SimplexGrid[j, i].Style.BackColor = Color.White;
                    }
                }

                if ((col != -1) & (row != -1))
                {
                    for (int i = 0; i < SimplexGrid.RowCount; i++)
                    {
                        SimplexGrid[col + 1, i].Style.BackColor = Color.LightYellow;
                    }

                    for (int i = 0; i < SimplexGrid.ColumnCount; i++)
                    {
                        SimplexGrid[i, row].Style.BackColor = Color.LightYellow;
                    }

                    if ((currentSimplexTable >= 0) & (currentSimplexTable < SimplexTables.Count))
                    {
                        SimplexTable currentsmptbl = SimplexTables[currentSimplexTable];

                        int PivotCol = col + 1;
                        int PivotRow = row + 1;

                        currentsmptbl.CalcMeritRestrictions(PivotCol);

                        if ((PivotRow != -1) & (PivotCol != -1))
                        {
                            for (int i = 0; i < currentsmptbl.M; i++)
                            {
                                if (currentsmptbl.MRVector(i) < 0)
                                {
                                    SimplexGrid[SimplexGrid.ColumnCount - 1, i].Value = "Не огр.";
                                }
                                else
                                {
                                    SimplexGrid[SimplexGrid.ColumnCount - 1, i].Value = currentsmptbl.MRVector(i);
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < currentsmptbl.M; i++)
                            {
                                SimplexGrid[SimplexGrid.ColumnCount - 1, i].Value = "";
                            }
                        }
                    }


                }
                else
                {
                    for (int i = 0; i < SimplexGrid.RowCount - 1; i++)
                    {
                        SimplexGrid[SimplexGrid.ColumnCount - 1, i].Value = "";
                    }
                }
            }
        }

        private void TaskTypeComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (TaskTypeComboBox.SelectedIndex == 0)
            {
                SimplexGrid.Rows[SimplexGrid.RowCount - 1].HeaderCell.Value = "Fmax";
            }
            else
            {
                SimplexGrid.Rows[SimplexGrid.RowCount - 1].HeaderCell.Value = "Fmin";
            }

            if ((currentSimplexTable >= 0) & (currentSimplexTable < SimplexTables.Count))
            {
                SimplexTable currentsmptbl = SimplexTables[currentSimplexTable];

                currentsmptbl.TaskType = TaskTypeComboBox.SelectedIndex;
            }

        }

        private void TaskTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TaskTypeComboBox.SelectedIndex != SourceTaskType)
            {
                SourceTaskType = TaskTypeComboBox.SelectedIndex;
                SimplexTables.Clear();
                
                if (!IsNewTask)
                {
                    CanonicalTaskType = TaskTypeComboBox.SelectedIndex;
                    for (int i = 0; i < CanonicalN; i++)
                    {
                        CanonicalVectorc[i] = -CanonicalVectorc[i];
                    } 
                }
                
                PrintSolveLog(TaskInfotextBox);

                SimplexTable smptbl = new SimplexTable(CanonicalN, CanonicalM);
                smptbl.GetTable(CanonicalMatrixA, CanonicalVectorb, CanonicalVectorc);
                smptbl.TaskType = CanonicalTaskType;
                SimplexTables.Add(smptbl);
                currentSimplexTable = SimplexTables.Count - 1;
                UpdateSimplexGrid();
                OpenTableMode();
            }
        }

        private void RecalcSimplexTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((currentSimplexTable >= 0) & (currentSimplexTable < SimplexTables.Count))
            {
                SimplexTable currentsmptbl = SimplexTables[currentSimplexTable];

                if (currentsmptbl.FillTable(SimplexGrid))
                {
                    // Итеративно решаем задачу симплекс-методом
                    if ((!currentsmptbl.Solved) & (!currentsmptbl.Unsolvable) & (!currentsmptbl.Infinity))
                    {
                        // Проверяем, имеет ли задача бесконечное множество решений
                        if (currentsmptbl.TestForInfinity())
                        {
                            currentsmptbl.Infinity = true;
                            toolStripStatusLabel.Text = "Задача не имеет бесконечное множество решений!";
                        }

                        // Проверяем, является ли решение допустимым
                        if (currentsmptbl.TestForUnsolvable())
                        {
                            currentsmptbl.Unsolvable = true;
                            toolStripStatusLabel.Text = "Задача не имеет допустимых решений!";
                        }

                        // Проверяем, является ли решение оптимальным
                        if (currentsmptbl.TestForSolved())
                        {
                            // Проверяем получившееся решение на целочисленность
                            if (currentsmptbl.TestForInteger())
                            {
                                currentsmptbl.Solved = true;
                                toolStripStatusLabel.Text = "Задача решена!";
                            }
                            else
                            {
                                currentsmptbl.NonIntegerSolved = true;
                                currentsmptbl.PivotRow = currentsmptbl.MaxDoubleIndex();

                                SimplexTable smptbl = new SimplexTable(currentsmptbl.N, currentsmptbl.M);
                                smptbl.Assign(currentsmptbl);
                                smptbl.FillTable(SimplexGrid);

                                smptbl.AddNewRest();
                                smptbl.PivotRow = smptbl.PivotRow + 1; 

                                SimplexTables.Add(smptbl);
                                currentSimplexTable = SimplexTables.Count - 1;
                                currentsmptbl = SimplexTables[currentSimplexTable];

                                UpdateSimplexGrid();
                                PrintSolveLog(TaskInfotextBox);

                                return;
                            }
                        }

                        if (currentsmptbl.FindOptimalStep())
                        {
                            SimplexTable smptbl = new SimplexTable(currentsmptbl.N, currentsmptbl.M);
                            smptbl.Assign(currentsmptbl);
                            smptbl.FillTable(SimplexGrid);

                            SimplexTables.Add(smptbl);
                            currentSimplexTable = SimplexTables.Count - 1;
                            toolStripStatusLabel.Text = " Итерация: " + currentSimplexTable.ToString();
                            
                            smptbl.PivotRow = smptbl.PivotRow + 1;
                            smptbl.RecalcTable();
                            smptbl.PivotRow = -1;
                            smptbl.PivotCol = -1;

                            UpdateSimplexGrid();
                        }
                    }
                }
            }

            PrintSolveLog(TaskInfotextBox);
        }

        private void NewTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewTaskForm ntf = new NewTaskForm();
            if (ntf.ShowDialog() == DialogResult.OK)
            {
                TaskEditForm tef = new TaskEditForm();

                tef.InitForm(ntf.N, ntf.M);

                if (tef.ShowDialog() == DialogResult.OK)
                {
                    IsNewTask = true;

                    TaskTypeComboBox.SelectedIndex = tef.TaskType;
                    SimplexTables.Clear();

                    SourceN = tef.N;
                    SourceM = tef.M;
                    SourceSign = tef.Signs;
                    SourceVectorb = tef.Vectorb;
                    SourceVectorc = tef.Vectorc;
                    SourceMatrixA = tef.MatrixA;
                    SourceTaskType = tef.TaskType;

                    PrintSolveLog(TaskInfotextBox);

                    SimplexTable smptbl = new SimplexTable(CanonicalN, CanonicalM);
                    smptbl.GetTable(CanonicalMatrixA, CanonicalVectorb, CanonicalVectorc);
                    smptbl.TaskType = CanonicalTaskType;
                    SimplexTables.Add(smptbl);
                    currentSimplexTable = SimplexTables.Count - 1;
                    UpdateSimplexGrid();
                    OpenTableMode();
                }
            }
        }

        private void NewSimplexTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewTaskForm ntf = new NewTaskForm();
            ntf.Text = NewSimplexTableToolStripMenuItem.Text;
            if (ntf.ShowDialog() == DialogResult.OK)
            {
                IsNewTask = false;

                SimplexTables.Clear();
                SimplexTable smptbl = new SimplexTable(ntf.N, ntf.M);
                
                CanonicalN = smptbl.N;
                CanonicalM = smptbl.M;

                for (int i = 0; i < CanonicalM; i++)
                {
                    CanonicalSign[i] = 2;
                }

                SourceTaskType = 0;
                CanonicalTaskType = 0;
                TaskTypeComboBox.SelectedIndex = 0;

                smptbl.FillVectorb(CanonicalVectorb);
                smptbl.FillVectorc(CanonicalVectorc);
                smptbl.FillMatrixA(CanonicalMatrixA); 

                SimplexTables.Add(smptbl);
                currentSimplexTable = SimplexTables.Count - 1;
                UpdateSimplexGrid();
                OpenTableMode();

                PrintSolveLog(TaskInfotextBox);
            }
        }


        private void CloseMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SimplexGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            currentSimplexTable = SimplexTables.Count - 1;
            SimplexTable cursmptbl = SimplexTables[currentSimplexTable];
            
            SimplexTable smptbl = new SimplexTable(cursmptbl.N, cursmptbl.M);
            smptbl.Assign(cursmptbl);

            SimplexTables.Clear();
            SimplexTables.Add(smptbl);
            
            if (smptbl.FillTable(SimplexGrid))
            {
                IsNewTask = false;

                smptbl.Solved = false;
                smptbl.Infinity = false;
                smptbl.Unsolvable = false;

                smptbl.ResetbaseVector();

                CanonicalN = smptbl.N;
                CanonicalM = smptbl.M;

                for (int i = 0; i < CanonicalM; i++)
                {
                    CanonicalSign[i] = 2;
                }

                CanonicalTaskType = TaskTypeComboBox.SelectedIndex;

                smptbl.FillVectorb(CanonicalVectorb);
                smptbl.FillVectorc(CanonicalVectorc);
                smptbl.FillMatrixA(CanonicalMatrixA);

                PrintSolveLog(TaskInfotextBox);

                toolStripStatusLabel.Text = "Итерация: ";
            }
        }

    }
}