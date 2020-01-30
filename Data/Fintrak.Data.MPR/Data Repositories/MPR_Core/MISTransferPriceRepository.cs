using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IMISTransferPriceRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MISTransferPriceRepository : DataRepositoryBase<MISTransferPrice>, IMISTransferPriceRepository
    {

        protected override MISTransferPrice AddEntity(MPRContext entityContext, MISTransferPrice entity)
        {
            return entityContext.Set<MISTransferPrice>().Add(entity);
        }

        protected override MISTransferPrice UpdateEntity(MPRContext entityContext, MISTransferPrice entity)
        {
            return (from e in entityContext.Set<MISTransferPrice>()
                    where e.mistransferpriceId == entity.mistransferpriceId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MISTransferPrice> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<MISTransferPrice>()
                   select e).Take(2000).OrderBy(x => x.mistransferpriceId);
        }

        protected override MISTransferPrice GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MISTransferPrice>()
                         where e.mistransferpriceId == id
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
        //public IEnumerable<ScoreCardMetrics> GetMetricsBySetUp()
        //{
        //    using (MPRContext entityContext = new MPRContext())
        //    {
        //        var setup = GetSetUp();

        //        var query = (from a in entityContext.ScoreCardMetricsSet
        //                     where Convert.ToString(a.Year) == setup.Year && a.Period == setup.Period
        //                     select a)
        //                     .OrderBy(x => x.Metric).Take(100)
        //                    .ToList();

        //        return query;
        //    }
        //}

        public IEnumerable<MISTransferPriceInfo> GetMISTransferPricebySetUp()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var setup = GetSetUp();

                //var query = from a in entityContext.MISTransferPriceSet
                //            where a.Year.ToString() == setup.Year && a.Period == setup.Period

                //var query = entityContext.MISTransferPriceSet
                //            .Where(a=>a.Year.ToString() == setup.Year && a.Period == setup.Period)

                var query = from a in entityContext.MISTransferPriceSet
                                //join b in entityContext.CurrencySet on a. equals b.CurrencyId
                            where a.Year.ToString() == setup.Year && a.Period == setup.Period

                            select new MISTransferPriceInfo()
                            //.Select(d => new MISTransferPriceInfo()
                            {
                                mistransferpriceId = a.mistransferpriceId,
                                DefinitionCode = a.DefinitionCode,
                                MisCode = a.MisCode,
                                BalanceSheetCategory = a.BalanceSheetCategory,
                                BSCategoryName = a.BalanceSheetCategory.ToString(),
                                CurrencyType = a.CurrencyType,
                                CurrencyTypeName = a.CurrencyType.ToString(),
                                Rate = a.Rate,
                                Period = a.Period,
                                Year = a.Year,
                                SolutionId = a.SolutionId,
                                CompanyCode = a.CompanyCode,
                                // });
                            };

                return query.ToFullyLoaded().Take(2000).OrderBy(x => x.DefinitionCode);
            }
        }

        public IEnumerable<MISTransferPriceInfo> GetMISTransferPricebyParams(string defCode, string miscode, string category, string currency, int year, int period)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.MISTransferPriceSet
                            //join b in entityContext.CurrencySet on a.Currency equals b.CurrencyId
                            where (a.DefinitionCode.StartsWith(defCode.Trim()) || string.IsNullOrEmpty(defCode))
                            && (a.MisCode.StartsWith(miscode.Trim()) || string.IsNullOrEmpty(miscode))
                            && (a.BalanceSheetCategory.ToString().StartsWith(category.Trim()) || string.IsNullOrEmpty(category))
                            && (a.CurrencyType.ToString().StartsWith(currency.Trim()) || string.IsNullOrEmpty(currency))
                            && (a.Period == period || period == 0)
                            && (a.Year == year || year == 0)

                            select new MISTransferPriceInfo()
                            //.Select(d => new MISTransferPriceInfo()
                            {
                                mistransferpriceId = a.mistransferpriceId,
                                DefinitionCode = a.DefinitionCode,
                                MisCode = a.MisCode,
                                BalanceSheetCategory = a.BalanceSheetCategory,
                                BSCategoryName = a.BalanceSheetCategory.ToString(),
                                CurrencyType = a.CurrencyType,
                                CurrencyTypeName = a.CurrencyType.ToString(),
                                Rate = a.Rate,
                                Period = a.Period,
                                Year = a.Year,
                                SolutionId = a.SolutionId,
                                CompanyCode = a.CompanyCode,
                            };

                return query.ToFullyLoaded().Take(2000).OrderBy(x => x.DefinitionCode);
            }
        }

        //public IEnumerable<MISTransferPriceInfo> GetMISTransferPricebyParams(string defCode, string miscode, string category, string currency, int year, int period)
        //{
        //    using (MPRContext entityContext = new MPRContext())
        //    {
        //        var query = entityContext.MISTransferPriceSet                     
        //                    .Where(              
        //                        x => x.DefinitionCode.StartsWith(defCode.Trim()) || string.IsNullOrEmpty(defCode)
        //                    && (x.MisCode.StartsWith(miscode.Trim()) || string.IsNullOrEmpty(miscode))
        //                    && (x.Category.StartsWith(category.Trim()) || string.IsNullOrEmpty(category))
        //                    && (x.Currency.StartsWith(currency.Trim()) || string.IsNullOrEmpty(currency))
        //                    && (x.Period == period || period == 0)
        //                    && (x.Year == year || year == 0))

        //                    //select new MISTransferPrice()
        //                    .Select(d => new MISTransferPriceInfo()
        //                    {
        //                        mistransferpriceId = d.mistransferpriceId,
        //                        DefinitionCode = d.DefinitionCode,
        //                        MisCode = d.MisCode,
        //                        Category = d.Category,
        //                        Currency = d.Currency,
        //                        Rate = d.Rate,
        //                        Period = d.Period,
        //                        Year = d.Year,
        //                        SolutionId = d.SolutionId,
        //                        CompanyCode = d.CompanyCode,
        //                    });

        //        return query.ToFullyLoaded().Take(2000).OrderBy(x => x.DefinitionCode);
        //    }
        //}

    }
}
