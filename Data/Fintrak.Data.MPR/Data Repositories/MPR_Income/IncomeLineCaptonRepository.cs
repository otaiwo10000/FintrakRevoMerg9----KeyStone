using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeLineCaptonRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeLineCaptonRepository : DataRepositoryBase<IncomeLineCapton>, IIncomeLineCaptonRepository
    {

        protected override IncomeLineCapton AddEntity(MPRContext entityContext, IncomeLineCapton entity)
        {
            return entityContext.Set<IncomeLineCapton>().Add(entity);
        }

        protected override IncomeLineCapton UpdateEntity(MPRContext entityContext, IncomeLineCapton entity)
        {
            return (from e in entityContext.Set<IncomeLineCapton>()
                    where e.IncomeLineCaptonId == entity.EntityId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeLineCapton> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<IncomeLineCapton>()
                   select e).OrderBy(x => x.Name);
        }

        protected override IncomeLineCapton GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeLineCapton>()
                         where e.IncomeLineCaptonId == id
                         select e); 

            var results = query.FirstOrDefault();

            return results;
        }
        
       
    }
}
