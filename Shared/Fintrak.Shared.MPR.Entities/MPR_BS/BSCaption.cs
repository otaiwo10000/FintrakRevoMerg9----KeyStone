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
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Shared.MPR.Entities
{
    public partial class BSCaption : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int CaptionId { get; set; }

        [DataMember]
        [Required]
        public string CaptionCode { get; set; }

        [DataMember]
        [Required]
        public string CaptionName { get; set; }

        [DataMember]
        [Required]
        public AccountTypeEnum Category { get; set; }

        [DataMember]
        [Required]
        public CurrencyType CurrencyType { get; set; }

        [DataMember]
        [Required]
        public BalanceSheetType BalanceSheetType { get; set; }

        [DataMember]
        public int Position { get; set; }

        [DataMember]
        public int? ParentId { get; set; }

        [DataMember]
        public ModuleOwnerType ModuleOwnerType { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }
        
        [DataMember]
        public string PLCaption { get; set; }

        [DataMember]
        public string NRFFCaption { get; set; }

        [DataMember]
        public string APRClassification { get; set; }

        [DataMember]
        public string deposit_class { get; set; }


        [DataMember]
        public string GroupCaption { get; set; }

        [DataMember]
        public string Summary1 { get; set; }
        [DataMember]
        public string Summary2 { get; set; }
        [DataMember]
        public string Summary3 { get; set; }
        [DataMember]
        public string Summary4 { get; set; }
        [DataMember]
        public string Summary5 { get; set; }
        [DataMember]
        public string Summary6 { get; set; }

        public int EntityId
        {
            get
            {
                return CaptionId;
            }
        }

    }
}
