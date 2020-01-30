using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeCustomerRatingOverrideTEMPRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeCustomerRatingOverrideTEMPRepository : DataRepositoryBase<IncomeCustomerRatingOverrideTEMP>, IIncomeCustomerRatingOverrideTEMPRepository
    {

        protected override IncomeCustomerRatingOverrideTEMP AddEntity(MPRContext entityContext, IncomeCustomerRatingOverrideTEMP entity)
        {
            return entityContext.Set<IncomeCustomerRatingOverrideTEMP>().Add(entity);
        }

        protected override IncomeCustomerRatingOverrideTEMP UpdateEntity(MPRContext entityContext, IncomeCustomerRatingOverrideTEMP entity)
        {
            return (from e in entityContext.Set<IncomeCustomerRatingOverrideTEMP>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeCustomerRatingOverrideTEMP> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<IncomeCustomerRatingOverrideTEMP>()
                   select e).Take(2000);
        }

        protected override IncomeCustomerRatingOverrideTEMP GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeCustomerRatingOverrideTEMP>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IncomeCustomerRatingOverrideTEMP> GetOverrideByRefNumber(string refnumber)
        {
            using (MPRContext entityContext = new MPRContext())
            {
               
                    var query = (from a in entityContext.IncomeCustomerRatingOverrideTEMPSet

                                 where a.Ref_No.Trim().ToUpper().Contains(refnumber.Trim().ToUpper())

                             select a)
                             
                            //.OrderByDescending(x => x.Ref_No)
                           .ToList();

                return query;
            }
        }

        public IEnumerable<IncomeCustomerRatingOverrideTEMP> ValidateByRefNumber(string refnumber)
        {
            using (MPRContext entityContext = new MPRContext())
            {

                var query = (from a in entityContext.IncomeCustomerRatingOverrideTEMPSet

                             where a.Ref_No.Trim().ToUpper() == refnumber.Trim().ToUpper()

                             select a)

                        //.OrderByDescending(x => x.Ref_No)
                       .ToList();

                return query;
            }
        }


    }
}
