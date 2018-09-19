namespace SGAB.SGAB_Karta
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAdmin));
            this.tabTest = new System.Windows.Forms.TabControl();
            this.tabSynkronisera = new System.Windows.Forms.TabPage();
            this.dgvForetag = new System.Windows.Forms.DataGridView();
            this.btnSync = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtDBPath = new System.Windows.Forms.TextBox();
            this.tabStartplatser = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.cmbFreight = new System.Windows.Forms.ComboBox();
            this.cmbSpread = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnSearchStartplatser = new System.Windows.Forms.Button();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.btnShowInMap = new System.Windows.Forms.Button();
            this.dgvStartplatser = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBind = new System.Windows.Forms.Button();
            this.tabEntreprenorer = new System.Windows.Forms.TabPage();
            this.cbxSpread = new System.Windows.Forms.CheckBox();
            this.cbxFreight = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvEntreprenorer = new System.Windows.Forms.DataGridView();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.tabHistorik = new System.Windows.Forms.TabPage();
            this.dgvStatus = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblClearTestData = new System.Windows.Forms.Label();
            this.btnClearTestData = new System.Windows.Forms.Button();
            this.lblTestMode = new System.Windows.Forms.Label();
            this.cbxTestMode = new System.Windows.Forms.CheckBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tabTest.SuspendLayout();
            this.tabSynkronisera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvForetag)).BeginInit();
            this.tabStartplatser.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStartplatser)).BeginInit();
            this.tabEntreprenorer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntreprenorer)).BeginInit();
            this.tabHistorik.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatus)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabTest
            // 
            this.tabTest.Controls.Add(this.tabSynkronisera);
            this.tabTest.Controls.Add(this.tabStartplatser);
            this.tabTest.Controls.Add(this.tabEntreprenorer);
            this.tabTest.Controls.Add(this.tabHistorik);
            this.tabTest.Controls.Add(this.tabPage1);
            this.tabTest.Location = new System.Drawing.Point(9, 10);
            this.tabTest.Margin = new System.Windows.Forms.Padding(2);
            this.tabTest.Name = "tabTest";
            this.tabTest.SelectedIndex = 0;
            this.tabTest.Size = new System.Drawing.Size(598, 471);
            this.tabTest.TabIndex = 0;
            this.tabTest.SelectedIndexChanged += new System.EventHandler(this.tabAdmin_SelectedIndexChanged);
            this.tabTest.Click += new System.EventHandler(this.tabTest_Click);
            // 
            // tabSynkronisera
            // 
            this.tabSynkronisera.Controls.Add(this.dgvForetag);
            this.tabSynkronisera.Controls.Add(this.btnSync);
            this.tabSynkronisera.Controls.Add(this.label12);
            this.tabSynkronisera.Controls.Add(this.label11);
            this.tabSynkronisera.Controls.Add(this.btnBrowse);
            this.tabSynkronisera.Controls.Add(this.txtDBPath);
            this.tabSynkronisera.Location = new System.Drawing.Point(4, 22);
            this.tabSynkronisera.Margin = new System.Windows.Forms.Padding(2);
            this.tabSynkronisera.Name = "tabSynkronisera";
            this.tabSynkronisera.Size = new System.Drawing.Size(590, 445);
            this.tabSynkronisera.TabIndex = 4;
            this.tabSynkronisera.Text = "Beställningsdatabas";
            this.tabSynkronisera.UseVisualStyleBackColor = true;
            // 
            // dgvForetag
            // 
            this.dgvForetag.AllowUserToAddRows = false;
            this.dgvForetag.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvForetag.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvForetag.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvForetag.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvForetag.Location = new System.Drawing.Point(17, 129);
            this.dgvForetag.Name = "dgvForetag";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvForetag.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvForetag.Size = new System.Drawing.Size(557, 234);
            this.dgvForetag.TabIndex = 5;
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
            this.label11.Size = new System.Drawing.Size(376, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Synkronisera lokal beställningsdatabas (Access) med domändatabas (MySQL):";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(499, 93);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(76, 25);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Bläddra...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtDBPath
            // 
            this.txtDBPath.Location = new System.Drawing.Point(14, 64);
            this.txtDBPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtDBPath.Name = "txtDBPath";
            this.txtDBPath.Size = new System.Drawing.Size(561, 20);
            this.txtDBPath.TabIndex = 0;
            // 
            // tabStartplatser
            // 
            this.tabStartplatser.Controls.Add(this.groupBox1);
            this.tabStartplatser.Controls.Add(this.btnSearchStartplatser);
            this.tabStartplatser.Controls.Add(this.btnShowAll);
            this.tabStartplatser.Controls.Add(this.btnShowInMap);
            this.tabStartplatser.Controls.Add(this.dgvStartplatser);
            this.tabStartplatser.Controls.Add(this.label5);
            this.tabStartplatser.Controls.Add(this.btnBind);
            this.tabStartplatser.Location = new System.Drawing.Point(4, 22);
            this.tabStartplatser.Margin = new System.Windows.Forms.Padding(2);
            this.tabStartplatser.Name = "tabStartplatser";
            this.tabStartplatser.Size = new System.Drawing.Size(590, 445);
            this.tabStartplatser.TabIndex = 3;
            this.tabStartplatser.Text = "Koppla startplatser";
            this.tabStartplatser.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Controls.Add(this.cmbStatus);
            this.groupBox1.Controls.Add(this.cmbFreight);
            this.groupBox1.Controls.Add(this.cmbSpread);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Location = new System.Drawing.Point(11, 306);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(362, 103);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 16);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Fraktentreprenör:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(228, 16);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 22;
            this.lblStatus.Text = "Status:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(231, 34);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(114, 21);
            this.cmbStatus.TabIndex = 21;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // cmbFreight
            // 
            this.cmbFreight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFreight.FormattingEnabled = true;
            this.cmbFreight.Location = new System.Drawing.Point(8, 34);
            this.cmbFreight.Margin = new System.Windows.Forms.Padding(2);
            this.cmbFreight.Name = "cmbFreight";
            this.cmbFreight.Size = new System.Drawing.Size(206, 21);
            this.cmbFreight.TabIndex = 0;
            this.cmbFreight.SelectedIndexChanged += new System.EventHandler(this.cmbFreight_SelectedIndexChanged);
            // 
            // cmbSpread
            // 
            this.cmbSpread.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpread.FormattingEnabled = true;
            this.cmbSpread.Location = new System.Drawing.Point(7, 74);
            this.cmbSpread.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSpread.Name = "cmbSpread";
            this.cmbSpread.Size = new System.Drawing.Size(206, 21);
            this.cmbSpread.TabIndex = 6;
            this.cmbSpread.SelectedIndexChanged += new System.EventHandler(this.cmbSpread_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 59);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 13);
            this.label10.TabIndex = 7;
            this.label10.Text = "Spridningsentreprenör:";
            // 
            // btnSearchStartplatser
            // 
            this.btnSearchStartplatser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearchStartplatser.Location = new System.Drawing.Point(11, 415);
            this.btnSearchStartplatser.Name = "btnSearchStartplatser";
            this.btnSearchStartplatser.Size = new System.Drawing.Size(84, 23);
            this.btnSearchStartplatser.TabIndex = 20;
            this.btnSearchStartplatser.Text = "Sök";
            this.btnSearchStartplatser.UseVisualStyleBackColor = true;
            this.btnSearchStartplatser.Click += new System.EventHandler(this.btnSearchStartplatser_Click);
            // 
            // btnShowAll
            // 
            this.btnShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnShowAll.Location = new System.Drawing.Point(388, 319);
            this.btnShowAll.Margin = new System.Windows.Forms.Padding(2);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(93, 25);
            this.btnShowAll.TabIndex = 19;
            this.btnShowAll.Text = "Visa alla";
            this.btnShowAll.UseVisualStyleBackColor = true;
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // btnShowInMap
            // 
            this.btnShowInMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnShowInMap.Location = new System.Drawing.Point(485, 319);
            this.btnShowInMap.Margin = new System.Windows.Forms.Padding(2);
            this.btnShowInMap.Name = "btnShowInMap";
            this.btnShowInMap.Size = new System.Drawing.Size(93, 25);
            this.btnShowInMap.TabIndex = 13;
            this.btnShowInMap.Text = "Markera i karta";
            this.btnShowInMap.UseVisualStyleBackColor = true;
            this.btnShowInMap.Click += new System.EventHandler(this.btnShowInMap_Click);
            // 
            // dgvStartplatser
            // 
            this.dgvStartplatser.AllowUserToAddRows = false;
            this.dgvStartplatser.AllowUserToDeleteRows = false;
            this.dgvStartplatser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStartplatser.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStartplatser.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvStartplatser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStartplatser.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvStartplatser.Location = new System.Drawing.Point(2, 44);
            this.dgvStartplatser.Margin = new System.Windows.Forms.Padding(2);
            this.dgvStartplatser.Name = "dgvStartplatser";
            this.dgvStartplatser.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStartplatser.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvStartplatser.RowTemplate.Height = 24;
            this.dgvStartplatser.Size = new System.Drawing.Size(586, 257);
            this.dgvStartplatser.TabIndex = 4;
            this.dgvStartplatser.SelectionChanged += new System.EventHandler(this.dgvStartplatser_SelectionChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 12);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(400, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Välj startplatser genom att markera en eller flera rader i listan, eller selekter" +
    "a i kartan.";
            // 
            // btnBind
            // 
            this.btnBind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBind.Location = new System.Drawing.Point(289, 415);
            this.btnBind.Margin = new System.Windows.Forms.Padding(2);
            this.btnBind.Name = "btnBind";
            this.btnBind.Size = new System.Drawing.Size(84, 23);
            this.btnBind.TabIndex = 2;
            this.btnBind.Text = "Koppla";
            this.btnBind.UseVisualStyleBackColor = true;
            this.btnBind.Click += new System.EventHandler(this.btnBind_Click);
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
            this.tabEntreprenorer.Size = new System.Drawing.Size(590, 445);
            this.tabEntreprenorer.TabIndex = 1;
            this.tabEntreprenorer.Text = "Entreprenörer";
            this.tabEntreprenorer.UseVisualStyleBackColor = true;
            // 
            // cbxSpread
            // 
            this.cbxSpread.AutoSize = true;
            this.cbxSpread.Enabled = false;
            this.cbxSpread.Location = new System.Drawing.Point(355, 12);
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
            this.cbxFreight.Location = new System.Drawing.Point(245, 12);
            this.cbxFreight.Name = "cbxFreight";
            this.cbxFreight.Size = new System.Drawing.Size(104, 17);
            this.cbxFreight.TabIndex = 16;
            this.cbxFreight.Text = "Fraktentreprenör";
            this.cbxFreight.UseVisualStyleBackColor = true;
            this.cbxFreight.CheckedChanged += new System.EventHandler(this.cbxFreight_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(480, 416);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(104, 25);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Spara ändringar";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgvEntreprenorer
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEntreprenorer.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvEntreprenorer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEntreprenorer.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvEntreprenorer.Location = new System.Drawing.Point(2, 44);
            this.dgvEntreprenorer.Margin = new System.Windows.Forms.Padding(2);
            this.dgvEntreprenorer.Name = "dgvEntreprenorer";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEntreprenorer.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvEntreprenorer.RowTemplate.Height = 24;
            this.dgvEntreprenorer.Size = new System.Drawing.Size(582, 368);
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
            this.lblName.Location = new System.Drawing.Point(11, 12);
            this.lblName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Namn:";
            // 
            // tabHistorik
            // 
            this.tabHistorik.Controls.Add(this.dgvStatus);
            this.tabHistorik.Controls.Add(this.label3);
            this.tabHistorik.Location = new System.Drawing.Point(4, 22);
            this.tabHistorik.Margin = new System.Windows.Forms.Padding(2);
            this.tabHistorik.Name = "tabHistorik";
            this.tabHistorik.Size = new System.Drawing.Size(590, 445);
            this.tabHistorik.TabIndex = 2;
            this.tabHistorik.Text = "Historik";
            this.tabHistorik.UseVisualStyleBackColor = true;
            // 
            // dgvStatus
            // 
            this.dgvStatus.AllowUserToAddRows = false;
            this.dgvStatus.AllowUserToDeleteRows = false;
            this.dgvStatus.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStatus.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStatus.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvStatus.Location = new System.Drawing.Point(2, 44);
            this.dgvStatus.Name = "dgvStatus";
            this.dgvStatus.ReadOnly = true;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStatus.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvStatus.Size = new System.Drawing.Size(585, 381);
            this.dgvStatus.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Statushistorik för vald startplats:";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblClearTestData);
            this.tabPage1.Controls.Add(this.btnClearTestData);
            this.tabPage1.Controls.Add(this.lblTestMode);
            this.tabPage1.Controls.Add(this.cbxTestMode);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(590, 445);
            this.tabPage1.TabIndex = 5;
            this.tabPage1.Text = "Testläge";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblClearTestData
            // 
            this.lblClearTestData.AutoSize = true;
            this.lblClearTestData.Location = new System.Drawing.Point(9, 113);
            this.lblClearTestData.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblClearTestData.Name = "lblClearTestData";
            this.lblClearTestData.Size = new System.Drawing.Size(236, 13);
            this.lblClearTestData.TabIndex = 22;
            this.lblClearTestData.Text = "Rensa testadata innebär att all testdata tas bort. ";
            // 
            // btnClearTestData
            // 
            this.btnClearTestData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearTestData.Location = new System.Drawing.Point(12, 140);
            this.btnClearTestData.Name = "btnClearTestData";
            this.btnClearTestData.Size = new System.Drawing.Size(109, 23);
            this.btnClearTestData.TabIndex = 21;
            this.btnClearTestData.Text = "Rensa testdata";
            this.btnClearTestData.UseVisualStyleBackColor = true;
            this.btnClearTestData.Click += new System.EventHandler(this.btnClearTestData_Click);
            // 
            // lblTestMode
            // 
            this.lblTestMode.AutoSize = true;
            this.lblTestMode.Location = new System.Drawing.Point(9, 48);
            this.lblTestMode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTestMode.Name = "lblTestMode";
            this.lblTestMode.Size = new System.Drawing.Size(554, 13);
            this.lblTestMode.TabIndex = 19;
            this.lblTestMode.Text = "Testläge innebär att nästa gång SG-GIS startas kommer startplatser att hämtas frå" +
    "n testmiljön istället för skarpa miljön";
            // 
            // cbxTestMode
            // 
            this.cbxTestMode.AutoSize = true;
            this.cbxTestMode.Location = new System.Drawing.Point(12, 13);
            this.cbxTestMode.Name = "cbxTestMode";
            this.cbxTestMode.Size = new System.Drawing.Size(92, 17);
            this.cbxTestMode.TabIndex = 18;
            this.cbxTestMode.Text = "Gå till testläge";
            this.cbxTestMode.UseVisualStyleBackColor = true;
            this.cbxTestMode.CheckedChanged += new System.EventHandler(this.cbxTestMode_CheckedChanged);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // FormAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 488);
            this.Controls.Add(this.tabTest);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormAdmin";
            this.Text = "Admin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAdmin_FormClosing);
            this.Load += new System.EventHandler(this.FormAdmin_Load);
            this.tabTest.ResumeLayout(false);
            this.tabSynkronisera.ResumeLayout(false);
            this.tabSynkronisera.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvForetag)).EndInit();
            this.tabStartplatser.ResumeLayout(false);
            this.tabStartplatser.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStartplatser)).EndInit();
            this.tabEntreprenorer.ResumeLayout(false);
            this.tabEntreprenorer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntreprenorer)).EndInit();
            this.tabHistorik.ResumeLayout(false);
            this.tabHistorik.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatus)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabTest;
        private System.Windows.Forms.TabPage tabEntreprenorer;
        private System.Windows.Forms.TabPage tabStartplatser;
        private System.Windows.Forms.TabPage tabHistorik;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnBind;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbFreight;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvEntreprenorer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvStartplatser;
        private System.Windows.Forms.TextBox tbxName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbSpread;
        private System.Windows.Forms.TabPage tabSynkronisera;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtDBPath;
        private System.Windows.Forms.CheckBox cbxSpread;
        private System.Windows.Forms.CheckBox cbxFreight;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.Button btnShowInMap;
        private System.Windows.Forms.DataGridView dgvForetag;
        private System.Windows.Forms.Button btnSearchStartplatser;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvStatus;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lblTestMode;
        private System.Windows.Forms.CheckBox cbxTestMode;
        private System.Windows.Forms.Label lblClearTestData;
        private System.Windows.Forms.Button btnClearTestData;
    }
}