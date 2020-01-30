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
    public partial class MISNewOthersTEMP : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int Id { get; set; }

        [DataMember]
       // [Required]
        public string State { get; set; }

        [DataMember]
        // [Required]
        public string Teamname { get; set; }

        [DataMember]
        // [Required]
        public string new_mis { get; set; }

        [DataMember]
        // [Required]
        public string old_mis { get; set; }

        [DataMember]
        // [Required]
        public string Accountofficer_code { get; set; }

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
                return Id;
            }
        }
    }
}
