using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IOpexMemounitPlmapRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpexMemounitPlmapRepository : DataRepositoryBase<OpexMemounitPlmap>, IOpexMemounitPlmapRepository
    {

        protected override OpexMemounitPlmap AddEntity(MPRContext entityContext, OpexMemounitPlmap entity)
        {
            return entityContext.Set<OpexMemounitPlmap>().Add(entity);
        }

        protected override OpexMemounitPlmap UpdateEntity(MPRContext entityContext, OpexMemounitPlmap entity)
        {
            return (from e in entityContext.Set<OpexMemounitPlmap>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OpexMemounitPlmap> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<OpexMemounitPlmap>()
                   select e;
        }

        protected override OpexMemounitPlmap GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OpexMemounitPlmap>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
