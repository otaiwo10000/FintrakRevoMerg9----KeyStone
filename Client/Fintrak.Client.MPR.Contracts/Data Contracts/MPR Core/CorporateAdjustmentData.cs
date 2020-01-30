
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.MPR.Contracts
{
    [DataContract]
    public class CorporateAdjustmentData : DataContractBase
    {
        [DataMember]
        public int CorporateAdjustmentId { get; set; }

        [DataMember]
        public string TeamCode { get; set; }

        [DataMember]
        public string AccountOfficerCode { get; set; }

        [DataMember]
        public string Narrative { get; set; }

        [DataMember]
        public string BranchCode { get; set; }

        [DataMember]
        public string GLCode { get; set; }


        [DataMember]
        public string Caption { get; set; }

        [DataMember]
        public string RelatedAccount { get; set; }   //account number

        [DataMember]
        public string AccountName { get; set; }

        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        public DateTime RunDate { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public string AdjustmentCode { get; set; }

        [DataMember]
        public string BrokerCode { get; set; }

        [DataMember]
        public string GLDescription { get; set; }

        [DataMember]
        public string Code { get; set; }

    }
}
