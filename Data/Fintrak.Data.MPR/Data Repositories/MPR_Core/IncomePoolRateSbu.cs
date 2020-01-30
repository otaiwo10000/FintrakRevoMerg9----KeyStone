using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomePoolRateSbuRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomePoolRateSbuRepository : DataRepositoryBase<IncomePoolRateSbu>, IIncomePoolRateSbuRepository
    {

        protected override IncomePoolRateSbu AddEntity(MPRContext entityContext, IncomePoolRateSbu entity)
        {
            return entityContext.Set<IncomePoolRateSbu>().Add(entity);
        }

        protected override IncomePoolRateSbu UpdateEntity(MPRContext entityContext, IncomePoolRateSbu entity)
        {
            return (from e in entityContext.Set<IncomePoolRateSbu>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomePoolRateSbu> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<IncomePoolRateSbu>()
                   select e;
        }

        protected override IncomePoolRateSbu GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomePoolRateSbu>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
