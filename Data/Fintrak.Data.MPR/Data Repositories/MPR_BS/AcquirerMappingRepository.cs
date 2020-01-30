using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IAcquirerMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AcquirerMappingRepository : DataRepositoryBase<AcquirerMapping>, IAcquirerMappingRepository
    {

        protected override AcquirerMapping AddEntity(MPRContext entityContext, AcquirerMapping entity)
        {
            return entityContext.Set<AcquirerMapping>().Add(entity);
        }

        protected override AcquirerMapping UpdateEntity(MPRContext entityContext, AcquirerMapping entity)
        {
            return (from e in entityContext.Set<AcquirerMapping>()
                    where e.mpr_Acquirer_Mapping_Id == entity.mpr_Acquirer_Mapping_Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<AcquirerMapping> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<AcquirerMapping>()
                   select e;
        }

        protected override AcquirerMapping GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<AcquirerMapping>()
                         where e.mpr_Acquirer_Mapping_Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
