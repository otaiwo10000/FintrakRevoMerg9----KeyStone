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
    public partial class IncomeAccountPoolRate : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public string AcccountNumber { get; set; }

        [DataMember]
        [Required]
        public string Customer_Code { get; set; }

        [DataMember]
        [Required]
        public decimal PoolRate { get; set; }
       
        public int EntityId
        {
            get
            {
                return Id;
            }
        }
    }
}