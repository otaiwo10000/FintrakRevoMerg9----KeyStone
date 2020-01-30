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
    public partial class FinstatMapping : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int FinstatMappingId { get; set; }

        [DataMember]
        [Required]
        public string GLSH { get; set; }

        [DataMember]
        [Required]
        public string Description { get; set; }

        [DataMember]
        [Required]
        public string MainCaption { get; set; }

        [DataMember]
        [Required]
        public string SubCaption { get; set; }

        [DataMember]
        [Required]
        public string SubSubCaption { get; set; }

        [DataMember]
        [Required]
        public int Class { get; set; }

        [DataMember]
        [Required]
        public string RefNote { get; set; }

        [DataMember]
        [Required]
        public int Position { get; set; }

        [DataMember]
        [Required]
        public string PARENTGL { get; set; }

        [DataMember]
        [Required]
        public int SubPosition { get; set; }

        public int EntityId
        {
            get
            {
                return FinstatMappingId;
            }
        }
    }
}
