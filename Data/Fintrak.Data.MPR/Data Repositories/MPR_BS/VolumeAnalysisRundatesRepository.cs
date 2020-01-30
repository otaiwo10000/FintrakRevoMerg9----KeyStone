using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IVolumeAnalysisRundatesRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class VolumeAnalysisRundatesRepository : DataRepositoryBase<VolumeAnalysisRundates>, IVolumeAnalysisRundatesRepository
    {

        protected override VolumeAnalysisRundates AddEntity(MPRContext entityContext, VolumeAnalysisRundates entity)
        {
            return entityContext.Set<VolumeAnalysisRundates>().Add(entity);
        }

        protected override VolumeAnalysisRundates UpdateEntity(MPRContext entityContext, VolumeAnalysisRundates entity)
        {
            return (from e in entityContext.Set<VolumeAnalysisRundates>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<VolumeAnalysisRundates> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<VolumeAnalysisRundates>()
                   select e;
        }

        protected override VolumeAnalysisRundates GetEntity(MPRContext entityContext, int Id)
        {
            var query = (from e in entityContext.Set<VolumeAnalysisRundates>()
                         where e.Id == Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
