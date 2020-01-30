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
    public partial class IncomeRetailProductOverride : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public int Customerid { get; set; }


        [DataMember]
        [Required]
        public string Bank { get; set; }

        [DataMember]
        [Required]
        public string Mis_code { get; set; }

        [DataMember]
        //[Required]
        public string AccountOfficer_Code { get; set; }



        public int EntityId
        {
            get
            {
                return Id;
            }
        }

    }
}
