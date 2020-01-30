using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeBranchesRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeBranchesRepository : DataRepositoryBase<IncomeBranches>, IIncomeBranchesRepository
    {

        protected override IncomeBranches AddEntity(MPRContext entityContext, IncomeBranches entity)
        {
            return entityContext.Set<IncomeBranches>().Add(entity);
        }

        protected override IncomeBranches UpdateEntity(MPRContext entityContext, IncomeBranches entity)
        {
            return (from e in entityContext.Set<IncomeBranches>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeBranches> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<IncomeBranches>()
                   select e;
        }

        protected override IncomeBranches GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeBranches>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

      
    }
}
