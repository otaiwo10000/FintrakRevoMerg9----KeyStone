using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(INRFFCaptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NRFFCaptionRepository : DataRepositoryBase<NRFFCaption>, INRFFCaptionRepository
    {

        protected override NRFFCaption AddEntity(MPRContext entityContext, NRFFCaption entity)
        {
            return entityContext.Set<NRFFCaption>().Add(entity);
        }

        protected override NRFFCaption UpdateEntity(MPRContext entityContext, NRFFCaption entity)
        {
            return (from e in entityContext.Set<NRFFCaption>()
                    where e.NRFFCaption_Id == entity.NRFFCaption_Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<NRFFCaption> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<NRFFCaption>()
                   select e;
        }

        protected override NRFFCaption GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<NRFFCaption>()
                         where e.NRFFCaption_Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
