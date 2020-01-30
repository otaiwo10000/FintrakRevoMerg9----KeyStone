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
    public partial class OneBankBranch : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int Id { get; set; }

        [DataMember]
        public string StaffName { get; set; }

        [DataMember]
        public string BRANCH_CODE { get; set; }
        [DataMember]
        public string GradeLevel { get; set; }

        [DataMember]
        public decimal CASATarget { get; set; }

        [DataMember]
        public int Period { get; set; }

        [DataMember]
        public int Year { get; set; }

        public int EntityId
        {
            get
            {
                return Id;
            }
        }
    }
}
