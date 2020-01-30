using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeCaptionPositionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeCaptionPositionRepository : DataRepositoryBase<IncomeCaptionPosition>, IIncomeCaptionPositionRepository
    {

        protected override IncomeCaptionPosition AddEntity(MPRContext entityContext, IncomeCaptionPosition entity)
        {
            return entityContext.Set<IncomeCaptionPosition>().Add(entity);
        }

        protected override IncomeCaptionPosition UpdateEntity(MPRContext entityContext, IncomeCaptionPosition entity)
        {
            return (from e in entityContext.Set<IncomeCaptionPosition>()
                    where e.ID == entity.EntityId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeCaptionPosition> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<IncomeCaptionPosition>()
                   select e).OrderBy(x => x.Caption);
        }

        protected override IncomeCaptionPosition GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeCaptionPosition>()
                         where e.ID == id
                         select e); 

            var results = query.FirstOrDefault();

            return results;
        }
        
       
    }
}
