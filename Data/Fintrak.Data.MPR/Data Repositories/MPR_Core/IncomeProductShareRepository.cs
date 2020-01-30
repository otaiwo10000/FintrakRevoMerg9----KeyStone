using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeProductShareRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeProductShareRepository : DataRepositoryBase<IncomeProductShare>, IIncomeProductShareRepository
    {

        protected override IncomeProductShare AddEntity(MPRContext entityContext, IncomeProductShare entity)
        {
            return entityContext.Set<IncomeProductShare>().Add(entity);
        }

        protected override IncomeProductShare UpdateEntity(MPRContext entityContext, IncomeProductShare entity)
        {
            return (from e in entityContext.Set<IncomeProductShare>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeProductShare> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<IncomeProductShare>()
                   select e;
        }

        protected override IncomeProductShare GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeProductShare>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
