namespace simplex
{
    partial class NewTaskForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MtextBox = new System.Windows.Forms.TextBox();
            this.Mlabel = new System.Windows.Forms.Label();
            this.Nlabel = new System.Windows.Forms.Label();
            this.NtextBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(229, 46);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(229, 17);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.MtextBox);
            this.groupBox1.Controls.Add(this.Mlabel);
            this.groupBox1.Controls.Add(this.Nlabel);
            this.groupBox1.Controls.Add(this.NtextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(208, 125);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // MtextBox
            // 
            this.MtextBox.Location = new System.Drawing.Point(16, 89);
            this.MtextBox.Name = "MtextBox";
            this.MtextBox.Size = new System.Drawing.Size(100, 20);
            this.MtextBox.TabIndex = 9;
            this.MtextBox.Text = "2";
            // 
            // Mlabel
            // 
            this.Mlabel.AutoSize = true;
            this.Mlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Mlabel.Location = new System.Drawing.Point(13, 70);
            this.Mlabel.Name = "Mlabel";
            this.Mlabel.Size = new System.Drawing.Size(166, 16);
            this.Mlabel.TabIndex = 8;
            this.Mlabel.Text = "Кол-во ограничений (m):";
            // 
            // Nlabel
            // 
            this.Nlabel.AutoSize = true;
            this.Nlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Nlabel.Location = new System.Drawing.Point(13, 18);
            this.Nlabel.Name = "Nlabel";
            this.Nlabel.Size = new System.Drawing.Size(148, 16);
            this.Nlabel.TabIndex = 7;
            this.Nlabel.Text = "Кол-во уравнений (n):";
            // 
            // NtextBox
            // 
            this.NtextBox.Location = new System.Drawing.Point(16, 37);
            this.NtextBox.Name = "NtextBox";
            this.NtextBox.Size = new System.Drawing.Size(100, 20);
            this.NtextBox.TabIndex = 6;
            this.NtextBox.Text = "2";
            // 
            // NewTaskForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(316, 157);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewTaskForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Новая задача";
            this.Load += new System.EventHandler(this.NewTaskForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox MtextBox;
        private System.Windows.Forms.Label Mlabel;
        private System.Windows.Forms.Label Nlabel;
        private System.Windows.Forms.TextBox NtextBox;
    }
}