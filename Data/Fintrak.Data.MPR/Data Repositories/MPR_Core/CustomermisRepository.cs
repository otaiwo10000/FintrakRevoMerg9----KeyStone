using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(ICustomermisRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CustomermisRepository : DataRepositoryBase<Customermis>, ICustomermisRepository
    {

        protected override Customermis AddEntity(MPRContext entityContext, Customermis entity)
        {
            return entityContext.Set<Customermis>().Add(entity);
        }

        protected override Customermis UpdateEntity(MPRContext entityContext, Customermis entity)
        {
            return (from e in entityContext.Set<Customermis>()
                    where e.CustomermisId == entity.CustomermisId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Customermis> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<Customermis>()
                   select e;
        }

        protected override Customermis GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<Customermis>()
                         where e.CustomermisId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
