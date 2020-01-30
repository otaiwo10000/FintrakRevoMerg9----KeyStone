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
    public partial class IncomeOtherBreakdown : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ID { get; set; }

        [DataMember]
        public string MIS_Code { get; set; }

        [DataMember]
        public string Caption { get; set; }

        [DataMember]
        public string Accountnumber { get; set; }

        [DataMember]
        public string Narrative { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public int Period { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public string AccountOfficer_Code { get; set; }

        [DataMember]
        public decimal Volume { get; set; }

        [DataMember]
        public string Indicator { get; set; }

        [DataMember]
        public string EntryStatus { get; set; }
        [DataMember]
        public DateTime DateEntered { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public DateTime RunDate { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
