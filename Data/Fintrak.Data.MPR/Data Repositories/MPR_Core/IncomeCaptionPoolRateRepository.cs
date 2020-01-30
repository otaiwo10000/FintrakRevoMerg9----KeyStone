using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeCaptionPoolRateRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeCaptionPoolRateRepository : DataRepositoryBase<IncomeCaptionPoolRate>, IIncomeCaptionPoolRateRepository
    {

        protected override IncomeCaptionPoolRate AddEntity(MPRContext entityContext, IncomeCaptionPoolRate entity)
        {
            return entityContext.Set<IncomeCaptionPoolRate>().Add(entity);
        }

        protected override IncomeCaptionPoolRate UpdateEntity(MPRContext entityContext, IncomeCaptionPoolRate entity)
        {
            return (from e in entityContext.Set<IncomeCaptionPoolRate>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeCaptionPoolRate> GetEntities(MPRContext entityContext)
        {
            var query =  from e in entityContext.Set<IncomeCaptionPoolRate>()
                   select e;

            return query.ToFullyLoaded().Take(500);
        }

        protected override IncomeCaptionPoolRate GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeCaptionPoolRate>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IncomeCaptionPoolRate> GetIncomeCaptionPoolRateBySearchValue(string searchvalue)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.IncomeCaptionPoolRateSet
                            where a.Caption.ToLower().Trim().Contains(searchvalue.ToLower().Trim())
                            select a;

                return query.ToFullyLoaded().Take(1000);
            }
        }

        public IncomeCaptionPoolRate ValidateIncomeCaptionPoolRate(string caption, int year)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.IncomeCaptionPoolRateSet
                            where a.Caption.ToLower().Trim() == caption.ToLower().Trim() && a.Year == year
                            select a;

                return query.FirstOrDefault();
            }
        }

    }
}
