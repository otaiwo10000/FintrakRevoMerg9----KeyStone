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
    public partial class OpexStaffcostDetail : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public string EMP_NAME { get; set; }

        [DataMember]
        public double AMOUNT { get; set; }

        [DataMember]
        public int PERIOD { get; set; }

        [DataMember]
        public int YEAR { get; set; }

        [DataMember]
        public string TEAM_CODE { get; set; }

        [DataMember]
        public string EMP_ID { get; set; }

        [DataMember]
        public string ACCOUNTOFFICER_CODE { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }

    }
}
