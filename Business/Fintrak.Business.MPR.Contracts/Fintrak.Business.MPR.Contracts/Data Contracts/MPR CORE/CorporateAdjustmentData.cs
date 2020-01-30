

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Business.MPR.Contracts
{
    [DataContract]
    public class CorporateAdjustmentData : DataContractBase
    {
        [DataMember]
        // [Browsable(false)]
        public Int32 CorporateAdjustmentId { get; set; }

        [DataMember]
        // [Required]
        public string TeamCode { get; set; }

        [DataMember]
        // [Required]
        public string AccountOfficerCode { get; set; }

        [DataMember]
        // [Required]
        public string Narrative { get; set; }

        [DataMember]
        public string BranchCode { get; set; }

        [DataMember]
        // [Required]
        public string GLCode { get; set; }

        [DataMember]
        // [Required]
        public string Caption { get; set; }

        [DataMember]
        // [Required]
        public string RelatedAccount { get; set; }

        [DataMember]
        public string AccountName { get; set; }

        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        // [Required]
        public DateTime RunDate { get; set; }

        [DataMember]
        // [Required]
        public string CompanyCode { get; set; }

        [DataMember]
        // [Required]
        public string AdjustmentCode { get; set; }

        [DataMember]
        public string BrokerCode { get; set; }

        [DataMember]
        // [Required]
        public string GLDescription { get; set; }

        [DataMember]
        public string Code { get; set; }
    }
}
