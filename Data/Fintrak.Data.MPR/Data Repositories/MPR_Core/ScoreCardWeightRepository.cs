using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IScoreCardWeightRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ScoreCardWeightRepository : DataRepositoryBase<ScoreCardWeight>, IScoreCardWeightRepository
    {

        protected override ScoreCardWeight AddEntity(MPRContext entityContext, ScoreCardWeight entity)
        {
            return entityContext.Set<ScoreCardWeight>().Add(entity);
        }

        protected override ScoreCardWeight UpdateEntity(MPRContext entityContext, ScoreCardWeight entity)
        {
            return (from e in entityContext.Set<ScoreCardWeight>() 
                    where e.WeightId == entity.WeightId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ScoreCardWeight> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<ScoreCardWeight>()
                   select e).OrderBy(x => x.Weight);
        }

        protected override ScoreCardWeight GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ScoreCardWeight>()
                         where e.WeightId == id
                         select e);

            var results = query.FirstOrDefault() ;

            return results;
        }

        public IEnumerable<ScoreCardWeightInfo> GetScoreCardWeightANDMetrics()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.ScoreCardWeightSet
                            join b in entityContext.ScoreCardMetricsSet on a.Metric_Code equals b.Metric_Code
                            //  from c in agg.DefaultIfEmpty()

                            select new ScoreCardWeightInfo()
                            {
                                //ScoreCardWeight = a,                             
                                //Metric = b.Metric,
                                //Metric_Code = b.Metric_Code

                                WeightId = a.WeightId,
                                //Weight = float.Parse(a.Weight.ToString()),
                                Weight = (decimal)a.Weight,
                                Metric_Code = a.Metric_Code,
                                Metric = b.Metric,
                                Period = a.Period,
                                Year = a.Year,
                            };

                return query.ToFullyLoaded().OrderBy(x => x.Metric);
            }
        }

        //public IEnumerable<BSCaptionInfo> GetBSCaptions()
        //{
        //    using (MPRContext entityContext = new MPRContext())
        //    {
        //        //var query = from a in entityContext.BSCaptionSet
        //        //            join b in entityContext.BSCaptionSet on a.CaptionId equals b.ParentId into parents
        //        //            from pt in parents.DefaultIfEmpty()
        //        //            join c in entityContext.PLCaptionSet on a.PLCaption equals c.Code into cparents
        //        //            from pc in cparents.DefaultIfEmpty()
        //        //            select new BSCaptionInfo()
        //        //            {
        //        //                BSCaption = a,
        //        //                Parent = pt,
        //        //                PLCaption = pc
        //        //            };

        //        //return query.ToFullyLoaded();



        //        var query = from a in entityContext.BSCaptionSet
        //                        //join b in entityContext.PLCaptionSet on a.PLCaption equals b.Code

        //                    select new BSCaptionInfo()
        //                    {
        //                        BSCaption = a
        //                        //PLCaption = b
        //                    };

        //        return query.ToFullyLoaded();
        //    }
        //}

    }
}
