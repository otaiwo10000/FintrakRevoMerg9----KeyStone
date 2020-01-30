using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class CustomerTransferPriceInfo
    {
        public int customertransferpriceId { get; set; }
        public string CustNo { get; set; }
        //public Fintrak.Shared.MPR.Framework.BalanceSheetCategory BalanceSheetCategory { get; set; }
        public Fintrak.Shared.MPR.Framework.BalanceSheetCategory Category { get; set; }
        public string BSCategoryName { get; set; }
        public int Period { get; set; }
        public int Year { get; set; }          
        public double Rate { get; set; }
        public int SolutionId { get; set; }
        public string CompanyCode { get; set; }
    }
}