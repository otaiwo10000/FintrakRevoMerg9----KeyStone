

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.MPR.Contracts
{
    [DataContract]
    public class crb_DataData : DataContractBase
    {
        [DataMember]
       // [Browsable(false)]
        public Int32 crb_Data_Id { get; set; }

        [DataMember]
       // [Required]
        public DateTime xDate { get; set; }

        [DataMember]
       // [Required]
        public Int32 Count { get; set; }

        [DataMember]
       // [Required]
        public Decimal Volume { get; set; }

        [DataMember]
        public string caption { get; set; }
    }
}
