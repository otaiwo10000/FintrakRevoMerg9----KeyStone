using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.SqlClient;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;
using Fintrak.Presentation.WebClient.Models;


namespace Fintrak.Data.IFRS
{
    [Export(typeof(IBondComputationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BondComputationRepository : DataRepositoryBase<BondComputation>, IBondComputationRepository
    {

        protected override BondComputation AddEntity(IFRSContext entityContext, BondComputation entity)
        {
            return entityContext.Set<BondComputation>().Add(entity);
        }

        protected override BondComputation UpdateEntity(IFRSContext entityContext, BondComputation entity)
        {
            return (from e in entityContext.Set<BondComputation>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<BondComputation> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<BondComputation>()
                   select e;
        }

        protected override BondComputation GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<BondComputation>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<string> GetDistinctBondComputationRefNos()
        {
            IFRSContext entityContext = new IFRSContext();

            var query = (entityContext.BondComputationSet.Select<BondComputation, string>(r => r.RefNo)).Distinct();

            return query.ToFullyLoaded();
        }

        public IEnumerable<BondComputation> GetBondPeriodicScheduleRefNos(string bondComputationRefNo)
        {
            IFRSContext entityContext = new IFRSContext();

            var query = entityContext.BondComputationSet.AsQueryable().Where(r => r.RefNo == bondComputationRefNo);

            return query.ToFullyLoaded();
        }

        public List<BondComputation> GetDistinctRefNo()
      //  public string[] GetDistinctRefNo()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

            var connectionString = IFRSContext.GetDataConnection();

           var BondComputations = new List<BondComputation>();
            //List<string> refno;
            //var refnoList = new List<string>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("Get_Distinct_ifrs_bond_computation_result", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();
                // SqlDataReader reader = cmd.ExecuteReader();
                //{
                //    while (reader.Read())
                //    {
                //        var myRefNo = new ReferenceNoModel();
                //        if (reader["RefNo"] != DBNull.Value)
                //            myRefNo.RefNo = reader["RefNo"].ToString();
                //        refnoList.Add(myRefNo.RefNo);
                //    }
                //    reader.Close();
                //    con.Close();
                //}

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var BondComputation = new BondComputation();

                    if (reader["RefNo"] != DBNull.Value)
                        BondComputation.RefNo = reader["RefNo"].ToString();

                    BondComputations.Add(BondComputation);
                }
                con.Close();
            }
          //  return refnoList.ToArray();

            return BondComputations;
        }


    }
}
