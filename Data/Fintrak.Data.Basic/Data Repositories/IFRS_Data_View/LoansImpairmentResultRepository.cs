using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(ILoansImpairmentResultRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoansImpairmentResultRepository : DataRepositoryBase<LoansImpairmentResult>, ILoansImpairmentResultRepository
    {

        protected override LoansImpairmentResult AddEntity(BasicContext entityContext, LoansImpairmentResult entity)
        {
            return entityContext.Set<LoansImpairmentResult>().Add(entity);
        }

        protected override LoansImpairmentResult UpdateEntity(BasicContext entityContext, LoansImpairmentResult entity)
        {
            return (from e in entityContext.Set<LoansImpairmentResult>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<LoansImpairmentResult> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<LoansImpairmentResult>()
                   select e;
        }

        protected override LoansImpairmentResult GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LoansImpairmentResult>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
 
    }
}
