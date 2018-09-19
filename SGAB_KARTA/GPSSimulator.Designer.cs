namespace SGAB.SGAB_Karta
{
    partial class GPSSimulator
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
            this.textBoxLoggPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonReadLog = new System.Windows.Forms.Button();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.labelNoOfPosts = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxLoggPath
            // 
            this.textBoxLoggPath.Location = new System.Drawing.Point(12, 22);
            this.textBoxLoggPath.Name = "textBoxLoggPath";
            this.textBoxLoggPath.Size = new System.Drawing.Size(450, 20);
            this.textBoxLoggPath.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sökväg till GPS-logg:";
            // 
            // buttonReadLog
            // 
            this.buttonReadLog.Location = new System.Drawing.Point(12, 48);
            this.buttonReadLog.Name = "buttonReadLog";
            this.buttonReadLog.Size = new System.Drawing.Size(75, 23);
            this.buttonReadLog.TabIndex = 2;
            this.buttonReadLog.Text = "Läs in logg";
            this.buttonReadLog.UseVisualStyleBackColor = true;
            this.buttonReadLog.Click += new System.EventHandler(this.buttonReadGPSLogg_Click);
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(468, 20);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse.TabIndex = 3;
            this.buttonBrowse.Text = "Bläddra...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Antal inlästa poster: ";
            // 
            // labelNoOfPosts
            // 
            this.labelNoOfPosts.AutoSize = true;
            this.labelNoOfPosts.Location = new System.Drawing.Point(117, 85);
            this.labelNoOfPosts.Name = "labelNoOfPosts";
            this.labelNoOfPosts.Size = new System.Drawing.Size(13, 13);
            this.labelNoOfPosts.TabIndex = 5;
            this.labelNoOfPosts.Text = "0";
            // 
            // GPSSimulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 262);
            this.Controls.Add(this.labelNoOfPosts);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.buttonReadLog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxLoggPath);
            this.Name = "GPSSimulator";
            this.Text = "GPSSimulator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxLoggPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonReadLog;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelNoOfPosts;
    }
}