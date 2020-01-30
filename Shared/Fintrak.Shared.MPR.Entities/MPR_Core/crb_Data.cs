

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
    public partial class crb_Data : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public Int32 crb_Data_Id { get; set; }

        [DataMember]
        [Required]
        public DateTime xDate { get; set; }

        [DataMember]
        [Required]
        public Int32  Count { get; set; }

        [DataMember]
        [Required]
        public Decimal Volume { get; set; }

        [DataMember]
        public string caption { get; set; }

        public int EntityId
        {
            get
            {
                return crb_Data_Id;
            }
        }

    }
}
