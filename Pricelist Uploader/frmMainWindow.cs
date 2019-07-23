using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmMainWindow : Form
{
    public frmMainWindow()
    {
        InitializeComponent();
    }

    private void frmMainWindow_Load(object sender, EventArgs e)
    {
        frmLogin frmLogin = new frmLogin();
        frmLogin.ShowDialog();
        panel1.Visible = false;
    }

    private void priceListToolStripMenuItem_Click(object sender, EventArgs e)
    {
        frmMainPriceList frmMainPriceList = new frmMainPriceList();
        frmMainPriceList.MdiParent = this;
        frmMainPriceList.Show();
    }

    private void priceListPerBranchToolStripMenuItem_Click(object sender, EventArgs e)
    {
        frmBranchPriceList frmBranchPriceList = new frmBranchPriceList();
        frmBranchPriceList.MdiParent = this;
        frmBranchPriceList.Show();
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        frmSystemConfig frmSystemConfig = new frmSystemConfig();
        frmSystemConfig.MdiParent = this;
        frmSystemConfig.Show();
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        tssDataStatus.Text = "";
        //clsSAPFunctions.UpdatePriceList(txtPriceList.Text);
        string _sqlPriceList = @"
                                    SELECT A.Code FROM [@OCRP] A 
                                    ";

        DataTable _tblPriceList = new DataTable();
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);


        double _MRowCount;
        int _MCount = 0;
        _MRowCount = _tblPriceList.Rows.Count;


        foreach (DataRow row in _tblPriceList.Rows)
        {
            string _PriceList = row["Code"].ToString();
            UpdateItemGroupList(_PriceList);



            string _sqlSelect = @"
                                        SELECT A.WhsCode AS Code, A.WhsName AS Name, B.Location AS Area, A.U_PriceList AS PriceList
                                        FROM OWHS A 
                                        LEFT OUTER JOIN OLCT B ON A.Location = B.Code
										WHERE ISNULL(A.U_PriceList,'') = '" + _PriceList.Replace("'", "''") + @"' AND ISNULL(B.Location,'') <> ''
                                        AND A.WhsCode IN (
                                                            SELECT DISTINCT B.Code
                                                            FROM [@OIPL] A 
                                                            LEFT JOIN [@OBPL] B ON A.Code = B.U_BPPriceList 
                                                            LEFT JOIN [@BPL1] C ON B.Code = C.Code AND A.U_ItemCode = C.U_ItemCode

                                                            WHERE  A.Code =  '" + _PriceList.Replace("'", "''") + @"'
                                                                AND CASE WHEN C.U_Frozen IS NULL THEN 'N' ELSE C.U_Frozen END = 'N'
									                            AND C.U_ItemCode IS NULL
                                                         )
                                    ";

            DataTable _tblSelect = new DataTable();
            _tblSelect = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlSelect);



            double _RowCount;
            int _Count = 0;
            _RowCount = _tblSelect.Rows.Count;

            foreach (DataRow rSelect in _tblSelect.Rows)
            {
                string _Code = rSelect["Code"].ToString();
                string _Name = rSelect["Name"].ToString();
                string _Area = rSelect["Area"].ToString();
                string _BPPricelist = rSelect["PriceList"].ToString();

                UpdateBranch(_BPPricelist, _Code, _Area);


                Application.DoEvents();
                _Count++;
                tssDataStatus.Text = "Pricelist Data Reset : (" + _Count + " / " + _RowCount + ") : Data Progress Percentage ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) : Current Price List Uploading " + _BPPricelist + " : Branch :" + _Name;
                pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
            }



            Application.DoEvents();
            _MCount++;
            lblStatus1.Text = "Pricelist Data Reset : (" + _MCount + " / " + _MRowCount + ") : Data Progress Percentage ( " + Math.Round(((_MCount / _MRowCount) * 100), 2) + " % ) : Current Price List Uploading " + _PriceList;
            pbDataProgress.Value = Convert.ToInt32(((_MCount / _MRowCount) * 100));

        }

        //UpdateItemGroupList();

        MessageBox.Show("Price List Successfully Updated");
        panel1.Visible = false;
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
                            , C.U_Discount, C.U_Frozen FROM [@OCPL] A 
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
                          SELECT A.LineId, A.U_Frozen FROM [@BPL2] A WHERE A.Code = '" + _BranchCode + @"' AND A.U_Category = '" + _Category + @"'
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

        _sqlPriceList = @"SELECT A.Code, B.Code, B.U_Area, A.U_ItemCode, A.U_ItemName, E.ItmsGrpNam, F.U_Frozen AS [C_Frozen]
                                , A.U_VisMinDisc, A.U_VisMinNet, A.U_VisMinPrice
                                , A.U_GMADisc, A.U_GMANet, A.U_GMAPrice 
                                , CASE WHEN B.U_Area = 'GMA' THEN A.U_GMANet ELSE A.U_VisMinNet END AS [Main Net]
                                , C.U_Discount, C.U_NetAmt, C.U_Price 
                                , C.U_Frozen
                                FROM [@OIPL] A 
                                INNER JOIN [@OBPL] B ON A.Code = B.U_BPPriceList
                                LEFT JOIN [@BPL1] C ON B.Code = C.Code AND A.U_ItemCode = C.U_ItemCode
								LEFT JOIN [OITM] D ON D.ItemCode = A.U_ItemCode
								LEFT JOIN [OITB] E ON D.ItmsGrpCod = E.ItmsGrpCod
								LEFT JOIN [@BPL2] F ON E.ItmsGrpNam = F.U_Category AND F.Code = B.Code

                                WHERE CASE WHEN B.U_Area = 'GMA' THEN A.U_GMANet ELSE A.U_VisMinNet END <> C.U_NetAmt
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
                          SELECT A.LineId, ISNULL(A.U_Frozen, 'N'), A.U_Discount, A.U_NetAmt FROM [@BPL1] A WHERE A.Code = '" + _BranchCode + @"' AND A.U_ItemCode = '" + _ItemCode + @"'
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

    //private void UpdateBranch(string _PriceList, string _BranchCode, string _Area)
    //{
    //    SAPbobsCOM.Company vComp;
    //    vComp = clsSAPFunctions.oCompany;

    //    SAPbobsCOM.GeneralService oDocGeneralService;
    //    SAPbobsCOM.GeneralData oDocGeneralData;
    //    SAPbobsCOM.GeneralDataCollection oDocLinesCollection;
    //    SAPbobsCOM.GeneralData oDocLineGeneralData;
    //    SAPbobsCOM.GeneralDataParams oDocGeneralParams;

    //    SAPbobsCOM.CompanyService oCompService;
    //    oCompService = vComp.GetCompanyService();

    //    // Retrieve the relevant service
    //    oDocGeneralService = oCompService.GetGeneralService("OBPL");
    //    // Point to the Header of the Doc UDO
    //    oDocGeneralData = (SAPbobsCOM.GeneralData)oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);


    //    oDocGeneralParams = oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
    //    oDocGeneralParams.SetProperty("Code", _BranchCode);
    //    oDocGeneralData = oDocGeneralService.GetByParams(oDocGeneralParams);


    //    string _sqlPriceList;
    //    DataTable _tblPriceList = new DataTable();




    //    #region Branch Item Group Price Update


    //    _sqlPriceList = @"
    //                        SELECT A.Code, A.U_Category, B.Code, B.U_Area, A.U_VisMinDisc, A.U_GMADisc
    //                        , CASE WHEN B.U_Area = 'GMA' THEN A.U_GMADisc ELSE A.U_VisMinDisc END AS [Main Discs]
    //                        , C.U_Discount, C.U_Frozen FROM [@OCPL] A 
    //                        LEFT JOIN [@OBPL] B ON A.Code = B.U_BPPriceList
    //                        LEFT JOIN [@BPL2] C ON B.Code = C.Code AND A.U_Category = C.U_Category
    //                        WHERE CASE WHEN B.U_Area = 'GMA' THEN A.U_GMADisc ELSE A.U_VisMinDisc END <> C.U_Discount
    //                        AND A.Code = '" + _PriceList + @"' AND B.Code = '" + _BranchCode + @"'
    //                        AND CASE WHEN C.U_Frozen IS NULL THEN 'N' ELSE C.U_Frozen END = 'N'               
    //                     ";

    //    _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);

    //    foreach (DataRow r_ItmGrp in _tblPriceList.Rows)
    //    {
    //        string _Category = r_ItmGrp["U_Category"].ToString();
    //        string _VISMINDisc = r_ItmGrp["U_VisMinDisc"].ToString();
    //        string _GMADisc = r_ItmGrp["U_GMADisc"].ToString();

    //        string _Discount = "";
    //        if (_Area == "GMA")
    //        {
    //            _Discount = _GMADisc;
    //        }
    //        else
    //        {
    //            _Discount = _VISMINDisc;
    //        }



    //        string _LineId;
    //        string _Frozen;


    //        string _sqlBPL2 = @"
    //                      SELECT A.LineId, A.U_Frozen FROM [@BPL2] A WHERE A.Code = '" + _BranchCode + @"' AND A.U_Category = '" + _Category + @"'
    //                     ";

    //        DataTable _tblBPL2 = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlBPL2);
    //        _LineId = clsSQLClientFunctions.GetData(_tblBPL2, "LineId", "0");
    //        _Frozen = clsSQLClientFunctions.GetData(_tblBPL2, "U_Frozen", "0");

    //        if (_tblBPL2.Rows.Count > 0)
    //        {
    //            oDocLinesCollection = oDocGeneralData.Child("BPL2");
    //            oDocLineGeneralData = oDocLinesCollection.Item(int.Parse(_LineId) - 1);

    //            oDocLineGeneralData.SetProperty("U_Category", _Category);

    //            if (_Frozen == "N")
    //            {
    //                oDocLineGeneralData.SetProperty("U_Discount", _Discount);
    //            }

    //        }
    //        else
    //        {
    //            oDocLinesCollection = oDocGeneralData.Child("BPL2");
    //            oDocLineGeneralData = oDocLinesCollection.Add();

    //            oDocLineGeneralData.SetProperty("U_Category", _Category);
    //            oDocLineGeneralData.SetProperty("U_Discount", _Discount);
    //        }

    //    }
    //    #endregion

    //    #region Branch Item List Price Update

    //    _sqlPriceList = @"SELECT A.Code, B.Code, B.U_Area, A.U_ItemCode, A.U_ItemName
    //                            , A.U_VisMinDisc, A.U_VisMinNet, A.U_VisMinPrice
    //                            , A.U_GMADisc, A.U_GMANet, A.U_GMAPrice 
    //                            , CASE WHEN B.U_Area = 'GMA' THEN A.U_GMANet ELSE A.U_VisMinNet END AS [Main Net]
    //                            , C.U_Discount, C.U_NetAmt, C.U_Price 
    //                            , C.U_Frozen
    //                            FROM [@OIPL] A 
    //                            LEFT JOIN [@OBPL] B ON A.Code = B.U_BPPriceList
    //                            LEFT JOIN [@BPL1] C ON B.Code = C.Code AND A.U_ItemCode = C.U_ItemCode

    //                            WHERE A.Code = '" + _PriceList + @"'  AND B.Code = '" + _BranchCode + @"' 
    //                                AND CASE WHEN C.U_Frozen IS NULL THEN 'N' ELSE C.U_Frozen END = 'N'
    //					AND C.U_ItemCode IS NULL
    //                      ";


    //    _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);


    //    double _RowCount;
    //    int _Count = 0;
    //    _RowCount = _tblPriceList.Rows.Count;

    //    foreach (DataRow r_Itm in _tblPriceList.Rows)
    //    {
    //        string _ItemCode = r_Itm["U_ItemCode"].ToString();
    //        string _ItemName = r_Itm["U_ItemName"].ToString();

    //        string _VisMinPrice = r_Itm["U_VisMinPrice"].ToString();
    //        string _VisMinDisc = r_Itm["U_VisMinDisc"].ToString();
    //        string _VisMinNet = r_Itm["U_VisMinNet"].ToString();

    //        string _GMAPrice = r_Itm["U_GMAPrice"].ToString();
    //        string _GMADisc = r_Itm["U_GMADisc"].ToString();
    //        string _GMANet = r_Itm["U_GMANet"].ToString();

    //        string _Price = "";
    //        string _Disc = "";
    //        string _NetAmt = "";
    //        if (_Area == "GMA")
    //        {
    //            _Price = _GMAPrice;
    //            _Disc = _GMADisc;
    //            _NetAmt = _GMANet;
    //        }
    //        else
    //        {
    //            _Price = _VisMinPrice;
    //            _Disc = _VisMinDisc;
    //            _NetAmt = _VisMinNet;
    //        }



    //        string _LineId;
    //        string _Frozen;


    //        string _sqlBPL2 = @"
    //                      SELECT A.LineId, A.U_Frozen FROM [@BPL1] A WHERE A.Code = '" + _BranchCode + @"' AND A.U_ItemCode = '" + _ItemCode + @"'
    //                     ";

    //        DataTable _tblBPL2 = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlBPL2);
    //        _LineId = clsSQLClientFunctions.GetData(_tblBPL2, "LineId", "0");
    //        _Frozen = clsSQLClientFunctions.GetData(_tblBPL2, "U_Frozen", "0");


    //        if (_tblBPL2.Rows.Count > 0)
    //        {

    //            oDocLinesCollection = oDocGeneralData.Child("BPL1");
    //            oDocLineGeneralData = oDocLinesCollection.Item(int.Parse(_LineId) - 1);

    //            //MessageBox.Show(oDocLineGeneralData.GetProperty("U_Category"));
    //            oDocLineGeneralData.SetProperty("U_ItemCode", _ItemCode);
    //            oDocLineGeneralData.SetProperty("U_ItemName", _ItemName);
    //            oDocLineGeneralData.SetProperty("U_Price", _Price);
    //            oDocLineGeneralData.SetProperty("U_Discount", _Disc);

    //            if (_Frozen == "N")
    //            {
    //                oDocLineGeneralData.SetProperty("U_NetAmt", _NetAmt);
    //            }

    //        }
    //        else
    //        {
    //            oDocLinesCollection = oDocGeneralData.Child("BPL1");
    //            oDocLineGeneralData = oDocLinesCollection.Add();
    //            oDocLineGeneralData.SetProperty("U_ItemCode", _ItemCode);
    //            oDocLineGeneralData.SetProperty("U_ItemName", _ItemName);
    //            oDocLineGeneralData.SetProperty("U_Price", _Price);
    //            oDocLineGeneralData.SetProperty("U_Discount", _Disc);
    //            oDocLineGeneralData.SetProperty("U_NetAmt", _NetAmt);
    //        }


    //        Application.DoEvents();
    //        _Count++;
    //        tssDataStatus.Text = "Pricelist Data Reset : (" + _Count + " / " + _RowCount + ") : Data Progress Percentage ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) : Current Item Uploading : " + _ItemName;
    //        pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
    //    }

    //    #endregion


    //    // Insert Values to the Lines properties try
    //    try
    //    {
    //        oDocGeneralService.Update(oDocGeneralData);
    //        //MessageBox.Show("Price List Successfully Updated");
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(ex.Message);
    //    }


    //    if (vComp.InTransaction)
    //    {
    //        vComp.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
    //        //MessageBox.Show("Price List Successfully Updated");
    //        //SBO_Application.MessageBox("Add Doc UDO Header and Lines successfully", 1, "", "", "");
    //    }
    //    else
    //    {
    //        //MessageBox.Show("Error in Updating Price List");
    //        //SBO_Application.MessageBox("Error adding object", 1, "", "", "");
    //    }
    //}


    //private void UpdateItemGroupList(string _PriceList = "")
    //{
    //    SAPbobsCOM.Company vComp;
    //    vComp = clsSAPFunctions.oCompany;

    //    SAPbobsCOM.GeneralService oDocGeneralService;
    //    SAPbobsCOM.GeneralData oDocGeneralData;
    //    SAPbobsCOM.GeneralDataCollection oDocLinesCollection;
    //    SAPbobsCOM.GeneralData oDocLineGeneralData;
    //    SAPbobsCOM.GeneralDataParams oDocGeneralParams;

    //    SAPbobsCOM.CompanyService oCompService;
    //    oCompService = vComp.GetCompanyService();

    //    oDocGeneralService = oCompService.GetGeneralService("OCRP");
    //    oDocGeneralData = (SAPbobsCOM.GeneralData)oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);


    //    oDocGeneralParams = oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
    //    oDocGeneralParams.SetProperty("Code", _PriceList);
    //    oDocGeneralData = oDocGeneralService.GetByParams(oDocGeneralParams);

    //    double _RowCount;
    //    int _Count;


    //    #region Item List Price Uploading
    //    string _sqlOIPL;
    //    DataTable _tblOIPL = new DataTable();

    //    _sqlOIPL = @"


    //                SELECT A.ItemCode,A.ItemName, B.U_ItemCode , C.U_GMADisc, C.U_VisMinDisc
    //                FROM OITM A INNER JOIN OITB A1 ON A.ItmsGrpCod = A1.ItmsGrpCod
    //                LEFT JOIN [@OCPL] C ON A1.ItmsGrpNam = C.U_Category AND C.Code = '" + _PriceList.Replace("'", "''") + @"'
    //                LEFT JOIN [@OIPL] B ON A.ItemCode = B.U_ItemCode AND B.Code = '" + _PriceList.Replace("'", "''") + @"'
    //                WHERE B.U_ItemCode IS NULL 

    //                     ";
    //    _tblOIPL = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlOIPL);



    //    _Count = 0;
    //    _RowCount = _tblOIPL.Rows.Count;

    //    foreach (DataRow rOIPL in _tblOIPL.Rows)
    //    {
    //        string _ItemCode = rOIPL["ItemCode"].ToString();
    //        string _ItemName = rOIPL["ItemName"].ToString();
    //        string _PriceList_ItemCode = rOIPL["U_ItemCode"].ToString();

    //        //string _OIPL_LineId = rOIPL["LineId"].ToString();

    //        string _GMADisc = rOIPL["U_GMADisc"].ToString();
    //        string _VISMINDisc = rOIPL["U_VisMinDisc"].ToString();



    //        oDocLinesCollection = oDocGeneralData.Child("OIPL");
    //        oDocLineGeneralData = oDocLinesCollection.Add();
    //        oDocLineGeneralData.SetProperty("U_ItemCode", _ItemCode);
    //        oDocLineGeneralData.SetProperty("U_ItemName", _ItemName);
    //        double _Value = 0;
    //        oDocLineGeneralData.SetProperty("U_VisMinPrice", _Value);
    //        oDocLineGeneralData.SetProperty("U_GMAPrice", _Value);


    //        oDocLineGeneralData.SetProperty("U_VisMinDisc", _VISMINDisc);
    //        oDocLineGeneralData.SetProperty("U_GMADisc", _GMADisc);


    //        _Value = double.Parse("0.00") - (double.Parse("0.00") * (double.Parse(_VISMINDisc) / 100));
    //        oDocLineGeneralData.SetProperty("U_VisMinNet", _Value);
    //        _Value = double.Parse("0.00") - (double.Parse("0.00") * (double.Parse(_GMADisc) / 100));
    //        oDocLineGeneralData.SetProperty("U_GMANet", _Value);




    //        Application.DoEvents();
    //        _Count++;
    //        tssDataStatus.Text = "Pricelist Data Reset : (" + _Count + " / " + _RowCount + ") : Data Progress Percentage ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) : Current Item Uploading : " + _ItemName;
    //        pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
    //    }
    //    #endregion






    //    // Insert Values to the Lines properties try
    //    try
    //    {
    //        oDocGeneralService.Update(oDocGeneralData);
    //        //MessageBox.Show("Price List Successfully Updated");
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(ex.Message);
    //    }


    //    if (vComp.InTransaction)
    //    {
    //        vComp.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
    //        //MessageBox.Show("Price List Successfully Updated");
    //        //SBO_Application.MessageBox("Add Doc UDO Header and Lines successfully", 1, "", "", "");
    //    }
    //    else
    //    {
    //        //MessageBox.Show("Error in Updating Price List");
    //        //SBO_Application.MessageBox("Error adding object", 1, "", "", "");
    //    }
    //}





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




        string _sqlPriceList = @"
                                    SELECT A.ItmsGrpCod AS Code, A.ItmsGrpNam AS Name
                                    , CASE WHEN B.U_VisMinDisc IS NULL THEN 0 ELSE B.U_VisMinDisc END AS [VISMIN DISC %]
                                    , CASE WHEN B.U_GMADisc IS NULL THEN 0 ELSE B.U_GMADisc END AS [GMA DISC %]
                                    FROM OITB A
                                    LEFT OUTER JOIN [@OCPL] B ON A.ItmsGrpNam = B.U_Category AND B.Code = '" + _PriceList.Replace("'", "''") + @"'
                                    ";

        DataTable _tblPriceList = new DataTable();
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);



        _Count = 0;
        _RowCount = _tblPriceList.Rows.Count;

        #region Item Group Price Update


        foreach (DataRow row in _tblPriceList.Rows)
        {
            string _ItmsGroupCode = row["Code"].ToString();
            string _Category = row["Name"].ToString();
            string _VISMINDisc = row["VISMIN DISC %"].ToString();
            string _GMADisc = row["GMA DISC %"].ToString();


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
            tssDataStatus.Text = "Pricelist Data Reset : (" + _Count + " / " + _RowCount + ") : Data Progress Percentage ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) : Current Pricelist : " + _PriceList + @" : Current Item Group Uploading : " + _Category;
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
        }

        #endregion

        #region Item List Price Uploading
        foreach (DataRow row in _tblPriceList.Rows)
        {
            string _ItmsGroupCode = row["Code"].ToString();
            string _Category = row["Name"].ToString();
            string _VISMINDisc = row["VISMIN DISC %"].ToString();
            string _GMADisc = row["GMA DISC %"].ToString();

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
                tssDataStatus.Text = "Pricelist Data Reset : (" + _Count + " / " + _RowCount + ") : Data Progress Percentage ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) : Current Pricelist : " + _PriceList + @" : Current Item Uploading : " + _ItemName;
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
    private void refreshPricelistToolStripMenuItem_Click(object sender, EventArgs e)
    {

        panel1.Visible = true;
        btnRefresh_Click(sender, e);

    }

    private void button1_Click(object sender, EventArgs e)
    {
        //frmItemList frmItemList = new frmItemList();
        //frmItemList.MdiParent = this;
        //frmItemList.Show();
        panel1.Visible = true;
        tssDataStatus.Text = "";
        //clsSAPFunctions.UpdatePriceList(txtPriceList.Text);

        string _sqlPriceList;
        DataTable _tblPriceList = new DataTable();
        double _MRowCount;
        int _MCount;


        _sqlPriceList = @"
                                    SELECT A.Code FROM [NXTRD_UPLOADING].dbo.[@OCRP] A 
                                    ";
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);


        _MCount = 0;
        _MRowCount = _tblPriceList.Rows.Count;


        foreach (DataRow row in _tblPriceList.Rows)
        {
            string _PriceList = row["Code"].ToString();

            clsSAPFunctions.CreatePriceList(_PriceList, _PriceList);

            Application.DoEvents();
            _MCount++;
            lblStatus1.Text = "Pricelist Data Reset : (" + _MCount + " / " + _MRowCount + ") : Data Progress Percentage ( " + Math.Round(((_MCount / _MRowCount) * 100), 2) + " % ) : Current Price List Uploading " + _PriceList;
            pbDataProgress.Value = Convert.ToInt32(((_MCount / _MRowCount) * 100));

        }



        _sqlPriceList = @"
         SELECT A.WhsCode AS Code, A.WhsName AS Name,B.Location AS Area, A.U_PriceList AS PriceList
                                        FROM OWHS A 
                                        LEFT OUTER JOIN OLCT B ON A.Location = B.Code
        		WHERE ISNULL(A.U_PriceList,'') <> '' AND ISNULL(B.Location,'') <> ''
                                    ";

        _tblPriceList = new DataTable();
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);



        _MCount = 0;
        _MRowCount = _tblPriceList.Rows.Count;


        foreach (DataRow row in _tblPriceList.Rows)
        {
            string _Code = row["Code"].ToString();
            string _Name = row["Name"].ToString();
            string _Area = row["Area"].ToString();
            string _BPPricelist = row["PriceList"].ToString();

            BranchPriceList(_Code, _Name, _Area, _BPPricelist);

            Application.DoEvents();
            _MCount++;
            lblStatus1.Text = "Pricelist Data Reset : (" + _MCount + " / " + _MRowCount + ") : Data Progress Percentage ( " + Math.Round(((_MCount / _MRowCount) * 100), 2) + " % ) : Current Price List Uploading " + _BPPricelist;
            pbDataProgress.Value = Convert.ToInt32(((_MCount / _MRowCount) * 100));

        }

        //UpdateItemGroupList();

        MessageBox.Show("Price list Successfully Reset");
        panel1.Visible = false;
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

    private void button2_Click(object sender, EventArgs e)
    {
        frmBranchList frmBranchList = new frmBranchList();
        frmBranchList.MdiParent = this;
        frmBranchList.Show();
    }
}