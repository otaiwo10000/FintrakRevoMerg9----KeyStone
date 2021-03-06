using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.Basic.Entities;
using Fintrak.Data.Basic.Contracts;

namespace Fintrak.Data.Basic
{
    [Export(typeof(IInstrumentTypeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class InstrumentTypeRepository : DataRepositoryBase<InstrumentType>, IInstrumentTypeRepository
    {

        protected override InstrumentType AddEntity(BasicContext entityContext, InstrumentType entity)
        {
            return entityContext.Set<InstrumentType>().Add(entity);
        }

        protected override InstrumentType UpdateEntity(BasicContext entityContext, InstrumentType entity)
        {
            return (from e in entityContext.Set<InstrumentType>() 
                    where e.InstrumentTypeId == entity.InstrumentTypeId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<InstrumentType> GetEntities(BasicContext entityContext)
        {
            return from e in entityContext.Set<InstrumentType>()
                   select e;
        }

        protected override InstrumentType GetEntity(BasicContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<InstrumentType>()
                         where e.InstrumentTypeId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<InstrumentTypeInfo> GetInstrumentTypes()
        {
            using (BasicContext entityContext = new BasicContext())
            {
                var query = from a in entityContext.InstrumentTypeSet
                            join b in entityContext.InstrumentTypeSet on a.ParentId equals b.InstrumentTypeId into parents
                            from pt in parents.DefaultIfEmpty()
                            select new InstrumentTypeInfo()
                            {
                                InstrumentType = a,
                                Parent = pt
                            };

                return query.ToFullyLoaded();
            }
        }
      
    }
}
