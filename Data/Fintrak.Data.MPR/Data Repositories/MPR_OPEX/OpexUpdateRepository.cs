using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IOpexUpdateRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpexUpdateRepository : DataRepositoryBase<OpexUpdate>, IOpexUpdateRepository
    {

        protected override OpexUpdate AddEntity(MPRContext entityContext, OpexUpdate entity)
        {
            return entityContext.Set<OpexUpdate>().Add(entity);
        }

        protected override OpexUpdate UpdateEntity(MPRContext entityContext, OpexUpdate entity)
        {
            return (from e in entityContext.Set<OpexUpdate>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OpexUpdate> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<OpexUpdate>()
                   select e;
        }

        protected override OpexUpdate GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OpexUpdate>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
