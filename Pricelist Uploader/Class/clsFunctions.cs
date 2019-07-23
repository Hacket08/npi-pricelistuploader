using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;



using System.IO;
using System.Net;
using System.Configuration;

using System.Management;

public static class clsFunctions
{
    public static int GetAge(DateTime dateOfBirth)
    {
        int age = DateTime.Now.Year - dateOfBirth.Year;
        if (dateOfBirth.AddYears(age) > DateTime.Now)
        {
            age = age - 1;
        }

        return age;
    }


    public static void SetSetting(string key, string value)
    {
        Configuration configuration = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location);
        configuration.AppSettings.Settings[key].Value = value;
        configuration.Save(ConfigurationSaveMode.Full, false);
        ConfigurationManager.RefreshSection("appSettings");
    }

    public static void btnSetup(Form frmSetup ,Button[,] btn_Setup, Panel pnlLocation, string ControlName, int Width, int Height)
    {
        int x = 0;

        for (int i = 0; i < btn_Setup.GetLength(0); i++)
        {

            for (int j = 0; j < btn_Setup.GetLength(1); j++)
            {
                //set index for button array
                x++;
                int index = x;

                // instance the control
                btn_Setup[i, j] = new Button();
                // set some initial properties
                btn_Setup[i, j].Name = ControlName + "_" + i.ToString() + "_" + j.ToString();
                btn_Setup[i, j].Text = x.ToString(); //"";
                btn_Setup[i, j].AccessibleDescription = ControlName;
                btn_Setup[i, j].Tag = x.ToString(); //"";
                btn_Setup[i, j].Cursor = Cursors.Hand;
               


                //switch (x)
                //{
                //    case 10:
                //        btn_Setup[i, j].Text = "OK"; //"";
                //        break;
                //    case 11:
                //        btn_Setup[i, j].Text = "0"; //"";
                //        break;
                //    case 12:
                //        btn_Setup[i, j].Text = "CANCEL"; //"";
                //        break;
                //    default:
                //        btn_Setup[i, j].Text = x.ToString(); //"";
                //        break;
                //}


                // add to form
                frmSetup.Controls.Add(btn_Setup[i, j]);

                btn_Setup[i, j].Parent = pnlLocation;
                // set position and size
                btn_Setup[i, j].Location = new Point(0 + j * Width, 0 + i * Height);
                btn_Setup[i, j].Size = new Size(Width, Height);

                //btn_Setup[i, j].Click += (sender, e) => this.Display(index);
                //btn_Setup[i, j].Click += new EventHandler(btn_Array_Click);
            }
        }


    }

    public static void lblSetup(Form frmSetup, Label[,] lbl_Setup, Panel pnlLocation, string ControlName, int Width, int Height)
    {
        int x = 0;

        for (int i = 0; i < lbl_Setup.GetLength(0); i++)
        {

            for (int j = 0; j < lbl_Setup.GetLength(1); j++)
            {
                //set index for button array
                x++;
                int index = x;

                // instance the control
                lbl_Setup[i, j] = new Label();
                // set some initial properties
                lbl_Setup[i, j].Name = ControlName + "_" + i.ToString() + "_" + j.ToString();
                lbl_Setup[i, j].Text = x.ToString(); //"";
                lbl_Setup[i, j].AccessibleDescription = ControlName;
                lbl_Setup[i, j].Tag = x.ToString(); //"";
                //lbl_Setup[i, j].BorderStyle = BorderStyle.FixedSingle;
  
                // add to form
                frmSetup.Controls.Add(lbl_Setup[i, j]);

                lbl_Setup[i, j].Parent = pnlLocation;
                // set position and size
                lbl_Setup[i, j].Location = new Point(0 + j * Width, 0 + i * Height);
                lbl_Setup[i, j].Size = new Size(Width, Height);

                //btn_Setup[i, j].Click += (sender, e) => this.Display(index);
                //btn_Setup[i, j].Click += new EventHandler(btn_Array_Click);
            }
        }


    }


    public static string SystemSettingValue(string _VarID)
    {
        string _sqlAccountCode = @"SELECT A.VariableValue FROM SysVariables A WHERE A.VarID = '" + _VarID + "'";
        return clsSQLClientFunctions.GetStringValue(clsDeclaration.sSystemConnection, _sqlAccountCode, "VariableValue");
    }


    public static void SaveSettingValue(string _VarID, string _VarValue)
    {
        string _sqlAccountCode = @"UPDATE A SET A.VariableValue = '" + _VarValue + "' FROM SysVariables A WHERE A.VarID = '" + _VarID + "'";
        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _sqlAccountCode);
    }
    public static void DataGridViewSetup(DataGridView _DataGridView, DataTable _DataTable, string _Group = "")
    {
        try
        {
            _DataGridView.MultiSelect = true;
            _DataGridView.RowTemplate.Resizable = DataGridViewTriState.False;
            _DataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _DataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(234, 241, 246);
            _DataGridView.DefaultCellStyle.SelectionForeColor = Color.Black;

            _DataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            _DataGridView.BackgroundColor = Color.FromArgb(234, 241, 246);
            _DataGridView.GridColor = Color.Gray;
            _DataGridView.BorderStyle = BorderStyle.FixedSingle;
            _DataGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            _DataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            _DataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            _DataGridView.RowHeadersWidth = 50;

            _DataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 7);
            _DataGridView.RowsDefaultCellStyle.Font = new Font("Segoe UI", 7);
            _DataGridView.RowHeadersDefaultCellStyle.Font = new Font("Segoe UI", 7, FontStyle.Bold);
            //'.RowTemplate.Height = 15
            //'.Rows.Add(10)
            _DataGridView.RowHeadersVisible = false;
            _DataGridView.AllowUserToAddRows = false;
            _DataGridView.AllowUserToDeleteRows = false;


            if (_Group != "Permission")
            {
                _DataGridView.DataSource = _DataTable;
            }
           


            foreach (DataGridViewColumn column in _DataGridView.Columns)
            {
                switch (_Group)
                {
                    case "ItemGroupPriceList":
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                        break;
                }

                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                column.Width = 100;
                column.DefaultCellStyle.BackColor = Color.FromArgb(234, 241, 246);
                column.DefaultCellStyle.SelectionBackColor = Color.FromArgb(254, 240, 158);
                column.ReadOnly = true;
            }



            switch (_Group)
            {
                case "MasterItemList":

                    _DataGridView.Columns[3].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[3].ReadOnly = false;
                    _DataGridView.Columns[3].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;

                    _DataGridView.Columns[4].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[5].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[6].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


                    _DataGridView.Columns[7].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[7].ReadOnly = false;
                    _DataGridView.Columns[7].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;

                    _DataGridView.Columns[8].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[9].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[10].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    break;
                case "BranchItemList":
                    _DataGridView.Columns[3].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    _DataGridView.Columns[4].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


                    _DataGridView.Columns[5].DefaultCellStyle.Format = "N5";
                    _DataGridView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    _DataGridView.Columns[6].DefaultCellStyle.Format = "N5";
                    _DataGridView.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[6].ReadOnly = false;
                    _DataGridView.Columns[6].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;


                    _DataGridView.Columns[7].ReadOnly = false;
                    //_DataGridView.Columns[4].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                    break;

                case "BranchItemGroupList":
                    _DataGridView.Columns[2].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    _DataGridView.Columns[3].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[3].ReadOnly = false;
                    _DataGridView.Columns[3].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;

                    _DataGridView.Columns[4].ReadOnly = false;
                    //_DataGridView.Columns[4].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                    break;
                case "BranchPriceList":
                    _DataGridView.Columns[3].ReadOnly = false;
                    //_DataGridView.Columns[3].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                    break;
                case "ItemGroupPriceList":
                    _DataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                


                    _DataGridView.Columns[2].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[2].ReadOnly = false;
                    _DataGridView.Columns[2].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;


                    _DataGridView.Columns[3].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[3].ReadOnly = false;
                    _DataGridView.Columns[3].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                    break;
                case "ItemPriceList":

                    _DataGridView.Columns[3].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[3].ReadOnly = false;
                    _DataGridView.Columns[3].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;


                    _DataGridView.Columns[4].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[5].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[6].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    break;
                case "Liquidation":
                    //_DataGridView.Columns[1].DefaultCellStyle.Format = "N2";
                    //_DataGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    break;
                case "Promotion":
                    _DataGridView.Columns[0].Visible = false;
                    _DataGridView.Columns[1].DefaultCellStyle.Format = "MM/dd/yyyy";

                    _DataGridView.Columns[3].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    break;
                case "PayrollTransaction":

                    _DataGridView.Columns[4].ReadOnly = false;
                    _DataGridView.Columns[4].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;

                    _DataGridView.Columns[5].ReadOnly = false;
                    _DataGridView.Columns[5].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;

                    break;
                case "PayrollProcess":
                    _DataGridView.Columns[3].Frozen = true;

                    _DataGridView.Columns[3].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    _DataGridView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    _DataGridView.Columns[5].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


                    _DataGridView.Columns[7].DefaultCellStyle.Format = "N5";
                    _DataGridView.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[8].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[9].DefaultCellStyle.Format = "N5";
                    _DataGridView.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[10].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[11].DefaultCellStyle.Format = "N5";
                    _DataGridView.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[12].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[13].DefaultCellStyle.Format = "N5";
                    _DataGridView.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[14].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[15].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[15].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[16].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[16].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[17].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[17].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[18].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[18].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[19].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[19].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


                    _DataGridView.Columns[20].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[20].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[21].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[21].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[22].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[22].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[23].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[23].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[24].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[24].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[25].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[25].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[26].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[26].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[27].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[27].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[28].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[28].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[29].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[29].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[30].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[30].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[31].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[31].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[32].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[32].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


                    _DataGridView.Columns[33].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[33].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[34].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[34].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[35].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[35].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


                    _DataGridView.Columns[28].DisplayIndex = 35;
                    _DataGridView.Columns[29].DisplayIndex = 35;



                    break;
                case "SSS": // SSS TABLE
                    _DataGridView.MultiSelect = false;

                    _DataGridView.Columns[0].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[1].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[2].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[3].DefaultCellStyle.Format = "N2";
                    _DataGridView.Columns[4].DefaultCellStyle.Format = "N2";

                    _DataGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    _DataGridView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    break;

                case "PayrollUpdate": // Apyroll Update Table


                    foreach (DataGridViewColumn column in _DataGridView.Columns)
                    {
                        column.Visible = false;


                        _DataGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        _DataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        _DataGridView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        _DataGridView.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        _DataGridView.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        _DataGridView.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        _DataGridView.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        _DataGridView.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        _DataGridView.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        _DataGridView.Columns[29].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        _DataGridView.Columns[35].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


                        _DataGridView.Columns[2].DefaultCellStyle.Format = "N2";
                        _DataGridView.Columns[3].DefaultCellStyle.Format = "N2";
                        _DataGridView.Columns[5].DefaultCellStyle.Format = "N2";
                        _DataGridView.Columns[6].DefaultCellStyle.Format = "N2";
                        _DataGridView.Columns[7].DefaultCellStyle.Format = "N2";
                        _DataGridView.Columns[8].DefaultCellStyle.Format = "N2";
                        _DataGridView.Columns[9].DefaultCellStyle.Format = "N2";
                        _DataGridView.Columns[10].DefaultCellStyle.Format = "N2";
                        _DataGridView.Columns[11].DefaultCellStyle.Format = "N2";
                        _DataGridView.Columns[29].DefaultCellStyle.Format = "N2";
                        _DataGridView.Columns[35].DefaultCellStyle.Format = "N2";

                        _DataGridView.Columns[0].Visible = true;
                        _DataGridView.Columns[1].Visible = true;
                        _DataGridView.Columns[2].Visible = false;
                        _DataGridView.Columns[3].Visible = false;
                        _DataGridView.Columns[4].Visible = false;
                        _DataGridView.Columns[5].Visible = true;
                        _DataGridView.Columns[6].Visible = true;
                        _DataGridView.Columns[7].Visible = true;
                        _DataGridView.Columns[8].Visible = true;
                        _DataGridView.Columns[9].Visible = true;
                        _DataGridView.Columns[10].Visible = true;
                        _DataGridView.Columns[11].Visible = true;
                        _DataGridView.Columns[29].Visible = true;
                        _DataGridView.Columns[35].Visible = true;

                        _DataGridView.Columns[29].DisplayIndex = 1;
                    }

                    break;
                case "ApprovedOT":
                    _DataGridView.Columns[0].Visible = false;
                    _DataGridView.Columns[1].Visible = false;


                    //DataGridViewCheckBoxColumn chk1 = new DataGridViewCheckBoxColumn();
                    //{
                    //    chk1.HeaderText = "##";
                    //    chk1.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    //    chk1.Width = 10;
                    //}
                    //_DataGridView.Columns.Insert(0, chk1);

                    break;
                case "UnderTime":

                    _DataGridView.Columns[0].Visible = false;
                    _DataGridView.Columns[1].Visible = false;

                    _DataGridView.Columns[5].DefaultCellStyle.Format = "hh:mm tt";
                    _DataGridView.Columns[6].DefaultCellStyle.Format = "hh:mm tt";
                   

                    DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                    {
                        chk.HeaderText = "##";
                        chk.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        chk.Width = 10;
                    }
                    _DataGridView.Columns.Insert(0, chk);
                    

                    break;
                case "Attendance":
                    _DataGridView.Columns[2].DefaultCellStyle.Format = "MM/dd/yyyy";

                    _DataGridView.Columns[3].DefaultCellStyle.Format = "hh:mm tt";
                    _DataGridView.Columns[4].DefaultCellStyle.Format = "hh:mm tt";
                    _DataGridView.Columns[5].DefaultCellStyle.Format = "hh:mm tt";
                    _DataGridView.Columns[6].DefaultCellStyle.Format = "hh:mm tt";


                   
                    
                    _DataGridView.Columns[17].DefaultCellStyle.Format = "hh:mm tt";
                    _DataGridView.Columns[18].DefaultCellStyle.Format = "hh:mm tt";
                    _DataGridView.Columns[19].DefaultCellStyle.Format = "hh:mm tt";
                    _DataGridView.Columns[20].DefaultCellStyle.Format = "hh:mm tt";

                    _DataGridView.Columns[24].DefaultCellStyle.Format = "hh:mm tt";
                    _DataGridView.Columns[25].DefaultCellStyle.Format = "hh:mm tt";

                    _DataGridView.Columns[4].Visible = false;
                    _DataGridView.Columns[5].Visible = false;


                    _DataGridView.Columns[7].Visible = false;
                    _DataGridView.Columns[8].Visible = false;
                    _DataGridView.Columns[9].Visible = false;
                    _DataGridView.Columns[10].Visible = false;
                    _DataGridView.Columns[11].Visible = false;
                    _DataGridView.Columns[12].Visible = false;
                    _DataGridView.Columns[13].Visible = false;
                    _DataGridView.Columns[14].Visible = false;
                    //_DataGridView.Columns[15].Visible = false;
                    _DataGridView.Columns[16].Visible = false;
                    _DataGridView.Columns[17].Visible = false;
                    _DataGridView.Columns[18].Visible = false;
                    _DataGridView.Columns[19].Visible = false;
                    _DataGridView.Columns[20].Visible = false;
                    //_DataGridView.Columns[21].Visible = false;
                    _DataGridView.Columns[22].Visible = true;
                    _DataGridView.Columns[23].Visible = true;

                    _DataGridView.Columns[24].Visible = true;
                    _DataGridView.Columns[25].Visible = true;


                    _DataGridView.Columns[24].DisplayIndex = 4;
                    _DataGridView.Columns[25].DisplayIndex = 5;

                    break;

                case "DTR":
                    _DataGridView.Columns[2].DefaultCellStyle.Format = "MM/dd/yyyy";

                    _DataGridView.Columns[3].DefaultCellStyle.Format = "hh:mm tt";
                    _DataGridView.Columns[4].DefaultCellStyle.Format = "hh:mm tt";
                    _DataGridView.Columns[5].DefaultCellStyle.Format = "hh:mm tt";
                    _DataGridView.Columns[6].DefaultCellStyle.Format = "hh:mm tt";




                    _DataGridView.Columns[17].DefaultCellStyle.Format = "hh:mm tt";
                    _DataGridView.Columns[18].DefaultCellStyle.Format = "hh:mm tt";
                    _DataGridView.Columns[19].DefaultCellStyle.Format = "hh:mm tt";
                    _DataGridView.Columns[20].DefaultCellStyle.Format = "hh:mm tt";

                    _DataGridView.Columns[24].DefaultCellStyle.Format = "hh:mm tt";
                    _DataGridView.Columns[25].DefaultCellStyle.Format = "hh:mm tt";

                    _DataGridView.Columns[4].Visible = false;
                    _DataGridView.Columns[5].Visible = false;


                    _DataGridView.Columns[7].Visible = false;
                    _DataGridView.Columns[8].Visible = false;
                    _DataGridView.Columns[9].Visible = false;
                    _DataGridView.Columns[10].Visible = false;
                    _DataGridView.Columns[11].Visible = false;
                    _DataGridView.Columns[12].Visible = false;
                    _DataGridView.Columns[13].Visible = false;
                    _DataGridView.Columns[14].Visible = false;
                    //_DataGridView.Columns[15].Visible = false;
                    _DataGridView.Columns[16].Visible = false;
                    _DataGridView.Columns[17].Visible = true;
                    _DataGridView.Columns[18].Visible = true;
                    _DataGridView.Columns[19].Visible = true;
                    _DataGridView.Columns[20].Visible = true;
                    //_DataGridView.Columns[21].Visible = false;
                    _DataGridView.Columns[22].Visible = true;
                    _DataGridView.Columns[23].Visible = true;

                    _DataGridView.Columns[24].Visible = true;
                    _DataGridView.Columns[25].Visible = true;


                    _DataGridView.Columns[24].DisplayIndex = 4;
                    _DataGridView.Columns[25].DisplayIndex = 5;

                    break;
                case "Branches":
                    //DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn();
                    //cmb.HeaderText = "Select Data";
                    //cmb.Name = "cmb";
                    ////cmb.MaxDropDownItems = 4;
                    //cmb.Items.Add("True");
                    //cmb.Items.Add("False");
                    //_DataGridView.Columns.Insert(4, cmb);

                    break;
                case "Permission":

                    //DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn();
                    //cmb.HeaderText = "Access";
                    //cmb.Name = "cmb";
                    //cmb.Items.Add("Full");
                    //cmb.Items.Add("No Access");
                    //_DataGridView.Columns.Add(cmb);

                    _DataGridView.Columns[0].ReadOnly = true;
                    _DataGridView.Columns[1].ReadOnly = true;
                    _DataGridView.Columns[2].ReadOnly = true;
                    _DataGridView.Columns[2].Visible = false;
                    _DataGridView.Columns[3].ReadOnly = false;
                    break;
                case "MasterData":
                    _DataGridView.Columns[2].Visible = false;
                    _DataGridView.Columns[3].Visible = false;
                    _DataGridView.Columns[4].Visible = false;

                    break;

                case "Deduction":
                    _DataGridView.Columns[0].Frozen = true;

                    break;
                default:
                    break;
            }



        }
        catch { }
         
    }


    /// <summary>
    /// Upload a file using FTP
    /// </summary>
    /// <param name="FTPServer">The server to upload to</param>
    /// <param name="remotePath">The remote path in the server</param>
    /// <param name="fileToUpload">The path to the local uploaded file</param>
    /// <param name="user">User to log onto the FTP server</param>
    /// <param name="password">Password to log onto the FTP server</param>
    /// <returns>The status of the upload</returns>
    public static bool FTPUpload(string FTPServer, string remotePath, string fileToUpload, string user, string password)
    {
        try
        {
            //Get FTP web resquest object.
            FtpWebRequest request = FtpWebRequest.Create(new Uri(@"ftp://" + FTPServer + @"/" + remotePath + @"/" + Path.GetFileName(fileToUpload))) as FtpWebRequest;
            request.UseBinary = true;
            request.KeepAlive = false;
            request.Method = WebRequestMethods.Ftp.UploadFile;
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(password))
                request.Credentials = new NetworkCredential(user, password);
            //Get physical file
            FileInfo fi = new FileInfo(fileToUpload);
            Byte[] contents = new Byte[fi.Length];
            //Read file
            FileStream fs = fi.OpenRead();
            fs.Read(contents, 0, Convert.ToInt32(fi.Length));
            fs.Close();
            //Write file contents to FTP server
            Stream rs = request.GetRequestStream();
            rs.Write(contents, 0, Convert.ToInt32(fi.Length));
            rs.Close();
            FtpWebResponse response = request.GetResponse() as FtpWebResponse;
            string statusDescription = response.StatusDescription;
            response.Close();
            return true;
            //return statusDescription;
        }
        catch
        {
            //throw new Exception("Error uploading to URL " + "ftp://" + FTPServer + @"/" + remotePath + @"/" + Path.GetFileName(fileToUpload), e);
            return false;
        }

    }


    /// <summary>
    /// Download a file using FTP
    /// </summary>
    /// <param name="FTPServer">The server to download from</param>
    /// <param name="remotePath">The remote path in the server</param>
    /// <param name="fileNameToDownload">The remote file name</param>
    /// <param name="saveToLocalPath">The path to the folder to download to</param>
    /// <param name="user">User to log onto the FTP server</param>
    /// <param name="password">Password to log onto the FTP server</param>
    /// <returns>The status of the download</returns>
    public static bool FTPDownload(string FTPServer, string remotePath, string fileNameToDownload, string saveToLocalPath, string user, string password)
    {
        try
        {
            //Get FTP web resquest object.
            FtpWebRequest request = FtpWebRequest.Create(new Uri(@"ftp://" + FTPServer + @"/" + remotePath + @"/" + fileNameToDownload)) as FtpWebRequest;
            request.UseBinary = true;
            request.KeepAlive = false;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(password))
                request.Credentials = new NetworkCredential(user, password);
            FtpWebResponse response = request.GetResponse() as FtpWebResponse;
            Stream responseStream = response.GetResponseStream();
            FileStream outputStream = new FileStream(saveToLocalPath + "\\" + fileNameToDownload, FileMode.Create);
            int bufferSize = 1024;
            int readCount;
            byte[] buffer = new byte[bufferSize];
            readCount = responseStream.Read(buffer, 0, bufferSize);
            while (readCount > 0)
            {
                outputStream.Write(buffer, 0, readCount);
                readCount = responseStream.Read(buffer, 0, bufferSize);
            }
            string statusDescription = response.StatusDescription;
            responseStream.Close();
            outputStream.Close();
            response.Close();
            return true;
            //return statusDescription;
        }
        catch
        {
            //throw new Exception("Error uploading to URL " + "ftp://" + FTPServer + @"/" + remotePath + @"/" + Path.GetFileName(fileToUpload), e);
            return false;
        }

    }

    public static string GetCompanyConnectionString(string _Company)
    {
        string _Con = "";
        DataTable _CompanyList;
        string _SQLCon;
        _SQLCon = "SELECT CONCAT(A.CompanyCode,' - ' ,A.CompanyName) AS Company,A.* FROM OCMP A WHERE A.CompanyCode = '" + _Company + "'";
        _CompanyList = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLCon);

        foreach (DataRow rowDB in _CompanyList.Rows)
        {
            clsDeclaration.sServer = rowDB[3].ToString();
            clsDeclaration.sCompany = rowDB[4].ToString();
            clsDeclaration.sUsername = rowDB[5].ToString();
            clsDeclaration.sPassword = rowDB[6].ToString();

            _Con = clsSQLClientFunctions.GlobalConnectionString(
                               clsDeclaration.sServer, clsDeclaration.sCompany,
                               clsDeclaration.sUsername, clsDeclaration.sPassword
                            );
        }

        return _Con;
    }


    public static bool CreateBranchFolder(string path)
    {
        try
        {
            bool folderExists = Directory.Exists(path);
            if (folderExists == false)
            {
                Application.DoEvents();
                Directory.CreateDirectory(path);
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }

    }

    public static string FormAccess(string _UserID, string _Module)
    {
        try
        {
            if (_UserID == "1")
            {
                return "Full";
            }

            DataTable _DataTable;
            string _SQLSyntax;
            _SQLSyntax = @"SELECT A.Access FROM OUAS A WHERE A.UserID = '" + _UserID + "' AND A.Module = '" + _Module + "'";
            _DataTable = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _SQLSyntax);


            return clsSQLClientFunctions.GetData(_DataTable, "Access","0");
        }
        catch
        {
            return "No Access";
        }

    }

    public static DialogResult ShowInputDialog(ref string input)
    {
        System.Drawing.Size size = new System.Drawing.Size(300, 70);
        Form inputBox = new Form();

        inputBox.Font = new Font("Segoe UI", 9);
        inputBox.StartPosition = FormStartPosition.CenterScreen;
        inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        inputBox.ClientSize = size;
        inputBox.Text = "Input Data";

        System.Windows.Forms.TextBox textBox = new TextBox();
        textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
        textBox.Location = new System.Drawing.Point(5, 5);
        textBox.Text = input;
        inputBox.Controls.Add(textBox);

        Button okButton = new Button();
        okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
        okButton.Name = "okButton";
        okButton.Size = new System.Drawing.Size(75, 23);
        okButton.Text = "&OK";
        okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
        inputBox.Controls.Add(okButton);

        Button cancelButton = new Button();
        cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new System.Drawing.Size(75, 23);
        cancelButton.Text = "&Cancel";
        cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
        inputBox.Controls.Add(cancelButton);

        inputBox.AcceptButton = okButton;
        inputBox.CancelButton = cancelButton;

        DialogResult result = inputBox.ShowDialog();
        input = textBox.Text;
        return result;
    }


    public static void InsertTimeRecord(string _TimeRecord, string _EmployeeCode, string _AcctCode, string _timeFormat, string _PayrollPeriod, string _ConString)
    {

        string _SQLInsert;

        string s;
        string[] parts;
        double i1;
        double i2;

        string _NoOfHrs = "0";
        string _NoOfMins = "0";

        string _TotalHours = "0";
        string _TotalDays = "0";


        double _ValueWH = 0;

        if (_TimeRecord != "")
        {
            _ValueWH = double.Parse(_TimeRecord);

            switch (_timeFormat)
            {
                case "d":
                    s = (_ValueWH * 8).ToString("0.00000");
                    parts = s.Split('.');
                    i1 = double.Parse(parts[0]);
                    i2 = double.Parse("." + parts[1]) * 60;

                    _NoOfHrs = i1.ToString();
                    _NoOfMins = i2.ToString();
                    _TotalHours = (_ValueWH * 8).ToString("0.00000");
                    _TotalDays = _ValueWH.ToString();

                    break;
                case "h":
                    s = (_ValueWH).ToString("0.00000");
                    parts = s.Split('.');
                    i1 = double.Parse(parts[0]);
                    i2 = double.Parse("." + parts[1]) * 60;

                    _NoOfHrs = i1.ToString();
                    _NoOfMins = i2.ToString();

                    _TotalHours = _ValueWH.ToString();
                    _TotalDays = (_ValueWH / 8).ToString("0.00000");

                    break;
                case "m":
                    s = (_ValueWH / 60).ToString("0.00000");
                    parts = s.Split('.');
                    i1 = double.Parse(parts[0]);
                    i2 = double.Parse("." + parts[1]) * 60;

                    _NoOfHrs = i1.ToString();
                    _NoOfMins = i2.ToString();

                    _TotalHours = (_ValueWH / 60).ToString("0.00000");
                    _TotalDays = ((_ValueWH / 60) / 8).ToString("0.00000");
                    break;
            }

            

            _SQLInsert = @"

									INSERT INTO [PayrollTrans01]
												([PayrollPeriod]
                                                ,[EmployeeNo]
                                                ,[AccountCode]
                                                ,[NoOfHrs]
                                                ,[NoOfMins]
                                                ,[TotalHrs]
                                                ,[TotalDays])
												VALUES
												(
												'" + _PayrollPeriod + @"',
												'" + _EmployeeCode + @"',
												'" + _AcctCode + @"',
												'" + _NoOfHrs + @"',
												'" + _NoOfMins + @"',
												'" + _TotalHours + @"',
												'" + _TotalDays + @"'
												)

                                   ";

            clsSQLClientFunctions.GlobalExecuteCommand(_ConString, _SQLInsert);

        }

    }


    public static string GetHardwareKey()
    {
        string cpuInfo = string.Empty;
        ManagementClass mc = new ManagementClass("win32_processor");
        ManagementObjectCollection moc = mc.GetInstances();

        foreach (ManagementObject mo in moc)
        {
            if (cpuInfo == "")
            {
                //Get only the first CPU's ID
                cpuInfo = mo.Properties["processorID"].Value.ToString();
                break;
            }
        }

        string drive = "C";
        ManagementObject dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + drive + @":""");
        dsk.Get();
        string volumeSerial = dsk["VolumeSerialNumber"].ToString();

        return cpuInfo + volumeSerial;
    }



    public static void InsertDetails(string _Company
                                       , string _PayrollPeriod
                                       , string _EmployeeNo
                                       , string _AccountCode
                                       , string _LoanRefenceNo
                                       , double _NoOfHours
                                       , double _Amount
                                       , double _NoOfMins
                                       , double _TotalHrs
                                       , double _TotalDays
                                       , string _Branch
                                       , string _Department)
    {
        string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);

        string _sqlInsertPayrollDetails = "";
        _sqlInsertPayrollDetails = @"
                                                        INSERT INTO [dbo].[PayrollDetails]
                                                                   ([PayrollPeriod]
                                                                   ,[EmployeeNo]
                                                                   ,[AccountCode]
                                                                   ,[LoanRefenceNo]
                                                                   ,[NoOfHours]
                                                                   ,[Amount]
                                                                   ,[NoOfMins]
                                                                   ,[BillingAmount]
                                                                   ,[TotalHrs]
                                                                   ,[TotalDays]
                                                                   ,[Branch]
                                                                   ,[Department])
                                                             VALUES
                                                                   ('" + _PayrollPeriod + @"'
                                                                   ,'" + _EmployeeNo + @"'
                                                                   ,'" + _AccountCode + @"'
                                                                   ,'" + _LoanRefenceNo + @"'
                                                                   ,'" + _NoOfHours + @"'
                                                                   ,'" + _Amount + @"'
                                                                   ,'" + _NoOfMins + @"'
                                                                   ,0.00
                                                                   ,'" + _TotalHrs + @"'
                                                                   ,'" + _TotalDays + @"'
                                                                   ,'" + _Branch + @"'
                                                                   ,'" + _Department + @"')
                                                     ";


        clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlInsertPayrollDetails);
    }



    public static void InsertLoanPayment(string _Company
                                           , string _PayrollPeriod
                                           , string _EmployeeNo
                                           , string _AccountCode
                                           , string _LoanRefenceNo
                                           , double _Amount
                                           , string _ORNo
                                           , string _Remarks
                                           , string _Type
                                           , string _PaymentDate)
    {
        string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);

        string _sqlInsertPayrollDetails = "";
        _sqlInsertPayrollDetails = @"

                                                        DELETE FROM [LoanCashPayment] WHERE [EmployeeNo] = '" + _EmployeeNo + @"' AND [PayrollPeriod] = '" + _PayrollPeriod + @"'
                                                        AND [AccountCode] = '" + _AccountCode + @"' AND [LoanRefNo] = '" + _LoanRefenceNo + @"'
                                                        AND [ORNo]  = '" + _ORNo + @"'

                                                        INSERT INTO [dbo].[LoanCashPayment]
                                                                   ([EmployeeNo]
                                                                   ,[AccountCode]
                                                                   ,[LoanRefNo]
                                                                   ,[ORNo]
                                                                   ,[PaymentDate]
                                                                   ,[Amount]
                                                                   ,[Remarks]
                                                                   ,[Type]
                                                                   ,[PayrollPeriod]
                                                                   ,[CreateDate]
                                                                   ,[UpdateDate])
                                                             VALUES
                                                                   ('" + _EmployeeNo + @"'
                                                                   ,'" + _AccountCode + @"'
                                                                   ,'" + _LoanRefenceNo + @"'
                                                                   ,'" + _ORNo + @"'
                                                                   ,'" + _PaymentDate + @"'
                                                                   ,'" + _Amount + @"'
                                                                   ,'" + _Remarks + @"'
                                                                   ,'" + _Type + @"'
                                                                   ,'" + _PayrollPeriod + @"'
                                                                   ,'" + DateTime.Today + @"'
                                                                   ,'" + DateTime.Today + @"')
                                                     ";


        clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlInsertPayrollDetails);
    }



    public static void InsertPayrollHeader(string _Company
                                       , string _PayrollPeriod
                                       , string _EmployeeNo
                                       , double _MonthlyRate
                                       , double _DailyRate
                                       , string _Department
                                       , string _Branch
                                       , string _PayrollType
                                       , string _BankAccountNo
                                       , string _PayrollMode)
    {
        string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);

        string _sqlPayrollDetails = "";
        DataTable _tblPayrollDetails = new DataTable();

        #region Regular Days Calculation
        // Get Regular Hours / Days
        _sqlPayrollDetails = @"
                                                SELECT ISNULL(SUM(A.Amount),0) AS Amount, ISNULL(SUM(A.TotalHrs),0) AS TotalHrs, ISNULL(SUM(A.TotalDays),0) AS TotalDays 
                                                FROM PayrollDetails A 
	                                                LEFT JOIN AccountCode B ON A.AccountCode = B.AccountCode
	                                                LEFT JOIN PayrollRegsCode C ON ISNULL(B.PayrollRegCode,A.AccountCode) = C.AccountCode
                                                WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.PayrollPeriod = '" + _PayrollPeriod + @"'
                                                    AND C.[Description] = 'Basic Pay'
                                            ";
        _tblPayrollDetails = clsSQLClientFunctions.DataList(_ConCompany, _sqlPayrollDetails);

        double _BasicPay = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "Amount", "1")), 2, MidpointRounding.AwayFromZero);
        double _RegularHrs = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "TotalHrs", "1")), 5, MidpointRounding.AwayFromZero);
        double _RegularDays = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "TotalDays", "1")), 5, MidpointRounding.AwayFromZero);
        #endregion
        #region Overtime Calculation
        _sqlPayrollDetails = @"
                                                SELECT ISNULL(SUM(A.Amount),0) AS Amount, ISNULL(SUM(A.TotalHrs),0) AS TotalHrs, ISNULL(SUM(A.TotalDays),0) AS TotalDays 
                                                FROM PayrollDetails A 
	                                                LEFT JOIN AccountCode B ON A.AccountCode = B.AccountCode
	                                                LEFT JOIN PayrollRegsCode C ON ISNULL(B.PayrollRegCode,A.AccountCode) = C.AccountCode
                                                WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.PayrollPeriod = '" + _PayrollPeriod + @"'
                                                    AND C.[Description] = 'OT Pay'
                                            ";
        _tblPayrollDetails = clsSQLClientFunctions.DataList(_ConCompany, _sqlPayrollDetails);

        double _OTPay = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "Amount", "1")), 2, MidpointRounding.AwayFromZero);
        double _OTHrs = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "TotalHrs", "1")), 5, MidpointRounding.AwayFromZero);
        #endregion
        #region Sunday/Restday Calculation
        _sqlPayrollDetails = @"
                                                SELECT ISNULL(SUM(A.Amount),0) AS Amount, ISNULL(SUM(A.TotalHrs),0) AS TotalHrs, ISNULL(SUM(A.TotalDays),0) AS TotalDays 
                                                FROM PayrollDetails A 
	                                                LEFT JOIN AccountCode B ON A.AccountCode = B.AccountCode
	                                                LEFT JOIN PayrollRegsCode C ON ISNULL(B.PayrollRegCode,A.AccountCode) = C.AccountCode
                                                WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.PayrollPeriod = '" + _PayrollPeriod + @"'
                                                    AND C.[Description] = 'SUN Pay'
                                            ";
        _tblPayrollDetails = clsSQLClientFunctions.DataList(_ConCompany, _sqlPayrollDetails);

        double _SUNPay = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "Amount", "1")), 2, MidpointRounding.AwayFromZero);
        double _SUNHrs = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "TotalHrs", "1")), 5, MidpointRounding.AwayFromZero);
        #endregion
        #region Special Holiday Calculation
        _sqlPayrollDetails = @"
                                                SELECT ISNULL(SUM(A.Amount),0) AS Amount, ISNULL(SUM(A.TotalHrs),0) AS TotalHrs, ISNULL(SUM(A.TotalDays),0) AS TotalDays 
                                                FROM PayrollDetails A 
	                                                LEFT JOIN AccountCode B ON A.AccountCode = B.AccountCode
	                                                LEFT JOIN PayrollRegsCode C ON ISNULL(B.PayrollRegCode,A.AccountCode) = C.AccountCode
                                                WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.PayrollPeriod = '" + _PayrollPeriod + @"'
                                                    AND C.[Description] = 'Special Holiday Pay'
                                            ";
        _tblPayrollDetails = clsSQLClientFunctions.DataList(_ConCompany, _sqlPayrollDetails);

        double _SPLPay = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "Amount", "1")), 2, MidpointRounding.AwayFromZero);
        double _SPLHrs = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "TotalHrs", "1")), 5, MidpointRounding.AwayFromZero);
        #endregion
        #region Legal Holiday Calculation
        _sqlPayrollDetails = @"
                                                SELECT ISNULL(SUM(A.Amount),0) AS Amount, ISNULL(SUM(A.TotalHrs),0) AS TotalHrs, ISNULL(SUM(A.TotalDays),0) AS TotalDays 
                                                FROM PayrollDetails A 
	                                                LEFT JOIN AccountCode B ON A.AccountCode = B.AccountCode
	                                                LEFT JOIN PayrollRegsCode C ON ISNULL(B.PayrollRegCode,A.AccountCode) = C.AccountCode
                                                WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.PayrollPeriod = '" + _PayrollPeriod + @"'
                                                    AND C.[Description] = 'Legal Holiday Pay'
                                            ";
        _tblPayrollDetails = clsSQLClientFunctions.DataList(_ConCompany, _sqlPayrollDetails);

        double _LEGPay = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "Amount", "1")), 2, MidpointRounding.AwayFromZero);
        double _LEGHrs = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "TotalHrs", "1")), 5, MidpointRounding.AwayFromZero);
        #endregion
        #region Goverment Calculation
        #region SSS Goverment Calculation
        double _SSSEmployee = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "SSS Employee"), 2, MidpointRounding.AwayFromZero);
        double _SSSEmployer = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "SSS Employer"), 2, MidpointRounding.AwayFromZero);
        double _SSSEC = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "SSSEC Employee"), 2, MidpointRounding.AwayFromZero);
        #endregion
        #region PHILHEALTH Goverment Calculation
        double _PhilHealthEmployee = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "PhilHealth Employee"), 2, MidpointRounding.AwayFromZero);
        double _PhilHealthEmployer = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "PhilHealth Employer"), 2, MidpointRounding.AwayFromZero);
        #endregion
        #region PAGIBIG Goverment Calculation
        double _PagIbigEmployee = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "PagIbig Employee"), 2, MidpointRounding.AwayFromZero);
        double _PagIbigEmployer = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "PagIbig Employer"), 2, MidpointRounding.AwayFromZero);
        #endregion
        #region WTAX Goverment Calculation
        double _WitholdingTax = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Withholding Tax"), 2, MidpointRounding.AwayFromZero);
        #endregion
        double _GovDeduction = _SSSEmployee + _PagIbigEmployee + _PhilHealthEmployee + _WitholdingTax;

        #endregion
        #region Gross Pay Calculation
        double _COLAAmount = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "COLA Amount"), 2, MidpointRounding.AwayFromZero);
        double _AllowanceAmount = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Allowance Amount"), 2, MidpointRounding.AwayFromZero);
        double _PERAAmount = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "PERRA Amount"), 2, MidpointRounding.AwayFromZero);
        double _OtherIncome = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Other Income"), 2, MidpointRounding.AwayFromZero);
        double _PaidLeaves = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Paid Leaves"), 2, MidpointRounding.AwayFromZero);
        //double _Absences = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Absences"), 2, MidpointRounding.AwayFromZero);

        double _TotalOtherIncome = Math.Round((_COLAAmount + _AllowanceAmount + _PERAAmount + _OtherIncome + _PaidLeaves), 2, MidpointRounding.AwayFromZero);
        double _GrossPay = Math.Round((_BasicPay + _OTPay + _SUNPay + _SPLPay + _LEGPay + _TotalOtherIncome), 2, MidpointRounding.AwayFromZero);
        #endregion
        #region Loan Pay Calculation
        double _SSSLoan = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "SSS Loan"), 2, MidpointRounding.AwayFromZero);
        double _PagibigLoan = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Pagibig Loan"), 2, MidpointRounding.AwayFromZero);
        double _CalamityLoan = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Calamity Loan"), 2, MidpointRounding.AwayFromZero);
        double _OtherLoan = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Other Loan"), 2, MidpointRounding.AwayFromZero);
        #endregion
        #region Net Pay Calculation
        double _OtherDeduction = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Other Deduction"), 2, MidpointRounding.AwayFromZero);
        double _TotalDeduction = Math.Round((_SSSLoan + _PagibigLoan + _CalamityLoan + _OtherLoan + _OtherDeduction + _GovDeduction), 2, MidpointRounding.AwayFromZero);
        double _NetPay = _GrossPay - _TotalDeduction;
        #endregion
        #region Insert Data in PayrollHeader
        double _Month13th = 0;
        string _sqlInsertPayrollDetails = "";
        _sqlInsertPayrollDetails = @"    
                                        INSERT INTO [dbo].[PayrollHeader]
                                                   ([PayrollPeriod]
                                                   ,[EmployeeNo]
                                                   ,[MonthlyRate]
                                                   ,[DailyRate]
                                                   ,[DepartmentCode]
                                                   ,[Department]
                                                   ,[TotalDays]
                                                   ,[TotalHrs]
                                                   ,[BasicPay]
                                                   ,[OTPay]
                                                   ,[OTHrs]
                                                   ,[SUNPay]
                                                   ,[SUNHrs]
                                                   ,[SPLPay]
                                                   ,[SPLHrs]
                                                   ,[LEGPay]
                                                   ,[LEGHrs]
                                                   ,[SSSEmployee]
                                                   ,[SSSEmployer]
                                                   ,[SSSEC]
                                                   ,[PhilHealthEmployee]
                                                   ,[PhilHealthEmployer]
                                                   ,[PagIbigEmployee]
                                                   ,[PagIbigEmployer]
                                                   ,[NonTaxPagIbig]
                                                   ,[WitholdingTax]
                                                   ,[SSSBaseAmount]
                                                   ,[PhilHealthBaseAmount]
                                                   ,[PagIbigBaseAmount]
                                                   ,[TaxBaseAmount]
                                                   ,[Month13th]
                                                   ,[COLAAmount]
                                                   ,[OtherIncome]
                                                   ,[Gross]
                                                   ,[SSSLoan]
                                                   ,[PagibigLoan]
                                                   ,[CalamityLoan]
                                                   ,[OtherLoan]
                                                   ,[OtherDeduction]
                                                   ,[TotalDeductions]
                                                   ,[NetPay]
                                                   ,[Branch]
                                                   ,[PayrollType]
                                                   ,[BankAccountNo]
                                                   ,[PayrollMode])
                                             VALUES
                                                   ('" + _PayrollPeriod + @"'
                                                   ,'" + _EmployeeNo + @"'
                                                   ,'" + _MonthlyRate + @"'
                                                   ,'" + _DailyRate + @"'
                                                   ,'" + _Department + @"'
                                                   ,'" + _Department + @"'
                                                   ,'" + _RegularDays + @"'
                                                   ,'" + _RegularHrs + @"'
                                                   ,'" + _BasicPay + @"'
                                                   ,'" + _OTPay + @"'
                                                   ,'" + _OTHrs + @"'
                                                   ,'" + _SUNPay + @"'
                                                   ,'" + _SUNHrs + @"'
                                                   ,'" + _SPLPay + @"'
                                                   ,'" + _SPLHrs + @"'
                                                   ,'" + _LEGPay + @"'
                                                   ,'" + _LEGHrs + @"'
                                                   ,'" + _SSSEmployee + @"'
                                                   ,'" + _SSSEmployer + @"'
                                                   ,'" + _SSSEC + @"'
                                                   ,'" + _PhilHealthEmployee + @"'
                                                   ,'" + _PhilHealthEmployer + @"'
                                                   ,'" + _PagIbigEmployee + @"'
                                                   ,'" + _PagIbigEmployer + @"'
                                                   ,'" + _PagIbigEmployee + @"'
                                                   ,'" + _WitholdingTax + @"'
                                                   ,'" + _MonthlyRate + @"'
                                                   ,'" + _MonthlyRate + @"'
                                                   ,'" + _MonthlyRate + @"'
                                                   ,'" + _MonthlyRate + @"'
                                                   ,'" + _Month13th + @"'
                                                   ,'" + _COLAAmount + @"'
                                                   ,'" + _TotalOtherIncome + @"'
                                                   ,'" + _GrossPay + @"'
                                                   ,'" + _SSSLoan + @"'
                                                   ,'" + _PagibigLoan + @"'
                                                   ,'" + _CalamityLoan + @"'
                                                   ,'" + _OtherLoan + @"'
                                                   ,'" + _OtherDeduction + @"'
                                                   ,'" + _TotalDeduction + @"'
                                                   ,'" + _NetPay + @"'
                                                   ,'" + _Branch + @"'
                                                   ,'" + _PayrollType + @"'
                                                   ,'" + _BankAccountNo + @"'
                                                   ,'" + _PayrollMode + @"')
                                                     ";


        clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlInsertPayrollDetails);
        #endregion

    }


    public static double GetAmount(string _ConCompany, string _EmployeeNo, string _PayrollPeriod, string _PayrolRegGroup)
    {
        string _sqlPayrollDetails = "";
        _sqlPayrollDetails = @"
                                                SELECT ISNULL(SUM(A.Amount),0) AS Amount, ISNULL(SUM(A.TotalHrs),0) AS TotalHrs, ISNULL(SUM(A.TotalDays),0) AS TotalDays 
                                                FROM PayrollDetails A 
	                                                LEFT JOIN AccountCode B ON A.AccountCode = B.AccountCode
	                                                LEFT JOIN PayrollRegsCode C ON ISNULL(B.PayrollRegCode,A.AccountCode) = C.AccountCode
                                                WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.PayrollPeriod = '" + _PayrollPeriod + @"'
                                                    AND C.[Description] = '" + _PayrolRegGroup + @"'
                                            ";
        return Math.Round(clsSQLClientFunctions.GetNumericValue(_ConCompany, _sqlPayrollDetails, "Amount"), 2, MidpointRounding.AwayFromZero);
    }



    public static void UpdatePayrollHeader(string _Company
                                   , string _PayrollPeriod
                                   , string _EmployeeNo
                                   , double _MonthlyRate)
    {
        string _ConCompany = clsFunctions.GetCompanyConnectionString(_Company);

        string _sqlPayrollDetails = "";
        DataTable _tblPayrollDetails = new DataTable();

        #region Regular Days Calculation
        // Get Regular Hours / Days
        _sqlPayrollDetails = @"
                                                SELECT ISNULL(SUM(A.Amount),0) AS Amount, ISNULL(SUM(A.TotalHrs),0) AS TotalHrs, ISNULL(SUM(A.TotalDays),0) AS TotalDays 
                                                FROM PayrollDetails A 
	                                                LEFT JOIN AccountCode B ON A.AccountCode = B.AccountCode
	                                                LEFT JOIN PayrollRegsCode C ON ISNULL(B.PayrollRegCode,A.AccountCode) = C.AccountCode
                                                WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.PayrollPeriod = '" + _PayrollPeriod + @"'
                                                    AND C.[Description] = 'Basic Pay'
                                            ";
        _tblPayrollDetails = clsSQLClientFunctions.DataList(_ConCompany, _sqlPayrollDetails);

        double _BasicPay = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "Amount", "1")), 2, MidpointRounding.AwayFromZero);
        double _RegularHrs = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "TotalHrs", "1")), 5, MidpointRounding.AwayFromZero);
        double _RegularDays = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "TotalDays", "1")), 5, MidpointRounding.AwayFromZero);
        #endregion
        #region Overtime Calculation
        _sqlPayrollDetails = @"
                                                SELECT ISNULL(SUM(A.Amount),0) AS Amount, ISNULL(SUM(A.TotalHrs),0) AS TotalHrs, ISNULL(SUM(A.TotalDays),0) AS TotalDays 
                                                FROM PayrollDetails A 
	                                                LEFT JOIN AccountCode B ON A.AccountCode = B.AccountCode
	                                                LEFT JOIN PayrollRegsCode C ON ISNULL(B.PayrollRegCode,A.AccountCode) = C.AccountCode
                                                WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.PayrollPeriod = '" + _PayrollPeriod + @"'
                                                    AND C.[Description] = 'OT Pay'
                                            ";
        _tblPayrollDetails = clsSQLClientFunctions.DataList(_ConCompany, _sqlPayrollDetails);

        double _OTPay = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "Amount", "1")), 2, MidpointRounding.AwayFromZero);
        double _OTHrs = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "TotalHrs", "1")), 5, MidpointRounding.AwayFromZero);
        #endregion
        #region Sunday/Restday Calculation
        _sqlPayrollDetails = @"
                                                SELECT ISNULL(SUM(A.Amount),0) AS Amount, ISNULL(SUM(A.TotalHrs),0) AS TotalHrs, ISNULL(SUM(A.TotalDays),0) AS TotalDays 
                                                FROM PayrollDetails A 
	                                                LEFT JOIN AccountCode B ON A.AccountCode = B.AccountCode
	                                                LEFT JOIN PayrollRegsCode C ON ISNULL(B.PayrollRegCode,A.AccountCode) = C.AccountCode
                                                WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.PayrollPeriod = '" + _PayrollPeriod + @"'
                                                    AND C.[Description] = 'SUN Pay'
                                            ";
        _tblPayrollDetails = clsSQLClientFunctions.DataList(_ConCompany, _sqlPayrollDetails);

        double _SUNPay = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "Amount", "1")), 2, MidpointRounding.AwayFromZero);
        double _SUNHrs = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "TotalHrs", "1")), 5, MidpointRounding.AwayFromZero);
        #endregion
        #region Special Holiday Calculation
        _sqlPayrollDetails = @"
                                                SELECT ISNULL(SUM(A.Amount),0) AS Amount, ISNULL(SUM(A.TotalHrs),0) AS TotalHrs, ISNULL(SUM(A.TotalDays),0) AS TotalDays 
                                                FROM PayrollDetails A 
	                                                LEFT JOIN AccountCode B ON A.AccountCode = B.AccountCode
	                                                LEFT JOIN PayrollRegsCode C ON ISNULL(B.PayrollRegCode,A.AccountCode) = C.AccountCode
                                                WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.PayrollPeriod = '" + _PayrollPeriod + @"'
                                                    AND C.[Description] = 'Special Holiday Pay'
                                            ";
        _tblPayrollDetails = clsSQLClientFunctions.DataList(_ConCompany, _sqlPayrollDetails);

        double _SPLPay = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "Amount", "1")), 2, MidpointRounding.AwayFromZero);
        double _SPLHrs = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "TotalHrs", "1")), 5, MidpointRounding.AwayFromZero);
        #endregion
        #region Legal Holiday Calculation
        _sqlPayrollDetails = @"
                                                SELECT ISNULL(SUM(A.Amount),0) AS Amount, ISNULL(SUM(A.TotalHrs),0) AS TotalHrs, ISNULL(SUM(A.TotalDays),0) AS TotalDays 
                                                FROM PayrollDetails A 
	                                                LEFT JOIN AccountCode B ON A.AccountCode = B.AccountCode
	                                                LEFT JOIN PayrollRegsCode C ON ISNULL(B.PayrollRegCode,A.AccountCode) = C.AccountCode
                                                WHERE A.EmployeeNo = '" + _EmployeeNo + @"' AND A.PayrollPeriod = '" + _PayrollPeriod + @"'
                                                    AND C.[Description] = 'Legal Holiday Pay'
                                            ";
        _tblPayrollDetails = clsSQLClientFunctions.DataList(_ConCompany, _sqlPayrollDetails);

        double _LEGPay = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "Amount", "1")), 2, MidpointRounding.AwayFromZero);
        double _LEGHrs = Math.Round(double.Parse(clsSQLClientFunctions.GetData(_tblPayrollDetails, "TotalHrs", "1")), 5, MidpointRounding.AwayFromZero);
        #endregion
        #region Goverment Calculation
        #region SSS Goverment Calculation
        double _SSSEmployee = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "SSS Employee"), 2, MidpointRounding.AwayFromZero);
        double _SSSEmployer = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "SSS Employer"), 2, MidpointRounding.AwayFromZero);
        double _SSSEC = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "SSSEC Employee"), 2, MidpointRounding.AwayFromZero);
        #endregion
        #region PHILHEALTH Goverment Calculation
        double _PhilHealthEmployee = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "PhilHealth Employee"), 2, MidpointRounding.AwayFromZero);
        double _PhilHealthEmployer = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "PhilHealth Employer"), 2, MidpointRounding.AwayFromZero);
        #endregion
        #region PAGIBIG Goverment Calculation
        double _PagIbigEmployee = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "PagIbig Employee"), 2, MidpointRounding.AwayFromZero);
        double _PagIbigEmployer = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "PagIbig Employer"), 2, MidpointRounding.AwayFromZero);
        #endregion
        #region WTAX Goverment Calculation
        double _WitholdingTax = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Withholding Tax"), 2, MidpointRounding.AwayFromZero);
        #endregion
        double _GovDeduction = _SSSEmployee + _PagIbigEmployee + _PhilHealthEmployee + _WitholdingTax;
        #endregion
        #region Gross Pay Calculation
        double _COLAAmount = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "COLA Amount"), 2, MidpointRounding.AwayFromZero);
        double _AllowanceAmount = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Allowance Amount"), 2, MidpointRounding.AwayFromZero);
        double _PERAAmount = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "PERRA Amount"), 2, MidpointRounding.AwayFromZero);
        double _OtherIncome = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Other Income"), 2, MidpointRounding.AwayFromZero);
        double _PaidLeaves = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Paid Leaves"), 2, MidpointRounding.AwayFromZero);
        //double _Absences = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Absences"), 2, MidpointRounding.AwayFromZero);

        double _TotalOtherIncome = Math.Round((_COLAAmount + _AllowanceAmount + _PERAAmount + _OtherIncome + _PaidLeaves), 2, MidpointRounding.AwayFromZero);
        double _GrossPay = Math.Round((_BasicPay + _OTPay + _SUNPay + _SPLPay + _LEGPay + _TotalOtherIncome), 2, MidpointRounding.AwayFromZero);
        #endregion
        #region Loan Pay Calculation
        double _SSSLoan = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "SSS Loan"), 2, MidpointRounding.AwayFromZero);
        double _PagibigLoan = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Pagibig Loan"), 2, MidpointRounding.AwayFromZero);
        double _CalamityLoan = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Calamity Loan"), 2, MidpointRounding.AwayFromZero);
        double _OtherLoan = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Other Loan"), 2, MidpointRounding.AwayFromZero);
        #endregion
        #region Net Pay Calculation
        double _OtherDeduction = Math.Round(GetAmount(_ConCompany, _EmployeeNo, _PayrollPeriod, "Other Deduction"), 2, MidpointRounding.AwayFromZero);
        double _TotalDeduction = Math.Round((_SSSLoan + _PagibigLoan + _CalamityLoan + _OtherLoan + _OtherDeduction + _GovDeduction), 2, MidpointRounding.AwayFromZero);
        double _NetPay = _GrossPay - _TotalDeduction;
        #endregion
        #region Update Data in PayrollHeader
        double _Month13th = 0;

        string _sqlInsertPayrollDetails = "";
        _sqlInsertPayrollDetails = @"    
UPDATE A SET 

 A.[TotalDays]= '" + _RegularDays + @"'
,A.[TotalHrs]= '" + _RegularHrs + @"'
,A.[BasicPay]= '" + _BasicPay + @"'
,A.[OTPay]= '" + _OTPay + @"'
,A.[OTHrs]= '" + _OTHrs + @"'
,A.[SUNPay]= '" + _SUNPay + @"'
,A.[SUNHrs]= '" + _SUNHrs + @"'
,A.[SPLPay]= '" + _SPLPay + @"'
,A.[SPLHrs]= '" + _SPLHrs + @"'
,A.[LEGPay]= '" + _LEGPay + @"'
,A.[LEGHrs]= '" + _LEGHrs + @"'
,A.[SSSEmployee]= '" + _SSSEmployee + @"'
,A.[SSSEmployer]= '" + _SSSEmployer + @"'
,A.[SSSEC]= '" + _SSSEC + @"'
,A.[PhilHealthEmployee]= '" + _PhilHealthEmployee + @"'
,A.[PhilHealthEmployer]= '" + _PhilHealthEmployer + @"'
,A.[PagIbigEmployee]= '" + _PagIbigEmployee + @"'
,A.[PagIbigEmployer]= '" + _PagIbigEmployer + @"'
,A.[NonTaxPagIbig]= '" + _PagIbigEmployee + @"'
,A.[WitholdingTax]= '" + _WitholdingTax + @"'
,A.[SSSBaseAmount]= '" + _MonthlyRate + @"'
,A.[PhilHealthBaseAmount]= '" + _MonthlyRate + @"'
,A.[PagIbigBaseAmount]= '" + _MonthlyRate + @"'
,A.[TaxBaseAmount]= '" + _MonthlyRate + @"'
,A.[Month13th]= '" + _Month13th + @"'
,A.[COLAAmount]= '" + _COLAAmount + @"'
,A.[OtherIncome]= '" + _TotalOtherIncome + @"'
,A.[Gross]= '" + _GrossPay + @"'
,A.[SSSLoan]= '" + _SSSLoan + @"'
,A.[PagibigLoan]= '" + _PagibigLoan + @"'
,A.[CalamityLoan]= '" + _CalamityLoan + @"'
,A.[OtherLoan]= '" + _OtherLoan + @"'
,A.[OtherDeduction]= '" + _OtherDeduction + @"'
,A.[TotalDeductions]= '" + _TotalDeduction + @"'
,A.[NetPay]= '" + _NetPay + @"'

FROM [dbo].[PayrollHeader] A
WHERE 
A.[PayrollPeriod]= '" + _PayrollPeriod + @"'
AND A.[EmployeeNo]= '" + _EmployeeNo + @"'
                                                     ";


        clsSQLClientFunctions.GlobalExecuteCommand(_ConCompany, _sqlInsertPayrollDetails);
        #endregion

    }

}

