using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Contracts
{
    [DataContract]
    public class CustomerTransferPriceData : DataContractBase
    {
        [DataMember]
        public int customertransferpriceId { get; set; }

        [DataMember]
        public string CustNo { get; set; }

        [DataMember]
        //public Fintrak.Shared.MPR.Framework.BalanceSheetCategory BalanceSheetCategory { get; set; }
        public Fintrak.Shared.MPR.Framework.BalanceSheetCategory Category { get; set; }

        [DataMember]
        public string BSCategoryName { get; set; }

        [DataMember]
        public double Rate { get; set; }

        [DataMember]
        public int Period { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }
        [DataMember]
        public int SolutionId { get; set; }
    }
}
