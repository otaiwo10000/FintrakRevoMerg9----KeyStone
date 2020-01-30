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
    public partial class TeamSector : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int Mpr_Team_Sector_ID { get; set; }
    
        [DataMember]
        [Required]
        public string Level1Code { get; set; }

        [DataMember]
        [Required]
        public string Level1Name { get; set; }

        [DataMember]
        [Required]
        public string Level2Code { get; set; }

        [DataMember]
        [Required]
        public string Level2Name { get; set; }

        [DataMember]
        [Required]
        public string Level3Code { get; set; }

        [DataMember]
        [Required]
        public string Level3Name { get; set; }

        [DataMember]
        [Required]
        public string Level4Code { get; set; }

        [DataMember]
        [Required]
        public string Level4Name { get; set; }

        public int EntityId
        {
            get
            {
                return Mpr_Team_Sector_ID;
            }
        }

    }
}
