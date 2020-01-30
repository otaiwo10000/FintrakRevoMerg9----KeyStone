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
    public partial class DownloadBaseFintrakFinalManual : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        public string AccountNumber { get; set; }

        [DataMember]
        public string customername { get; set; }
        [DataMember]
        public string sbuCode { get; set; }

        [DataMember]
        public string MIS_Code { get; set; }
        [DataMember]
        public string accountofficercode { get; set; }

        [DataMember]
        public string accountofficer { get; set; }
        [DataMember]
        public decimal ActualBalance { get; set; }

        [DataMember]
        public decimal AverageBalance { get; set; }
        [DataMember]
        public decimal RevExp { get; set; }

        [DataMember]
        public decimal interestRate { get; set; }
        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public string Currency_Type { get; set; }

        [DataMember]
        public DateTime postedDate { get; set; }
        [DataMember]
        public int Period { get; set; }

        [DataMember]
        public int Year { get; set; }
        [DataMember]
        public string EntryStatus { get; set; }

        [DataMember]
        public string GL_Sub { get; set; }
        [DataMember]
        public string Refno { get; set; }

        [DataMember]
        public decimal PoolRate { get; set; }

        [DataMember]
        public decimal BankMaxRate { get; set; }
        [DataMember]
        public string CustomerRating { get; set; }


        [DataMember]
        public decimal EffYield { get; set; }

        [DataMember]
        public decimal PenalRate { get; set; }

        [DataMember]
        public decimal PenalCharge { get; set; }
        [DataMember]
        public decimal ExpRev { get; set; }
        [DataMember]
        public string Caption { get; set; }

        [DataMember]
        public string Category_Filter { get; set; }

        [DataMember]
        public string Branch { get; set; }
        [DataMember]
        public decimal Share_Ratio { get; set; }
        [DataMember]
        public string Indicator { get; set; }

        [DataMember]
        public DateTime Entry_Date { get; set; }

        [DataMember]
        public string Currency_Code { get; set; }
       

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
