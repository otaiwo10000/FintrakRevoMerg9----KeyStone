using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class IncomeNEAMappingInfo
    {
        public int Id { get; set; }
        public string Category_Code { get; set; }
        public string CATEGORY_DESCRIPTION { get; set; }
        public string Product_Code { get; set; }
        public string Class { get; set; }     
        public string Caption { get; set; }
        public string AssetType { get; set; }
    }
}