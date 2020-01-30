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
    public class BSCaptionDataN : DataContractBase
    {
        [DataMember]
        public int CaptionId { get; set; }

        [DataMember]
        public string CaptionCode { get; set; }        

        [DataMember]
        public string CaptionName { get; set; }

        [DataMember]
        public int Category { get; set; }

        [DataMember]
        public string CategoryName { get; set; }

        [DataMember]
        public int CurrencyType { get; set; }

        [DataMember]
        public string CurrencyTypeName { get; set; }

        [DataMember]
        public int BalanceSheetType { get; set; }

        [DataMember]
        public string BalanceSheetTypeName { get; set; }

        [DataMember]
        public int Position { get; set; }

        [DataMember]
        public string PLCaption { get; set; }

        [DataMember]
        public string PLCaptionName { get; set; }

        [DataMember]
        public string NRFFCaption { get; set; }

        [DataMember]
        public int? ParentId { get; set; }

        [DataMember]
        public int ModuleOwnerType { get; set; }

        [DataMember]
        public string ModuleName { get; set; }
        
        [DataMember]
        public string ParentName { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public bool Active { get; set; }
    }
}