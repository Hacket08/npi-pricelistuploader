using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Windows.Forms;
using System.Data;

using System.Configuration;

using System.Data.SqlClient;
using System.Text.RegularExpressions;

class clsSAPFunctions
{
    public static SAPbobsCOM.Company oCompany;

    public static SAPbobsCOM.Company SAPInitializeConnection(string dftDBName, string SAPUser, string SAPPass, out bool isConnected, out string _ErrorMsg)
    {
        int lRetCode;
        SAPbobsCOM.Company newCompany;
        newCompany = new SAPbobsCOM.Company();

        newCompany.LicenseServer = ConfigurationManager.AppSettings["sysLicenseServer"];

        newCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012;
        newCompany.Server = ConfigurationManager.AppSettings["sysDBServer"];
        newCompany.DbUserName = ConfigurationManager.AppSettings["sysDBUsername"];
        newCompany.DbPassword = ConfigurationManager.AppSettings["sysDBPassword"];

        //oCompany.CompanyDB = ConfigurationManager.AppSettings["sysDftDBCompany"];
        //oCompany.UserName = ConfigurationManager.AppSettings["sysSAPUsername"];
        //oCompany.Password = ConfigurationManager.AppSettings["sysSAPPassword"];

        newCompany.CompanyDB = dftDBName;
        newCompany.UserName = SAPUser;
        newCompany.Password = SAPPass;

        newCompany.language = SAPbobsCOM.BoSuppLangs.ln_English;


        lRetCode = newCompany.Connect();
        //DIErrorHandler(lRetCode, "Connecting To SAP", "SAP Connection");

        string sErrMsg;
        int lErrCode;

        if (lRetCode != 0)
        {
            newCompany.GetLastError(out lErrCode, out sErrMsg);
            _ErrorMsg = lErrCode + " " + sErrMsg;
            isConnected = false;
        }
        else
        {
            _ErrorMsg = "Conneted To SAP";
            isConnected = true;
        }

        return newCompany;
        //return 
    }

    
    public static bool DIErrorHandler(int lRetCode, string Action, string MsgTitle)
    {
        string sErrMsg;
        int lErrCode;

        if (lRetCode != 0)
        {
            oCompany.GetLastError(out lErrCode, out sErrMsg);
            return false;
        }
        else
        {
            return true;
        }
    }

    //public static bool JEAllocation(string _Branch, string _PayrolPeriod)
    //{

    //    string sysDftDBCompany = "DESIHOFC";
    //    string sysDBUsername = ConfigurationManager.AppSettings["sysSAPUsername"];
    //    string sysDBPassword = ConfigurationManager.AppSettings["sysSAPPassword"];

    //    bool isConnected = false;
    //    string _Msg = "";
    //    oCompany = SAPInitializeConnection(sysDftDBCompany, sysDBUsername, sysDBPassword, out isConnected, out _Msg );
    //    MessageBox.Show(_Msg);
    //    if (isConnected == true)
    //    {

    //        string _sqlSelect;
    //        DataTable _tblSelect;

    //        _sqlSelect = @"
    //                                    SELECT A.Account, A.Credit, A.Debit, A.AccountName, A.EmployeeName, A.DocDate
    //                                    FROM [dbo].[fnSAPTransaction]('" + _Branch + @"','" + _PayrolPeriod + @"') A
    //                                ";
    //        _tblSelect = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlSelect);

    //        string _DocDate = clsSQLClientFunctions.GetData(_tblSelect, "DocDate", "0");


    //        SAPbobsCOM.JournalEntries _JournalEntries;
    //        _JournalEntries = (SAPbobsCOM.JournalEntries)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);

    //        _JournalEntries.ReferenceDate = DateTime.Parse(_DocDate);
    //        _JournalEntries.TaxDate = DateTime.Parse(_DocDate);
    //        _JournalEntries.DueDate = DateTime.Parse(_DocDate);
    //        _JournalEntries.Memo = "PAYROLL PERIOD " + _PayrolPeriod + @" FOR " + _Branch;
    //        _JournalEntries.UserFields.Fields.Item("U_NSAPADVISENO").Value = _PayrolPeriod;


    //        foreach (DataRow row in _tblSelect.Rows)
    //        {
    //            {
    //                string _Account = row["Account"].ToString();
    //                string _Credit = row["Credit"].ToString();
    //                string _Debit = row["Debit"].ToString();
    //                string _AccountName = row["AccountName"].ToString();
    //                string _EmployeeName = row["EmployeeName"].ToString();

    //                if (_Account.Substring(0,1) == "V")
    //                {
    //                    _JournalEntries.Lines.ShortName = _Account;
    //                }
    //                else
    //                {
    //                    _JournalEntries.Lines.AccountCode = _Account;
    //                }
        
    //                _JournalEntries.Lines.LineMemo = _AccountName;

    //                _JournalEntries.Lines.Credit = double.Parse(_Credit);
    //                _JournalEntries.Lines.Debit = double.Parse(_Debit);
    //                _JournalEntries.Lines.UserFields.Fields.Item("U_NSAPADVISENO").Value = _PayrolPeriod;
    //                _JournalEntries.Lines.UserFields.Fields.Item("U_EMPLOYEE").Value = _EmployeeName;
    //                _JournalEntries.Lines.Add();
    //            }
    //        }


    //        int lRetCode;
    //        string sErrMsg;
    //        int lErrCode;
    //        Application.DoEvents();
    //        lRetCode = _JournalEntries.Add();

    //        if (lRetCode != 0)
    //        {
    //            oCompany.GetLastError(out lErrCode, out sErrMsg);
    //            MessageBox.Show(lErrCode + " " + sErrMsg);
    //            return false;
    //        }
    //        else
    //        {
    //            MessageBox.Show("Payroll Successfully Posted");
    //            return true;
    //        }

    //        oCompany.Disconnect();
    //    }
    //    else
    //    {
    //        return false;
    //    }



    //}



    public static void CreateIncomingPayment(string _Branch, string _PayrolPeriod, int _DocEntry
        , string _CardCode, string _CardName, string _CashAccount, double _Amount, DateTime _DocDate)
    {

        string sysDftDBCompany = "DESIHOFC";
        string sysDBUsername = ConfigurationManager.AppSettings["sysSAPUsername"];
        string sysDBPassword = ConfigurationManager.AppSettings["sysSAPPassword"];

        bool isConnected = false;
        string _Msg = "";
        oCompany = SAPInitializeConnection(sysDftDBCompany, sysDBUsername, sysDBPassword, out isConnected, out _Msg);
        //MessageBox.Show(_Msg);

        if (isConnected == true)
        {
            SAPbobsCOM.Payments _Payments;
            _Payments = (SAPbobsCOM.Payments)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oIncomingPayments);

            
            _Payments.CardCode = _CardCode;
            _Payments.CardName = _CardName;
            _Payments.CashAccount = _CashAccount;
            _Payments.CashSum = _Amount;
            _Payments.DocDate = _DocDate;
            _Payments.JournalRemarks = "Integra Console " + _PayrolPeriod + @" Branch : " + _Branch;
            _Payments.TaxDate = _DocDate;
            

            _Payments.Invoices.DocEntry = _DocEntry;
            _Payments.Invoices.DocLine = 0;
            _Payments.Invoices.InvoiceType = SAPbobsCOM.BoRcptInvTypes.it_Invoice;
            _Payments.Invoices.SumApplied = _Amount;
            

            int lRetCode;
            string sErrMsg;
            int lErrCode;
            Application.DoEvents();
            lRetCode = _Payments.Add();

            if (lRetCode != 0)
            {
                oCompany.GetLastError(out lErrCode, out sErrMsg);
                //MessageBox.Show(lErrCode + " " + sErrMsg);
            }
            else
            {
                //MessageBox.Show("Payroll Successfully Posted");
            }

            oCompany.Disconnect();
        }
        else
        {
            MessageBox.Show(_Msg);
        }



    }




    public static void CreateARInvoice(DataTable _tblData)
    {
        SAPbobsCOM.Documents _ARInvoice;
        _ARInvoice = (SAPbobsCOM.Documents)clsSAPFunctions.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices);

        string _ExcelFile = "";
        foreach (DataRow row in _tblData.Rows)
        {
            //row["ExcelFile"].ToString()

            _ARInvoice.DocDate = DateTime.Parse(row["SalesDocDate"].ToString()) ;
            _ARInvoice.TaxDate = DateTime.Parse(row["SalesDocDate"].ToString()) ;
            _ExcelFile = row["ExcelFile"].ToString();

            _ARInvoice.CardCode = row["CardCode"].ToString();
            _ARInvoice.NumAtCard = row["ExcelFile"].ToString().Replace(".xls", "").Replace(".xlsx", "");
            _ARInvoice.UserFields.Fields.Item("U_WhsCode").Value = row["Branch"].ToString();

            _ARInvoice.Comments = "Auto Created By Add-on | File Name : " + row["ExcelFile"].ToString() + "  | Branch : " + row["Branch"].ToString();
            _ARInvoice.JournalMemo = row["ExcelFile"].ToString().Replace(".xls", "").Replace(".xlsx", "");

            break;
        }


        int i = 0;
        foreach (DataRow row in _tblData.Rows)
        {
            if (i != 0)
            {
                _ARInvoice.Lines.Add();
            }

            string _ItemCode = row["ItemCode"].ToString();
            string _Branch = row["Branch"].ToString();

            _ARInvoice.Lines.ItemCode = row["ItemCode"].ToString();
            //_ARInvoice.Lines.ItemDescription = row["Dscription"].ToString();

            double _Quantity = double.Parse(row["Quantity"].ToString());
            _ARInvoice.Lines.UseBaseUnits = SAPbobsCOM.BoYesNoEnum.tYES;
            _ARInvoice.Lines.InventoryQuantity = double.Parse(row["Quantity"].ToString());
            _ARInvoice.Lines.PriceAfterVAT = double.Parse(row["SAPPrice"].ToString());
            _ARInvoice.Lines.WarehouseCode = row["Branch"].ToString();
            _ARInvoice.Lines.UserFields.Fields.Item("U_PurchPrice").Value = row["Price"].ToString();
            _ARInvoice.Lines.UserFields.Fields.Item("U_Amount").Value = row["Amount"].ToString();


            string _sqlBatch = @"
                                                SELECT T1.[ItemCode], T1.[SysNumber], T1.[DistNumber], T1.[MnfSerial], T1.[LotNumber], T1.[ExpDate], T1.[MnfDate], T1.[InDate], T1.[GrntStart],
                                                    T1.[GrntExp], T1.[CreateDate], T1.[Location], T1.[Status], T1.[Notes], T1.[DataSource], T1.[UserSign], T1.[Transfered], T1.[Instance], T1.[AbsEntry], 
                                                    T1.[ObjType], T1.[itemName], T1.[LogInstanc], T1.[UserSign2], T1.[UpdateDate], T1.[CostTotal], T1.[Quantity], T1.[QuantOut], T1.[PriceDiff], 
                                                    T1.[Balance], T1.[TrackingNt], T1.[TrackiNtLn], T1.[SumDec], T0.[GET_AVAIL_RES_QTY_COL_ALIAS], T0.[GET_AVAIL_RES_COMMIT_QTY_COL_ALIAS], 
                                                    T0.[GET_AVAIL_RES_COUNTED_QTY_COL_ALIAS], T2.[ItemCode], T2.[SysNumber], T2.[WhsCode], T2.[Location], T2.[DataSource], T2.[UserSign], 
                                                    T2.[Transfered], T2.[Instance], T2.[AbsEntry], T2.[MdAbsEntry], T2.[ObjType], T2.[LogInstanc], T2.[UserSign2], T2.[UpdateDate] 
                                                    FROM  (SELECT Z.[ItemCode] AS 'GET_AVAIL_RES_ITEM_COL_ALIAS', Z.[SysNumber] AS 'GET_AVAIL_RES_SYS_NUM_COL_ALIAS', Z.[MdAbsEntry] AS 'GET_AVAIL_RES_ABS_ENTRY_COL_ALIAS', 
                                                Z.[Quantity] AS 'GET_AVAIL_RES_QTY_COL_ALIAS', Z.[CommitQty] AS 'GET_AVAIL_RES_COMMIT_QTY_COL_ALIAS', Z.[CountQty] AS 'GET_AVAIL_RES_COUNTED_QTY_COL_ALIAS' 
                                                FROM  [dbo].[OBTQ] Z  INNER  JOIN [dbo].[OBTN] X  ON  X.[ItemCode] = Z.[ItemCode]  AND  X.[SysNumber] = Z.[SysNumber]   WHERE Z.[ItemCode] = (N'" + _ItemCode + @"' )  
                                                AND  Z.[WhsCode] = ('" + _Branch + @"' )  AND  X.[Status] <= ('0')  AND  Z.[Quantity] <> (0)  
                                                ) T0  INNER  JOIN [dbo].[OBTN] T1  ON  T1.[AbsEntry] = T0.[GET_AVAIL_RES_ABS_ENTRY_COL_ALIAS]    
                                                    LEFT OUTER  JOIN [dbo].[OBTW] T2  ON  T2.[MdAbsEntry] = T0.[GET_AVAIL_RES_ABS_ENTRY_COL_ALIAS]  AND  T2.[WhsCode] = N'" + _Branch + @"'   
                                                    ORDER BY T1.[AbsEntry],T1.[ExpDate]
                                             ";


            DataTable _tblBatch;
            _tblBatch = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlBatch);

            double _reqQuantity = _Quantity;
            foreach (DataRow rowBatch in _tblBatch.Rows)
            {
                string _DistNumber = rowBatch["DistNumber"].ToString();
                double _AvailableQty =double.Parse(rowBatch["GET_AVAIL_RES_QTY_COL_ALIAS"].ToString()) ;

                if (_AvailableQty >= _reqQuantity)
                {
                    _ARInvoice.Lines.BatchNumbers.BatchNumber = _DistNumber;
                    _ARInvoice.Lines.BatchNumbers.Quantity = _reqQuantity;
                    _ARInvoice.Lines.BatchNumbers.Add();

                    break;
                }
                else
                {
                    _ARInvoice.Lines.BatchNumbers.BatchNumber = _DistNumber;
                    _ARInvoice.Lines.BatchNumbers.Quantity = _AvailableQty;
                    _ARInvoice.Lines.BatchNumbers.Add();
                    _reqQuantity = _reqQuantity - _AvailableQty;
                }
            }

                i++;
        }






        int lRetCode;
        string sErrMsg;
        int lErrCode;
        Application.DoEvents();
        lRetCode = _ARInvoice.Add();

        if (lRetCode != 0)
        {
            oCompany.GetLastError(out lErrCode, out sErrMsg);
            string _err = lErrCode + " : " + sErrMsg;
            string _SQLSyntax = @"UPDATE A SET  A.[SAPError] = '" + _err.Replace("'","''") + @"' 
                                                    FROM [SalesData] A  WHERE A.[ExcelFile] = '" + _ExcelFile + @"'
                                                ";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);
            MessageBox.Show(lErrCode + " : " + sErrMsg);
        }
        else
        {


            string _SQLSyntax = @"UPDATE A SET A.[Uploaded] = 'Y', A.[SAPError] = '' , A.DateUploaded = GETDATE() 
                                                    FROM [SalesData] A  WHERE A.[ExcelFile] = '" + _ExcelFile + @"'
                                                ";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);

            MessageBox.Show("Sales Liquidation Successfully Posted");
        }

    }





    public static void CreateStockTransfer(DataTable _tblData)
    {
        int lRetCode;
        string sErrMsg;
        int lErrCode;

        SAPbobsCOM.StockTransfer _StockTransferRequest;
        _StockTransferRequest = (SAPbobsCOM.StockTransfer)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oStockTransfer);

        _StockTransferRequest.DocDate = DateTime.Parse(DateTime.Today.ToShortDateString());
        _StockTransferRequest.TaxDate = DateTime.Parse(DateTime.Today.ToShortDateString());


        _StockTransferRequest.UserFields.Fields.Item("U_Del_Status").Value = "Received";
        _StockTransferRequest.UserFields.Fields.Item("U_STF_Return_Reason").Value = "STOCK TRANSFER";

        int i = 0;
        foreach (DataRow row in _tblData.Rows)
        {
            if (i != 0)
                _StockTransferRequest.Lines.Add();

            string _ItemCode = row["ItemCode"].ToString();
            string _MainWhsCode = row["MainWhsCode"].ToString();
            string _Branch = row["Branch"].ToString();
            double _Quantity = double.Parse(row["Quantity"].ToString());


            _StockTransferRequest.Lines.ItemCode = _ItemCode;
            _StockTransferRequest.Lines.Quantity = _Quantity;

            _StockTransferRequest.Lines.FromWarehouseCode = _MainWhsCode;
            _StockTransferRequest.Lines.WarehouseCode = _Branch;

            

            string _sqlBatch = @"
                                                SELECT T1.[ItemCode], T1.[SysNumber], T1.[DistNumber], T1.[MnfSerial], T1.[LotNumber], T1.[ExpDate], T1.[MnfDate], T1.[InDate], T1.[GrntStart],
                                                    T1.[GrntExp], T1.[CreateDate], T1.[Location], T1.[Status], T1.[Notes], T1.[DataSource], T1.[UserSign], T1.[Transfered], T1.[Instance], T1.[AbsEntry], 
                                                    T1.[ObjType], T1.[itemName], T1.[LogInstanc], T1.[UserSign2], T1.[UpdateDate], T1.[CostTotal], T1.[Quantity], T1.[QuantOut], T1.[PriceDiff], 
                                                    T1.[Balance], T1.[TrackingNt], T1.[TrackiNtLn], T1.[SumDec], T0.[GET_AVAIL_RES_QTY_COL_ALIAS], T0.[GET_AVAIL_RES_COMMIT_QTY_COL_ALIAS], 
                                                    T0.[GET_AVAIL_RES_COUNTED_QTY_COL_ALIAS], T2.[ItemCode], T2.[SysNumber], T2.[WhsCode], T2.[Location], T2.[DataSource], T2.[UserSign], 
                                                    T2.[Transfered], T2.[Instance], T2.[AbsEntry], T2.[MdAbsEntry], T2.[ObjType], T2.[LogInstanc], T2.[UserSign2], T2.[UpdateDate] 
                                                    FROM  (SELECT Z.[ItemCode] AS 'GET_AVAIL_RES_ITEM_COL_ALIAS', Z.[SysNumber] AS 'GET_AVAIL_RES_SYS_NUM_COL_ALIAS', Z.[MdAbsEntry] AS 'GET_AVAIL_RES_ABS_ENTRY_COL_ALIAS', 
                                                Z.[Quantity] AS 'GET_AVAIL_RES_QTY_COL_ALIAS', Z.[CommitQty] AS 'GET_AVAIL_RES_COMMIT_QTY_COL_ALIAS', Z.[CountQty] AS 'GET_AVAIL_RES_COUNTED_QTY_COL_ALIAS' 
                                                FROM  [dbo].[OBTQ] Z  INNER  JOIN [dbo].[OBTN] X  ON  X.[ItemCode] = Z.[ItemCode]  AND  X.[SysNumber] = Z.[SysNumber]   WHERE Z.[ItemCode] = (N'" + _ItemCode + @"' )  
                                                AND  Z.[WhsCode] = ('" + _MainWhsCode + @"' )  AND  X.[Status] <= ('0')  AND  Z.[Quantity] <> (0)  
                                                ) T0  INNER  JOIN [dbo].[OBTN] T1  ON  T1.[AbsEntry] = T0.[GET_AVAIL_RES_ABS_ENTRY_COL_ALIAS]    
                                                    LEFT OUTER  JOIN [dbo].[OBTW] T2  ON  T2.[MdAbsEntry] = T0.[GET_AVAIL_RES_ABS_ENTRY_COL_ALIAS]  AND  T2.[WhsCode] = N'" + _MainWhsCode + @"'   
                                                    ORDER BY T1.[AbsEntry],T1.[ExpDate]
                                             ";


            DataTable _tblBatch;
            _tblBatch = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlBatch);

            double _reqQuantity = _Quantity;
            foreach (DataRow rowBatch in _tblBatch.Rows)
            {
                string _DistNumber = rowBatch["DistNumber"].ToString();
                double _AvailableQty = double.Parse(rowBatch["GET_AVAIL_RES_QTY_COL_ALIAS"].ToString());

                if (_AvailableQty >= _reqQuantity)
                {
                    _StockTransferRequest.Lines.BatchNumbers.BatchNumber = _DistNumber;
                    _StockTransferRequest.Lines.BatchNumbers.Quantity = _reqQuantity;
                    _StockTransferRequest.Lines.BatchNumbers.Add();

                    break;
                }
                else
                {
                    _StockTransferRequest.Lines.BatchNumbers.BatchNumber = _DistNumber;
                    _StockTransferRequest.Lines.BatchNumbers.Quantity = _AvailableQty;
                    _StockTransferRequest.Lines.BatchNumbers.Add();
                    _reqQuantity = _reqQuantity - _AvailableQty;
                }
            }



            i++;
        }

        Application.DoEvents();
        lRetCode = _StockTransferRequest.Add();

        if (lRetCode != 0)
        {
            oCompany.GetLastError(out lErrCode, out sErrMsg);
            MessageBox.Show(lErrCode + " " + sErrMsg);
        }
        else
        {
            MessageBox.Show("Stock Transfer Successfully Added");
        }
    }


    public static void CreatePriceList(string _Code, string _Name)
    {
        SAPbobsCOM.GeneralService oDocGeneralService;
        SAPbobsCOM.GeneralData oDocGeneralData;
        SAPbobsCOM.GeneralDataCollection oDocLinesCollection;
        SAPbobsCOM.GeneralData oDocLineGeneralData;

        SAPbobsCOM.CompanyService oCompService;
        oCompService = oCompany.GetCompanyService();

        // Retrieve the relevant service
        oDocGeneralService = oCompService.GetGeneralService("OCRP");
        // Point to the Header of the Doc UDO
        oDocGeneralData = (SAPbobsCOM.GeneralData)oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);

        // Insert values to the Header properties
        oDocGeneralData.SetProperty("Code", _Code);
        oDocGeneralData.SetProperty("Name", _Name);

        string _sqlPriceList;
        DataTable _tblPriceList = new DataTable();
        _sqlPriceList = @"
                                    SELECT A.ItmsGrpCod, A.ItmsGrpNam
                                    , 0.00 AS [VISMIN DISC %]
                                    , 0.00 AS [GMA DISC %]
                                    FROM OITB A
                                    ";
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);
        

        foreach (DataRow row in _tblPriceList.Rows)
        {
            string _ItmsGrpNam = row["ItmsGrpNam"].ToString();
            string _VisMinDisc = row["VISMIN DISC %"].ToString();
            string _GMADisc = row["GMA DISC %"].ToString();

            oDocLinesCollection = oDocGeneralData.Child("OCPL");
            oDocLineGeneralData = oDocLinesCollection.Add();
            oDocLineGeneralData.SetProperty("U_Category", _ItmsGrpNam);
            oDocLineGeneralData.SetProperty("U_VisMinDisc", _VisMinDisc);
            oDocLineGeneralData.SetProperty("U_GMADisc", _GMADisc);
        }


        _sqlPriceList = @"
                                        SELECT A.ItemCode, A.ItemName FROM OITM A 
                                    ";
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);


        foreach (DataRow row in _tblPriceList.Rows)
        {
            string _ItemCode = row["ItemCode"].ToString();
            string _ItemName = row["ItemName"].ToString();
            string _Value = "0.00";

            oDocLinesCollection = oDocGeneralData.Child("OIPL");
            oDocLineGeneralData = oDocLinesCollection.Add();
            oDocLineGeneralData.SetProperty("U_ItemCode", _ItemCode);
            oDocLineGeneralData.SetProperty("U_ItemName", _ItemName);
            oDocLineGeneralData.SetProperty("U_VisMinPrice", _Value);
            oDocLineGeneralData.SetProperty("U_GMAPrice", _Value);
            oDocLineGeneralData.SetProperty("U_VisMinDisc", _Value);
            oDocLineGeneralData.SetProperty("U_GMADisc", _Value);
            oDocLineGeneralData.SetProperty("U_VisMinNet", _Value);
            oDocLineGeneralData.SetProperty("U_GMANet", _Value);
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

        if (oCompany.InTransaction)
        {
            oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
            //SBO_Application.MessageBox("Add Doc UDO Header and Lines successfully", 1, "", "", "");
        }
        else
        {
            //SBO_Application.MessageBox("Error adding object", 1, "", "", "");
        }
    }

    public static void CreateBranchPriceList(string _Code, string _Name,string _Area, string _PriceList)
    {
        SAPbobsCOM.GeneralService oDocGeneralService;
        SAPbobsCOM.GeneralData oDocGeneralData;
        SAPbobsCOM.GeneralDataCollection oDocLinesCollection;
        SAPbobsCOM.GeneralData oDocLineGeneralData;

        SAPbobsCOM.CompanyService oCompService;
        oCompService = oCompany.GetCompanyService();

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
                         , CASE WHEN 'GMA' = '" + _Area  + @"' THEN A.U_GMADisc ELSE A.U_VisMinDisc END AS Discount
                    FROM [@OCPL] A WHERE A.Code = '" + _PriceList  + @"'
                                    ";
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);


        foreach (DataRow row in _tblPriceList.Rows)
        {
            string _ItmsGrpNam = row["ItmsGrpNam"].ToString();
            string _Disc = row["Discount"].ToString();


            oDocLinesCollection = oDocGeneralData.Child("BPL2");
            oDocLineGeneralData = oDocLinesCollection.Add();
            oDocLineGeneralData.SetProperty("U_Category", _ItmsGrpNam);
            oDocLineGeneralData.SetProperty("U_Discount", _Disc);
            oDocLineGeneralData.SetProperty("U_Frozen", "N");
        }


        _sqlPriceList = @"
                          SELECT A.U_ItemCode AS ItemCode, A.U_ItemName AS ItemName
						  , CASE WHEN 'GMA' = '" + _Area + @"' THEN A.U_GMAPrice ELSE A.U_VisMinPrice END AS Price 
						  , CASE WHEN 'GMA' = '" + _Area + @"' THEN A.U_GMADisc ELSE A.U_VisMinDisc END AS Discount 
						  , CASE WHEN 'GMA' = '" + _Area + @"' THEN A.U_GMANet ELSE A.U_VisMinNet END AS NetAmt 
						   FROM [@OIPL] A WHERE A.Code =  '" + _PriceList + @"'
                                    ";
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);


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

        if (oCompany.InTransaction)
        {
            oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
            //SBO_Application.MessageBox("Add Doc UDO Header and Lines successfully", 1, "", "", "");
        }
        else
        {
            //SBO_Application.MessageBox("Error adding object", 1, "", "", "");
        }
    }





    public static void UpdatePriceList(string _Code)
    {
        SAPbobsCOM.GeneralService oDocGeneralService;
        SAPbobsCOM.GeneralData oDocGeneralData;
        SAPbobsCOM.GeneralDataCollection oDocLinesCollection;
        //SAPbobsCOM.GeneralData oDocLineGeneralData;
        SAPbobsCOM.GeneralDataParams oDocGeneralParams;

        SAPbobsCOM.CompanyService oCompService;
        oCompService = oCompany.GetCompanyService();

        // Retrieve the relevant service
        oDocGeneralService = oCompService.GetGeneralService("OCRP");
        // Point to the Header of the Doc UDO
        oDocGeneralData = (SAPbobsCOM.GeneralData)oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);
        

        oDocGeneralParams = oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
        oDocGeneralParams.SetProperty("Code", _Code);
        oDocGeneralData = oDocGeneralService.GetByParams(oDocGeneralParams);

        //oGeneralData.SetProperty("U_Name", "Guy")
        

        string _sqlPriceList;
        DataTable _tblPriceList = new DataTable();
        _sqlPriceList = @"
                                SELECT A.U_Category, 0.00 AS VisMinDisc, 0.00 AS GMADisc, A.LineId FROM [@OCPL] A WHERE A.Code = '" + _Code + @"'
                         ";
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);


        foreach (DataRow row in _tblPriceList.Rows)
        {
            //string _ItmsGrpNam = row["U_Category"].ToString();
            string _VisMinDisc = row["VisMinDisc"].ToString();
            string _GMADisc = row["GMADisc"].ToString();
            string _LineId = row["LineId"].ToString();


            oDocLinesCollection = oDocGeneralData.Child("OCPL");

            //oDocLineGeneralData = oDocLinesCollection.Add();

            //oDocLinesCollection.Item(int.Parse(_LineId)).SetProperty("U_Category", _ItmsGrpNam);
            oDocLinesCollection.Item(int.Parse(_LineId) - 1).SetProperty("U_VisMinDisc", _VisMinDisc);
            oDocLinesCollection.Item(int.Parse(_LineId) - 1).SetProperty("U_GMADisc", _GMADisc);
        }




        _sqlPriceList = @"
                                        SELECT A.ItemCode, A.ItemName, A.LineId FROM OITM A 
                                    ";
        _tblPriceList = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlPriceList);


        foreach (DataRow row in _tblPriceList.Rows)
        {
            string _ItemCode = row["ItemCode"].ToString();
            string _ItemName = row["ItemName"].ToString();
            string _LineId = row["LineId"].ToString();
            string _Value = "0.00";

            oDocLinesCollection = oDocGeneralData.Child("OIPL");
            //oDocLineGeneralData = oDocLinesCollection.Add();
            oDocLinesCollection.Item(int.Parse(_LineId)).SetProperty("U_ItemCode", _ItemCode);
            oDocLinesCollection.Item(int.Parse(_LineId)).SetProperty("U_ItemName", _ItemName);
            oDocLinesCollection.Item(int.Parse(_LineId)).SetProperty("U_VisMinPrice", _Value);
            oDocLinesCollection.Item(int.Parse(_LineId)).SetProperty("U_GMAPrice", _Value);
            oDocLinesCollection.Item(int.Parse(_LineId)).SetProperty("U_VisMinDisc", _Value);
            oDocLinesCollection.Item(int.Parse(_LineId)).SetProperty("U_GMADisc", _Value);
            oDocLinesCollection.Item(int.Parse(_LineId)).SetProperty("U_VisMinNet", _Value);
            oDocLinesCollection.Item(int.Parse(_LineId)).SetProperty("U_GMANet", _Value);
        }



        // Insert Values to the Lines properties try
        try
        {
            oDocGeneralService.Update(oDocGeneralData);
        }
        catch
        {
            //oDocGeneralService.Update(oDocGeneralData);
        }


        if (oCompany.InTransaction)
        {
            oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
            MessageBox.Show("Price List Successfully Updated");
            //SBO_Application.MessageBox("Add Doc UDO Header and Lines successfully", 1, "", "", "");
        }
        else
        {
            MessageBox.Show("Error in Updating Price List");
            //SBO_Application.MessageBox("Error adding object", 1, "", "", "");
        }
    }





    public static void ColorData()
    {
        SAPbobsCOM.GeneralService oDocGeneralService;
        SAPbobsCOM.GeneralData oDocGeneralData;
        SAPbobsCOM.GeneralDataCollection oDocLinesCollection;
        SAPbobsCOM.GeneralData oDocLineGeneralData;


        string _sqlColorDetails = @"SELECT A.Code, A.Name FROM [@COLOR_DETAILS] A";
        DataTable _tblColorDetails = new DataTable();
        _tblColorDetails = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlColorDetails);

        foreach (DataRow rColor in _tblColorDetails.Rows)
        {
            SAPbobsCOM.CompanyService oCompService;
            oCompService = oCompany.GetCompanyService();

            // Retrieve the relevant service
            oDocGeneralService = oCompService.GetGeneralService("Color");
            // Point to the Header of the Doc UDO
            oDocGeneralData = (SAPbobsCOM.GeneralData)oDocGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);

            string _Code = rColor["Code"].ToString();
            string _Name = rColor["Name"].ToString();

            // Insert values to the Header properties
            oDocGeneralData.SetProperty("Code", _Code);
            oDocGeneralData.SetProperty("Name", _Name);

            string _sqlColorSelection = @"SELECT A.U_Color, A.U_PNPCOLOR, A.U_ColorCode FROM [@COLOR_SELECTION] A WHERE A.Code = '" + _Code + @"'";
            DataTable _tblColorSelection = new DataTable();
            _tblColorSelection = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlColorSelection);


            foreach (DataRow rColorSelection in _tblColorSelection.Rows)
            {
                string _Color = rColorSelection["U_Color"].ToString();
                string _PNPCOLOR = rColorSelection["U_PNPCOLOR"].ToString();
                string _ColorCode = rColorSelection["U_ColorCode"].ToString();

                oDocLinesCollection = oDocGeneralData.Child("COLOR_SELECTION");
                oDocLineGeneralData = oDocLinesCollection.Add();
                oDocLineGeneralData.SetProperty("U_Color", _Color);
                oDocLineGeneralData.SetProperty("U_PNPCOLOR", _PNPCOLOR);
                oDocLineGeneralData.SetProperty("U_ColorCode", _ColorCode);
            }



            // Insert Values to the Lines properties try
            try
            {
                oDocGeneralService.Add(oDocGeneralData);
            }
            catch
            {
                //oDocGeneralService.Update(oDocGeneralData);
            }

            if (oCompany.InTransaction)
            {
                oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                //SBO_Application.MessageBox("Add Doc UDO Header and Lines successfully", 1, "", "", "");
            }
            else
            {
                //SBO_Application.MessageBox("Error adding object", 1, "", "", "");
            }

        }


    }

}