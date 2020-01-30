using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IMISNewOthersTEMPRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MISNewOthersTEMPRepository : DataRepositoryBase<MISNewOthersTEMP>, IMISNewOthersTEMPRepository
    {

        protected override MISNewOthersTEMP AddEntity(MPRContext entityContext, MISNewOthersTEMP entity)
        {
            return entityContext.Set<MISNewOthersTEMP>().Add(entity);
        }

        protected override MISNewOthersTEMP UpdateEntity(MPRContext entityContext, MISNewOthersTEMP entity)
        {
            return (from e in entityContext.Set<MISNewOthersTEMP>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<MISNewOthersTEMP> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<MISNewOthersTEMP>()
                   select e).Take(2000);
        }

        protected override MISNewOthersTEMP GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<MISNewOthersTEMP>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<MISNewOthersTEMP> GetMISNewOthersTEMPBySearchVal(string search)
        {
            using (MPRContext entityContext = new MPRContext())
            {

                var query = (from a in entityContext.MISNewOthersTEMPSet

                             where a.Accountofficer_code.Trim().ToUpper().Contains(search.Trim().ToUpper())
                             || a.old_mis.Trim().ToUpper().Contains(search.Trim().ToUpper())
                             || a.new_mis.Trim().ToUpper().Contains(search.Trim().ToUpper())
                             || a.State.Trim().ToUpper().Contains(search.Trim().ToUpper())
                             || a.Teamname.Trim().ToUpper().Contains(search.Trim().ToUpper())

                             select a)

                       // .OrderByDescending(x => x.accountnumber)
                       .ToList();

                return query;
            }
        }

    }
}
