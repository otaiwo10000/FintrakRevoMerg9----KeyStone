using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeCommFeeBusinessRuleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeCommFeeBusinessRuleRepository : DataRepositoryBase<IncomeCommFeeBusinessRule>, IIncomeCommFeeBusinessRuleRepository
    {

        protected override IncomeCommFeeBusinessRule AddEntity(MPRContext entityContext, IncomeCommFeeBusinessRule entity)
        {
            return entityContext.Set<IncomeCommFeeBusinessRule>().Add(entity);
        }

        protected override IncomeCommFeeBusinessRule UpdateEntity(MPRContext entityContext, IncomeCommFeeBusinessRule entity)
        {
            return (from e in entityContext.Set<IncomeCommFeeBusinessRule>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeCommFeeBusinessRule> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<IncomeCommFeeBusinessRule>()
                   select e).Take(500);
        }

        protected override IncomeCommFeeBusinessRule GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeCommFeeBusinessRule>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IncomeCommFeeBusinessRule> GetIncomeCommFeeBusinessRuleBySearchValue(string searchvalue)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.IncomeCommFeeBusinessRuleSet
                            where a.GL_Description.Trim().ToLower().StartsWith(searchvalue.Trim().ToLower()) || a.GLCode.Trim().ToLower().StartsWith(searchvalue.Trim().ToLower()) || a.Basis_of_Allocation.Trim().ToLower().StartsWith(searchvalue.Trim().ToLower())
                            select a;

                return query.ToFullyLoaded().Take(1000).OrderBy(x => x.GL_Description);
            }
        }


    }
}
