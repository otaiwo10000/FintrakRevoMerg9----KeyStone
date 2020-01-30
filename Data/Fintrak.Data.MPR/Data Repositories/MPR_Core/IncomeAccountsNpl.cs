using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeAccountsNplRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeAccountsNplRepository : DataRepositoryBase<IncomeAccountsNpl>, IIncomeAccountsNplRepository
    {

        protected override IncomeAccountsNpl AddEntity(MPRContext entityContext, IncomeAccountsNpl entity)
        {
            return entityContext.Set<IncomeAccountsNpl>().Add(entity);
        }

        protected override IncomeAccountsNpl UpdateEntity(MPRContext entityContext, IncomeAccountsNpl entity)
        {
            return (from e in entityContext.Set<IncomeAccountsNpl>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeAccountsNpl> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<IncomeAccountsNpl>()
                   select e;
        }

        protected override IncomeAccountsNpl GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeAccountsNpl>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IncomeAccountsNplInfo> GetIncomeAccountsCustomers()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.IncomeAccountNplSet
                            join b in entityContext.IncomeAccountsPartSet on a.AccountNumber equals b.AcccountNumber
                            //  from c in agg.DefaultIfEmpty()

                            select new IncomeAccountsNplInfo()
                            {
                                AccountNumber = a.AccountNumber,
                                CustomerName = b.CustomerName,

                            };

                return query.ToFullyLoaded().OrderBy(x => x.CustomerName);
            }
        }

    }
}
