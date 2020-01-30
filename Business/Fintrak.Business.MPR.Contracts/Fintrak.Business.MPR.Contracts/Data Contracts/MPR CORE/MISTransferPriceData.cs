using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Business.MPR.Contracts
{
    [DataContract]
    public class MISTransferPriceData : DataContractBase
    {
        [DataMember]
        public int mistransferpriceId { get; set; }

        [DataMember]
        public string DefinitionCode { get; set; }

        [DataMember]
        public string MisCode { get; set; }

        [DataMember]
        public Fintrak.Shared.MPR.Framework.BalanceSheetCategory BalanceSheetCategory { get; set; }

        [DataMember]
        public string BSCategoryName { get; set; }

        [DataMember]
        public Fintrak.Shared.MPR.Framework.CurrencyType CurrencyType { get; set; }

        [DataMember]
        public string CurrencyTypeName { get; set; }

        [DataMember]
        public double Rate { get; set; }

        [DataMember]
        public int Period { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public int SolutionId { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }
    }
}
