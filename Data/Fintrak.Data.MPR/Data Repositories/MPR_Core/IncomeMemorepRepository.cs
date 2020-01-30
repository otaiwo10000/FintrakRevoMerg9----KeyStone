using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeMemorepRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeMemorepRepository : DataRepositoryBase<IncomeMemorep>, IIncomeMemorepRepository
    {

        protected override IncomeMemorep AddEntity(MPRContext entityContext, IncomeMemorep entity)
        {
            return entityContext.Set<IncomeMemorep>().Add(entity);
        }

        protected override IncomeMemorep UpdateEntity(MPRContext entityContext, IncomeMemorep entity)
        {
            return (from e in entityContext.Set<IncomeMemorep>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeMemorep> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<IncomeMemorep>()
                   select e;
        }

        protected override IncomeMemorep GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeMemorep>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
