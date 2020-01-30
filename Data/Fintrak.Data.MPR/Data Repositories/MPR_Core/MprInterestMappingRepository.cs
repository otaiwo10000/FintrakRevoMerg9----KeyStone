using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IMprInterestMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MprInterestMappingRepository : DataRepositoryBase<MprInterestMapping>, IMprInterestMappingRepository
    {

        protected override MprInterestMapping AddEntity(MPRContext entityContext, MprInterestMapping entity)
        {
            return entityContext.Set<MprInterestMapping>().Add(entity);
        }

        protected override MprInterestMapping UpdateEntity(MPRContext entityContext, MprInterestMapping entity)
        {
            return (from e in entityContext.Set<MprInterestMapping>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MprInterestMapping> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<MprInterestMapping>()
                   select e;
        }

        protected override MprInterestMapping GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MprInterestMapping>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
