using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class frmItemList : Form
{
    DataTable _ItemGroupList = new DataTable();
    static int _HeaderColumn;

    static string _sItemGroupCode;


    public frmItemList()
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

    private void frmItemList_Load(object sender, EventArgs e)
    {
        btnCreate.Visible = false;
        string _sqlPriceList = @"SELECT A.Code FROM [@OCRP] A";
        DataTable _tblPriceList = new DataTable();
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);

        txtPriceList.AutoCompleteCustomSource = LoadAutoComplete(_tblPriceList, 0);
        txtPriceList.AutoCompleteMode = AutoCompleteMode.Suggest;
        txtPriceList.AutoCompleteSource = AutoCompleteSource.CustomSource;

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
        RerfeshGrid();
        string _sqlPriceList;
        DataTable _tblPriceList = new DataTable();

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

        _sqlPriceList = @"
                                    SELECT 'TRUE' FROM [@OCRP] A WHERE A.Code = '" + txtPriceList.Text + @"'
                                    ";

        //DataTable _tblPriceList = new DataTable();
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);
        //clsFunctions.DataGridViewSetup(dgvItemGroup, _tblPriceList, "ItemGroupPriceList");
        btnCreate.Visible = false;
        if (_tblPriceList.Rows.Count < 1)
        {

            if(txtPriceList.Text != "")
            {
                btnCreate.Visible = true;
            }
       

            //DialogResult result;
            //result = MessageBox.Show("Pricelist Not Found! Do You Want to Create this Pricelist?", "Pricelist Creation", MessageBoxButtons.YesNo);
            //if (result == System.Windows.Forms.DialogResult.Yes)
            //{
            //    clsSAPFunctions.CreatePriceList(txtPriceList.Text, txtPriceList.Text);
            //    //APInvoiceSundry(_DocEntry, _Branch, _DBBranch, _APIDocEntry, DateTime.Parse(txtPostingDate.Text), double.Parse(txtSundryAmount.Text));
            //}
            //else
            //{
            //    return;
            //}


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



        _sqlPriceList = @"
                        SELECT A.ItemCode AS Code, B.FirmName AS [Brand] ,A.ItemName AS Name, 0.00 AS [Price],0.00 AS [Disc], 0.00 AS [Net Of Disc], 0.00 AS [New Net Of Disc] FROM OITM A 
                        INNER JOIN OMRC B ON A.FirmCode = B.FirmCode
                        WHERE A.ItmsGrpCod = ''
                        ";
        _tblPriceList = new DataTable();
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);
        clsFunctions.DataGridViewSetup(dgvItemList_GMA, _tblPriceList);



        _sqlPriceList = @"
                        SELECT A.ItemCode AS Code, B.FirmName AS [Brand] ,A.ItemName AS Name, 0.00 AS [Price],0.00 AS [Disc], 0.00 AS [Net Of Disc], 0.00 AS [New Net Of Disc] FROM OITM A 
                        INNER JOIN OMRC B ON A.FirmCode = B.FirmCode
                        WHERE A.ItmsGrpCod = ''
                        ";
        _tblPriceList = new DataTable();
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);
        clsFunctions.DataGridViewSetup(dgvItemList_VISMIN, _tblPriceList);
    }

    private void RerfeshGrid()
    {
        string _sqlPriceList;
        DataTable _tblPriceList = new DataTable();

        _sqlPriceList = @"
                        SELECT A.ItmsGrpCod AS Code, A.ItmsGrpNam AS Name
                        , CASE WHEN B.U_VisMinDisc IS NULL THEN 0 ELSE B.U_VisMinDisc END AS [VISMIN DISC %]
                        , CASE WHEN B.U_GMADisc IS NULL THEN 0 ELSE B.U_GMADisc END AS [GMA DISC %]
                        FROM OITB A
                        LEFT OUTER JOIN [@OCPL] B ON A.ItmsGrpNam = B.U_Category
                        WHERE A.ItmsGrpCod = ''
                        ";

        _tblPriceList = new DataTable();
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);
        clsFunctions.DataGridViewSetup(dgvItemGroup, _tblPriceList, "ItemGroupPriceList");



        _sqlPriceList = @"
                                                    SELECT A.ItemCode AS Code, B.FirmName AS [Brand] ,A.ItemName AS Name, 0.00 AS [Price],0.00 AS [Disc], 0.00 AS [Net Of Disc], 0.00 AS [New Net Of Disc] FROM OITM A 
                                                    INNER JOIN OMRC B ON A.FirmCode = B.FirmCode
                                                    WHERE A.ItmsGrpCod = ''
                                                 ";
        _tblPriceList = new DataTable();
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);
        clsFunctions.DataGridViewSetup(dgvItemList_GMA, _tblPriceList);



        _sqlPriceList = @"
                                                    SELECT A.ItemCode AS Code, B.FirmName AS [Brand] ,A.ItemName AS Name, 0.00 AS [Price],0.00 AS [Disc], 0.00 AS [Net Of Disc], 0.00 AS [New Net Of Disc] FROM OITM A 
                                                    INNER JOIN OMRC B ON A.FirmCode = B.FirmCode
                                                    WHERE A.ItmsGrpCod = ''
                                                 ";
        _tblPriceList = new DataTable();
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);
        clsFunctions.DataGridViewSetup(dgvItemList_VISMIN, _tblPriceList);
    }

    private void dgvItemGroup_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string _ItmsGrpCod = dgvItemGroup.Rows[e.RowIndex].Cells["Code"].Value.ToString().Trim();
            _sItemGroupCode = _ItmsGrpCod;
            string _ItmsGrpNam = dgvItemGroup.Rows[e.RowIndex].Cells["Name"].Value.ToString().Trim();
            string _VISMINDisc = dgvItemGroup.Rows[e.RowIndex].Cells["VISMIN DISC %"].Value.ToString().Trim();
            string _GMADisc = dgvItemGroup.Rows[e.RowIndex].Cells["GMA DISC %"].Value.ToString().Trim();


            string _sqlPriceList;
            _sqlPriceList = @"
                                                    SELECT A.ItemCode AS Code, B.FirmName AS [Brand] ,A.ItemName AS Name
													, CASE WHEN C.U_GMAPrice IS NULL THEN 0 ELSE C.U_GMAPrice END AS [Price]
													, '" + double.Parse(_GMADisc).ToString("N2") + @"' AS [Disc]
													, (CASE WHEN C.U_GMAPrice IS NULL THEN 0 ELSE C.U_GMAPrice END) - ((CASE WHEN C.U_GMAPrice IS NULL THEN 0 ELSE C.U_GMAPrice END) * ( " + double.Parse(_GMADisc).ToString("N2") + @" / 100)) AS [Net Of Disc] 
													, (CASE WHEN C.U_GMAPrice IS NULL THEN 0 ELSE C.U_GMAPrice END) - ((CASE WHEN C.U_GMAPrice IS NULL THEN 0 ELSE C.U_GMAPrice END) * ( " + double.Parse(_GMADisc).ToString("N2") + @" / 100)) AS [New Net Of Disc]
													FROM OITM A 
                                                    INNER JOIN OMRC B ON A.FirmCode = B.FirmCode
													LEFT OUTER JOIN [@OIPL] C ON A.ItemCode = C.U_ItemCode AND C.Code = '" + txtPriceList.Text + @"'
                                                    WHERE A.ItmsGrpCod = '" + _ItmsGrpCod + @"'
                                                 ";
            DataTable _tblPriceList = new DataTable();
            _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);
            clsFunctions.DataGridViewSetup(dgvItemList_GMA, _tblPriceList, "ItemPriceList");




            _sqlPriceList = @"
                                                    SELECT A.ItemCode AS Code, B.FirmName AS [Brand] ,A.ItemName AS Name
												    , CASE WHEN C.U_VisMinPrice IS NULL THEN 0 ELSE C.U_VisMinPrice END AS [Price]
												    , '" + double.Parse(_VISMINDisc).ToString("N2") + @"' AS [Disc]
												    , (CASE WHEN C.U_VisMinPrice IS NULL THEN 0 ELSE C.U_VisMinPrice END) - ((CASE WHEN C.U_VisMinPrice IS NULL THEN 0 ELSE C.U_VisMinPrice END) * ( " + double.Parse(_VISMINDisc).ToString("N2") + @" / 100)) AS [Net Of Disc] 
												    , (CASE WHEN C.U_VisMinPrice IS NULL THEN 0 ELSE C.U_VisMinPrice END) - ((CASE WHEN C.U_VisMinPrice IS NULL THEN 0 ELSE C.U_VisMinPrice END) * ( " + double.Parse(_VISMINDisc).ToString("N2") + @" / 100)) AS [New Net Of Disc]
												    FROM OITM A 
                                                    INNER JOIN OMRC B ON A.FirmCode = B.FirmCode
												    LEFT OUTER JOIN [@OIPL] C ON A.ItemCode = C.U_ItemCode AND C.Code =  '" + txtPriceList.Text + @"'
                                                    WHERE A.ItmsGrpCod = '" + _ItmsGrpCod + @"'
                                                    ";
            _tblPriceList = new DataTable();
            _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);
            clsFunctions.DataGridViewSetup(dgvItemList_VISMIN, _tblPriceList, "ItemPriceList");

        }
        catch
        {

        }
    }
    private void ItemPriceCalc()
    {

    }

    private void txtItemSearch_TextChanged(object sender, EventArgs e)
    {
        String someText;
        someText = txtItemSearch_GMA.Text;

        int gridRow = 0;
        int gridColumn = _HeaderColumn;

        dgvItemList_GMA.ClearSelection();
        dgvItemList_GMA.CurrentCell = null;

        foreach (DataGridViewRow row in dgvItemList_GMA.Rows)
        {
            //cboPayrolPeriod.Items.Add(row[0].ToString());

            DataGridViewCell _cell = dgvItemList_GMA.Rows[gridRow].Cells[gridColumn];
            if (_cell.Value.ToString().ToLower().Contains(someText.ToLower()) == true)
            {
                dgvItemList_GMA.Rows[gridRow].Selected = true;
                dgvItemList_GMA.FirstDisplayedScrollingRowIndex = gridRow;

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

            string _VISMINDisc = dgvItemGroup.Rows[e.RowIndex].Cells["VISMIN DISC %"].Value.ToString().Trim();
            string _GMADisc = dgvItemGroup.Rows[e.RowIndex].Cells["GMA DISC %"].Value.ToString().Trim();


            foreach (DataGridViewRow rGMA in dgvItemList_GMA.Rows)
            {
                string _Price = rGMA.Cells["Price"].Value.ToString().Trim();
                rGMA.Cells["Disc"].Value = double.Parse(_GMADisc).ToString("N2");

                double _NetAmount = 0;
                _NetAmount = double.Parse(_Price) - ((double.Parse(_Price) * (double.Parse(_GMADisc) / 100)));
                rGMA.Cells["New Net Of Disc"].Value = _NetAmount.ToString("N2");
            }

            foreach (DataGridViewRow rVISMIN in dgvItemList_VISMIN.Rows)
            {
                string _Price = rVISMIN.Cells["Price"].Value.ToString().Trim();
                rVISMIN.Cells["Disc"].Value = double.Parse(_VISMINDisc).ToString("N2");

                double _NetAmount = 0;
                _NetAmount = double.Parse(_Price) - ((double.Parse(_Price) * (double.Parse(_VISMINDisc) / 100)));
                rVISMIN.Cells["New Net Of Disc"].Value = _NetAmount.ToString("N2");
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

            string _Price = dgvItemList_VISMIN.Rows[e.RowIndex].Cells["Price"].Value.ToString().Trim();
            string _Disc = dgvItemList_VISMIN.Rows[e.RowIndex].Cells["Disc"].Value.ToString().Trim();

            dgvItemList_VISMIN.Rows[e.RowIndex].Cells["New Net Of Disc"].Value = (double.Parse(_Price) - (double.Parse(_Price) * (double.Parse(_Disc) / 100))).ToString("N2");
        }
        catch
        {

        }
    }
    private void dgvItemList_GMA_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string _Price = dgvItemList_GMA.Rows[e.RowIndex].Cells["Price"].Value.ToString().Trim();
            string _Disc = dgvItemList_GMA.Rows[e.RowIndex].Cells["Disc"].Value.ToString().Trim();

            dgvItemList_GMA.Rows[e.RowIndex].Cells["New Net Of Disc"].Value = (double.Parse(_Price) - (double.Parse(_Price) * (double.Parse(_Disc) / 100))).ToString("N2");
        }
        catch
        {

        }
    }

    private void txtItemSearch_VISMIN_TextChanged(object sender, EventArgs e)
    {
        String someText;
        someText = txtItemSearch_VISMIN.Text;

        int gridRow = 0;
        int gridColumn = _HeaderColumn;

        dgvItemList_VISMIN.ClearSelection();
        dgvItemList_VISMIN.CurrentCell = null;

        foreach (DataGridViewRow row in dgvItemList_VISMIN.Rows)
        {
            //cboPayrolPeriod.Items.Add(row[0].ToString());

            DataGridViewCell _cell = dgvItemList_VISMIN.Rows[gridRow].Cells[gridColumn];
            if (_cell.Value.ToString().ToLower().Contains(someText.ToLower()) == true)
            {
                dgvItemList_VISMIN.Rows[gridRow].Selected = true;
                dgvItemList_VISMIN.FirstDisplayedScrollingRowIndex = gridRow;

                //_gDocEntry = dgvDisplay.Rows[gridRow].Cells[0].Value.ToString().Trim();

                return;
            }
            gridRow++;
        }
    }

    private void txtItemSearch_GMA_TextChanged(object sender, EventArgs e)
    {
        String someText;
        someText = txtItemSearch_GMA.Text;

        int gridRow = 0;
        int gridColumn = _HeaderColumn;

        dgvItemList_GMA.ClearSelection();
        dgvItemList_GMA.CurrentCell = null;

        foreach (DataGridViewRow row in dgvItemList_GMA.Rows)
        {
            //cboPayrolPeriod.Items.Add(row[0].ToString());

            DataGridViewCell _cell = dgvItemList_GMA.Rows[gridRow].Cells[gridColumn];
            if (_cell.Value.ToString().ToLower().Contains(someText.ToLower()) == true)
            {
                dgvItemList_GMA.Rows[gridRow].Selected = true;
                dgvItemList_GMA.FirstDisplayedScrollingRowIndex = gridRow;

                //_gDocEntry = dgvDisplay.Rows[gridRow].Cells[0].Value.ToString().Trim();

                return;
            }
            gridRow++;
        }
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        
        if(txtPriceList.Text == "")
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

        MessageBox.Show("Data Updated Complete");
    }

    private void dgvItemList_VISMIN_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {

    }


    private void UpdateVisMinPrice()
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
        oDocGeneralService = oCompService.GetGeneralService("OCRP");
        // Point to the Header of the Doc UDO
        oDocGeneralData = (SAPbobsCOM.GeneralData)oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);


        oDocGeneralParams = oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
        oDocGeneralParams.SetProperty("Code", txtPriceList.Text);
        oDocGeneralData = oDocGeneralService.GetByParams(oDocGeneralParams);



        foreach (DataGridViewRow row in dgvItemList_VISMIN.Rows)
        {

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

    private void UpdateItemList_VISMIN(string _PriceList = "")
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


        foreach (DataGridViewRow row in dgvItemList_VISMIN.Rows)
        {
            string _Code = row.Cells["Code"].Value.ToString().Trim();
            string _Name = row.Cells["Name"].Value.ToString().Trim();
            string _Price = row.Cells["Price"].Value.ToString().Trim();
            string _Disc = row.Cells["Disc"].Value.ToString().Trim();
            string _NetAmt = row.Cells["New Net Of Disc"].Value.ToString().Trim();



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
                oDocLineGeneralData.SetProperty("U_VisMinPrice", _Price);
                oDocLineGeneralData.SetProperty("U_VisMinDisc", _Disc);
                oDocLineGeneralData.SetProperty("U_VisMinNet", _NetAmt);

            }
            else
            {
                oDocLinesCollection = oDocGeneralData.Child("OIPL");
                oDocLineGeneralData = oDocLinesCollection.Add();
                oDocLineGeneralData.SetProperty("U_ItemCode", _Code);
                oDocLineGeneralData.SetProperty("U_ItemName", _Name);
                oDocLineGeneralData.SetProperty("U_VisMinPrice", _Price);
                oDocLineGeneralData.SetProperty("U_VisMinDisc", _Disc);
                oDocLineGeneralData.SetProperty("U_VisMinNet", _NetAmt);
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

    private void UpdateItemList_GMA(string _PriceList = "")
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


        foreach (DataGridViewRow row in dgvItemList_GMA.Rows)
        {
            string _Code = row.Cells["Code"].Value.ToString().Trim();
            string _Name = row.Cells["Name"].Value.ToString().Trim();
            string _Price = row.Cells["Price"].Value.ToString().Trim();
            string _Disc = row.Cells["Disc"].Value.ToString().Trim();
            string _NetAmt = row.Cells["New Net Of Disc"].Value.ToString().Trim();



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
                oDocLineGeneralData.SetProperty("U_GMAPrice", _Price);
                oDocLineGeneralData.SetProperty("U_GMADisc", _Disc);
                oDocLineGeneralData.SetProperty("U_GMANet", _NetAmt);

            }
            else
            {
                oDocLinesCollection = oDocGeneralData.Child("OIPL");
                oDocLineGeneralData = oDocLinesCollection.Add();
                oDocLineGeneralData.SetProperty("U_ItemCode", _Code);
                oDocLineGeneralData.SetProperty("U_ItemName", _Name);
                oDocLineGeneralData.SetProperty("U_GMAPrice", _Price);
                oDocLineGeneralData.SetProperty("U_GMADisc", _Disc);
                oDocLineGeneralData.SetProperty("U_GMANet", _NetAmt);
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

        // Retrieve the relevant service
        oDocGeneralService = oCompService.GetGeneralService("OCRP");
        // Point to the Header of the Doc UDO
        oDocGeneralData = (SAPbobsCOM.GeneralData)oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);


        oDocGeneralParams = oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
        oDocGeneralParams.SetProperty("Code", _PriceList);
        oDocGeneralData = oDocGeneralService.GetByParams(oDocGeneralParams);

        

        foreach (DataGridViewRow row in dgvItemGroup.Rows)
        {
            string _ItmsGroupCode = row.Cells["Code"].Value.ToString().Trim();
            string _Category = row.Cells["Name"].Value.ToString().Trim();
            string _VISMINDisc = row.Cells["VISMIN DISC %"].Value.ToString().Trim();
            string _GMADisc = row.Cells["GMA DISC %"].Value.ToString().Trim();



            string _sqlOCPL;
            DataTable _tblOCPL = new DataTable();
            _sqlOCPL = @"
                          SELECT A.LineId FROM [@OCPL] A WHERE A.Code = '" + _PriceList.Replace("'","''") + @"' AND A.U_Category = '" + _Category + @"'
                         ";
            _tblOCPL = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlOCPL);
            string _OCPL_LineId = clsSQLClientFunctions.GetData(_tblOCPL, "LineId","0");


            if(_tblOCPL.Rows.Count > 0)
            {
                oDocLinesCollection = oDocGeneralData.Child("OCPL");
                oDocLineGeneralData = oDocLinesCollection.Item(int.Parse(_OCPL_LineId) - 1);

                //MessageBox.Show(oDocLineGeneralData.GetProperty("U_Category"));
                oDocLineGeneralData.SetProperty("U_Category", _Category);
                oDocLineGeneralData.SetProperty("U_VisMinDisc", _VISMINDisc);
                oDocLineGeneralData.SetProperty("U_GMADisc", _GMADisc);

                //oDocLinesCollection.Item(int.Parse(_OCPL_LineId) - 1).SetProperty("U_VisMinDisc", _VISMINDisc);
                //oDocLinesCollection.Item(int.Parse(_OCPL_LineId) - 1).SetProperty("U_GMADisc", _GMADisc);
            }
            else
            {

                oDocLinesCollection = oDocGeneralData.Child("OCPL");
                oDocLineGeneralData = oDocLinesCollection.Add();
                oDocLineGeneralData.SetProperty("U_Category", _Category);
                oDocLineGeneralData.SetProperty("U_VisMinDisc", _VISMINDisc);
                oDocLineGeneralData.SetProperty("U_GMADisc", _GMADisc);
            }


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


            double _RowCount;
            int _Count = 0;
            _RowCount = _tblOIPL.Rows.Count;


            foreach (DataRow rOIPL in _tblOIPL.Rows)
            {
                string _ItemCode = rOIPL["ItemCode"].ToString();
                string _ItemName = rOIPL["ItemName"].ToString();
                string _PriceList_ItemCode = rOIPL["U_ItemCode"].ToString();

                string _OIPL_LineId = rOIPL["LineId"].ToString();
                string _GMAPrice = rOIPL["U_GMAPrice"].ToString();
                string _VisMinPrice = rOIPL["U_VisMinPrice"].ToString();


                if(_PriceList_ItemCode != "")
                {

                    oDocLinesCollection = oDocGeneralData.Child("OIPL");
                    oDocLineGeneralData = oDocLinesCollection.Item(int.Parse(_OIPL_LineId) - 1);

                    //MessageBox.Show(oDocLineGeneralData.GetProperty("U_Category"));
                    oDocLineGeneralData.SetProperty("U_ItemCode", _ItemCode);
                    oDocLineGeneralData.SetProperty("U_ItemName", _ItemName);
                    oDocLineGeneralData.SetProperty("U_VisMinPrice", _VisMinPrice);
                    oDocLineGeneralData.SetProperty("U_GMAPrice", _GMAPrice);


                    oDocLineGeneralData.SetProperty("U_VisMinDisc", _VISMINDisc);
                    oDocLineGeneralData.SetProperty("U_GMADisc", _GMADisc);

                    double _Value = 0;
                    _Value = double.Parse(_VisMinPrice) - (double.Parse(_VisMinPrice) * (double.Parse(_VISMINDisc) / 100));
                    //oDocLinesCollection.Item(int.Parse(_OIPL_LineId) - 1).SetProperty("U_VisMinNet", _Value);
                    oDocLineGeneralData.SetProperty("U_VisMinNet", _Value);
                    _Value = double.Parse(_GMAPrice) - (double.Parse(_GMAPrice) * (double.Parse(_GMADisc) / 100));
                    //oDocLinesCollection.Item(int.Parse(_OIPL_LineId) - 1).SetProperty("U_GMANet", _Value);
                    oDocLineGeneralData.SetProperty("U_GMANet", _Value);

                    ////oDocLineGeneralData = oDocLinesCollection.Add();
                    //oDocLinesCollection.Item(int.Parse(_OIPL_LineId) - 1).SetProperty("U_ItemCode", _ItemCode);
                    //oDocLinesCollection.Item(int.Parse(_OIPL_LineId) - 1).SetProperty("U_ItemName", _ItemName);
                    //oDocLinesCollection.Item(int.Parse(_OIPL_LineId) - 1).SetProperty("U_VisMinPrice", _VisMinPrice);
                    //oDocLinesCollection.Item(int.Parse(_OIPL_LineId) - 1).SetProperty("U_GMAPrice", _GMAPrice);

                    //oDocLinesCollection.Item(int.Parse(_OIPL_LineId) - 1).SetProperty("U_VisMinDisc", _VISMINDisc);
                    //oDocLinesCollection.Item(int.Parse(_OIPL_LineId) - 1).SetProperty("U_GMADisc", _GMADisc);

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

        // Insert Values to the Lines properties try
        try
        {
            oDocGeneralService.Add(oDocGeneralData);
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

    private void btnUpdateVisMin_Click(object sender, EventArgs e)
    {

        if (txtPriceList.Text == "")
        {
            MessageBox.Show("Pricelist Not Define");
            return;
        }



        UpdateItemList_VISMIN(txtPriceList.Text);
        string _sqlPriceList = @"
                                        SELECT A.WhsCode AS Code, A.WhsName AS Name, B.Location AS Area, A.U_PriceList AS PriceList
                                        FROM OWHS A 
                                        LEFT OUTER JOIN OLCT B ON A.Location = B.Code
										WHERE ISNULL(A.U_PriceList,'') = '" + txtPriceList.Text + @"' AND ISNULL(B.Location,'') = 'VISMIN'
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

            UpdateBranchItem(_BPPricelist, _sItemGroupCode, _Code, _Area);



            Application.DoEvents();
            _Count++;
            lblStatus1.Text = "Pricelist Data Reset : (" + _Count + " / " + _RowCount + ") : Data Progress Percentage ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) : Current Price List Uploading " + _BPPricelist + " : Branch :" + _Name;
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
        }
        //UpdateItemGroupList();
        MessageBox.Show("Data Updated Complete");
    }

    private void UpdateBranchItem(string _PriceList, string _ItemGroupCode, string _BranchCode, string _Area)
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


        _sqlPriceList = @"SELECT A.Code, B.Code, B.U_Area, A.U_ItemCode, A.U_ItemName
                                , A.U_VisMinDisc, A.U_VisMinNet, A.U_VisMinPrice
                                , A.U_GMADisc, A.U_GMANet, A.U_GMAPrice 
                                , CASE WHEN B.U_Area = 'GMA' THEN A.U_GMANet ELSE A.U_VisMinNet END AS [Main Net]
                                , C.U_Discount, C.U_NetAmt, C.U_Price 
                                , C.U_Frozen
                                FROM [@OIPL] A 
                                INNER JOIN [@OBPL] B ON A.Code = B.U_BPPriceList
                                LEFT JOIN [@BPL1] C ON B.Code = C.Code AND A.U_ItemCode = C.U_ItemCode

                                WHERE CASE WHEN B.U_Area = 'GMA' THEN A.U_GMANet ELSE A.U_VisMinNet END <> C.U_NetAmt
						   AND A.Code = '" + _PriceList + @"' AND B.ItmsGrpCod = '" + _ItemGroupCode + @"'  AND B.Code = '" + _BranchCode + @"'";

        //_sqlPriceList = @"SELECT A.U_ItemCode, A.U_ItemName
        //                       , A.U_VisMinDisc, A.U_VisMinNet, A.U_VisMinPrice
        //                       , A.U_GMADisc, A.U_GMANet, A.U_GMAPrice 
        //                  FROM [@OIPL] A INNER JOIN [OITM] B ON A.U_ItemCode = B.ItemCode

        // WHERE A.Code = '" + _PriceList + @"' AND B.ItmsGrpCod = '" + _ItemGroupCode + @"'";
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);


        double _RowCount;
        int _Count = 0;
        _RowCount = _tblPriceList.Rows.Count;

        foreach (DataRow r_Itm in _tblPriceList.Rows)
        {
            string _ItemCode = r_Itm["U_ItemCode"].ToString();
            string _ItemName = r_Itm["U_ItemName"].ToString();

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
                          SELECT A.LineId, A.U_Frozen FROM [@BPL1] A WHERE A.Code = '" + _BranchCode + @"' AND A.U_ItemCode = '" + _ItemCode + @"'
                         ";

            DataTable _tblBPL2 = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlBPL2);
            _LineId = clsSQLClientFunctions.GetData(_tblBPL2, "LineId", "0");
            _Frozen = clsSQLClientFunctions.GetData(_tblBPL2, "U_Frozen", "0");



            if (_tblBPL2.Rows.Count > 0)
            {

                oDocLinesCollection = oDocGeneralData.Child("BPL1");
                oDocLineGeneralData = oDocLinesCollection.Item(int.Parse(_LineId) - 1);

                //MessageBox.Show(oDocLineGeneralData.GetProperty("U_Category"));
                oDocLineGeneralData.SetProperty("U_ItemCode", _ItemCode);
                oDocLineGeneralData.SetProperty("U_ItemName", _ItemName);
                oDocLineGeneralData.SetProperty("U_Price", _Price);
                oDocLineGeneralData.SetProperty("U_Discount", _Disc);

                if (_Frozen == "N")
                {
                    oDocLineGeneralData.SetProperty("U_NetAmt", _NetAmt);
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

        _sqlPriceList = @"
                            SELECT A.Code, A.U_Category, B.Code, B.U_Area, A.U_VisMinDisc, A.U_GMADisc
                            , CASE WHEN B.U_Area = 'GMA' THEN A.U_GMADisc ELSE A.U_VisMinDisc END AS [Main Discs]
                            , C.U_Discount, C.U_Frozen FROM [@OCPL] A 
                            INNER JOIN [@OBPL] B ON A.Code = B.U_BPPriceList
                            INNER JOIN [@BPL2] C ON B.Code = C.Code AND A.U_Category = C.U_Category
                            WHERE CASE WHEN B.U_Area = 'GMA' THEN A.U_GMADisc ELSE A.U_VisMinDisc END <> C.U_Discount
                            AND A.Code = '" + _PriceList + @"' AND B.Code = '" + _BranchCode + @"'

                         ";

        //        _sqlPriceList = @"

        //SELECT A.U_Category, A.U_VisMinDisc, A.U_GMADisc FROM [@OCPL] A WHERE A.Code = '" + _PriceList + @"'

        //";
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

        _sqlPriceList = @"SELECT A.Code, B.Code, B.U_Area, A.U_ItemCode, A.U_ItemName
                                , A.U_VisMinDisc, A.U_VisMinNet, A.U_VisMinPrice
                                , A.U_GMADisc, A.U_GMANet, A.U_GMAPrice 
                                , CASE WHEN B.U_Area = 'GMA' THEN A.U_GMANet ELSE A.U_VisMinNet END AS [Main Net]
                                , C.U_Discount, C.U_NetAmt, C.U_Price 
                                , C.U_Frozen
                                FROM [@OIPL] A 
                                INNER JOIN [@OBPL] B ON A.Code = B.U_BPPriceList
                                LEFT JOIN [@BPL1] C ON B.Code = C.Code AND A.U_ItemCode = C.U_ItemCode

                                WHERE CASE WHEN B.U_Area = 'GMA' THEN A.U_GMANet ELSE A.U_VisMinNet END <> C.U_NetAmt
                                    AND A.Code = '" + _PriceList + @"'  AND B.Code = '" + _BranchCode + @"'";

        //_sqlPriceList = @"SELECT A.U_ItemCode, A.U_ItemName
        //                       , A.U_VisMinDisc, A.U_VisMinNet, A.U_VisMinPrice
        //                       , A.U_GMADisc, A.U_GMANet, A.U_GMAPrice 
        //                  FROM [@OIPL] A WHERE A.Code = '" + _PriceList + @"'";
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);


        double _RowCount;
        int _Count = 0;
        _RowCount = _tblPriceList.Rows.Count;

        foreach (DataRow r_Itm in _tblPriceList.Rows)
        {
            string _ItemCode = r_Itm["U_ItemCode"].ToString();
            string _ItemName = r_Itm["U_ItemName"].ToString();

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
                          SELECT A.LineId, A.U_Frozen FROM [@BPL1] A WHERE A.Code = '" + _BranchCode + @"' AND A.U_ItemCode = '" + _ItemCode + @"'
                         ";

            DataTable _tblBPL2 = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlBPL2);
            _LineId = clsSQLClientFunctions.GetData(_tblBPL2, "LineId", "0");
            _Frozen = clsSQLClientFunctions.GetData(_tblBPL2, "U_Frozen", "0");


            if (_tblBPL2.Rows.Count > 0)
            {

                oDocLinesCollection = oDocGeneralData.Child("BPL1");
                oDocLineGeneralData = oDocLinesCollection.Item(int.Parse(_LineId) - 1);

                //MessageBox.Show(oDocLineGeneralData.GetProperty("U_Category"));
                oDocLineGeneralData.SetProperty("U_ItemCode", _ItemCode);
                oDocLineGeneralData.SetProperty("U_ItemName", _ItemName);
                oDocLineGeneralData.SetProperty("U_Price", _Price);
                oDocLineGeneralData.SetProperty("U_Discount", _Disc);

                if (_Frozen == "N")
                {
                    oDocLineGeneralData.SetProperty("U_NetAmt", _NetAmt);
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

    private void btnUpdateGMA_Click(object sender, EventArgs e)
    {
        UpdateItemList_GMA(txtPriceList.Text);
        string _sqlPriceList = @"
                                        SELECT A.WhsCode AS Code, A.WhsName AS Name, B.Location AS Area, A.U_PriceList AS PriceList
                                        FROM OWHS A 
                                        LEFT OUTER JOIN OLCT B ON A.Location = B.Code
										WHERE ISNULL(A.U_PriceList,'') = '" + txtPriceList.Text + @"' AND ISNULL(B.Location,'') = 'GMA'
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

            UpdateBranchItem(_BPPricelist, _sItemGroupCode, _Code, _Area);



            Application.DoEvents();
            _Count++;
            lblStatus1.Text = "Pricelist Data Reset : (" + _Count + " / " + _RowCount + ") : Data Progress Percentage ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) : Current Price List Uploading " + _BPPricelist + " : Branch :" + _Name;
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));
        }
        //UpdateItemGroupList();
        MessageBox.Show("Data Updated Complete");
    }

    private void button1_Click(object sender, EventArgs e)
    {
        //clsSAPFunctions.UpdatePriceList(txtPriceList.Text);
        string _sqlPriceList = @"
                                    SELECT A.Code FROM [SBOLIVE_KPOP].dbo.[@OCRP] A 
                                    ";

        DataTable _tblPriceList = new DataTable();
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);


        double _RowCount;
        int _Count = 0;
        _RowCount = _tblPriceList.Rows.Count;


        foreach (DataRow row in _tblPriceList.Rows)
        {
            string _PriceList = row["Code"].ToString();
            txtPriceList.Text = _PriceList;

            _sqlPriceList = @"
                                    SELECT A.ItmsGrpCod AS Code, A.ItmsGrpNam AS Name
                                    , CASE WHEN B.U_VisMinDisc IS NULL THEN 0 ELSE B.U_VisMinDisc END AS [VISMIN DISC %]
                                    , CASE WHEN B.U_GMADisc IS NULL THEN 0 ELSE B.U_GMADisc END AS [GMA DISC %]
                                    FROM OITB A
                                    LEFT OUTER JOIN [@OCPL] B ON A.ItmsGrpNam = B.U_Category AND B.Code = '" + txtPriceList.Text.Replace("'","''") + @"'
                                    ";

            _tblPriceList = new DataTable();
            _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);
            clsFunctions.DataGridViewSetup(dgvItemGroup, _tblPriceList, "ItemGroupPriceList");

            UpdateItemGroupList(_PriceList);



            Application.DoEvents();
            _Count++;
            lblStatus1.Text = "Pricelist Data Reset : (" + _Count + " / " + _RowCount + ") : Data Progress Percentage ( " + Math.Round(((_Count / _RowCount) * 100), 2) + " % ) : Current Price List Uploading " + _PriceList;
            pbDataProgress.Value = Convert.ToInt32(((_Count / _RowCount) * 100));

        }

        MessageBox.Show("Price list Successfully Reset");
    }

    private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
    {

    }

    private void btnExcelUpload_Click(object sender, EventArgs e)
    {
        frmPriceListUploader frmPriceListUploader = new frmPriceListUploader();
        frmPriceListUploader.MdiParent = ((frmMainWindow)(this.MdiParent));
        frmPriceListUploader._sPriceList = txtPriceList.Text;
        frmPriceListUploader.Show();
    }
}