
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IScoreCardPerspectiveRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ScoreCardPerspectiveRepository : DataRepositoryBase<ScoreCardPerspective>, IScoreCardPerspectiveRepository
    {
        protected override ScoreCardPerspective AddEntity(MPRContext entityContext, ScoreCardPerspective entity)
        {
            return entityContext.Set<ScoreCardPerspective>().Add(entity);
        }

        protected override ScoreCardPerspective UpdateEntity(MPRContext entityContext, ScoreCardPerspective entity)
        {
            return (from e in entityContext.Set<ScoreCardPerspective>()
                    where e.PerspectiveId == entity.PerspectiveId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ScoreCardPerspective> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<ScoreCardPerspective>()
                   select e;
        }

        protected override ScoreCardPerspective GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ScoreCardPerspective>()
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

      
    }
}
