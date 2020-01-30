using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeSplitPoolsRatesAndBasisRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeSplitPoolsRatesAndBasisRepository : DataRepositoryBase<IncomeSplitPoolsRatesAndBasis>, IIncomeSplitPoolsRatesAndBasisRepository
    {

        protected override IncomeSplitPoolsRatesAndBasis AddEntity(MPRContext entityContext, IncomeSplitPoolsRatesAndBasis entity)
        {
            return entityContext.Set<IncomeSplitPoolsRatesAndBasis>().Add(entity);
        }

        protected override IncomeSplitPoolsRatesAndBasis UpdateEntity(MPRContext entityContext, IncomeSplitPoolsRatesAndBasis entity)
        {
            return (from e in entityContext.Set<IncomeSplitPoolsRatesAndBasis>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeSplitPoolsRatesAndBasis> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<IncomeSplitPoolsRatesAndBasis>()
                   select e;
        }

        protected override IncomeSplitPoolsRatesAndBasis GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeSplitPoolsRatesAndBasis>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IncomeSplitPoolsRatesAndBasis> IncomeSplitByYearAndPeriod(int year, int period)
        {
            using (MPRContext entityContext = new MPRContext())
            {
               
                    var query = (from a in entityContext.IncomeSplitPoolsRatesAndBasisSet

                             where a.Year == year && a.Period == period

                             select a)
                             
                            .OrderByDescending(x => x.Period)
                           .ToList();

                return query;
            }
        }
      

    }
}
