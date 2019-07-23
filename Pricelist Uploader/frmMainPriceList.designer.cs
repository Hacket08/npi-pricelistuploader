partial class frmMainPriceList
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
            this.txtPriceList = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cmbWorkSheet = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtExcelFile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvItemGroup = new System.Windows.Forms.DataGridView();
            this.lblRefresh = new System.Windows.Forms.Label();
            this.btnItemGroupSave = new System.Windows.Forms.Button();
            this.lblFolderBrws = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnItemListSave = new System.Windows.Forms.Button();
            this.dgvItemList = new System.Windows.Forms.DataGridView();
            this.lblStatus1 = new System.Windows.Forms.Label();
            this.tssDataStatus = new System.Windows.Forms.Label();
            this.pbDataProgress = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.txtItemSearch = new System.Windows.Forms.TextBox();
            this.ofdExcel = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPriceList
            // 
            this.txtPriceList.Location = new System.Drawing.Point(100, 8);
            this.txtPriceList.Name = "txtPriceList";
            this.txtPriceList.Size = new System.Drawing.Size(393, 22);
            this.txtPriceList.TabIndex = 44;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "Default Price List";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.DarkGreen;
            this.label2.Location = new System.Drawing.Point(-10, -10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1261, 13);
            this.label2.TabIndex = 45;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(5, 36);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1228, 511);
            this.tabControl1.TabIndex = 46;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnItemGroupSave);
            this.tabPage1.Controls.Add(this.dgvItemGroup);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1220, 485);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Item Group";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.txtItemSearch);
            this.tabPage2.Controls.Add(this.btnItemListSave);
            this.tabPage2.Controls.Add(this.dgvItemList);
            this.tabPage2.Controls.Add(this.lblFolderBrws);
            this.tabPage2.Controls.Add(this.btnGenerate);
            this.tabPage2.Controls.Add(this.cmbWorkSheet);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.txtExcelFile);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1220, 485);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Item List";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cmbWorkSheet
            // 
            this.cmbWorkSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWorkSheet.FormattingEnabled = true;
            this.cmbWorkSheet.Location = new System.Drawing.Point(74, 32);
            this.cmbWorkSheet.Name = "cmbWorkSheet";
            this.cmbWorkSheet.Size = new System.Drawing.Size(193, 21);
            this.cmbWorkSheet.TabIndex = 200;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 199;
            this.label7.Text = "Worksheet";
            // 
            // txtExcelFile
            // 
            this.txtExcelFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExcelFile.Location = new System.Drawing.Point(74, 6);
            this.txtExcelFile.Name = "txtExcelFile";
            this.txtExcelFile.ReadOnly = true;
            this.txtExcelFile.Size = new System.Drawing.Size(1117, 22);
            this.txtExcelFile.TabIndex = 198;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 197;
            this.label3.Text = "Data File";
            // 
            // dgvItemGroup
            // 
            this.dgvItemGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItemGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemGroup.Location = new System.Drawing.Point(6, 6);
            this.dgvItemGroup.Name = "dgvItemGroup";
            this.dgvItemGroup.Size = new System.Drawing.Size(1208, 444);
            this.dgvItemGroup.TabIndex = 12;
            // 
            // lblRefresh
            // 
            this.lblRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblRefresh.Image = global::Pricelist_Uploader.Properties.Resources.redo;
            this.lblRefresh.Location = new System.Drawing.Point(492, 8);
            this.lblRefresh.Name = "lblRefresh";
            this.lblRefresh.Size = new System.Drawing.Size(24, 22);
            this.lblRefresh.TabIndex = 195;
            this.lblRefresh.Click += new System.EventHandler(this.lblRefresh_Click);
            // 
            // btnItemGroupSave
            // 
            this.btnItemGroupSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnItemGroupSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnItemGroupSave.Image = global::Pricelist_Uploader.Properties.Resources.filesave;
            this.btnItemGroupSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnItemGroupSave.Location = new System.Drawing.Point(6, 455);
            this.btnItemGroupSave.Name = "btnItemGroupSave";
            this.btnItemGroupSave.Size = new System.Drawing.Size(101, 24);
            this.btnItemGroupSave.TabIndex = 202;
            this.btnItemGroupSave.Text = "Save";
            this.btnItemGroupSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnItemGroupSave.UseVisualStyleBackColor = true;
            this.btnItemGroupSave.Click += new System.EventHandler(this.btnItemGroupSave_Click);
            // 
            // lblFolderBrws
            // 
            this.lblFolderBrws.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFolderBrws.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblFolderBrws.Image = global::Pricelist_Uploader.Properties.Resources._16_view;
            this.lblFolderBrws.Location = new System.Drawing.Point(1192, 6);
            this.lblFolderBrws.Name = "lblFolderBrws";
            this.lblFolderBrws.Size = new System.Drawing.Size(24, 22);
            this.lblFolderBrws.TabIndex = 202;
            this.lblFolderBrws.Click += new System.EventHandler(this.lblFolderBrws_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerate.Image = global::Pricelist_Uploader.Properties.Resources._16_getlist;
            this.btnGenerate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerate.Location = new System.Drawing.Point(271, 31);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(118, 24);
            this.btnGenerate.TabIndex = 201;
            this.btnGenerate.Text = "Generate Data";
            this.btnGenerate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnItemListSave
            // 
            this.btnItemListSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnItemListSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnItemListSave.Image = global::Pricelist_Uploader.Properties.Resources.filesave;
            this.btnItemListSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnItemListSave.Location = new System.Drawing.Point(6, 455);
            this.btnItemListSave.Name = "btnItemListSave";
            this.btnItemListSave.Size = new System.Drawing.Size(101, 24);
            this.btnItemListSave.TabIndex = 204;
            this.btnItemListSave.Text = "Save";
            this.btnItemListSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnItemListSave.UseVisualStyleBackColor = true;
            this.btnItemListSave.Click += new System.EventHandler(this.btnItemListSave_Click);
            // 
            // dgvItemList
            // 
            this.dgvItemList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemList.Location = new System.Drawing.Point(6, 95);
            this.dgvItemList.Name = "dgvItemList";
            this.dgvItemList.Size = new System.Drawing.Size(1208, 355);
            this.dgvItemList.TabIndex = 203;
            this.dgvItemList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemList_CellEndEdit);
            this.dgvItemList.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvItemList_ColumnHeaderMouseClick);
            // 
            // lblStatus1
            // 
            this.lblStatus1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus1.Location = new System.Drawing.Point(2, 552);
            this.lblStatus1.Name = "lblStatus1";
            this.lblStatus1.Size = new System.Drawing.Size(1227, 13);
            this.lblStatus1.TabIndex = 198;
            this.lblStatus1.Text = "Status";
            // 
            // tssDataStatus
            // 
            this.tssDataStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tssDataStatus.Location = new System.Drawing.Point(2, 570);
            this.tssDataStatus.Name = "tssDataStatus";
            this.tssDataStatus.Size = new System.Drawing.Size(1227, 13);
            this.tssDataStatus.TabIndex = 197;
            this.tssDataStatus.Text = "Status";
            // 
            // pbDataProgress
            // 
            this.pbDataProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDataProgress.Location = new System.Drawing.Point(5, 586);
            this.pbDataProgress.Name = "pbDataProgress";
            this.pbDataProgress.Size = new System.Drawing.Size(1224, 17);
            this.pbDataProgress.TabIndex = 196;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 205;
            this.label5.Text = "Search";
            // 
            // txtItemSearch
            // 
            this.txtItemSearch.Location = new System.Drawing.Point(52, 67);
            this.txtItemSearch.Name = "txtItemSearch";
            this.txtItemSearch.Size = new System.Drawing.Size(390, 22);
            this.txtItemSearch.TabIndex = 206;
            this.txtItemSearch.TextChanged += new System.EventHandler(this.txtItemSearch_TextChanged);
            // 
            // ofdExcel
            // 
            this.ofdExcel.FileName = "ofdExcel";
            // 
            // frmMainPriceList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 606);
            this.Controls.Add(this.lblStatus1);
            this.Controls.Add(this.tssDataStatus);
            this.Controls.Add(this.pbDataProgress);
            this.Controls.Add(this.txtPriceList);
            this.Controls.Add(this.lblRefresh);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmMainPriceList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Price List";
            this.Load += new System.EventHandler(this.frmMainPriceList_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtPriceList;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.Label lblRefresh;
    private System.Windows.Forms.Label lblFolderBrws;
    private System.Windows.Forms.Button btnGenerate;
    private System.Windows.Forms.ComboBox cmbWorkSheet;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox txtExcelFile;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button btnItemGroupSave;
    private System.Windows.Forms.DataGridView dgvItemGroup;
    private System.Windows.Forms.Button btnItemListSave;
    private System.Windows.Forms.DataGridView dgvItemList;
    private System.Windows.Forms.Label lblStatus1;
    private System.Windows.Forms.Label tssDataStatus;
    private System.Windows.Forms.ProgressBar pbDataProgress;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtItemSearch;
    private System.Windows.Forms.OpenFileDialog ofdExcel;
}