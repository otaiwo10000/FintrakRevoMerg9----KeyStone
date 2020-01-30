using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IDownloadBaseFintrakFinalManualRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DownloadBaseFintrakFinalManualRepository : DataRepositoryBase<DownloadBaseFintrakFinalManual>, IDownloadBaseFintrakFinalManualRepository
    {

        protected override DownloadBaseFintrakFinalManual AddEntity(MPRContext entityContext, DownloadBaseFintrakFinalManual entity)
        {
            return entityContext.Set<DownloadBaseFintrakFinalManual>().Add(entity);
        }

        protected override DownloadBaseFintrakFinalManual UpdateEntity(MPRContext entityContext, DownloadBaseFintrakFinalManual entity)
        {
            return (from e in entityContext.Set<DownloadBaseFintrakFinalManual>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<DownloadBaseFintrakFinalManual> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<DownloadBaseFintrakFinalManual>()
                   select e;
        }

        protected override DownloadBaseFintrakFinalManual GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<DownloadBaseFintrakFinalManual>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
