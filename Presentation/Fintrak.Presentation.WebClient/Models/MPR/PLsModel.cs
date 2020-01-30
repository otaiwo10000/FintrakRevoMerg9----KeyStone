using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class IncomeIncomeNewDetailsModel
    {
        public int ID { get; set; }
        public string MIS_Code { get; set; }
        public string Caption { get; set; }
        public string Accountnumber { get; set; }
        public string CustomerName { get; set; }
        public string AccountOfficer_Code { get; set; }
        public Int64 Period { get; set; }
        public Int64 Year { get; set; }
        public string Narrative { get; set; }
        public string EntryStatus { get; set; }
        public string GLName { get; set; }
        public float Amount { get; set; }
        public DateTime DateEntered { get; set; }
        public string ProductCode { get; set; }
        public string Tran_ID { get; set; }
        public DateTime Tran_Date { get; set; }
        public DateTime RunDate { get; set; }
        public string StaffID { get; set; }
        public string Sbucode { get; set; }
        

        public string username { get; set; }

   
    }

    public class IncomeIncomeOtherBreakdownModel
    {
        public Int64 ID { get; set; }
        public string MIS_Code { get; set; }
        public string Caption { get; set; }
        public string Accountnumber { get; set; }
        public string Narrative { get; set; }
        public string CustomerName { get; set; }
        public int Period { get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; }
        public string AccountOfficer_Code { get; set; }
        public decimal Volume { get; set; }
        public string Indicator { get; set; }
        public string EntryStatus { get; set; }
        public DateTime DateEntered { get; set; }
        public string ProductCode { get; set; }
        public DateTime RunDate { get; set; }
        public DateTime EntryDate { get; set; }
    }

   
}