using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmBrowse : Form
{
    static int _HeaderColumn;

    public static string _ObjTable;
    public static string _ObjName;
    public static string _sqlQuery;


    public static string _Code = "";
    public static string _Name = "";
    public static string _Area = "";

    public frmBrowse()
    {
        InitializeComponent();
    }

    private void frmBrowse_Load(object sender, EventArgs e)
    {
        this.Text = _ObjName;
        _DataDisplay(_sqlQuery);
    }

    private void _DataDisplay(string _sqlData)
    {
        DataTable _tblData = new DataTable();
        _tblData = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlData);
        clsFunctions.DataGridViewSetup(dgvDisplay, _tblData);
    }

    private void dgvDisplay_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            switch(_ObjTable)
            {
                case "OWHS":
                    _Code = dgvDisplay.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                    _Name = dgvDisplay.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
                    _Area = dgvDisplay.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
                    break;


            }
        }
        catch
        {

        }
    }

    private void dgvDisplay_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        try
        {
            _HeaderColumn = e.ColumnIndex;
        }
        catch
        {
        }
    }

    private void txtSearch_TextChanged(object sender, EventArgs e)
    {
        String someText;
        someText = txtSearch.Text;

        int gridRow = 0;
        int gridColumn = _HeaderColumn;

        dgvDisplay.ClearSelection();
        dgvDisplay.CurrentCell = null;

        foreach (DataGridViewRow row in dgvDisplay.Rows)
        {
            //cboPayrolPeriod.Items.Add(row[0].ToString());

            DataGridViewCell _cell = dgvDisplay.Rows[gridRow].Cells[gridColumn];
            if (_cell.Value.ToString().ToLower().Contains(someText.ToLower()) == true)
            {
                dgvDisplay.Rows[gridRow].Selected = true;
                dgvDisplay.FirstDisplayedScrollingRowIndex = gridRow;

                switch (_ObjTable)
                {
                    case "OWHS":
                        _Code = dgvDisplay.Rows[gridRow].Cells[0].Value.ToString().Trim();
                        _Name = dgvDisplay.Rows[gridRow].Cells[1].Value.ToString().Trim();
                        _Area = dgvDisplay.Rows[gridRow].Cells[2].Value.ToString().Trim();
                        break;
                }

                return;
            }
            gridRow++;
        }
    }

    private void btnChoose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void dgvDisplay_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        dgvDisplay_CellClick(sender, e);
        Close();
    }
}