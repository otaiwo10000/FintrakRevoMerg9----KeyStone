﻿using Fintrak.Shared.Common.Contracts;
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
    public partial class AcquirerMapping : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int mpr_Acquirer_Mapping_Id { get; set; }

        [DataMember]
        [Required]
        public string AccountNumber { get; set; }


        [DataMember]
        public string IntroducerCode { get; set; }

        
        public int EntityId
        {
            get
            {
                return mpr_Acquirer_Mapping_Id;
            }
        }

    }
}
