using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace SGAB.SGAB_Karta
{
    partial class Karta
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Karta));
            TatukGIS.NDK.TGIS_CSUnknownCoordinateSystem tgiS_CSUnknownCoordinateSystem2 = new TatukGIS.NDK.TGIS_CSUnknownCoordinateSystem();
            TatukGIS.NDK.TGIS_CSUnits tgiS_CSUnits2 = new TatukGIS.NDK.TGIS_CSUnits();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
            this.btnLayerExtent = new System.Windows.Forms.ToolStripButton();
            this.btnZoomBox = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPan = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnMeasure = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuLangd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAreal = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSelectMultiple = new System.Windows.Forms.ToolStripButton();
            this.btnInfo = new System.Windows.Forms.ToolStripButton();
            this.btnHyperLink = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGPScenter = new System.Windows.Forms.ToolStripButton();
            this.btnUnselect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLegend = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnScaleBar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUpdateStatusInfo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.btnMapError = new System.Windows.Forms.ToolStripButton();
            this.btnAdmin = new System.Windows.Forms.ToolStripButton();
            this.tgisLegend = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.tgisKarta = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.tgisScale = new TatukGIS.NDK.WinForms.TGIS_ControlScale();
            this.splitPanel = new System.Windows.Forms.SplitContainer();
            this.lblSkala = new System.Windows.Forms.Label();
            this.lblXlabel = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.lblYlabel = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.txtSkala = new System.Windows.Forms.TextBox();
            this.groupBoxLabels = new System.Windows.Forms.GroupBox();
            this.lblMapOrGPSError = new System.Windows.Forms.Label();
            this.infoText = new System.Windows.Forms.Label();
            this.lblObjektAreal = new System.Windows.Forms.Label();
            this.dlgSaveImage = new System.Windows.Forms.SaveFileDialog();
            this._gps = new TatukGIS.NDK.WinForms.TGIS_GpsNmea();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.GIS_ControlPrintPreviewSimple = new TatukGIS.NDK.WinForms.TGIS_ControlPrintPreviewSimple();
            this.tgiS_GpsNmea1 = new TatukGIS.NDK.WinForms.TGIS_GpsNmea();
            this.toolStrip.SuspendLayout();
            this.tgisLegend.SuspendLayout();
            this.tgisKarta.SuspendLayout();
            this.splitPanel.Panel1.SuspendLayout();
            this.splitPanel.Panel2.SuspendLayout();
            this.splitPanel.SuspendLayout();
            this.groupBoxLabels.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFullExtent,
            this.btnLayerExtent,
            this.btnZoomBox,
            this.toolStripSeparator1,
            this.btnPan,
            this.toolStripSeparator2,
            this.btnMeasure,
            this.toolStripSeparator3,
            this.btnSelectMultiple,
            this.btnInfo,
            this.btnHyperLink,
            this.toolStripSeparator8,
            this.btnSave,
            this.btnPrint,
            this.toolStripSeparator7,
            this.btnAdd,
            this.btnRemove,
            this.toolStripSeparator4,
            this.btnGPScenter,
            this.btnUnselect,
            this.toolStripSeparator9,
            this.btnLegend,
            this.toolStripSeparator5,
            this.btnScaleBar,
            this.toolStripSeparator6,
            this.btnUpdateStatusInfo,
            this.toolStripSeparator10,
            this.btnMapError,
            this.btnAdmin});
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip.Size = new System.Drawing.Size(912, 48);
            this.toolStrip.Stretch = true;
            this.toolStrip.TabIndex = 2;
            // 
            // btnFullExtent
            // 
            this.btnFullExtent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFullExtent.Image = ((System.Drawing.Image)(resources.GetObject("btnFullExtent.Image")));
            this.btnFullExtent.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnFullExtent.ImageTransparentColor = System.Drawing.SystemColors.Control;
            this.btnFullExtent.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.Size = new System.Drawing.Size(52, 48);
            this.btnFullExtent.ToolTipText = "Zooma fullt";
            this.btnFullExtent.Click += new System.EventHandler(this.btnFullExtent_Click);
            // 
            // btnLayerExtent
            // 
            this.btnLayerExtent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLayerExtent.Image = ((System.Drawing.Image)(resources.GetObject("btnLayerExtent.Image")));
            this.btnLayerExtent.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnLayerExtent.ImageTransparentColor = System.Drawing.SystemColors.Control;
            this.btnLayerExtent.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnLayerExtent.Name = "btnLayerExtent";
            this.btnLayerExtent.Size = new System.Drawing.Size(52, 48);
            this.btnLayerExtent.ToolTipText = "Zooma till aktivt lager";
            this.btnLayerExtent.Click += new System.EventHandler(this.btnLayerExtent_Click);
            // 
            // btnZoomBox
            // 
            this.btnZoomBox.Checked = true;
            this.btnZoomBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnZoomBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomBox.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomBox.Image")));
            this.btnZoomBox.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnZoomBox.ImageTransparentColor = System.Drawing.SystemColors.Control;
            this.btnZoomBox.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.btnZoomBox.Name = "btnZoomBox";
            this.btnZoomBox.Size = new System.Drawing.Size(52, 48);
            this.btnZoomBox.ToolTipText = "Zooma in/ut";
            this.btnZoomBox.Click += new System.EventHandler(this.btnZoomBox_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 48);
            // 
            // btnPan
            // 
            this.btnPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPan.Image = ((System.Drawing.Image)(resources.GetObject("btnPan.Image")));
            this.btnPan.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnPan.ImageTransparentColor = System.Drawing.SystemColors.Control;
            this.btnPan.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.btnPan.Name = "btnPan";
            this.btnPan.Size = new System.Drawing.Size(52, 48);
            this.btnPan.ToolTipText = "Panorera/Flytta kartan";
            this.btnPan.Click += new System.EventHandler(this.btnPan_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 48);
            // 
            // btnMeasure
            // 
            this.btnMeasure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMeasure.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLangd,
            this.mnuAreal});
            this.btnMeasure.Image = ((System.Drawing.Image)(resources.GetObject("btnMeasure.Image")));
            this.btnMeasure.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnMeasure.ImageTransparentColor = System.Drawing.SystemColors.Control;
            this.btnMeasure.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.btnMeasure.Name = "btnMeasure";
            this.btnMeasure.Size = new System.Drawing.Size(45, 48);
            this.btnMeasure.ToolTipText = "Mätning";
            // 
            // mnuLangd
            // 
            this.mnuLangd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuLangd.Name = "mnuLangd";
            this.mnuLangd.Size = new System.Drawing.Size(107, 22);
            this.mnuLangd.Text = "Längd";
            this.mnuLangd.ToolTipText = "Mät längd";
            this.mnuLangd.Click += new System.EventHandler(this.mnuLangd_Click);
            // 
            // mnuAreal
            // 
            this.mnuAreal.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuAreal.Name = "mnuAreal";
            this.mnuAreal.Size = new System.Drawing.Size(107, 22);
            this.mnuAreal.Text = "Areal";
            this.mnuAreal.ToolTipText = "Mät areal";
            this.mnuAreal.Click += new System.EventHandler(this.mnuAreal_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 48);
            // 
            // btnSelectMultiple
            // 
            this.btnSelectMultiple.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSelectMultiple.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectMultiple.Image")));
            this.btnSelectMultiple.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSelectMultiple.ImageTransparentColor = System.Drawing.SystemColors.Control;
            this.btnSelectMultiple.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.btnSelectMultiple.Name = "btnSelectMultiple";
            this.btnSelectMultiple.Size = new System.Drawing.Size(36, 48);
            this.btnSelectMultiple.ToolTipText = "Välj och summera startplatser";
            this.btnSelectMultiple.Click += new System.EventHandler(this.btnSelectMultiple_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnInfo.Image = ((System.Drawing.Image)(resources.GetObject("btnInfo.Image")));
            this.btnInfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnInfo.ImageTransparentColor = System.Drawing.SystemColors.Control;
            this.btnInfo.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(36, 48);
            this.btnInfo.ToolTipText = "Information om beställning";
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // btnHyperLink
            // 
            this.btnHyperLink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHyperLink.Image = ((System.Drawing.Image)(resources.GetObject("btnHyperLink.Image")));
            this.btnHyperLink.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnHyperLink.ImageTransparentColor = System.Drawing.SystemColors.Control;
            this.btnHyperLink.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.btnHyperLink.Name = "btnHyperLink";
            this.btnHyperLink.Size = new System.Drawing.Size(36, 48);
            this.btnHyperLink.Text = "toolStripButton1";
            this.btnHyperLink.ToolTipText = "Öppna Excelark";
            this.btnHyperLink.Click += new System.EventHandler(this.btnHyperLink_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 48);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSave.ImageTransparentColor = System.Drawing.SystemColors.Control;
            this.btnSave.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(36, 48);
            this.btnSave.Text = "toolStripButton1";
            this.btnSave.ToolTipText = "Spara kartbild";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnPrint.ImageTransparentColor = System.Drawing.SystemColors.Control;
            this.btnPrint.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(36, 48);
            this.btnPrint.Text = "toolStripButton1";
            this.btnPrint.ToolTipText = "Skriv ut";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 48);
            // 
            // btnAdd
            // 
            this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAdd.ImageTransparentColor = System.Drawing.SystemColors.Control;
            this.btnAdd.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(36, 48);
            this.btnAdd.Text = "toolStripButton1";
            this.btnAdd.ToolTipText = "Lägg till nytt lager";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
            this.btnRemove.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnRemove.ImageTransparentColor = System.Drawing.SystemColors.Control;
            this.btnRemove.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(36, 48);
            this.btnRemove.Text = "toolStripButton1";
            this.btnRemove.ToolTipText = "Stäng ett eller flera lager";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 48);
            // 
            // btnGPScenter
            // 
            this.btnGPScenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnGPScenter.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline);
            this.btnGPScenter.Image = ((System.Drawing.Image)(resources.GetObject("btnGPScenter.Image")));
            this.btnGPScenter.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnGPScenter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGPScenter.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnGPScenter.Name = "btnGPScenter";
            this.btnGPScenter.Size = new System.Drawing.Size(83, 48);
            this.btnGPScenter.Text = "GPS-centrering";
            this.btnGPScenter.ToolTipText = "Växla mellan starta/stoppa GPS-centrering";
            this.btnGPScenter.Click += new System.EventHandler(this.btnGPScenter_Click);
            // 
            // btnUnselect
            // 
            this.btnUnselect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnUnselect.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline);
            this.btnUnselect.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnUnselect.Name = "btnUnselect";
            this.btnUnselect.Size = new System.Drawing.Size(63, 48);
            this.btnUnselect.Text = "Avmarkera";
            this.btnUnselect.ToolTipText = "Avmarkera alla startplatser";
            this.btnUnselect.Click += new System.EventHandler(this.btnUnselect_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 48);
            // 
            // btnLegend
            // 
            this.btnLegend.BackColor = System.Drawing.SystemColors.Control;
            this.btnLegend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnLegend.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline);
            this.btnLegend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLegend.Margin = new System.Windows.Forms.Padding(0);
            this.btnLegend.Name = "btnLegend";
            this.btnLegend.Size = new System.Drawing.Size(46, 17);
            this.btnLegend.Text = "Legend";
            this.btnLegend.ToolTipText = "Visa/Dölj legend";
            this.btnLegend.Click += new System.EventHandler(this.btnLegend_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 48);
            // 
            // btnScaleBar
            // 
            this.btnScaleBar.BackColor = System.Drawing.SystemColors.Control;
            this.btnScaleBar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnScaleBar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline);
            this.btnScaleBar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnScaleBar.Margin = new System.Windows.Forms.Padding(0);
            this.btnScaleBar.Name = "btnScaleBar";
            this.btnScaleBar.Size = new System.Drawing.Size(55, 17);
            this.btnScaleBar.Text = "Skalstock";
            this.btnScaleBar.ToolTipText = "Visa/Dölj skalstock";
            this.btnScaleBar.Click += new System.EventHandler(this.btnScaleBar_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 48);
            // 
            // btnUpdateStatusInfo
            // 
            this.btnUpdateStatusInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnUpdateStatusInfo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateStatusInfo.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateStatusInfo.Image")));
            this.btnUpdateStatusInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUpdateStatusInfo.Margin = new System.Windows.Forms.Padding(0);
            this.btnUpdateStatusInfo.Name = "btnUpdateStatusInfo";
            this.btnUpdateStatusInfo.Size = new System.Drawing.Size(100, 17);
            this.btnUpdateStatusInfo.Text = "Överför statusinfo";
            this.btnUpdateStatusInfo.Visible = false;
            this.btnUpdateStatusInfo.Click += new System.EventHandler(this.btnUpdateStatusInfo_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 6);
            // 
            // btnMapError
            // 
            this.btnMapError.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMapError.Image = ((System.Drawing.Image)(resources.GetObject("btnMapError.Image")));
            this.btnMapError.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnMapError.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapError.Name = "btnMapError";
            this.btnMapError.Size = new System.Drawing.Size(52, 52);
            this.btnMapError.Text = "Rapportera kartfel";
            this.btnMapError.Click += new System.EventHandler(this.btnMapError_Click);
            // 
            // btnAdmin
            // 
            this.btnAdmin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAdmin.Image = ((System.Drawing.Image)(resources.GetObject("btnAdmin.Image")));
            this.btnAdmin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdmin.Name = "btnAdmin";
            this.btnAdmin.Size = new System.Drawing.Size(47, 19);
            this.btnAdmin.Text = "Admin";
            this.btnAdmin.Click += new System.EventHandler(this.btnAdmin_Click);
            // 
            // tgisLegend
            // 
            this.tgisLegend.AutoScroll = true;
            this.tgisLegend.BackColor = System.Drawing.SystemColors.Control;
            this.tgisLegend.ColorSelected = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            this.tgisLegend.ColorSelectedFrame = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(105)))), ((int)(((byte)(198)))));
            this.tgisLegend.Controls.Add(this.tgiS_GpsNmea1);
            this.tgisLegend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tgisLegend.FontSubtitle = new System.Drawing.Font("Arial", 7F);
            this.tgisLegend.FontSubtitleColor = System.Drawing.SystemColors.WindowText;
            this.tgisLegend.FontTitle = new System.Drawing.Font("Arial", 8F);
            this.tgisLegend.FontTitleColor = System.Drawing.SystemColors.WindowText;
            this.tgisLegend.FreeThread = false;
            this.tgisLegend.GIS_Group = null;
            this.tgisLegend.GIS_Layer = null;
            this.tgisLegend.GIS_Viewer = this.tgisKarta;
            this.tgisLegend.Location = new System.Drawing.Point(0, 0);
            this.tgisLegend.Margin = new System.Windows.Forms.Padding(3, 30, 3, 3);
            this.tgisLegend.Mode = TatukGIS.NDK.WinForms.TGIS_ControlLegendMode.gisControlLegendModeLayers;
            this.tgisLegend.Name = "tgisLegend";
            this.tgisLegend.Options = ((TatukGIS.NDK.WinForms.TGIS_ControlLegendOptions)(((((TatukGIS.NDK.WinForms.TGIS_ControlLegendOptions.gisControlLegendAllowMove | TatukGIS.NDK.WinForms.TGIS_ControlLegendOptions.gisControlLegendAllowActive)
                        | TatukGIS.NDK.WinForms.TGIS_ControlLegendOptions.gisControlLegendAllowExpand)
                        | TatukGIS.NDK.WinForms.TGIS_ControlLegendOptions.gisControlLegendAllowParams)
                        | TatukGIS.NDK.WinForms.TGIS_ControlLegendOptions.gisControlLegendAllowSelect)));
            this.tgisLegend.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.tgisLegend.ReverseOrder = false;
            this.tgisLegend.Size = new System.Drawing.Size(466, 673);
            this.tgisLegend.Spacing = 3;
            this.tgisLegend.TabIndex = 4;
            this.tgisLegend.TabStop = true;
            this.tgisLegend.LayerSelect += new TatukGIS.NDK.TGIS_LayerEvent(this.ChangeActiveLayer);
            this.tgisLegend.LayerActiveChange += new TatukGIS.NDK.TGIS_LayerEvent(this.tgisLegend_LayerActiveChange);
            this.tgisLegend.LayerParamsChange += new TatukGIS.NDK.TGIS_LayerEvent(this.tgisLegend_LayerParamsChange);
            this.tgisLegend.OrderChange += new System.EventHandler(this.tgisLegend_OrderChange);
            // 
            // tgisKarta
            // 
            this.tgisKarta.AccessibleRole = System.Windows.Forms.AccessibleRole.Alert;
            this.tgisKarta.BackColor = System.Drawing.SystemColors.Window;
            this.tgisKarta.BigExtentMargin = -10;
            this.tgisKarta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tgisKarta.CausesValidation = false;
            this.tgisKarta.CodePage = 0;
            this.tgisKarta.Controls.Add(this.tgisScale);
            this.tgisKarta.Copyright = "TatukGIS Developer Kernel. $Rev: 7718 $ $Copyright (c)1997-2000 TATUK, (c)2000-20" +
                "09 TatukGIS. ALL RIGHTS RESERVED. $";
            tgiS_CSUnknownCoordinateSystem2.DescriptionEx = null;
            tgiS_CSUnknownCoordinateSystem2.ReversedCoordinates = false;
            this.tgisKarta.CS = tgiS_CSUnknownCoordinateSystem2;
            this.tgisKarta.Cursor = System.Windows.Forms.Cursors.Default;
            this.tgisKarta.CursorForDrag = System.Windows.Forms.Cursors.Default;
            this.tgisKarta.CursorForEdit = System.Windows.Forms.Cursors.Cross;
            this.tgisKarta.CursorForSelect = System.Windows.Forms.Cursors.Arrow;
            this.tgisKarta.CursorForUserDefined = System.Windows.Forms.Cursors.Default;
            this.tgisKarta.CursorForZoom = System.Windows.Forms.Cursors.Cross;
            this.tgisKarta.CursorForZoomEx = System.Windows.Forms.Cursors.Default;
            this.tgisKarta.Device = TatukGIS.NDK.TGIS_Device.gisDeviceWindow;
            this.tgisKarta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tgisKarta.FullPaint = true;
            //this.tgisKarta.GDIType = TatukGIS.NDK.TGIS_GdiType.gisGDI32;
            this.tgisKarta.Location = new System.Drawing.Point(0, 0);
            this.tgisKarta.MinZoomSize = -5;
            this.tgisKarta.Mode = TatukGIS.NDK.TGIS_ViewerMode.gisSelect;
            this.tgisKarta.MultiUserMode = TatukGIS.NDK.TGIS_MultiUser.gisDefault;
            this.tgisKarta.Name = "tgisKarta";
            this.tgisKarta.OutCodePage = 0;
            this.tgisKarta.PrinterModeForceBitmap = false;
            this.tgisKarta.PrinterTileSize = 512;
            this.tgisKarta.PrintFooter = null;
            this.tgisKarta.PrintFooterFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tgisKarta.PrintFooterFontColor = System.Drawing.SystemColors.WindowText;
            this.tgisKarta.PrintSubtitle = "";
            this.tgisKarta.PrintSubtitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tgisKarta.PrintSubtitleFontColor = System.Drawing.SystemColors.WindowText;
            this.tgisKarta.PrintTitle = "";
            this.tgisKarta.PrintTitleFont = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tgisKarta.PrintTitleFontColor = System.Drawing.SystemColors.WindowText;
            this.tgisKarta.RotationAngle = 0;
            this.tgisKarta.ScaleAsFloat = 1;
            this.tgisKarta.ScaleAsText = "1:1";
            this.tgisKarta.SelectionColor = System.Drawing.Color.Yellow;
            this.tgisKarta.SelectionOutlineOnly = true;
            this.tgisKarta.SelectionPattern = ((System.Drawing.Bitmap)(resources.GetObject("tgisKarta.SelectionPattern")));
            this.tgisKarta.SelectionTransparency = 100;
            this.tgisKarta.SelectionWidth = 100;
            this.tgisKarta.Size = new System.Drawing.Size(546, 673);
            this.tgisKarta.TabIndex = 3;
            this.tgisKarta.TemporaryScaleInternal = 0;
            this.tgisKarta.ViewportOffset = new System.Drawing.Point(0, 0);
            this.tgisKarta.ZoomEx = 15.047123889803848;
            this.tgisKarta.Resize += new System.EventHandler(this.tgisKarta_Resize);
            this.tgisKarta.DoubleClick += new System.EventHandler(this.tgisKarta_DoubleClick);
            this.tgisKarta.AfterPaint += new System.Windows.Forms.PaintEventHandler(this.tgisKarta_AfterPaint);
            this.tgisKarta.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tgisKarta_MouseMove);
            this.tgisKarta.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tgisKarta_MouseDown);
            // 
            // tgisScale
            // 
            this.tgisScale.BackColor = System.Drawing.SystemColors.Control;
            this.tgisScale.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tgisScale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tgisScale.DividerColor1 = System.Drawing.Color.Black;
            this.tgisScale.DividerColor2 = System.Drawing.Color.White;
            this.tgisScale.Dividers = 5;
            this.tgisScale.FreeThread = false;
            this.tgisScale.GIS_Viewer = this.tgisKarta;
            this.tgisScale.Location = new System.Drawing.Point(142, 651);
            this.tgisScale.Margin = new System.Windows.Forms.Padding(0);
            this.tgisScale.Name = "tgisScale";
            this.tgisScale.Size = new System.Drawing.Size(400, 18);
            this.tgisScale.TabIndex = 4;
            this.tgisScale.Transparent = true;
            tgiS_CSUnits2.DescriptionEx = null;
            this.tgisScale.Units = tgiS_CSUnits2;
            this.tgisScale.UnitsEPSG = 904201;
            // 
            // splitPanel
            // 
            this.splitPanel.BackColor = System.Drawing.SystemColors.Control;
            this.splitPanel.Location = new System.Drawing.Point(0, 48);
            this.splitPanel.Name = "splitPanel";
            // 
            // splitPanel.Panel1
            // 
            this.splitPanel.Panel1.Controls.Add(this.tgisKarta);
            // 
            // splitPanel.Panel2
            // 
            this.splitPanel.Panel2.Controls.Add(this.tgisLegend);
            this.splitPanel.Size = new System.Drawing.Size(1016, 673);
            this.splitPanel.SplitterDistance = 546;
            this.splitPanel.TabIndex = 0;
            // 
            // lblSkala
            // 
            this.lblSkala.AutoSize = true;
            this.lblSkala.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSkala.Location = new System.Drawing.Point(21, 23);
            this.lblSkala.Name = "lblSkala";
            this.lblSkala.Size = new System.Drawing.Size(54, 13);
            this.lblSkala.TabIndex = 2;
            this.lblSkala.Text = "Skala 1:";
            // 
            // lblXlabel
            // 
            this.lblXlabel.AutoSize = true;
            this.lblXlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXlabel.Location = new System.Drawing.Point(144, 23);
            this.lblXlabel.Name = "lblXlabel";
            this.lblXlabel.Size = new System.Drawing.Size(19, 13);
            this.lblXlabel.TabIndex = 5;
            this.lblXlabel.Text = "X:";
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(160, 23);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(37, 13);
            this.lblX.TabIndex = 6;
            this.lblX.Text = "xxxxxx";
            // 
            // lblYlabel
            // 
            this.lblYlabel.AutoSize = true;
            this.lblYlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYlabel.Location = new System.Drawing.Point(215, 23);
            this.lblYlabel.Name = "lblYlabel";
            this.lblYlabel.Size = new System.Drawing.Size(19, 13);
            this.lblYlabel.TabIndex = 7;
            this.lblYlabel.Text = "Y:";
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(229, 23);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(37, 13);
            this.lblY.TabIndex = 8;
            this.lblY.Text = "yyyyyy";
            // 
            // txtSkala
            // 
            this.txtSkala.Location = new System.Drawing.Point(72, 20);
            this.txtSkala.Name = "txtSkala";
            this.txtSkala.Size = new System.Drawing.Size(53, 20);
            this.txtSkala.TabIndex = 3;
            this.txtSkala.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSkala_KeyDown);
            // 
            // groupBoxLabels
            // 
            this.groupBoxLabels.BackColor = System.Drawing.SystemColors.Control;
            this.groupBoxLabels.Controls.Add(this.lblMapOrGPSError);
            this.groupBoxLabels.Controls.Add(this.infoText);
            this.groupBoxLabels.Controls.Add(this.txtSkala);
            this.groupBoxLabels.Controls.Add(this.lblSkala);
            this.groupBoxLabels.Controls.Add(this.lblY);
            this.groupBoxLabels.Controls.Add(this.lblYlabel);
            this.groupBoxLabels.Controls.Add(this.lblXlabel);
            this.groupBoxLabels.Controls.Add(this.lblX);
            this.groupBoxLabels.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBoxLabels.Location = new System.Drawing.Point(0, 718);
            this.groupBoxLabels.Margin = new System.Windows.Forms.Padding(0);
            this.groupBoxLabels.Name = "groupBoxLabels";
            this.groupBoxLabels.Padding = new System.Windows.Forms.Padding(0);
            this.groupBoxLabels.Size = new System.Drawing.Size(839, 48);
            this.groupBoxLabels.TabIndex = 0;
            this.groupBoxLabels.TabStop = false;
            // 
            // lblMapOrGPSError
            // 
            this.lblMapOrGPSError.AutoSize = true;
            this.lblMapOrGPSError.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMapOrGPSError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblMapOrGPSError.Location = new System.Drawing.Point(731, 21);
            this.lblMapOrGPSError.Name = "lblMapOrGPSError";
            this.lblMapOrGPSError.Size = new System.Drawing.Size(105, 17);
            this.lblMapOrGPSError.TabIndex = 30;
            this.lblMapOrGPSError.Text = "Fel registerat";
            this.lblMapOrGPSError.Visible = false;
            // 
            // infoText
            // 
            this.infoText.AutoSize = true;
            this.infoText.ForeColor = System.Drawing.Color.Red;
            this.infoText.Location = new System.Drawing.Point(282, 23);
            this.infoText.Name = "infoText";
            this.infoText.Size = new System.Drawing.Size(0, 13);
            this.infoText.TabIndex = 18;
            // 
            // lblObjektAreal
            // 
            this.lblObjektAreal.AutoSize = true;
            this.lblObjektAreal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblObjektAreal.Location = new System.Drawing.Point(276, 23);
            this.lblObjektAreal.Margin = new System.Windows.Forms.Padding(0);
            this.lblObjektAreal.Name = "lblObjektAreal";
            this.lblObjektAreal.Size = new System.Drawing.Size(51, 13);
            this.lblObjektAreal.TabIndex = 9;
            this.lblObjektAreal.Text = "Ytareal:";
            // 
            // dlgSaveImage
            // 
            this.dlgSaveImage.Filter = "BMP|*.BMP|JPG|*.JPG|PNG|*.PNG|TIF|*.TIF";
            this.dlgSaveImage.Title = "Export to Image";
            // 
            // Gps
            // 
            this._gps.Active = false;
            this._gps.BaudRate = 4800;
            this._gps.Com = 1;
            this._gps.Location = new System.Drawing.Point(0, 56);
            this._gps.Name = "Gps";
            this._gps.Size = new System.Drawing.Size(16, 160);
            this._gps.TabIndex = 29;
            this._gps.Text = " ";
            this._gps.Timeout = 1000;
            this._gps.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // GIS_ControlPrintPreviewSimple
            // 
            this.GIS_ControlPrintPreviewSimple.GIS_Viewer = this.tgisKarta;
            this.GIS_ControlPrintPreviewSimple.Text = "Print Preview";
            this.GIS_ControlPrintPreviewSimple.WindowHeight = 480;
            this.GIS_ControlPrintPreviewSimple.WindowLeft = 0;
            this.GIS_ControlPrintPreviewSimple.WindowPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.GIS_ControlPrintPreviewSimple.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.GIS_ControlPrintPreviewSimple.WindowTop = 0;
            this.GIS_ControlPrintPreviewSimple.WindowWidth = 640;
            // 
            // tgiS_GpsNmea1
            // 
            this.tgiS_GpsNmea1.Active = false;
            this.tgiS_GpsNmea1.BaudRate = 4800;
            this.tgiS_GpsNmea1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tgiS_GpsNmea1.Com = 1;
            this.tgiS_GpsNmea1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tgiS_GpsNmea1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tgiS_GpsNmea1.Location = new System.Drawing.Point(0, 30);
            this.tgiS_GpsNmea1.Name = "tgiS_GpsNmea1";
            this.tgiS_GpsNmea1.Size = new System.Drawing.Size(241, 639);
            this.tgiS_GpsNmea1.TabIndex = 3;
            this.tgiS_GpsNmea1.Text = " ";
            this.tgiS_GpsNmea1.Timeout = 1000;
            this.tgiS_GpsNmea1.Visible = false;
            // 
            // Karta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.groupBoxLabels);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.splitPanel);
            this.Controls.Add(this._gps);
            this.Name = "Karta";
            this.Size = new System.Drawing.Size(1016, 766);
            this.Load += new System.EventHandler(this.Karta_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.tgisLegend.ResumeLayout(false);
            this.tgisKarta.ResumeLayout(false);
            this.splitPanel.Panel1.ResumeLayout(false);
            this.splitPanel.Panel2.ResumeLayout(false);
            this.splitPanel.ResumeLayout(false);
            this.groupBoxLabels.ResumeLayout(false);
            this.groupBoxLabels.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnFullExtent;
        private System.Windows.Forms.ToolStripButton btnZoomBox;
        private System.Windows.Forms.ToolStripButton btnPan;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd tgisKarta;
        private TatukGIS.NDK.WinForms.TGIS_ControlLegend tgisLegend;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.SplitContainer splitPanel;
        private System.Windows.Forms.Label lblSkala;
        private TatukGIS.NDK.WinForms.TGIS_ControlScale tgisScale;
        private System.Windows.Forms.Label lblXlabel;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblYlabel;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.ToolStripButton btnSelectMultiple;
        private System.Windows.Forms.ToolTip toolTip;
        //private System.Windows.Forms.PictureBox picHektar;
        private System.Windows.Forms.ToolStripDropDownButton btnMeasure;
        private System.Windows.Forms.ToolStripMenuItem mnuLangd;
        private System.Windows.Forms.ToolStripMenuItem mnuAreal;
        private System.Windows.Forms.TextBox txtSkala;
        private System.Windows.Forms.GroupBox groupBoxLabels;
        private System.Windows.Forms.Label lblObjektAreal;
        private System.Windows.Forms.ToolStripButton btnInfo;
        private System.Windows.Forms.SaveFileDialog dlgSaveImage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnRemove;
        private System.Windows.Forms.ToolStripButton btnLayerExtent;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.ToolStripButton btnLegend;
        private System.Windows.Forms.ToolStripButton btnScaleBar;
        /* added by me */
        private System.Windows.Forms.ToolStripButton btnUnselect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton btnHyperLink;
        private System.Windows.Forms.ToolStripButton btnGPScenter;
        private System.Windows.Forms.ToolStripButton btnUpdateStatusInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton btnMapError;
        private System.Windows.Forms.Label lblMapOrGPSError;
        private System.Windows.Forms.ToolStripButton btnAdmin;
        private TGIS_GpsNmea tgiS_GpsNmea1;
      
               
    }
}
