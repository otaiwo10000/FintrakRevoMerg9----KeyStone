using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(ICorporateAdjustmentRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CorporateAdjustmentRepository : DataRepositoryBase<CorporateAdjustment>, ICorporateAdjustmentRepository
    {

        protected override CorporateAdjustment AddEntity(MPRContext entityContext, CorporateAdjustment entity)
        {
            return entityContext.Set<CorporateAdjustment>().Add(entity);
        }

        protected override CorporateAdjustment UpdateEntity(MPRContext entityContext, CorporateAdjustment entity)
        {
            return (from e in entityContext.Set<CorporateAdjustment>()
                    where e.CorporateAdjustmentId== entity.CorporateAdjustmentId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CorporateAdjustment> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<CorporateAdjustment>()
                   select e;
        }

        protected override CorporateAdjustment GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CorporateAdjustment>()
                         where e.CorporateAdjustmentId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
