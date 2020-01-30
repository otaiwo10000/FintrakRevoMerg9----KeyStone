

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
    public partial class ScoreCardPerspective : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public Int32 PerspectiveId { get; set; }

        [DataMember]
        [Required]
        public string Perspective { get; set; }

        [DataMember]
        [Required]
        public string PerspectiveSub { get; set; }

        [DataMember]
        [Required]
        public int Year { get; set; }

        [DataMember]
        [Required]
        public int Position { get; set; }


        public int EntityId
        {
            get
            {
                return PerspectiveId;
            }
        }

    }
}
