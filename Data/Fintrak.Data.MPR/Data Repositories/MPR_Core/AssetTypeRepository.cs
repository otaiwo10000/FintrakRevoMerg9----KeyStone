
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
    [Export(typeof(IAssetTypeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AssetTypeRepository : DataRepositoryBase<AssetType>, IAssetTypeRepository
    {
        protected override AssetType AddEntity(MPRContext entityContext, AssetType entity)
        {
            return entityContext.Set<AssetType>().Add(entity);
        }

        protected override AssetType UpdateEntity(MPRContext entityContext, AssetType entity)
        {
            return (from e in entityContext.Set<AssetType>()
                    where e.AssetType_Id == entity.AssetType_Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<AssetType> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<AssetType>()
                   select e;
        }

        protected override AssetType GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<AssetType>()
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
