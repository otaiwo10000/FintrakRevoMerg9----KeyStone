using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.MPR.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fintrak.Shared.MPR.Entities
{
    public partial class CorporateAdjustment : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int CorporateAdjustmentId { get; set; }

        [DataMember]
        [Required]
        public string TeamCode { get; set; }

        [DataMember]
        [Required]
        public string AccountOfficerCode { get; set; }

        [DataMember]
        [Required]
        public string Narrative { get; set; }

        [DataMember]
        [Required]
        public string BranchCode { get; set; }

        [DataMember]
        [Required]
        public string GLCode { get; set; }


        [DataMember]
        [Required]
        public string Caption { get; set; }

        [DataMember]
        [Required]
        public string RelatedAccount { get; set; }   //account number

        [DataMember]
        [Required]
        public string AccountName { get; set; }

        [DataMember]
        [Required]
        public double Amount { get; set; }

        [DataMember]
        [Required]
        public DateTime RunDate { get; set; }

        [DataMember]
        [Required]
        public string CompanyCode { get; set; }

        [DataMember]
        [Required]     
        public string AdjustmentCode { get; set; }

        [DataMember]
        [Required]
        public string BrokerCode { get; set; }

        [DataMember]
        [Required]
        public string GLDescription { get; set; }

        [DataMember]
        [Required]
        public string Code { get; set; }

        public int EntityId
        {
            get
            {
                return CorporateAdjustmentId;
            }
        }
    }
}
