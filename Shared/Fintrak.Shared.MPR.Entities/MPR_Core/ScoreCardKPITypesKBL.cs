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
    public partial class ScoreCardKPITypesKBL : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        //[Required]
        public string KPI_TYPE { get; set; }

        [DataMember]
        //[Required]
        public string PERSPECTIVE { get; set; }

        [DataMember]
        //[Required]
        public string KPI_METRIC { get; set; }

        [DataMember]
        //[Required]
        public decimal KPI_WEIGHT { get; set; }

        [DataMember]
        //[Required]
        public int Period { get; set; }

        [DataMember]
        //[Required]
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
