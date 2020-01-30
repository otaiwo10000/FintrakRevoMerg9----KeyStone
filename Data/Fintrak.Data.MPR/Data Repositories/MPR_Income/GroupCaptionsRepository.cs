using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IGroupCaptionsRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GroupCaptionsRepository : DataRepositoryBase<GroupCaptions>, IGroupCaptionsRepository
    {

        protected override GroupCaptions AddEntity(MPRContext entityContext, GroupCaptions entity)
        {
            return entityContext.Set<GroupCaptions>().Add(entity);
        }

        protected override GroupCaptions UpdateEntity(MPRContext entityContext, GroupCaptions entity)
        {
            return (from e in entityContext.Set<GroupCaptions>()
                    where e.GroupCaptionID == entity.EntityId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<GroupCaptions> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<GroupCaptions>()
                   select e).OrderBy(x => x.GroupCaption);
        }

        protected override GroupCaptions GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<GroupCaptions>()
                         where e.GroupCaptionID == id
                         select e); 

            var results = query.FirstOrDefault();

            return results;
        }
        
       
    }
}
