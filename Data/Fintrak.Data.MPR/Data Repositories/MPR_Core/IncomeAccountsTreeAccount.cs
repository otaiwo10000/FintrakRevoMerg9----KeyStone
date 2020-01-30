using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeAccountsTreeAccountRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeAccountsTreeAccountRepository : DataRepositoryBase<IncomeAccountsTreeAccount>, IIncomeAccountsTreeAccountRepository
    {

        protected override IncomeAccountsTreeAccount AddEntity(MPRContext entityContext, IncomeAccountsTreeAccount entity)
        {
            return entityContext.Set<IncomeAccountsTreeAccount>().Add(entity);
        }

        protected override IncomeAccountsTreeAccount UpdateEntity(MPRContext entityContext, IncomeAccountsTreeAccount entity)
        {
            return (from e in entityContext.Set<IncomeAccountsTreeAccount>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeAccountsTreeAccount> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<IncomeAccountsTreeAccount>()
                   select e;
        }

        protected override IncomeAccountsTreeAccount GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeAccountsTreeAccount>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IncomeAccountsTreeAccount> FilterByAccountNumber(string AccountNumber)
        {
            using (MPRContext entityContext = new MPRContext())
            {

                var query = (from a in entityContext.IncomeAccountsTreeAccountSet

                             where a.AccountNumber.Trim().ToUpper() == AccountNumber.Trim().ToUpper()

                             select a)

                        .OrderByDescending(x => x.AccountNumber)
                       .ToList();

                return query;
            }
        }

    }
}
