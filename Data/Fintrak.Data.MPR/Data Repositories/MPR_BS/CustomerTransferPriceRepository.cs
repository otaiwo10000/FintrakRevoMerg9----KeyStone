using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(ICustomerTransferPriceRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CustomerTransferPriceRepository : DataRepositoryBase<CustomerTransferPrice>, ICustomerTransferPriceRepository
    {

        protected override CustomerTransferPrice AddEntity(MPRContext entityContext, CustomerTransferPrice entity)
        {
            return entityContext.Set<CustomerTransferPrice>().Add(entity);
        }

        protected override CustomerTransferPrice UpdateEntity(MPRContext entityContext, CustomerTransferPrice entity)
        {
            return (from e in entityContext.Set<CustomerTransferPrice>()
                    where e.customertransferpriceId == entity.customertransferpriceId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CustomerTransferPrice> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<CustomerTransferPrice>()
                   select e;
        }

        protected override CustomerTransferPrice GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CustomerTransferPrice>()
                         where e.customertransferpriceId == id
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
       
        public IEnumerable<CustomerTransferPriceInfo> GetCustomerTransferPricebySetUp()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var setup = GetSetUp();

                //var query = entityContext.MISTransferPriceSet
                //            .Where(a=>a.Year.ToString() == setup.Year && a.Period == setup.Period)

                var query = from a in entityContext.CustomerTransferPriceSet
                                //join b in entityContext.CurrencySet on a. equals b.CurrencyId
                            where a.Year.ToString() == setup.Year && a.Period == setup.Period

                            select new CustomerTransferPriceInfo()
                            //.Select(d => new CategoryTransferPriceInfo()
                            {
                                customertransferpriceId = a.customertransferpriceId,
                                CustNo = a.CustNo,
                                Category = a.Category,
                                BSCategoryName = a.Category.ToString(),
                                Period = a.Period,
                                Year = a.Year,
                                CompanyCode = a.CompanyCode,
                                Rate = a.Rate,
                                SolutionId = a.SolutionId,

                                // });
                            };

                return query.ToFullyLoaded().Take(1000).OrderBy(x => x.BSCategoryName);
            }
        }

        public IEnumerable<CustomerTransferPriceInfo> GetCustomerTransferPricebysearch(string search)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                //var query = entityContext.MISTransferPriceSet
                //            .Where(a=>a.Year.ToString() == setup.Year && a.Period == setup.Period)

                var query = (from a in entityContext.CustomerTransferPriceSet
                                 //join b in entityContext.CurrencySet on a. equals b.CurrencyId

                             select new CustomerTransferPriceInfo()
                             //.Select(d => new CategoryTransferPriceInfo()
                             {
                                 customertransferpriceId = a.customertransferpriceId,
                                 CustNo = a.CustNo,
                                 Category = a.Category,
                                 BSCategoryName = a.Category.ToString(),
                                 Period = a.Period,
                                 Year = a.Year,
                                 CompanyCode = a.CompanyCode,
                                 Rate = a.Rate,
                                 SolutionId = a.SolutionId,

                                 // });
                             }).Where(x => x.CustNo.StartsWith(search.Trim()) || x.BSCategoryName.StartsWith(search.Trim()));

                return query.ToFullyLoaded().Take(1000).OrderBy(x => x.BSCategoryName);
            }
        }
    }
}
