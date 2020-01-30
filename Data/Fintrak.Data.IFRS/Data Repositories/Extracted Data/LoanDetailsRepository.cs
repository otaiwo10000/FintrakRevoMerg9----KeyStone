using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IRawLoanDetailsRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RawLoanDetailsRepository : DataRepositoryBase<RawLoanDetails>, IRawLoanDetailsRepository
    {
        protected override RawLoanDetails AddEntity(IFRSContext entityContext, RawLoanDetails entity)
        {
            return entityContext.Set<RawLoanDetails>().Add(entity);
        }

        protected override RawLoanDetails UpdateEntity(IFRSContext entityContext, RawLoanDetails entity)
        {
            return (from e in entityContext.Set<RawLoanDetails>()
                    where e.LoanDetailId == entity.LoanDetailId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<RawLoanDetails> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<RawLoanDetails>()
                   select e;
        }

        protected override RawLoanDetails GetEntity(IFRSContext entityContext, int loanDetailId)
        {
            var query = (from e in entityContext.Set<RawLoanDetails>()
                         where e.LoanDetailId == loanDetailId
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

      
    }
}