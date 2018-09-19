namespace SGAB.SGAB_Karta
{
    partial class frmInfo
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
            TatukGIS.NDK.TGIS_CSUnits tgiS_CSUnits1 = new TatukGIS.NDK.TGIS_CSUnits();
            this.controlAttributes = new TatukGIS.NDK.WinForms.TGIS_ControlAttributes();
            this.SuspendLayout();
            // 
            // controlAttributes
            // 
            this.controlAttributes.AllowNull = false;
            this.controlAttributes.AllowRestructure = false;
            this.controlAttributes.AutoSize = true;
            this.controlAttributes.ColorFont = System.Drawing.SystemColors.ControlText;
            this.controlAttributes.ColorGrid = System.Drawing.SystemColors.Info;
            this.controlAttributes.ColorHeader = System.Drawing.SystemColors.Control;
            this.controlAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlAttributes.FieldNameColumnSize = 60;
            this.controlAttributes.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.controlAttributes.Location = new System.Drawing.Point(0, 0);
            this.controlAttributes.Name = "controlAttributes";
            this.controlAttributes.ReadOnly = true;
            this.controlAttributes.ShowInternalFields = true;
            this.controlAttributes.Size = new System.Drawing.Size(404, 377);
            this.controlAttributes.TabIndex = 0;
            tgiS_CSUnits1.DescriptionEx = null;
            this.controlAttributes.Units = tgiS_CSUnits1;
            this.controlAttributes.UnitsEPSG = 904201;
            // 
            // frmInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(404, 377);
            this.Controls.Add(this.controlAttributes);
            this.Name = "frmInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Information";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmInfo_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}