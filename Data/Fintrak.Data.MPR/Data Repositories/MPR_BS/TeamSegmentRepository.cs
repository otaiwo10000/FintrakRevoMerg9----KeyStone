using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(ITeamSegmentRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TeamSegmentRepository : DataRepositoryBase<TeamSegment>, ITeamSegmentRepository
    {

        protected override TeamSegment AddEntity(MPRContext entityContext, TeamSegment entity)
        {
            return entityContext.Set<TeamSegment>().Add(entity);
        }

        protected override TeamSegment UpdateEntity(MPRContext entityContext, TeamSegment entity)
        {
            return (from e in entityContext.Set<TeamSegment>()
                    where e.Mpr_Team_Segment_ID == entity.Mpr_Team_Segment_ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TeamSegment> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<TeamSegment>()
                   select e).Take(500);
        }

        protected override TeamSegment GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TeamSegment>()
                         where e.Mpr_Team_Segment_ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<TeamSegment> GetTeamSegmentbysearch(string search)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = (from a in entityContext.TeamSegmentSet
                             where a.TargetCode.StartsWith(search.Trim()) || a.TargetSegment.StartsWith(search.Trim())
                             || a.CustomerTypeCode.StartsWith(search.Trim()) || a.CustomerType.StartsWith(search.Trim())
                             || a.CustomerSegmentCode.StartsWith(search.Trim()) || a.CustomerSegment.StartsWith(search.Trim())
                             select a);
      
                return query.ToFullyLoaded().OrderBy(x => x.CustomerSegment);
            }
        }

    }
}
