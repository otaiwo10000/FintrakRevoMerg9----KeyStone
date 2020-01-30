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
    public partial class MPRReportStatus : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int MPRReportStatusId { get; set; }

        [DataMember]
        [Required]
        public int Year { get; set; }

        [DataMember]
        [Required]
        //public int Period { get; set; }
        public string Period { get; set; }

        [DataMember]
        [Required]
        public string Status { get; set; }

        public int EntityId
        {
            get
            {
                return MPRReportStatusId;
            }
        }
    }
}
