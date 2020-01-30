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
    public partial class RiskAdjustedCharge : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int RiskAdjustedChargeId { get; set; }

        [DataMember]
        [Required]
        public string AccountNo { get; set; }

        [DataMember]
        [Required]
        public Double Amount { get; set; }

        [DataMember]
        [Required]
        public string RiskDefinitionCode { get; set; }

        [DataMember]
        [Required]
        public string AccountOfficerCode { get; set; }

        [DataMember]
        [Required]
        public string TeamCode { get; set; }

        [DataMember]
        [Required]
        public string BranchCode { get; set; }

        [DataMember]
        [Required]
        public string MemoCode { get; set; }

        [DataMember]
        [Required]
        public string SectorCode { get; set; }

        [DataMember]
        [Required]
        public string SegmentCode { get; set; }

        [DataMember]
        [Required]
        public string AccountTitle { get; set; }

        [DataMember]
        [Required]
        public string CurrencyType { get; set; }

        [DataMember]
        [Required]
        public string Productcode { get; set; }
        public int EntityId
        {
            get
            {
                return RiskAdjustedChargeId;
            }
        }
    }
}
