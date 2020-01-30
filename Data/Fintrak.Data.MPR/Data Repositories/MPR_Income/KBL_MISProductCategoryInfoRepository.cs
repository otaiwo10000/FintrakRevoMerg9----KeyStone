using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IKBL_MISProductCategoryInfoRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class KBL_MISProductCategoryInfoRepository : DataRepositoryBase<KBL_MISProductCategoryInfo>, IKBL_MISProductCategoryInfoRepository
    {

        protected override KBL_MISProductCategoryInfo AddEntity(MPRContext entityContext, KBL_MISProductCategoryInfo entity)
        {
            return entityContext.Set<KBL_MISProductCategoryInfo>().Add(entity);
        }

        protected override KBL_MISProductCategoryInfo UpdateEntity(MPRContext entityContext, KBL_MISProductCategoryInfo entity)
        {
            return (from e in entityContext.Set<KBL_MISProductCategoryInfo>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<KBL_MISProductCategoryInfo> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<KBL_MISProductCategoryInfo>()
                   select e).Take(1000).OrderBy(x => x.Id);
        }

        protected override KBL_MISProductCategoryInfo GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<KBL_MISProductCategoryInfo>()
                         where e.Id == id
                         select e); 

            var results = query.FirstOrDefault();

            return results;
        }

       
    }
}
