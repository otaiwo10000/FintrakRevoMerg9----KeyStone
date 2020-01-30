using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class MISTransferPriceInfo
    {
        public int mistransferpriceId { get; set; }

        public string DefinitionCode { get; set; }

      
        public string MisCode { get; set; }

   
        public Fintrak.Shared.MPR.Framework.BalanceSheetCategory BalanceSheetCategory { get; set; }

        public string BSCategoryName { get; set; }

        
        public Fintrak.Shared.MPR.Framework.CurrencyType CurrencyType { get; set; }
        public string CurrencyTypeName { get; set; }

        public double Rate { get; set; }

       
        public int Period { get; set; }

   
        public int Year { get; set; }

      
        public int SolutionId { get; set; }

       
        public string CompanyCode { get; set; }
    }
    
}