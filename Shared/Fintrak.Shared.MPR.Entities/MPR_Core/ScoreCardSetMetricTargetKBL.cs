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
    public partial class ScoreCardSetMetricTargetKBL : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public string SetMetric { get; set; }

        [DataMember]
        public decimal SetTarget { get; set; }

        [DataMember]
        //[Required]
        public decimal FullYear { get; set; }
        [DataMember]
        //[Required]
        public bool ProrateYTD { get; set; }
        [DataMember]
        public int Period { get; set; }

        [DataMember]
        public int Year { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
