
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
    [Export(typeof(ICrb_DataRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class crb_DataRepository : DataRepositoryBase<crb_Data>, ICrb_DataRepository
    {
        protected override crb_Data AddEntity(MPRContext entityContext, crb_Data entity)
        {
            return entityContext.Set<crb_Data>().Add(entity);
        }

        protected override crb_Data UpdateEntity(MPRContext entityContext, crb_Data entity)
        {
            return (from e in entityContext.Set<crb_Data>()
                    where e.crb_Data_Id == entity.crb_Data_Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<crb_Data> GetEntities(MPRContext entityContext)
        {
            return from e in entityContext.Set<crb_Data>()
                   select e;
        }

        protected override crb_Data GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<crb_Data>()
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        //public IEnumerable<crb_Data> GetAllcrb_Data()
        //{
        //    using (MPRContext entityContext = new MPRContext())
        //    {
        //        var query = (from e in entityContext.Set<crb_Data>()
        //                     select e);

        //        return query;
        //    }
        //}

        
    }
}
