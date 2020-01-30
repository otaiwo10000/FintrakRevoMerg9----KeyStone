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
    public partial class IncomeSetup : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ID { get; set; }

        [DataMember]
        //[Required]
        public decimal NDIC { get; set; }

        [DataMember]
        //[Required]
        public decimal CRR { get; set; }  //cash reserve

        [DataMember]
        //[Required]
        public int CurrentPeriod { get; set; }

        [DataMember]
        //[Required]
        public int Year { get; set; }

        [DataMember]
        //[Required]
        public string PDLProductCode { get; set; }

        [DataMember]
        //[Required]
        public int FinYearCount { get; set; }

        [DataMember]
        //[Required]
        public decimal GLLP { get; set; }

        [DataMember]
        //[Required]
        public DateTime StartDate { get; set; }

        [DataMember]
        //[Required]
        public DateTime EndDate { get; set; }

        [DataMember]
        //[Required]
        public double TaxRate { get; set; }

        [DataMember]
        //[Required]
        public string Runmode { get; set; }

        [DataMember]
        //[Required]
        public string ReportMode { get; set; }

        [DataMember]
        //[Required]
        public string ExcoMIS { get; set; }

        [DataMember]
        //[Required]
        public string HRMIS { get; set; }

        [DataMember]
        //[Required]
        public decimal ManagingSharePerc { get; set; }

        [DataMember]
        //[Required]
        public decimal BrokerSharePerc { get; set; }

        [DataMember]
        //[Required]
        public string SFU { get; set; }

        //[DataMember]
        ////[Required]
        //public string ExcoAccountOfficer { get; set; }

        //[DataMember]
        ////[Required]
        //public string ProPrietryMIS { get; set; }

        //[DataMember]
        ////[Required]
        //public string Othermis { get; set; }

        //[DataMember]
        ////[Required]
        //public int accountnumberdigit { get; set; }
        //[DataMember]
        ////[Required]
        //public string localcurrcode { get; set; }

        //[DataMember]
        ////[Required]
        //public decimal LMP { get; set; }

        //[DataMember]
        ////[Required]
        //public decimal CRRInt { get; set; }

        //[DataMember]
        ////[Required]
        //public decimal LMPInt { get; set; }

        [DataMember]
        [Required]
        public Double Vault_Cash { get; set; }  //vault cash rate

        //[DataMember]
        ////[Required]
        //public string Title { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
