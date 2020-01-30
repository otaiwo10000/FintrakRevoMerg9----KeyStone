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
    public partial class MISTransferPrice : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int mistransferpriceId { get; set; }

        [DataMember]
        [Required]
        public string DefinitionCode { get; set; }

        [DataMember]
        [Required]
        public string MisCode { get; set; }

        [DataMember]
        [Required]
        public Fintrak.Shared.MPR.Framework.BalanceSheetCategory BalanceSheetCategory { get; set; }

        //[DataMember]
        //[Required]
        //public int Currency { get; set; }

        [DataMember]
        [Required]
        public Fintrak.Shared.MPR.Framework.CurrencyType CurrencyType { get; set; }

        [DataMember]
       // [Required]
        public double Rate { get; set; }

        [DataMember]
        [Required]
        public int Period { get; set; }

        [DataMember]
        [Required]
        public int Year { get; set; }

        [DataMember]
       // [Required]
        public int SolutionId { get; set; }

        [DataMember]
        //[Required]
        public string CompanyCode { get; set; }

      
        public int EntityId
        {
            get
            {
                return mistransferpriceId;
            }
        }
    }
}
