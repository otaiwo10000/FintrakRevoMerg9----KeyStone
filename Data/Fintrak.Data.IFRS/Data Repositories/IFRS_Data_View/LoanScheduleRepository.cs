using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ILoanScheduleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoanScheduleRepository : DataRepositoryBase<LoanSchedule>, ILoanScheduleRepository
    {

        protected override LoanSchedule AddEntity(IFRSContext entityContext, LoanSchedule entity)
        {
            return entityContext.Set<LoanSchedule>().Add(entity);
        }

        protected override LoanSchedule UpdateEntity(IFRSContext entityContext, LoanSchedule entity)
        {
            return (from e in entityContext.Set<LoanSchedule>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<LoanSchedule> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LoanSchedule>()
                   select e;
        }

        protected override LoanSchedule GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LoanSchedule>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<string> GetDistinctLoanScheduleRefNos()
        {
            IFRSContext entityContext = new IFRSContext();

            var query = (entityContext.LoanScheduleSet.Select<LoanSchedule, string>(r => r.RefNo)).Distinct();

            return query.ToFullyLoaded();
        }

        public IEnumerable<LoanSchedule> GetLoanScheduleRefNos(string loanScheduleRefNo)
        {
            IFRSContext entityContext = new IFRSContext();

            var query = entityContext.LoanScheduleSet.AsQueryable().Where(r => r.RefNo == loanScheduleRefNo);

            return query.ToFullyLoaded();
        }


        public List<LoanSchedule> GetDistinctRefNo()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

            var connectionString = IFRSContext.GetDataConnection();

            var LoanSchedules = new List<LoanSchedule>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("Get_Distinct_ifrs_loan_schedule", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
               

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var LoanSchedule = new LoanSchedule();

                    if (reader["RefNo"] != DBNull.Value)
                        LoanSchedule.RefNo = reader["RefNo"].ToString();

                    LoanSchedules.Add(LoanSchedule);
                }

                con.Close();
            }

            return LoanSchedules;
        }

    }
}
