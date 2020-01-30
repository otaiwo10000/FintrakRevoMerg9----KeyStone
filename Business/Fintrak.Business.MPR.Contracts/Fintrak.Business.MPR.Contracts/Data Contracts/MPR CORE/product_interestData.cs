
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Business.MPR.Contracts
{
    [DataContract]
    public class product_interestData : DataContractBase
    {
        [DataMember]
        //[Browsable(false)]
        public Int32 product_interestId { get; set; }

        [DataMember]
        // [Required]
        public string ProductCode { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        //[Required]
        public AccountTypeEnum Category { get; set; }

        [DataMember]
        public string CategoryName { get; set; }

        [DataMember]
        // [Required]
        public Double InterestRate { get; set; }
    }
}
