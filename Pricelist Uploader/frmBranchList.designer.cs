partial class frmBranchList
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
            this.btnUpdate = new System.Windows.Forms.Button();
            this.dgvBranchList = new System.Windows.Forms.DataGridView();
            this.txtPriceList = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBranchSearch = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnUpdateItemGroup = new System.Windows.Forms.Button();
            this.dgvItemGroupList = new System.Windows.Forms.DataGridView();
            this.btnUpdateItem = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtItemSearch = new System.Windows.Forms.TextBox();
            this.dgvItemList = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.lblStatus1 = new System.Windows.Forms.Label();
            this.tssDataStatus = new System.Windows.Forms.Label();
            this.pbDataProgress = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBranchList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemGroupList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList)).BeginInit();
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
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdate.Location = new System.Drawing.Point(3, 613);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(133, 23);
            this.btnUpdate.TabIndex = 11;
            this.btnUpdate.Text = "Add";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // dgvBranchList
            // 
            this.dgvBranchList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBranchList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBranchList.Location = new System.Drawing.Point(0, 35);
            this.dgvBranchList.Name = "dgvBranchList";
            this.dgvBranchList.Size = new System.Drawing.Size(427, 575);
            this.dgvBranchList.TabIndex = 11;
            this.dgvBranchList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemGroup_CellClick);
            this.dgvBranchList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBranchList_CellContentClick);
            this.dgvBranchList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemGroup_CellEndEdit);
            this.dgvBranchList.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvBranchList_ColumnHeaderMouseClick);
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
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(4, 37);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.txtBranchSearch);
            this.splitContainer1.Panel1.Controls.Add(this.btnUpdate);
            this.splitContainer1.Panel1.Controls.Add(this.dgvBranchList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1342, 639);
            this.splitContainer1.SplitterDistance = 427;
            this.splitContainer1.TabIndex = 44;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Search";
            // 
            // txtBranchSearch
            // 
            this.txtBranchSearch.Location = new System.Drawing.Point(50, 7);
            this.txtBranchSearch.Name = "txtBranchSearch";
            this.txtBranchSearch.Size = new System.Drawing.Size(233, 22);
            this.txtBranchSearch.TabIndex = 17;
            this.txtBranchSearch.TextChanged += new System.EventHandler(this.txtBranchSearch_TextChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btnUpdateItemGroup);
            this.splitContainer2.Panel1.Controls.Add(this.dgvItemGroupList);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btnUpdateItem);
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Panel2.Controls.Add(this.txtItemSearch);
            this.splitContainer2.Panel2.Controls.Add(this.dgvItemList);
            this.splitContainer2.Size = new System.Drawing.Size(911, 639);
            this.splitContainer2.SplitterDistance = 443;
            this.splitContainer2.TabIndex = 0;
            // 
            // btnUpdateItemGroup
            // 
            this.btnUpdateItemGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdateItemGroup.Location = new System.Drawing.Point(3, 613);
            this.btnUpdateItemGroup.Name = "btnUpdateItemGroup";
            this.btnUpdateItemGroup.Size = new System.Drawing.Size(133, 23);
            this.btnUpdateItemGroup.TabIndex = 14;
            this.btnUpdateItemGroup.Text = "Update";
            this.btnUpdateItemGroup.UseVisualStyleBackColor = true;
            this.btnUpdateItemGroup.Click += new System.EventHandler(this.btnUpdateItemGroup_Click);
            // 
            // dgvItemGroupList
            // 
            this.dgvItemGroupList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItemGroupList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemGroupList.Location = new System.Drawing.Point(0, 35);
            this.dgvItemGroupList.Name = "dgvItemGroupList";
            this.dgvItemGroupList.Size = new System.Drawing.Size(443, 575);
            this.dgvItemGroupList.TabIndex = 13;
            this.dgvItemGroupList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemGroupList_CellClick);
            this.dgvItemGroupList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemGroupList_CellContentClick);
            this.dgvItemGroupList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemGroupList_CellEndEdit);
            // 
            // btnUpdateItem
            // 
            this.btnUpdateItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdateItem.Location = new System.Drawing.Point(3, 613);
            this.btnUpdateItem.Name = "btnUpdateItem";
            this.btnUpdateItem.Size = new System.Drawing.Size(133, 23);
            this.btnUpdateItem.TabIndex = 16;
            this.btnUpdateItem.Text = "Update";
            this.btnUpdateItem.UseVisualStyleBackColor = true;
            this.btnUpdateItem.Click += new System.EventHandler(this.btnUpdateItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Search";
            // 
            // txtItemSearch
            // 
            this.txtItemSearch.Location = new System.Drawing.Point(50, 7);
            this.txtItemSearch.Name = "txtItemSearch";
            this.txtItemSearch.Size = new System.Drawing.Size(233, 22);
            this.txtItemSearch.TabIndex = 15;
            this.txtItemSearch.TextChanged += new System.EventHandler(this.txtItemSearch_TextChanged_1);
            // 
            // dgvItemList
            // 
            this.dgvItemList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemList.Location = new System.Drawing.Point(0, 35);
            this.dgvItemList.Name = "dgvItemList";
            this.dgvItemList.Size = new System.Drawing.Size(464, 575);
            this.dgvItemList.TabIndex = 13;
            this.dgvItemList.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvItemList_ColumnHeaderMouseClick_1);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1200, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 23);
            this.button1.TabIndex = 45;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblStatus1
            // 
            this.lblStatus1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus1.AutoSize = true;
            this.lblStatus1.Location = new System.Drawing.Point(2, 679);
            this.lblStatus1.Name = "lblStatus1";
            this.lblStatus1.Size = new System.Drawing.Size(39, 13);
            this.lblStatus1.TabIndex = 69;
            this.lblStatus1.Text = "Status";
            // 
            // tssDataStatus
            // 
            this.tssDataStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tssDataStatus.AutoSize = true;
            this.tssDataStatus.Location = new System.Drawing.Point(2, 699);
            this.tssDataStatus.Name = "tssDataStatus";
            this.tssDataStatus.Size = new System.Drawing.Size(39, 13);
            this.tssDataStatus.TabIndex = 68;
            this.tssDataStatus.Text = "Status";
            // 
            // pbDataProgress
            // 
            this.pbDataProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDataProgress.Location = new System.Drawing.Point(5, 715);
            this.pbDataProgress.Name = "pbDataProgress";
            this.pbDataProgress.Size = new System.Drawing.Size(1341, 17);
            this.pbDataProgress.TabIndex = 67;
            // 
            // frmBranchList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 737);
            this.Controls.Add(this.lblStatus1);
            this.Controls.Add(this.tssDataStatus);
            this.Controls.Add(this.pbDataProgress);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.txtPriceList);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmBranchList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Price List Creation";
            this.Load += new System.EventHandler(this.frmBranchList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBranchList)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemGroupList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnUpdate;
    private System.Windows.Forms.DataGridView dgvBranchList;
    private System.Windows.Forms.TextBox txtPriceList;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.DataGridView dgvItemGroupList;
    private System.Windows.Forms.DataGridView dgvItemList;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtBranchSearch;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtItemSearch;
    private System.Windows.Forms.Button btnUpdateItemGroup;
    private System.Windows.Forms.Button btnUpdateItem;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Label lblStatus1;
    private System.Windows.Forms.Label tssDataStatus;
    private System.Windows.Forms.ProgressBar pbDataProgress;
}