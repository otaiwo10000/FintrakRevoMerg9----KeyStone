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
    public partial class TeamSegment : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int Mpr_Team_Segment_ID { get; set; }
    
        [DataMember]
        [Required]
        public string TargetCode { get; set; }

        [DataMember]
        [Required]
        public string TargetSegment { get; set; }

        [DataMember]
        [Required]
        public string CustomerTypeCode { get; set; }

        [DataMember]
        [Required]
        public string CustomerType { get; set; }

        [DataMember]
        [Required]
        public string CustomerSegmentCode { get; set; }

        [DataMember]
        [Required]
        public string CustomerSegment { get; set; }

       
        public int EntityId
        {
            get
            {
                return Mpr_Team_Segment_ID;
            }
        }

    }
}
