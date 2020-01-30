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
    public partial class IncomeNEAMapping : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        public string Category_Code { get; set; }

        //[DataMember]
        //[Required]
        //public string Category_Description { get; set; }

        [DataMember]
        //[Required]
        public string Product_Code { get; set; }

        [DataMember]
        // [Required]
        public string Class { get; set; }
        [DataMember]
        // [Required]
        public string Caption { get; set; }

        [DataMember]
        // [Required]
        public string AssetType { get; set; }
        
        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
