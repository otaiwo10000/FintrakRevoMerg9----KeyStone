

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.MPR.Contracts
{
    [DataContract]
    public class account_interestData : DataContractBase
    {
        [DataMember]
        [System.ComponentModel.Browsable(false)]
        public Int32 account_interest_Id { get; set; }

        [DataMember]
      //  [Required]
        public string AccountNo { get; set; }

        [DataMember]
       // [Required]
        public Double InterestRate { get; set; }

        [DataMember]
       // [Required]
        public string Productcode { get; set; }

        [DataMember]
       // [Required]
        public string Period { get; set; }

        [DataMember]
       // [Required]
        public string  Year { get; set; }

        [DataMember]
        public string caption { get; set; }
    }
}
