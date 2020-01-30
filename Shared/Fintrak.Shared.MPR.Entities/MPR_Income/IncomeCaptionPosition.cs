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
    public partial class IncomeCaptionPosition : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        public string Caption { get; set; }

        //[DataMember]
        //[Required]
        //public string Category_Description { get; set; }

        [DataMember]
        [Required]
        public int position { get; set; }

        [DataMember]
        // [Required]
        public int Class { get; set; }
        [DataMember]
        // [Required]
        public string ReportColour { get; set; }

        [DataMember]
       [Required]
        public bool Visible { get; set; }

        [DataMember]
        [Required]
        public bool IsBreakdown { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
