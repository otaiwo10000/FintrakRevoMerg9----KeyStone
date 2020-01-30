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
    public partial class AssetType : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public Int32 AssetType_Id { get; set; }

        [DataMember]
        [Required]
        public string AssetTypeCol { get; set; }
      
        public int EntityId
        {
            get
            {
                return AssetType_Id;
            }
        }
    }
}
