
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.MPR.Contracts
{
    [DataContract]
    public class ScoreCardPerspectiveData : DataContractBase
    {
        [DataMember]
        public Int32 PerspectiveId { get; set; }

        [DataMember]
        public string Perspective { get; set; }

        [DataMember]
        public string PerspectiveSub { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public int Position { get; set; }

    }
}
