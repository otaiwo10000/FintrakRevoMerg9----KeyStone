using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(ISlaryScheduleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SlaryScheduleRepository : DataRepositoryBase<SlarySchedule>, ISlaryScheduleRepository
    {

        protected override SlarySchedule AddEntity(MPRContext entityContext, SlarySchedule entity)
        {
            return entityContext.Set<SlarySchedule>().Add(entity);
        }

        protected override SlarySchedule UpdateEntity(MPRContext entityContext, SlarySchedule entity)
        {
            return (from e in entityContext.Set<SlarySchedule>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<SlarySchedule> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<SlarySchedule>()
                   select e;
        }

        protected override SlarySchedule GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<SlarySchedule>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
