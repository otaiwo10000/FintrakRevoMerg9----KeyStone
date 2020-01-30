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
    public partial class IncomeSplitPoolsRate : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        public decimal PoolRateLCYAsset { get; set; }

        [DataMember]
        [Required]
        public decimal PoolRateLCYLiability { get; set; }

        [DataMember]
        [Required]
        public decimal PoolRateFCYAsset { get; set; }

        [DataMember]
        [Required]
        public decimal PoolRateFCYLiability { get; set; }

        [DataMember]
        [Required]
        public int Period { get; set; }

        [DataMember]
        [Required]
        public int Year { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
