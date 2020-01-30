using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class CategoryTransferPriceInfo
    {
        public int CategoryTransferPriceId { get; set; }
        public Fintrak.Shared.MPR.Framework.BalanceSheetCategory BalanceSheetCategory { get; set; }
        public string BSCategoryName { get; set; }
        public int Period { get; set; }
        public int Year { get; set; }      
        public Fintrak.Shared.MPR.Framework.CurrencyType CurrencyType { get; set; }
        public string CurrencyTypeName { get; set; }
        public decimal Rate { get; set; }
    }
}