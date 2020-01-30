
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IProductInterestRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProductInterestRepository : DataRepositoryBase<product_interest>, IProductInterestRepository
    {
       
        protected override product_interest AddEntity(MPRContext entityContext, product_interest entity)
        {
            return entityContext.Set<product_interest>().Add(entity);
        }

        protected override product_interest UpdateEntity(MPRContext entityContext, product_interest entity)
        {
            return (from e in entityContext.Set<product_interest>()
                    where e.product_interestId == entity.product_interestId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<product_interest> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<product_interest>()
                   select e;
        }

        protected override product_interest GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<product_interest>()
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ProductInterestInfo> GetProductInterests()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.product_interestSet
                            join b in entityContext.ProductSet on a.ProductCode equals b.Code                           
                            select new ProductInterestInfo()
                            {
                                product_interest = a,
                                Product = b
                            };

                return query.ToFullyLoaded();
            }
        }

    }
}
