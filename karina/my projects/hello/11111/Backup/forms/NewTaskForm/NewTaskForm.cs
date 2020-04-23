using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace simplex
{
    public partial class NewTaskForm : Form
    {
        public NewTaskForm()
        {
            InitializeComponent();
        }

        public int N
        {
            get { return Convert.ToInt32(NtextBox.Text); }
        }

        public int M
        {
            get { return Convert.ToInt32(MtextBox.Text); }
        }

        private void NewTaskForm_Load(object sender, EventArgs e)
        {

        }
    }
}