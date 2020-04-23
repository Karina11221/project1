using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace simplex
{
    static class Program
    {
        public static MainForm MainForm;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Program.MainForm = new MainForm();
            Application.Run(Program.MainForm);
        }
    }
}