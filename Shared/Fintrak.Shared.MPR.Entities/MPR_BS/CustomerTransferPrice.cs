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
    public partial class CustomerTransferPrice : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int customertransferpriceId { get; set; }

        [DataMember]
        [Required]
        public string CustNo { get; set; }


        [DataMember]
        [Required]
        // public Fintrak.Shared.MPR.Framework.BalanceSheetCategory BalanceSheetCategory { get; set; }
        public Fintrak.Shared.MPR.Framework.BalanceSheetCategory Category { get; set; }

        [DataMember]
        [Required]
        public double Rate { get; set; }


        [DataMember]
        [Required]
        public int Year { get; set; }

        [DataMember]
        [Required]
        public int Period { get; set; }

        [DataMember]
        public int SolutionId { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }


        public int EntityId
        {
            get
            {
                return customertransferpriceId;
            }
        }

    }
}
