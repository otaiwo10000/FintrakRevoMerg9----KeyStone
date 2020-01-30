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
    public partial class ScoreCard : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int mpr_scorecard_stgId { get; set; }

        [DataMember]
        //[Required]
        public string Caption { get; set; }

        [DataMember]
        //[Required]
        public string CaptionCode { get; set; }

        [DataMember]
        //[Required]
        public string Accountofficercode { get; set; }

        [DataMember]
       // [Required]
        public string TeamCode { get; set; }

        [DataMember]
       // [Required]
        public string Branchcode { get; set; }

        [DataMember]
       // [Required]
        public decimal Actual { get; set; }
        [DataMember]
        // [Required]
        public decimal AverageBal { get; set; }

        [DataMember]
        // [Required]
        public decimal Amount { get; set; }
        [DataMember]
        // [Required]
        public decimal Budget { get; set; }

        [DataMember]
        // [Required]
        public string Type { get; set; }
        [DataMember]
        // [Required]
        public int Period { get; set; }
        [DataMember]
        // [Required]
        public int Year { get; set; }

        [DataMember]
        // [Required]
        public DateTime Rundate { get; set; }

        public int EntityId
        {
            get
            {
                return mpr_scorecard_stgId;
            }
        }
    }
}
