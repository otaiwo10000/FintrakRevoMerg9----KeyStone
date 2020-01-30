using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeAccountsTreeMisCodesTEMPRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeAccountsTreeMisCodesTEMPRepository : DataRepositoryBase<IncomeAccountsTreeMisCodesTEMP>, IIncomeAccountsTreeMisCodesTEMPRepository
    {

        protected override IncomeAccountsTreeMisCodesTEMP AddEntity(MPRContext entityContext, IncomeAccountsTreeMisCodesTEMP entity)
        {
            return entityContext.Set<IncomeAccountsTreeMisCodesTEMP>().Add(entity);
        }

        protected override IncomeAccountsTreeMisCodesTEMP UpdateEntity(MPRContext entityContext, IncomeAccountsTreeMisCodesTEMP entity)
        {
            return (from e in entityContext.Set<IncomeAccountsTreeMisCodesTEMP>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeAccountsTreeMisCodesTEMP> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<IncomeAccountsTreeMisCodesTEMP>()
                   select e).Take(2000);
        }

        protected override IncomeAccountsTreeMisCodesTEMP GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeAccountsTreeMisCodesTEMP>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IncomeAccountsTreeMisCodesTEMP> GetIncomeAccountsTreeMisCodesTEMPBySearchVal(string search)
        {
            using (MPRContext entityContext = new MPRContext())
            {

                var query = (from a in entityContext.IncomeAccountsTreeMisCodesTEMPSet

                             where a.AccountNumber.Trim().ToUpper().Contains(search.Trim().ToUpper())
                             || a.AccountOfficer_Code.Trim().ToUpper().Contains(search.Trim().ToUpper())
                             || a.AccountOfficerName.Trim().ToUpper().Contains(search.Trim().ToUpper())
                             || a.Team_Code.Trim().ToUpper().Contains(search.Trim().ToUpper())

                             select a)

                        //.OrderByDescending(x => x.AccountNumber)
                       .ToList();

                return query;
            }
        }

    }
}
