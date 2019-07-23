partial class frmItemList
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
            this.btnUpdateItemGroup = new System.Windows.Forms.Button();
            this.dgvItemGroup = new System.Windows.Forms.DataGridView();
            this.txtPriceList = new System.Windows.Forms.TextBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.btnUpdateVisMin = new System.Windows.Forms.Button();
            this.dgvItemList_VISMIN = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.txtItemSearch_VISMIN = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnUpdateGMA = new System.Windows.Forms.Button();
            this.dgvItemList_GMA = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.txtItemSearch_GMA = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnCreate = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tssDataStatus = new System.Windows.Forms.Label();
            this.pbDataProgress = new System.Windows.Forms.ProgressBar();
            this.lblStatus1 = new System.Windows.Forms.Label();
            this.btnExcelUpload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemGroup)).BeginInit();
            this.tabControl2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList_VISMIN)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList_GMA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Default Price List";
            // 
            // btnUpdateItemGroup
            // 
            this.btnUpdateItemGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateItemGroup.Location = new System.Drawing.Point(239, 408);
            this.btnUpdateItemGroup.Name = "btnUpdateItemGroup";
            this.btnUpdateItemGroup.Size = new System.Drawing.Size(133, 23);
            this.btnUpdateItemGroup.TabIndex = 11;
            this.btnUpdateItemGroup.Text = "Update";
            this.btnUpdateItemGroup.UseVisualStyleBackColor = true;
            this.btnUpdateItemGroup.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // dgvItemGroup
            // 
            this.dgvItemGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItemGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemGroup.Location = new System.Drawing.Point(0, 0);
            this.dgvItemGroup.Name = "dgvItemGroup";
            this.dgvItemGroup.Size = new System.Drawing.Size(375, 405);
            this.dgvItemGroup.TabIndex = 11;
            this.dgvItemGroup.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemGroup_CellClick);
            this.dgvItemGroup.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemGroup_CellEndEdit);
            // 
            // txtPriceList
            // 
            this.txtPriceList.Location = new System.Drawing.Point(104, 11);
            this.txtPriceList.Name = "txtPriceList";
            this.txtPriceList.Size = new System.Drawing.Size(393, 22);
            this.txtPriceList.TabIndex = 42;
            this.txtPriceList.TextChanged += new System.EventHandler(this.txtPriceList_TextChanged);
            this.txtPriceList.Leave += new System.EventHandler(this.txtPriceList_Leave);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(777, 435);
            this.tabControl2.TabIndex = 43;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.btnUpdateVisMin);
            this.tabPage5.Controls.Add(this.dgvItemList_VISMIN);
            this.tabPage5.Controls.Add(this.label2);
            this.tabPage5.Controls.Add(this.txtItemSearch_VISMIN);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(769, 409);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "VISMIN";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // btnUpdateVisMin
            // 
            this.btnUpdateVisMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateVisMin.Location = new System.Drawing.Point(630, 383);
            this.btnUpdateVisMin.Name = "btnUpdateVisMin";
            this.btnUpdateVisMin.Size = new System.Drawing.Size(133, 23);
            this.btnUpdateVisMin.TabIndex = 44;
            this.btnUpdateVisMin.Text = "Update";
            this.btnUpdateVisMin.UseVisualStyleBackColor = true;
            this.btnUpdateVisMin.Click += new System.EventHandler(this.btnUpdateVisMin_Click);
            // 
            // dgvItemList_VISMIN
            // 
            this.dgvItemList_VISMIN.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItemList_VISMIN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemList_VISMIN.Location = new System.Drawing.Point(3, 39);
            this.dgvItemList_VISMIN.Name = "dgvItemList_VISMIN";
            this.dgvItemList_VISMIN.Size = new System.Drawing.Size(760, 342);
            this.dgvItemList_VISMIN.TabIndex = 12;
            this.dgvItemList_VISMIN.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemList_VISMIN_CellDoubleClick);
            this.dgvItemList_VISMIN.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemList_VISMIN_CellEndEdit);
            this.dgvItemList_VISMIN.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvItemList_ColumnHeaderMouseClick);
            this.dgvItemList_VISMIN.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvItemList_ColumnHeaderMouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Search";
            // 
            // txtItemSearch_VISMIN
            // 
            this.txtItemSearch_VISMIN.Location = new System.Drawing.Point(53, 11);
            this.txtItemSearch_VISMIN.Name = "txtItemSearch_VISMIN";
            this.txtItemSearch_VISMIN.Size = new System.Drawing.Size(390, 22);
            this.txtItemSearch_VISMIN.TabIndex = 9;
            this.txtItemSearch_VISMIN.TextChanged += new System.EventHandler(this.txtItemSearch_VISMIN_TextChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnUpdateGMA);
            this.tabPage4.Controls.Add(this.dgvItemList_GMA);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.txtItemSearch_GMA);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(769, 409);
            this.tabPage4.TabIndex = 2;
            this.tabPage4.Text = "GMA";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnUpdateGMA
            // 
            this.btnUpdateGMA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateGMA.Location = new System.Drawing.Point(472, 383);
            this.btnUpdateGMA.Name = "btnUpdateGMA";
            this.btnUpdateGMA.Size = new System.Drawing.Size(133, 23);
            this.btnUpdateGMA.TabIndex = 45;
            this.btnUpdateGMA.Text = "Update";
            this.btnUpdateGMA.UseVisualStyleBackColor = true;
            this.btnUpdateGMA.Click += new System.EventHandler(this.btnUpdateGMA_Click);
            // 
            // dgvItemList_GMA
            // 
            this.dgvItemList_GMA.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItemList_GMA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemList_GMA.Location = new System.Drawing.Point(3, 39);
            this.dgvItemList_GMA.Name = "dgvItemList_GMA";
            this.dgvItemList_GMA.Size = new System.Drawing.Size(602, 342);
            this.dgvItemList_GMA.TabIndex = 12;
            this.dgvItemList_GMA.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemList_GMA_CellEndEdit);
            this.dgvItemList_GMA.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvItemList_ColumnHeaderMouseClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Search";
            // 
            // txtItemSearch_GMA
            // 
            this.txtItemSearch_GMA.Location = new System.Drawing.Point(53, 11);
            this.txtItemSearch_GMA.Name = "txtItemSearch_GMA";
            this.txtItemSearch_GMA.Size = new System.Drawing.Size(390, 22);
            this.txtItemSearch_GMA.TabIndex = 9;
            this.txtItemSearch_GMA.TextChanged += new System.EventHandler(this.txtItemSearch_GMA_TextChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(9, 38);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnCreate);
            this.splitContainer1.Panel1.Controls.Add(this.dgvItemGroup);
            this.splitContainer1.Panel1.Controls.Add(this.btnUpdateItemGroup);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl2);
            this.splitContainer1.Size = new System.Drawing.Size(1156, 435);
            this.splitContainer1.SplitterDistance = 375;
            this.splitContainer1.TabIndex = 44;
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreate.Location = new System.Drawing.Point(3, 408);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(133, 23);
            this.btnCreate.TabIndex = 12;
            this.btnCreate.Text = "Create Price List";
            this.btnCreate.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1032, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 23);
            this.button1.TabIndex = 45;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tssDataStatus
            // 
            this.tssDataStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tssDataStatus.AutoSize = true;
            this.tssDataStatus.Location = new System.Drawing.Point(6, 502);
            this.tssDataStatus.Name = "tssDataStatus";
            this.tssDataStatus.Size = new System.Drawing.Size(39, 13);
            this.tssDataStatus.TabIndex = 65;
            this.tssDataStatus.Text = "Status";
            // 
            // pbDataProgress
            // 
            this.pbDataProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDataProgress.Location = new System.Drawing.Point(9, 518);
            this.pbDataProgress.Name = "pbDataProgress";
            this.pbDataProgress.Size = new System.Drawing.Size(1156, 17);
            this.pbDataProgress.TabIndex = 64;
            // 
            // lblStatus1
            // 
            this.lblStatus1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus1.AutoSize = true;
            this.lblStatus1.Location = new System.Drawing.Point(6, 482);
            this.lblStatus1.Name = "lblStatus1";
            this.lblStatus1.Size = new System.Drawing.Size(39, 13);
            this.lblStatus1.TabIndex = 66;
            this.lblStatus1.Text = "Status";
            // 
            // btnExcelUpload
            // 
            this.btnExcelUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcelUpload.Location = new System.Drawing.Point(1000, 472);
            this.btnExcelUpload.Name = "btnExcelUpload";
            this.btnExcelUpload.Size = new System.Drawing.Size(165, 23);
            this.btnExcelUpload.TabIndex = 67;
            this.btnExcelUpload.Text = "Upload Excel";
            this.btnExcelUpload.UseVisualStyleBackColor = true;
            this.btnExcelUpload.Click += new System.EventHandler(this.btnExcelUpload_Click);
            // 
            // frmItemList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1168, 541);
            this.Controls.Add(this.btnExcelUpload);
            this.Controls.Add(this.lblStatus1);
            this.Controls.Add(this.tssDataStatus);
            this.Controls.Add(this.pbDataProgress);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.txtPriceList);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmItemList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Price List Creation";
            this.Load += new System.EventHandler(this.frmItemList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemGroup)).EndInit();
            this.tabControl2.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList_VISMIN)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList_GMA)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnUpdateItemGroup;
    private System.Windows.Forms.DataGridView dgvItemGroup;
    private System.Windows.Forms.TextBox txtPriceList;
    private System.Windows.Forms.TabControl tabControl2;
    private System.Windows.Forms.TabPage tabPage5;
    private System.Windows.Forms.DataGridView dgvItemList_VISMIN;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtItemSearch_VISMIN;
    private System.Windows.Forms.TabPage tabPage4;
    private System.Windows.Forms.DataGridView dgvItemList_GMA;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtItemSearch_GMA;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.Button btnUpdateVisMin;
    private System.Windows.Forms.Button btnUpdateGMA;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Label tssDataStatus;
    private System.Windows.Forms.ProgressBar pbDataProgress;
    private System.Windows.Forms.Label lblStatus1;
    private System.Windows.Forms.Button btnCreate;
    private System.Windows.Forms.Button btnExcelUpload;
}