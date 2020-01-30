using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeAccountMISOverrideTEMPRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeAccountMISOverrideTEMPRepository : DataRepositoryBase<IncomeAccountMISOverrideTEMP>, IIncomeAccountMISOverrideTEMPRepository
    {

        protected override IncomeAccountMISOverrideTEMP AddEntity(MPRContext entityContext, IncomeAccountMISOverrideTEMP entity)
        {
            return entityContext.Set<IncomeAccountMISOverrideTEMP>().Add(entity);
        }

        protected override IncomeAccountMISOverrideTEMP UpdateEntity(MPRContext entityContext, IncomeAccountMISOverrideTEMP entity)
        {
            return (from e in entityContext.Set<IncomeAccountMISOverrideTEMP>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeAccountMISOverrideTEMP> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<IncomeAccountMISOverrideTEMP>()
                   select e).Take(2000);
        }

        protected override IncomeAccountMISOverrideTEMP GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeAccountMISOverrideTEMP>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IncomeAccountMISOverrideTEMP> OverrideByAccountNumber(string accountno)
        {
            using (MPRContext entityContext = new MPRContext())
            {
               
                    var query = (from a in entityContext.IncomeAccountMISOverrideTEMPSet

                                 where a.accountnumber.Trim().ToUpper().Contains(accountno.Trim().ToUpper())

                             select a)
                             
                           // .OrderByDescending(x => x.accountnumber)
                           .ToList();

                return query;
            }
        }

        public IEnumerable<IncomeAccountMISOverrideTEMP> ValidateByAccountNumber2(string accountno)
        {
            using (MPRContext entityContext = new MPRContext())
            {

                var query = (from a in entityContext.IncomeAccountMISOverrideTEMPSet

                             where a.accountnumber.Trim().ToUpper() == accountno.Trim().ToUpper()

                             select a)

                       // .OrderByDescending(x => x.accountnumber)
                       .ToList();

                return query;
            }
        }

        public IEnumerable<IncomeAccountMISOverrideTEMP> SearchByAccountNoORMISORAcctOfficer(string search)
        {
            using (MPRContext entityContext = new MPRContext())
            {

                var query = (from a in entityContext.IncomeAccountMISOverrideTEMPSet

                             where a.accountnumber.Trim().ToUpper().Contains(search.Trim().ToUpper())
                             || a.mis.Trim().ToUpper().Contains(search.Trim().ToUpper())
                             || a.AccountOfficer_Code.Trim().ToUpper().Contains(search.Trim().ToUpper())

                             select a)

                       // .OrderByDescending(x => x.accountnumber)
                       .ToList();

                return query;
            }
        }


    }
}
