
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Contracts
{
    [DataContract]
    public class ScoreCardWeightData : DataContractBase
    {
        [DataMember]
        public int WeightId { get; set; }

        [DataMember]
        public int Metric_Code { get; set; }

        [DataMember]
        public string Metric { get; set; }

        [DataMember]
        public decimal Weight { get; set; }

        [DataMember]
        public int Period { get; set; }

        [DataMember]
        public int Year { get; set; }
    }
}
