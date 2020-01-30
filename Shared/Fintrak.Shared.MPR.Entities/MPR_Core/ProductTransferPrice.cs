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
    public partial class ProductTransferPrice : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        public string ProductCode { get; set; }

        [DataMember]
        [Required]
        public string Rating { get; set; }

        [DataMember]
        //[Required]
        public string Description { get; set; }

        [DataMember]
        //   [Required]
        public Fintrak.Shared.MPR.Framework.BalanceSheetCategory Category { get; set; }
       // public string Category { get; set; }


        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
