using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Business.MPR.Contracts
{
    [DataContract]
    public class CategoryTransferPriceData : DataContractBase
    {
        [DataMember]
        public int CategoryTransferPriceId { get; set; }

        [DataMember]
        public Fintrak.Shared.MPR.Framework.BalanceSheetCategory BalanceSheetCategory { get; set; }

        [DataMember]
        public string BSCategoryName { get; set; }

        [DataMember]
        public Fintrak.Shared.MPR.Framework.CurrencyType CurrencyType { get; set; }

        [DataMember]
        public string CurrencyTypeName { get; set; }

        [DataMember]
        public decimal Rate { get; set; }

        [DataMember]
        public int Period { get; set; }

        [DataMember]
        public int Year { get; set; }

        
    }
}
