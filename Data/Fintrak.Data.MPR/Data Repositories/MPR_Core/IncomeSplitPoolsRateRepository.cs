using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeSplitPoolsRateRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeSplitPoolsRateRepository : DataRepositoryBase<IncomeSplitPoolsRate>, IIncomeSplitPoolsRateRepository
    {

        protected override IncomeSplitPoolsRate AddEntity(MPRContext entityContext, IncomeSplitPoolsRate entity)
        {
            return entityContext.Set<IncomeSplitPoolsRate>().Add(entity);
        }

        protected override IncomeSplitPoolsRate UpdateEntity(MPRContext entityContext, IncomeSplitPoolsRate entity)
        {
            return (from e in entityContext.Set<IncomeSplitPoolsRate>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeSplitPoolsRate> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<IncomeSplitPoolsRate>()
                   select e;
        }

        protected override IncomeSplitPoolsRate GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeSplitPoolsRate>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
