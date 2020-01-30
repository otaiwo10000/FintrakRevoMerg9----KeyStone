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
    public partial class IncomeCurrency : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        public string CurrencyCode { get; set; }

        [DataMember]
        [Required]
        public double Rate { get; set; }

        [DataMember]
        [Required]
        public string Description { get; set; }

        [DataMember]
        [Required]
        public string CurrencyType { get; set; }

        [DataMember]
        [Required]
        public DateTime DateModified { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
