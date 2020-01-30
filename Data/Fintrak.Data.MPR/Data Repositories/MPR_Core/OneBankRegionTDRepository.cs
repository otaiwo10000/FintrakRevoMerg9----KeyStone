using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IOneBankRegionTDRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OneBankRegionTDRepository : DataRepositoryBase<OneBankRegionTD>, IOneBankRegionTDRepository
    {

        protected override OneBankRegionTD AddEntity(MPRContext entityContext, OneBankRegionTD entity)
        {
            return entityContext.Set<OneBankRegionTD>().Add(entity);
        }

        protected override OneBankRegionTD UpdateEntity(MPRContext entityContext, OneBankRegionTD entity)
        {
            return (from e in entityContext.Set<OneBankRegionTD>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OneBankRegionTD> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<OneBankRegionTD>()
                   select e;
        }

        protected override OneBankRegionTD GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OneBankRegionTD>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
