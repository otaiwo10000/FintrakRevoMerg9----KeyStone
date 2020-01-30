

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.MPR.Contracts
{
    [DataContract]
    public class ScoreCardMappingData : DataContractBase
    {
        [DataMember]
        public int MappingId { get; set; }

        [DataMember]
        public int Metric_Code { get; set; }

        [DataMember]
        public string Actual_Caption { get; set; }

        [DataMember]
        public string Budget_Caption { get; set; }

        [DataMember]
        public int Period { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public int Mapping_code { get; set; }

        [DataMember]
        public string Metric { get; set; }
    }
}
