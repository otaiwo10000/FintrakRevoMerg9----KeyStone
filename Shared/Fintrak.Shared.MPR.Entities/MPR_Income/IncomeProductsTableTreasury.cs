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
    public partial class IncomeProductsTableTreasury : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        public string GLCode { get; set; }

        [DataMember]
        //[Required]
        public string Caption { get; set; }

        [DataMember]
        // [Required]
        public string Type { get; set; }

        [DataMember]
        // [Required]
        public string Status { get; set; }

        [DataMember]
        // [Required]
        public string Currency { get; set; }

        [DataMember]
        // [Required]
        public string SBUCode { get; set; }

        [DataMember]
       // [Required]
        public string Category { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
