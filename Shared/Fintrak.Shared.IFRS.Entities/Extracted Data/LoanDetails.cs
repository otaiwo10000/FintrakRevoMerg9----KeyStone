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
    public partial class RawLoanDetails : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int LoanDetailId { get; set; }

        [DataMember]
        [Required]
        public string RefNo { get; set; }

        [DataMember]
        [Required]
        public string AccountNo { get; set; }
        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        public double PastDueAmount { get; set; }

        [DataMember]
        public double ODLimit { get; set; }

        [DataMember]
        public double CollateralHaircut { get; set; }

        [DataMember]
        public double CollateralRecoverableAmt { get; set; }

        [DataMember]
        public string Segment { get; set; }

        [DataMember]
        public string CollateralType { get; set; }

        public double PrincipalOutstandingBal { get; set; }

        public double Interest_Receiv_Pay_UnEarn { get; set; }

        [DataMember]
        [Required]
        public string ProductCode { get; set; }

        [DataMember]
        [Required]
        public string ProductName { get; set; }

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
        public DateTime FirstRepaymentdate { get; set; }


        [DataMember]
        [Required]
        public DateTime PrincipalFirstRepaymentDate { get; set; }


        [DataMember]
        public int InterestRepayFreq { get; set; }

        [DataMember]
        public int PrincipalRepayFreq { get; set; }
        [DataMember]
        public int Stage { get; set; }

        [DataMember]
        public decimal ExchangeRate { get; set; }

        [DataMember]
        public decimal Rate { get; set; }

        [DataMember]
        public string Classification { get; set; }

        [DataMember]
        public double CollateralValue { get; set; }

        [DataMember]
        public string SubClassification { get; set; }


        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public string Sector { get; set; }
        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return LoanDetailId;
            }
        }
    }
}
