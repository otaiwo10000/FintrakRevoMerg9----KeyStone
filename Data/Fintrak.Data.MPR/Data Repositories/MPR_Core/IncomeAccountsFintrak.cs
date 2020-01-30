using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeAccountsFintrakRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeAccountsFintrakRepository : DataRepositoryBase<IncomeAccountsFintrak>, IIncomeAccountsFintrakRepository
    {

        protected override IncomeAccountsFintrak AddEntity(MPRContext entityContext, IncomeAccountsFintrak entity)
        {
            return entityContext.Set<IncomeAccountsFintrak>().Add(entity);
        }

        protected override IncomeAccountsFintrak UpdateEntity(MPRContext entityContext, IncomeAccountsFintrak entity)
        {
            return (from e in entityContext.Set<IncomeAccountsFintrak>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeAccountsFintrak> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<IncomeAccountsFintrak>()
                   select e;
        }

        protected override IncomeAccountsFintrak GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeAccountsFintrak>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
