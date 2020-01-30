using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IScoreCardKPITypesKBLRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ScoreCardKPITypesKBLRepository : DataRepositoryBase<ScoreCardKPITypesKBL>, IScoreCardKPITypesKBLRepository
    {

        protected override ScoreCardKPITypesKBL AddEntity(MPRContext entityContext, ScoreCardKPITypesKBL entity)
        {
            return entityContext.Set<ScoreCardKPITypesKBL>().Add(entity);
        }

        protected override ScoreCardKPITypesKBL UpdateEntity(MPRContext entityContext, ScoreCardKPITypesKBL entity)
        {
            return (from e in entityContext.Set<ScoreCardKPITypesKBL>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ScoreCardKPITypesKBL> GetEntities(MPRContext entityContext)
        {
            //int maxyear = Convert.ToInt32(entityContext.TeamStructureSet.Max(x => x.Year));
            int currentyear = Convert.ToInt32(entityContext.IncomeSetupSet.Select(x => x.Year).FirstOrDefault());


            return (from e in entityContext.Set<ScoreCardKPITypesKBL>()
                    select e).Where(x=>x.Year== currentyear).Take(500).OrderBy(x => x.KPI_TYPE);
        }

        protected override ScoreCardKPITypesKBL GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ScoreCardKPITypesKBL>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ScoreCardKPITypesKBL> GetScoreCardKPITypesKBLBySearchValue(string searchvalue)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = (from a in entityContext.ScoreCardKPITypesKBLSet
                                 //where a.TeamName.Contains(SearchValue) || a.Team_Code.Contains(SearchValue)
                             where (a.KPI_TYPE.Trim().ToLower().StartsWith(searchvalue.Trim().ToLower()) || a.KPI_METRIC.Trim().ToLower().StartsWith(searchvalue.Trim().ToLower()))
                             select a).OrderBy(x => x.KPI_TYPE);

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<ScoreCardKPITypesKBL> GetScoreCardKPITypesKBLByPeriodYearKPIType(int period, int year, string searchvalue)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = (from a in entityContext.ScoreCardKPITypesKBLSet
                                 //where a.TeamName.Contains(SearchValue) || a.Team_Code.Contains(SearchValue)
                             where (a.Period == period && a.Year == year && a.KPI_TYPE.Trim().ToLower().Contains(searchvalue.Trim().ToLower())) //== searchvalue.Trim().ToLower())
                             select a).OrderBy(x => x.KPI_TYPE);

                return query.ToFullyLoaded();
            }
        }

        //public IEnumerable<ScoreCardKPITypesKBL> GetProductTransferPriceBySearchValue(string searchvalue)
        //{
        //    using (MPRContext entityContext = new MPRContext())
        //    {
        //        var query = (from a in entityContext.ProductTransferPriceSet
        //                     join b in entityContext.ProductSet on a.ProductCode equals b.Code

        //                     select new ProductTransferPriceInfo()
        //                     {
        //                         ID = a.ID,
        //                         ProductCode = a.ProductCode,
        //                         ProductName = b.Name,
        //                         Rating = a.Rating,
        //                         Description = a.Description,
        //                         Category = a.Category,
        //                         BSCategoryName = a.Category.ToString(),
        //                     }).Where(x => x.ProductCode.StartsWith(searchvalue.Trim()) || x.ProductName.StartsWith(searchvalue.Trim())
        //                     || x.Rating.StartsWith(searchvalue.Trim()) || x.BSCategoryName.StartsWith(searchvalue.Trim()));                         

        //        return query.ToFullyLoaded().OrderBy(x => x.ProductCode);
        //    }
        //}



    }
}
