using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeProductsTableRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeProductsTableRepository : DataRepositoryBase<IncomeProductsTable>, IIncomeProductsTableRepository
    {

        protected override IncomeProductsTable AddEntity(MPRContext entityContext, IncomeProductsTable entity)
        {
            return entityContext.Set<IncomeProductsTable>().Add(entity);
        }

        protected override IncomeProductsTable UpdateEntity(MPRContext entityContext, IncomeProductsTable entity)
        {
            return (from e in entityContext.Set<IncomeProductsTable>()
                    where e.ProductID == entity.ProductID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeProductsTable> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<IncomeProductsTable>()
                   select e).Take(1000).OrderBy(x => x.ProductID);
        }

        protected override IncomeProductsTable GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeProductsTable>()
                         where e.ProductID == id
                         select e); 

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IncomeProductsTable> GetIncomeProductBySearchValue(string searchvalue)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.IncomeProductsTableSet
                            where a.ProductCode.StartsWith(searchvalue.Trim()) || a.ProductName.StartsWith(searchvalue.Trim()) 
                            || a.Caption.StartsWith(searchvalue.Trim()) || a.Category.StartsWith(searchvalue.Trim())
                            || a.PL_Caption.StartsWith(searchvalue.Trim()) || a.PPR_Caption.StartsWith(searchvalue.Trim())

                            select a;
                           
                return query.ToFullyLoaded().Take(1000).OrderBy(x => x.ProductName);
            }
        }

       
    }
}
