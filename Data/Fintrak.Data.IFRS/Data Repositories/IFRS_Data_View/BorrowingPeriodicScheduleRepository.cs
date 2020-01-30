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
    [Export(typeof(IBorrowingPeriodicScheduleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BorrowingPeriodicScheduleRepository : DataRepositoryBase<BorrowingPeriodicSchedule>, IBorrowingPeriodicScheduleRepository
    {

        protected override BorrowingPeriodicSchedule AddEntity(IFRSContext entityContext, BorrowingPeriodicSchedule entity)
        {
            return entityContext.Set<BorrowingPeriodicSchedule>().Add(entity);
        }

        protected override BorrowingPeriodicSchedule UpdateEntity(IFRSContext entityContext, BorrowingPeriodicSchedule entity)
        {
            return (from e in entityContext.Set<BorrowingPeriodicSchedule>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<BorrowingPeriodicSchedule> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<BorrowingPeriodicSchedule>()
                   select e;
        }

        protected override BorrowingPeriodicSchedule GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BorrowingPeriodicSchedule>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<string> GetDistinctBorrowingPeriodicScheduleRefNos()
        {
            IFRSContext entityContext = new IFRSContext();

            var query = (entityContext.BorrowingPeriodicScheduleSet.Select<BorrowingPeriodicSchedule, string>(r => r.RefNo)).Distinct();

            return query.ToFullyLoaded();
        }

        public IEnumerable<BorrowingPeriodicSchedule> GetBorrowingPeriodicScheduleRefNos(string borrowingPeriodicScheduleRefNo)
        {
            IFRSContext entityContext = new IFRSContext();

            var query = entityContext.BorrowingPeriodicScheduleSet.AsQueryable().Where(r => r.RefNo == borrowingPeriodicScheduleRefNo);

            return query.ToFullyLoaded();
        }

        public List<BorrowingPeriodicSchedule> GetDistinctRefNo()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

            var connectionString = IFRSContext.GetDataConnection();

            var borrowingPeriodicSchedules = new List<BorrowingPeriodicSchedule>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("Get_Distinct_ifrs_borrowing_periodic_schedule", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var borrowingPeriodicSchedule = new BorrowingPeriodicSchedule();

                    if (reader["RefNo"] != DBNull.Value)
                        borrowingPeriodicSchedule.RefNo = reader["RefNo"].ToString();

                    borrowingPeriodicSchedules.Add(borrowingPeriodicSchedule);
                }

                con.Close();
            }

            return borrowingPeriodicSchedules;
        }

    }
}
