using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core
{
    [Export(typeof(IIncomeCRRSectorRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeCRRSectorRepository : DataRepositoryBase<IncomeCRRSector>, IIncomeCRRSectorRepository
    {
        protected override IncomeCRRSector AddEntity(CoreContext entityContext, IncomeCRRSector entity)
        {
            return entityContext.Set<IncomeCRRSector>().Add(entity);
        }

        protected override IncomeCRRSector UpdateEntity(CoreContext entityContext, IncomeCRRSector entity)
        {
            return (from e in entityContext.Set<IncomeCRRSector>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeCRRSector> GetEntities(CoreContext entityContext)
        {
            return from e in entityContext.Set<IncomeCRRSector>()
                   select e;
        }

        protected override IncomeCRRSector GetEntity(CoreContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeCRRSector>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
