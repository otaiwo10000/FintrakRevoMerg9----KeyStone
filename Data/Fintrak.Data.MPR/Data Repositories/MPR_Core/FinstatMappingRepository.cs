using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IFinstatMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FinstatMappingRepository : DataRepositoryBase<FinstatMapping>, IFinstatMappingRepository
    {

        protected override FinstatMapping AddEntity(MPRContext entityContext, FinstatMapping entity)
        {
            return entityContext.Set<FinstatMapping>().Add(entity);
        }

        protected override FinstatMapping UpdateEntity(MPRContext entityContext, FinstatMapping entity)
        {
            return (from e in entityContext.Set<FinstatMapping>()
                    where e.FinstatMappingId == entity.FinstatMappingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<FinstatMapping> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<FinstatMapping>()
                   select e;
        }

        protected override FinstatMapping GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<FinstatMapping>()
                         where e.FinstatMappingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
