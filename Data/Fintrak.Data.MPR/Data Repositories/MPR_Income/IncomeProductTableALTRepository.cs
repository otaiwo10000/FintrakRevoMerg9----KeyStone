using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeProductsTableALTRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeProductsTableALTRepository : DataRepositoryBase<IncomeProductsTableALT>, IIncomeProductsTableALTRepository
    {

        protected override IncomeProductsTableALT AddEntity(MPRContext entityContext, IncomeProductsTableALT entity)
        {
            return entityContext.Set<IncomeProductsTableALT>().Add(entity);
        }

        protected override IncomeProductsTableALT UpdateEntity(MPRContext entityContext, IncomeProductsTableALT entity)
        {
            return (from e in entityContext.Set<IncomeProductsTableALT>()
                    where e.ProductID == entity.ProductID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeProductsTableALT> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<IncomeProductsTableALT>()
                   select e).Take(1000).OrderBy(x => x.ProductID);
        }

        protected override IncomeProductsTableALT GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeProductsTableALT>()
                         where e.ProductID == id
                         select e); 

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IncomeProductsTableALT> GetIncomeProductBySearchValue(string searchvalue)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.IncomeProductsTableALTSet
                            where a.ProductCode.StartsWith(searchvalue.Trim()) || a.ProductName.StartsWith(searchvalue.Trim()) 
                            || a.Caption.StartsWith(searchvalue.Trim()) || a.Category.StartsWith(searchvalue.Trim())
                           //|| a.PPR_Caption.StartsWith(searchvalue.Trim())

                            select a;
                           
                return query.ToFullyLoaded().Take(1000).OrderBy(x => x.ProductName);
            }
        }

       
    }
}
