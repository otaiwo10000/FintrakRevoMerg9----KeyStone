
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
using Fintrak.Shared.Core.Framework;


namespace Fintrak.Shared.MPR.Entities
{
    public partial class product_interest : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public Int32 product_interestId { get; set; }

        [DataMember]
       // [Required]
        public string ProductCode { get; set; }

        [DataMember]
        [Required]
        public AccountTypeEnum Category { get; set; }

        [DataMember]
       // [Required]
        public Double InterestRate { get; set; }
        
        public int EntityId
        {
            get
            {
                return product_interestId;
            }
        }

    }
}
