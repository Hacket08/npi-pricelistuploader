using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmPriceList : Form
{
    public frmPriceList()
    {
        InitializeComponent();
    }

    private void frmPriceList_Load(object sender, EventArgs e)
    {

    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void lblBrowse_Click(object sender, EventArgs e)
    {
        frmBrowse frmBrowse = new frmBrowse();

        frmBrowse._ObjTable = "OITM";
        frmBrowse._ObjName = "Item List";


        frmBrowse._sqlQuery = @"SELECT A.ItemCode, A.ItemName FROM OITM A";

        frmBrowse.ShowDialog();
        txtCode.Text = frmBrowse._Code;
        txtName.Text = frmBrowse._Name;

        frmBrowse._Code = "";
        frmBrowse._Name = "";
    }
}