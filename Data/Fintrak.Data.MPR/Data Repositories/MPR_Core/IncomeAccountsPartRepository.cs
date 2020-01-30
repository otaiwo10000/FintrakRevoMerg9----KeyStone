using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeAccountsPartRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeAccountsPartRepository : DataRepositoryBase<IncomeAccountsPart>, IIncomeAccountsPartRepository
    {

        protected override IncomeAccountsPart AddEntity(MPRContext entityContext, IncomeAccountsPart entity)
        {
            return entityContext.Set<IncomeAccountsPart>().Add(entity);
        }

        protected override IncomeAccountsPart UpdateEntity(MPRContext entityContext, IncomeAccountsPart entity)
        {
            return (from e in entityContext.Set<IncomeAccountsPart>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeAccountsPart> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<IncomeAccountsPart>()
                   select e;
        }

        protected override IncomeAccountsPart GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeAccountsPart>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

       

      
    }
}
