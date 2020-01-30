
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
    public class IncomeAccountsNplData : DataContractBase
    {
        [DataMember]
        public string AcccountNumber { get; set; }
        [DataMember]
        public string AccountNumber { get; set; }
        [DataMember]
        public string CustomerName { get; set; }
    }
}
