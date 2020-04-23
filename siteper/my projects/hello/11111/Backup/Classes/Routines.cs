using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace simplex.Classes
{
    class Routines
    {
        public static void InitGridView(DataGridView dataGridView)
        {
            for (int i = 0; i < dataGridView.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView.RowCount; j++)
                {
                    dataGridView[i, j].Value = 0;
                }
            }
        }
    }
}
