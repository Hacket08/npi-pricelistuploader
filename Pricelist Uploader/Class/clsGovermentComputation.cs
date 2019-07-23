using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Windows.Forms;
using System.Data;

using System.Data.SqlClient;
//using System.Windows.Forms;

class clsGovermentComputation
{

    public static void WTAXComputationAnnual(string _ConString, string _EmployeeNo, out double _WTax)
    {
        string _sqlSyntax;
        DataTable _tblScript;

        _sqlSyntax = "SELECT A.MonthlyRate, A.DailyRate, A.RateDivisor, A.TaxStatus FROM vwsEmployees A WHERE A.EmployeeNo = '" + _EmployeeNo + "'";
        _tblScript = clsSQLClientFunctions.DataList(_ConString, _sqlSyntax);

        double _MonthlyRate = double.Parse(clsSQLClientFunctions.GetData(_tblScript, "MonthlyRate", "1"));
        double _DailyRate = double.Parse(clsSQLClientFunctions.GetData(_tblScript, "DailyRate", "1"));
        double _RateDivisor = double.Parse(clsSQLClientFunctions.GetData(_tblScript, "RateDivisor", "1"));

        string _TaxStatus = clsSQLClientFunctions.GetData(_tblScript, "TaxStatus", "0");

        _sqlSyntax = "SELECT A.[BasicSalary] FROM SalaryTable A WHERE '" + _MonthlyRate  + "' BETWEEN A.[BracketFrom] AND A.[BracketTo]";
        _tblScript = clsSQLClientFunctions.DataList(_ConString, _sqlSyntax);
        
        double _BasicSalary = double.Parse(clsSQLClientFunctions.GetData(_tblScript, "BasicSalary", "1"));
        double _DailyBasicRate = double.Parse((_BasicSalary / _RateDivisor).ToString("N2"));

        //double _DailyBasicRate = Math.Round((_BasicSalary / _RateDivisor), 2, MidpointRounding.AwayFromZero);


        double _GrossCompensation = 0;
        _GrossCompensation = _BasicSalary * 12;

        
        double _SSSEmployee;
        double _SSSEmployer;
        double _SSSECC;
        SSSComputation(_ConString, _MonthlyRate, out _SSSEmployee, out _SSSEmployer, out _SSSECC);

        double _PAGIBIGEmployee;
        double _PAGIBIGEmployer;
        PAGIBIGComputation(_ConString, _MonthlyRate, out _PAGIBIGEmployee, out _PAGIBIGEmployer);


        double _PhilHealthEmployee;
        double _PhilHealthEmployer;
        PHILHEALTHComputation(_ConString, _RateDivisor, _DailyBasicRate, out _PhilHealthEmployee, out _PhilHealthEmployer);

        double _TotalDeduction;

        double _TotalSSS = double.Parse((_SSSEmployee * 12).ToString("N2"));
        double _TotalPAGIBIG = double.Parse((100 * 12).ToString("N2"));
        double _TotalPHILHEALTH = double.Parse((_PhilHealthEmployee * 12).ToString("N2"));


        _TotalDeduction = _TotalSSS + _TotalPAGIBIG + _TotalPHILHEALTH;

        double _AnnualBasicSalary = _GrossCompensation - _TotalDeduction;

        double _OtherSalary = 0;
        double _OtherCompensation = 0;

        double _TotalTaxableIncome;
        _TotalTaxableIncome = _AnnualBasicSalary + _OtherSalary + _OtherCompensation;

       
        double _NetTaxableCompensation = _TotalTaxableIncome;


        _sqlSyntax = "SELECT A.[TaxAmount], A.[Rate], A.[Excess] FROM [TaxTableAnnual] A WHERE '" + _GrossCompensation + "' BETWEEN A.[BracketFrom] AND A.[BracketTo]";
        _tblScript = clsSQLClientFunctions.DataList(_ConString, _sqlSyntax);

        double _TaxAmount = double.Parse(clsSQLClientFunctions.GetData(_tblScript, "TaxAmount", "1"));
        double _TaxRate = double.Parse(clsSQLClientFunctions.GetData(_tblScript, "Rate", "1"));
        double _TaxExcess = double.Parse(clsSQLClientFunctions.GetData(_tblScript, "Excess", "1"));
        

        double _ExcessAmount;
        _ExcessAmount = (_NetTaxableCompensation - _TaxExcess) * (_TaxRate / 100);

        double _AnnualTaxAmount;
        _AnnualTaxAmount = (_TaxAmount + _ExcessAmount) / 12;

        _WTax = double.Parse(_AnnualTaxAmount.ToString("N2")); 
    }

    
    public static void SSSComputation(string _ConString, double _Monthly,  out double _SSSEmployee, out double _SSSEmployer, out double _SSSECC)
    {
        string _sqlSyntax;
        _sqlSyntax = "SELECT Z.Employer, Z.Employee, Z.ECC FROM SSSTable Z WHERE '" + _Monthly + "' BETWEEN Z.BracketFrom AND Z.BracketTo";
        _SSSEmployee = clsSQLClientFunctions.GetNumericValue(_ConString, _sqlSyntax, "Employee");
        _SSSEmployer = clsSQLClientFunctions.GetNumericValue(_ConString, _sqlSyntax, "Employer");
        _SSSECC = clsSQLClientFunctions.GetNumericValue(_ConString, _sqlSyntax, "ECC");
    }


    public static void PAGIBIGComputation(string _ConString, double _Monthly, out double _PAGIBIGEmployee, out double _PAGIBIGEmployer)
    {
        string _sqlSyntax;
        _sqlSyntax = @"SELECT * FROM PAGIBIGTable
                            Z WHERE '" + _Monthly + "' BETWEEN Z.BracketFrom AND Z.BracketTo";

        double _MaxCont = clsSQLClientFunctions.GetNumericValue(_ConString, _sqlSyntax, "MaxContribution");
        _PAGIBIGEmployee = Math.Round(_Monthly * (clsSQLClientFunctions.GetNumericValue(_ConString, _sqlSyntax, "Employee") / 100), 0, MidpointRounding.AwayFromZero);
        _PAGIBIGEmployer = Math.Round(_Monthly * (clsSQLClientFunctions.GetNumericValue(_ConString, _sqlSyntax, "Employer") / 100), 0, MidpointRounding.AwayFromZero);

        if (_PAGIBIGEmployee > _MaxCont)
        {
            _PAGIBIGEmployee = _MaxCont;
            _PAGIBIGEmployer = _MaxCont;
        }

    }


    public static void PHILHEALTHComputation(string _ConString, double _RateDivisor, double _DailyRate, out double _PhilHealthEmployee, out double _PhilHealthEmployer)
    {
        string _sqlSyntax;
        _sqlSyntax = @"SELECT Z.Employee, Z.Employer, Z.Rate FROM PhilHealthTable Z WHERE '" + _DailyRate + @"' BETWEEN Z.BracketFrom AND Z.BracketTo";
        _PhilHealthEmployee = clsSQLClientFunctions.GetNumericValue(_ConString, _sqlSyntax, "Employee");
        _PhilHealthEmployer = clsSQLClientFunctions.GetNumericValue(_ConString, _sqlSyntax, "Employer");
        double _PhilHealthRate = clsSQLClientFunctions.GetNumericValue(_ConString, _sqlSyntax, "Rate");

        if (_PhilHealthRate != 1)
        {
            double _PhilHealthBase = double.Parse((_DailyRate * _RateDivisor * (_PhilHealthRate / 100)).ToString("N2"));
            double _FirstRead = double.Parse((_PhilHealthBase / 2).ToString("N2"));
            double _SecondRead = double.Parse((_PhilHealthBase - _FirstRead).ToString("N2"));

            //double _PhilHealthBase = Math.Round((_DailyRate * _RateDivisor * (_PhilHealthRate / 100)), 2,MidpointRounding.AwayFromZero);
            //double _FirstRead = Math.Round((_PhilHealthBase / 2), 2, MidpointRounding.AwayFromZero);
            //double _SecondRead = Math.Round((_PhilHealthBase - _FirstRead), 2, MidpointRounding.AwayFromZero);

            if (_FirstRead < _SecondRead)
            {
                _PhilHealthEmployer = _SecondRead;
                _PhilHealthEmployee = _FirstRead;
            }
            else
            {
                _PhilHealthEmployer = _FirstRead;
                _PhilHealthEmployee = _SecondRead;
            }

        }

    }



}