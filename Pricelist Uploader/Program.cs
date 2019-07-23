using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using System.Configuration;

namespace Pricelist_Uploader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            clsDeclaration.sSAPConnection = clsSQLClientFunctions.GlobalConnectionString(
               ConfigurationManager.AppSettings["sysDBServer"],
                ConfigurationManager.AppSettings["sysDftDBCompany"],
                ConfigurationManager.AppSettings["sysDBUsername"],
                ConfigurationManager.AppSettings["sysDBPassword"]);

            clsDeclaration.sSystemConnection = clsSQLClientFunctions.GlobalConnectionString(
               ConfigurationManager.AppSettings["DBServer"],
                ConfigurationManager.AppSettings["DBName"],
                ConfigurationManager.AppSettings["DBUsername"],
                ConfigurationManager.AppSettings["DBPassword"]);

            //clsDeclaration.isConnected = false;
            //string _errMsg = "";
            //clsSAPFunctions.oCompany = clsSAPFunctions.SAPInitializeConnection(ConfigurationManager.AppSettings["sysDftDBCompany"], ConfigurationManager.AppSettings["sysSAPUsername"], ConfigurationManager.AppSettings["sysSAPPassword"], out clsDeclaration.isConnected, out _errMsg
            //    );
            //MessageBox.Show(_errMsg);

            Application.Run(new frmMainWindow());
        }
    }
}
