using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeOtherBreakdownRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeOtherBreakdownRepository : DataRepositoryBase<IncomeOtherBreakdown>, IIncomeOtherBreakdownRepository
    {
        protected override IncomeOtherBreakdown AddEntity(MPRContext entityContext, IncomeOtherBreakdown entity)
        {
            return entityContext.Set<IncomeOtherBreakdown>().Add(entity);
        }

        protected override IncomeOtherBreakdown UpdateEntity(MPRContext entityContext, IncomeOtherBreakdown entity)
        {
            return (from e in entityContext.Set<IncomeOtherBreakdown>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeOtherBreakdown> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<IncomeOtherBreakdown>()
                   select e;
        }

        protected override IncomeOtherBreakdown GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeOtherBreakdown>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
