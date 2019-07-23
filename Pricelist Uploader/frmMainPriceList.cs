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
public partial class frmMainPriceList : Form
{
    static int _HeaderColumn;
    static bool _ExcelUploading = false;

    public frmMainPriceList()
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

    private void frmMainPriceList_Load(object sender, EventArgs e)
    {
        string _sqlPriceList = @"SELECT A.Code FROM [@OCRP] A";
        DataTable _tblPriceList = new DataTable();
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);

        txtPriceList.AutoCompleteCustomSource = LoadAutoComplete(_tblPriceList, 0);
        txtPriceList.AutoCompleteMode = AutoCompleteMode.Suggest;
        txtPriceList.AutoCompleteSource = AutoCompleteSource.CustomSource;
    }

    private void lblRefresh_Click(object sender, EventArgs e)
    {




        _ExcelUploading = false;
        string _sqlPriceList;
        DataTable _tblPriceList = new DataTable();

        _sqlPriceList = @"
                                    SELECT 'TRUE' FROM [@OCRP] A WHERE A.Code = '" + txtPriceList.Text + @"'
                                    ";
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);

        if (_tblPriceList.Rows.Count < 1)
        {

            DialogResult result;
            result = MessageBox.Show("Pricelist Not Found! Do You Want to Create this Pricelist?", "Pricelist Creation", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                clsSAPFunctions.CreatePriceList(txtPriceList.Text, txtPriceList.Text);
            }
            else
            {
                return;
            }
        }




        _sqlPriceList = @"
                                    SELECT A.ItmsGrpCod AS Code, A.ItmsGrpNam AS Name
                                    , CASE WHEN B.U_VisMinDisc IS NULL THEN 0 ELSE B.U_VisMinDisc END AS [VISMIN DISC %]
                                    , CASE WHEN B.U_GMADisc IS NULL THEN 0 ELSE B.U_GMADisc END AS [GMA DISC %]
                                    FROM OITB A
                                    LEFT OUTER JOIN [@OCPL] B ON A.ItmsGrpNam = B.U_Category AND B.Code = '" + txtPriceList.Text + @"'
                                    ";

        _tblPriceList = new DataTable();
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);
        clsFunctions.DataGridViewSetup(dgvItemGroup, _tblPriceList, "ItemGroupPriceList");


        dgvItemList.Columns.Clear();
        _sqlPriceList = @"
                                  SELECT A.ItemCode AS Code, B.FirmName AS [Brand] ,A.ItemName AS Name
                                , C. U_GMAPrice AS [GMA Price], C.U_GMADisc AS [GMA Disc], C.U_GMANet AS [GMA Net Amount] , C.U_GMANet AS [GMA New Net Amount] 
                                , C.U_VisMinPrice AS [VISMIN Price], C.U_VisMinDisc AS [VISMIN Disc], C.U_VisMinNet AS [VISMIN Net Amount], C.U_VisMinNet AS [VISMIN New Net Amount]
                                FROM OITM A 
                                INNER JOIN OMRC B ON A.FirmCode = B.FirmCode
                                LEFT OUTER JOIN [@OIPL] C ON A.ItemCode = C.U_ItemCode               
                                WHERE C.Code = '" + txtPriceList.Text + @"'
                         ";

        _tblPriceList = new DataTable();
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);
        clsFunctions.DataGridViewSetup(dgvItemList, _tblPriceList, "MasterItemList");
    }

    private void dgvItemList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if(e.ColumnIndex == 3)
            {
                string _Price = dgvItemList.Rows[e.RowIndex].Cells["GMA Price"].Value.ToString().Trim();
                string _Disc = dgvItemList.Rows[e.RowIndex].Cells["GMA Disc"].Value.ToString().Trim();

                dgvItemList.Rows[e.RowIndex].Cells["GMA New Net Amount"].Value = (double.Parse(_Price) - (double.Parse(_Price) * (double.Parse(_Disc) / 100))).ToString("N2");
            }

            if (e.ColumnIndex == 7)
            {
                string _Price = dgvItemList.Rows[e.RowIndex].Cells["VISMIN Price"].Value.ToString().Trim();
                string _Disc = dgvItemList.Rows[e.RowIndex].Cells["VISMIN Disc"].Value.ToString().Trim();

                dgvItemList.Rows[e.RowIndex].Cells["VISMIN New Net Amount"].Value = (double.Parse(_Price) - (double.Parse(_Price) * (double.Parse(_Disc) / 100))).ToString("N2");
            }
        }
        catch
        {

        }
    }

    private void btnItemGroupSave_Click(object sender, EventArgs e)
    {
        if (txtPriceList.Text == "")
        {
            MessageBox.Show("Pricelist Not Define");
            return;
        }

        UpdateItemGroupList(txtPriceList.Text);

        string _sqlPriceList = @"
                                        SELECT A.WhsCode AS Code, A.WhsName AS Name, B.Location AS Area, A.U_PriceList AS PriceList
                                        FROM OWHS A 
                                        LEFT OUTER JOIN OLCT B ON A.Location = B.Code
										WHERE ISNULL(A.U_PriceList,'') = '" + txtPriceList.Text + @"' AND ISNULL(B.Location,'') <> ''
                                        AND A.WhsCode IN (
                                                            SELECT DISTINCT B.Code FROM [@OCPL] A 
                                                            INNER JOIN [@OBPL] B ON A.Code = B.U_BPPriceList
                                                            INNER JOIN [@BPL2] C ON B.Code = C.Code AND A.U_Category = C.U_Category
                                                            WHERE CASE WHEN B.U_Area = 'GMA' THEN A.U_GMADisc ELSE A.U_VisMinDisc END <> C.U_Discount
                                                            AND A.Code = '" + txtPriceList.Text + @"'
                                                         )
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

            UpdateBranch(_BPPricelist, _Code, _Area);


            Application.DoEvents();
            _Count++;
            lblStatus1.Text = "Pricelist Data Reset : (" + _Count + " / " + _RowCount + ") : Data Progress Percentage ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) : Current Price List Uploading " + _BPPricelist + " : Branch :" + _Name;
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
        }
        //UpdateItemGroupList();

        MessageBox.Show("Price List Successfully Updated");
        lblRefresh_Click(sender, e);



    }



    private void UpdateBranch(string _PriceList, string _BranchCode, string _Area)
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


        string _sqlPriceList;
        DataTable _tblPriceList = new DataTable();

        #region Branch Item Group Price Update


        _sqlPriceList = @"
                            SELECT A.Code, A.U_Category, B.Code, B.U_Area, A.U_VisMinDisc, A.U_GMADisc
                            , CASE WHEN B.U_Area = 'GMA' THEN A.U_GMADisc ELSE A.U_VisMinDisc END AS [Main Discs]
                            , C.U_Discount, ISNULL(C.U_Frozen,'N') AS U_Frozen FROM [@OCPL] A 
                            INNER JOIN [@OBPL] B ON A.Code = B.U_BPPriceList
                            INNER JOIN [@BPL2] C ON B.Code = C.Code AND A.U_Category = C.U_Category
                            WHERE CASE WHEN B.U_Area = 'GMA' THEN A.U_GMADisc ELSE A.U_VisMinDisc END <> C.U_Discount
                            AND A.Code = '" + _PriceList + @"' AND B.Code = '" + _BranchCode + @"'
                         ";

        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);

        foreach (DataRow r_ItmGrp in _tblPriceList.Rows)
        {
            string _Category = r_ItmGrp["U_Category"].ToString();
            string _VISMINDisc = r_ItmGrp["U_VisMinDisc"].ToString();
            string _GMADisc = r_ItmGrp["U_GMADisc"].ToString();

            string _Discount = "";
            if (_Area == "GMA")
            {
                _Discount = _GMADisc;
            }
            else
            {
                _Discount = _VISMINDisc;
            }



            string _LineId;
            string _Frozen;


            string _sqlBPL2 = @"
                          SELECT A.LineId, ISNULL(A.U_Frozen,'N') AS U_Frozen FROM [@BPL2] A WHERE A.Code = '" + _BranchCode + @"' AND A.U_Category = '" + _Category + @"'
                         ";

            DataTable _tblBPL2 = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlBPL2);
            _LineId = clsSQLClientFunctions.GetData(_tblBPL2, "LineId", "0");
            _Frozen = clsSQLClientFunctions.GetData(_tblBPL2, "U_Frozen", "0");

            if (_tblBPL2.Rows.Count > 0)
            {
                oDocLinesCollection = oDocGeneralData.Child("BPL2");
                oDocLineGeneralData = oDocLinesCollection.Item(int.Parse(_LineId) - 1);

                oDocLineGeneralData.SetProperty("U_Category", _Category);

                if (_Frozen == "N")
                {
                    oDocLineGeneralData.SetProperty("U_Discount", _Discount);
                }

            }
            else
            {
                oDocLinesCollection = oDocGeneralData.Child("BPL2");
                oDocLineGeneralData = oDocLinesCollection.Add();

                oDocLineGeneralData.SetProperty("U_Category", _Category);
                oDocLineGeneralData.SetProperty("U_Discount", _Discount);
            }

        }
        #endregion

        #region Branch Item List Price Update

        _sqlPriceList = @"SELECT A.Code, B.Code, B.U_Area, A.U_ItemCode, A.U_ItemName, E.ItmsGrpNam, ISNULL(F.U_Frozen, 'N') AS [C_Frozen]
                                , A.U_VisMinDisc, A.U_VisMinNet, A.U_VisMinPrice
                                , A.U_GMADisc, A.U_GMANet, A.U_GMAPrice 
                                , CASE WHEN B.U_Area = 'GMA' THEN A.U_GMANet ELSE A.U_VisMinNet END AS [Main Net]
                                , C.U_Discount, C.U_NetAmt, C.U_Price 
                                , ISNULL(C.U_Frozen, 'N') AS U_Frozen
                                FROM [@OIPL] A 
                                INNER JOIN [@OBPL] B ON A.Code = B.U_BPPriceList
                                LEFT JOIN [@BPL1] C ON B.Code = C.Code AND A.U_ItemCode = C.U_ItemCode
								LEFT JOIN [OITM] D ON D.ItemCode = A.U_ItemCode
								LEFT JOIN [OITB] E ON D.ItmsGrpCod = E.ItmsGrpCod
								LEFT JOIN [@BPL2] F ON E.ItmsGrpNam = F.U_Category AND F.Code = B.Code

                                WHERE CASE WHEN B.U_Area = 'GMA' THEN A.U_GMANet ELSE A.U_VisMinNet END <> ISNULL(C.U_NetAmt, 0)
                                    AND A.Code = '" + _PriceList + @"'  AND B.Code = '" + _BranchCode + @"'
                                    AND CASE WHEN C.U_Frozen IS NULL THEN 'N' ELSE C.U_Frozen END = 'N'";


        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);


        double _RowCount;
        int _Count = 0;
        _RowCount = _tblPriceList.Rows.Count;

        foreach (DataRow r_Itm in _tblPriceList.Rows)
        {
            string _ItemCode = r_Itm["U_ItemCode"].ToString();
            string _ItemName = r_Itm["U_ItemName"].ToString();
            string _C_Frozen = r_Itm["C_Frozen"].ToString();

            string _VisMinPrice = r_Itm["U_VisMinPrice"].ToString();
            string _VisMinDisc = r_Itm["U_VisMinDisc"].ToString();
            string _VisMinNet = r_Itm["U_VisMinNet"].ToString();

            string _GMAPrice = r_Itm["U_GMAPrice"].ToString();
            string _GMADisc = r_Itm["U_GMADisc"].ToString();
            string _GMANet = r_Itm["U_GMANet"].ToString();

            string _Price = "";
            string _Disc = "";
            string _NetAmt = "";
            if (_Area == "GMA")
            {
                _Price = _GMAPrice;
                _Disc = _GMADisc;
                _NetAmt = _GMANet;
            }
            else
            {
                _Price = _VisMinPrice;
                _Disc = _VisMinDisc;
                _NetAmt = _VisMinNet;
            }



            string _LineId;
            string _Frozen;


            string _sqlBPL2 = @"
                          SELECT A.LineId, ISNULL(A.U_Frozen,'N') AS U_Frozen, A.U_Discount, A.U_NetAmt FROM [@BPL1] A WHERE A.Code = '" + _BranchCode + @"' AND A.U_ItemCode = '" + _ItemCode + @"'
                         ";

            DataTable _tblBPL2 = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlBPL2);
            _LineId = clsSQLClientFunctions.GetData(_tblBPL2, "LineId", "0");
            _Frozen = clsSQLClientFunctions.GetData(_tblBPL2, "U_Frozen", "0");
            string Itm_Discount = clsSQLClientFunctions.GetData(_tblBPL2, "U_Discount", "0");
            string Itm_NetAmt = clsSQLClientFunctions.GetData(_tblBPL2, "U_NetAmt", "0");


            if (_tblBPL2.Rows.Count > 0)
            {

                oDocLinesCollection = oDocGeneralData.Child("BPL1");
                oDocLineGeneralData = oDocLinesCollection.Item(int.Parse(_LineId) - 1);

                //MessageBox.Show(oDocLineGeneralData.GetProperty("U_Category"));
                oDocLineGeneralData.SetProperty("U_ItemCode", _ItemCode);
                oDocLineGeneralData.SetProperty("U_ItemName", _ItemName);
                oDocLineGeneralData.SetProperty("U_Price", _Price);

                switch (_C_Frozen)
                {
                    case "N":
                        oDocLineGeneralData.SetProperty("U_Discount", _Disc);
                        break;
                    case "Y":
                        oDocLineGeneralData.SetProperty("U_Discount", Itm_Discount);
                        _Disc = Itm_Discount;
                        break;
                }

                double _Value = 0;
                _Value = double.Parse(_Price) - (double.Parse(_Price) * (double.Parse(_Disc) / 100));

                if (_Frozen == "N")
                {
                    oDocLineGeneralData.SetProperty("U_NetAmt", _Value);
                }

            }
            else
            {
                oDocLinesCollection = oDocGeneralData.Child("BPL1");
                oDocLineGeneralData = oDocLinesCollection.Add();
                oDocLineGeneralData.SetProperty("U_ItemCode", _ItemCode);
                oDocLineGeneralData.SetProperty("U_ItemName", _ItemName);
                oDocLineGeneralData.SetProperty("U_Price", _Price);
                oDocLineGeneralData.SetProperty("U_Discount", _Disc);
                oDocLineGeneralData.SetProperty("U_NetAmt", _NetAmt);
            }


            Application.DoEvents();
            _Count++;
            tssDataStatus.Text = "Pricelist Data Reset : (" + _Count + " / " + _RowCount + ") : Data Progress Percentage ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) : Current Item Uploading : " + _ItemName;
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
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


    private void UpdateBranchItem(string _PriceList, string _BranchCode, string _Area)
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

        string _sqlPriceList;
        DataTable _tblPriceList = new DataTable();

        #region Branch Item List Price Update

        _sqlPriceList = @"SELECT A.Code, B.Code, B.U_Area, A.U_ItemCode, A.U_ItemName, E.ItmsGrpNam, ISNULL(F.U_Frozen,'N') AS [C_Frozen]
                                , A.U_VisMinDisc, A.U_VisMinNet, A.U_VisMinPrice
                                , A.U_GMADisc, A.U_GMANet, A.U_GMAPrice 
                                , CASE WHEN B.U_Area = 'GMA' THEN A.U_GMANet ELSE A.U_VisMinNet END AS [Main Net]
                                , C.U_Discount, C.U_NetAmt, C.U_Price 
                                , ISNULL(C.U_Frozen,'N') AS U_Frozen
                                FROM [@OIPL] A 
                                INNER JOIN [@OBPL] B ON A.Code = B.U_BPPriceList
                                LEFT JOIN [@BPL1] C ON B.Code = C.Code AND A.U_ItemCode = C.U_ItemCode
								LEFT JOIN [OITM] D ON D.ItemCode = A.U_ItemCode
								LEFT JOIN [OITB] E ON D.ItmsGrpCod = E.ItmsGrpCod
								LEFT JOIN [@BPL2] F ON E.ItmsGrpNam = F.U_Category AND F.Code = B.Code

                                WHERE CASE WHEN B.U_Area = 'GMA' THEN A.U_GMANet ELSE A.U_VisMinNet END <> ISNULL(C.U_NetAmt,0)
                                    AND A.Code = '" + _PriceList + @"'  AND B.Code = '" + _BranchCode + @"'
                                    AND CASE WHEN C.U_Frozen IS NULL THEN 'N' ELSE C.U_Frozen END = 'N'";


        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);


        double _RowCount;
        int _Count = 0;
        _RowCount = _tblPriceList.Rows.Count;

        foreach (DataRow r_Itm in _tblPriceList.Rows)
        {
            string _ItemCode = r_Itm["U_ItemCode"].ToString();
            string _ItemName = r_Itm["U_ItemName"].ToString();
            string _C_Frozen = r_Itm["C_Frozen"].ToString();

            string _VisMinPrice = r_Itm["U_VisMinPrice"].ToString();
            string _VisMinDisc = r_Itm["U_VisMinDisc"].ToString();
            string _VisMinNet = r_Itm["U_VisMinNet"].ToString();

            string _GMAPrice = r_Itm["U_GMAPrice"].ToString();
            string _GMADisc = r_Itm["U_GMADisc"].ToString();
            string _GMANet = r_Itm["U_GMANet"].ToString();

            string _Price = "";
            string _Disc = "";
            string _NetAmt = "";
            if (_Area == "GMA")
            {
                _Price = _GMAPrice;
                _Disc = _GMADisc;
                _NetAmt = _GMANet;
            }
            else
            {
                _Price = _VisMinPrice;
                _Disc = _VisMinDisc;
                _NetAmt = _VisMinNet;
            }



            string _LineId;
            string _Frozen;


            string _sqlBPL2 = @"
                          SELECT A.LineId, ISNULL(A.U_Frozen, 'N') AS U_Frozen, A.U_Discount, A.U_NetAmt FROM [@BPL1] A WHERE A.Code = '" + _BranchCode + @"' AND A.U_ItemCode = '" + _ItemCode + @"'
                         ";

            DataTable _tblBPL2 = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlBPL2);
            _LineId = clsSQLClientFunctions.GetData(_tblBPL2, "LineId", "0");
            _Frozen = clsSQLClientFunctions.GetData(_tblBPL2, "U_Frozen", "0");
            string Itm_Discount = clsSQLClientFunctions.GetData(_tblBPL2, "U_Discount", "0");
            string Itm_NetAmt = clsSQLClientFunctions.GetData(_tblBPL2, "U_NetAmt", "0");


            if (_tblBPL2.Rows.Count > 0)
            {

                oDocLinesCollection = oDocGeneralData.Child("BPL1");
                oDocLineGeneralData = oDocLinesCollection.Item(int.Parse(_LineId) - 1);

                //MessageBox.Show(oDocLineGeneralData.GetProperty("U_Category"));
                oDocLineGeneralData.SetProperty("U_ItemCode", _ItemCode);
                oDocLineGeneralData.SetProperty("U_ItemName", _ItemName);
                oDocLineGeneralData.SetProperty("U_Price", _Price);

                switch (_C_Frozen)
                {
                    case "N":
                        oDocLineGeneralData.SetProperty("U_Discount", _Disc);
                        break;
                    case "Y":
                        oDocLineGeneralData.SetProperty("U_Discount", Itm_Discount);
                        _Disc = Itm_Discount;
                        break;
                }

                double _Value = 0;
                _Value = double.Parse(_Price) - (double.Parse(_Price) * (double.Parse(_Disc) / 100));

                if (_Frozen == "N")
                {
                    oDocLineGeneralData.SetProperty("U_NetAmt", _Value);
                }

            }
            else
            {
                oDocLinesCollection = oDocGeneralData.Child("BPL1");
                oDocLineGeneralData = oDocLinesCollection.Add();
                oDocLineGeneralData.SetProperty("U_ItemCode", _ItemCode);
                oDocLineGeneralData.SetProperty("U_ItemName", _ItemName);
                oDocLineGeneralData.SetProperty("U_Price", _Price);
                oDocLineGeneralData.SetProperty("U_Discount", _Disc);
                oDocLineGeneralData.SetProperty("U_NetAmt", _NetAmt);
            }


            Application.DoEvents();
            _Count++;
            tssDataStatus.Text = "Pricelist Data Reset : (" + _Count + " / " + _RowCount + ") : Data Progress Percentage ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) : Current Item Uploading : " + _ItemName;
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
        }

        #endregion


        //#region Branch Item List Price Update

        //_sqlPriceList = @"SELECT A.Code, B.Code, B.U_Area, A.U_ItemCode, A.U_ItemName
        //                        , A.U_VisMinDisc, A.U_VisMinNet, A.U_VisMinPrice
        //                        , A.U_GMADisc, A.U_GMANet, A.U_GMAPrice 

        //                        , C.U_Discount, C.U_NetAmt, C.U_Price 
        //                        , C.U_Frozen
        //                        FROM [@OIPL] A 
        //                        INNER JOIN [@OBPL] B ON A.Code = B.U_BPPriceList
        //                        INNER JOIN [@BPL1] C ON B.Code = C.Code AND A.U_ItemCode = C.U_ItemCode

        //                        WHERE CASE WHEN B.U_Area = 'GMA' THEN A.U_GMANet ELSE A.U_VisMinNet END <> C.U_NetAmt
        //                            AND A.Code = '" + _PriceList + @"'  AND B.Code = '" + _BranchCode + @"'";


        //_tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);


        //double _RowCount;
        //int _Count = 0;
        //_RowCount = _tblPriceList.Rows.Count;

        //foreach (DataRow r_Itm in _tblPriceList.Rows)
        //{
        //    string _ItemCode = r_Itm["U_ItemCode"].ToString();
        //    string _ItemName = r_Itm["U_ItemName"].ToString();

        //    string _VisMinPrice = r_Itm["U_VisMinPrice"].ToString();
        //    string _VisMinDisc = r_Itm["U_VisMinDisc"].ToString();
        //    string _VisMinNet = r_Itm["U_VisMinNet"].ToString();

        //    string _GMAPrice = r_Itm["U_GMAPrice"].ToString();
        //    string _GMADisc = r_Itm["U_GMADisc"].ToString();
        //    string _GMANet = r_Itm["U_GMANet"].ToString();

        //    string _Price = "";
        //    string _Disc = "";
        //    string _NetAmt = "";
        //    if (_Area == "GMA")
        //    {
        //        _Price = _GMAPrice;
        //        _Disc = _GMADisc;
        //        _NetAmt = _GMANet;
        //    }
        //    else
        //    {
        //        _Price = _VisMinPrice;
        //        _Disc = _VisMinDisc;
        //        _NetAmt = _VisMinNet;
        //    }



        //    string _LineId;
        //    string _Frozen;


        //    string _sqlBPL2 = @"
        //                  SELECT A.LineId, A.U_Frozen FROM [@BPL1] A WHERE A.Code = '" + _BranchCode + @"' AND A.U_ItemCode = '" + _ItemCode + @"'
        //                 ";

        //    DataTable _tblBPL2 = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlBPL2);
        //    _LineId = clsSQLClientFunctions.GetData(_tblBPL2, "LineId", "0");
        //    _Frozen = clsSQLClientFunctions.GetData(_tblBPL2, "U_Frozen", "0");


        //    if (_tblBPL2.Rows.Count > 0)
        //    {

        //        oDocLinesCollection = oDocGeneralData.Child("BPL1");
        //        oDocLineGeneralData = oDocLinesCollection.Item(int.Parse(_LineId) - 1);

        //        //MessageBox.Show(oDocLineGeneralData.GetProperty("U_Category"));
        //        oDocLineGeneralData.SetProperty("U_ItemCode", _ItemCode);
        //        oDocLineGeneralData.SetProperty("U_ItemName", _ItemName);
        //        oDocLineGeneralData.SetProperty("U_Price", _Price);
        //        oDocLineGeneralData.SetProperty("U_Discount", _Disc);

        //        if (_Frozen == "N")
        //        {
        //            oDocLineGeneralData.SetProperty("U_NetAmt", _NetAmt);
        //        }

        //    }
        //    else
        //    {
        //        oDocLinesCollection = oDocGeneralData.Child("BPL1");
        //        oDocLineGeneralData = oDocLinesCollection.Add();
        //        oDocLineGeneralData.SetProperty("U_ItemCode", _ItemCode);
        //        oDocLineGeneralData.SetProperty("U_ItemName", _ItemName);
        //        oDocLineGeneralData.SetProperty("U_Price", _Price);
        //        oDocLineGeneralData.SetProperty("U_Discount", _Disc);
        //        oDocLineGeneralData.SetProperty("U_NetAmt", _NetAmt);
        //    }


        //    Application.DoEvents();
        //    _Count++;
        //    tssDataStatus.Text = "Pricelist Data Reset : (" + _Count + " / " + _RowCount + ") : Data Progress Percentage ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) : Current Item Uploading : " + _ItemName;
        //    pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
        //}

        //#endregion


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


    private void UpdateItemGroupList(string _PriceList = "")
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

        oDocGeneralService = oCompService.GetGeneralService("OCRP");
        oDocGeneralData = (SAPbobsCOM.GeneralData)oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);


        oDocGeneralParams = oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
        oDocGeneralParams.SetProperty("Code", _PriceList);
        oDocGeneralData = oDocGeneralService.GetByParams(oDocGeneralParams);

        double _RowCount;
        int _Count;


        _Count = 0;
        _RowCount = dgvItemGroup.Rows.Count;

        #region Item Group Price Update
        foreach (DataGridViewRow row in dgvItemGroup.Rows)
        {
            string _ItmsGroupCode = row.Cells["Code"].Value.ToString().Trim();
            string _Category = row.Cells["Name"].Value.ToString().Trim();
            string _VISMINDisc = row.Cells["VISMIN DISC %"].Value.ToString().Trim();
            string _GMADisc = row.Cells["GMA DISC %"].Value.ToString().Trim();



            string _sqlOCPL;
            DataTable _tblOCPL = new DataTable();
            _sqlOCPL = @"
                          SELECT A.LineId FROM [@OCPL] A WHERE A.Code = '" + _PriceList.Replace("'", "''") + @"' AND A.U_Category = '" + _Category + @"'
                         ";
            _tblOCPL = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlOCPL);
            string _OCPL_LineId = clsSQLClientFunctions.GetData(_tblOCPL, "LineId", "0");


            if (_tblOCPL.Rows.Count > 0)
            {
                oDocLinesCollection = oDocGeneralData.Child("OCPL");
                oDocLineGeneralData = oDocLinesCollection.Item(int.Parse(_OCPL_LineId) - 1);


                oDocLineGeneralData.SetProperty("U_Category", _Category);
                oDocLineGeneralData.SetProperty("U_VisMinDisc", _VISMINDisc);
                oDocLineGeneralData.SetProperty("U_GMADisc", _GMADisc);
            }
            else
            {

                oDocLinesCollection = oDocGeneralData.Child("OCPL");
                oDocLineGeneralData = oDocLinesCollection.Add();

                oDocLineGeneralData.SetProperty("U_Category", _Category);
                oDocLineGeneralData.SetProperty("U_VisMinDisc", _VISMINDisc);
                oDocLineGeneralData.SetProperty("U_GMADisc", _GMADisc);
            }



            Application.DoEvents();
            _Count++;
            tssDataStatus.Text = "Pricelist Data Reset : (" + _Count + " / " + _RowCount + ") : Data Progress Percentage ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) : Current Item Group Uploading : " + _Category;
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
        }

        #endregion

        #region Item List Price Uploading
        foreach (DataGridViewRow row in dgvItemGroup.Rows)
        {
            string _ItmsGroupCode = row.Cells["Code"].Value.ToString().Trim();
            string _Category = row.Cells["Name"].Value.ToString().Trim();
            string _VISMINDisc = row.Cells["VISMIN DISC %"].Value.ToString().Trim();
            string _GMADisc = row.Cells["GMA DISC %"].Value.ToString().Trim();

            lblStatus1.Text = _Category + " Currently Updating... ";

            string _sqlOIPL;
            DataTable _tblOIPL = new DataTable();

            _sqlOIPL = @"
                               SELECT A.ItemCode, A.ItemName
                               , CASE WHEN B.U_GMAPrice IS NULL THEN 0.00 ELSE B.U_GMAPrice END U_GMAPrice
                               , CASE WHEN B.U_VisMinPrice IS NULL THEN 0.00 ELSE B.U_VisMinPrice END U_VisMinPrice
                               , B.U_ItemCode, B.LineId 
                               FROM OITM A LEFT JOIN [@OIPL] B ON A.ItemCode = B.U_ItemCode AND B.Code =  '" + _PriceList.Replace("'", "''") + @"'
                               WHERE A.ItmsGrpCod = '" + _ItmsGroupCode + @"'
                         ";
            _tblOIPL = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlOIPL);



            _Count = 0;
            _RowCount = _tblOIPL.Rows.Count;

            foreach (DataRow rOIPL in _tblOIPL.Rows)
            {
                string _ItemCode = rOIPL["ItemCode"].ToString();
                string _ItemName = rOIPL["ItemName"].ToString();
                string _PriceList_ItemCode = rOIPL["U_ItemCode"].ToString();

                string _OIPL_LineId = rOIPL["LineId"].ToString();
                string _GMAPrice = rOIPL["U_GMAPrice"].ToString();
                string _VisMinPrice = rOIPL["U_VisMinPrice"].ToString();


                if (_PriceList_ItemCode != "")
                {

                    oDocLinesCollection = oDocGeneralData.Child("OIPL");
                    oDocLineGeneralData = oDocLinesCollection.Item(int.Parse(_OIPL_LineId) - 1);

                    oDocLineGeneralData.SetProperty("U_ItemCode", _ItemCode);
                    oDocLineGeneralData.SetProperty("U_ItemName", _ItemName);
                    oDocLineGeneralData.SetProperty("U_VisMinPrice", _VisMinPrice);
                    oDocLineGeneralData.SetProperty("U_GMAPrice", _GMAPrice);


                    oDocLineGeneralData.SetProperty("U_VisMinDisc", _VISMINDisc);
                    oDocLineGeneralData.SetProperty("U_GMADisc", _GMADisc);

                    double _Value = 0;
                    _Value = double.Parse(_VisMinPrice) - (double.Parse(_VisMinPrice) * (double.Parse(_VISMINDisc) / 100));
                    oDocLineGeneralData.SetProperty("U_VisMinNet", _Value);
                    _Value = double.Parse(_GMAPrice) - (double.Parse(_GMAPrice) * (double.Parse(_GMADisc) / 100));
                    oDocLineGeneralData.SetProperty("U_GMANet", _Value);

                }
                else
                {
                    oDocLinesCollection = oDocGeneralData.Child("OIPL");
                    oDocLineGeneralData = oDocLinesCollection.Add();
                    oDocLineGeneralData.SetProperty("U_ItemCode", _ItemCode);
                    oDocLineGeneralData.SetProperty("U_ItemName", _ItemName);
                    double _Value = 0;
                    oDocLineGeneralData.SetProperty("U_VisMinPrice", _Value);
                    oDocLineGeneralData.SetProperty("U_GMAPrice", _Value);


                    oDocLineGeneralData.SetProperty("U_VisMinDisc", _VISMINDisc);
                    oDocLineGeneralData.SetProperty("U_GMADisc", _GMADisc);


                    _Value = double.Parse("0.00") - (double.Parse("0.00") * (double.Parse(_VISMINDisc) / 100));
                    oDocLineGeneralData.SetProperty("U_VisMinNet", _Value);
                    _Value = double.Parse("0.00") - (double.Parse("0.00") * (double.Parse(_GMADisc) / 100));
                    oDocLineGeneralData.SetProperty("U_GMANet", _Value);
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

    private void txtItemSearch_TextChanged(object sender, EventArgs e)
    {
        String someText;
        someText = txtItemSearch.Text;

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

    private void btnItemListSave_Click(object sender, EventArgs e)
    {
        if (txtPriceList.Text == "")
        {
            MessageBox.Show("Pricelist Not Define");
            return;
        }

        if (_ExcelUploading == false)
        {
            UpdateItemList(txtPriceList.Text);
        }
        else
        {
            UpdateExcelItemList(txtPriceList.Text);
        }



        string _sqlPriceList = @"
                                        SELECT A.WhsCode AS Code, A.WhsName AS Name, B.Location AS Area, A.U_PriceList AS PriceList
                                        FROM OWHS A 
                                        LEFT OUTER JOIN OLCT B ON A.Location = B.Code
										WHERE ISNULL(A.U_PriceList,'') = '" + txtPriceList.Text + @"' AND ISNULL(B.Location,'') <> ''
                                        AND A.WhsCode IN (
							 SELECT DISTINCT B.Code
                                FROM [@OIPL] A 
                                INNER JOIN [@OBPL] B ON A.Code = B.U_BPPriceList
                                LEFT JOIN [@BPL1] C ON B.Code = C.Code AND A.U_ItemCode = C.U_ItemCode

                                WHERE CASE WHEN B.U_Area = 'GMA' THEN A.U_GMANet ELSE A.U_VisMinNet END <> ISNULL(C.U_NetAmt, 0)
						   AND A.Code = '" + txtPriceList.Text + @"'
                                                         )
                                    ";

        DataTable _tblPriceList = new DataTable();
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);


        double _Count = 0;
        double _RowCount = _tblPriceList.Rows.Count;


        foreach (DataRow rowB in _tblPriceList.Rows)
        {
            string _Code = rowB["Code"].ToString();
            string _Name = rowB["Name"].ToString();
            string _Area = rowB["Area"].ToString();
            string _BPPricelist = rowB["PriceList"].ToString();

            UpdateBranchItem(_BPPricelist, _Code, _Area);


            Application.DoEvents();
            _Count++;
            lblStatus1.Text = "Pricelist Data Reset : (" + _Count + " / " + _RowCount + ") : Data Progress Percentage ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) : Current Price List Uploading " + _BPPricelist + " : Branch :" + _Name;
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
        }


        MessageBox.Show("Price List Successfully Updated");
        lblRefresh_Click(sender, e);
    }


    private void UpdateExcelItemList(string _PriceList = "")
    {
        double _Value = 0;
        double _Price = 0;

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
        oDocGeneralService = oCompService.GetGeneralService("OCRP");
        // Point to the Header of the Doc UDO
        oDocGeneralData = (SAPbobsCOM.GeneralData)oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);


        oDocGeneralParams = oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
        oDocGeneralParams.SetProperty("Code", _PriceList);
        oDocGeneralData = oDocGeneralService.GetByParams(oDocGeneralParams);


        foreach (DataGridViewRow row in dgvItemList.Rows)
        {
            string _Code = row.Cells["ITEMCODE"].Value.ToString().Trim();
            string _GMAPrice = row.Cells["GMA PRICE"].Value.ToString().Trim();
            string _VISMINPrice = row.Cells["VISMIN PRICE"].Value.ToString().Trim();

            string _sqlOCPL;
            DataTable _tblOCPL = new DataTable();
            _sqlOCPL = @"
                          SELECT A.LineId, A.U_VisMinDisc, A.U_GMADisc FROM [@OIPL] A WHERE A.Code = '" + _PriceList + @"' AND A.U_ItemCode = '" + _Code + @"'
                         ";
            _tblOCPL = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlOCPL);
            string _LineId = clsSQLClientFunctions.GetData(_tblOCPL, "LineId", "0");
            string _VisMinDisc = clsSQLClientFunctions.GetData(_tblOCPL, "U_VisMinDisc", "0");
            string _GMADisc = clsSQLClientFunctions.GetData(_tblOCPL, "U_GMADisc", "0");

            if ((double.Parse(_GMAPrice) + double.Parse(_VISMINPrice)) != 0)
            {

                if (_LineId != "")
                {

                    oDocLinesCollection = oDocGeneralData.Child("OIPL");
                    oDocLineGeneralData = oDocLinesCollection.Item(int.Parse(_LineId) - 1);

                    //MessageBox.Show(oDocLineGeneralData.GetProperty("U_Category"));
                    oDocLineGeneralData.SetProperty("U_ItemCode", _Code);
                    oDocLineGeneralData.SetProperty("U_VisMinPrice", _VISMINPrice);
                    oDocLineGeneralData.SetProperty("U_GMAPrice", _GMAPrice);


                    _Price = double.Parse(_VISMINPrice);
                    _Value = _Price - (_Price * (double.Parse(_VisMinDisc) / 100));
                    oDocLineGeneralData.SetProperty("U_VisMinDisc", _VisMinDisc);
                    oDocLineGeneralData.SetProperty("U_VisMinNet", _Value);

                    _Price = double.Parse(_GMAPrice);
                    _Value = _Price - (_Price * (double.Parse(_GMADisc) / 100));
                    oDocLineGeneralData.SetProperty("U_GMADisc", _GMADisc);
                    oDocLineGeneralData.SetProperty("U_GMANet", _Value);

                }
                else
                {
                    oDocLinesCollection = oDocGeneralData.Child("OIPL");
                    oDocLineGeneralData = oDocLinesCollection.Add();
                    oDocLineGeneralData.SetProperty("U_ItemCode", _Code);
                    oDocLineGeneralData.SetProperty("U_VisMinPrice", _VISMINPrice);
                    oDocLineGeneralData.SetProperty("U_GMAPrice", _GMAPrice);


                    _Price = double.Parse(_VISMINPrice);
                    _Value = _Price - (_Price * (double.Parse(_VisMinDisc) / 100));
                    oDocLineGeneralData.SetProperty("U_VisMinDisc", _VisMinDisc);
                    oDocLineGeneralData.SetProperty("U_VisMinNet", _Value);

                    _Price = double.Parse(_GMAPrice);
                    _Value = _Price - (_Price * (double.Parse(_GMADisc) / 100));
                    oDocLineGeneralData.SetProperty("U_GMADisc", _GMADisc);
                    oDocLineGeneralData.SetProperty("U_GMANet", _Value);
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
        }
        else
        {

        }
    }



    private void UpdateItemList(string _PriceList = "")
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
        oDocGeneralService = oCompService.GetGeneralService("OCRP");
        // Point to the Header of the Doc UDO
        oDocGeneralData = (SAPbobsCOM.GeneralData)oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);


        oDocGeneralParams = oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
        oDocGeneralParams.SetProperty("Code", _PriceList);
        oDocGeneralData = oDocGeneralService.GetByParams(oDocGeneralParams);

        double _RowCount;
        int _Count;

        _Count = 0;
        _RowCount = dgvItemList.Rows.Count;
        foreach (DataGridViewRow row in dgvItemList.Rows)
        {
            string _Code = row.Cells["Code"].Value.ToString().Trim();
            string _Name = row.Cells["Name"].Value.ToString().Trim();

            string _GMAPrice = row.Cells["GMA Price"].Value.ToString().Trim();
            string _GMADisc = row.Cells["GMA Disc"].Value.ToString().Trim();
            string _GMANetAmt = row.Cells["GMA New Net Amount"].Value.ToString().Trim();

            string _VISMINPrice = row.Cells["VISMIN Price"].Value.ToString().Trim();
            string _VISMINDisc = row.Cells["VISMIN Disc"].Value.ToString().Trim();
            string _VISMINNetAmt = row.Cells["VISMIN New Net Amount"].Value.ToString().Trim();


            string _sqlOCPL;
            DataTable _tblOCPL = new DataTable();
            _sqlOCPL = @"
                          SELECT A.LineId FROM [@OIPL] A WHERE A.Code = '" + _PriceList + @"' AND A.U_ItemCode = '" + _Code + @"'
                         ";
            _tblOCPL = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlOCPL);
            string _LineId = clsSQLClientFunctions.GetData(_tblOCPL, "LineId", "0");


            if (_LineId != "")
            {

                oDocLinesCollection = oDocGeneralData.Child("OIPL");
                oDocLineGeneralData = oDocLinesCollection.Item(int.Parse(_LineId) - 1);

                //MessageBox.Show(oDocLineGeneralData.GetProperty("U_Category"));
                oDocLineGeneralData.SetProperty("U_ItemCode", _Code);
                oDocLineGeneralData.SetProperty("U_ItemName", _Name);

                oDocLineGeneralData.SetProperty("U_GMAPrice", _GMAPrice);
                oDocLineGeneralData.SetProperty("U_GMADisc", _GMADisc);
                oDocLineGeneralData.SetProperty("U_GMANet", _GMANetAmt);

                oDocLineGeneralData.SetProperty("U_VisMinPrice", _VISMINPrice);
                oDocLineGeneralData.SetProperty("U_VisMinDisc", _VISMINDisc);
                oDocLineGeneralData.SetProperty("U_VisMinNet", _VISMINNetAmt);

            }
            else
            {
                oDocLinesCollection = oDocGeneralData.Child("OIPL");
                oDocLineGeneralData = oDocLinesCollection.Add();
                oDocLineGeneralData.SetProperty("U_ItemCode", _Code);
                oDocLineGeneralData.SetProperty("U_ItemName", _Name);

                oDocLineGeneralData.SetProperty("U_GMAPrice", _GMAPrice);
                oDocLineGeneralData.SetProperty("U_GMADisc", _GMADisc);
                oDocLineGeneralData.SetProperty("U_GMANet", _GMANetAmt);

                oDocLineGeneralData.SetProperty("U_VisMinPrice", _VISMINPrice);
                oDocLineGeneralData.SetProperty("U_VisMinDisc", _VISMINDisc);
                oDocLineGeneralData.SetProperty("U_VisMinNet", _VISMINNetAmt);
            }



            Application.DoEvents();
            _Count++;
            lblStatus1.Text = "Item Data Update";
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
        cmbWorkSheet.Items.Clear();
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

    private void btnGenerate_Click(object sender, EventArgs e)
    {
        if (txtExcelFile.Text == "")
        {
            MessageBox.Show("Please define Excel File Before Generating Data");
            return;
        }

        _ExcelUploading = true;
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
        clsFunctions.DataGridViewSetup(dgvItemList, _DataTable);
    }
}