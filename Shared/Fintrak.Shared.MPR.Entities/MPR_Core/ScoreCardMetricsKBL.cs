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
    public partial class ScoreCardMetricsKBL : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int MetricID { get; set; }

        [DataMember]
        //[Required]
        public string Metric_Description { get; set; }

        [DataMember]
        //[Required]
        public string Metric { get; set; }

        [DataMember]
        //[Required]
        public string Actual { get; set; }

        [DataMember]
       // [Required]
        public string Budget { get; set; }

        [DataMember]
       // [Required]
        public bool TargetIsPreviousYear { get; set; }

        [DataMember]
       // [Required]
        public bool TargetOverActual { get; set; }
        [DataMember]
        // [Required]
        public decimal Divisior { get; set; }

        [DataMember]
        // [Required]
        public int Position { get; set; }
        [DataMember]
        // [Required]
        public int Year { get; set; }

        [DataMember]
        // [Required]
        public bool SetToZeroIfNoBudget { get; set; }
        [DataMember]
        // [Required]
        public string YTDAction { get; set; }
        [DataMember]
        // [Required]
        public bool SetToZeroIfNegativeActual { get; set; }

       
        public int EntityId
        {
            get
            {
                return MetricID;
            }
        }
    }
}
