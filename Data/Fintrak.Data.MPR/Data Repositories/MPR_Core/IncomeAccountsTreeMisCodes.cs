using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeAccountsTreeMisCodesRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeAccountsTreeMisCodesRepository : DataRepositoryBase<IncomeAccountsTreeMisCodes>, IIncomeAccountsTreeMisCodesRepository
    {

        protected override IncomeAccountsTreeMisCodes AddEntity(MPRContext entityContext, IncomeAccountsTreeMisCodes entity)
        {
            return entityContext.Set<IncomeAccountsTreeMisCodes>().Add(entity);
        }

        protected override IncomeAccountsTreeMisCodes UpdateEntity(MPRContext entityContext, IncomeAccountsTreeMisCodes entity)
        {
            return (from e in entityContext.Set<IncomeAccountsTreeMisCodes>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeAccountsTreeMisCodes> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<IncomeAccountsTreeMisCodes>()
                   select e;
        }

        protected override IncomeAccountsTreeMisCodes GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeAccountsTreeMisCodes>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IncomeAccountsTreeMisCodes> GetByAccountNumber(string AccountNumber)
        {
            using (MPRContext entityContext = new MPRContext())
            {

                var query = (from a in entityContext.IncomeAccountsTreeMisCodesSet

                             where a.AccountNumber.Trim().ToUpper() == AccountNumber.Trim().ToUpper()

                             select a)

                        .OrderByDescending(x => x.AccountNumber)
                       .ToList();

                return query;
            }
        }

    }
}
