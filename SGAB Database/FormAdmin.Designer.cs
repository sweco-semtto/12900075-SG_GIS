namespace SGAB.SGAB_Database
{
    partial class FormAdmin
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
            this.tabAdmin = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.dgForetag = new System.Windows.Forms.DataGridView();
            this.btnSync = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbSpread = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dgvStartplatser = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBind = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbFreight = new System.Windows.Forms.ComboBox();
            this.tabEntreprenorer = new System.Windows.Forms.TabPage();
            this.cbxSpread = new System.Windows.Forms.CheckBox();
            this.cbxFreight = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvEntreprenorer = new System.Windows.Forms.DataGridView();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button8 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.tabAdmin.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgForetag)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStartplatser)).BeginInit();
            this.tabEntreprenorer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntreprenorer)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabAdmin
            // 
            this.tabAdmin.Controls.Add(this.tabPage1);
            this.tabAdmin.Controls.Add(this.tabPage5);
            this.tabAdmin.Controls.Add(this.tabPage4);
            this.tabAdmin.Controls.Add(this.tabEntreprenorer);
            this.tabAdmin.Controls.Add(this.tabPage3);
            this.tabAdmin.Location = new System.Drawing.Point(9, 10);
            this.tabAdmin.Margin = new System.Windows.Forms.Padding(2);
            this.tabAdmin.Name = "tabAdmin";
            this.tabAdmin.SelectedIndex = 0;
            this.tabAdmin.Size = new System.Drawing.Size(598, 390);
            this.tabAdmin.TabIndex = 0;
            this.tabAdmin.SelectedIndexChanged += new System.EventHandler(this.tabAdmin_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.button7);
            this.tabPage1.Controls.Add(this.button6);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(590, 364);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Startplatser";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 58);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(155, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Ladda ned från domändatabas:";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(189, 54);
            this.button7.Margin = new System.Windows.Forms.Padding(2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(55, 24);
            this.button7.TabIndex = 2;
            this.button7.Text = "OK";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(189, 22);
            this.button6.Margin = new System.Windows.Forms.Padding(2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(55, 24);
            this.button6.TabIndex = 1;
            this.button6.Text = "OK";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ladda upp till domändatabas:";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.dgForetag);
            this.tabPage5.Controls.Add(this.btnSync);
            this.tabPage5.Controls.Add(this.label12);
            this.tabPage5.Controls.Add(this.label11);
            this.tabPage5.Controls.Add(this.button10);
            this.tabPage5.Controls.Add(this.textBox4);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(590, 364);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Beställningsdatabas";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // dgForetag
            // 
            this.dgForetag.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgForetag.Location = new System.Drawing.Point(17, 129);
            this.dgForetag.Name = "dgForetag";
            this.dgForetag.Size = new System.Drawing.Size(557, 234);
            this.dgForetag.TabIndex = 5;
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(14, 93);
            this.btnSync.Margin = new System.Windows.Forms.Padding(2);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(76, 25);
            this.btnSync.TabIndex = 4;
            this.btnSync.Text = "Synkronisera";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 39);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(167, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Sökväg till beställningsdatabasen:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 12);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(374, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Synkronisera med beställningsdatabas (Access) med domändatabas (MySQL):";
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(499, 93);
            this.button10.Margin = new System.Windows.Forms.Padding(2);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(76, 25);
            this.button10.TabIndex = 1;
            this.button10.Text = "Bläddra...";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(14, 64);
            this.textBox4.Margin = new System.Windows.Forms.Padding(2);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(561, 20);
            this.textBox4.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox1);
            this.tabPage4.Controls.Add(this.label10);
            this.tabPage4.Controls.Add(this.cmbSpread);
            this.tabPage4.Controls.Add(this.label8);
            this.tabPage4.Controls.Add(this.dgvStartplatser);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.btnBind);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.cmbFreight);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(590, 364);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Koppla startplatser";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Location = new System.Drawing.Point(358, 237);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 109);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sök";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(5, 79);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(73, 25);
            this.button2.TabIndex = 19;
            this.button2.Text = "Visa alla";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(6, 42);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(129, 17);
            this.checkBox2.TabIndex = 18;
            this.checkBox2.Text = "Spridningsentreprenör";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 19);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(104, 17);
            this.checkBox1.TabIndex = 17;
            this.checkBox1.Text = "Fraktentreprenör";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(146, 79);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(73, 25);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "Sök";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(358, 90);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 13);
            this.label10.TabIndex = 7;
            this.label10.Text = "Spridningsentreprenör:";
            // 
            // cmbSpread
            // 
            this.cmbSpread.FormattingEnabled = true;
            this.cmbSpread.Location = new System.Drawing.Point(358, 105);
            this.cmbSpread.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSpread.Name = "cmbSpread";
            this.cmbSpread.Size = new System.Drawing.Size(206, 21);
            this.cmbSpread.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 46);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Valda startplatser:";
            // 
            // dgvStartplatser
            // 
            this.dgvStartplatser.AllowUserToAddRows = false;
            this.dgvStartplatser.AllowUserToDeleteRows = false;
            this.dgvStartplatser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStartplatser.Location = new System.Drawing.Point(11, 63);
            this.dgvStartplatser.Margin = new System.Windows.Forms.Padding(2);
            this.dgvStartplatser.Name = "dgvStartplatser";
            this.dgvStartplatser.RowTemplate.Height = 24;
            this.dgvStartplatser.Size = new System.Drawing.Size(327, 297);
            this.dgvStartplatser.TabIndex = 4;
            this.dgvStartplatser.SelectionChanged += new System.EventHandler(this.dgvStartplatser_SelectionChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 20);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(201, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Välj startplatser som ska kopplas i kartan.";
            // 
            // btnBind
            // 
            this.btnBind.Location = new System.Drawing.Point(455, 155);
            this.btnBind.Margin = new System.Windows.Forms.Padding(2);
            this.btnBind.Name = "btnBind";
            this.btnBind.Size = new System.Drawing.Size(109, 23);
            this.btnBind.TabIndex = 2;
            this.btnBind.Text = "Koppla";
            this.btnBind.UseVisualStyleBackColor = true;
            this.btnBind.Click += new System.EventHandler(this.btnBind_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(355, 46);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Fraktentreprenör:";
            // 
            // cmbFreight
            // 
            this.cmbFreight.FormattingEnabled = true;
            this.cmbFreight.Location = new System.Drawing.Point(358, 63);
            this.cmbFreight.Margin = new System.Windows.Forms.Padding(2);
            this.cmbFreight.Name = "cmbFreight";
            this.cmbFreight.Size = new System.Drawing.Size(206, 21);
            this.cmbFreight.TabIndex = 0;
            // 
            // tabEntreprenorer
            // 
            this.tabEntreprenorer.Controls.Add(this.cbxSpread);
            this.tabEntreprenorer.Controls.Add(this.cbxFreight);
            this.tabEntreprenorer.Controls.Add(this.btnSave);
            this.tabEntreprenorer.Controls.Add(this.dgvEntreprenorer);
            this.tabEntreprenorer.Controls.Add(this.tbxName);
            this.tabEntreprenorer.Controls.Add(this.lblName);
            this.tabEntreprenorer.Location = new System.Drawing.Point(4, 22);
            this.tabEntreprenorer.Margin = new System.Windows.Forms.Padding(2);
            this.tabEntreprenorer.Name = "tabEntreprenorer";
            this.tabEntreprenorer.Padding = new System.Windows.Forms.Padding(2);
            this.tabEntreprenorer.Size = new System.Drawing.Size(590, 364);
            this.tabEntreprenorer.TabIndex = 1;
            this.tabEntreprenorer.Text = "Entreprenörer";
            this.tabEntreprenorer.UseVisualStyleBackColor = true;
            // 
            // cbxSpread
            // 
            this.cbxSpread.AutoSize = true;
            this.cbxSpread.Enabled = false;
            this.cbxSpread.Location = new System.Drawing.Point(355, 11);
            this.cbxSpread.Name = "cbxSpread";
            this.cbxSpread.Size = new System.Drawing.Size(129, 17);
            this.cbxSpread.TabIndex = 17;
            this.cbxSpread.Text = "Spridningsentreprenör";
            this.cbxSpread.UseVisualStyleBackColor = true;
            this.cbxSpread.CheckedChanged += new System.EventHandler(this.cbxSpread_CheckedChanged);
            // 
            // cbxFreight
            // 
            this.cbxFreight.AutoSize = true;
            this.cbxFreight.Enabled = false;
            this.cbxFreight.Location = new System.Drawing.Point(245, 11);
            this.cbxFreight.Name = "cbxFreight";
            this.cbxFreight.Size = new System.Drawing.Size(104, 17);
            this.cbxFreight.TabIndex = 16;
            this.cbxFreight.Text = "Fraktentreprenör";
            this.cbxFreight.UseVisualStyleBackColor = true;
            this.cbxFreight.CheckedChanged += new System.EventHandler(this.cbxFreight_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(482, 335);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(104, 25);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Spara Ändringar";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgvEntreprenorer
            // 
            this.dgvEntreprenorer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEntreprenorer.Location = new System.Drawing.Point(4, 36);
            this.dgvEntreprenorer.Margin = new System.Windows.Forms.Padding(2);
            this.dgvEntreprenorer.Name = "dgvEntreprenorer";
            this.dgvEntreprenorer.RowTemplate.Height = 24;
            this.dgvEntreprenorer.Size = new System.Drawing.Size(582, 295);
            this.dgvEntreprenorer.TabIndex = 4;
            this.dgvEntreprenorer.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvEntreprenorer_RowHeaderMouseClick);
            this.dgvEntreprenorer.SelectionChanged += new System.EventHandler(this.dgvEntreprenorer_SelectionChanged);
            // 
            // tbxName
            // 
            this.tbxName.Enabled = false;
            this.tbxName.Location = new System.Drawing.Point(56, 12);
            this.tbxName.Margin = new System.Windows.Forms.Padding(2);
            this.tbxName.Name = "tbxName";
            this.tbxName.Size = new System.Drawing.Size(170, 20);
            this.tbxName.TabIndex = 1;
            this.tbxName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbxName_KeyUp);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Enabled = false;
            this.lblName.Location = new System.Drawing.Point(4, 12);
            this.lblName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Namn:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.textBox2);
            this.tabPage3.Controls.Add(this.button8);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.button4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(590, 364);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Historik";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(64, 41);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(182, 20);
            this.textBox2.TabIndex = 4;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(250, 40);
            this.button8.Margin = new System.Windows.Forms.Padding(2);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 21);
            this.button8.TabIndex = 3;
            this.button8.Text = "Bläddra...";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 44);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Filnamn:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 19);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(235, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Välj en startplats i kartan och sedan Skapa logg.";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(250, 67);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 21);
            this.button4.TabIndex = 0;
            this.button4.Text = "Skapa logg";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // FormAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 403);
            this.Controls.Add(this.tabAdmin);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormAdmin";
            this.Text = "SG-GIS";
            this.tabAdmin.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgForetag)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStartplatser)).EndInit();
            this.tabEntreprenorer.ResumeLayout(false);
            this.tabEntreprenorer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntreprenorer)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabAdmin;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabEntreprenorer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnBind;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbFreight;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvEntreprenorer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgvStartplatser;
        private System.Windows.Forms.TextBox tbxName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbSpread;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.CheckBox cbxSpread;
        private System.Windows.Forms.CheckBox cbxFreight;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgForetag;
    }
}