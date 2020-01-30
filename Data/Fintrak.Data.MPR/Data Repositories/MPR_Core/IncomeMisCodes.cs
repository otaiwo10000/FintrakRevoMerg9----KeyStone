using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeMisCodesRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeMisCodesRepository : DataRepositoryBase<IncomeMisCodes>, IIncomeMisCodesRepository
    {

        protected override IncomeMisCodes AddEntity(MPRContext entityContext, IncomeMisCodes entity)
        {
            return entityContext.Set<IncomeMisCodes>().Add(entity);
        }

        protected override IncomeMisCodes UpdateEntity(MPRContext entityContext, IncomeMisCodes entity)
        {
            return (from e in entityContext.Set<IncomeMisCodes>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeMisCodes> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<IncomeMisCodes>()
                   select e;
        }

        protected override IncomeMisCodes GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeMisCodes>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
