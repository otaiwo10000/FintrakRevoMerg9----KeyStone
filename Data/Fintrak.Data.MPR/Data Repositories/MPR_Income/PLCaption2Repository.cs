using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IPLCaption2Repository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PLCaption2Repository : DataRepositoryBase<PLCaption2>, IPLCaption2Repository
    {

        protected override PLCaption2 AddEntity(MPRContext entityContext, PLCaption2 entity)
        {
            return entityContext.Set<PLCaption2>().Add(entity);
        }

        protected override PLCaption2 UpdateEntity(MPRContext entityContext, PLCaption2 entity)
        {
            return (from e in entityContext.Set<PLCaption2>()
                    where e.PL_CaptionId == entity.EntityId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<PLCaption2> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<PLCaption2>()
                   select e).OrderBy(x => x.Name);
        }

        protected override PLCaption2 GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<PLCaption2>()
                         where e.PL_CaptionId == id
                         select e); 

            var results = query.FirstOrDefault();

            return results;
        }
        
       
    }
}
