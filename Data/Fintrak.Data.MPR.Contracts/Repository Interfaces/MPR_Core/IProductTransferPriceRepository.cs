using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.MPR.Contracts
{
    public interface IProductTransferPriceRepository : IDataRepository<ProductTransferPrice>
    {
        //IEnumerable<ScoreCardMetrics> GetMetricsBySearchValue(string searchvalue);
        //IEnumerable<ScoreCardMetrics> GetMetricsBySetUp();

        IEnumerable<ProductTransferPriceInfo> GetProductTransferPriceBySearchValue(string searchvalue);

    }
}
