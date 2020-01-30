using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IScoreCardMetricsRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ScoreCardMetricsRepository : DataRepositoryBase<ScoreCardMetrics>, IScoreCardMetricsRepository
    {

        protected override ScoreCardMetrics AddEntity(MPRContext entityContext, ScoreCardMetrics entity)
        {
            return entityContext.Set<ScoreCardMetrics>().Add(entity);
        }

        protected override ScoreCardMetrics UpdateEntity(MPRContext entityContext, ScoreCardMetrics entity)
        {
            return (from e in entityContext.Set<ScoreCardMetrics>()
                    where e.MetricId == entity.MetricId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ScoreCardMetrics> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<ScoreCardMetrics>()
                   select e).Take(50).OrderBy(x => x.Metric);
        }

        protected override ScoreCardMetrics GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ScoreCardMetrics>()
                         where e.MetricId == id
                         select e); 

            var results = query.FirstOrDefault();

            return results;
        }


        //public IEnumerable<ScoreCardMetrics> GetMetricsBySearchValue(string searchvalue)
        //{
        //    using (MPRContext entityContext = new MPRContext())
        //    {
        //        var query = (from a in entityContext.ScoreCardMetricsSet
        //                         //where a.TeamName.Contains(SearchValue) || a.Team_Code.Contains(SearchValue)
        //                     where (a.Metric.StartsWith(searchvalue.Trim()) || a.Metric_Code.ToString().StartsWith(searchvalue.Trim())
        //                     || a.MisCode.ToString().StartsWith(searchvalue.Trim()) || a.Year.ToString().StartsWith(searchvalue.Trim())
        //                     || a.Metric_Code.ToString().StartsWith(searchvalue.Trim()))                            
        //                     select a).OrderBy(x=>x.Metric);

        //        return query.ToFullyLoaded();
        //    }
        //}

        private SetUp GetSetUp()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                SetUp query = (from a in entityContext.SetUpSet
                               select a).FirstOrDefault();

                return query;
            }
        }
        //public IEnumerable<ScoreCardMetrics> GetMetricsBySetUp()
        //{
        //    using (MPRContext entityContext = new MPRContext())
        //    {
        //        var setup = GetSetUp();

        //        var query = (from a in entityContext.ScoreCardMetricsSet
        //                     where Convert.ToString(a.Year) == setup.Year && a.Period == setup.Period
        //                     select a)
        //                     .OrderBy(x => x.Metric).Take(100)
        //                    .ToList();

        //        return query;
        //    }
        //}

        //public IEnumerable<ScoreCardMetrics> GetMetricsBySetUp_1()
        //{
        //    using (MPRContext entityContext = new MPRContext())
        //    {
        //        var setup = GetSetUp();

        //        var query = 

        //            (from a in entityContext.ScoreCardMetricsSet
        //                     where a.Year.ToString() == setup.Year && a.Period == setup.Period
        //                     select a)
        //                     .OrderBy(x => x.Metric).Take(100)
        //                    .ToList();

        //        return query;
        //    }
        //}
        public IEnumerable<ScoreCardMetricsInfo> GetMetricsBySetUp()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var setup = GetSetUp();

                var query = from a in entityContext.ScoreCardMetricsSet
                            join b in entityContext.ScorecardPerspectiveSet on a.PerspectiveId equals b.PerspectiveId
                            where a.Year.ToString() == setup.Year && a.Period == setup.Period

                            select new ScoreCardMetricsInfo()
                            {
                                MetricId = a.MetricId,
                                Metric_Code = a.Metric_Code,
                                Metric_Description = a.Metric_Description,
                                Metric = a.Metric,
                                MisCode = a.MisCode,
                                Metric_Score_determinant = a.Metric_Score_determinant,
                                Metric_Position = a.Metric_Position,
                                Period = a.Period,
                                Year = a.Year,
                                PerspectiveId = a.PerspectiveId,
                                perspective = b.Perspective,

                            };

                return query.ToFullyLoaded().Take(2000).OrderBy(x => x.Metric);
            }
        }

        public IEnumerable<ScoreCardMetricsInfo> GetMetricsBySearchValue(string searchvalue)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.ScoreCardMetricsSet
                            join b in entityContext.ScorecardPerspectiveSet on a.PerspectiveId equals b.PerspectiveId
                            where (a.Metric.StartsWith(searchvalue.Trim()) || a.Metric_Code.ToString().StartsWith(searchvalue.Trim())
                           || a.MisCode.ToString().StartsWith(searchvalue.Trim()) || a.Year.ToString().StartsWith(searchvalue.Trim())
                            || a.Metric.StartsWith(searchvalue.Trim()) || a.Period.ToString() == searchvalue.Trim()
                            || b.Perspective.StartsWith(searchvalue.Trim())) 

                            select new ScoreCardMetricsInfo()
                            {
                                MetricId = a.MetricId,
                                Metric_Code = a.Metric_Code,
                                Metric_Description = a.Metric_Description,
                                Metric = a.Metric,
                                MisCode = a.MisCode,
                                Metric_Score_determinant = a.Metric_Score_determinant,
                                Metric_Position = a.Metric_Position,
                                Period = a.Period,
                                Year = a.Year,
                                PerspectiveId = a.PerspectiveId,
                                perspective = b.Perspective,

                            };

                return query.ToFullyLoaded().Take(100).OrderBy(x => x.Metric);
            }
        }

        //public IEnumerable<ScoreCardMetricsInfo> GetMetricsByParams(string metric, int miscode, string perspective, int period, int year)
        //{
        //    using (MPRContext entityContext = new MPRContext())
        //    {
        //        var query = from a in entityContext.ScoreCardMetricsSet
        //                    join b in entityContext.ScorecardPerspectiveSet on a.PerspectiveId equals b.PerspectiveId
        //                    // where (a.Metric.StartsWith(searchvalue.Trim()) || a.Metric_Code.ToString().StartsWith(searchvalue.Trim())
        //                    //|| a.MisCode.ToString().StartsWith(searchvalue.Trim()) || a.Year.ToString().StartsWith(searchvalue.Trim())
        //                    // || a.Metric.StartsWith(searchvalue.Trim()) || a.Period.ToString() == searchvalue.Trim()
        //                    // || b.Perspective.StartsWith(searchvalue.Trim()))

        //                    where (
        //                    (string.IsNullOrEmpty(a.Metric) || a.Metric.StartsWith(metric.Trim()))
        //                 && (a.Metric_Code == 0 || a.Metric_Code == miscode)
        //                 && (string.IsNullOrEmpty(b.Perspective) || b.Perspective.StartsWith(perspective.Trim()))
        //                 && (a.Period == 0 || a.Period == period)
        //                 && (a.Year == 0 || a.Year == year) )

        //                    select new ScoreCardMetricsInfo()
        //                    {
        //                        MetricId = a.MetricId,
        //                        Metric_Code = a.Metric_Code,
        //                        Metric_Description = a.Metric_Description,
        //                        Metric = a.Metric,
        //                        MisCode = a.MisCode,
        //                        Metric_Score_determinant = a.Metric_Score_determinant,
        //                        Metric_Position = a.Metric_Position,
        //                        Period = a.Period,
        //                        Year = a.Year,
        //                        PerspectiveId = a.PerspectiveId,
        //                        perspective = b.Perspective,
        //                    };

        //        return query.ToFullyLoaded().Take(100).OrderBy(x => x.Metric);
        //    }
        //}

       
    }
}
