using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeProductsTableTreasuryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeProductsTableTreasuryRepository : DataRepositoryBase<IncomeProductsTableTreasury>, IIncomeProductsTableTreasuryRepository
    {

        protected override IncomeProductsTableTreasury AddEntity(MPRContext entityContext, IncomeProductsTableTreasury entity)
        {
            return entityContext.Set<IncomeProductsTableTreasury>().Add(entity);
        }

        protected override IncomeProductsTableTreasury UpdateEntity(MPRContext entityContext, IncomeProductsTableTreasury entity)
        {
            return (from e in entityContext.Set<IncomeProductsTableTreasury>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeProductsTableTreasury> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<IncomeProductsTableTreasury>()
                   select e).Take(1000).OrderBy(x => x.ID);
        }

        protected override IncomeProductsTableTreasury GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeProductsTableTreasury>()
                         where e.ID == id
                         select e); 

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IncomeProductsTableTreasury> GetIncomeProductsTableTreasuryBySearchValue(string searchvalue)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.IncomeProductsTableTreasurySet
                            where a.GLCode.StartsWith(searchvalue.Trim()) || a.Caption.StartsWith(searchvalue.Trim()) 
                            || a.SBUCode.StartsWith(searchvalue.Trim()) || a.Category.StartsWith(searchvalue.Trim())
                            || a.Type.StartsWith(searchvalue.Trim()) || a.Status.StartsWith(searchvalue.Trim())

                            select a;
                           
                return query.ToFullyLoaded().Take(1000).OrderBy(x => x.Caption);
            }
        }

       
    }
}
