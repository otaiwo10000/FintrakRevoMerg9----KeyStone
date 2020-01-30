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
    public partial class OffBalanceSheetExposure : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ObeId { get; set; }

        [DataMember]
        [Required]
        public string RefNo { get; set; }

        [DataMember]
       
        public DateTime TRNX_DATE { get; set; }

        [DataMember]
        [Required]
        public DateTime MATURITY_DATE { get; set; }
 
        
        [DataMember]
        public string CUR { get; set; }

        [DataMember]
        public double Amount_FCY { get; set; }
           
              
        [DataMember]
        public decimal Ex_Rate { get; set; }

         [DataMember]
        public double Amount_NGN { get; set; }
        
        [DataMember]
         public int TenorMonths { get; set; }

         [DataMember]
        public string Maturity_profile { get; set; }

         [DataMember]
         public string RATING { get; set; }
        

         [DataMember]
         public string Portfolio { get; set; }

         [DataMember]
         public string SUB_PORTFOLIO { get; set; }

         [DataMember]
         public int Staging { get; set; }

         [DataMember]
         public double EIR { get; set; }

         [DataMember]
         public string Type { get; set; }
         [DataMember]

         public bool CanCrystallize { get; set; }

         [DataMember]       
         public DateTime Rundate { get; set; }

        [DataMember]
        public bool Active { get; set; }
      
        public int EntityId
        {
            get
            {
                return ObeId;
            }
        }
    }
}
