using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fintrak.Shared.MPR.Entities
{
    public partial class IncomeMemorep : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        public string PL_CATEG { get; set; }

        [DataMember]
        [Required]
        [Column("CATEGORY NAME")]
        public string CATEGORYNAME { get; set; }

        [DataMember]
        [Required]
        public string GLName { get; set; }

        [DataMember]
        [Required]
        public int YEAR { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
