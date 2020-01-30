using System;
using System.Linq;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class ProductInterestInfo
    {
        public product_interest product_interest { get; set; }
        public Product Product { get; set; }
       
    }
}