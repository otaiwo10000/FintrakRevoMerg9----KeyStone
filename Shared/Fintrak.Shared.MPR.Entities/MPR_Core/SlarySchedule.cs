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
    public partial class SlarySchedule : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ID { get; set; }

        [DataMember]
        //[Required]
        public string EmpID { get; set; }

        [DataMember]
        //[Required]
        public string EMP_Name { get; set; }

        [DataMember]
        //[Required]
        public string Emp_Level { get; set; }

        [DataMember]
        //[Required]
        public string MIS_Code { get; set; }

        [DataMember]
        //[Required]
        public decimal Amount { get; set; }

        [DataMember]
        //[Required]
        public string Pay_Code { get; set; }


        [DataMember]
        //[Required]
        public string Location { get; set; }
        [DataMember]
        //[Required]
        public string SBU { get; set; }

        [DataMember]
        //[Required]
        public string Sol { get; set; }

        [DataMember]
        //[Required]
        public decimal AnnualPay { get; set; }

        [DataMember]
        //[Required]
        public string SType { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
