using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IScoreCardRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ScoreCardRepository : DataRepositoryBase<ScoreCard>, IScoreCardRepository
    {

        protected override ScoreCard AddEntity(MPRContext entityContext, ScoreCard entity)
        {
            return entityContext.Set<ScoreCard>().Add(entity);
        }

        protected override ScoreCard UpdateEntity(MPRContext entityContext, ScoreCard entity)
        {
            return (from e in entityContext.Set<ScoreCard>() 
                    where e.mpr_scorecard_stgId == entity.mpr_scorecard_stgId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ScoreCard> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<ScoreCard>()
                   select e).OrderBy(x => x.mpr_scorecard_stgId);
        }

        protected override ScoreCard GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ScoreCard>()
                         where e.mpr_scorecard_stgId == id
                         select e);

            var results = query.FirstOrDefault() ;

            return results;
        }

        public IEnumerable<ScoreCard> GetScoreCardCaptions()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = (from a in entityContext.ScoreCardSet                              
                             select new
                             {
                                 Caption = a.Caption
                             })
                             .AsEnumerable().Select(x => new ScoreCard
                             {
                                 Caption = x.Caption
                             })
                             //.GroupBy(x => new { Team_Code, x.MainCaption, x.Currency }).Select(o => o.FirstOrDefault());
                             //.GroupBy(x => x.Team_Code).Select(o => o.FirstOrDefault())
                             .GroupBy(x => x.Caption).Select(o => o.FirstOrDefault())
                             //.Distinct()
                             .OrderBy(x => x.Caption)
                            .ToList();

                return query;
            }
        }
        //public IEnumerable<ScoreCardWeightInfo> GetScoreCardWeightANDMetrics()
        //{
        //    using (MPRContext entityContext = new MPRContext())
        //    {
        //        var query = from a in entityContext.ScoreCardWeightSet

        //                    select new ScoreCardWeightInfo()
        //                    {
        //                        //ScoreCardWeight = a,                             
        //                        //Metric = b.Metric,
        //                        //Metric_Code = b.Metric_Code

        //                        WeightId = a.WeightId,
        //                        //Weight = float.Parse(a.Weight.ToString()),
        //                        Weight = (decimal)a.Weight,
        //                        Metric_Code = a.Metric_Code,
        //                        Metric = b.Metric,
        //                        Period = a.Period,
        //                        Year = a.Year,
        //                    };

        //        return query.ToFullyLoaded().OrderBy(x => x.Metric);
        //    }
        //}

    }
}
