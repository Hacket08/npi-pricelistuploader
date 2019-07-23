partial class frmSystemConfig
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
            this.label12 = new System.Windows.Forms.Label();
            this.txtLicenseServer = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDefaultPassword = new System.Windows.Forms.TextBox();
            this.txtDefaultUsername = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSAPDatabase = new System.Windows.Forms.TextBox();
            this.txtSAPServer = new System.Windows.Forms.TextBox();
            this.txtSAPPassword = new System.Windows.Forms.TextBox();
            this.txtSAPUsername = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSysServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtSysUsername = new System.Windows.Forms.TextBox();
            this.txtSysDatabase = new System.Windows.Forms.TextBox();
            this.txtSysPassword = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 13);
            this.label12.TabIndex = 21;
            this.label12.Text = "SAP License Server";
            // 
            // txtLicenseServer
            // 
            this.txtLicenseServer.Location = new System.Drawing.Point(136, 7);
            this.txtLicenseServer.Name = "txtLicenseServer";
            this.txtLicenseServer.Size = new System.Drawing.Size(249, 22);
            this.txtLicenseServer.TabIndex = 22;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 169);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Default User";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 197);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(97, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "Default Password";
            // 
            // txtDefaultPassword
            // 
            this.txtDefaultPassword.Font = new System.Drawing.Font("Wingdings", 9.75F);
            this.txtDefaultPassword.Location = new System.Drawing.Point(136, 194);
            this.txtDefaultPassword.Name = "txtDefaultPassword";
            this.txtDefaultPassword.PasswordChar = 'l';
            this.txtDefaultPassword.Size = new System.Drawing.Size(249, 22);
            this.txtDefaultPassword.TabIndex = 20;
            // 
            // txtDefaultUsername
            // 
            this.txtDefaultUsername.Location = new System.Drawing.Point(136, 166);
            this.txtDefaultUsername.Name = "txtDefaultUsername";
            this.txtDefaultUsername.Size = new System.Drawing.Size(249, 22);
            this.txtDefaultUsername.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "SAP Server IP Address";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "SAP DB Username";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 94);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "SAP DB Password";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 122);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "SAP Database Name";
            // 
            // txtSAPDatabase
            // 
            this.txtSAPDatabase.Location = new System.Drawing.Point(136, 119);
            this.txtSAPDatabase.Name = "txtSAPDatabase";
            this.txtSAPDatabase.Size = new System.Drawing.Size(249, 22);
            this.txtSAPDatabase.TabIndex = 16;
            // 
            // txtSAPServer
            // 
            this.txtSAPServer.Location = new System.Drawing.Point(136, 35);
            this.txtSAPServer.Name = "txtSAPServer";
            this.txtSAPServer.Size = new System.Drawing.Size(249, 22);
            this.txtSAPServer.TabIndex = 13;
            // 
            // txtSAPPassword
            // 
            this.txtSAPPassword.Font = new System.Drawing.Font("Wingdings", 9.75F);
            this.txtSAPPassword.Location = new System.Drawing.Point(136, 91);
            this.txtSAPPassword.Name = "txtSAPPassword";
            this.txtSAPPassword.PasswordChar = 'l';
            this.txtSAPPassword.Size = new System.Drawing.Size(249, 22);
            this.txtSAPPassword.TabIndex = 15;
            // 
            // txtSAPUsername
            // 
            this.txtSAPUsername.Location = new System.Drawing.Point(136, 63);
            this.txtSAPUsername.Name = "txtSAPUsername";
            this.txtSAPUsername.Size = new System.Drawing.Size(249, 22);
            this.txtSAPUsername.TabIndex = 14;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(11, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(399, 310);
            this.tabControl1.TabIndex = 16;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.txtLicenseServer);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.txtDefaultPassword);
            this.tabPage2.Controls.Add(this.txtDefaultUsername);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.txtSAPDatabase);
            this.tabPage2.Controls.Add(this.txtSAPServer);
            this.tabPage2.Controls.Add(this.txtSAPPassword);
            this.tabPage2.Controls.Add(this.txtSAPUsername);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(391, 284);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "SAP Config";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(130, 329);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(113, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BackColor = System.Drawing.Color.Lime;
            this.label5.Location = new System.Drawing.Point(-5, -8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(428, 13);
            this.label5.TabIndex = 14;
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(11, 329);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(113, 23);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.txtSysServer);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Controls.Add(this.txtSysUsername);
            this.tabPage3.Controls.Add(this.txtSysDatabase);
            this.tabPage3.Controls.Add(this.txtSysPassword);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(391, 284);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "System Config";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Server IP Address";
            // 
            // txtSysServer
            // 
            this.txtSysServer.Location = new System.Drawing.Point(136, 7);
            this.txtSysServer.Name = "txtSysServer";
            this.txtSysServer.Size = new System.Drawing.Size(249, 22);
            this.txtSysServer.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Username";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(11, 66);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 13);
            this.label13.TabIndex = 10;
            this.label13.Text = "Password";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(11, 94);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(55, 13);
            this.label14.TabIndex = 11;
            this.label14.Text = "Database";
            // 
            // txtSysUsername
            // 
            this.txtSysUsername.Location = new System.Drawing.Point(136, 35);
            this.txtSysUsername.Name = "txtSysUsername";
            this.txtSysUsername.Size = new System.Drawing.Size(249, 22);
            this.txtSysUsername.TabIndex = 13;
            // 
            // txtSysDatabase
            // 
            this.txtSysDatabase.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.txtSysDatabase.Location = new System.Drawing.Point(136, 91);
            this.txtSysDatabase.Name = "txtSysDatabase";
            this.txtSysDatabase.Size = new System.Drawing.Size(249, 22);
            this.txtSysDatabase.TabIndex = 15;
            // 
            // txtSysPassword
            // 
            this.txtSysPassword.Font = new System.Drawing.Font("Wingdings", 9.75F);
            this.txtSysPassword.Location = new System.Drawing.Point(136, 63);
            this.txtSysPassword.Name = "txtSysPassword";
            this.txtSysPassword.PasswordChar = 'l';
            this.txtSysPassword.Size = new System.Drawing.Size(249, 22);
            this.txtSysPassword.TabIndex = 14;
            // 
            // frmSystemConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 363);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSave);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmSystemConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "System Config";
            this.Load += new System.EventHandler(this.frmSystemConfig_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.TextBox txtLicenseServer;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.TextBox txtDefaultPassword;
    private System.Windows.Forms.TextBox txtDefaultUsername;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox txtSAPDatabase;
    private System.Windows.Forms.TextBox txtSAPServer;
    private System.Windows.Forms.TextBox txtSAPPassword;
    private System.Windows.Forms.TextBox txtSAPUsername;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtSysServer;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.TextBox txtSysUsername;
    private System.Windows.Forms.TextBox txtSysDatabase;
    private System.Windows.Forms.TextBox txtSysPassword;
}