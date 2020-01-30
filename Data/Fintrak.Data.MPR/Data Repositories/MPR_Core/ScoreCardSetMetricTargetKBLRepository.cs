using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IScoreCardSetMetricTargetKBLRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ScoreCardSetMetricTargetKBLRepository : DataRepositoryBase<ScoreCardSetMetricTargetKBL>, IScoreCardSetMetricTargetKBLRepository
    {

        protected override ScoreCardSetMetricTargetKBL AddEntity(MPRContext entityContext, ScoreCardSetMetricTargetKBL entity)
        {
            return entityContext.Set<ScoreCardSetMetricTargetKBL>().Add(entity);
        }

        protected override ScoreCardSetMetricTargetKBL UpdateEntity(MPRContext entityContext, ScoreCardSetMetricTargetKBL entity)
        {
            return (from e in entityContext.Set<ScoreCardSetMetricTargetKBL>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ScoreCardSetMetricTargetKBL> GetEntities(MPRContext entityContext)
        {
            //int maxyear = Convert.ToInt32(entityContext.TeamStructureSet.Max(x => x.Year));
            int currentyear = Convert.ToInt32(entityContext.IncomeSetupSet.Select(x=>x.Year).FirstOrDefault());

            return (from e in entityContext.Set<ScoreCardSetMetricTargetKBL>()
                    select e).Where(x=>x.Year== currentyear).Take(500).OrderBy(x => x.SetMetric);
        }

        protected override ScoreCardSetMetricTargetKBL GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ScoreCardSetMetricTargetKBL>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ScoreCardSetMetricTargetKBL> GetScoreCardSetMetricTargetKBLBySearchValue(string searchvalue)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = (from a in entityContext.ScoreCardSetMetricTargetKBLSet
                                 //where a.TeamName.Contains(SearchValue) || a.Team_Code.Contains(SearchValue)
                             where (a.SetMetric.Trim().ToLower().StartsWith(searchvalue.Trim().ToLower()))
                             select a).OrderBy(x => x.SetMetric);

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<ScoreCardSetMetricTargetKBL> GetScoreCardSetMetricTargetKBLByPeriodANDYear(int period, int year)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = (from a in entityContext.ScoreCardSetMetricTargetKBLSet
                                 //where a.TeamName.Contains(SearchValue) || a.Team_Code.Contains(SearchValue)
                             where a.Period == period && a.Year == year
                             select a).OrderBy(x => x.SetMetric);

                return query.ToFullyLoaded();
            }
        }


    }
}
