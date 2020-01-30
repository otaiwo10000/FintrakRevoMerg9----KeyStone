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
    public partial class OneBankRegionTD : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        public string StaffName { get; set; }

        [DataMember]
        [Required]
        public string Grade_Level { get; set; }

        [DataMember]
        [Required]
        public string Region_Code { get; set; }

        [DataMember]
        [Required]
        public decimal CASA_Target { get; set; }

        [DataMember]
        [Required]
        public int Period { get; set; }

        [DataMember]
        [Required]
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
