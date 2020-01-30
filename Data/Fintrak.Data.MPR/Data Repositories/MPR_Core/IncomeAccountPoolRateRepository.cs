using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeAccountPoolRateRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeAccountPoolRateRepository : DataRepositoryBase<IncomeAccountPoolRate>, IIncomeAccountPoolRateRepository
    {

        protected override IncomeAccountPoolRate AddEntity(MPRContext entityContext, IncomeAccountPoolRate entity)
        {
            return entityContext.Set<IncomeAccountPoolRate>().Add(entity);
        }

        protected override IncomeAccountPoolRate UpdateEntity(MPRContext entityContext, IncomeAccountPoolRate entity)
        {
            return (from e in entityContext.Set<IncomeAccountPoolRate>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeAccountPoolRate> GetEntities(MPRContext entityContext)
        {
            var query =  from e in entityContext.Set<IncomeAccountPoolRate>()
                   select e;

            return query.ToFullyLoaded().Take(2000);
        }

        protected override IncomeAccountPoolRate GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeAccountPoolRate>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IncomeAccountPoolRate> GetIncomeAccountPoolRateBySearchValue(string searchvalue)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.IncomeAccountPoolRateSet
                            where a.AcccountNumber.ToLower().Trim().Contains(searchvalue.ToLower().Trim())
                            select a;

                return query.ToFullyLoaded();
            }
        }

        
    }
}
