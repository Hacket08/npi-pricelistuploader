using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class clsSystemQuery
{

    public static string _qryEmployeeList(string _Area = "", string _Branch = "", string _EmpCode = "", string _EmpName = "", string _Position = "")
    {
        string _Syntax = "";
        _Syntax = @"

            SELECT A.[EmployeeNo]
,A.[LastName],A.[FirstName],A.[MiddleName],A.[MiddleInitial],A.[SuffixName],A.[DateHired],A.[DateFinish]
,A.[EmpStatus],A.[PayrollMode],A.[Department],A.[Category],A.[MonthlyRate],A.[DailyRate],A.[SSSNo],A.[PhilHealthNo],A.[PagIbigNo]
,A.[TaxIDNo],A.[BankAccountNo],A.[EmpPosition],A.[Address01],A.[Address02],A.[Telephone01],A.[Telephone02],A.[Birthday],A.[CivilStatus]
,A.[Gender],A.[WithSSS],A.[WithPhilHealth],A.[WithTIN],A.[WithPagibig],A.[TaxStatus],A.[DateOfClearance],A.[PayrollTerms]
,A.[ScheduleCode],A.[WithOvertime],A.[WithTardiness],A.[RestDay],A.[IsFlexiTime],A.[COLAAmount],A.[ConfiLevel],A.[TimeKeepingID]
,A.[CustomPYCode],A.[RateDivisor],A.[MonthlyAllowance],A.[DailyAllowance],A.[BankCode],A.[WithPERAA],A.[PERAANo]
,A.[WithUndertime],A.[AttendanceExempt],A.[TimekeepingAbsHrs],A.[TimekeepingAbsMins],A.[BillingRate],A.[SLVLCode],A.[SchedAllow]
,A.[SchedPass],A.[GracePeriod],A.[TardySetup],A.[UseTardyTable],A.[IncludeGracePeriod],A.[MaxTardiness],A.[DaysOfYear],A.[TaxStart]
,A.[TaxEnd],A.[DateRegular],A.[Remarks],A.[DepCode],A.[AsBranchCode]
,(SELECT DISTINCT X.[BName] FROM [vwsDepartmentList] X WHERE X.[BCode] = A.[AsBranchCode]) AS [AsBranchName]
,A.[Company],A.[EmployeeName], A.[dftLeaveCredit]
,B.[DepartmentCode],B.[DepartmentDesc],B.[DepName],B.[BranchCode],B.[BranchName]
,B.[AREA],B.[BCode],B.[BName],B.[SchedCode], C.CompanyName
                            FROM[vwsEmployees] A INNER JOIN [vwsDepartmentList] B ON A.Department = B.DepartmentCode
                                    INNER JOIN [OCMP] C ON A.Company = C.CompanyCode AND C.Active = 1
                                            WHERE B.Area LIKE '%" + _Area + @"%' 
										                AND B.BCode LIKE '%" + _Branch + @"%'
										                AND A.EmployeeNo  COLLATE Latin1_General_CI_AS  LIKE  '%" + _EmpCode + @"%' 
										                AND A.EmployeeName  COLLATE Latin1_General_CI_AS  LIKE  '%" + _EmpName + @"%' 
                                                        AND B.DepCode LIKE '%" + _Position + @"%'
										                AND A.ConfiLevel IN (" + clsDeclaration.sConfiLevel + @")
                        ";


        return _Syntax;
    }


    public static string _qryLeaveGeneratedList(string _Year = "", string _Area = "", string _Branch = "", string _EmpCode = "", string _EmpName = "", string _Position = "")
    {
        string _Syntax = "";
        _Syntax = @"

            SELECT X1.*,A.[Company],A.[EmployeeName]
,B.[DepartmentCode],B.[DepartmentDesc],B.[DepName],B.[BranchCode],B.[BranchName]
,B.[AREA],B.[BCode],B.[BName],B.[SchedCode], C.CompanyName
                            FROM [vwsLeaveFileData] X1 INNER JOIN [vwsEmployees] A ON X1.EmployeeNo = A.EmployeeNo
                                    INNER JOIN [vwsDepartmentList] B ON A.Department = B.DepartmentCode
                                    INNER JOIN [OCMP] C ON A.Company = C.CompanyCode AND C.Active = 1
                                            WHERE B.Area LIKE '%" + _Area + @"%' 
										                AND B.BCode LIKE '%" + _Branch + @"%'
										                AND A.EmployeeNo  COLLATE Latin1_General_CI_AS  LIKE  '%" + _EmpCode + @"%' 
										                AND A.EmployeeName  COLLATE Latin1_General_CI_AS  LIKE  '%" + _EmpName + @"%' 
                                                        AND B.DepCode LIKE '%" + _Position + @"%'
										                AND A.ConfiLevel IN (" + clsDeclaration.sConfiLevel + @")
                                                        AND X1.Year = '" + _Year + @"'
                        ";


        return _Syntax;
    }
}