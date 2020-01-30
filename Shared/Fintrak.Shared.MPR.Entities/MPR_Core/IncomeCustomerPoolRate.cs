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
    public partial class IncomeCustomerPoolRate : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public string CustomerNo { get; set; }

        [DataMember]
        [Required]
        public string AccountClass { get; set; }

        [DataMember]
        [Required]
        public string AccountClassName { get; set; }

        [DataMember]
        [Required]
        public decimal PoolRate { get; set; }
       
        [DataMember]
        [Required]
        public int Year { get; set; }

        public int EntityId
        {
            get
            {
                return Id;
            }
        }
    }
}
