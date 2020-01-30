using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeNEAMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeNEAMappingRepository : DataRepositoryBase<IncomeNEAMapping>, IIncomeNEAMappingRepository
    {

        protected override IncomeNEAMapping AddEntity(MPRContext entityContext, IncomeNEAMapping entity)
        {
            return entityContext.Set<IncomeNEAMapping>().Add(entity);
        }

        protected override IncomeNEAMapping UpdateEntity(MPRContext entityContext, IncomeNEAMapping entity)
        {
            return (from e in entityContext.Set<IncomeNEAMapping>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }     

        protected override IncomeNEAMapping GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeNEAMapping>()
                         where e.ID == id
                         select e); 

            var results = query.FirstOrDefault();

            return results;
        }

        protected override IEnumerable<IncomeNEAMapping> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<IncomeNEAMapping>()
                    select e).Take(1000).OrderBy(x => x.ID);
        }

        //public IEnumerable<IncomeNEAMapping> GetIncomeNEAMappingBySearchValue(string searchvalue)
        //{
        //    using (MPRContext entityContext = new MPRContext())
        //    {
        //        var query = from a in entityContext.IncomeNEAMappingSet
        //                    where a.Product_Code.StartsWith(searchvalue.Trim()) || a.AssetType.StartsWith(searchvalue.Trim()) 
        //                    || a.Class.StartsWith(searchvalue.Trim())

        //                    select a;
                           
        //        return query.ToFullyLoaded().Take(1000).OrderBy(x => x.Class);
        //    }
        //}

        public IEnumerable<IncomeNEAMappingInfo> GetIncomeNEAMappingBySearchValue(string searchvalue)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.IncomeNEAMappingSet
                            join b in entityContext.KBL_MISProductCategoryInfoSet on a.Category_Code equals b.CATEGORY_CODE

                            select new IncomeNEAMappingInfo()
                            {
                                
                                Id = a.ID,
                                Category_Code = a.Category_Code,
                                CATEGORY_DESCRIPTION = b.CATEGORY_DESCRIPTION,
                                Product_Code = a.Product_Code,
                                Class = a.Class,
                                Caption = a.Caption,
                                AssetType = a.AssetType,
                            };

                return query.ToFullyLoaded().OrderBy(x => x.CATEGORY_DESCRIPTION);
            }
        }

        public IEnumerable<IncomeNEAMappingInfo> GetFullIncomeNEAMapping()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.IncomeNEAMappingSet
                            join b in entityContext.KBL_MISProductCategoryInfoSet on a.Category_Code equals b.CATEGORY_CODE

                            select new IncomeNEAMappingInfo()
                            {

                                Id = a.ID,
                                Category_Code = a.Category_Code,
                                CATEGORY_DESCRIPTION = b.CATEGORY_DESCRIPTION,
                                Product_Code = a.Product_Code,
                                Class = a.Class,
                                Caption = a.Caption,
                                AssetType = a.AssetType,
                            };

                return query.ToFullyLoaded().OrderBy(x => x.CATEGORY_DESCRIPTION);
            }
        }

        
    }
}
