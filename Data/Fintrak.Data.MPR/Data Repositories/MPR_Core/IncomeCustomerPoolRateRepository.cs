using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeCustomerPoolRateRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeCustomerPoolRateRepository : DataRepositoryBase<IncomeCustomerPoolRate>, IIncomeCustomerPoolRateRepository
    {

        protected override IncomeCustomerPoolRate AddEntity(MPRContext entityContext, IncomeCustomerPoolRate entity)
        {
            return entityContext.Set<IncomeCustomerPoolRate>().Add(entity);
        }

        protected override IncomeCustomerPoolRate UpdateEntity(MPRContext entityContext, IncomeCustomerPoolRate entity)
        {
            return (from e in entityContext.Set<IncomeCustomerPoolRate>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeCustomerPoolRate> GetEntities(MPRContext entityContext)
        {
            var query =  from e in entityContext.Set<IncomeCustomerPoolRate>()
                   select e;

            return query.ToFullyLoaded().Take(2000);
        }

        protected override IncomeCustomerPoolRate GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeCustomerPoolRate>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IncomeCustomerPoolRate> GetIncomeCustomerPoolRateBySearchValue(string searchvalue)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.IncomeCustomerPoolRateSet
                            where a.CustomerNo.ToLower().Trim().Contains(searchvalue.ToLower().Trim())
                            select a;

                return query.ToFullyLoaded();
            }
        }

        public IncomeCustomerPoolRate ValidateIncomeCustomerPoolRate(string customernumber, int year)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.IncomeCustomerPoolRateSet
                            where a.CustomerNo.ToLower().Trim() == customernumber.ToLower().Trim() && a.Year == year
                            select a;

                return query.FirstOrDefault();
            }
        }
    }
}
