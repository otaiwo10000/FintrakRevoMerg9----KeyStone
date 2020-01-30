using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IBondPeriodicScheduleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BondPeriodicScheduleRepository : DataRepositoryBase<BondPeriodicSchedule>, IBondPeriodicScheduleRepository
    {

        protected override BondPeriodicSchedule AddEntity(IFRSContext entityContext, BondPeriodicSchedule entity)
        {
            return entityContext.Set<BondPeriodicSchedule>().Add(entity);
        }

        protected override BondPeriodicSchedule UpdateEntity(IFRSContext entityContext, BondPeriodicSchedule entity)
        {
            return (from e in entityContext.Set<BondPeriodicSchedule>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<BondPeriodicSchedule> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<BondPeriodicSchedule>()
                   select e;
        }

        protected override BondPeriodicSchedule GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BondPeriodicSchedule>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<string> GetDistinctBondPeriodicScheduleRefNos()
        {
            IFRSContext entityContext = new IFRSContext();

            var query = (entityContext.BondPeriodicScheduleSet.Select<BondPeriodicSchedule, string>(r => r.RefNo)).Distinct();

            return query.ToFullyLoaded(); 
        }

        public IEnumerable<BondPeriodicSchedule> GetBondPeriodicScheduleRefNos(string refNo)
        {
            IFRSContext entityContext = new IFRSContext();

            var query = entityContext.BondPeriodicScheduleSet.AsQueryable().Where(r => r.RefNo == refNo);

            return query.ToFullyLoaded();
        }

    }
}
