

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Data.MPR
{
    
    [Export(typeof(IAccount_interestRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class account_interestRepository : DataRepositoryBase<account_interest>, IAccount_interestRepository
    {
        protected override account_interest AddEntity(MPRContext entityContext, account_interest entity)
        {
            return entityContext.Set<account_interest>().Add(entity);
        }

        protected override account_interest UpdateEntity(MPRContext entityContext, account_interest entity)
        {
            return (from e in entityContext.Set<account_interest>()
                    where e.account_interest_Id == entity.account_interest_Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<account_interest> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<account_interest>()
                   select e;
        }

        protected override account_interest GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<account_interest>()
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
