using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IOneBankAORepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OneBankAORepository : DataRepositoryBase<OneBankAO>, IOneBankAORepository
    {

        protected override OneBankAO AddEntity(MPRContext entityContext, OneBankAO entity)
        {
            return entityContext.Set<OneBankAO>().Add(entity);
        }

        protected override OneBankAO UpdateEntity(MPRContext entityContext, OneBankAO entity)
        {
            return (from e in entityContext.Set<OneBankAO>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OneBankAO> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<OneBankAO>()
                   select e;
        }

        protected override OneBankAO GetEntity(MPRContext entityContext, int Id)
        {
            var query = (from e in entityContext.Set<OneBankAO>()
                         where e.Id == Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<OneBankAO> GetOneBankAOByParams(string SearchValue, int year, int period)
        {
            using (MPRContext entityContext = new MPRContext())
            {              
                var query = new List<OneBankAO>();

                    query = (from a in entityContext.OneBankAOSet
                             where a.Year == year && a.Period == period && (a.AccountOfficerCode.Contains(SearchValue.Trim()) || a.GradeLevel.Contains(SearchValue.Trim()))
                            select a).ToList();

                return query;
            }
        }

    }
}
