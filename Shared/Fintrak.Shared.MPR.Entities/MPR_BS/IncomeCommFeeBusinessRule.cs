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
    public partial class IncomeCommFeeBusinessRule : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        public string GLCode { get; set; }

        [DataMember]
        [Required]
        public string Bank { get; set; }

        [DataMember]
        [Required]
        public string GL_Description { get; set; }

        [DataMember]
        [Required]
        public string Channel { get; set; }

        [DataMember]
        public string Related_Account { get; set; }

        [DataMember]
        public string Branches { get; set; }

        [DataMember]
        [Required]
        public string Basis_of_Allocation { get; set; }

        [DataMember]
        public string rule { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
