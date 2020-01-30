using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core
{
    [Export(typeof(IIncomeMonthsRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeMonthsRepository : DataRepositoryBase<IncomeMonths>, IIncomeMonthsRepository
    {
        protected override IncomeMonths AddEntity(CoreContext entityContext, IncomeMonths entity)
        {
            return entityContext.Set<IncomeMonths>().Add(entity);
        }

        protected override IncomeMonths UpdateEntity(CoreContext entityContext, IncomeMonths entity)
        {
            return (from e in entityContext.Set<IncomeMonths>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeMonths> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<IncomeMonths>()
                   select e;
        }

        protected override IncomeMonths GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeMonths>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
