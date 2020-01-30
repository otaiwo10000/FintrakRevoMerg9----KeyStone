using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IScoreCardMetricsKBLRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ScoreCardMetricsKBLRepository : DataRepositoryBase<ScoreCardMetricsKBL>, IScoreCardMetricsKBLRepository
    {

        protected override ScoreCardMetricsKBL AddEntity(MPRContext entityContext, ScoreCardMetricsKBL entity)
        {
            return entityContext.Set<ScoreCardMetricsKBL>().Add(entity);
        }

        protected override ScoreCardMetricsKBL UpdateEntity(MPRContext entityContext, ScoreCardMetricsKBL entity)
        {
            return (from e in entityContext.Set<ScoreCardMetricsKBL>()
                    where e.MetricID == entity.MetricID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ScoreCardMetricsKBL> GetEntities(MPRContext entityContext)
        {
            //int maxyear = Convert.ToInt32(entityContext.TeamStructureSet.Max(x => x.Year));
            int currentyear = Convert.ToInt32(entityContext.IncomeSetupSet.Select(x => x.Year).FirstOrDefault());

            return (from e in entityContext.Set<ScoreCardMetricsKBL>()
                        //select e).Where(x=>x.Year== currentyear).Take(500).OrderBy(x => x.Metric);
                    select e).Where(x => x.Year == currentyear).OrderBy(x => x.Metric);
        }

        protected override ScoreCardMetricsKBL GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ScoreCardMetricsKBL>()
                         where e.MetricID == id
                         select e); 

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<ScoreCardMetricsKBL> GetMetricsBySearchValue(string searchvalue)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = (from a in entityContext.ScoreCardMetricsKBLSet
                                 //where a.TeamName.Contains(SearchValue) || a.Team_Code.Contains(SearchValue)
                             where (a.Metric.Trim().ToLower().StartsWith(searchvalue.Trim().ToLower()))
                             select a).OrderBy(x => x.Metric);

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<ScoreCardMetricsKBL> GetMetricsByYear(int year)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = (from a in entityContext.ScoreCardMetricsKBLSet
                                 //where a.TeamName.Contains(SearchValue) || a.Team_Code.Contains(SearchValue)
                             where a.Year == year
                             select a).OrderBy(x => x.Metric);

                return query.ToFullyLoaded();
            }
        }



    }
}
