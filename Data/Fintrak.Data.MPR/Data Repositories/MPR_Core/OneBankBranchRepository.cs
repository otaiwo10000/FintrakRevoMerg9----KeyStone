using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IOneBankBranchRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OneBankBranchRepository : DataRepositoryBase<OneBankBranch>, IOneBankBranchRepository
    {

        protected override OneBankBranch AddEntity(MPRContext entityContext, OneBankBranch entity)
        {
            return entityContext.Set<OneBankBranch>().Add(entity);
        }

        protected override OneBankBranch UpdateEntity(MPRContext entityContext, OneBankBranch entity)
        {
            return (from e in entityContext.Set<OneBankBranch>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<OneBankBranch> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<OneBankBranch>()
                   select e;
        }

        protected override OneBankBranch GetEntity(MPRContext entityContext, int Id)
        {
            var query = (from e in entityContext.Set<OneBankBranch>()
                         where e.Id == Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<OneBankBranch> GetOneBankBranchByParams(string SearchValue, int year, int period)
        {
            using (MPRContext entityContext = new MPRContext())
            {              
                var query = new List<OneBankBranch>();

                    query = (from a in entityContext.OneBankBranchSet
                             where a.Year == year && a.Period == period && (a.StaffName.Contains(SearchValue.Trim()) || a.BRANCH_CODE.Contains(SearchValue.Trim()) || a.GradeLevel.Contains(SearchValue.Trim()))
                            select a).ToList();

                return query;
            }
        }

    }
}
