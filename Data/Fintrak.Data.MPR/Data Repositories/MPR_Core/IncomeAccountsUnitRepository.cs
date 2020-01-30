using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeAccountsUnitRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeAccountsUnitRepository : DataRepositoryBase<IncomeAccountsUnit>, IIncomeAccountsUnitRepository
    {

        protected override IncomeAccountsUnit AddEntity(MPRContext entityContext, IncomeAccountsUnit entity)
        {
            return entityContext.Set<IncomeAccountsUnit>().Add(entity);
        }

        protected override IncomeAccountsUnit UpdateEntity(MPRContext entityContext, IncomeAccountsUnit entity)
        {
            return (from e in entityContext.Set<IncomeAccountsUnit>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeAccountsUnit> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<IncomeAccountsUnit>()
                   select e;
        }

        protected override IncomeAccountsUnit GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeAccountsUnit>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
