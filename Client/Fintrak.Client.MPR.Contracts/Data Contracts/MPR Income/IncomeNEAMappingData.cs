

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.MPR.Contracts
{
    [DataContract]
    public class IncomeNEAMappingData : DataContractBase
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Category_Code { get; set; }
        [DataMember]
        public string CATEGORY_DESCRIPTION { get; set; }
        [DataMember]
        public string Product_Code { get; set; }
        [DataMember]
        public string Class { get; set; }
        [DataMember]
        public string Caption { get; set; }
        [DataMember]
        public string AssetType { get; set; }
    }
}
