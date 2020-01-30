using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomePoolRateSbuYearRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomePoolRateSbuYearRepository : DataRepositoryBase<IncomePoolRateSbuYear>, IIncomePoolRateSbuYearRepository
    {

        protected override IncomePoolRateSbuYear AddEntity(MPRContext entityContext, IncomePoolRateSbuYear entity)
        {
            return entityContext.Set<IncomePoolRateSbuYear>().Add(entity);
        }

        protected override IncomePoolRateSbuYear UpdateEntity(MPRContext entityContext, IncomePoolRateSbuYear entity)
        {
            return (from e in entityContext.Set<IncomePoolRateSbuYear>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomePoolRateSbuYear> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<IncomePoolRateSbuYear>()
                   select e;
        }

        protected override IncomePoolRateSbuYear GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomePoolRateSbuYear>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
