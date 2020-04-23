using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using simplex.Classes;
using System.Collections;
using System.Windows.Forms;
using Mehroz;

namespace simplex.forms.NewTaskForm
{
    public partial class TaskEditForm : Form
    {
        private int m = 2;
        private int n = 2;
        private List<ComboBox> signs = new List<ComboBox>();

        public Fraction[,] MatrixA = new Fraction[100, 100];
        public Fraction[] Vectorb = new Fraction[100];
        public Fraction[] Vectorc = new Fraction[100];
        public int[] Signs = new int[100];  

        public TaskEditForm()
        {
            InitializeComponent();
        }

        public void InitForm(int an, int am)
        {
            n = an;
            m = am;

            MatrixAGridView.RowCount = m;
            MatrixAGridView.ColumnCount = n;

            MatrixAGridView.Height = MatrixAGridView.RowCount * MatrixAGridView.RowTemplate.Height + 3;
            MatrixAGridView.Width = MatrixAGridView.ColumnCount * 50;

            signs.Clear();
            for (int i = 0; i < m; i++)
            {
                ComboBox cb = new ComboBox();
                cb.Parent = this;
                cb.Width = 50;
                cb.Items.Add("=");
                cb.Items.Add(">=");
                cb.Items.Add("<=");
                cb.SelectedIndex = 0;
                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                cb.Left = MatrixAGridView.Left + MatrixAGridView.Width + 15;
                cb.Top = MatrixAGridView.Top + (i * MatrixAGridView.RowTemplate.Height);

                signs.Add(cb);
            }

            label3.Left = MatrixAGridView.Left + MatrixAGridView.Width + 80;
            MatrixBGridView.Left = MatrixAGridView.Left + MatrixAGridView.Width + 80;
            MatrixBGridView.Height = MatrixAGridView.RowCount * MatrixAGridView.RowTemplate.Height + 3;
            MatrixBGridView.RowCount = m;
            MatrixBGridView.ColumnCount = 1;

            label2.Top = MatrixAGridView.Top + MatrixAGridView.Height + 15;
            MatrixCGridView.Top = label2.Top + label2.Height + 5;
            MatrixCGridView.RowCount = 1;
            MatrixCGridView.ColumnCount = n;
            MatrixCGridView.Width = MatrixCGridView.ColumnCount * 50;
            MatrixCGridView.Height = MatrixCGridView.RowCount * MatrixCGridView.RowTemplate.Height + 3;

            BottomPanel.Top = MatrixCGridView.Top + MatrixCGridView.Height + 15;

            if (label3.Width < MatrixBGridView.Width)
            {
                BottomPanel.Width = MatrixBGridView.Left + MatrixBGridView.Width - BottomPanel.Left;
            }
            else
            {
                BottomPanel.Width = label3.Left + label3.Width - BottomPanel.Left;
            }

            Width = BottomPanel.Left + BottomPanel.Width + 15;
            Height = BottomPanel.Top + BottomPanel.Height + 70;

            Routines.InitGridView(MatrixAGridView);
            Routines.InitGridView(MatrixBGridView);
            Routines.InitGridView(MatrixCGridView);

            TaskTypecomboBox.Top = btnOk.Top;
            if (TaskTypecomboBox.Bounds.Right > btnOk.Left)
            {
                TaskTypecomboBox.Width = btnOk.Left - 30;
            }
            TaskTypecomboBox.SelectedIndex = 0;

        }

        private void NewTaskForm_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            btnOk.DialogResult = DialogResult.OK;

            for (int i = 0; i < m; i++)
            {
                Signs[i] = signs[i].SelectedIndex;
            }

            string val = "";
            for (int i = 0; i < MatrixAGridView.RowCount; i++)
            {
                for (int j = 0; j < MatrixAGridView.ColumnCount; j++)
                {
                    try
                    {
                        val = MatrixAGridView[j, i].Value.ToString();
                        val = val.Replace(".", ",");
                        MatrixA[i, j] = val;
                    }
                    catch
                    {
                        btnOk.DialogResult = DialogResult.None;
                        MessageBox.Show("Неправильный формат числа: \"" + MatrixAGridView[j, i].Value.ToString() + "\"", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            for (int i = 0; i < MatrixBGridView.RowCount; i++)
            {
                try
                {
                    val = MatrixBGridView[0, i].Value.ToString();
                    val = val.Replace(".", ",");
                    Vectorb[i] = val;
                }
                catch
                {
                    btnOk.DialogResult = DialogResult.None;
                    MessageBox.Show("Неправильный формат числа: \"" + MatrixBGridView[0, i].Value.ToString() + "\"", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            for (int i = 0; i < MatrixCGridView.ColumnCount; i++)
            {
                try
                {
                    val = MatrixCGridView[i, 0].Value.ToString();
                    val = val.Replace(".", ",");
                    Vectorc[i] = val;
                }
                catch
                {
                    btnOk.DialogResult = DialogResult.None;
                    MessageBox.Show("Неправильный формат числа: \"" + MatrixCGridView[i, 0].Value.ToString() + "\"", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (btnOk.DialogResult == DialogResult.OK)
            {
                DialogResult = DialogResult.OK;
            }

        }

        public int N
        {
            get { return n; }
        }

        public int M
        {
            get { return m; }
        }

        public int TaskType
        {
            get { return TaskTypecomboBox.SelectedIndex;  }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}