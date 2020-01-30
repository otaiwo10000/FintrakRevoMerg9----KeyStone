
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
    public partial class account_interest : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public Int32 account_interest_Id { get; set; }

        [DataMember]
        [Required]
        public string AccountNo { get; set; }

        [DataMember]
        [Required]
        public Double InterestRate { get; set; }

        [DataMember]
        [Required]
        public string Productcode { get; set; }

        [DataMember]
        [Required]
        public string Period { get; set; }

        [DataMember]
        [Required]
        public string Year { get; set; }

        [DataMember]
        public string caption { get; set; }

        public int EntityId
        {
            get
            {
                return account_interest_Id;
            }
        }

    }
}
