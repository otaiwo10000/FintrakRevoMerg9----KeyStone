using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IOpexSBUBaseCostRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpexSBUBaseCostRepository : DataRepositoryBase<OpexSBUBaseCost>, IOpexSBUBaseCostRepository
    {

        protected override OpexSBUBaseCost AddEntity(MPRContext entityContext, OpexSBUBaseCost entity)
        {
            return entityContext.Set<OpexSBUBaseCost>().Add(entity);
        }

        protected override OpexSBUBaseCost UpdateEntity(MPRContext entityContext, OpexSBUBaseCost entity)
        {
            return (from e in entityContext.Set<OpexSBUBaseCost>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OpexSBUBaseCost> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<OpexSBUBaseCost>()
                   where e.SOURCE == "TOTAL"
                   select e;
        }

        protected override OpexSBUBaseCost GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OpexSBUBaseCost>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
