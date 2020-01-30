
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Data.MPR
{
    [Export(typeof(ICaption_transfer_priceRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class caption_transfer_priceRepository : DataRepositoryBase<caption_transfer_price>, ICaption_transfer_priceRepository
    {
        protected override caption_transfer_price AddEntity(MPRContext entityContext, caption_transfer_price entity)
        {
            return entityContext.Set<caption_transfer_price>().Add(entity);
        }

        protected override caption_transfer_price UpdateEntity(MPRContext entityContext, caption_transfer_price entity)
        {
            return (from e in entityContext.Set<caption_transfer_price>()
                    where e.caption_transfer_price_Id == entity.caption_transfer_price_Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<caption_transfer_price> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<caption_transfer_price>()
                   select e;
        }

        protected override caption_transfer_price GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<caption_transfer_price>()
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        //public IEnumerable<crb_Data> GetAllcrb_Data()
        //{
        //    using (MPRContext entityContext = new MPRContext())
        //    {
        //        var query = (from e in entityContext.Set<crb_Data>()
        //                     select e);

        //        return query;
        //    }
        //}

        
    }
}
