//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

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
    [Export(typeof(ITeamBankRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TeamBankRepository : DataRepositoryBase<TeamBank>, ITeamBankRepository
    {
        //team - branch
        //branch - division
        //region - region
        //division - directorate
        //accountofficer - accountofficer

        protected override TeamBank AddEntity(MPRContext entityContext, TeamBank entity)
        {
            return entityContext.Set<TeamBank>().Add(entity);
        }

        protected override TeamBank UpdateEntity(MPRContext entityContext, TeamBank entity)
        {
            return (from e in entityContext.Set<TeamBank>()
                    where e.TeamBankId == entity.TeamBankId
                    select e).FirstOrDefault();
        }

        protected override TeamBank GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TeamBank>()
                         where e.TeamBankId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override IEnumerable<TeamBank> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<TeamBank>()
                    select e).Take(50);
        }

        public IEnumerable<TeamBank> GetTeamBanksBySearchValue(string searchvalue, int year)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = (from a in entityContext.TeamBankSet
                                 //where a.TeamName.Contains(SearchValue) || a.Team_Code.Contains(SearchValue)
                             where 
                             (
                             a.TeamName.StartsWith(searchvalue.Trim()) || a.Team_Code.StartsWith(searchvalue.Trim()) ||
                             a.BranchName.StartsWith(searchvalue.Trim()) || a.Branch_Code.StartsWith(searchvalue.Trim()) ||
                             a.RegionName.StartsWith(searchvalue.Trim()) || a.Region_Code.StartsWith(searchvalue.Trim()) ||
                             a.DivisionName.StartsWith(searchvalue.Trim()) || a.Division_Code.StartsWith(searchvalue.Trim()) ||
                             a.AccountOfficer_Code.StartsWith(searchvalue.Trim()) || a.AccountOfficer_Code.StartsWith(searchvalue.Trim()) ||
                             a.StaffID.StartsWith(searchvalue.Trim())
                             )
                             && a.Year == year
                             select a);

                return query.ToFullyLoaded();
            }
        }


        //NOTE: The team definition codes are hard coded here because they are not mapped/referenced in the teanstructure table.
        public IEnumerable<TeamBank> GetTeamBankItemsByYear(string code)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                int maxyear = entityContext.TeamBankSet.Max(x => x.Year);
       
                var query = new List<TeamBank>();

                if (code == "TEM")
                {
                    query = (from a in entityContext.TeamBankSet
                             where a.Year == maxyear

                             select new
                             {
                                 Team_Code = a.Team_Code,
                                 TeamName = a.TeamName
                             })
                            .AsEnumerable().Select(x => new TeamBank
                            {
                                Team_Code = x.Team_Code,
                                TeamName = x.TeamName
                            })
                            .GroupBy(x => x.Team_Code).Select(o => o.FirstOrDefault())
                            .OrderBy(x => x.TeamName)
                           .ToList();
                }

                else if (code == "ACCT")
                {
                    query = (from a in entityContext.TeamBankSet
                             where a.Year == maxyear

                             select new
                             {
                                 AccountOfficer_Code = a.AccountOfficer_Code,
                                 AccountOfficer_Name = a.AccountOfficer_Name
                             })
                            .AsEnumerable().Select(x => new TeamBank
                            {
                                AccountOfficer_Code = x.AccountOfficer_Code,
                                AccountOfficer_Name = x.AccountOfficer_Name
                            })
                            .GroupBy(x => x.AccountOfficer_Code).Select(o => o.FirstOrDefault())
                            .OrderBy(x => x.AccountOfficer_Name)
                           .ToList();
                }

                else if (code == "BRH")
                {
                    query = (from a in entityContext.TeamBankSet
                             where a.Year == maxyear

                             select new
                             {
                                 Branch_Code = a.Branch_Code,
                                 BranchName = a.BranchName
                             })
                            .AsEnumerable().Select(x => new TeamBank
                            {
                                Branch_Code = x.Branch_Code,
                                BranchName = x.BranchName
                            })
                            .GroupBy(x => x.Branch_Code).Select(o => o.FirstOrDefault())
                            .OrderBy(x => x.BranchName)
                           .ToList();
                }

                else if (code == "DIV")
                {
                    query = (from a in entityContext.TeamBankSet
                             where a.Year == maxyear

                             select new
                             {
                                 Division_Code = a.Division_Code,
                                 DivisionName = a.DivisionName
                             })
                            .AsEnumerable().Select(x => new TeamBank
                            {
                                Division_Code = x.Division_Code,
                                DivisionName = x.DivisionName
                            })
                            .GroupBy(x => x.Division_Code).Select(o => o.FirstOrDefault())
                            .OrderBy(x => x.DivisionName)
                           .ToList();
                }

                else if (code == "REG")
                {
                    query = (from a in entityContext.TeamBankSet
                             where a.Year == maxyear

                             select new
                             {
                                 Region_Code = a.Region_Code,
                                 RegionName = a.RegionName
                             })
                            .AsEnumerable().Select(x => new TeamBank
                            {
                                Region_Code = x.Region_Code,
                                RegionName = x.RegionName
                            })
                            .GroupBy(x => x.Region_Code).Select(o => o.FirstOrDefault())
                            .OrderBy(x => x.RegionName)
                           .ToList();
                }

                return query;
            }
        }


        ////public IEnumerable<TeamStructure> GetTeamstructureByDefinitionCode(string code)
        ////{
        ////    //var connectionString = GetDataConnection();

        ////    var teamstructures = new List<TeamStructure>();
        ////    using (var con = new System.Data.SqlClient.SqlConnection("FintrakDBConnection"))
        ////    {
        ////        var cmd = new System.Data.SqlClient.SqlCommand("spp_team_structure", con);
        ////        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        ////        cmd.CommandTimeout = 0;
        ////        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter
        ////        {
        ////            ParameterName = "cod",
        ////            Value = code,
        ////        });

        ////        con.Open();

        ////        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        ////        while (reader.Read())
        ////        {
        ////            var teamstructure = new TeamStructure();

        ////            if(code=="TEM")
        ////                teamstructure.Team_Code = reader["Team_Code"].ToString();
        ////                teamstructure.TeamName = reader["TeamName"].ToString();

        ////            if (code == "BRH")
        ////                teamstructure.Branch_Code = reader["Branch_Code"].ToString();
        ////            teamstructure.BranchName = reader["BranchName"].ToString();

        ////            if (code == "DIV")
        ////                teamstructure.Division_Code = reader["Division_Code"].ToString();
        ////            teamstructure.DivisionName = reader["DivisionName"].ToString();

        ////            if (code == "GRP")
        ////                teamstructure.Group_Code = reader["Group_Code"].ToString();
        ////            teamstructure.GroupName = reader["GroupName"].ToString();

        ////            if (code == "REG")
        ////                teamstructure.Region_Code = reader["Region_Code"].ToString();
        ////            teamstructure.RegionName = reader["RegionName"].ToString();

        ////            if (code == "DIR")
        ////                teamstructure.DIRECTORATECODE = reader["DIRECTORATECODE"].ToString();
        ////            teamstructure.DIRECTORATENAME = reader["DIRECTORATENAME"].ToString();

        ////            teamstructures.Add(teamstructure);
        ////        }

        ////        con.Close();
        ////    }

        ////    return teamstructures.ToArray();
        ////}


        ////public IEnumerable<TeamStructure> GetTeamstructureByParamsAndeSetUp(string code, string SearchValue)
        ////{
        ////    //var connectionString = GetDataConnection();

        ////    var accountofficers = new List<TeamStructure>();
        ////    using (var con = new System.Data.SqlClient.SqlConnection("FintrakDBConnection"))
        ////    {
        ////        var cmd = new System.Data.SqlClient.SqlCommand("spp_accountoffice", con);
        ////        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        ////        cmd.CommandTimeout = 0;
        ////        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter
        ////        {
        ////            ParameterName = "cod",
        ////            Value = code,
        ////        });

        ////        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter
        ////        {
        ////            ParameterName = "sValue",
        ////            Value = SearchValue,
        ////        });

        ////        con.Open();

        ////        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

        ////        while (reader.Read())
        ////        {
        ////            var acctofficer = new TeamStructure();

        ////            if (code == "ACCT")
        ////                acctofficer.Accountofficer_Code = reader["Accountofficer_Code"].ToString();
        ////            acctofficer.AccountofficerName = reader["AccountofficerName"].ToString();

        ////            accountofficers.Add(acctofficer);
        ////        }

        ////        con.Close();
        ////    }

        ////    return accountofficers.ToArray();
        ////}


        //////NOTE: The team definition codes are hard coded here because they are not mapped/referenced in the teanstructure table.
        //public IEnumerable<TeamStructure> GetTeamstructureByParamsAndeSetUp(string code, string SearchValue)
        //{
        //    using (MPRContext_2 entityContext = new MPRContext_2())
        //    {
        //        //string maxyear = entityContext.TeamStructureSet.Max(x => x.Year);
        //        //int maxperiod = entityContext.TeamStructureSet.Max(x => x.Period);

        //        string maxyear = entityContext.TeamStructureSet.Max(x => x.Year);
        //        List<int> periodsForLatestYear = entityContext.TeamStructureSet.Where(x => x.Year == maxyear).Select(x => x.Period).ToList();
        //        int maxperiod = periodsForLatestYear.Max();

        //        //var setup = GetSetUp();

        //        var query = new List<TeamStructure>();

        //        if (code == "ACCT")
        //        {
        //            query = (from a in entityContext.TeamStructureSet
        //                         //where a.Year == setup.Year && a.Period == setup.Period && (a.AccountofficerName.StartsWith(SearchValue.Trim()) || a.Accountofficer_Code.StartsWith(SearchValue.Trim()))
                             
        //                     where a.Year == maxyear && a.Period == maxperiod && (a.AccountofficerName.StartsWith(SearchValue.Trim()) || a.Accountofficer_Code.StartsWith(SearchValue.Trim()))

        //                     select new
        //                     {
        //                         Accountofficer_Code = a.Accountofficer_Code,
        //                         AccountofficerName = a.AccountofficerName
        //                     })
        //                    .AsEnumerable().Select(x => new TeamStructure
        //                    {
        //                        Accountofficer_Code = x.Accountofficer_Code,
        //                        AccountofficerName = x.AccountofficerName
        //                    })
        //                    .GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault())
        //                    .OrderBy(x => x.AccountofficerName)
        //                   .ToList();
        //        }

        //        return query;
        //    }
        //}

        ////public ActionResult AutoComplete1(string fname)
        ////{
        ////    List<string> st;          //for autocomplete
        ////    {
        ////        st = db.Staffs.Where(x => x.FirstName.StartsWith(fname)).Select(y => y.FirstName).ToList();
        ////        return Json(st, JsonRequestBehavior.AllowGet);
        ////    }
        ////}

    }
}




