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
    public partial class IncomeAccountsFintrak : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        public string AccountNumber { get; set; }

        [DataMember]
        [Required]
        public string CustomerName { get; set; }

        [DataMember]
        [Required]
        public string MIS_Code { get; set; }

        [DataMember]
        [Required]
        public string AccountOfficer_Code { get; set; }

        [DataMember]
        [Required]
        public string OldMIS_Code { get; set; }

        [DataMember]
        [Required]
        public string OldAccountOfficer_Code { get; set; }

        [DataMember]
        [Required]
        public int ExpirationPeriod { get; set; }

        [DataMember]
        [Required]
        public string Comments { get; set; }

        [DataMember]
        [Required]
        public int ExpirationYear { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
