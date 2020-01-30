using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Data.MPR.Contracts;
using System.Configuration;
using System.Data.SqlClient;

namespace Fintrak.Data.MPR
{
    [Export(typeof(IIncomeAdjustmentCommFeeSearchManualRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IncomeAdjustmentCommFeeSearchManualRepository : DataRepositoryBase<IncomeAdjustmentCommFeeSearchManual>, IIncomeAdjustmentCommFeeSearchManualRepository
    {

        protected override IncomeAdjustmentCommFeeSearchManual AddEntity(MPRContext entityContext, IncomeAdjustmentCommFeeSearchManual entity)
        {
            return entityContext.Set<IncomeAdjustmentCommFeeSearchManual>().Add(entity);
        }

        protected override IncomeAdjustmentCommFeeSearchManual UpdateEntity(MPRContext entityContext, IncomeAdjustmentCommFeeSearchManual entity)
        {
            return (from e in entityContext.Set<IncomeAdjustmentCommFeeSearchManual>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IncomeAdjustmentCommFeeSearchManual> GetEntities(MPRContext entityContext)
        {
            var currentsetup = entityContext.IncomeSetupSet.FirstOrDefault();
            int currentyear = currentsetup.Year;
            int currentperiod = currentsetup.CurrentPeriod;

            var query = (from e in entityContext.Set<IncomeAdjustmentCommFeeSearchManual>()
                         where e.Year == currentyear && e.Period == currentperiod
                         select e);
            var results = query;
            return results;
        }

        protected override IncomeAdjustmentCommFeeSearchManual GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<IncomeAdjustmentCommFeeSearchManual>()
                         where e.ID == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<IncomeAdjustmentCommFeeSearchManual> GetCommFeesByYearPeriod(int year, int period, string search)
        {
            search = search.Replace("FORWARDSLASHXTER", "/");
            search = search.Replace("DOTXTER", ".");

            using (MPRContext entityContext = new MPRContext())
            {
                var query = from a in entityContext.IncomeAdjustmentCommFeeSearchManualSet
                            where a.Year ==year && a.Period == period &&
                            (a.AccountOfficer_Code.Contains(search) || a.BranchCode.Contains(search) || a.Caption.Contains(search) ||
                            a.CustomerName.Contains(search) || a.CustomerCode.Contains(search) || a.GLName.Contains(search) ||
                            a.GL_Code.Contains(search) || a.MIS_Code.Contains(search) || a.ProductCode.Contains(search))
                            select a;

                return query.ToFullyLoaded();

                //if (number == 0)
                //    return query.ToFullyLoaded();

                //return query.ToFullyLoaded().Take(number);
            }
        }



    }
}
