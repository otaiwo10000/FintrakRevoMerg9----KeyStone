﻿using Fintrak.Shared.Common.Contracts;
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
    public partial class IncomePoolRateSbu : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        public string Levels { get; set; }

        [DataMember]
        [Required]
        public string SBU { get; set; }

        [DataMember]
        [Required]
        public double Rate { get; set; }

        [DataMember]
        [Required]
        public string Category { get; set; }

        [DataMember]
        [Required]
        public string Currency_Type { get; set; }


        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
