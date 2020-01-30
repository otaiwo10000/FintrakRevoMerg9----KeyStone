using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Fintrak.Shared.Core.Entities
{
    public partial class IncomeMonths : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        public int Period { get; set; }

        [DataMember]
        [Required]
        public string Month { get; set; }

        [DataMember]
        public int NumberOfDaysInMonth { get; set; }

        [DataMember]
        [Required]
        public int Year { get; set; }

        [DataMember]
        [Required]
        public int LastDay { get; set; }
        
        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
