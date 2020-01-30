﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Fintrak.Client.MPR.Contracts
{
    [DataContract]
    public class PublicSectorData : DataContractBase
    {
        [DataMember]
        public string Account_Number { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public string PSEC_ProductCode { get; set; }
    }
}
