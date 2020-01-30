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
    public partial class IncomeCaptionPoolRate : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        public string Caption { get; set; }

        [DataMember]
        [Required]
        public decimal Pool_rate { get; set; }

        [DataMember]
        [Required]
        public bool ComputeInterest { get; set; }

        [DataMember]
       // [Required]
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
