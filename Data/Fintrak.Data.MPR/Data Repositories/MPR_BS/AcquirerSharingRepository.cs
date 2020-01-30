using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IAcquirerSharingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AcquirerSharingRepository : DataRepositoryBase<AcquirerSharing>, IAcquirerSharingRepository
    {

        protected override AcquirerSharing AddEntity(MPRContext entityContext, AcquirerSharing entity)
        {
            return entityContext.Set<AcquirerSharing>().Add(entity);
        }

        protected override AcquirerSharing UpdateEntity(MPRContext entityContext, AcquirerSharing entity)
        {
            return (from e in entityContext.Set<AcquirerSharing>()
                    where e.mpr_Acquirer_Sharing_Id == entity.mpr_Acquirer_Sharing_Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<AcquirerSharing> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<AcquirerSharing>()
                   select e;
        }

        protected override AcquirerSharing GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<AcquirerSharing>()
                         where e.mpr_Acquirer_Sharing_Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
