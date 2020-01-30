using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class IncomeAdjustmentVolumeSearchModel
    {
        public Int64 ID { get; set; }
        public string Caption { get; set; }
        public string AccountNumber { get; set; }
        public string Refno { get; set; }
        public string customername { get; set; }
        public string EntryStatus { get; set; }
        public string sbuCode { get; set; }
        public string MIS_Code { get; set; }
        public string accountofficercode { get; set; }
        //public string accountofficer { get; set; }
        public decimal ActualBalance { get; set; }
        public decimal AverageBalance { get; set; }
        public decimal RevExp { get; set; }
        
        //public decimal Pool { get; set; }
        public decimal interestRate { get; set; }
        public decimal PoolRate { get; set; }
        public string ProductCode { get; set; }
        public string Category { get; set; }
        public string Currency_Type { get; set; }
        public string GL_Sub { get; set; }
        public DateTime postedDate { get; set; }
        public string Indicator { get; set; }
        public Int64 Period { get; set; }
        public Int64 Year { get; set; }

    }


    public class AddIncomeAdjustmentVolumeSearchModel
    {
        public Int64 ID { get; set; }
        public string MISCODE { get; set; }
        public string ACCTCODE { get; set; }
        public string ACCOUNTNUMBER { get; set; }
        public string CUSTNAME { get; set; }
        public double BALANCE { get; set; }
        public double AVERAGE { get; set; }
        public double INTEREST { get; set; }
        public string PRODUCTCODE { get; set; }
        public string CATEGORY { get; set; }
        public string CURRENCY { get; set; }

    }

    public class UpdateIncomeAdjustmentVolumeSearchModel
    {
        public Int64 ID { get; set; }
        public string MISCODE { get; set; }
        public string ACCTCODE { get; set; }
        public int PERIOD { get; set; }
        public int YEAR { get; set; }
        public string ACCOUNTNUMBER { get; set; }
        public string PRODUCTCODE { get; set; }
        public string CATEGORY { get; set; }
        public string CURRENCY { get; set; }
        public string CUSTNAME { get; set; }
        public string CAPTION { get; set; }
        public string ACCOUNTNUMBER1 { get; set; }
       

    }


}