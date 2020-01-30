using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IScoreCardMISMappingKBLRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ScoreCardMISMappingKBLRepository : DataRepositoryBase<ScoreCardMISMappingKBL>, IScoreCardMISMappingKBLRepository
    {

        protected override ScoreCardMISMappingKBL AddEntity(MPRContext entityContext, ScoreCardMISMappingKBL entity)
        {
            return entityContext.Set<ScoreCardMISMappingKBL>().Add(entity);
        }

        protected override ScoreCardMISMappingKBL UpdateEntity(MPRContext entityContext, ScoreCardMISMappingKBL entity)
        {
            return (from e in entityContext.Set<ScoreCardMISMappingKBL>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ScoreCardMISMappingKBL> GetEntities(MPRContext entityContext)
        {
            int currentyear = Convert.ToInt32(entityContext.IncomeSetupSet.Select(x => x.Year).FirstOrDefault());
            return (from e in entityContext.Set<ScoreCardMISMappingKBL>()
                   select e).Where(x => x.Year == currentyear).OrderBy(x => x.KPI_TYPE);
        }

        protected override ScoreCardMISMappingKBL GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ScoreCardMISMappingKBL>()
                         where e.ID == id
                         select e); 

            var results = query.FirstOrDefault();

            return results;
        }



        public IEnumerable<ScoreCardMISMappingKBL> GetScoreCardMISMappingKBLBySearchValue(string searchvalue)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                int currentyear = Convert.ToInt32(entityContext.IncomeSetupSet.Select(x => x.Year).FirstOrDefault());
                var query = (from a in entityContext.ScoreCardMISMappingKBLSet
                                 //where a.TeamName.Contains(SearchValue) || a.Team_Code.Contains(SearchValue)
                             where (a.MIS.Contains(searchvalue.Trim()) || a.KPI_TYPE.Contains(searchvalue.Trim())) && a.Year==currentyear
                             select a)
                  .OrderBy(x => x.KPI_TYPE);

                return query.ToFullyLoaded();
            }
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


        //public IEnumerable<ScoreCardMappingInfo> GetMappingBySearchValue(string searchvalue)
        //{
        //    using (MPRContext entityContext = new MPRContext())
        //    {
        //        var query = from a in entityContext.ScoreCardMappingSet
        //                    join b in entityContext.ScoreCardMetricsSet on a.Metric_Code equals b.Metric_Code
        //                    where (a.Mapping_code == Convert.ToInt32(searchvalue) || a.Actual_Caption.StartsWith(searchvalue.Trim())
        //                     || a.Budget_Caption.StartsWith(searchvalue.Trim()) || a.Year == Convert.ToInt32(searchvalue)
        //                     || a.Period == Convert.ToInt32(searchvalue) || a.Metric_Code.ToString().StartsWith(searchvalue.Trim())
        //                     || b.Metric.StartsWith(searchvalue.Trim()))

        //                    select new ScoreCardMappingInfo()
        //                    {
        //                        MappingId = a.MappingId,
        //                        Actual_Caption = a.Actual_Caption,
        //                        Budget_Caption = a.Budget_Caption,
        //                        Mapping_code = a.Mapping_code,
        //                        Metric = b.Metric,
        //                        Period = a.Period,
        //                        Year = a.Year
        //                    };

        //        return query.ToFullyLoaded().OrderBy(x => x.Metric);
        //    }
        //}



    }
}
