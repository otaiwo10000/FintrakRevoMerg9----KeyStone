using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.SqlClient;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ILoanPeriodicScheduleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoanPeriodicScheduleRepository : DataRepositoryBase<LoanPeriodicSchedule>, ILoanPeriodicScheduleRepository
    {

        protected override LoanPeriodicSchedule AddEntity(IFRSContext entityContext, LoanPeriodicSchedule entity)
        {
            return entityContext.Set<LoanPeriodicSchedule>().Add(entity);
        }

        protected override LoanPeriodicSchedule UpdateEntity(IFRSContext entityContext, LoanPeriodicSchedule entity)
        {
            return (from e in entityContext.Set<LoanPeriodicSchedule>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<LoanPeriodicSchedule> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LoanPeriodicSchedule>()
                   select e;
        }

        protected override LoanPeriodicSchedule GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LoanPeriodicSchedule>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<string> GetDistinctLoanPeriodicScheduleRefNos()
        {
            IFRSContext entityContext = new IFRSContext();

            var query = (entityContext.LoanPeriodicScheduleSet.Select<LoanPeriodicSchedule, string>(r => r.RefNo)).Distinct();

            return query.ToFullyLoaded();
        }

        public IEnumerable<LoanPeriodicSchedule> GetLoanPeriodicScheduleRefNos(string loanPeriodicScheduleRefNo)
        {
            IFRSContext entityContext = new IFRSContext();

            var query = entityContext.LoanPeriodicScheduleSet.AsQueryable().Where(r => r.RefNo == loanPeriodicScheduleRefNo);

            return query.ToFullyLoaded();
        }

        public List<LoanPeriodicSchedule> GetDistinctRefNo()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

            var connectionString = IFRSContext.GetDataConnection();

            var loanPeriodicSchedules = new List<LoanPeriodicSchedule>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("Get_Distinct_ifrs_loan_periodic_schedule", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var loanPeriodicSchedule = new LoanPeriodicSchedule();

                    if (reader["RefNo"] != DBNull.Value)
                        loanPeriodicSchedule.RefNo = reader["RefNo"].ToString();

                    loanPeriodicSchedules.Add(loanPeriodicSchedule);
                }

                con.Close();
            }

            return loanPeriodicSchedules;
        }

    }
}
