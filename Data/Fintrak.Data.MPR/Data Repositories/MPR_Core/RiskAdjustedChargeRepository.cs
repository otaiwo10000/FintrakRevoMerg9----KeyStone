using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IRiskAdjustedChargeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RiskAdjustedChargeRepository : DataRepositoryBase<RiskAdjustedCharge>, IRiskAdjustedChargeRepository
    {

        protected override RiskAdjustedCharge AddEntity(MPRContext entityContext, RiskAdjustedCharge entity)
        {
            return entityContext.Set<RiskAdjustedCharge>().Add(entity);
        }

        protected override RiskAdjustedCharge UpdateEntity(MPRContext entityContext, RiskAdjustedCharge entity)
        {
            return (from e in entityContext.Set<RiskAdjustedCharge>()
                    where e.RiskAdjustedChargeId== entity.RiskAdjustedChargeId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<RiskAdjustedCharge> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<RiskAdjustedCharge>()
                   select e;
        }

        protected override RiskAdjustedCharge GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<RiskAdjustedCharge>()
                         where e.RiskAdjustedChargeId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
