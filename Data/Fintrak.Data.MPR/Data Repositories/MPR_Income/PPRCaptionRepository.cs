using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IPPRCaptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PPRCaptionRepository : DataRepositoryBase<PPRCaption>, IPPRCaptionRepository
    {

        protected override PPRCaption AddEntity(MPRContext entityContext, PPRCaption entity)
        {
            return entityContext.Set<PPRCaption>().Add(entity);
        }

        protected override PPRCaption UpdateEntity(MPRContext entityContext, PPRCaption entity)
        {
            return (from e in entityContext.Set<PPRCaption>()
                    where e.PPR_CaptionId == entity.EntityId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<PPRCaption> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<PPRCaption>()
                   select e).OrderBy(x => x.Name);
        }

        protected override PPRCaption GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<PPRCaption>()
                         where e.PPR_CaptionId == id
                         select e); 

            var results = query.FirstOrDefault();

            return results;
        }
        
       
    }
}
