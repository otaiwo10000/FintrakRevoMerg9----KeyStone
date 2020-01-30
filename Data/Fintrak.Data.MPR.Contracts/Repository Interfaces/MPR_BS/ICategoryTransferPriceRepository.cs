using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.MPR.Contracts
{
    public interface ICategoryTransferPriceRepository : IDataRepository<CategoryTransferPrice>
    {
        IEnumerable<CategoryTransferPriceInfo> GetCategoryTransferPricebySetUp();
        IEnumerable<CategoryTransferPriceInfo> GetCategoryTransferPricebysearch(string search);

    }
}
