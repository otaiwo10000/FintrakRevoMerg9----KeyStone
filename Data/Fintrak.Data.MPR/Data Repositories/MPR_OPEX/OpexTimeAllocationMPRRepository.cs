using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IOpexTimeAllocationMPRRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpexTimeAllocationMPRRepository : DataRepositoryBase<OpexTimeAllocationMPR>, IOpexTimeAllocationMPRRepository
    {

        protected override OpexTimeAllocationMPR AddEntity(MPRContext entityContext, OpexTimeAllocationMPR entity)
        {
            return entityContext.Set<OpexTimeAllocationMPR>().Add(entity);
        }

        protected override OpexTimeAllocationMPR UpdateEntity(MPRContext entityContext, OpexTimeAllocationMPR entity)
        {
            return (from e in entityContext.Set<OpexTimeAllocationMPR>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OpexTimeAllocationMPR> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<OpexTimeAllocationMPR>()
                   select e;
        }

        protected override OpexTimeAllocationMPR GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OpexTimeAllocationMPR>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
