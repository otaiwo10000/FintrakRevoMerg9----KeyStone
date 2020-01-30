using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class ProductTransferPriceInfo
    {
        
        public int ID { get; set; }

        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Rating { get; set; }

        public string Description { get; set; }

        public Fintrak.Shared.MPR.Framework.BalanceSheetCategory Category { get; set; }
        public string BSCategoryName { get; set; }
        
    }
}