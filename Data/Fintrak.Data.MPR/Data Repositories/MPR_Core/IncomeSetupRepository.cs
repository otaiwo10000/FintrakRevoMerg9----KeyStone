using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeSetupRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeSetupRepository : DataRepositoryBase<IncomeSetup>, IIncomeSetupRepository
    {

        protected override IncomeSetup AddEntity(MPRContext entityContext, IncomeSetup entity)
        {
            return entityContext.Set<IncomeSetup>().Add(entity);
        }

        protected override IncomeSetup UpdateEntity(MPRContext entityContext, IncomeSetup entity)
        {
            return (from e in entityContext.Set<IncomeSetup>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeSetup> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<IncomeSetup>()
                   select e;
        }

        protected override IncomeSetup GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeSetup>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IncomeSetup LatestIncomeSetUp()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = (from e in entityContext.Set<IncomeSetup>()
                             select e).OrderByDescending(x => new { x.Year, x.CurrentPeriod }).FirstOrDefault();

                //var results = query.FirstOrDefault();

                //return results;
                return query;
            }
        }

    }
}
