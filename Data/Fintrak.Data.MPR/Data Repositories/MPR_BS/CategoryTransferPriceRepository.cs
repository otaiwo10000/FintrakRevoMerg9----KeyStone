using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(ICategoryTransferPriceRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CategoryTransferPriceRepository : DataRepositoryBase<CategoryTransferPrice>, ICategoryTransferPriceRepository
    {

        protected override CategoryTransferPrice AddEntity(MPRContext entityContext, CategoryTransferPrice entity)
        {
            return entityContext.Set<CategoryTransferPrice>().Add(entity);
        }

        protected override CategoryTransferPrice UpdateEntity(MPRContext entityContext, CategoryTransferPrice entity)
        {
            return (from e in entityContext.Set<CategoryTransferPrice>()
                    where e.CategoryTransferPriceId == entity.CategoryTransferPriceId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CategoryTransferPrice> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<CategoryTransferPrice>()
                   select e;
        }

        protected override CategoryTransferPrice GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CategoryTransferPrice>()
                         where e.CategoryTransferPriceId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        private SetUp GetSetUp()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                SetUp query = (from a in entityContext.SetUpSet
                               select a).FirstOrDefault();

                return query;
            }
        }
        //public IEnumerable<CategoryTransferPriceInfo> GetCategoryTransferPriceInfobySetUp()
        //{
        //    using (MPRContext entityContext = new MPRContext())
        //    {
        //        var setup = GetSetUp();

        //        //var query = entityContext.MISTransferPriceSet
        //        //            .Where(a=>a.Year.ToString() == setup.Year && a.Period == setup.Period)

        //        var query = from a in entityContext.CategoryTransferPriceSet
        //                        //join b in entityContext.CurrencySet on a. equals b.CurrencyId
        //                    where a.Year.ToString() == setup.Year && a.Period == setup.Period

        //                    select new CategoryTransferPriceInfo()
        //                    //.Select(d => new CategoryTransferPriceInfo()
        //                    {
        //                        CategoryTransferPriceId = a.CategoryTransferPriceId,
        //                        BalanceSheetCategory = a.BalanceSheetCategory,
        //                        BSCategoryName = a.BalanceSheetCategory.ToString(),
        //                        Period = a.Period,
        //                        Year = a.Year,
        //                        CurrencyType = a.CurrencyType,
        //                        CurrencyTypeName = a.CurrencyType.ToString(),                              
        //                        Rate = a.Rate,
                               
        //                        // });
        //                    };

        //        return query.ToFullyLoaded().Take(500).OrderBy(x => x.BSCategoryName);
        //    }
        //}

        public IEnumerable<CategoryTransferPriceInfo> GetCategoryTransferPricebySetUp()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var setup = GetSetUp();

                //var query = entityContext.MISTransferPriceSet
                //            .Where(a=>a.Year.ToString() == setup.Year && a.Period == setup.Period)

                var query = from a in entityContext.CategoryTransferPriceSet
                                //join b in entityContext.CurrencySet on a. equals b.CurrencyId
                            where a.Year.ToString() == setup.Year && a.Period == setup.Period

                            select new CategoryTransferPriceInfo()
                            //.Select(d => new CategoryTransferPriceInfo()
                            {
                                CategoryTransferPriceId = a.CategoryTransferPriceId,
                                BalanceSheetCategory = a.BalanceSheetCategory,
                                BSCategoryName = a.BalanceSheetCategory.ToString(),
                                Period = a.Period,
                                Year = a.Year,
                                CurrencyType = a.CurrencyType,
                                CurrencyTypeName = a.CurrencyType.ToString(),
                                Rate = a.Rate,

                                // });
                            };

                return query.ToFullyLoaded().Take(1000).OrderBy(x => x.BSCategoryName);
            }
        }

        public IEnumerable<CategoryTransferPriceInfo> GetCategoryTransferPricebysearch(string search)
        {
            using (MPRContext entityContext = new MPRContext())
            {              
                //var query = entityContext.MISTransferPriceSet
                //            .Where(a=>a.Year.ToString() == setup.Year && a.Period == setup.Period)

                var query = (from a in entityContext.CategoryTransferPriceSet
                                //join b in entityContext.CurrencySet on a. equals b.CurrencyId

                            select new CategoryTransferPriceInfo()
                            //.Select(d => new CategoryTransferPriceInfo()
                            {
                                CategoryTransferPriceId = a.CategoryTransferPriceId,
                                BalanceSheetCategory = a.BalanceSheetCategory,
                                BSCategoryName = a.BalanceSheetCategory.ToString(),
                                Period = a.Period,
                                Year = a.Year,
                                CurrencyType = a.CurrencyType,
                                CurrencyTypeName = a.CurrencyType.ToString(),
                                Rate = a.Rate,

                                // });
                            }).Where(x=>x.BSCategoryName.StartsWith(search.Trim()) || x.CurrencyTypeName==search.Trim());

                return query.ToFullyLoaded().Take(1000).OrderBy(x => x.BSCategoryName);
            }
        }

    }
}
