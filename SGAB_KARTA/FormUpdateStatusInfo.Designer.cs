namespace SGAB.SGAB_Karta
{
    partial class FormUpdateStatusInfo
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
            this.label1 = new System.Windows.Forms.Label();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtFilePathOldFile = new System.Windows.Forms.TextBox();
            this.txtFilePathNewFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSelectOldFile = new System.Windows.Forms.Button();
            this.btnSelectNewFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ursprunglig fil som innehåller information om status:";
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(320, 115);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 2;
            this.ButtonCancel.Text = "Avbryt";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(239, 115);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 3;
            this.btnUpdate.Text = "Överför";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtFilePathOldFile
            // 
            this.txtFilePathOldFile.Location = new System.Drawing.Point(12, 32);
            this.txtFilePathOldFile.Name = "txtFilePathOldFile";
            this.txtFilePathOldFile.Size = new System.Drawing.Size(346, 20);
            this.txtFilePathOldFile.TabIndex = 4;
            // 
            // txtFilePathNewFile
            // 
            this.txtFilePathNewFile.Location = new System.Drawing.Point(12, 80);
            this.txtFilePathNewFile.Name = "txtFilePathNewFile";
            this.txtFilePathNewFile.Size = new System.Drawing.Size(346, 20);
            this.txtFilePathNewFile.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Ny Bestallning_Tatuk.shp (t.ex. på USB):";
            // 
            // btnSelectOldFile
            // 
            this.btnSelectOldFile.Location = new System.Drawing.Point(364, 30);
            this.btnSelectOldFile.Name = "btnSelectOldFile";
            this.btnSelectOldFile.Size = new System.Drawing.Size(30, 23);
            this.btnSelectOldFile.TabIndex = 7;
            this.btnSelectOldFile.Text = "...";
            this.btnSelectOldFile.UseVisualStyleBackColor = true;
            this.btnSelectOldFile.Click += new System.EventHandler(this.btnSelectOldFile_Click);
            // 
            // btnSelectNewFile
            // 
            this.btnSelectNewFile.Location = new System.Drawing.Point(364, 80);
            this.btnSelectNewFile.Name = "btnSelectNewFile";
            this.btnSelectNewFile.Size = new System.Drawing.Size(30, 23);
            this.btnSelectNewFile.TabIndex = 8;
            this.btnSelectNewFile.Text = "...";
            this.btnSelectNewFile.UseVisualStyleBackColor = true;
            this.btnSelectNewFile.Click += new System.EventHandler(this.btnSelectNewFile_Click);
            // 
            // FormUpdateStatusInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 151);
            this.Controls.Add(this.btnSelectNewFile);
            this.Controls.Add(this.btnSelectOldFile);
            this.Controls.Add(this.txtFilePathNewFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFilePathOldFile);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.label1);
            this.Name = "FormUpdateStatusInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Överför statusinformation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TextBox txtFilePathOldFile;
        private System.Windows.Forms.TextBox txtFilePathNewFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSelectOldFile;
        private System.Windows.Forms.Button btnSelectNewFile;
    }
}