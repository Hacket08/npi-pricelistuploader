using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Configuration;

public partial class frmLogin : Form
{
    public frmLogin()
    {
        InitializeComponent();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void frmLogin_Load(object sender, EventArgs e)
    {
        

        //string sysDBServer = ConfigurationManager.AppSettings["sysDBServer"];
        //string sysDftDBCompany = ConfigurationManager.AppSettings["sysDftDBCompany"];
        //string sysDBUsername = ConfigurationManager.AppSettings["sysDBUsername"];
        //string sysDBPassword = ConfigurationManager.AppSettings["sysDBPassword"];
        
        //clsSQLClientFunctions.SAPConnection = clsSQLClientFunctions.GlobalConnectionString(sysDBServer, sysDftDBCompany, sysDBUsername, sysDBPassword);

        if(clsSQLClientFunctions.CheckConnection(clsDeclaration.sSAPConnection) == false )
        {
            frmSystemConfig frmSystemConfig = new frmSystemConfig();
            frmSystemConfig.ShowDialog();
        }


        if (clsSQLClientFunctions.CheckConnection(clsDeclaration.sSystemConnection) == false)
        {
            frmSystemConfig frmSystemConfig = new frmSystemConfig();
            frmSystemConfig.ShowDialog();
        }



        //DataTable dtTable;
        //string sqlSelect;

        //sqlSelect = "SELECT Z.Code, Z.Name FROM dbo.[@BRANCHL] Z WHERE Z.Code COLLATE Latin1_General_CI_AS IN (SELECT A.NAME FROM sys.databases A WHERE LEFT(A.name,4) IN ('DESI','DESM'))";
        //dtTable = clsSQLClientFunctions.DataList(clsSQLClientFunctions.SAPConnection, sqlSelect);

        //foreach (DataRow row in dtTable.Rows)
        //{
        //    string _Area = row["Code"].ToString();
        //    cmbCompany.Items.Add(_Area);
        //}


        cmbCompany.Text = ConfigurationManager.AppSettings["sysDftDBCompany"];


    





    //txtDBServerName.Text = ConfigurationManager.AppSettings["DBServer"];
    //txtDBName.Text = ConfigurationManager.AppSettings["DBName"];
    //txtDBUsername.Text = ConfigurationManager.AppSettings["DBUsername"];
    //txtDBPassword.Text = ConfigurationManager.AppSettings["DBPassword"];
    }

private void  sysInitialize()
    {

    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        clsDeclaration.sDatabaseName = cmbCompany.Text;
        clsDeclaration.sSAPUsername = txtSAPUsername.Text;
        clsDeclaration.sSAPPassword = txtSAPPassword.Text;
        
        string sysDftDBCompany = cmbCompany.Text;
        string sysSAPUsername = txtSAPUsername.Text;
        string sysSAPPassword = txtSAPPassword.Text;

        bool isConnected = false;
        string strErrMsg = "";
        clsSAPFunctions.oCompany = clsSAPFunctions.SAPInitializeConnection(sysDftDBCompany, sysSAPUsername, sysSAPPassword,out isConnected,out strErrMsg);
        MessageBox.Show(strErrMsg);

        //clsSAPFunctions.oToCompany = clsSAPFunctions.SAPInitializeConnection("DESIHOFC", sysSAPUsername, sysSAPPassword, out isConnected, out strErrMsg);
        //MessageBox.Show(strErrMsg);

        if (isConnected == false)
        {
            frmSystemConfig frmSystemConfig = new frmSystemConfig();
            frmSystemConfig.ShowDialog();
            return;
        }
        else
        {
            //clsSAPFunctions.oCompany.Disconnect();
        }

        //if (clsSAPFunctions.SAPDIConnection(sysDftDBCompany, sysSAPUsername, sysSAPPassword) == false)
        //{
        //    frmSystemConfig frmSystemConfig = new frmSystemConfig();
        //    frmSystemConfig.ShowDialog();
        //    return;
        //}
        //else
        //{
        //    clsSAPFunctions.oCompany.Disconnect();
        //}

        Close();
    }
}