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
    public partial class FTPRiskRatings : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        public string Ratings { get; set; }

        [DataMember]
        [Required]
        public string Currency { get; set; }

        [DataMember]
        [Required]
        public string Category { get; set; }

        [DataMember]
        [Required]
        public string Caption { get; set; }

        [DataMember]
        [Required]
        public string Levels { get; set; }

        [DataMember]
        [Required]
        public string LevelCode { get; set; }

        [DataMember]
        [Required]
        public decimal JAN { get; set; }

        [DataMember]
        [Required]
        public decimal Feb { get; set; }

        [DataMember]
        [Required]
        public decimal Mar { get; set; }

        [DataMember]
        [Required]
        public decimal Apr { get; set; }

        [DataMember]
        [Required]
        public decimal May { get; set; }

        [DataMember]
        [Required]
        public decimal Jun { get; set; }

        [DataMember]
        [Required]
        public decimal Jul { get; set; }

        [DataMember]
        [Required]
        public decimal Aug { get; set; }

        [DataMember]
        [Required]
        public decimal Sep { get; set; }

        [DataMember]
        [Required]
        public decimal Oct { get; set; }

        [DataMember]
        [Required]
        public decimal Nov { get; set; }

        [DataMember]
        [Required]
        public decimal Dec { get; set; }

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
