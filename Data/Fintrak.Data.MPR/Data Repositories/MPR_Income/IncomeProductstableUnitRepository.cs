using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeProductstableUnitRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeProductstableUnitRepository : DataRepositoryBase<IncomeProductstableUnit>, IIncomeProductstableUnitRepository
    {

        protected override IncomeProductstableUnit AddEntity(MPRContext entityContext, IncomeProductstableUnit entity)
        {
            return entityContext.Set<IncomeProductstableUnit>().Add(entity);
        }

        protected override IncomeProductstableUnit UpdateEntity(MPRContext entityContext, IncomeProductstableUnit entity)
        {
            return (from e in entityContext.Set<IncomeProductstableUnit>()
                    where e.ID == entity.EntityId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeProductstableUnit> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<IncomeProductstableUnit>()
                   select e).OrderBy(x => x.ProductName);
        }

        protected override IncomeProductstableUnit GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeProductstableUnit>()
                         where e.ID == id
                         select e); 

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IncomeProductstableUnit> GetIncomeProductUnitBySearchValue(string searchvalue)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.IncomeProductstableUnitSet
                            //where a.Product.TrimStart(searchvalue)
                            where a.Product.StartsWith(searchvalue.Trim()) || a.ProductName.StartsWith(searchvalue.Trim()) || a.Unit.StartsWith(searchvalue.Trim())
                            select a;
                
                return query.ToFullyLoaded().Take(1000).OrderBy(x => x.ProductName);
            }
        }

    }
}
