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
    public partial class IncomeCRRSector : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        public string SECTOR_CODE { get; set; }

        [DataMember]
        [Required]
        public string SECTOR { get; set; }

        [DataMember]
        public decimal CRR { get; set; }
     
       
        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
