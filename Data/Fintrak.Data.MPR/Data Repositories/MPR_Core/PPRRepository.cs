using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IPPRRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PPRRepository : DataRepositoryBase<PPR>, IPPRRepository
    {

        protected override PPR AddEntity(MPRContext entityContext, PPR entity)
        {
            return entityContext.Set<PPR>().Add(entity);
        }

        protected override PPR UpdateEntity(MPRContext entityContext, PPR entity)
        {
            return (from e in entityContext.Set<PPR>()
                    where e.PPRId == entity.PPRId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<PPR> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<PPR>()
                   select e;
        }

        protected override PPR GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<PPR>()
                         where e.PPRId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
