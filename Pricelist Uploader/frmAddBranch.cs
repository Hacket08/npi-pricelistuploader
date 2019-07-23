using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmAddBranch : Form
{
    public static string _PriceList;
    public frmAddBranch()
    {
        InitializeComponent();
    }

    private void frmAddBranch_Load(object sender, EventArgs e)
    {

    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void lblBrowse_Click(object sender, EventArgs e)
    {
        frmBrowse frmBrowse = new frmBrowse();

        frmBrowse._ObjTable = "OWHS";
        frmBrowse._ObjName = "Branch List";


        frmBrowse._sqlQuery = @" SELECT A.WhsCode AS Code, A.WhsName AS Name,B.Location AS Area
                                        FROM OWHS A 
                                        LEFT OUTER JOIN OLCT B ON A.Location = B.Code
WHERE A.WhsCode NOT IN (SELECT DISTINCT Z.Code FROM [@OBPL] Z)";

        frmBrowse.ShowDialog();
        txtCode.Text = frmBrowse._Code;
        txtName.Text = frmBrowse._Name;
        txtArea.Text = frmBrowse._Area;

        frmBrowse._Code = "";
        frmBrowse._Name = "";
        frmBrowse._Area = "";
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        clsSAPFunctions.CreateBranchPriceList(txtCode.Text, txtName.Text, txtArea.Text, _PriceList);
        MessageBox.Show("Branch Successfully Added");
        Close();
    }
}