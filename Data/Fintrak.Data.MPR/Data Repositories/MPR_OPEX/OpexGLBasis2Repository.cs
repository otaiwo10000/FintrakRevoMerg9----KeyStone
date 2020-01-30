using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IOpexGLBasis2Repository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpexGLBasis2Repository : DataRepositoryBase<OpexGLBasis2>, IOpexGLBasis2Repository
    {

        protected override OpexGLBasis2 AddEntity(MPRContext entityContext, OpexGLBasis2 entity)
        {
            return entityContext.Set<OpexGLBasis2>().Add(entity);
        }

        protected override OpexGLBasis2 UpdateEntity(MPRContext entityContext, OpexGLBasis2 entity)
        {
            return (from e in entityContext.Set<OpexGLBasis2>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OpexGLBasis2> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<OpexGLBasis2>()
                   select e;
        }

        protected override OpexGLBasis2 GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OpexGLBasis2>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
