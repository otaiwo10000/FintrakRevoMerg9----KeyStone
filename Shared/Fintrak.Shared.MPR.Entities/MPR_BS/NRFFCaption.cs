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
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Shared.MPR.Entities
{
    public partial class NRFFCaption : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int NRFFCaption_Id { get; set; }

        [DataMember]
        [Required]
        public string Description { get; set; }
      
        public int EntityId
        {
            get
            {
                return NRFFCaption_Id;
            }
        }

    }
}
