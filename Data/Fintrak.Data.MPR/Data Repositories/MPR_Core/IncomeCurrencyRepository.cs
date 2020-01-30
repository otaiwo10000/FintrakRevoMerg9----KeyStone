using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeCurrencyRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeCurrencyRepository : DataRepositoryBase<IncomeCurrency>, IIncomeCurrencyRepository
    {

        protected override IncomeCurrency AddEntity(MPRContext entityContext, IncomeCurrency entity)
        {
            return entityContext.Set<IncomeCurrency>().Add(entity);
        }

        protected override IncomeCurrency UpdateEntity(MPRContext entityContext, IncomeCurrency entity)
        {
            return (from e in entityContext.Set<IncomeCurrency>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeCurrency> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<IncomeCurrency>()
                   select e;
        }

        protected override IncomeCurrency GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeCurrency>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
