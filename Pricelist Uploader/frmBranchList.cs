using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmBranchList : Form
{
    private enum enumYesNo
    { Y, N }

    DataTable _dtBranchList = new DataTable();
    DataTable _dtItemGroupList = new DataTable();
    DataTable _dtItemList = new DataTable();

    static int _HeaderColumn;
    static int _HeaderItemColumn;
    static string _BranchCode;
    public frmBranchList()
    {
        InitializeComponent();
    }


    public static AutoCompleteStringCollection LoadAutoComplete(DataTable _table, int RowId)
    {
        DataTable dt = _table; //suppose this method returns a DataTable with fetched records from database.
        AutoCompleteStringCollection stringCol = new AutoCompleteStringCollection();
        foreach (DataRow row in dt.Rows)
        {
            stringCol.Add(Convert.ToString(row[RowId]));
        }
        return stringCol; //return the string collection with added records
    }

    private void frmBranchList_Load(object sender, EventArgs e)
    {
        string _sqlPriceList = @"SELECT A.Code FROM [@OCRP] A";
        DataTable _tblPriceList = new DataTable();
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);

        txtPriceList.AutoCompleteCustomSource = LoadAutoComplete(_tblPriceList, 0);
        txtPriceList.AutoCompleteMode = AutoCompleteMode.Suggest;
        txtPriceList.AutoCompleteSource = AutoCompleteSource.CustomSource;



        // create DataSource as list of Name-Value pairs from enum
        var cboSrc = Enum.GetNames(typeof(enumYesNo)).
                            Select(x => new
                            {
                                Name = x,
                                Value = (int)Enum.Parse(typeof(enumYesNo), x)
                            }
                                   ).ToList();



        // Branch List Table
        _dtBranchList.Columns.Add("Code", typeof(string));
        _dtBranchList.Columns.Add("Name", typeof(string));
        _dtBranchList.Columns.Add("Area", typeof(string));
        _dtBranchList.Columns.Add("Freeze", typeof(enumYesNo));

        dgvBranchList.DataSource = _dtBranchList;


        DataGridViewComboBoxColumn cb = new DataGridViewComboBoxColumn();
        cb.ValueType = typeof(enumYesNo);
        cb.DataSource = cboSrc;
        cb.DisplayMember = "Name";          // important
        cb.ValueMember = "Value";           // important
        cb.HeaderText = "Freeze";
        cb.DataPropertyName = "Freeze";  // where to store the value

        dgvBranchList.Columns.Remove(dgvBranchList.Columns[3]);  // remove txt col
        dgvBranchList.Columns.Add(cb);
        cb.DisplayIndex = 3;

        clsFunctions.DataGridViewSetup(dgvBranchList, _dtBranchList);



        // Item Group Table

        _dtItemGroupList.Columns.Add("Code", typeof(string));
        _dtItemGroupList.Columns.Add("Name", typeof(string));
        _dtItemGroupList.Columns.Add("MainDisc", typeof(double));
        _dtItemGroupList.Columns.Add("Disc", typeof(double));
        _dtItemGroupList.Columns.Add("Freeze", typeof(enumYesNo));

        dgvItemGroupList.DataSource = _dtItemGroupList;


        DataGridViewComboBoxColumn cbItemGroupList = new DataGridViewComboBoxColumn();
        cbItemGroupList.ValueType = typeof(enumYesNo);
        cbItemGroupList.DataSource = cboSrc;
        cbItemGroupList.DisplayMember = "Name";          // important
        cbItemGroupList.ValueMember = "Value";           // important
        cbItemGroupList.HeaderText = "Freeze";
        cbItemGroupList.DataPropertyName = "Freeze";  // where to store the value

        dgvItemGroupList.Columns.Remove(dgvItemGroupList.Columns[4]);  // remove txt col
        dgvItemGroupList.Columns.Add(cbItemGroupList);
        cbItemGroupList.DisplayIndex = 4;

        clsFunctions.DataGridViewSetup(dgvItemGroupList, _dtItemGroupList);









        // Item Master Data Table

        _dtItemList.Columns.Add("Code", typeof(string));
        _dtItemList.Columns.Add("Brand", typeof(string));
        _dtItemList.Columns.Add("Name", typeof(string));
        _dtItemList.Columns.Add("Price", typeof(double));
        _dtItemList.Columns.Add("Disc", typeof(double));
        _dtItemList.Columns.Add("Net Amount", typeof(double));
        _dtItemList.Columns.Add("New Net Amount", typeof(double));
        _dtItemList.Columns.Add("Freeze", typeof(enumYesNo));

        dgvItemList.DataSource = _dtItemList;


        DataGridViewComboBoxColumn cbItemList = new DataGridViewComboBoxColumn();
        cbItemList.ValueType = typeof(enumYesNo);
        cbItemList.DataSource = cboSrc;
        cbItemList.DisplayMember = "Name";          // important
        cbItemList.ValueMember = "Value";           // important
        cbItemList.HeaderText = "Freeze";
        cbItemList.DataPropertyName = "Freeze";  // where to store the value

        dgvItemList.Columns.Remove(dgvItemList.Columns[7]);  // remove txt col
        dgvItemList.Columns.Add(cbItemList);
        cbItemList.DisplayIndex = 7;

        clsFunctions.DataGridViewSetup(dgvItemList, _dtItemList);




        //cmbPriceList.Items.Clear();
        //foreach (DataRow row in _tblPriceList.Rows)
        //{
        //    string _Code = row[0].ToString();
        //    cmbPriceList.Items.Add(_Code);
        //}
    }

    private void cmbPriceList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string _sqlPriceList = @"SELECT A.Code FROM [@OCRP] A";
        DataTable _tblPriceList = new DataTable();
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);
    }

    private void lblAdd_Click(object sender, EventArgs e)
    {
        //frmPriceList frmPriceList = new frmPriceList();
        //frmPriceList.ShowDialog();
    }

    private void txtPriceList_TextChanged(object sender, EventArgs e)
    {
        
    }

    private void txtPriceList_Leave(object sender, EventArgs e)
    {





        //DataTable dtStruct = new DataTable();
        //dtStruct.Columns.Add("Name", typeof(string));
        //dtStruct.Columns.Add("Type", typeof(string));
        //dtStruct.Columns.Add("Structure", typeof(enumYesNo));
        //dtStruct.Columns.Add("Count", typeof(int));

        //// autogen columns == true
        //dgvItemGroup.DataSource = dtStruct;

        //// create DataSource as list of Name-Value pairs from enum
        //var cboSrc = Enum.GetNames(typeof(enumYesNo)).
        //                    Select(x => new
        //                    {
        //                        Name = x,
        //                        Value = (int)Enum.Parse(typeof(enumYesNo), x)
        //                    }
        //                           ).ToList();

        //// replace auto Text col with CBO col
        //DataGridViewComboBoxColumn cb = new DataGridViewComboBoxColumn();
        //cb.ValueType = typeof(enumYesNo);
        //cb.DataSource = cboSrc;
        //cb.DisplayMember = "Name";          // important
        //cb.ValueMember = "Value";           // important
        //cb.HeaderText = "Structure";
        //cb.DataPropertyName = "Structure";  // where to store the value

        //dgvItemGroup.Columns.Remove(dgvItemGroup.Columns[2]);  // remove txt col
        //dgvItemGroup.Columns.Add(cb);
        //cb.DisplayIndex = 2;

        //// add data
        //dtStruct.Rows.Add("Ziggy", "Foo", 0, 6);



        //Create Data Table
       


        //RerfeshGrid();
        string _sqlPriceList;
        DataTable _tblPriceList = new DataTable();


        _sqlPriceList = @"
                                        SELECT A.WhsCode AS Code, A.WhsName AS Name,C.U_Area AS Area,
                                        C.U_Inactive AS [Freeze]
                                        FROM OWHS A 
                                        LEFT OUTER JOIN OLCT B ON A.Location = B.Code
                                        FULL OUTER JOIN [@OBPL] C ON A.WhsCode = C.Code
                                        WHERE C.U_BPPriceList  = '" + txtPriceList.Text.Replace("'", "''") + @"' AND ISNULL(A.WhsCode, '') <> ''
                                    ";

        _tblPriceList = new DataTable();
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);


        _dtBranchList.Rows.Clear();
        _dtItemGroupList.Rows.Clear();
        _dtItemList.Rows.Clear();

        foreach (DataRow row in _tblPriceList.Rows)
        {
            switch (row[2].ToString())
            {
                case "N":
                    _dtBranchList.Rows.Add(row[0], row[1], row[2], enumYesNo.N);
                    break;
                case "Y":
                    _dtBranchList.Rows.Add(row[0], row[1], row[2], enumYesNo.Y);
                    break;
                default:
                    _dtBranchList.Rows.Add(row[0], row[1], row[2], enumYesNo.N);
                    break;
            }
        }

        clsFunctions.DataGridViewSetup(dgvBranchList, _dtBranchList, "BranchPriceList");


        //        if (txtPriceList.Text == "")
        //        {
        //            _sqlPriceList = @"SELECT A.ItmsGrpCod, A.ItmsGrpNam, 0.00 AS [VISMIN DISC %], 0.00 AS [GMA DISC %] FROM OITB	A WHERE A.ItmsGrpCod = ''";
        //        }
        //        else
        //        {
        //            _sqlPriceList = @"

        //SELECT A.ItmsGrpCod, A.ItmsGrpNam
        //, CASE WHEN B.U_VisMinDisc IS NULL THEN 0 ELSE B.U_VisMinDisc END AS [VISMIN DISC %]
        //, CASE WHEN B.U_GMADisc IS NULL THEN 0 ELSE B.U_GMADisc END AS [GMA DISC %]
        //FROM OITB A
        //LEFT OUTER JOIN [@OCPL] B ON A.ItmsGrpNam = B.U_Category AND B.Code = '" + txtPriceList.Text + @"'
        //";
        //        }

        //_sqlPriceList = @"
        //                            SELECT 'TRUE' FROM [@OCPL] A WHERE A.Code = '" + txtPriceList.Text + @"'
        //                            ";

        //
        //_tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);
        ////clsFunctions.DataGridViewSetup(dgvItemGroup, _tblPriceList, "ItemGroupPriceList");

        //if(_tblPriceList.Rows.Count < 1)
        //{
        //    DialogResult result;
        //    result = MessageBox.Show("Pricelist Not Found! Do You Want to Create this Pricelist?", "Pricelist Creation", MessageBoxButtons.YesNo);
        //    if (result == System.Windows.Forms.DialogResult.Yes)
        //    {
        //        //clsSAPFunctions.CreatePriceList(txtPriceList.Text, txtPriceList.Text);
        //        //APInvoiceSundry(_DocEntry, _Branch, _DBBranch, _APIDocEntry, DateTime.Parse(txtPostingDate.Text), double.Parse(txtSundryAmount.Text));
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}




        //////////_sqlPriceList = @"
        //////////                                            SELECT A.ItemCode, B.FirmName AS [Brand] ,A.ItemName, 0.00 AS [Price],0.00 AS [Disc], 0.00 AS [Net Of Disc], 0.00 AS [Updated Price] FROM OITM A 
        //////////                                            INNER JOIN OMRC B ON A.FirmCode = B.FirmCode
        //////////                                            WHERE A.ItmsGrpCod = ''
        //////////                                         ";
        //////////_tblPriceList = new DataTable();
        //////////_tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);
        //////////clsFunctions.DataGridViewSetup(dgvItemList_GMA, _tblPriceList);



        //////////_sqlPriceList = @"
        //////////                                            SELECT A.ItemCode, B.FirmName AS [Brand] ,A.ItemName, 0.00 AS [Price],0.00 AS [Disc], 0.00 AS [Net Of Disc], 0.00 AS [Updated Price] FROM OITM A 
        //////////                                            INNER JOIN OMRC B ON A.FirmCode = B.FirmCode
        //////////                                            WHERE A.ItmsGrpCod = ''
        //////////                                         ";
        //////////_tblPriceList = new DataTable();
        //////////_tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);
        //////////clsFunctions.DataGridViewSetup(dgvItemList_VISMIN, _tblPriceList);


        //_sqlPriceList = @"
        //                                SELECT A.CardCode, A.CardName,B.TrnspName, 0.00 AS [Discount] FROM OCRD A 
        //                                LEFT OUTER JOIN OSHP B ON ISNULL(A.ShipType,2) = B.TrnspCode
        //                                WHERE A.CardType = 'C'
        //                                         ";
        //_tblPriceList = new DataTable();
        //_tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);
        //clsFunctions.DataGridViewSetup(dgvCustomerList, _tblPriceList);


        //_sqlPriceList = @"
        //                                SELECT A.WhsCode, A.WhsName,B.Location, 0.00 AS [Discount] FROM OWHS A 
        //                                LEFT OUTER JOIN OLCT B ON A.Location = B.Code
        //                                         ";
        //_tblPriceList = new DataTable();
        //_tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);
        //clsFunctions.DataGridViewSetup(dgvBranchList, _tblPriceList);

    }

    private void RerfeshGrid()
    {
        string _sqlPriceList;
        DataTable _tblPriceList = new DataTable();

        _sqlPriceList = @"
                                    SELECT A.ItmsGrpCod, A.ItmsGrpNam
                                    , CASE WHEN B.U_VisMinDisc IS NULL THEN 0 ELSE B.U_VisMinDisc END AS [VISMIN DISC %]
                                    , CASE WHEN B.U_GMADisc IS NULL THEN 0 ELSE B.U_GMADisc END AS [GMA DISC %]
                                    FROM OITB A
                                    LEFT OUTER JOIN [@OCPL] B ON A.ItmsGrpNam = B.U_Category
                                    WHERE A.ItmsGrpCod = ''
                                    ";

        _tblPriceList = new DataTable();
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);
        clsFunctions.DataGridViewSetup(dgvBranchList, _tblPriceList, "ItemGroupPriceList");



        _sqlPriceList = @"
                                                    SELECT A.ItemCode, B.FirmName AS [Brand] ,A.ItemName, 0.00 AS [Price],0.00 AS [Disc], 0.00 AS [Net Of Disc], 0.00 AS [Updated Price] FROM OITM A 
                                                    INNER JOIN OMRC B ON A.FirmCode = B.FirmCode
                                                    WHERE A.ItmsGrpCod = ''
                                                 ";
        _tblPriceList = new DataTable();
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);
        clsFunctions.DataGridViewSetup(dgvItemList, _tblPriceList);



        _sqlPriceList = @"
                                                    SELECT A.ItemCode, B.FirmName AS [Brand] ,A.ItemName, 0.00 AS [Price],0.00 AS [Disc], 0.00 AS [Net Of Disc], 0.00 AS [Updated Price] FROM OITM A 
                                                    INNER JOIN OMRC B ON A.FirmCode = B.FirmCode
                                                    WHERE A.ItmsGrpCod = ''
                                                 ";
        _tblPriceList = new DataTable();
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);
        clsFunctions.DataGridViewSetup(dgvItemGroupList, _tblPriceList);
    }

    private void dgvItemGroup_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (e.RowIndex < 0)
            {
                return;
            }


            string _Code = dgvBranchList.Rows[e.RowIndex].Cells["Code"].Value.ToString().Trim();
            string _Name = dgvBranchList.Rows[e.RowIndex].Cells["Name"].Value.ToString().Trim();
            string _Freeze = dgvBranchList.Rows[e.RowIndex].Cells[3].Value.ToString().Trim();
            _BranchCode = _Code;


            string _sqlPriceList;
            _sqlPriceList = @"
                                    SELECT A.ItmsGrpCod AS Code, A.ItmsGrpNam AS Name
									,ISNULL( CASE WHEN C.U_Area = 'VISMIN' THEN D.U_VisMinDisc ELSE D.U_GMADisc END , 0 ) AS [MainDisc]
                                    , CASE WHEN B.U_Discount IS NULL THEN 0 ELSE B.U_Discount END AS [Disc]
                                    , B.U_Frozen AS [Freeze]
                                    FROM OITB A
                                    LEFT OUTER JOIN [@BPL2] B ON A.ItmsGrpNam = B.U_Category AND B.Code = '" + _Code.Replace("'","''")  + @"'
									LEFT OUTER JOIN [@OBPL] C ON B.Code = C.Code 
									LEFT OUTER JOIN [@OCPL] D ON D.Code = C.U_BPPriceList AND A.ItmsGrpNam = D.U_Category
                                                 ";
            DataTable _tblPriceList = new DataTable();
            _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);


            _dtItemGroupList.Rows.Clear();
            foreach (DataRow row in _tblPriceList.Rows)
            {
                if (row[4].ToString() == "Y")
                {
                    _dtItemGroupList.Rows.Add(row[0], row[1], row[2], row[3], enumYesNo.Y);
                }
                else
                {
                    _dtItemGroupList.Rows.Add(row[0], row[1], row[2], row[3], enumYesNo.N);
                }
  
            }


            clsFunctions.DataGridViewSetup(dgvItemGroupList, _dtItemGroupList, "BranchItemGroupList");

        }
        catch(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    private void ItemPriceCalc()
    {

    }

    private void txtItemSearch_TextChanged(object sender, EventArgs e)
    {
        string someText;
        //someText = txtItemSearch_GMA.Text;
        someText = "";
        int gridRow = 0;
        int gridColumn = _HeaderColumn;

        dgvItemList.ClearSelection();
        dgvItemList.CurrentCell = null;

        foreach (DataGridViewRow row in dgvItemList.Rows)
        {
            //cboPayrolPeriod.Items.Add(row[0].ToString());

            DataGridViewCell _cell = dgvItemList.Rows[gridRow].Cells[gridColumn];
            if (_cell.Value.ToString().ToLower().Contains(someText.ToLower()) == true)
            {
                dgvItemList.Rows[gridRow].Selected = true;
                dgvItemList.FirstDisplayedScrollingRowIndex = gridRow;

                //_gDocEntry = dgvDisplay.Rows[gridRow].Cells[0].Value.ToString().Trim();

                return;
            }
            gridRow++;
        }
    }

    private void dgvItemList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        try
        {
            _HeaderColumn = e.ColumnIndex;
        }
        catch
        {
        }
    }

    private void dgvItemGroup_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string _VISMINDisc = dgvBranchList.Rows[e.RowIndex].Cells["VISMIN DISC %"].Value.ToString().Trim();
            string _GMADisc = dgvBranchList.Rows[e.RowIndex].Cells["GMA DISC %"].Value.ToString().Trim();


            foreach (DataGridViewRow rGMA in dgvItemList.Rows)
            {
                string _Price = rGMA.Cells["Price"].Value.ToString().Trim();
                rGMA.Cells["Disc"].Value = double.Parse(_GMADisc).ToString("N2");

                double _NetAmount = 0;
                _NetAmount = double.Parse(_Price) - ((double.Parse(_Price) * (double.Parse(_GMADisc) / 100)));
                rGMA.Cells["Updated Price"].Value = _NetAmount.ToString("N2");
            }

            foreach (DataGridViewRow rVISMIN in dgvItemGroupList.Rows)
            {
                string _Price = rVISMIN.Cells["Price"].Value.ToString().Trim();
                rVISMIN.Cells["Disc"].Value = double.Parse(_VISMINDisc).ToString("N2");

                double _NetAmount = 0;
                _NetAmount = double.Parse(_Price) - ((double.Parse(_Price) * (double.Parse(_VISMINDisc) / 100)));
                rVISMIN.Cells["Updated Price"].Value = _NetAmount.ToString("N2");
            }
        }
        catch
        {

        }
    }

    private void dgvItemList_VISMIN_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string _Price = dgvItemGroupList.Rows[e.RowIndex].Cells["Price"].Value.ToString().Trim();
            string _Disc = dgvItemGroupList.Rows[e.RowIndex].Cells["Disc"].Value.ToString().Trim();

            dgvItemGroupList.Rows[e.RowIndex].Cells["Updated Price"].Value = (double.Parse(_Price) - (double.Parse(_Price) * (double.Parse(_Disc) / 100))).ToString("N2");

        }
        catch
        {

        }
    }

    private void dgvItemGroupList_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string _ItmsGrpCod = dgvItemGroupList.Rows[e.RowIndex].Cells["Code"].Value.ToString().Trim();
            string _ItmsGrpNam = dgvItemGroupList.Rows[e.RowIndex].Cells["Name"].Value.ToString().Trim();
            string _Disc = dgvItemGroupList.Rows[e.RowIndex].Cells["Disc"].Value.ToString().Trim();
            string _Freeze = dgvItemGroupList.Rows[e.RowIndex].Cells[4].Value.ToString().Trim();

            string _sqlPriceList;
            _sqlPriceList = @"
                                SELECT A.ItemCode AS Code, B.FirmName AS [Brand] ,A.ItemName AS Name
							    , CASE WHEN C.U_Price IS NULL THEN 0 ELSE C.U_Price END AS [Price]
							    , '" + _Disc + @"' AS [Discount]
							    , CASE WHEN C.U_NetAmt IS NULL THEN 0 ELSE C.U_NetAmt END AS [NetAmount]
							    , CASE WHEN C.U_NetAmt IS NULL THEN 0 ELSE C.U_NetAmt END AS [New NetAmount]
                                , C.U_Frozen 
							    FROM OITM A 
                                INNER JOIN OMRC B ON A.FirmCode = B.FirmCode
							    LEFT OUTER JOIN [@BPL1] C ON A.ItemCode = C.U_ItemCode AND C.Code = '" + _BranchCode + @"'
                                WHERE A.ItmsGrpCod = '" + _ItmsGrpCod + @"'
                            ";
            DataTable _tblPriceList = new DataTable();
            _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);
            //clsFunctions.DataGridViewSetup(dgvItemList, _tblPriceList);

            _dtItemList.Rows.Clear();
            foreach (DataRow row in _tblPriceList.Rows)
            {
                if(row[7].ToString() == "Y")
                {
                    _dtItemList.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5], row[6], enumYesNo.Y);
                }
                else
                {
                    _dtItemList.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5], row[6], enumYesNo.N);
                }
    
            }


            clsFunctions.DataGridViewSetup(dgvItemList, _dtItemList, "BranchItemList");

        }
        catch
        {

        }
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        frmAddBranch frmAddBranch = new frmAddBranch();
        frmAddBranch._PriceList = txtPriceList.Text;
        frmAddBranch.ShowDialog();
    }

    private void btnUpdateItemGroup_Click(object sender, EventArgs e)
    {
        SAPbobsCOM.Company vComp;
        vComp = clsSAPFunctions.oCompany;

        SAPbobsCOM.GeneralService oDocGeneralService;
        SAPbobsCOM.GeneralData oDocGeneralData;
        SAPbobsCOM.GeneralDataCollection oDocLinesCollection;
        SAPbobsCOM.GeneralData oDocLineGeneralData;
        SAPbobsCOM.GeneralDataParams oDocGeneralParams;

        SAPbobsCOM.CompanyService oCompService;
        oCompService = vComp.GetCompanyService();

        // Retrieve the relevant service
        oDocGeneralService = oCompService.GetGeneralService("OBPL");
        // Point to the Header of the Doc UDO
        oDocGeneralData = (SAPbobsCOM.GeneralData)oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);


        oDocGeneralParams = oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
        oDocGeneralParams.SetProperty("Code", _BranchCode);
        oDocGeneralData = oDocGeneralService.GetByParams(oDocGeneralParams);



        foreach (DataGridViewRow row in dgvItemGroupList.Rows)
        {
            string _ItmsGroupCode = row.Cells["Code"].Value.ToString().Trim();
            string _Category = row.Cells["Name"].Value.ToString().Trim();
            string _Discount = row.Cells["Disc"].Value.ToString().Trim();
            string _Freeze = row.Cells[4].Value.ToString().Trim();


            string _sqlOCPL;
            DataTable _tblOCPL = new DataTable();
            _sqlOCPL = @"
                          SELECT A.LineId, A.U_Frozen FROM [@BPL2] A WHERE A.Code = '" + _BranchCode + @"' AND A.U_Category = '" + _Category + @"'
                         ";
            _tblOCPL = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlOCPL);
            string _OCPL_LineId = clsSQLClientFunctions.GetData(_tblOCPL, "LineId", "0");
            string _Frozen = clsSQLClientFunctions.GetData(_tblOCPL, "U_Frozen", "0");

            if (_tblOCPL.Rows.Count > 0)
            {
                oDocLinesCollection = oDocGeneralData.Child("BPL2");
                oDocLineGeneralData = oDocLinesCollection.Item(int.Parse(_OCPL_LineId) - 1);

                //MessageBox.Show(oDocLineGeneralData.GetProperty("U_Category"));
                oDocLineGeneralData.SetProperty("U_Category", _Category);
                oDocLineGeneralData.SetProperty("U_Discount", _Discount);

                if (_Freeze == "0")
                {
                    oDocLineGeneralData.SetProperty("U_Frozen", "Y");
                }
                else
                {
                    oDocLineGeneralData.SetProperty("U_Frozen", "N");
                }
                //oDocLinesCollection.Item(int.Parse(_OCPL_LineId) - 1).SetProperty("U_VisMinDisc", _VISMINDisc);
                //oDocLinesCollection.Item(int.Parse(_OCPL_LineId) - 1).SetProperty("U_GMADisc", _GMADisc);
            }
            else
            {

                oDocLinesCollection = oDocGeneralData.Child("BPL2");
                oDocLineGeneralData = oDocLinesCollection.Add();
                oDocLineGeneralData.SetProperty("U_Category", _Category);
                oDocLineGeneralData.SetProperty("U_Discount", _Discount);
            }


            string _sqlOIPL;
            DataTable _tblOIPL = new DataTable();

            _sqlOIPL = @"
                               SELECT A.ItemCode, A.ItemName
                               , CASE WHEN B.U_Price IS NULL THEN 0.00 ELSE B.U_Price END Price
                               , CASE WHEN B.U_Discount IS NULL THEN 0.00 ELSE B.U_Discount END Discount
                               , B.U_ItemCode, B.LineId , B.U_Frozen
                               FROM OITM A LEFT JOIN [@BPL1] B ON A.ItemCode = B.U_ItemCode AND B.Code =  '" + _BranchCode + @"'
                               WHERE A.ItmsGrpCod = '" + _ItmsGroupCode + @"'

                         ";
            _tblOIPL = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlOIPL);


            foreach (DataRow rOIPL in _tblOIPL.Rows)
            {
                string itm_Frozen = rOIPL["U_Frozen"].ToString();
                string _ItemCode = rOIPL["ItemCode"].ToString();
                string _ItemName = rOIPL["ItemName"].ToString();
                string _PriceList_ItemCode = rOIPL["U_ItemCode"].ToString();

                string _OIPL_LineId = rOIPL["LineId"].ToString();
                string _Price = rOIPL["Price"].ToString();
                //string _Item_Discount = rOIPL["Discount"].ToString();


                if (_PriceList_ItemCode != "")
                {
                    oDocLinesCollection = oDocGeneralData.Child("BPL1");
                    //oDocLineGeneralData = oDocLinesCollection.Add();
                    oDocLinesCollection.Item(int.Parse(_OIPL_LineId) - 1).SetProperty("U_ItemCode", _ItemCode);
                    oDocLinesCollection.Item(int.Parse(_OIPL_LineId) - 1).SetProperty("U_ItemName", _ItemName);
                    oDocLinesCollection.Item(int.Parse(_OIPL_LineId) - 1).SetProperty("U_Price", _Price);
                    oDocLinesCollection.Item(int.Parse(_OIPL_LineId) - 1).SetProperty("U_Discount", _Discount);


                    if(itm_Frozen == "N")
                    {
                        double _Value = 0;
                        _Value = double.Parse(_Price) - (double.Parse(_Price) * (double.Parse(_Discount) / 100));
                        oDocLinesCollection.Item(int.Parse(_OIPL_LineId) - 1).SetProperty("U_NetAmt", _Value);
                    }

                }
                else
                {
                    oDocLinesCollection = oDocGeneralData.Child("BPL1");
                    oDocLineGeneralData = oDocLinesCollection.Add();
                    oDocLineGeneralData.SetProperty("U_ItemCode", _ItemCode);
                    oDocLineGeneralData.SetProperty("U_ItemName", _ItemName);
                    double _Value = 0;
                    oDocLineGeneralData.SetProperty("U_Price", _Value);
                    oDocLineGeneralData.SetProperty("U_Discount", _Discount);

                    _Value = double.Parse("0.00") - (double.Parse("0.00") * (double.Parse(_Discount) / 100));
                    oDocLineGeneralData.SetProperty("U_NetAmt", _Value);

                }
            }


        }

        // Insert Values to the Lines properties try
        try
        {
            oDocGeneralService.Update(oDocGeneralData);
            //MessageBox.Show("Price List Successfully Updated");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }


        if (vComp.InTransaction)
        {
            vComp.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
            //MessageBox.Show("Price List Successfully Updated");
            //SBO_Application.MessageBox("Add Doc UDO Header and Lines successfully", 1, "", "", "");
        }
        else
        {
            //MessageBox.Show("Error in Updating Price List");
            //SBO_Application.MessageBox("Error adding object", 1, "", "", "");
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        string _sqlPriceList = @"
 SELECT A.WhsCode AS Code, A.WhsName AS Name,B.Location AS Area, A.U_PriceList AS PriceList
                                        FROM OWHS A 
                                        LEFT OUTER JOIN OLCT B ON A.Location = B.Code
										WHERE ISNULL(A.U_PriceList,'') <> '' AND ISNULL(B.Location,'') <> ''
                                    ";

        DataTable _tblPriceList = new DataTable();
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);



        double _RowCount;
        int _Count = 0;
        _RowCount = _tblPriceList.Rows.Count;


        foreach (DataRow row in _tblPriceList.Rows)
        {
            string _Code = row["Code"].ToString();
            string _Name = row["Name"].ToString();
            string _Area = row["Area"].ToString();
            string _BPPricelist = row["PriceList"].ToString();

            BranchPriceList(_Code, _Name, _Area, _BPPricelist);


            Application.DoEvents();
            _Count++;
            lblStatus1.Text = "Pricelist Data Reset : (" + _Count + " / " + _RowCount + ") : Data Progress Percentage ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) : Current Price List Uploading " + _BPPricelist + " : " + _Code + " - " + _Name;
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
        }
    }





    private void BranchPriceList(string _Code, string _Name, string _Area, string _PriceList)
    {
        SAPbobsCOM.Company vComp;
        vComp = clsSAPFunctions.oCompany;

        SAPbobsCOM.GeneralService oDocGeneralService;
        SAPbobsCOM.GeneralData oDocGeneralData;
        SAPbobsCOM.GeneralDataCollection oDocLinesCollection;
        SAPbobsCOM.GeneralData oDocLineGeneralData;

        SAPbobsCOM.CompanyService oCompService;
        oCompService = vComp.GetCompanyService();

        // Retrieve the relevant service
        oDocGeneralService = oCompService.GetGeneralService("OBPL");
        // Point to the Header of the Doc UDO
        oDocGeneralData = (SAPbobsCOM.GeneralData)oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);

        // Insert values to the Header properties
        oDocGeneralData.SetProperty("Code", _Code);
        oDocGeneralData.SetProperty("Name", _Name);
        oDocGeneralData.SetProperty("U_WhsName", _Name);
        oDocGeneralData.SetProperty("U_Area", _Area);
        oDocGeneralData.SetProperty("U_BPPriceList", _PriceList);
        oDocGeneralData.SetProperty("U_Inactive", "N");

        string _sqlPriceList;
        DataTable _tblPriceList = new DataTable();
        _sqlPriceList = @"
                    SELECT A.U_Category AS ItmsGrpNam
                         , CASE WHEN 'GMA' = '" + _Area + @"' THEN A.U_GMADisc ELSE A.U_VisMinDisc END AS Discount
                    FROM [@OCPL] A WHERE A.Code = '" + _PriceList + @"'
                                    ";
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);


        double _RowCount;
        int _Count = 0;
        _RowCount = _tblPriceList.Rows.Count;

        foreach (DataRow row in _tblPriceList.Rows)
        {
            string _ItmsGrpNam = row["ItmsGrpNam"].ToString();
            string _Disc = row["Discount"].ToString();


            oDocLinesCollection = oDocGeneralData.Child("BPL2");
            oDocLineGeneralData = oDocLinesCollection.Add();
            oDocLineGeneralData.SetProperty("U_Category", _ItmsGrpNam);
            oDocLineGeneralData.SetProperty("U_Discount", _Disc);
            oDocLineGeneralData.SetProperty("U_Frozen", "N");



            Application.DoEvents();
            _Count++;
            tssDataStatus.Text = "Pricelist Data Reset : (" + _Count + " / " + _RowCount + ") : Data Progress Percentage ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) : Current Item Group Uploading : " + _ItmsGrpNam;
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
        }


        _sqlPriceList = @"
                          SELECT A.U_ItemCode AS ItemCode, A.U_ItemName AS ItemName
						  , CASE WHEN 'GMA' = '" + _Area + @"' THEN A.U_GMAPrice ELSE A.U_VisMinPrice END AS Price 
						  , CASE WHEN 'GMA' = '" + _Area + @"' THEN A.U_GMADisc ELSE A.U_VisMinDisc END AS Discount 
						  , CASE WHEN 'GMA' = '" + _Area + @"' THEN A.U_GMANet ELSE A.U_VisMinNet END AS NetAmt 
						   FROM [@OIPL] A WHERE A.Code =  '" + _PriceList + @"'
                                    ";
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);


        _Count = 0;
        _RowCount = _tblPriceList.Rows.Count;

        foreach (DataRow row in _tblPriceList.Rows)
        {
            string _ItemCode = row["ItemCode"].ToString();
            string _ItemName = row["ItemName"].ToString();
            string _Price = row["Price"].ToString();
            string _Discount = row["Discount"].ToString();
            string _NetAmt = row["NetAmt"].ToString();


            oDocLinesCollection = oDocGeneralData.Child("BPL1");
            oDocLineGeneralData = oDocLinesCollection.Add();
            oDocLineGeneralData.SetProperty("U_ItemCode", _ItemCode);
            oDocLineGeneralData.SetProperty("U_ItemName", _ItemName);
            oDocLineGeneralData.SetProperty("U_Price", _Price);
            oDocLineGeneralData.SetProperty("U_Discount", _Discount);
            oDocLineGeneralData.SetProperty("U_NetAmt", _NetAmt);
            oDocLineGeneralData.SetProperty("U_Frozen", "N");


            Application.DoEvents();
            _Count++;
            tssDataStatus.Text = "Pricelist Data Reset : (" + _Count + " / " + _RowCount + ") : Data Progress Percentage ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) : Current Item Uploading : " + _ItemName;
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
        }



        // Insert Values to the Lines properties try
        try
        {
            oDocGeneralService.Add(oDocGeneralData);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            //oDocGeneralService.Update(oDocGeneralData);
        }

        if (vComp.InTransaction)
        {
            vComp.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
            //SBO_Application.MessageBox("Add Doc UDO Header and Lines successfully", 1, "", "", "");
        }
        else
        {
            //SBO_Application.MessageBox("Error adding object", 1, "", "", "");
        }
    }



    private void btnUpdateItem_Click(object sender, EventArgs e)
    {
        SAPbobsCOM.Company vComp;
        vComp = clsSAPFunctions.oCompany;

        SAPbobsCOM.GeneralService oDocGeneralService;
        SAPbobsCOM.GeneralData oDocGeneralData;
        SAPbobsCOM.GeneralDataCollection oDocLinesCollection;
        SAPbobsCOM.GeneralData oDocLineGeneralData;
        SAPbobsCOM.GeneralDataParams oDocGeneralParams;

        SAPbobsCOM.CompanyService oCompService;
        oCompService = vComp.GetCompanyService();

        // Retrieve the relevant service
        oDocGeneralService = oCompService.GetGeneralService("OBPL");
        // Point to the Header of the Doc UDO
        oDocGeneralData = (SAPbobsCOM.GeneralData)oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);


        oDocGeneralParams = oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
        oDocGeneralParams.SetProperty("Code", _BranchCode);
        oDocGeneralData = oDocGeneralService.GetByParams(oDocGeneralParams);


        foreach (DataGridViewRow row in dgvItemList.Rows)
        {
            string _Code = row.Cells["Code"].Value.ToString().Trim();
            string _Name = row.Cells["Name"].Value.ToString().Trim();
            string _Price = row.Cells["Price"].Value.ToString().Trim();
            string _Disc = row.Cells["Disc"].Value.ToString().Trim();
            string _NetAmt = row.Cells["New Net Amount"].Value.ToString().Trim();
            string _Freeze = row.Cells[7].Value.ToString().Trim();



            string _sqlOCPL;
            DataTable _tblOCPL = new DataTable();
            _sqlOCPL = @"
                          SELECT A.LineId FROM [@OIPL] A WHERE A.Code = '" + txtPriceList.Text + @"' AND A.U_ItemCode = '" + _Code + @"'
                         ";
            _tblOCPL = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlOCPL);
            string _LineId = clsSQLClientFunctions.GetData(_tblOCPL, "LineId", "0");


            if (_LineId != "")
            {
                oDocLinesCollection = oDocGeneralData.Child("BPL1");
                oDocLineGeneralData = oDocLinesCollection.Item(int.Parse(_LineId) - 1);
                //MessageBox.Show(oDocLineGeneralData.GetProperty("U_Category"));
                oDocLineGeneralData.SetProperty("U_ItemCode", _Code);
                oDocLineGeneralData.SetProperty("U_ItemName", _Name);
                oDocLineGeneralData.SetProperty("U_Price", _Price);
                oDocLineGeneralData.SetProperty("U_Discount", _Disc);
                oDocLineGeneralData.SetProperty("U_NetAmt", _NetAmt);

                if(_Freeze == "0")
                {
                    oDocLineGeneralData.SetProperty("U_Frozen", "Y");
                }
                else
                {
                    oDocLineGeneralData.SetProperty("U_Frozen", "N");
                }

            }
            else
            {
                oDocLinesCollection = oDocGeneralData.Child("BPL1");
                oDocLineGeneralData = oDocLinesCollection.Add();
                oDocLineGeneralData.SetProperty("U_ItemCode", _Code);
                oDocLineGeneralData.SetProperty("U_ItemName", _Name);
                oDocLineGeneralData.SetProperty("U_Price", _Price);
                oDocLineGeneralData.SetProperty("U_Discount", _Disc);
                oDocLineGeneralData.SetProperty("U_NetAmt", _NetAmt);
                oDocLineGeneralData.SetProperty("U_Frozen", "N");
            }
        }

        // Insert Values to the Lines properties try
        try
        {
            oDocGeneralService.Update(oDocGeneralData);
            //MessageBox.Show("Price List Successfully Updated");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }


        if (vComp.InTransaction)
        {
            vComp.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
        }
        else
        {

        }
    }

    private void dgvItemGroupList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string _Disc = dgvItemGroupList.Rows[e.RowIndex].Cells["Disc"].Value.ToString().Trim();


            foreach (DataGridViewRow row in dgvItemList.Rows)
            {
                string _Price = row.Cells["Price"].Value.ToString().Trim();
                row.Cells["Disc"].Value = double.Parse(_Disc).ToString("N2");

                double _NetAmount = 0;
                _NetAmount = double.Parse(_Price) - ((double.Parse(_Price) * (double.Parse(_Disc) / 100)));
                row.Cells["New Net Amount"].Value = _NetAmount.ToString("N5");
            }

        }
        catch
        {

        }
    }

    private void txtBranchSearch_TextChanged(object sender, EventArgs e)
    {
        String someText;
        someText = txtBranchSearch.Text;

        int gridRow = 0;
        int gridColumn = _HeaderColumn;

        dgvBranchList.ClearSelection();
        dgvBranchList.CurrentCell = null;

        foreach (DataGridViewRow row in dgvBranchList.Rows)
        {
            //cboPayrolPeriod.Items.Add(row[0].ToString());

            DataGridViewCell _cell = dgvBranchList.Rows[gridRow].Cells[gridColumn];
            if (_cell.Value.ToString().ToLower().Contains(someText.ToLower()) == true)
            {
                dgvBranchList.Rows[gridRow].Selected = true;
                dgvBranchList.FirstDisplayedScrollingRowIndex = gridRow;

                //_gDocEntry = dgvDisplay.Rows[gridRow].Cells[0].Value.ToString().Trim();

                return;
            }
            gridRow++;
        }
    }

    private void txtItemSearch_TextChanged_1(object sender, EventArgs e)
    {
        String someText;
        someText = txtItemSearch.Text;

        int gridRow = 0;
        int gridColumn = _HeaderItemColumn;

        dgvItemList.ClearSelection();
        dgvItemList.CurrentCell = null;

        foreach (DataGridViewRow row in dgvItemList.Rows)
        {
            //cboPayrolPeriod.Items.Add(row[0].ToString());

            DataGridViewCell _cell = dgvItemList.Rows[gridRow].Cells[gridColumn];
            if (_cell.Value.ToString().ToLower().Contains(someText.ToLower()) == true)
            {
                dgvItemList.Rows[gridRow].Selected = true;
                dgvItemList.FirstDisplayedScrollingRowIndex = gridRow;

                //_gDocEntry = dgvDisplay.Rows[gridRow].Cells[0].Value.ToString().Trim();

                return;
            }
            gridRow++;
        }
    }

    private void dgvBranchList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        try
        {
            _HeaderColumn = e.ColumnIndex;
        }
        catch
        {
        }
    }

    private void dgvItemList_ColumnHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
    {
        try
        {
            _HeaderItemColumn = e.ColumnIndex;
        }
        catch
        {
        }
    }

    private void dgvBranchList_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void dgvItemGroupList_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
}