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
    public partial class CategoryTransferPrice : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int CategoryTransferPriceId { get; set; }

        [DataMember]
        [Required]
        public Fintrak.Shared.MPR.Framework.BalanceSheetCategory BalanceSheetCategory { get; set; }


        [DataMember]
        [Required]
        public int Period { get; set; }

        [DataMember]
        [Required]
        public int Year { get; set; }

        [DataMember]
        [Required]
        public Fintrak.Shared.MPR.Framework.CurrencyType CurrencyType { get; set; }

        [DataMember]
        public decimal Rate { get; set; }

        public int EntityId
        {
            get
            {
                return CategoryTransferPriceId;
            }
        }

    }
}
