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
    public partial class KBL_MISProductCategoryInfo : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public string CATEGORY_CODE { get; set; }

        [DataMember]
        //[Required]
        public string CATEGORY_DESCRIPTION { get; set; }

        [DataMember]
        // [Required]
        public string SHORT_NAME { get; set; }

       
        public int EntityId
        {
            get
            {
                return Id;
            }
        }
    }
}
