namespace simplex
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewSimplexTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.CloseMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.TaskTypeComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.RecalTablebutton = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.Mainpanel = new System.Windows.Forms.Panel();
            this.splitter = new System.Windows.Forms.Splitter();
            this.TaskInfotextBox = new System.Windows.Forms.TextBox();
            this.SimplexGrid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.TopPanel.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.Mainpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SimplexGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(859, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewTaskToolStripMenuItem,
            this.NewSimplexTableToolStripMenuItem,
            this.toolStripMenuItem1,
            this.CloseMenuToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.FileToolStripMenuItem.Text = "Файл";
            // 
            // NewTaskToolStripMenuItem
            // 
            this.NewTaskToolStripMenuItem.Name = "NewTaskToolStripMenuItem";
            this.NewTaskToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.NewTaskToolStripMenuItem.Text = "Новая задача";
            this.NewTaskToolStripMenuItem.Click += new System.EventHandler(this.NewTaskToolStripMenuItem_Click);
            // 
            // NewSimplexTableToolStripMenuItem
            // 
            this.NewSimplexTableToolStripMenuItem.Name = "NewSimplexTableToolStripMenuItem";
            this.NewSimplexTableToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.NewSimplexTableToolStripMenuItem.Text = "Новая симплекс-таблица";
            this.NewSimplexTableToolStripMenuItem.Click += new System.EventHandler(this.NewSimplexTableToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(208, 6);
            // 
            // CloseMenuToolStripMenuItem
            // 
            this.CloseMenuToolStripMenuItem.Name = "CloseMenuToolStripMenuItem";
            this.CloseMenuToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.CloseMenuToolStripMenuItem.Text = "Выход";
            this.CloseMenuToolStripMenuItem.Click += new System.EventHandler(this.CloseMenuToolStripMenuItem_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Симплекс таблица (*.smt)|*.smt";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Симплекс таблица (*.smt)|*.smt";
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TaskTypeComboBox,
            this.toolStripSeparator4});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(859, 25);
            this.toolStrip.TabIndex = 3;
            this.toolStrip.Text = "toolStrip1";
            // 
            // TaskTypeComboBox
            // 
            this.TaskTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TaskTypeComboBox.Items.AddRange(new object[] {
            "Максимизировать",
            "Минимизировать"});
            this.TaskTypeComboBox.Name = "TaskTypeComboBox";
            this.TaskTypeComboBox.Size = new System.Drawing.Size(121, 25);
            this.TaskTypeComboBox.Tag = "";
            this.TaskTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.TaskTypeComboBox_SelectedIndexChanged);
            this.TaskTypeComboBox.DropDownClosed += new System.EventHandler(this.TaskTypeComboBox_DropDownClosed);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.TopPanel.Controls.Add(this.RecalTablebutton);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 49);
            this.TopPanel.Margin = new System.Windows.Forms.Padding(0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(859, 35);
            this.TopPanel.TabIndex = 5;
            // 
            // RecalTablebutton
            // 
            this.RecalTablebutton.Enabled = false;
            this.RecalTablebutton.Location = new System.Drawing.Point(3, 6);
            this.RecalTablebutton.Name = "RecalTablebutton";
            this.RecalTablebutton.Size = new System.Drawing.Size(187, 23);
            this.RecalTablebutton.TabIndex = 2;
            this.RecalTablebutton.Text = "Пересчитать задачу";
            this.RecalTablebutton.UseVisualStyleBackColor = true;
            this.RecalTablebutton.Click += new System.EventHandler(this.RecalcSimplexTableToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 468);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(859, 24);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.BackColor = System.Drawing.Color.White;
            this.toolStripStatusLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripStatusLabel.ForeColor = System.Drawing.Color.Black;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(102, 19);
            this.toolStripStatusLabel.Text = "Итерация: ";
            // 
            // Mainpanel
            // 
            this.Mainpanel.Controls.Add(this.splitter);
            this.Mainpanel.Controls.Add(this.TaskInfotextBox);
            this.Mainpanel.Controls.Add(this.SimplexGrid);
            this.Mainpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Mainpanel.Location = new System.Drawing.Point(0, 84);
            this.Mainpanel.Name = "Mainpanel";
            this.Mainpanel.Size = new System.Drawing.Size(859, 384);
            this.Mainpanel.TabIndex = 9;
            this.Mainpanel.Visible = false;
            // 
            // splitter
            // 
            this.splitter.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.splitter.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter.Location = new System.Drawing.Point(488, 0);
            this.splitter.Name = "splitter";
            this.splitter.Size = new System.Drawing.Size(3, 384);
            this.splitter.TabIndex = 12;
            this.splitter.TabStop = false;
            // 
            // TaskInfotextBox
            // 
            this.TaskInfotextBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.TaskInfotextBox.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TaskInfotextBox.Location = new System.Drawing.Point(491, 0);
            this.TaskInfotextBox.Multiline = true;
            this.TaskInfotextBox.Name = "TaskInfotextBox";
            this.TaskInfotextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TaskInfotextBox.Size = new System.Drawing.Size(368, 384);
            this.TaskInfotextBox.TabIndex = 11;
            // 
            // SimplexGrid
            // 
            this.SimplexGrid.AllowUserToAddRows = false;
            this.SimplexGrid.AllowUserToDeleteRows = false;
            this.SimplexGrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SimplexGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.SimplexGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SimplexGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.SimplexGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SimplexGrid.Location = new System.Drawing.Point(0, 0);
            this.SimplexGrid.MultiSelect = false;
            this.SimplexGrid.Name = "SimplexGrid";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SimplexGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.SimplexGrid.RowHeadersWidth = 75;
            this.SimplexGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.SimplexGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.SimplexGrid.ShowCellErrors = false;
            this.SimplexGrid.ShowCellToolTips = false;
            this.SimplexGrid.ShowEditingIcon = false;
            this.SimplexGrid.ShowRowErrors = false;
            this.SimplexGrid.Size = new System.Drawing.Size(859, 384);
            this.SimplexGrid.TabIndex = 9;
            this.SimplexGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.SimplexGrid_CellEndEdit);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(859, 492);
            this.Controls.Add(this.Mainpanel);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Simplex Gomory Lite";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.TopPanel.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.Mainpanel.ResumeLayout(false);
            this.Mainpanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SimplexGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CloseMenuToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripComboBox TaskTypeComboBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem NewSimplexTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.Panel Mainpanel;
        private System.Windows.Forms.Splitter splitter;
        private System.Windows.Forms.TextBox TaskInfotextBox;
        private System.Windows.Forms.DataGridView SimplexGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button RecalTablebutton;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
    }
}

