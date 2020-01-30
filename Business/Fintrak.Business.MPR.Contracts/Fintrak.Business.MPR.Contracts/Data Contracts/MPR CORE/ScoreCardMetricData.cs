using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Business.MPR.Contracts
{
    [DataContract]
    public class ScoreCardMetricsData : DataContractBase
    {
        [DataMember]
        public int MetricId { get; set; } 

        [DataMember]
        public string Metric { get; set; }

        [DataMember]
        public int Metric_Code { get; set; }

        [DataMember]
        public string Metric_Description { get; set; }

        [DataMember]
        public string MisCode { get; set; }

        [DataMember]
        public int Metric_Score_determinant { get; set; }

        [DataMember]
        public int Period { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public int PerspectiveId { get; set; }
        [DataMember]
        public string Perspective { get; set; }

        [DataMember]
        public int Metric_Position { get; set; }

        [DataMember]
        public int Mapping_Code { get; set; }
    }
}
