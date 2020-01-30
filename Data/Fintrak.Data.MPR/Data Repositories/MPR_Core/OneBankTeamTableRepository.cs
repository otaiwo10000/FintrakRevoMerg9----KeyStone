using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IOneBankTeamTableRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OneBankTeamTableRepository : DataRepositoryBase<OneBankTeamTable>, IOneBankTeamTableRepository
    {

        protected override OneBankTeamTable AddEntity(MPRContext entityContext, OneBankTeamTable entity)
        {
            return entityContext.Set<OneBankTeamTable>().Add(entity);
        }

        protected override OneBankTeamTable UpdateEntity(MPRContext entityContext, OneBankTeamTable entity)
        {
            return (from e in entityContext.Set<OneBankTeamTable>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OneBankTeamTable> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<OneBankTeamTable>()
                   select e;
        }

        protected override OneBankTeamTable GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<OneBankTeamTable>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
