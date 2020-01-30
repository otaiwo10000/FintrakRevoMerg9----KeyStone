using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(ITeamSectorRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TeamSectorRepository : DataRepositoryBase<TeamSector>, ITeamSectorRepository
    {

        protected override TeamSector AddEntity(MPRContext entityContext, TeamSector entity)
        {
            return entityContext.Set<TeamSector>().Add(entity);
        }

        protected override TeamSector UpdateEntity(MPRContext entityContext, TeamSector entity)
        {
            return (from e in entityContext.Set<TeamSector>()
                    where e.Mpr_Team_Sector_ID == entity.Mpr_Team_Sector_ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<TeamSector> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<TeamSector>()
                   select e).Take(500);
        }

        protected override TeamSector GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TeamSector>()
                         where e.Mpr_Team_Sector_ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<TeamSector> GetTeamSectorbysearch(string search)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = (from a in entityContext.TeamSectorSet
                             where a.Level1Code.StartsWith(search.Trim()) || a.Level1Name.StartsWith(search.Trim())
                             || a.Level2Code.StartsWith(search.Trim()) || a.Level2Name.StartsWith(search.Trim())
                             || a.Level3Code.StartsWith(search.Trim()) || a.Level3Name.StartsWith(search.Trim())
                             || a.Level4Code.StartsWith(search.Trim()) || a.Level4Name.StartsWith(search.Trim())
                             select a);
      
                return query.ToFullyLoaded().OrderBy(x => x.Level1Name);
            }
        }

    }
}
