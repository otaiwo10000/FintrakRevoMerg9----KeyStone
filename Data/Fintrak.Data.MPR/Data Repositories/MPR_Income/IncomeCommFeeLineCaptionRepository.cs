using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeCommFeeLineCaptionRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeCommFeeLineCaptionRepository : DataRepositoryBase<IncomeCommFeeLineCaption>, IIncomeCommFeeLineCaptionRepository
    {

        protected override IncomeCommFeeLineCaption AddEntity(MPRContext entityContext, IncomeCommFeeLineCaption entity)
        {
            return entityContext.Set<IncomeCommFeeLineCaption>().Add(entity);
        }

        protected override IncomeCommFeeLineCaption UpdateEntity(MPRContext entityContext, IncomeCommFeeLineCaption entity)
        {
            return (from e in entityContext.Set<IncomeCommFeeLineCaption>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeCommFeeLineCaption> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<IncomeCommFeeLineCaption>()
                   select e).Take(1000).OrderBy(x => x.ID);
        }

        protected override IncomeCommFeeLineCaption GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeCommFeeLineCaption>()
                         where e.ID == id
                         select e); 

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IncomeCommFeeLineCaption> GetIncomeCommFeeLineCaptionBySearchValue(string searchvalue)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.IncomeCommFeeLineCaptionSet
                            where a.IncomeLineCapton.StartsWith(searchvalue.Trim()) || a.GLCode.StartsWith(searchvalue.Trim()) || a.GroupName.StartsWith(searchvalue.Trim())
                            select a;
                           
                return query.ToFullyLoaded().Take(1000).OrderBy(x => x.IncomeLineCapton);
            }
        }

       
    }
}
