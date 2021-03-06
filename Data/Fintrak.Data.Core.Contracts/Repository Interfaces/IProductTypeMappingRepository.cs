
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core.Contracts
{
    public interface IProductTypeMappingRepository : IDataRepository<ProductTypeMapping>
    {
        IEnumerable<ProductTypeMappingInfo> GetProductTypeMappingByProduct(string productCode);
    }
}
