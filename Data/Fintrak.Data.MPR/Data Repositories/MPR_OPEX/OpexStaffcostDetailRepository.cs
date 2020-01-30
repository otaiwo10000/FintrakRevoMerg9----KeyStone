using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IOpexStaffcostDetailRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OpexStaffcostDetailRepository : DataRepositoryBase<OpexStaffcostDetail>, IOpexStaffcostDetailRepository
    {

        protected override OpexStaffcostDetail AddEntity(MPRContext entityContext, OpexStaffcostDetail entity)
        {
            return entityContext.Set<OpexStaffcostDetail>().Add(entity);
        }

        protected override OpexStaffcostDetail UpdateEntity(MPRContext entityContext, OpexStaffcostDetail entity)
        {
            return (from e in entityContext.Set<OpexStaffcostDetail>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OpexStaffcostDetail> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<OpexStaffcostDetail>()
                   select e;
        }

        protected override OpexStaffcostDetail GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OpexStaffcostDetail>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
