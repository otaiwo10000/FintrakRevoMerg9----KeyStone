using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class MPRDownloadBaseFintrakFinalManualModel
    {
        public Int64 ID { get; set; }
        public string AccountNumber { get; set; }
        public string customername { get; set; }
        public string sbuCode { get; set; }
        public string MIS_Code { get; set; }
        public string accountofficercode { get; set; }
        public string accountofficer { get; set; }
        public decimal ActualBalance { get; set; }
        public decimal AverageBalance { get; set; }
        public decimal RevExp { get; set; }
        public decimal interestRate { get; set; }

        public string ProductCode { get; set; }
        public string Category { get; set; }
        public string Currency_Type { get; set; }
        public DateTime postedDate { get; set; }
        public Int64 Period { get; set; }
        public Int64 Year { get; set; }

        public string EntryStatus { get; set; }
        public string GL_Sub { get; set; }
        public string Refno { get; set; }
        public decimal PoolRate { get; set; }
        public decimal BankMaxRate { get; set; }
        public string CustomerRating { get; set; }
        public decimal EffYield { get; set; }
        public decimal PenalRate { get; set; }
        public decimal PenalCharge { get; set; }
        public decimal ExpRev { get; set; }

        public string Caption { get; set; }
        public string Category_Filter { get; set; }
        public string Branch { get; set; }
        public decimal Share_Ratio { get; set; }
        public string Indicator { get; set; }
        public DateTime Entry_Date { get; set; }
        public string Currency_Code { get; set; }
    }

   
}