using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeAccountsTreeAccountTEMPRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeAccountsTreeAccountTEMPRepository : DataRepositoryBase<IncomeAccountsTreeAccountTEMP>, IIncomeAccountsTreeAccountTEMPRepository
    {

        protected override IncomeAccountsTreeAccountTEMP AddEntity(MPRContext entityContext, IncomeAccountsTreeAccountTEMP entity)
        {
            return entityContext.Set<IncomeAccountsTreeAccountTEMP>().Add(entity);
        }

        protected override IncomeAccountsTreeAccountTEMP UpdateEntity(MPRContext entityContext, IncomeAccountsTreeAccountTEMP entity)
        {
            return (from e in entityContext.Set<IncomeAccountsTreeAccountTEMP>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeAccountsTreeAccountTEMP> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<IncomeAccountsTreeAccountTEMP>()
                   select e).Take(2000);
        }

        protected override IncomeAccountsTreeAccountTEMP GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeAccountsTreeAccountTEMP>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IncomeAccountsTreeAccountTEMP> GetIncomeAccountsTreeAccountTEMPBySearchVal(string search)
        {
            using (MPRContext entityContext = new MPRContext())
            {

                var query = (from a in entityContext.IncomeAccountsTreeAccountTEMPSet

                             where a.AccountNumber.Trim().ToUpper().Contains(search.Trim().ToUpper())
                            // || a.ExpirationPeriod == int.TryParse(search.Trim())
                            // || a.ExpirationYear == int.TryParse(search.Trim())

                             select a)

                        //.OrderByDescending(x => x.AccountNumber)
                       .ToList();

                return query;
            }
        }


    }
}
