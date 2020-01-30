using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ILoanPryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoanPryRepository : DataRepositoryBase<LoanPry>, ILoanPryRepository
    {
        protected override LoanPry AddEntity(IFRSContext entityContext, LoanPry entity)
        {
            return entityContext.Set<LoanPry>().Add(entity);
        }

        protected override LoanPry UpdateEntity(IFRSContext entityContext, LoanPry entity)
        {
            return (from e in entityContext.Set<LoanPry>()
                    where e.PryId == entity.PryId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<LoanPry> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LoanPry>()
                   select e;
        }

        protected override LoanPry GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LoanPry>()
                         where e.PryId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<LoanPryInfo> GetLoanPrys()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.LoanPryDataSet
                            join b in entityContext.ScheduleTypeSet on a.Schedule_Type equals b.Code
                            select new LoanPryInfo()
                            {
                                LoanPry = a,
                                ScheduleType = b
                            };

                return query.ToFullyLoaded();
            }
        }
    }
}