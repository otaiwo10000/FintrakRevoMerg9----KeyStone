using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.IFRS.Contracts
{
    [DataContract]
    public class LoanPryData : DataContractBase
    {

        [DataMember]
        public int PryId { get; set; }

        [DataMember]
        public string RefNo { get; set; }

        [DataMember]
         public string AccountNo { get; set; }
        [DataMember]
        public string ProductCategory { get; set; }

        [DataMember]
       
        public string ProductCode { get; set; }

        [DataMember]
        
        public string ProductName { get; set; }

        [DataMember]
        public string ProductType { get; set; }

        [DataMember]
      
        public DateTime ValueDate { get; set; }

        [DataMember]
        public string Sector { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
      
        public DateTime MaturityDate { get; set; }

        [DataMember]
      
        public DateTime FirstRepaymentdate { get; set; }

        [DataMember]

        public DateTime InterestFirstRepayDate { get; set; }
        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        public double PeriodicRepaymentAmount { get; set; }


        [DataMember]
        public decimal ExchangeRate { get; set; }

        [DataMember]
        public decimal Rate { get; set; }

        [DataMember]
        public int Tenor { get; set; }

        [DataMember]
        public int InterestRepayFreq { get; set; }

        [DataMember]
        public int PrincipalRepayFreq { get; set; }

        [DataMember]
        public string Schedule_Type { get; set; }

        [DataMember]
        public string ScheduleName { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }
        [DataMember]
        public bool Active { get; set; }
    }
}
