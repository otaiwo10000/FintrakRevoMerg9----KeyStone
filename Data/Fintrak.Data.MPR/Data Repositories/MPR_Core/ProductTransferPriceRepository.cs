using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IProductTransferPriceRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProductTransferPriceRepository : DataRepositoryBase<ProductTransferPrice>, IProductTransferPriceRepository
    {

        protected override ProductTransferPrice AddEntity(MPRContext entityContext, ProductTransferPrice entity)
        {
            return entityContext.Set<ProductTransferPrice>().Add(entity);
        }

        protected override ProductTransferPrice UpdateEntity(MPRContext entityContext, ProductTransferPrice entity)
        {
            return (from e in entityContext.Set<ProductTransferPrice>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ProductTransferPrice> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<ProductTransferPrice>()
                    select e).Take(50).OrderBy(x => x.ProductCode);
        }

        protected override ProductTransferPrice GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ProductTransferPrice>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ProductTransferPriceInfo> GetProductTransferPriceBySearchValue(string searchvalue)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = (from a in entityContext.ProductTransferPriceSet
                             join b in entityContext.ProductSet on a.ProductCode equals b.Code

                             select new ProductTransferPriceInfo()
                             {
                                 ID = a.ID,
                                 ProductCode = a.ProductCode,
                                 ProductName = b.Name,
                                 Rating = a.Rating,
                                 Description = a.Description,
                                 Category = a.Category,
                                 BSCategoryName = a.Category.ToString(),
                             }).Where(x => x.ProductCode.StartsWith(searchvalue.Trim()) || x.ProductName.StartsWith(searchvalue.Trim())
                             || x.Rating.StartsWith(searchvalue.Trim()) || x.BSCategoryName.StartsWith(searchvalue.Trim()));                         

                return query.ToFullyLoaded().OrderBy(x => x.ProductCode);
            }
        }



    }
}
