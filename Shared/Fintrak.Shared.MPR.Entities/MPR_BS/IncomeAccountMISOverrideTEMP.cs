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
    public partial class IncomeAccountMISOverrideTEMP : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public string accountnumber { get; set; }


        [DataMember]
        [Required]
        public string mis { get; set; }

        [DataMember]
        //[Required]
        public string AccountOfficer_Code { get; set; }
       
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
