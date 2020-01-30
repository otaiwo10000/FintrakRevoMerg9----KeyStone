using Fintrak.Client.Core.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.SystemCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    //public class OpexStaffExpDiffModel
    //{
    //    public int OPEX_STAFFEXP_DIFF_Id { get; set; }
    //    public string Salary_BranchCode { get; set; }
    //    public string RawExpense_BranchCode { get; set; }
    //    public double SalaryAMount { get; set; }
    //    public double RawExpenseAmount { get; set; }
    //    public double Differences { get; set; }

    //}


    public class OpexStaffExpDiffModel
    {
        public int ID { get; set; }
        public string GL_Code { get; set; }
        public string GL_Description { get; set; }
        public double FinstatAmount { get; set; }
        public double OpexAmount { get; set; }
        public double Difference { get; set; }
    }

    public class OpexStaffExpDiffModel_2
    {
        public int ID { get; set; }
        public string GL_Code { get; set; }
        public double Difference { get; set; }
        public string miscode { get; set; }
    }

    public class RawExpensesFixedModel
    {
        public int FINID { get; set; }
        public string GL_Code { get; set; }
        public string GL_Name { get; set; }
        public string Post_Date { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public string ChkMIS1 { get; set; }
        public string ChkMIS2 { get; set; }
        public string MIS_Code { get; set; }
        public string BranchCode { get; set; }
        public string TRANSID { get; set; }
        public string SUBGL { get; set; }
        public string SBUCODE { get; set; }
        public string Account { get; set; }
        public string Product { get; set; }
    }

    public class OPEX_DETAIL_SHARE_BUD
    {
        public string TEAM_CODE { get; set; }
        public string BranchName { get; set; }
        public string RegionName { get; set; }
        public string DIRECTORATENAME { get; set; }
        public string CAPTION { get; set; }
        public double AMOUNT { get; set; }
      
    }

    public class sharedCostCC
    {
        public string MIS_CODE { get; set; }
        public double AMOUNT { get; set; }
        //public string TEAM_CODE { get; set; }
       
    }

}