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
    public partial class IncomeFintrakAccountsSegment : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public string SegmentCode { get; set; }

        [DataMember]
        [Required]
        public string AccoutofficerCode { get; set; }

        [DataMember]
        [Required]
        public string CUSTOMERID { get; set; }

        [DataMember]
        [Required]
        public string ACCOUNTNUMBER { get; set; }

        [DataMember]
        [Required]
        public string CUSTOMERNAME { get; set; }

        [DataMember]
        [Required]
        public string TEAMNAME { get; set; }

        [DataMember]
        [Required]
        public string Bank { get; set; }

        [DataMember]
        [Required]
        public string Description { get; set; }

        public int EntityId
        {
            get
            {
                return Id;
            }
        }
    }
}
