using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IOpexGLMapRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpexGLMapRepository : DataRepositoryBase<OpexGLMap>, IOpexGLMapRepository
    {

        protected override OpexGLMap AddEntity(MPRContext entityContext, OpexGLMap entity)
        {
            return entityContext.Set<OpexGLMap>().Add(entity);
        }

        protected override OpexGLMap UpdateEntity(MPRContext entityContext, OpexGLMap entity)
        {
            return (from e in entityContext.Set<OpexGLMap>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OpexGLMap> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<OpexGLMap>()
                   select e;
        }

        protected override OpexGLMap GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OpexGLMap>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
