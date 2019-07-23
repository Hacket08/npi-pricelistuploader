using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.IO;
using System.Windows.Forms;
using System.Data;

using System.Data.SqlClient;

class clsExcelFileReading
{


    public static DataTable Excel_CRUST00001(DataTable _DataTable, string _Branch)
    {
        foreach (DataRow row in _DataTable.Rows)
        {
            try
            {
                Application.DoEvents();
                string _ItemSKU = Microsoft.VisualBasic.Strings.Right(row[3].ToString(), 6);

                if(_ItemSKU != "")
                {
                    string _sqlSelect = @"SELECT A.ItemCode FROM 
                        OITW A 
                        WHERE A.U_branch_sku = '" + _ItemSKU + @"' AND A.WhsCode = '" + _Branch + @"' ";
                    DataTable _table = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlSelect);
                    string _ItemCode = clsSQLClientFunctions.GetData(_table, "ItemCode", "0");
                    row[0] = _ItemCode;
                }


            }
            catch
            {
            }
        }

        return _DataTable;
    }


}