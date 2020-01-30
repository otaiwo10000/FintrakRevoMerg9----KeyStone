using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IFTPRiskRatingsRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FTPRiskRatingsRepository : DataRepositoryBase<FTPRiskRatings>, IFTPRiskRatingsRepository
    {

        protected override FTPRiskRatings AddEntity(MPRContext entityContext, FTPRiskRatings entity)
        {
            return entityContext.Set<FTPRiskRatings>().Add(entity);
        }

        protected override FTPRiskRatings UpdateEntity(MPRContext entityContext, FTPRiskRatings entity)
        {
            return (from e in entityContext.Set<FTPRiskRatings>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<FTPRiskRatings> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<FTPRiskRatings>()
                   select e;
        }

        protected override FTPRiskRatings GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<FTPRiskRatings>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<FTPRiskRatings> GetFTPRiskRatingsBySearchValue(string searchvalue)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.FTPRiskRatingsSet
                            where a.Caption.ToLower().Trim().Contains(searchvalue.ToLower().Trim()) || a.Category.ToLower().Trim().Contains(searchvalue.ToLower().Trim()) || 
                            a.LevelCode.ToLower().Trim().Contains(searchvalue.ToLower().Trim()) || a.Levels.ToLower().Trim().Contains(searchvalue.ToLower().Trim())
                            select a;

                return query.ToFullyLoaded().Take(1000).OrderBy(x => x.Caption);
            }
        }
    }
}
