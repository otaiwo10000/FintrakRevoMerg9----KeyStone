using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Business.MPR.Contracts
{
    [DataContract]
    public class ScoreCardData : DataContractBase
    {
        [DataMember]
        public int mpr_scorecard_stgId { get; set; }

        [DataMember]
        public string Caption { get; set; }

        [DataMember]
        public string CaptionCode { get; set; }

        [DataMember]
        public string Accountofficercode { get; set; }

        [DataMember]
        public string TeamCode { get; set; }

        [DataMember]
        public string Branchcode { get; set; }

        [DataMember]
        public decimal Actual { get; set; }
        [DataMember]
        public decimal AverageBal { get; set; }

        [DataMember]
        public decimal Amount { get; set; }
        [DataMember]
        public decimal Budget { get; set; }

        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public int Period { get; set; }
        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public DateTime Rundate { get; set; }

    }
}
