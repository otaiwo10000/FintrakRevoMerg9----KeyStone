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
    [Export(typeof(IBorrowingScheduleRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BorrowingScheduleRepository : DataRepositoryBase<BorrowingSchedule>, IBorrowingScheduleRepository
    {

        protected override BorrowingSchedule AddEntity(IFRSContext entityContext, BorrowingSchedule entity)
        {
            return entityContext.Set<BorrowingSchedule>().Add(entity);
        }

        protected override BorrowingSchedule UpdateEntity(IFRSContext entityContext, BorrowingSchedule entity)
        {
            return (from e in entityContext.Set<BorrowingSchedule>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<BorrowingSchedule> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<BorrowingSchedule>()
                   select e;
        }

        protected override BorrowingSchedule GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BorrowingSchedule>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<string> GetDistinctBorrowingScheduleRefNos()
        {
            IFRSContext entityContext = new IFRSContext();

            var query = (entityContext.BorrowingScheduleSet.Select<BorrowingSchedule, string>(r => r.RefNo)).Distinct();

            return query.ToFullyLoaded();
        }

        public IEnumerable<BorrowingSchedule> GetBorrowingScheduleRefNos(string borrowingScheduleRefNo)
        {
            IFRSContext entityContext = new IFRSContext();

            var query = entityContext.BorrowingScheduleSet.AsQueryable().Where(r => r.RefNo == borrowingScheduleRefNo);

            return query.ToFullyLoaded();
        }


        public List<BorrowingSchedule> GetDistinctRefNo()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

            var connectionString = IFRSContext.GetDataConnection();

            var BorrowingSchedules = new List<BorrowingSchedule>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("Get_Distinct_ifrs_borrowing_schedule", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
               

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var BorrowingSchedule = new BorrowingSchedule();

                    if (reader["RefNo"] != DBNull.Value)
                        BorrowingSchedule.RefNo = reader["RefNo"].ToString();

                    BorrowingSchedules.Add(BorrowingSchedule);
                }

                con.Close();
            }

            return BorrowingSchedules;
        }

    }
}
