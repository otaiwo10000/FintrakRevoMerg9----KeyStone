using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeCommFeeMisRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeCommFeeMisRepository : DataRepositoryBase<IncomeCommFeeMis>, IIncomeCommFeeMisRepository
    {

        protected override IncomeCommFeeMis AddEntity(MPRContext entityContext, IncomeCommFeeMis entity)
        {
            return entityContext.Set<IncomeCommFeeMis>().Add(entity);
        }

        protected override IncomeCommFeeMis UpdateEntity(MPRContext entityContext, IncomeCommFeeMis entity)
        {
            return (from e in entityContext.Set<IncomeCommFeeMis>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeCommFeeMis> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<IncomeCommFeeMis>()
                   select e;
        }

        protected override IncomeCommFeeMis GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeCommFeeMis>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
