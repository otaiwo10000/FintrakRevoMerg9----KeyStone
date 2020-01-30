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
    public partial class IncomeCustomerRatingOverride : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int Id { get; set; }

        [DataMember]
        //[Required]
        public string Cust_ID { get; set; }

        [DataMember]
        //[Required]
        public string Ref_No { get; set; }

        [DataMember]
        //[Required]
        public string Settlement_Account { get; set; }

        [DataMember]
        //[Required]
        public string Customer_Name { get; set; }

        [DataMember]
        //[Required]
        public decimal Limit { get; set; }

        [DataMember]
        //[Required]
        public decimal PrincipalOutstandingBal { get; set; }
       
        [DataMember]
        //[Required]
        public DateTime Value_Date { get; set; }

        [DataMember]
        //[Required]
        public DateTime Maturity_Date { get; set; }

        [DataMember]
        //[Required]
        public string Risk_Rating { get; set; }


        public int EntityId
        {
            get
            {
                return Id;
            }
        }

    }
}
