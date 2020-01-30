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
    public partial class OpexSBUBaseCost : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public string MIS_CODE { get; set; }

        [DataMember]
        public double AMOUNT { get; set; }

        [DataMember]
        public string TEMPLATE { get; set; }

        [DataMember]
        public string SOURCE { get; set; }

        [DataMember]
        public int SN { get; set; }

        [DataMember]
        public string TRANS_TYPE { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }

    }
}
