using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Business.MPR.Contracts
{
    [DataContract]
    public class ProductTransferPriceData : DataContractBase
    {
        [DataMember]
        public int ID { get; set; } 

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public string Rating { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public Fintrak.Shared.MPR.Framework.BalanceSheetCategory Category { get; set; }

        [DataMember]
        public string BSCategoryName { get; set; }
        

    }
}
