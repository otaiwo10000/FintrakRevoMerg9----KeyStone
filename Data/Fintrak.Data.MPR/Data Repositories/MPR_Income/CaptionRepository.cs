using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(ICaptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CaptionRepository : DataRepositoryBase<Caption>, ICaptionRepository
    {

        protected override Caption AddEntity(MPRContext entityContext, Caption entity)
        {
            return entityContext.Set<Caption>().Add(entity);
        }

        protected override Caption UpdateEntity(MPRContext entityContext, Caption entity)
        {
            return (from e in entityContext.Set<Caption>()
                    where e.CaptionId == entity.EntityId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Caption> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<Caption>()
                   select e).OrderBy(x => x.Name);
        }

        protected override Caption GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Caption>()
                         where e.CaptionId == id
                         select e); 

            var results = query.FirstOrDefault();

            return results;
        }
        
       
    }
}
