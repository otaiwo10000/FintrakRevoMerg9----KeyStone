using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeRetailProductOverrideTEMPRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeRetailProductOverrideTEMPRepository : DataRepositoryBase<IncomeRetailProductOverrideTEMP>, IIncomeRetailProductOverrideTEMPRepository
    {

        protected override IncomeRetailProductOverrideTEMP AddEntity(MPRContext entityContext, IncomeRetailProductOverrideTEMP entity)
        {
            return entityContext.Set<IncomeRetailProductOverrideTEMP>().Add(entity);
        }

        protected override IncomeRetailProductOverrideTEMP UpdateEntity(MPRContext entityContext, IncomeRetailProductOverrideTEMP entity)
        {
            return (from e in entityContext.Set<IncomeRetailProductOverrideTEMP>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeRetailProductOverrideTEMP> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<IncomeRetailProductOverrideTEMP>()
                   select e).Take(2000);
        }

        protected override IncomeRetailProductOverrideTEMP GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeRetailProductOverrideTEMP>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IncomeRetailProductOverrideTEMP> OverrideByCustomerIdAndBank(int customerId, string bank)
        {
            using (MPRContext entityContext = new MPRContext())
            {
               
                    var query = (from a in entityContext.IncomeRetailProductOverrideTEMPSet

                                 where a.Customerid == customerId && a.Bank.Trim().ToUpper() == bank.Trim().ToUpper()

                             select a)
                             
                            //.OrderByDescending(x => x.Customerid)
                           .ToList();

                return query;
            }
        }

        public IEnumerable<IncomeRetailProductOverrideTEMP> ValidateByCustomerIdAndBank(int customerId, string bank)
        {
            using (MPRContext entityContext = new MPRContext())
            {

                var query = (from a in entityContext.IncomeRetailProductOverrideTEMPSet

                             where a.Customerid == customerId && a.Bank.Trim().ToUpper() == bank.Trim().ToUpper()

                             select a)

                        //.OrderByDescending(x => x.Customerid)
                       .ToList();

                return query;
            }
        }

        public IEnumerable<IncomeRetailProductOverrideTEMP> SearchByCustomerORMISORAcctOfficer(string search)
        {
            using (MPRContext entityContext = new MPRContext())
            {

                var query = (from a in entityContext.IncomeRetailProductOverrideTEMPSet

                                 //where a.Customerid == customerId && a.Bank.Trim().ToUpper() == bank.Trim().ToUpper()
                             where a.Customerid == Convert.ToInt32(search)
                            || a.Mis_code.Trim().ToUpper().Contains(search.Trim().ToUpper())
                            || a.AccountOfficer_Code.Trim().ToUpper().Contains(search.Trim().ToUpper())

                             select a)

                        //.OrderByDescending(x => x.Customerid)
                       .ToList();

                return query;
            }
        }


    }
}
