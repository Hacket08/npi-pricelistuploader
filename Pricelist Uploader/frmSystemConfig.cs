using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Configuration;

public partial class frmSystemConfig : Form
{
    public frmSystemConfig()
    {
        InitializeComponent();

        //txtDBServerName.Text = ConfigurationManager.AppSettings["DBServer"];
        //txtDBName.Text = ConfigurationManager.AppSettings["DBName"];
        //txtDBUsername.Text = ConfigurationManager.AppSettings["DBUsername"];
        //txtDBPassword.Text = ConfigurationManager.AppSettings["DBPassword"];

        txtLicenseServer.Text = ConfigurationManager.AppSettings["sysLicenseServer"];

        txtSAPServer.Text = ConfigurationManager.AppSettings["sysDBServer"];
        txtSAPDatabase.Text = ConfigurationManager.AppSettings["sysDftDBCompany"];
        txtSAPUsername.Text = ConfigurationManager.AppSettings["sysDBUsername"];
        txtSAPPassword.Text = ConfigurationManager.AppSettings["sysDBPassword"];


        txtDefaultUsername.Text = ConfigurationManager.AppSettings["sysSAPUsername"];
        txtDefaultPassword.Text = ConfigurationManager.AppSettings["sysSAPPassword"];



        txtSysServer.Text = ConfigurationManager.AppSettings["DBServer"];
        txtSysUsername.Text = ConfigurationManager.AppSettings["DBUsername"];
        txtSysPassword.Text = ConfigurationManager.AppSettings["DBPassword"];
        txtSysDatabase.Text = ConfigurationManager.AppSettings["DBName"];

    }

    private void frmSystemConfig_Load(object sender, EventArgs e)
    {

    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        //clsFunctions.SetSetting("DBServer", txtDBServerName.Text);
        //clsFunctions.SetSetting("DBName", txtDBName.Text);
        //clsFunctions.SetSetting("DBUsername", txtDBUsername.Text);
        //clsFunctions.SetSetting("DBPassword", txtDBPassword.Text);


        clsFunctions.SetSetting("sysLicenseServer", txtLicenseServer.Text);

        clsFunctions.SetSetting("sysDBServer", txtSAPServer.Text);
        clsFunctions.SetSetting("sysDftDBCompany", txtSAPDatabase.Text);
        clsFunctions.SetSetting("sysDBUsername", txtSAPUsername.Text);
        clsFunctions.SetSetting("sysDBPassword", txtSAPPassword.Text);

        clsFunctions.SetSetting("sysSAPUsername", txtDefaultUsername.Text);
        clsFunctions.SetSetting("sysSAPPassword", txtDefaultPassword.Text);


        clsFunctions.SetSetting("DBServer", txtSysServer.Text);
        clsFunctions.SetSetting("DBUsername", txtSysUsername.Text);
        clsFunctions.SetSetting("DBPassword", txtSysPassword.Text);
        clsFunctions.SetSetting("DBName", txtSysDatabase.Text);
        

        MessageBox.Show("New Settings Applied!", "Connection Settings", MessageBoxButtons.OK, MessageBoxIcon.None);
        Application.Restart();
    }
}