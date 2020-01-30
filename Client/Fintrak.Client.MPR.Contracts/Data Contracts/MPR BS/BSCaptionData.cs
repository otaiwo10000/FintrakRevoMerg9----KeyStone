using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Contracts
{
    [DataContract]
    public class BSCaptionData : DataContractBase
    {
        [DataMember]
        public int CaptionId { get; set; }

        [DataMember]
        public string CaptionCode { get; set; }

        [DataMember]
        public string CaptionName { get; set; }

        [DataMember]
        public AccountTypeEnum Category { get; set; }

        [DataMember]
        public string CategoryName { get; set; }

        [DataMember]
        public CurrencyType CurrencyType { get; set; }

        [DataMember]
        public string CurrencyTypeName { get; set; }

        [DataMember]
        public BalanceSheetType BalanceSheetType { get; set; }

        [DataMember]
        public string BalanceSheetTypeName { get; set; }

        [DataMember]
        public int Position { get; set; }
      
        [DataMember]
        public ModuleOwnerType ModuleOwnerType { get; set; }

        [DataMember]
        public string ModuleName { get; set; }
        
        [DataMember]
        public string PLCaption { get; set; }

        [DataMember]
        public string PLCaptionName { get; set; }

        [DataMember]
        public int? ParentId { get; set; }

        [DataMember]
        public string ParentName { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

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

        [DataMember]
        public bool Active { get; set; }
    }
}
