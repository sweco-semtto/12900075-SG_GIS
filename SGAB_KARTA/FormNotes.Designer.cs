namespace SGAB.SGAB_Karta
{
    partial class FormNotes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNotes));
            this.txbNote1 = new System.Windows.Forms.TextBox();
            this.txbNote2 = new System.Windows.Forms.TextBox();
            this.txbNote3 = new System.Windows.Forms.TextBox();
            this.txbNote4 = new System.Windows.Forms.TextBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl3 = new System.Windows.Forms.Label();
            this.lbl4 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txbNote1
            // 
            this.txbNote1.Location = new System.Drawing.Point(12, 36);
            this.txbNote1.Name = "txbNote1";
            this.txbNote1.Size = new System.Drawing.Size(495, 20);
            this.txbNote1.TabIndex = 0;
            this.txbNote1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbNote1_KeyDown);
            // 
            // txbNote2
            // 
            this.txbNote2.Location = new System.Drawing.Point(12, 90);
            this.txbNote2.Name = "txbNote2";
            this.txbNote2.Size = new System.Drawing.Size(495, 20);
            this.txbNote2.TabIndex = 1;
            this.txbNote2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbNote2_KeyDown);
            // 
            // txbNote3
            // 
            this.txbNote3.Location = new System.Drawing.Point(12, 150);
            this.txbNote3.Name = "txbNote3";
            this.txbNote3.Size = new System.Drawing.Size(495, 20);
            this.txbNote3.TabIndex = 2;
            this.txbNote3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbNote3_KeyDown);
            // 
            // txbNote4
            // 
            this.txbNote4.Location = new System.Drawing.Point(12, 210);
            this.txbNote4.Name = "txbNote4";
            this.txbNote4.Size = new System.Drawing.Size(495, 20);
            this.txbNote4.TabIndex = 3;
            this.txbNote4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbNote4_KeyDown);
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(9, 20);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(119, 13);
            this.lbl1.TabIndex = 4;
            this.lbl1.Text = "Kommentar Ej påbörjad:";
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(9, 76);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(138, 13);
            this.lbl2.TabIndex = 5;
            this.lbl2.Text = "Kommentar Gödsel utkörda:";
            // 
            // lbl3
            // 
            this.lbl3.AutoSize = true;
            this.lbl3.Location = new System.Drawing.Point(9, 136);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(129, 13);
            this.lbl3.TabIndex = 6;
            this.lbl3.Text = "Kommentar Färdiggödslat:";
            // 
            // lbl4
            // 
            this.lbl4.AutoSize = true;
            this.lbl4.Location = new System.Drawing.Point(9, 196);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(144, 13);
            this.lbl4.TabIndex = 7;
            this.lbl4.Text = "Kommentar Säckar hämtade:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(432, 251);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Spara";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FormNotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 286);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lbl4);
            this.Controls.Add(this.lbl3);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.txbNote4);
            this.Controls.Add(this.txbNote3);
            this.Controls.Add(this.txbNote2);
            this.Controls.Add(this.txbNote1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormNotes";
            this.Text = "Skriv in en kort kommentar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbNote1;
        private System.Windows.Forms.TextBox txbNote2;
        private System.Windows.Forms.TextBox txbNote3;
        private System.Windows.Forms.TextBox txbNote4;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Label lbl4;
        private System.Windows.Forms.Button btnSave;
    }
}