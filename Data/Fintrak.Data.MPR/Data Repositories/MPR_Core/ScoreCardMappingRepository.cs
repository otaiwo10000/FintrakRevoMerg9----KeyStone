using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IScoreCardMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ScoreCardMappingRepository : DataRepositoryBase<ScoreCardMapping>, IScoreCardMappingRepository
    {

        protected override ScoreCardMapping AddEntity(MPRContext entityContext, ScoreCardMapping entity)
        {
            return entityContext.Set<ScoreCardMapping>().Add(entity);
        }

        protected override ScoreCardMapping UpdateEntity(MPRContext entityContext, ScoreCardMapping entity)
        {
            return (from e in entityContext.Set<ScoreCardMapping>()
                    where e.MappingId == entity.MappingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ScoreCardMapping> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<ScoreCardMapping>()
                   select e).Take(50).OrderBy(x => x.MappingId);
        }

        protected override ScoreCardMapping GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ScoreCardMapping>()
                         where e.MappingId == id
                         select e); 

            var results = query.FirstOrDefault();

            return results;
        }


        //public IEnumerable<ScoreCardMapping> GetMappingBySearchValue(string searchvalue)
        //{
        //    using (MPRContext entityContext = new MPRContext())
        //    {
        //        var query = (from a in entityContext.ScoreCardMappingSet
        //                         //where a.TeamName.Contains(SearchValue) || a.Team_Code.Contains(SearchValue)
        //                     where (a.Mapping_code == Convert.ToInt32(searchvalue) || a.Actual_Caption.StartsWith(searchvalue.Trim())
        //                     || a.Budget_Caption.StartsWith(searchvalue.Trim()) || a.Year == Convert.ToInt32(searchvalue)
        //                     || a.Period == Convert.ToInt32(searchvalue) || a.Metric_Code.ToString().StartsWith(searchvalue.Trim()))
        //                     select a)
        //          .OrderBy(x => x.Actual_Caption);

        //        return query.ToFullyLoaded();
        //    }
        //}

        
        public IEnumerable<ScoreCardMappingInfo> GetMappingBySearchValue(string searchvalue)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.ScoreCardMappingSet
                            join b in entityContext.ScoreCardMetricsSet on a.Metric_Code equals b.Metric_Code
                            where (a.Mapping_code == Convert.ToInt32(searchvalue) || a.Actual_Caption.StartsWith(searchvalue.Trim())
                             || a.Budget_Caption.StartsWith(searchvalue.Trim()) || a.Year == Convert.ToInt32(searchvalue)
                             || a.Period == Convert.ToInt32(searchvalue) || a.Metric_Code.ToString().StartsWith(searchvalue.Trim())
                             || b.Metric.StartsWith(searchvalue.Trim()))

                            select new ScoreCardMappingInfo()
                            {
                                MappingId = a.MappingId,
                                Actual_Caption = a.Actual_Caption,
                                Budget_Caption = a.Budget_Caption,
                                Mapping_code = a.Mapping_code,
                                Metric = b.Metric,
                                Period = a.Period,
                                Year = a.Year
                            };

                return query.ToFullyLoaded().OrderBy(x => x.Metric);
            }
        }

        private SetUp GetSetUp()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                SetUp query = (from a in entityContext.SetUpSet
                               select a).FirstOrDefault();

                return query;
            }
        }
       
        //public IEnumerable<ScoreCardMapping> GetMappingBySetUp()
        //{
        //    using (MPRContext entityContext = new MPRContext())
        //    {
        //        var setup = GetSetUp();

        //        var query = (from a in entityContext.ScoreCardMappingSet
        //                     where a.Year.ToString() == setup.Year && a.Period == setup.Period
        //                     select a)
        //                     .OrderBy(x => x.Actual_Caption).Take(500)
        //                    .ToList();

        //        return query;
        //    }
        //}

        public IEnumerable<ScoreCardMappingInfo> GetMappingBySetUp()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var setup = GetSetUp();

                var query = from a in entityContext.ScoreCardMappingSet
                            join b in entityContext.ScoreCardMetricsSet on a.Metric_Code equals b.Metric_Code
                            where a.Year.ToString() == setup.Year && a.Period == setup.Period

                            select new ScoreCardMappingInfo()
                            {
                                MappingId = a.MappingId,
                                Metric_Code = a.Metric_Code,
                                Actual_Caption = a.Actual_Caption,
                                Budget_Caption = a.Budget_Caption,
                                Mapping_code = a.Mapping_code,
                                Metric = b.Metric,
                                Period = a.Period,
                                Year = a.Year
                            };

                return query.ToFullyLoaded().OrderBy(x => x.Metric).Take(500);
            }
        }
    }
}
