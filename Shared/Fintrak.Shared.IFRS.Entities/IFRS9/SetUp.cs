using Fintrak.Shared.IFRS.Framework;
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

namespace Fintrak.Shared.IFRS.Entities
{
    public partial class SetUp : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int SetUpId { get; set; }

        [DataMember]
        public double Threshold { get; set; }

        [DataMember]
        public int Deteroriation_Level { get; set; }

        [DataMember]
        public int Classification_Type { get; set; }
        [DataMember]
        public int Historical_PD_Year_Count { get; set; }

        public bool PDBasis { get; set; }
        [DataMember]

        public int Ltpdapproach { get; set; }
        [DataMember]
        public bool Active { get; set; }

        [DataMember]
        public double CCF { get; set; }

        [DataMember]
        public string GroupBased { get; set; }

        public int EntityId
        {
            get
            {
                return SetUpId;
            }
        }
    }
}
