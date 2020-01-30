

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
    public partial class caption_transfer_price : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public Int32 caption_transfer_price_Id { get; set; }

        [DataMember]
        [Required]
        public string Caption { get; set; }

        [DataMember]
        [Required]
        public string Currency { get; set; }

        [DataMember]
        [Required]
        public Decimal Rating { get; set; }

        public int EntityId
        {
            get
            {
                return caption_transfer_price_Id;
            }
        }

    }
}
