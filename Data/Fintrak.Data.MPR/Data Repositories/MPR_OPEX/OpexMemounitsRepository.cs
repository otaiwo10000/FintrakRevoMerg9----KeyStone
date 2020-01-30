using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IOpexMemounitsRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpexMemounitsRepository : DataRepositoryBase<OpexMemounits>, IOpexMemounitsRepository
    {

        protected override OpexMemounits AddEntity(MPRContext entityContext, OpexMemounits entity)
        {
            return entityContext.Set<OpexMemounits>().Add(entity);
        }

        protected override OpexMemounits UpdateEntity(MPRContext entityContext, OpexMemounits entity)
        {
            return (from e in entityContext.Set<OpexMemounits>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OpexMemounits> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<OpexMemounits>()
                   select e;
        }

        protected override OpexMemounits GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OpexMemounits>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
