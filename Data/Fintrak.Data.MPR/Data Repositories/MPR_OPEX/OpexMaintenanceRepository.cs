using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IOpexMaintenanceRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpexMaintenanceRepository : DataRepositoryBase<OpexMaintenance>, IOpexMaintenanceRepository
    {

        protected override OpexMaintenance AddEntity(MPRContext entityContext, OpexMaintenance entity)
        {
            return entityContext.Set<OpexMaintenance>().Add(entity);
        }

        protected override OpexMaintenance UpdateEntity(MPRContext entityContext, OpexMaintenance entity)
        {
            return (from e in entityContext.Set<OpexMaintenance>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OpexMaintenance> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<OpexMaintenance>()
                   select e;
        }

        protected override OpexMaintenance GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OpexMaintenance>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
