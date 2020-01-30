using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeRetailProductOverrideRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeRetailProductOverrideRepository : DataRepositoryBase<IncomeRetailProductOverride>, IIncomeRetailProductOverrideRepository
    {

        protected override IncomeRetailProductOverride AddEntity(MPRContext entityContext, IncomeRetailProductOverride entity)
        {
            return entityContext.Set<IncomeRetailProductOverride>().Add(entity);
        }

        protected override IncomeRetailProductOverride UpdateEntity(MPRContext entityContext, IncomeRetailProductOverride entity)
        {
            return (from e in entityContext.Set<IncomeRetailProductOverride>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeRetailProductOverride> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<IncomeRetailProductOverride>()
                   select e).Take(500);
        }

        protected override IncomeRetailProductOverride GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeRetailProductOverride>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IncomeRetailProductOverride> OverrideByCustomerIdAndBank(int customerId, string bank)
        {
            using (MPRContext entityContext = new MPRContext())
            {
               
                    var query = (from a in entityContext.IncomeRetailProductOverrideSet

                                     //where a.Customerid == customerId && a.Bank == bank
                                 where a.Customerid == customerId && a.Bank.Trim().ToUpper() == bank.Trim().ToUpper()

                                 select a)
                             
                            .OrderByDescending(x => x.Customerid)
                           .ToList();

                return query;
            }
        }
      

    }
}
