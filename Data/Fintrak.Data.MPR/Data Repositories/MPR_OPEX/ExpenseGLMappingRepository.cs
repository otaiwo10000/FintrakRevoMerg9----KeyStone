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
    [Export(typeof(IExpenseGLMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ExpenseGLMappingRepository : DataRepositoryBase<ExpenseGLMapping>, IExpenseGLMappingRepository
    {

        protected override ExpenseGLMapping AddEntity(MPRContext entityContext, ExpenseGLMapping entity)
        {
            return entityContext.Set<ExpenseGLMapping>().Add(entity);
        }

        protected override ExpenseGLMapping UpdateEntity(MPRContext entityContext, ExpenseGLMapping entity)
        {
            return (from e in entityContext.Set<ExpenseGLMapping>()
                    where e.ExpenseGLId == entity.ExpenseGLId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ExpenseGLMapping> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<ExpenseGLMapping>()
                   select e;
        }

        protected override ExpenseGLMapping GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<ExpenseGLMapping>()
                         where e.ExpenseGLId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ExpenseGLMappingInfo> GetExpenseGLMappings()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.ExpenseGLMappingSet
                            join b in entityContext.ExpenseBasisSet on a.BasisCode equals b.Code into fg
                            from fgi in fg.DefaultIfEmpty()
                            join c in entityContext.GLDefinitionSet on a.GLCode equals c.GL_Code into fg1
                            from fgj in fg1.DefaultIfEmpty()
                            select new ExpenseGLMappingInfo()
                            {
                                ExpenseGLMapping = a,
                                ExpenseBasis = fgi,
                                GLDefinition = fgj
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}


//SELECT f.value
//FROM period as p 
//LEFT OUTER JOIN facts AS f ON p.id = f.periodid 
//WHERE p.companyid = 100 AND f.otherid = 17



//from p in context.Periods
//join f in context.Facts on p.id equals f.periodid into fg
//from fgi in fg.DefaultIfEmpty()
//where p.companyid == 100 && fgi.otherid == 17
//select f.value