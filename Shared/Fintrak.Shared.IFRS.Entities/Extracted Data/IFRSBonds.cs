using Fintrak.Shared.IFRS.Framework;
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

namespace Fintrak.Shared.IFRS.Entities
{
    public partial class IFRSBonds : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int BondId { get; set; }

        [DataMember]
        [Required]
        public string RefNo { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        [Required]
        public DateTime ValueDate { get; set; }

        [DataMember]
        [Required]
        public DateTime MaturityDate { get; set; }
        
        [DataMember]
        [Required]
        public DateTime FirstCouponDate { get; set; }
        
        [DataMember]
        public double FaceValue { get; set; }

        [DataMember]
        public double CleanPrice { get; set; }
           
              
        [DataMember]
        public decimal CouponRate { get; set; }

         [DataMember]
        public decimal CurrentMarketYield { get; set; }
        
        [DataMember]
        public string Classification { get; set; }
        
        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public string SandP_Rating { get; set; }
        
        [DataMember]
        public double Price { get; set; }

        [DataMember]
        public bool Split { get; set; }

        [DataMember]
        public string Classification_Category { get; set; }
        

         [DataMember]
        public string Narration { get; set; }
        
        [DataMember]
        public bool Active { get; set; }


        [DataMember]
        public string Symbol { get; set; }
      
        public int EntityId
        {
            get
            {
                return BondId;
            }
        }
    }
}
