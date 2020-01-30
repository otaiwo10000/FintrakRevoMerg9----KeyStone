using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class IncomeAdjustmentSummaryModel  
    {
        public string Caption { get; set; }
        public string sbu_Code { get; set; }
        public string SBUName { get; set; }
        public string MIS_Code { get; set; }
        public string ProductCode { get; set; }
        public decimal ActualBalance { get; set; }
        public decimal AverageBalance { get; set; }
        public decimal Rev_Exp { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public string CurrencyType { get; set; }
        public DateTime RunDate { get; set; }
        public int Period { get; set; }
        public int Year { get; set; }
      
    }

   
}