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
    public partial class ScoreCardMetrics : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int MetricId { get; set; }

        [DataMember]
        [Required]
        public string Metric { get; set; }

        [DataMember]
        [Required]
        public int Metric_Code { get; set; }

        [DataMember]
        //[Required]
        public string Metric_Description { get; set; }

        [DataMember]
       // [Required]
        public string MisCode { get; set; }

        [DataMember]
       // [Required]
        public int Metric_Score_determinant { get; set; }

        [DataMember]
        [Required]
        public int Period { get; set; }

        [DataMember]
        [Required]
        public int Year { get; set; }

        [DataMember]
       // [Required]
        public int PerspectiveId { get; set; }

        [DataMember]
        //[Required]
        public int Metric_Position { get; set; }

        [DataMember]
       // [Required]
        public int Mapping_Code { get; set; }

        public int EntityId
        {
            get
            {
                return MetricId;
            }
        }
    }
}
