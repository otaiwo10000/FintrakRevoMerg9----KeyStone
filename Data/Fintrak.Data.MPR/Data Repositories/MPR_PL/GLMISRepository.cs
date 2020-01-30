using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{

    [Export(typeof(IGLMISRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GLMISRepository : DataRepositoryBase<GLMIS>, IGLMISRepository
    {

        protected override GLMIS AddEntity(MPRContext entityContext, GLMIS entity)
        {
            return entityContext.Set<GLMIS>().Add(entity);
        }

        protected override GLMIS UpdateEntity(MPRContext entityContext, GLMIS entity)
        {
            return (from e in entityContext.Set<GLMIS>() 
                    where e.GlMisId == entity.GlMisId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<GLMIS> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<GLMIS>()
                   select e;
        }

        protected override GLMIS GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<GLMIS>()
                         where e.GlMisId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<GLMISInfo> GetGLMIS()
        {
            MPRContext entityContext_Setup = new MPRContext();

            var _Setup = (from ty in entityContext_Setup.SetUpSet
                          select new MPRSetUp() { Period = ty.Period, Year = ty.Year }).FirstOrDefault();

            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.GLMISSet
                            join c in entityContext.TeamDefinitionSet on a.TeamDefinitionCode equals c.Code
                            join d in entityContext.TeamSet on a.TeamCode equals d.Code into teams
                            from t in teams.Where(e => (c.Year == e.Year)).DefaultIfEmpty()
                            join e in entityContext.TeamDefinitionSet on a.AccountOfficerDefinitionCode equals e.Code
                            join f in entityContext.TeamSet on a.AccountOfficerCode equals f.Code into officers
                            from o in officers.Where(q => q.Year == e.Year).DefaultIfEmpty()
                            where (c.Period == _Setup.Period) && (t.Period == _Setup.Period) && (e.Period == _Setup.Period) && (o.Period == _Setup.Period) && (c.Year == _Setup.Year) && (t.Year == _Setup.Year) && (e.Year == _Setup.Year) && (o.Year == _Setup.Year)
                            select new GLMISInfo()
                            {
                                GLMIS = a,
                                TeamDefinition = c,
                                Team = t,
                                AccountOfficerDefinition = e,
                                AccountOfficer = o
                            };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<GLMISInfo> GetGLMIS(string year)
        {

            MPRContext entityContext_Setup = new MPRContext();

            var _Setup = (from ty in entityContext_Setup.SetUpSet.Where(c => c.Year == year)
                          select new MPRSetUp() { Period = ty.Period, Year = ty.Year }).FirstOrDefault();

            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.GLMISSet
                            join c in entityContext.TeamDefinitionSet on a.TeamDefinitionCode equals c.Code
                            where c.Year == year
                            join d in entityContext.TeamSet on a.TeamCode equals d.Code into teams
                            from t in teams.Where(e => (c.Year == e.Year)).DefaultIfEmpty()
                            join e in entityContext.TeamDefinitionSet on a.AccountOfficerDefinitionCode equals e.Code
                            where e.Year == year
                            join f in entityContext.TeamSet on a.AccountOfficerCode equals f.Code into officers
                            from o in officers.Where(q => (q.Year == e.Year)).DefaultIfEmpty()
                            where c.Year == year && t.Year == year && e.Year == year && o.Year == year &&
                                  c.Period == _Setup.Period && t.Period == _Setup.Period && e.Period == _Setup.Period && o.Period == _Setup.Period

                            select new GLMISInfo()
                            {
                                GLMIS = a,
                                TeamDefinition = c,
                                Team = t,
                                AccountOfficerDefinition = e,
                                AccountOfficer = o
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
