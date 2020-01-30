using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IMPRReportStatusRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MPRReportStatusRepository : DataRepositoryBase<MPRReportStatus>, IMPRReportStatusRepository
    {

        protected override MPRReportStatus AddEntity(MPRContext entityContext, MPRReportStatus entity)
        {
            return entityContext.Set<MPRReportStatus>().Add(entity);
        }

        protected override MPRReportStatus UpdateEntity(MPRContext entityContext, MPRReportStatus entity)
        {
            return (from e in entityContext.Set<MPRReportStatus>()
                    where e.MPRReportStatusId == entity.MPRReportStatusId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MPRReportStatus> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<MPRReportStatus>()
                   select e;
        }

        protected override MPRReportStatus GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MPRReportStatus>()
                         where e.MPRReportStatusId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
