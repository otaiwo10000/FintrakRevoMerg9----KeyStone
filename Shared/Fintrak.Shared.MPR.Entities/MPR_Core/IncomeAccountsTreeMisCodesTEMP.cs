using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
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
    public partial class IncomeAccountsTreeMisCodesTEMP : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        public string AccountNumber { get; set; }

        [DataMember]
        [Required]
        public string AccountOfficer_Code { get; set; }

        [DataMember]
        [Required]
        public string AccountOfficerName { get; set; }

        [DataMember]
        [Required]
        public decimal ShareRatio { get; set; }

        //[DataMember]
        //[Required]
        //public decimal Ratio { get; set; }

        [DataMember]
        [Required]
        public string Team_Code { get; set; }

        [DataMember]
        [Required]
        public string ApprovalStatus { get; set; }

        [DataMember]
        [Required]
        public bool Migrated { get; set; }


        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
