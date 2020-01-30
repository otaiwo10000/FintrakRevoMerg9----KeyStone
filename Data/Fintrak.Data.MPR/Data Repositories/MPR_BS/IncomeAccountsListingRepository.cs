using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeAccountsListingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeAccountsListingRepository : DataRepositoryBase<IncomeAccountsListing>, IIncomeAccountsListingRepository
    {

        protected override IncomeAccountsListing AddEntity(MPRContext entityContext, IncomeAccountsListing entity)
        {
            return entityContext.Set<IncomeAccountsListing>().Add(entity);
        }

        protected override IncomeAccountsListing UpdateEntity(MPRContext entityContext, IncomeAccountsListing entity)
        {
            return (from e in entityContext.Set<IncomeAccountsListing>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeAccountsListing> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<IncomeAccountsListing>()
                   select e).Take(500);
        }

        protected override IncomeAccountsListing GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeAccountsListing>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IncomeAccountsListing> FilterByAccountNumber(string accountnumber)
        {
            using (MPRContext entityContext = new MPRContext())
            {
               
                    var query = (from a in entityContext.IncomeAccountsListingSet

                                 where a.accountnumber.Trim().ToUpper() == accountnumber.Trim().ToUpper()

                                 select a)
                             
                            .OrderByDescending(x => x.accountnumber)
                           .ToList();

                return query;
            }
        }


    }
}
