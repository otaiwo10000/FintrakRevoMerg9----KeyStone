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
    public partial class IncomeCommFeeLineCaption : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }


        [DataMember]
        [Required]
        public string GLCode { get; set; }

        [DataMember]
        //[Required]
        public string IncomeLineCapton { get; set; }

        [DataMember]
        //[Required]
        public string Description { get; set; }

        [DataMember]
       // [Required]
        public string GroupCode { get; set; }

        [DataMember]
       // [Required]
        public string GroupName { get; set; }

        [DataMember]
        //[Required]
        public string SubGroupCode { get; set; }

       
        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
