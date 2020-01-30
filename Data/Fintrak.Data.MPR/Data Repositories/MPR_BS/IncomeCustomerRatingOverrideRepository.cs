using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeCustomerRatingOverrideRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeCustomerRatingOverrideRepository : DataRepositoryBase<IncomeCustomerRatingOverride>, IIncomeCustomerRatingOverrideRepository
    {

        protected override IncomeCustomerRatingOverride AddEntity(MPRContext entityContext, IncomeCustomerRatingOverride entity)
        {
            return entityContext.Set<IncomeCustomerRatingOverride>().Add(entity);
        }

        protected override IncomeCustomerRatingOverride UpdateEntity(MPRContext entityContext, IncomeCustomerRatingOverride entity)
        {
            return (from e in entityContext.Set<IncomeCustomerRatingOverride>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeCustomerRatingOverride> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<IncomeCustomerRatingOverride>()
                   select e).Take(500);
        }

        protected override IncomeCustomerRatingOverride GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeCustomerRatingOverride>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IncomeCustomerRatingOverride> GetOverrideByRefNumber(string refnumber)
        {
            using (MPRContext entityContext = new MPRContext())
            {
               
                    var query = (from a in entityContext.IncomeCustomerRatingOverrideSet

                                 where a.Ref_No.Trim().ToUpper().Contains(refnumber.Trim().ToUpper())

                             select a)
                             
                            .OrderByDescending(x => x.Ref_No)
                           .ToList();

                return query;
            }
        }

        public IEnumerable<IncomeCustomerRatingOverride> ValidateByRefNumber(string refnumber)
        {
            using (MPRContext entityContext = new MPRContext())
            {

                var query = (from a in entityContext.IncomeCustomerRatingOverrideSet

                             where a.Ref_No.Trim().ToUpper() == refnumber.Trim().ToUpper()

                             select a)

                        .OrderByDescending(x => x.Ref_No)
                       .ToList();

                return query;
            }
        }


    }
}
