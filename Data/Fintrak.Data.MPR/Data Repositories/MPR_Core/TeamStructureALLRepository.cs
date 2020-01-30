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
    [Export(typeof(ITeamStructureALLRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TeamStructureALLRepository : DataRepositoryBase<TeamStructureALL>, ITeamStructureALLRepository
    {
        protected override TeamStructureALL AddEntity(MPRContext entityContext, TeamStructureALL entity)
        {
            return entityContext.Set<TeamStructureALL>().Add(entity);
        }

        protected override TeamStructureALL UpdateEntity(MPRContext entityContext, TeamStructureALL entity)
        {
            return (from e in entityContext.Set<TeamStructureALL>()
                    where e.Team_StructureId == entity.Team_StructureId
                    select e).FirstOrDefault();
        }

        protected override TeamStructureALL GetEntity(MPRContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<TeamStructureALL>()
                         where e.Team_StructureId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        protected override IEnumerable<TeamStructureALL> GetEntities(MPRContext entityContext)
        {
            return (from e in entityContext.Set<TeamStructureALL>()
                    select e).Take(50);
        }

        //public IEnumerable<TeamStructureALL> GetTeamStructureALLByParams(string SearchValue, string year)
        public IEnumerable<TeamStructureALL> GetTeamStructureALLByParams(string SearchValue, int year)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var query = (from a in entityContext.TeamStructureALLSet
                                 //where a.TeamName.Contains(SearchValue) || a.Team_Code.Contains(SearchValue)
                                 //where (a.TeamName.StartsWith(SearchValue.Trim()) || a.Team_Code.StartsWith(SearchValue.Trim()))
                              where
                                (a.SuperSegmentName.Contains(SearchValue.Trim()) || a.SuperSegment_Code.Contains(SearchValue.Trim()) ||
                               a.SegmentName.Contains(SearchValue.Trim()) || a.Segment_Code.Contains(SearchValue.Trim()) ||
                                a.DIRECTORATENAME.Contains(SearchValue.Trim()) || a.DIRECTORATECODE.Contains(SearchValue.Trim()) ||
                               a.RegionName.Contains(SearchValue.Trim()) || a.Region_Code.Contains(SearchValue.Trim()) ||
                               a.DivisionName.Contains(SearchValue.Trim()) || a.Division_Code.Contains(SearchValue.Trim()) ||
                               a.ZoneName.Contains(SearchValue.Trim()) || a.Zone_Code.Contains(SearchValue.Trim()) ||
                               a.BranchName.Contains(SearchValue.Trim()) || a.Branch_Code.Contains(SearchValue.Trim()) ||
                               a.TeamName.Contains(SearchValue.Trim()) || a.Team_Code.Contains(SearchValue.Trim()) ||
                               a.GroupName.Contains(SearchValue.Trim()) || a.Group_Code.Contains(SearchValue.Trim()) ||
                               a.unitname.Contains(SearchValue.Trim()) || a.unit_code.Contains(SearchValue.Trim()) ||
                               a.AccountofficerName.Contains(SearchValue.Trim()) || a.Accountofficer_Code.Contains(SearchValue.Trim())
                             )
                              && a.Year == year
                             //select a).Take(50);
                            select a);

                return query.ToFullyLoaded();
            }
        }

        //public IEnumerable<TeamStructureALL> GetTeamStructureALLByParameters(string selectedDefinitionCode, string SearchValue, string year)
        public IEnumerable<TeamStructureALL> GetTeamStructureALLByParameters(string selectedDefinitionCode, string SearchValue, int year)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                List<TeamStructureALL> query = new List<TeamStructureALL>();

                switch(selectedDefinitionCode.ToUpper().Trim())
                {
                    case "SSG":
                        query = (from a in entityContext.TeamStructureALLSet
                                 where (a.SuperSegmentName.Contains(SearchValue.Trim()) || a.SuperSegment_Code.Contains(SearchValue.Trim())) && a.Year == year
                                 select a).ToList();
                        break;

                    case "SEG":
                        query = (from a in entityContext.TeamStructureALLSet
                                 where (a.SegmentName.Contains(SearchValue.Trim()) || a.Segment_Code.Contains(SearchValue.Trim())) && a.Year == year
                                 select a).ToList();
                        break;

                    case "DIR":
                        query = (from a in entityContext.TeamStructureALLSet
                                 where (a.DIRECTORATENAME.Contains(SearchValue.Trim()) || a.DIRECTORATECODE.Contains(SearchValue.Trim())) && a.Year == year
                                 select a).ToList();
                        break;

                    case "REG":
                        query = (from a in entityContext.TeamStructureALLSet
                                 where (a.RegionName.Contains(SearchValue.Trim()) || a.Region_Code.Contains(SearchValue.Trim())) && a.Year == year
                                 select a).ToList();
                        break;

                    case "DIV":
                        query = (from a in entityContext.TeamStructureALLSet
                                 where (a.DivisionName.Contains(SearchValue.Trim()) || a.Division_Code.Contains(SearchValue.Trim())) && a.Year == year
                                 select a).ToList();
                        break;

                    case "ZON":
                        query = (from a in entityContext.TeamStructureALLSet
                                 where (a.ZoneName.Contains(SearchValue.Trim()) || a.Zone_Code.Contains(SearchValue.Trim())) && a.Year == year
                                 select a).ToList();
                        break;

                    case "BRH":
                        query = (from a in entityContext.TeamStructureALLSet
                                 where (a.BranchName.Contains(SearchValue.Trim()) || a.Branch_Code.Contains(SearchValue.Trim())) && a.Year == year
                                 select a).ToList();
                        break;

                    case "TEM":
                        query = (from a in entityContext.TeamStructureALLSet
                                 where (a.TeamName.Contains(SearchValue.Trim()) || a.Team_Code.Contains(SearchValue.Trim())) && a.Year == year
                                 select a).ToList();
                        break;

                    case "GRP":
                        query = (from a in entityContext.TeamStructureALLSet
                                 where (a.GroupName.Contains(SearchValue.Trim()) || a.Group_Code.Contains(SearchValue.Trim())) && a.Year == year
                                 select a).ToList();
                        break;

                    case "UNT":
                        query = (from a in entityContext.TeamStructureALLSet
                                 where (a.unitname.Contains(SearchValue.Trim()) || a.unit_code.Contains(SearchValue.Trim())) && a.Year == year
                                 select a).ToList();
                        break;

                    case "ACCT":
                        query = (from a in entityContext.TeamStructureALLSet
                                 where (a.AccountofficerName.Contains(SearchValue.Trim()) || a.Accountofficer_Code.Contains(SearchValue.Trim())) && a.Year == year
                                 select a).ToList();
                        break;
                }
                

                //return query.ToFullyLoaded();
                return query;
            }
        }

        private SetUp GetSetUp()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                SetUp query = (from a in entityContext.SetUpSet
                               select a).FirstOrDefault();

                return query;
            }
        }
        public IEnumerable<TeamStructureALL> GetTeamStructureALLBySetUp()
        {
            using (MPRContext_2 entityContext = new MPRContext_2())
            {
                //var setup = GetSetUp();

                //string maxyear = entityContext.TeamStructureALLSet.Max(x => x.Year);
                //List<int> periodsForLatestYear = entityContext.TeamStructureALLSet.Where(x => x.Year == maxyear).Select(x => x.Period).ToList();
                //int maxperiod = periodsForLatestYear.Max();

                var periodsForLatestYear = entityContext.TeamStructureALLSet.OrderByDescending(x => x.Year).Take(1).FirstOrDefault();
                int maxyear = periodsForLatestYear.Year;
                int maxperiod = periodsForLatestYear.Period;

                var query = (from a in entityContext.TeamStructureALLSet

                               where a.Year == maxyear && a.Period == maxperiod
                             //where a.Year == entityContext.TeamStructureALLSet.Max(x => x.Year) && a.Period == entityContext.TeamStructureALLSet.Max(x => x.Period)
                             //select a);
                             select new 
                             {
                                // NPSDate = DbFunctions.TruncateTime(n.NPSDate),
                                // Team_StructureId = a.Team_StructureId,
                                 Team_Code = a.Team_Code,
                                 TeamName = a.TeamName                                
                             })
                             .AsEnumerable().Select(x => new TeamStructureALL
                             {
                                // Team_StructureId = x.Team_StructureId,
                                 Team_Code = x.Team_Code,
                                 TeamName = x.TeamName
                                 // }).GroupBy(x => new { Team_Code, x.MainCaption, x.Currency }).Select(o => o.FirstOrDefault());
                             }).GroupBy(x => x.Team_Code).Select(o => o.FirstOrDefault())
                             .OrderBy(x => x.TeamName)
                            .ToList();
               
                return query;
            }
        }

        //NOTE: The team definition codes are hard coded here because they are not mapped/referenced in the teanstructure table.
        //NOTE: The below uses daily db.
        public IEnumerable<TeamStructureALL> GetTeamStructureALLByDefinitionCode(string code)
        {
            using (MPRContext_2 entityContext = new MPRContext_2())
            {
                var periodsForLatestYear = entityContext.TeamStructureALLSet.OrderByDescending(x => x.Year).Take(1).FirstOrDefault();
                int maxyear = periodsForLatestYear.Year;
                int maxperiod = periodsForLatestYear.Period;

                //var setup = GetSetUp();

                var query = new List<TeamStructureALL>();

                if (code == "TEM")
                {
                    query = (from a in entityContext.TeamStructureALLSet
                                 //where a.Year == setup.Year && a.Period == setup.Period
                             where a.Year == maxyear && a.Period == maxperiod

                             select new
                             {
                                 Team_Code = a.Team_Code,
                                 TeamName = a.TeamName
                             })
                            .AsEnumerable().Select(x => new TeamStructureALL
                            {
                                Team_Code = x.Team_Code,
                                TeamName = x.TeamName
                            })
                            .GroupBy(x => x.Team_Code).Select(o => o.FirstOrDefault())
                            .OrderBy(x => x.TeamName)
                           .ToList();
                }

                //else if (code == "ACCT")
                //{
                //    query = (from a in entityContext.TeamStructureALLSet
                //             where a.Year == setup.Year && a.Period == setup.Period

                //             select new
                //             {
                //                 Accountofficer_Code = a.Accountofficer_Code,
                //                 AccountofficerName = a.AccountofficerName
                //             })
                //            .AsEnumerable().Select(x => new TeamStructureALL
                //            {
                //                Accountofficer_Code = x.Accountofficer_Code,
                //                AccountofficerName = x.AccountofficerName
                //            })
                //            .GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault())
                //            .OrderBy(x => x.AccountofficerName)
                //           .ToList();
                //}

                else if (code == "BRH")
                {
                    query = (from a in entityContext.TeamStructureALLSet
                                 //where a.Year == setup.Year && a.Period == setup.Period
                             where a.Year == maxyear && a.Period == maxperiod

                             select new
                             {
                                 Branch_Code = a.Branch_Code,
                                 BranchName = a.BranchName
                             })
                            .AsEnumerable().Select(x => new TeamStructureALL
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
                    query = (from a in entityContext.TeamStructureALLSet
                                 //where a.Year == setup.Year && a.Period == setup.Period
                             where a.Year == maxyear && a.Period == maxperiod

                             select new
                             {
                                 Division_Code = a.Division_Code,
                                 DivisionName = a.DivisionName
                             })
                            .AsEnumerable().Select(x => new TeamStructureALL
                            {
                                Division_Code = x.Division_Code,
                                DivisionName = x.DivisionName
                            })
                            .GroupBy(x => x.Division_Code).Select(o => o.FirstOrDefault())
                            .OrderBy(x => x.DivisionName)
                           .ToList();
                }

                else if (code == "GRP")
                {
                    query = (from a in entityContext.TeamStructureALLSet
                                 // where a.Year == setup.Year && a.Period == setup.Period
                             where a.Year == maxyear && a.Period == maxperiod

                             select new
                             {
                                 Group_Code = a.Group_Code,
                                 GroupName = a.GroupName
                             })
                            .AsEnumerable().Select(x => new TeamStructureALL
                            {
                                Group_Code = x.Group_Code,
                                GroupName = x.GroupName
                            })
                            .GroupBy(x => x.Group_Code).Select(o => o.FirstOrDefault())
                            .OrderBy(x => x.GroupName)
                           .ToList();
                }

                else if (code == "REG")
                {
                    query = (from a in entityContext.TeamStructureALLSet
                                 // where a.Year == setup.Year && a.Period == setup.Period
                             where a.Year == maxyear && a.Period == maxperiod

                             select new
                             {
                                 Region_Code = a.Region_Code,
                                 RegionName = a.RegionName
                             })
                            .AsEnumerable().Select(x => new TeamStructureALL
                            {
                                Region_Code = x.Region_Code,
                                RegionName = x.RegionName
                            })
                            .GroupBy(x => x.Region_Code).Select(o => o.FirstOrDefault())
                            .OrderBy(x => x.RegionName)
                           .ToList();
                }

                else if (code == "DIR")
                {
                    query = (from a in entityContext.TeamStructureALLSet
                                 //where a.Year == setup.Year && a.Period == setup.Period
                             where a.Year == maxyear && a.Period == maxperiod

                             select new
                             {
                                 DIRECTORATECODE = a.DIRECTORATECODE,
                                 DIRECTORATENAME = a.DIRECTORATENAME
                             })
                            .AsEnumerable().Select(x => new TeamStructureALL
                            {
                                DIRECTORATECODE = x.DIRECTORATECODE,
                                DIRECTORATENAME = x.DIRECTORATENAME
                            })
                            .GroupBy(x => x.DIRECTORATECODE).Select(o => o.FirstOrDefault())
                            .OrderBy(x => x.DIRECTORATENAME)
                           .ToList();
                }

                else if (code == "ZON")
                {
                    query = (from a in entityContext.TeamStructureALLSet
                                 //where a.Year == setup.Year && a.Period == setup.Period
                             where a.Year == maxyear && a.Period == maxperiod

                             select new
                             {
                                 Zone_Code = a.Zone_Code,
                                 ZoneName = a.ZoneName
                             })
                            .AsEnumerable().Select(x => new TeamStructureALL
                            {
                                Zone_Code = x.Zone_Code,
                                ZoneName = x.ZoneName
                            })
                            .GroupBy(x => x.Zone_Code).Select(o => o.FirstOrDefault())
                            .OrderBy(x => x.ZoneName)
                           .ToList();
                }
             
                else if (code == "SEG")
                {
                    query = (from a in entityContext.TeamStructureALLSet
                                 //where a.Year == setup.Year && a.Period == setup.Period
                             where a.Year == maxyear && a.Period == maxperiod

                             select new
                             {
                                 Segment_Code = a.Segment_Code,
                                 SegmentName = a.SegmentName
                             })
                            .AsEnumerable().Select(x => new TeamStructureALL
                            {
                                Segment_Code = x.Segment_Code,
                                SegmentName = x.SegmentName
                            })
                            .GroupBy(x => x.Segment_Code).Select(o => o.FirstOrDefault())
                            .OrderBy(x => x.SegmentName)
                           .ToList();
                }

                else if (code == "SSG")
                {
                    query = (from a in entityContext.TeamStructureALLSet
                                 //where a.Year == setup.Year && a.Period == setup.Period
                             where a.Year == maxyear && a.Period == maxperiod

                             select new
                             {
                                 SuperSegment_Code = a.SuperSegment_Code,
                                 SuperSegmentName = a.SuperSegmentName
                             })
                            .AsEnumerable().Select(x => new TeamStructureALL
                            {
                                SuperSegment_Code = x.SuperSegment_Code,
                                SuperSegmentName = x.SuperSegmentName
                            })
                            .GroupBy(x => x.SuperSegment_Code).Select(o => o.FirstOrDefault())
                            .OrderBy(x => x.SuperSegmentName)
                           .ToList();
                }


                return query;
            }
        }


        ////NOTE: The team definition codes are hard coded here because they are not mapped/referenced in the teanstructure table.
        public IEnumerable<TeamStructureALL> GetTeamStructureALLByParamsAndeSetUp(string code, string SearchValue)
        {
            using (MPRContext_2 entityContext = new MPRContext_2())
            {
                var periodsForLatestYear = entityContext.TeamStructureALLSet.OrderByDescending(x => x.Year).Take(1).FirstOrDefault();
                int maxyear = periodsForLatestYear.Year;
                int maxperiod = periodsForLatestYear.Period;

                var query = new List<TeamStructureALL>();

                if (code == "ACCT")
                {
                    query = (from a in entityContext.TeamStructureALLSet
                                 //where a.Year == setup.Year && a.Period == setup.Period && (a.AccountofficerName.StartsWith(SearchValue.Trim()) || a.Accountofficer_Code.StartsWith(SearchValue.Trim()))
                             
                             where a.Year == maxyear && a.Period == maxperiod && (a.AccountofficerName.StartsWith(SearchValue.Trim()) || a.Accountofficer_Code.StartsWith(SearchValue.Trim()))

                             select new
                             {
                                 Accountofficer_Code = a.Accountofficer_Code,
                                 AccountofficerName = a.AccountofficerName
                             })
                            .AsEnumerable().Select(x => new TeamStructureALL
                            {
                                Accountofficer_Code = x.Accountofficer_Code,
                                AccountofficerName = x.AccountofficerName
                            })
                            .GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault())
                            .OrderBy(x => x.AccountofficerName)
                           .ToList();
                }

                return query;
            }
        }
        // using daily db ends

        //NOTE: The team definition codes are hard coded here because they are not mapped/referenced in the teanstructure table.
        //NOTE: The below uses montly db.
        public IEnumerable<TeamStructureALL> GetTeamStructureALLByDefinitionCodeMonthly(string code)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var periodsForLatestYear = entityContext.TeamStructureALLSet.OrderByDescending(x => x.Year).Take(1).FirstOrDefault();
                int maxyear = periodsForLatestYear.Year;
                int maxperiod = periodsForLatestYear.Period;

                var query = new List<TeamStructureALL>();


                if (code == "TEM")
                {
                    query = (from a in entityContext.TeamStructureALLSet
                                 //where a.Year == setup.Year && a.Period == setup.Period
                             where a.Year == maxyear && a.Period == maxperiod

                             select new
                             {
                                 Team_Code = a.Team_Code,
                                 TeamName = a.TeamName
                             })
                            .AsEnumerable().Select(x => new TeamStructureALL
                            {
                                Team_Code = x.Team_Code,
                                TeamName = x.TeamName
                            })
                            .GroupBy(x => x.Team_Code).Select(o => o.FirstOrDefault())
                            .OrderBy(x => x.TeamName)
                           .ToList();
                }

                //else if (code == "ACCT")
                //{
                //    query = (from a in entityContext.TeamStructureALLSet
                //             where a.Year == setup.Year && a.Period == setup.Period

                //             select new
                //             {
                //                 Accountofficer_Code = a.Accountofficer_Code,
                //                 AccountofficerName = a.AccountofficerName
                //             })
                //            .AsEnumerable().Select(x => new TeamStructureALL
                //            {
                //                Accountofficer_Code = x.Accountofficer_Code,
                //                AccountofficerName = x.AccountofficerName
                //            })
                //            .GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault())
                //            .OrderBy(x => x.AccountofficerName)
                //           .ToList();
                //}

                else if (code == "BRH")
                {
                    query = (from a in entityContext.TeamStructureALLSet
                                 //where a.Year == setup.Year && a.Period == setup.Period
                             where a.Year == maxyear && a.Period == maxperiod

                             select new
                             {
                                 Branch_Code = a.Branch_Code,
                                 BranchName = a.BranchName
                             })
                            .AsEnumerable().Select(x => new TeamStructureALL
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
                    query = (from a in entityContext.TeamStructureALLSet
                                 //where a.Year == setup.Year && a.Period == setup.Period
                             where a.Year == maxyear && a.Period == maxperiod

                             select new
                             {
                                 Division_Code = a.Division_Code,
                                 DivisionName = a.DivisionName
                             })
                            .AsEnumerable().Select(x => new TeamStructureALL
                            {
                                Division_Code = x.Division_Code,
                                DivisionName = x.DivisionName
                            })
                            .GroupBy(x => x.Division_Code).Select(o => o.FirstOrDefault())
                            .OrderBy(x => x.DivisionName)
                           .ToList();
                }

                else if (code == "GRP")
                {
                    query = (from a in entityContext.TeamStructureALLSet
                                 // where a.Year == setup.Year && a.Period == setup.Period
                             where a.Year == maxyear && a.Period == maxperiod

                             select new
                             {
                                 Group_Code = a.Group_Code,
                                 GroupName = a.GroupName
                             })
                            .AsEnumerable().Select(x => new TeamStructureALL
                            {
                                Group_Code = x.Group_Code,
                                GroupName = x.GroupName
                            })
                            .GroupBy(x => x.Group_Code).Select(o => o.FirstOrDefault())
                            .OrderBy(x => x.GroupName)
                           .ToList();
                }

                else if (code == "REG")
                {
                    query = (from a in entityContext.TeamStructureALLSet
                                 // where a.Year == setup.Year && a.Period == setup.Period
                             where a.Year == maxyear && a.Period == maxperiod

                             select new
                             {
                                 Region_Code = a.Region_Code,
                                 RegionName = a.RegionName
                             })
                            .AsEnumerable().Select(x => new TeamStructureALL
                            {
                                Region_Code = x.Region_Code,
                                RegionName = x.RegionName
                            })
                            .GroupBy(x => x.Region_Code).Select(o => o.FirstOrDefault())
                            .OrderBy(x => x.RegionName)
                           .ToList();
                }

                else if (code == "DIR")
                {
                    query = (from a in entityContext.TeamStructureALLSet
                                 //where a.Year == setup.Year && a.Period == setup.Period
                             where a.Year == maxyear && a.Period == maxperiod

                             select new
                             {
                                 DIRECTORATECODE = a.DIRECTORATECODE,
                                 DIRECTORATENAME = a.DIRECTORATENAME
                             })
                            .AsEnumerable().Select(x => new TeamStructureALL
                            {
                                DIRECTORATECODE = x.DIRECTORATECODE,
                                DIRECTORATENAME = x.DIRECTORATENAME
                            })
                            .GroupBy(x => x.DIRECTORATECODE).Select(o => o.FirstOrDefault())
                            .OrderBy(x => x.DIRECTORATENAME)
                           .ToList();
                }

                else if (code == "ZON")
                {
                    query = (from a in entityContext.TeamStructureALLSet
                                 //where a.Year == setup.Year && a.Period == setup.Period
                             where a.Year == maxyear && a.Period == maxperiod

                             select new
                             {
                                 Zone_Code = a.Zone_Code,
                                 ZoneName = a.ZoneName
                             })
                            .AsEnumerable().Select(x => new TeamStructureALL
                            {
                                Zone_Code = x.Zone_Code,
                                ZoneName = x.ZoneName
                            })
                            .GroupBy(x => x.Zone_Code).Select(o => o.FirstOrDefault())
                            .OrderBy(x => x.ZoneName)
                           .ToList();
                }

                else if (code == "SEG")
                {
                    query = (from a in entityContext.TeamStructureALLSet
                                 //where a.Year == setup.Year && a.Period == setup.Period
                             where a.Year == maxyear && a.Period == maxperiod

                             select new
                             {
                                 Segment_Code = a.Segment_Code,
                                 SegmentName = a.SegmentName
                             })
                            .AsEnumerable().Select(x => new TeamStructureALL
                            {
                                Segment_Code = x.Segment_Code,
                                SegmentName = x.SegmentName
                            })
                            .GroupBy(x => x.Segment_Code).Select(o => o.FirstOrDefault())
                            .OrderBy(x => x.SegmentName)
                           .ToList();
                }

                else if (code == "SSG")
                {
                    query = (from a in entityContext.TeamStructureALLSet
                                 //where a.Year == setup.Year && a.Period == setup.Period
                             where a.Year == maxyear && a.Period == maxperiod

                             select new
                             {
                                 SuperSegment_Code = a.SuperSegment_Code,
                                 SuperSegmentName = a.SuperSegmentName
                             })
                            .AsEnumerable().Select(x => new TeamStructureALL
                            {
                                SuperSegment_Code = x.SuperSegment_Code,
                                SuperSegmentName = x.SuperSegmentName
                            })
                            .GroupBy(x => x.SuperSegment_Code).Select(o => o.FirstOrDefault())
                            .OrderBy(x => x.SuperSegmentName)
                           .ToList();
                }


                return query;
            }
        }


        ////NOTE: The team definition codes are hard coded here because they are not mapped/referenced in the teanstructure table.
        public IEnumerable<TeamStructureALL> GetTeamStructureALLByParamsAndeSetUpMonthly(string code, string SearchValue)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var periodsForLatestYear = entityContext.TeamStructureALLSet.OrderByDescending(x => x.Year).Take(1).FirstOrDefault();
                int maxyear = periodsForLatestYear.Year;
                int maxperiod = periodsForLatestYear.Period;

                var query = new List<TeamStructureALL>();

                if (code == "ACCT")
                {
                    query = (from a in entityContext.TeamStructureALLSet
                                 //where a.Year == setup.Year && a.Period == setup.Period && (a.AccountofficerName.StartsWith(SearchValue.Trim()) || a.Accountofficer_Code.StartsWith(SearchValue.Trim()))

                             where a.Year == maxyear && a.Period == maxperiod && (a.AccountofficerName.StartsWith(SearchValue.Trim()) || a.Accountofficer_Code.StartsWith(SearchValue.Trim()))

                             select new
                             {
                                 Accountofficer_Code = a.Accountofficer_Code,
                                 AccountofficerName = a.AccountofficerName
                             })
                            .AsEnumerable().Select(x => new TeamStructureALL
                            {
                                Accountofficer_Code = x.Accountofficer_Code,
                                AccountofficerName = x.AccountofficerName
                            })
                            .GroupBy(x => x.Accountofficer_Code).Select(o => o.FirstOrDefault())
                            .OrderBy(x => x.AccountofficerName)
                           .ToList();
                }

                return query;
            }
        }


        public IEnumerable<TeamStructureALL> GetTeamStructureALLBySetUpMonthly()
        {
            using (MPRContext entityContext = new MPRContext())
            {
                var periodsForLatestYear = entityContext.TeamStructureALLSet.OrderByDescending(x => x.Year).Take(1).FirstOrDefault();
                int maxyear = periodsForLatestYear.Year;
                int maxperiod = periodsForLatestYear.Period;

                var query = (from a in entityContext.TeamStructureALLSet

                             where a.Year == maxyear && a.Period == maxperiod
                             //where a.Year == entityContext.TeamStructureALLSet.Max(x => x.Year) && a.Period == entityContext.TeamStructureALLSet.Max(x => x.Period)
                             //select a);
                             select new
                             {
                                 // NPSDate = DbFunctions.TruncateTime(n.NPSDate),
                                 // Team_StructureId = a.Team_StructureId,
                                 Team_Code = a.Team_Code,
                                 TeamName = a.TeamName
                             })
                             .AsEnumerable().Select(x => new TeamStructureALL
                             {
                                 // Team_StructureId = x.Team_StructureId,
                                 Team_Code = x.Team_Code,
                                 TeamName = x.TeamName
                                 // }).GroupBy(x => new { Team_Code, x.MainCaption, x.Currency }).Select(o => o.FirstOrDefault());
                             }).GroupBy(x => x.Team_Code).Select(o => o.FirstOrDefault())
                             .OrderBy(x => x.TeamName)
                            .ToList();

                return query;
            }
        }

        public TeamStructureALL GetTeamStructureALLTop1(string branch, string defcode, int year)
        {
            using (MPRContext entityContext = new MPRContext())
            {
                branch = branch.Trim().ToLower();
                defcode = defcode.Trim().ToUpper();
                //year = year.Trim();

                TeamStructureALL query = null;

                //switch (defcode)
                //{
                //    case "TEM":
                //        query = entityContext.TeamStructureALLSet.Where(x => x.Year.Trim() == year && (x.Team_Code.Trim().ToLower() == branch || x.TeamName.Trim().ToLower() == branch)).FirstOrDefault();
                //        break;

                //    case "BRH":
                //        query = entityContext.TeamStructureALLSet.Where(x => x.Year.Trim() == year && (x.Branch_Code.Trim().ToLower() == branch || x.BranchName.Trim().ToLower() == branch)).FirstOrDefault();
                //        break;

                //    case "ZON":
                //        query = entityContext.TeamStructureALLSet.Where(x => x.Year.Trim() == year && (x.Zone_Code.Trim().ToLower() == branch || x.ZoneName.Trim().ToLower() == branch)).FirstOrDefault();
                //        break;
                //}

                return query;
            }
        }

        //public TeamStructureALL GetTeamStructureALLTop1(string branch,  string year)
        //{
        //    using (MPRContext entityContext = new MPRContext())
        //    {
        //        branch = branch.Trim().ToLower();
        //        year = year.Trim();

        //        var query = (from a in entityContext.TeamStructureALLSet

        //                     where (a.BranchName.Trim().ToLower() == branch || a.Branch_Code.Trim().ToLower() == branch) && a.Year.Trim() == year
        //                     //select a);
        //                     select new
        //                     {
        //                         AccountofficerName = a.AccountofficerName,
        //                         Accountofficer_Code = a.Accountofficer_Code,
        //                         BranchName = a.BranchName,
        //                         TeamName = a.TeamName,
        //                          Team_Code = a.Team_Code,
        //                         ZoneName = a.ZoneName,
        //                         Zone_Code = a.Zone_Code,

        //                         DivisionName = a.DivisionName,
        //                         Division_Code = a.Division_Code,
        //                         RegionName = a.RegionName,
        //                         Region_Code = a.Region_Code,
        //                         DIRECTORATENAME = a.DIRECTORATENAME,
        //                         DIRECTORATECODE = a.DIRECTORATECODE,

        //                         GroupName = a.GroupName,
        //                         Group_Code = a.Group_Code,
        //                         unitname = a.unitname,
        //                         unit_code = a.unit_code
        //                     })
        //                     .AsEnumerable().Select(x => new TeamStructureALL
        //                     {
        //                         AccountofficerName = x.AccountofficerName,
        //                         Accountofficer_Code = x.Accountofficer_Code,
        //                         BranchName = x.BranchName,
        //                         TeamName = x.TeamName,
        //                         Team_Code = x.Team_Code,
        //                         ZoneName = x.ZoneName,
        //                         Zone_Code = x.Zone_Code,

        //                         DivisionName = x.DivisionName,
        //                         Division_Code = x.Division_Code,
        //                         RegionName = x.RegionName,
        //                         Region_Code = x.Region_Code,
        //                         DIRECTORATENAME = x.DIRECTORATENAME,
        //                         DIRECTORATECODE = x.DIRECTORATECODE,

        //                         GroupName = x.GroupName,
        //                         Group_Code = x.Group_Code,
        //                         unitname = x.unitname,
        //                         unit_code = x.unit_code
        //                     }).FirstOrDefault();

        //        return query;
        //    }
        //}

    }
}




