using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeCashVaultScheduleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeCashVaultScheduleRepository : DataRepositoryBase<IncomeCashVaultSchedule>, IIncomeCashVaultScheduleRepository
    {

        protected override IncomeCashVaultSchedule AddEntity(MPRContext entityContext, IncomeCashVaultSchedule entity)
        {
            return entityContext.Set<IncomeCashVaultSchedule>().Add(entity);
        }

        protected override IncomeCashVaultSchedule UpdateEntity(MPRContext entityContext, IncomeCashVaultSchedule entity)
        {
            return (from e in entityContext.Set<IncomeCashVaultSchedule>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeCashVaultSchedule> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<IncomeCashVaultSchedule>()
                   select e;
        }

        protected override IncomeCashVaultSchedule GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeCashVaultSchedule>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
