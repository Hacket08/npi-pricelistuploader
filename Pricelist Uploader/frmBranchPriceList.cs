using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;

using System.Configuration;

public partial class frmBranchPriceList : Form
{
    private enum enumYesNo
    { Y, N }

    DataTable _dtBranchList = new DataTable();
    DataTable _dtItemGroupList = new DataTable();
    DataTable _dtItemList = new DataTable();


    DataGridView _datagridviewItemGroup = new DataGridView();
    DataTable _dtTempItemGroupList = new DataTable();

    static int _HeaderColumn;
    static int _HeaderItemColumn;
    static string _BranchCode;


    static bool _Excel_ItemGroupUploading = false;
    static bool _Excel_ItemsUploading = false;

    public frmBranchPriceList()
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


    private void frmBranchPriceList_Load(object sender, EventArgs e)
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

        dgvBranchList.DataSource = _dtBranchList;


        //DataGridViewComboBoxColumn cb = new DataGridViewComboBoxColumn();
        //cb.ValueType = typeof(enumYesNo);
        //cb.DataSource = cboSrc;
        //cb.DisplayMember = "Name";          // important
        //cb.ValueMember = "Value";           // important
        //cb.HeaderText = "Freeze";
        //cb.DataPropertyName = "Freeze";  // where to store the value

        //dgvBranchList.Columns.Remove(dgvBranchList.Columns[3]);  // remove txt col
        //dgvBranchList.Columns.Add(cb);
        //cb.DisplayIndex = 3;

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



        // Item Group Table

        _dtTempItemGroupList.Columns.Add("Code", typeof(string));
        _dtTempItemGroupList.Columns.Add("Name", typeof(string));
        _dtTempItemGroupList.Columns.Add("MainDisc", typeof(double));
        _dtTempItemGroupList.Columns.Add("Disc", typeof(double));
        _dtTempItemGroupList.Columns.Add("Freeze", typeof(enumYesNo));

        _datagridviewItemGroup.DataSource = _dtTempItemGroupList;


        //DataGridViewComboBoxColumn cbTempItemGroupList = new DataGridViewComboBoxColumn();
        //cbTempItemGroupList.ValueType = typeof(enumYesNo);
        //cbTempItemGroupList.DataSource = cboSrc;
        //cbTempItemGroupList.DisplayMember = "Name";          // important
        //cbTempItemGroupList.ValueMember = "Value";           // important
        //cbTempItemGroupList.HeaderText = "Freeze";
        //cbTempItemGroupList.DataPropertyName = "Freeze";  // where to store the value

        //_datagridviewItemGroup.Columns.Remove(_datagridviewItemGroup.Columns[4]);  // remove txt col
        //_datagridviewItemGroup.Columns.Add(cbTempItemGroupList);
        //cbTempItemGroupList.DisplayIndex = 4;


        clsFunctions.DataGridViewSetup(_datagridviewItemGroup, _dtTempItemGroupList);







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

    }

    private void txtPriceList_TextChanged(object sender, EventArgs e)
    {

    }

    private void lblRefresh_Click(object sender, EventArgs e)
    {
        _Excel_ItemGroupUploading = false;
        _Excel_ItemsUploading = false;
        dgvItemGroupList.DataSource = _dtItemGroupList;
        dgvItemList.DataSource = _dtItemList;

        // create DataSource as list of Name-Value pairs from enum
        var cboSrc = Enum.GetNames(typeof(enumYesNo)).
                            Select(x => new
                            {
                                Name = x,
                                Value = (int)Enum.Parse(typeof(enumYesNo), x)
                            }
                                   ).ToList();


        // Item Group Table
        _dtItemGroupList.Columns.Clear();
        _dtItemGroupList.Columns.Add("Code", typeof(string));
        _dtItemGroupList.Columns.Add("Name", typeof(string));
        _dtItemGroupList.Columns.Add("MainDisc", typeof(double));
        _dtItemGroupList.Columns.Add("Disc", typeof(double));
        _dtItemGroupList.Columns.Add("Freeze", typeof(enumYesNo));

        //dgvItemGroupList.Columns.Clear();
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

        _dtItemList.Columns.Clear();
        _dtItemList.Columns.Add("Code", typeof(string));
        _dtItemList.Columns.Add("Brand", typeof(string));
        _dtItemList.Columns.Add("Name", typeof(string));
        _dtItemList.Columns.Add("Price", typeof(double));
        _dtItemList.Columns.Add("Disc", typeof(double));
        _dtItemList.Columns.Add("Net Amount", typeof(double));
        _dtItemList.Columns.Add("New Net Amount", typeof(double));
        _dtItemList.Columns.Add("Freeze", typeof(enumYesNo));

        //dgvItemList.Columns.Clear();
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

        string _sqlPriceList;
        DataTable _tblPriceList = new DataTable();


        _sqlPriceList = @"
                                        SELECT A.WhsCode AS Code, A.WhsName AS Name,C.U_Area AS Area
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

            _dtBranchList.Rows.Add(row[0], row[1], row[2]);
            //switch (row[2].ToString())
            //{
            //    case "N":
            //        _dtBranchList.Rows.Add(row[0], row[1], row[2], enumYesNo.N);
            //        break;
            //    case "Y":
            //        _dtBranchList.Rows.Add(row[0], row[1], row[2], enumYesNo.Y);
            //        break;
            //    default:
            //        _dtBranchList.Rows.Add(row[0], row[1], row[2], enumYesNo.N);
            //        break;
            //}
        }

        clsFunctions.DataGridViewSetup(dgvBranchList, _dtBranchList, "BranchPriceList");
    }

    private void txtPriceList_Leave(object sender, EventArgs e)
    {
        

    }

    private void dgvBranchList_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (e.RowIndex < 0)
            {
                return;
            }


            string _Code = dgvBranchList.Rows[e.RowIndex].Cells["Code"].Value.ToString().Trim();
            string _Name = dgvBranchList.Rows[e.RowIndex].Cells["Name"].Value.ToString().Trim();
            _BranchCode = _Code;


            string _sqlPriceList;
            _sqlPriceList = @"
                                    SELECT A.ItmsGrpCod AS Code, A.ItmsGrpNam AS Name
									,ISNULL( CASE WHEN C.U_Area = 'VISMIN' THEN D.U_VisMinDisc ELSE D.U_GMADisc END , 0 ) AS [MainDisc]
                                    , CASE WHEN B.U_Discount IS NULL THEN 0 ELSE B.U_Discount END AS [Disc]
                                    , B.U_Frozen AS [Freeze]
                                    FROM OITB A
                                    LEFT OUTER JOIN [@BPL2] B ON A.ItmsGrpNam = B.U_Category AND B.Code = '" + _Code.Replace("'", "''") + @"'
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

            _sqlPriceList = @"
                                SELECT A.ItemCode AS Code, B.FirmName AS [Brand] ,A.ItemName AS Name
							    , CASE WHEN C.U_Price IS NULL THEN 0 ELSE C.U_Price END AS [Price]
							    , CASE WHEN C.U_Discount IS NULL THEN 0 ELSE C.U_Discount END AS [Discount]
							    , CASE WHEN C.U_NetAmt IS NULL THEN 0 ELSE C.U_NetAmt END AS [NetAmount]
							    , CASE WHEN C.U_NetAmt IS NULL THEN 0 ELSE C.U_NetAmt END AS [New NetAmount]
                                , C.U_Frozen 
							    FROM OITM A 
                                INNER JOIN OMRC B ON A.FirmCode = B.FirmCode
							    LEFT OUTER JOIN [@BPL1] C ON A.ItemCode = C.U_ItemCode AND C.Code = '" + _BranchCode + @"'
                            ";
            _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);

            _dtItemList.Rows.Clear();
            foreach (DataRow row in _tblPriceList.Rows)
            {
                if (row[7].ToString() == "Y")
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
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void dgvBranchList_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void btnNew_Click(object sender, EventArgs e)
    {
        frmAddBranch frmAddBranch = new frmAddBranch();
        frmAddBranch._PriceList = txtPriceList.Text;
        frmAddBranch.ShowDialog();
        lblRefresh_Click(sender, e);
    }

    private void lblFolderBrws_Click(object sender, EventArgs e)
    {

        DataTable _DataList = new DataTable();
        _DataList.Clear();
        ofdExcel.Filter = "EXCEL files (*.xlsx)|*.xlsx|EXCEL files 2003 (*.xls)|*.xls|All files (*.*)|*.*";
        ofdExcel.FilterIndex = 3;
        string _ExtensionName = "";

        DialogResult result = ofdExcel.ShowDialog();
        if (result == DialogResult.OK)
        {
            txtExcelFile.Text = ofdExcel.FileName;
            _ExtensionName = Path.GetExtension(txtExcelFile.Text);
        }

        if (result == DialogResult.Cancel)
        {
            return;
        }

        switch (_ExtensionName)
        {
            case ".xls":
            case ".xlsx":

                DataTable _DataTable;
                OleDbConnection MyConnection;
                string _ExcelPath = txtExcelFile.Text;
                string _ExcelCon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _ExcelPath + ";Extended Properties=Excel 12.0;";
                MyConnection = new OleDbConnection(_ExcelCon);

                MyConnection.Open();
                // Get the data table containg the schema guid.
                _DataTable = MyConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (_DataTable == null)
                {
                    return;
                }

                cmbWorkSheet.Items.Clear();
                foreach (DataRow row in _DataTable.Rows)
                {
                    if (!row["TABLE_NAME"].ToString().Contains("#"))
                    {
                        cmbWorkSheet.Items.Add(row["TABLE_NAME"].ToString());
                    }
                }
                cmbWorkSheet.SelectedIndex = 0;
                MyConnection.Close();
                break;
        }
    }

    private void btnItemGroupSave_Click(object sender, EventArgs e)
    {
        if(_Excel_ItemGroupUploading == false)
        {
            UpdateItemGroupList(_BranchCode);
        }
        else
        {
            Excel_UpdateItemGroupList();
        }


        MessageBox.Show("Price List Successfully Updated");
        lblRefresh_Click(sender, e);
    }


    private void Excel_UpdateItemGroupList()
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


        double _RowCount;
        int _Count;

        //_Count = 0;
        //_RowCount = dgvItemGroupList.Rows.Count;

        //foreach (DataGridViewRow row in dgvItemGroupList.Rows)
        //{
        //    string _BRANCHCODE = row.Cells["BRANCHCODE"].Value.ToString().Trim();
        //    string _ITEMGROUP = row.Cells["ITEMGROUP"].Value.ToString().Trim();
        //    string _DISC = row.Cells["DISC"].Value.ToString().Trim();
        //    string _FREEZE = row.Cells["FREEZE"].Value.ToString().Trim();

        //    oDocGeneralParams = oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
        //    oDocGeneralParams.SetProperty("Code", _BRANCHCODE);
        //    oDocGeneralData = oDocGeneralService.GetByParams(oDocGeneralParams);



        //    string _sqlOCPL;
        //    DataTable _tblOCPL = new DataTable();
        //    _sqlOCPL = @"
        //                  SELECT A.LineId, A.U_Frozen FROM [@BPL2] A WHERE A.Code = '" + _BRANCHCODE + @"' AND A.U_Category = '" + _ITEMGROUP + @"'
        //                 ";

        //    _tblOCPL = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlOCPL);
        //    string _OCPL_LineId = clsSQLClientFunctions.GetData(_tblOCPL, "LineId", "0");
        //    string _Frozen = clsSQLClientFunctions.GetData(_tblOCPL, "U_Frozen", "0");

        //    if (_tblOCPL.Rows.Count > 0)
        //    {
        //        oDocLinesCollection = oDocGeneralData.Child("BPL2");
        //        oDocLineGeneralData = oDocLinesCollection.Item(int.Parse(_OCPL_LineId) - 1);

        //        //MessageBox.Show(oDocLineGeneralData.GetProperty("U_Category"));
        //        oDocLineGeneralData.SetProperty("U_Category", _ITEMGROUP);
        //        oDocLineGeneralData.SetProperty("U_Discount", _DISC);
        //        oDocLineGeneralData.SetProperty("U_Frozen", _FREEZE);
        //    }
        //    else
        //    {

        //        oDocLinesCollection = oDocGeneralData.Child("BPL2");
        //        oDocLineGeneralData = oDocLinesCollection.Add();
        //        oDocLineGeneralData.SetProperty("U_Category", _ITEMGROUP);
        //        oDocLineGeneralData.SetProperty("U_Discount", _DISC);
        //        oDocLineGeneralData.SetProperty("U_Frozen", _FREEZE);
        //    }


        //}

        

        _Count = 0;
        _RowCount = dgvItemGroupList.Rows.Count;


        foreach (DataGridViewRow row in dgvItemGroupList.Rows)
        {
            string _BRANCHCODE = row.Cells["BRANCHCODE"].Value.ToString().Trim();
            string _ITEMGROUP = row.Cells["ITEMGROUP"].Value.ToString().Trim();
            string _DISC = row.Cells["DISC"].Value.ToString().Trim();
            string _FREEZE = row.Cells["FREEZE"].Value.ToString().Trim();

            oDocGeneralParams = oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
            oDocGeneralParams.SetProperty("Code", _BRANCHCODE);
            oDocGeneralData = oDocGeneralService.GetByParams(oDocGeneralParams);



            string _sqlOCPL;
            DataTable _tblOCPL = new DataTable();
            _sqlOCPL = @"
                          SELECT A.LineId, ISNULL(A.U_Frozen, 'N') AS U_Frozen FROM [@BPL2] A WHERE A.Code = '" + _BRANCHCODE + @"' AND A.U_Category = '" + _ITEMGROUP + @"'
                         ";

            _tblOCPL = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlOCPL);
            string _OCPL_LineId = clsSQLClientFunctions.GetData(_tblOCPL, "LineId", "0");
            string _Frozen = clsSQLClientFunctions.GetData(_tblOCPL, "U_Frozen", "0");

            if (_tblOCPL.Rows.Count > 0)
            {
                oDocLinesCollection = oDocGeneralData.Child("BPL2");
                oDocLineGeneralData = oDocLinesCollection.Item(int.Parse(_OCPL_LineId) - 1);

                //MessageBox.Show(oDocLineGeneralData.GetProperty("U_Category"));
                oDocLineGeneralData.SetProperty("U_Category", _ITEMGROUP);
                oDocLineGeneralData.SetProperty("U_Discount", _DISC);
                oDocLineGeneralData.SetProperty("U_Frozen", _FREEZE);
            }
            else
            {

                oDocLinesCollection = oDocGeneralData.Child("BPL2");
                oDocLineGeneralData = oDocLinesCollection.Add();
                oDocLineGeneralData.SetProperty("U_Category", _ITEMGROUP);
                oDocLineGeneralData.SetProperty("U_Discount", _DISC);
                oDocLineGeneralData.SetProperty("U_Frozen", _FREEZE);
            }


            string _sqlOIPL;
            DataTable _tblOIPL = new DataTable();


            _sqlOIPL = @"SELECT A.Code, B.Code, B.U_Area, A.U_ItemCode, A.U_ItemName, E.ItmsGrpNam, ISNULL(F.U_Frozen,'N') AS [C_Frozen]
                                , A.U_VisMinDisc, A.U_VisMinNet, A.U_VisMinPrice
                                , A.U_GMADisc, A.U_GMANet, A.U_GMAPrice 
                                , CASE WHEN B.U_Area = 'GMA' THEN A.U_GMANet ELSE A.U_VisMinNet END AS [Main Net]
                                , C.U_Discount, C.U_NetAmt, C.U_Price 
                                , ISNULL(C.U_Frozen, 'N') AS U_Frozen, C.LineId, C.U_Frozen
                                FROM [@OIPL] A 
                                INNER JOIN [@OBPL] B ON A.Code = B.U_BPPriceList
                                LEFT JOIN [@BPL1] C ON B.Code = C.Code AND A.U_ItemCode = C.U_ItemCode
								LEFT JOIN [OITM] D ON D.ItemCode = C.U_ItemCode
								LEFT JOIN [OITB] E ON D.ItmsGrpCod = E.ItmsGrpCod
								LEFT JOIN [@BPL2] F ON E.ItmsGrpNam = F.U_Category AND F.Code = B.Code

                                WHERE E.ItmsGrpNam = '" + _ITEMGROUP + @"'  AND B.Code = '" + _BRANCHCODE + @"'";



            //_sqlOIPL = @"
            //                   SELECT A.ItemCode, A.ItemName
            //                   , CASE WHEN B.U_Price IS NULL THEN 0.00 ELSE B.U_Price END Price
            //                   , CASE WHEN B.U_Discount IS NULL THEN 0.00 ELSE B.U_Discount END Discount
            //                   , B.U_ItemCode, B.LineId , B.U_Frozen
            //                   FROM OITM A INNER JOIN [OITB] C ON A.ItmsGrpCod = C.ItmsGrpCod
            //                   LEFT JOIN [@BPL1] B ON A.ItemCode = B.U_ItemCode AND B.Code =  '" + _BRANCHCODE + @"'
            //                   WHERE C.ItmsGrpNam = '" + _ITEMGROUP + @"'

            //             ";
            _tblOIPL = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlOIPL);


            double _Count1 = 0;
            double _RowCount1 = _tblOIPL.Rows.Count;

            foreach (DataRow rOIPL in _tblOIPL.Rows)
            {
                string itm_Frozen = rOIPL["U_Frozen"].ToString();
                string _ItemCode = rOIPL["U_ItemCode"].ToString();
                string _ItemName = rOIPL["U_ItemName"].ToString();
                string _PriceList_ItemCode = rOIPL["U_ItemCode"].ToString();

                string _OIPL_LineId = rOIPL["LineId"].ToString();
                string _Price = rOIPL["U_Price"].ToString();
                //string _Item_Discount = rOIPL["U_Discount"].ToString();


                if (_PriceList_ItemCode != "")
                {
                    oDocLinesCollection = oDocGeneralData.Child("BPL1");
                    oDocLineGeneralData = oDocLinesCollection.Item(int.Parse(_OIPL_LineId) - 1);

                    oDocLineGeneralData.SetProperty("U_ItemCode", _ItemCode);
                    oDocLineGeneralData.SetProperty("U_ItemName", _ItemName);
                    oDocLineGeneralData.SetProperty("U_Price", _Price);
                    oDocLineGeneralData.SetProperty("U_Discount", _DISC);

                    if (itm_Frozen == "N")
                    {
                        double _Value = 0;
                        _Value = double.Parse(_Price) - (double.Parse(_Price) * (double.Parse(_DISC) / 100));
                        oDocLineGeneralData.SetProperty("U_NetAmt", _Value);
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
                    oDocLineGeneralData.SetProperty("U_Discount", _DISC);

                    _Value = double.Parse("0.00") - (double.Parse("0.00") * (double.Parse(_DISC) / 100));
                    oDocLineGeneralData.SetProperty("U_NetAmt", _Value);

                }



                Application.DoEvents();
                _Count1++;
                tssDataStatus.Text = "Pricelist Data Reset : (" + _Count1 + " / " + _RowCount1 + ") : Data Progress Percentage ( " + Math.Round(((_Count1 / _RowCount1) * 100), 2) + " % ) : Current Item Uploading : " + _ItemName;
                pbDataProgress.Value = Convert.ToInt32(((_Count1 / _RowCount1) * 100));
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



            Application.DoEvents();
            _Count++;
            lblStatus1.Text = "Pricelist Data Reset : (" + _Count + " / " + _RowCount + ") : Data Progress Percentage ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) : Current Item Group Uploading : " + _ITEMGROUP;
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
        }
    }



    private void Excel_UpdateItemList()
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

        double _RowCount;
        int _Count;

        _Count = 0;
        _RowCount = dgvItemList.Rows.Count;


        foreach (DataGridViewRow row in dgvItemList.Rows)
        {
            string _BRANCHCODE = row.Cells["BRANCHCODE"].Value.ToString().Trim();
            string _ITEMCODE = row.Cells["ITEMCODE"].Value.ToString().Trim();
            string _PRICE = row.Cells["PRICE"].Value.ToString().Trim();
            string _FREEZE = row.Cells["FREEZE"].Value.ToString().Trim();



            oDocGeneralParams = oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
            oDocGeneralParams.SetProperty("Code", _BRANCHCODE);
            oDocGeneralData = oDocGeneralService.GetByParams(oDocGeneralParams);



            string _sqlOCPL;
            DataTable _tblOCPL = new DataTable();
            _sqlOCPL = @"
                          SELECT A.LineId, A.U_ItemName, A.U_Discount FROM [@BPL1] A WHERE A.Code = '" + _BRANCHCODE + @"' AND A.U_ItemCode = '" + _ITEMCODE + @"'
                         ";
            _tblOCPL = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlOCPL);
            string _LineId = clsSQLClientFunctions.GetData(_tblOCPL, "LineId", "0");
            string _Discount = clsSQLClientFunctions.GetData(_tblOCPL, "U_Discount", "0");
            string _ItemName = clsSQLClientFunctions.GetData(_tblOCPL, "U_ItemName", "0");


            if (_LineId != "")
            {
                oDocLinesCollection = oDocGeneralData.Child("BPL1");
                oDocLineGeneralData = oDocLinesCollection.Item(int.Parse(_LineId) - 1);
                oDocLineGeneralData.SetProperty("U_NetAmt", _PRICE);
                oDocLineGeneralData.SetProperty("U_Frozen", _FREEZE);
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



            Application.DoEvents();
            _Count++;
            tssDataStatus.Text = "Pricelist Data Reset : (" + _Count + " / " + _RowCount + ") : Data Progress Percentage ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) : Current Item Uploading : " + _ItemName;
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
        }
    }

    private void UpdateItemList(string _BranchCodeList = "")
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
        oDocGeneralParams.SetProperty("Code", _BranchCodeList);
        oDocGeneralData = oDocGeneralService.GetByParams(oDocGeneralParams);


        double _RowCount;
        int _Count;

        _Count = 0;
        _RowCount = dgvItemList.Rows.Count;

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
                          SELECT A.LineId FROM [@BPL1] A WHERE A.Code = '" + _BranchCodeList + @"' AND A.U_ItemCode = '" + _Code + @"'
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

                if (_Freeze == "0")
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


            Application.DoEvents();
            _Count++;
            tssDataStatus.Text = "Pricelist Data Reset : (" + _Count + " / " + _RowCount + ") : Data Progress Percentage ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) : Current Item Uploading : " + _Name;
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
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



    private void UpdateItemGroupListAll(string _BranchCodeList = "")
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
        oDocGeneralParams.SetProperty("Code", _BranchCodeList);
        oDocGeneralData = oDocGeneralService.GetByParams(oDocGeneralParams);








        string _sqlPriceList;
        //_sqlPriceList = @"
        //                            SELECT A.ItmsGrpCod AS Code, A.ItmsGrpNam AS Name
								//	,ISNULL( CASE WHEN C.U_Area = 'VISMIN' THEN D.U_VisMinDisc ELSE D.U_GMADisc END , 0 ) AS [MainDisc]
        //                            , CASE WHEN B.U_Discount IS NULL THEN 0 ELSE B.U_Discount END AS [Disc]
        //                            , B.U_Frozen AS [Freeze]
        //                            FROM OITB A
        //                            LEFT OUTER JOIN [@BPL2] B ON A.ItmsGrpNam = B.U_Category AND B.Code = '" + _BranchCodeList.Replace("'", "''") + @"'
								//	LEFT OUTER JOIN [@OBPL] C ON B.Code = C.Code 
								//	LEFT OUTER JOIN [@OCPL] D ON D.Code = C.U_BPPriceList AND A.ItmsGrpNam = D.U_Category
        //                                         ";
        _sqlPriceList = @"
                                    SELECT DISTINCT A.ItmsGrpCod AS Code, A.ItmsGrpNam AS Name
									,ISNULL( CASE WHEN C.U_Area = 'VISMIN' THEN D.U_VisMinDisc ELSE D.U_GMADisc END , 0 ) AS [MainDisc]
                                    , CASE WHEN B.U_Discount IS NULL THEN 0 ELSE B.U_Discount END AS [Disc]
                                    , B.U_Frozen AS [Freeze] , E.U_Frozen, E.U_Discount 
                                    FROM OITB A
                                    LEFT OUTER JOIN [@BPL2] B ON A.ItmsGrpNam = B.U_Category 
									LEFT OUTER JOIN [@OBPL] C ON B.Code = C.Code 
									LEFT OUTER JOIN [@OCPL] D ON D.Code = C.U_BPPriceList AND A.ItmsGrpNam = D.U_Category
									LEFT OUTER JOIN [OITM] F ON A.ItmsGrpCod = F.ItmsGrpCod
									LEFT OUTER JOIN [@BPL1] E ON B.Code = E.Code AND F.ItemCode = E.U_ItemCode
									WHERE  (CASE WHEN B.U_Discount IS NULL THEN 0 ELSE B.U_Discount END) <> ISNULL(E.U_Discount ,0 ) AND ISNULL(E.U_ItemCode, '') <> '' 
                                            AND B.Code = '" + _BranchCodeList.Replace("'", "''") + @"' 
                                                 ";
        DataTable _tblPriceList = new DataTable();
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);
        

        double _RowCount;
        int _Count;

        _Count = 0;
        _RowCount = _tblPriceList.Rows.Count;

        #region Item Group Price Update
        
        foreach (DataRow row in _tblPriceList.Rows)
        {
            string _ItmsGroupCode = row["Code"].ToString().Trim();
            string _Category = row["Name"].ToString().Trim();
            string _Discount = row["Disc"].ToString().Trim();
            string _Freeze = row[4].ToString().Trim();


            string _sqlOCPL;
            DataTable _tblOCPL = new DataTable();
            _sqlOCPL = @"
                          SELECT A.LineId, ISNULL(A.U_Frozen, 'N') AS U_Frozen FROM [@BPL2] A WHERE A.Code = '" + _BranchCodeList + @"' AND A.U_Category = '" + _Category + @"'
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
            }
            else
            {

                oDocLinesCollection = oDocGeneralData.Child("BPL2");
                oDocLineGeneralData = oDocLinesCollection.Add();
                oDocLineGeneralData.SetProperty("U_Category", _Category);
                oDocLineGeneralData.SetProperty("U_Discount", _Discount);
            }



            Application.DoEvents();
            _Count++;
            tssDataStatus.Text = "Pricelist Data Reset : (" + _Count + " / " + _RowCount + ") : Data Progress Percentage ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) : Current Item Group Uploading : " + _Category;
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
        }
        #endregion




        #region Item List Price Uploading
        foreach (DataRow row in _tblPriceList.Rows)
        {
            string _ItmsGroupCode = row["Code"].ToString().Trim();
            string _Category = row["Name"].ToString().Trim();
            string _Discount = row["Disc"].ToString().Trim();
            string _Freeze = row[4].ToString().Trim();

            string _sqlOIPL;
            DataTable _tblOIPL = new DataTable();

            _sqlOIPL = @"
                               SELECT A.ItemCode, A.ItemName
                               , CASE WHEN B.U_Price IS NULL THEN 0.00 ELSE B.U_Price END Price
                               , CASE WHEN B.U_Discount IS NULL THEN 0.00 ELSE B.U_Discount END Discount
                               , B.U_ItemCode, B.LineId , ISNULL(B.U_Frozen, 'N') AS U_Frozen
                               FROM OITM A LEFT JOIN [@BPL1] B ON A.ItemCode = B.U_ItemCode AND B.Code =  '" + _BranchCodeList + @"'
                               WHERE A.ItmsGrpCod = '" + _ItmsGroupCode + @"'

                         ";
            _tblOIPL = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlOIPL);

            _Count = 0;
            _RowCount = _tblOIPL.Rows.Count;

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
                    oDocLineGeneralData = oDocLinesCollection.Item(int.Parse(_OIPL_LineId) - 1);

                    oDocLineGeneralData.SetProperty("U_ItemCode", _ItemCode);
                    oDocLineGeneralData.SetProperty("U_ItemName", _ItemName);
                    oDocLineGeneralData.SetProperty("U_Price", _Price);
                    oDocLineGeneralData.SetProperty("U_Discount", _Discount);

                    if (itm_Frozen == "N")
                    {
                        double _Value = 0;
                        _Value = double.Parse(_Price) - (double.Parse(_Price) * (double.Parse(_Discount) / 100));
                        oDocLineGeneralData.SetProperty("U_NetAmt", _Value);
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



                Application.DoEvents();
                _Count++;
                tssDataStatus.Text = "Pricelist Data Reset : (" + _Count + " / " + _RowCount + ") : Data Progress Percentage ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) : Current Item Uploading : " + _ItemName;
                pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
            }


        }
        #endregion


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


    private void UpdateItemGroupList(string _BranchCodeList = "")
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
        oDocGeneralParams.SetProperty("Code", _BranchCodeList);
        oDocGeneralData = oDocGeneralService.GetByParams(oDocGeneralParams);


        double _RowCount;
        int _Count;

        _Count = 0;
        _RowCount = dgvItemGroupList.Rows.Count;

        #region Item Group Price Update
        foreach (DataGridViewRow row in dgvItemGroupList.Rows)
        {
            string _ItmsGroupCode = row.Cells["Code"].Value.ToString().Trim();
            string _Category = row.Cells["Name"].Value.ToString().Trim();
            string _Discount = row.Cells["Disc"].Value.ToString().Trim();
            string _Freeze = row.Cells[4].Value.ToString().Trim();


            string _sqlOCPL;
            DataTable _tblOCPL = new DataTable();
            _sqlOCPL = @"
                          SELECT A.LineId, ISNULL(A.U_Frozen, 'N') AS U_Frozen FROM [@BPL2] A WHERE A.Code = '" + _BranchCodeList + @"' AND A.U_Category = '" + _Category + @"'
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
            }
            else
            {

                oDocLinesCollection = oDocGeneralData.Child("BPL2");
                oDocLineGeneralData = oDocLinesCollection.Add();
                oDocLineGeneralData.SetProperty("U_Category", _Category);
                oDocLineGeneralData.SetProperty("U_Discount", _Discount);
            }



            Application.DoEvents();
            _Count++;
            tssDataStatus.Text = "Pricelist Data Reset : (" + _Count + " / " + _RowCount + ") : Data Progress Percentage ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) : Current Item Group Uploading : " + _Category;
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
        }
        #endregion
        
        #region Item List Price Uploading

        foreach (DataGridViewRow row in dgvItemGroupList.Rows)
        {
            string _ItmsGroupCode = row.Cells["Code"].Value.ToString().Trim();
            string _Category = row.Cells["Name"].Value.ToString().Trim();
            string _Discount = row.Cells["Disc"].Value.ToString().Trim();
            string _Freeze = row.Cells[4].Value.ToString().Trim();

            string _sqlOIPL;
            DataTable _tblOIPL = new DataTable();

            _sqlOIPL = @"
                               SELECT A.ItemCode, A.ItemName
                               , CASE WHEN B.U_Price IS NULL THEN 0.00 ELSE B.U_Price END Price
                               , CASE WHEN B.U_Discount IS NULL THEN 0.00 ELSE B.U_Discount END Discount
                               , B.U_ItemCode, B.LineId , ISNULL(B.U_Frozen, 'N') AS U_Frozen
                               FROM OITM A LEFT JOIN [@BPL1] B ON A.ItemCode = B.U_ItemCode AND B.Code =  '" + _BranchCodeList + @"'
                               WHERE A.ItmsGrpCod = '" + _ItmsGroupCode + @"'

                         ";
            _tblOIPL = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlOIPL);

            _Count = 0;
            _RowCount = _tblOIPL.Rows.Count;

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
                    oDocLineGeneralData = oDocLinesCollection.Item(int.Parse(_OIPL_LineId) - 1);

                    oDocLineGeneralData.SetProperty("U_ItemCode", _ItemCode);
                    oDocLineGeneralData.SetProperty("U_ItemName", _ItemName);
                    oDocLineGeneralData.SetProperty("U_Price", _Price);
                    oDocLineGeneralData.SetProperty("U_Discount", _Discount);

                    if (itm_Frozen == "N")
                    {
                        double _Value = 0;
                        _Value = double.Parse(_Price) - (double.Parse(_Price) * (double.Parse(_Discount) / 100));
                        oDocLineGeneralData.SetProperty("U_NetAmt", _Value);
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



                Application.DoEvents();
                _Count++;
                tssDataStatus.Text = "Pricelist Data Reset : (" + _Count + " / " + _RowCount + ") : Data Progress Percentage ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) : Current Item Uploading : " + _ItemName;
                pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
            }


        }
        #endregion


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

    private void btnItemListSave_Click(object sender, EventArgs e)
    {


        if (_Excel_ItemsUploading == false)
        {
            UpdateItemList(_BranchCode);
        }
        else
        {
            Excel_UpdateItemList();
        }


        MessageBox.Show("Price List Successfully Updated");
        lblRefresh_Click(sender, e);
    }

    private void btnGenerate_Click(object sender, EventArgs e)
    {
        if (txtExcelFile.Text == "")
        {
            MessageBox.Show("Please define Excel File Before Generating Data");
            return;
        }

        _Excel_ItemsUploading = true;
        _dtBranchList.Rows.Clear();
        DataTable _DataTable;
        OleDbConnection MyConnection;
        string _ExcelPath = txtExcelFile.Text;
        string _ExcelCon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _ExcelPath + ";Extended Properties=Excel 12.0;";
        MyConnection = new OleDbConnection(_ExcelCon);

        MyConnection.Open();
        _DataTable = MyConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

        if (_DataTable == null)
        {
            return;
        }

        DataSet DtSet;
        OleDbDataAdapter MyDataAdapte;

        MyDataAdapte = new OleDbDataAdapter("SELECT * FROM [" + cmbWorkSheet.Text + "]", MyConnection);
        DtSet = new DataSet();
        MyDataAdapte.Fill(DtSet);
        _DataTable = DtSet.Tables[0];
        MyConnection.Close();

        DataTable _DataList = new DataTable();
        _DataList = _DataTable;
        dgvItemList.Columns.Clear();
        clsFunctions.DataGridViewSetup(dgvItemList, _DataTable);
    }

    private void btnItemGroup_Click(object sender, EventArgs e)
    {
        if(txtExcelFileItemGroup.Text == "")
        {
            MessageBox.Show("Please define Excel File Before Generating Data");
            return;
        }

        _Excel_ItemGroupUploading = true;
        _dtBranchList.Rows.Clear();
        DataTable _DataTable;
        OleDbConnection MyConnection;
        string _ExcelPath = txtExcelFileItemGroup.Text;
        string _ExcelCon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _ExcelPath + ";Extended Properties=Excel 12.0;";
        MyConnection = new OleDbConnection(_ExcelCon);

        MyConnection.Open();
        _DataTable = MyConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

        if (_DataTable == null)
        {
            return;
        }

        DataSet DtSet;
        OleDbDataAdapter MyDataAdapte;

        MyDataAdapte = new OleDbDataAdapter("SELECT * FROM [" + cmbWorkSheetItemGroup.Text + "]", MyConnection);
        DtSet = new DataSet();
        MyDataAdapte.Fill(DtSet);
        _DataTable = DtSet.Tables[0];
        MyConnection.Close();

        DataTable _DataList = new DataTable();
        _DataList = _DataTable;
        dgvItemGroupList.Columns.Clear();
        clsFunctions.DataGridViewSetup(dgvItemGroupList, _DataTable);
    }

    private void lblFolderBrwsGrp_Click(object sender, EventArgs e)
    {

        DataTable _DataList = new DataTable();
        _DataList.Clear();
        ofdExcel.Filter = "EXCEL files (*.xlsx)|*.xlsx|EXCEL files 2003 (*.xls)|*.xls|All files (*.*)|*.*";
        ofdExcel.FilterIndex = 3;
        string _ExtensionName = "";

        DialogResult result = ofdExcel.ShowDialog();
        if (result == DialogResult.OK)
        {
            txtExcelFileItemGroup.Text = ofdExcel.FileName;
            _ExtensionName = Path.GetExtension(txtExcelFileItemGroup.Text);
        }

        if (result == DialogResult.Cancel)
        {
            return;
        }


        switch (_ExtensionName)
        {
            case ".xls":
            case ".xlsx":

                DataTable _DataTable;
                OleDbConnection MyConnection;
                string _ExcelPath = txtExcelFileItemGroup.Text;
                string _ExcelCon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _ExcelPath + ";Extended Properties=Excel 12.0;";
                MyConnection = new OleDbConnection(_ExcelCon);

                MyConnection.Open();
                // Get the data table containg the schema guid.
                _DataTable = MyConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (_DataTable == null)
                {
                    return;
                }

                cmbWorkSheetItemGroup.Items.Clear();
                foreach (DataRow row in _DataTable.Rows)
                {
                    if (!row["TABLE_NAME"].ToString().Contains("#"))
                    {
                        cmbWorkSheetItemGroup.Items.Add(row["TABLE_NAME"].ToString());
                    }
                }
                cmbWorkSheetItemGroup.SelectedIndex = 0;
                MyConnection.Close();
                break;
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

    private void dgvItemList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        try
        {
            _HeaderItemColumn = e.ColumnIndex;
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

    private void txtItemSearch_TextChanged(object sender, EventArgs e)
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

    private void btnDelete_Click(object sender, EventArgs e)
    {
        SAPbobsCOM.Company vComp;
        vComp = clsSAPFunctions.oCompany;

        SAPbobsCOM.GeneralService oDocGeneralService;
        SAPbobsCOM.GeneralData oDocGeneralData;
        //SAPbobsCOM.GeneralDataCollection oDocLinesCollection;
        //SAPbobsCOM.GeneralData oDocLineGeneralData;
        SAPbobsCOM.GeneralDataParams oDocGeneralParams;

        SAPbobsCOM.CompanyService oCompService;
        oCompService = vComp.GetCompanyService();

        // Retrieve the relevant service
        oDocGeneralService = oCompService.GetGeneralService("OBPL");
        // Point to the Header of the Doc UDO
        oDocGeneralData = (SAPbobsCOM.GeneralData)oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);


        oDocGeneralParams = oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
        oDocGeneralParams.SetProperty("Code", _BranchCode);

        // Insert Values to the Lines properties try
        try
        {
            oDocGeneralService.Delete(oDocGeneralParams);
            MessageBox.Show("Branch " + _BranchCode + "  Successfully Deleted");
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

        lblRefresh_Click(sender, e);
    }

    private void lblRefBranch_Click(object sender, EventArgs e)
    {
        tssDataStatus.Text = "";
        //clsSAPFunctions.UpdatePriceList(txtPriceList.Text);
        string _sqlPriceList = @"
                                        SELECT A.WhsCode AS Code, A.WhsName AS Name,C.U_Area AS Area
                                        FROM OWHS A 
                                        LEFT OUTER JOIN OLCT B ON A.Location = B.Code
                                        FULL OUTER JOIN [@OBPL] C ON A.WhsCode = C.Code
                                        WHERE ISNULL(C.U_BPPriceList, '') <> '' AND ISNULL(A.WhsCode, '') <> ''
                                    ";

        DataTable _tblPriceList = new DataTable();
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);


        double _MRowCount;
        int _MCount = 0;
        _MRowCount = _tblPriceList.Rows.Count;


        foreach (DataRow row in _tblPriceList.Rows)
        {
            string _BranchCode = row["Code"].ToString();
            UpdateItemGroupListAll(_BranchCode);

            Application.DoEvents();
            _MCount++;
            lblStatus1.Text = "Pricelist Data Reset : (" + _MCount + " / " + _MRowCount + ") : Data Progress Percentage ( " + Math.Round(((_MCount / _MRowCount) * 100), 2) + " % ) : Current Branch Updating " + _BranchCode;
            pbDataProgress.Value = Convert.ToInt32(((_MCount / _MRowCount) * 100));

        }

        //UpdateItemGroupList();

        MessageBox.Show("Price List Successfully Updated");
    }
}