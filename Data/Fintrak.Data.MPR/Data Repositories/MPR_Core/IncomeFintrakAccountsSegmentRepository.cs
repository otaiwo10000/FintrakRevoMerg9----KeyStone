using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeFintrakAccountsSegmentRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeFintrakAccountsSegmentRepository : DataRepositoryBase<IncomeFintrakAccountsSegment>, IIncomeFintrakAccountsSegmentRepository
    {

        protected override IncomeFintrakAccountsSegment AddEntity(MPRContext entityContext, IncomeFintrakAccountsSegment entity)
        {
            return entityContext.Set<IncomeFintrakAccountsSegment>().Add(entity);
        }

        protected override IncomeFintrakAccountsSegment UpdateEntity(MPRContext entityContext, IncomeFintrakAccountsSegment entity)
        {
            return (from e in entityContext.Set<IncomeFintrakAccountsSegment>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeFintrakAccountsSegment> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<IncomeFintrakAccountsSegment>()
                   select e;
        }

        protected override IncomeFintrakAccountsSegment GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeFintrakAccountsSegment>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IncomeFintrakAccountsSegment> GetAccountsSegmentByCustomerIdBank(string customerId, string bank)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                customerId = customerId.Replace("FORWARDSLASHXTER", "/");
                customerId = customerId.Replace("DOTXTER", ".");

                bank = bank.Replace("FORWARDSLASHXTER", "/");
                bank = bank.Replace("DOTXTER", ".");

                var query = new List<IncomeFintrakAccountsSegment>();

                {
                    query = (from a in entityContext.IncomeFintrakAccountsSegmentSet
                             where a.CUSTOMERID.StartsWith(customerId.Trim()) && a.Bank.StartsWith(bank.Trim())
                             select a).ToList();
                }

                return query;
            }
        }


    }
}
