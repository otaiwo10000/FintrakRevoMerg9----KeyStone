using Fintrak.Presentation.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fintrak.Presentation.WebClient.Additionalmethods
{
    public class ProfitORLosspMtd
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
       
        public IEnumerable<Models.IncomeSummaryModel> GetIncomeSummary(int yr, int pr)
        {
            List<IncomeSummaryModel> incomesummaryList = new List<IncomeSummaryModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("Proc_IncomeSummary", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Year",
                    Value = yr,
                });

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Period",
                    Value = pr,
                });

                con.Open();
                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

               if(pr==1)
               { 
                    while (reader.Read())
                    {
                        var incomesummary = new IncomeSummaryModel();

                        incomesummary.MainCaption = reader["MainCaption"] != DBNull.Value ? reader["MainCaption"].ToString() : "default";
                        incomesummary.Budget = reader["Budget"] != DBNull.Value ? reader["Budget"].ToString() : "default";
                        incomesummary.Jan = reader["January"] != DBNull.Value ? reader["January"].ToString() : "default";
                        incomesummaryList.Add(incomesummary);
                    }
                }

                else if (pr == 2)
                {
                    while (reader.Read())
                    {
                        var incomesummary = new IncomeSummaryModel();

                        incomesummary.MainCaption = reader["MainCaption"] != DBNull.Value ? reader["MainCaption"].ToString() : "default";
                        incomesummary.Budget = reader["Budget"] != DBNull.Value ? reader["Budget"].ToString() : "default";
                        incomesummary.Jan = reader["January"] != DBNull.Value ? reader["January"].ToString() : "default";
                        incomesummary.Feb = reader["February"] != DBNull.Value ? reader["February"].ToString() : "default";
                        incomesummaryList.Add(incomesummary);
                    }
                }

                else if (pr == 3)
                {
                    while (reader.Read())
                    {
                        var incomesummary = new IncomeSummaryModel();

                        incomesummary.MainCaption = reader["MainCaption"] != DBNull.Value ? reader["MainCaption"].ToString() : "default";
                        incomesummary.Budget = reader["Budget"] != DBNull.Value ? reader["Budget"].ToString() : "default";
                        incomesummary.Jan = reader["January"] != DBNull.Value ? reader["January"].ToString() : "default";
                        incomesummary.Feb = reader["February"] != DBNull.Value ? reader["February"].ToString() : "default";
                        incomesummary.Mar = reader["March"] != DBNull.Value ? reader["March"].ToString() : "default";
                        incomesummaryList.Add(incomesummary);
                    }
                }

                else if (pr == 4)
                {
                    while (reader.Read())
                    {
                        var incomesummary = new IncomeSummaryModel();

                        incomesummary.MainCaption = reader["MainCaption"] != DBNull.Value ? reader["MainCaption"].ToString() : "default";
                        incomesummary.Budget = reader["Budget"] != DBNull.Value ? reader["Budget"].ToString() : "default";
                        incomesummary.Feb = reader["February"] != DBNull.Value ? reader["February"].ToString() : "default";
                        incomesummary.Mar = reader["March"] != DBNull.Value ? reader["March"].ToString() : "default";
                        incomesummary.Apr = reader["April"] != DBNull.Value ? reader["April"].ToString() : "default";
                        incomesummaryList.Add(incomesummary);
                    }
                }

                else if (pr == 5)
                {
                    while (reader.Read())
                    {
                        var incomesummary = new IncomeSummaryModel();

                        incomesummary.MainCaption = reader["MainCaption"] != DBNull.Value ? reader["MainCaption"].ToString() : "default";
                        incomesummary.Budget = reader["Budget"] != DBNull.Value ? reader["Budget"].ToString() : "default";
                        incomesummary.Mar = reader["March"] != DBNull.Value ? reader["March"].ToString() : "default";
                        incomesummary.Apr = reader["April"] != DBNull.Value ? reader["April"].ToString() : "default";
                        incomesummary.May = reader["May"] != DBNull.Value ? reader["May"].ToString() : "default";
                        incomesummaryList.Add(incomesummary);
                    }
                }

                else if (pr == 6)
                {
                    while (reader.Read())
                    {
                        var incomesummary = new IncomeSummaryModel();

                        incomesummary.MainCaption = reader["MainCaption"] != DBNull.Value ? reader["MainCaption"].ToString() : "default";
                        incomesummary.Budget = reader["Budget"] != DBNull.Value ? reader["Budget"].ToString() : "default";
                        incomesummary.Apr = reader["April"] != DBNull.Value ? reader["April"].ToString() : "default";
                        incomesummary.May = reader["May"] != DBNull.Value ? reader["May"].ToString() : "default";
                        incomesummary.Jun = reader["June"] != DBNull.Value ? reader["June"].ToString() : "default";
                        incomesummaryList.Add(incomesummary);
                    }
                }

                else if (pr == 7)
                {
                    while (reader.Read())
                    {
                        var incomesummary = new IncomeSummaryModel();

                        incomesummary.MainCaption = reader["MainCaption"] != DBNull.Value ? reader["MainCaption"].ToString() : "default";
                        incomesummary.Budget = reader["Budget"] != DBNull.Value ? reader["Budget"].ToString() : "default";
                        incomesummary.May = reader["May"] != DBNull.Value ? reader["May"].ToString() : "default";
                        incomesummary.Jun = reader["June"] != DBNull.Value ? reader["June"].ToString() : "default";
                        incomesummary.Jul = reader["July"] != DBNull.Value ? reader["July"].ToString() : "default";

                        incomesummaryList.Add(incomesummary);
                    }
                }

                else if (pr == 8)
                {
                    while (reader.Read())
                    {
                        var incomesummary = new IncomeSummaryModel();

                        incomesummary.MainCaption = reader["MainCaption"] != DBNull.Value ? reader["MainCaption"].ToString() : "default";
                        incomesummary.Budget = reader["Budget"] != DBNull.Value ? reader["Budget"].ToString() : "default";
                        incomesummary.Jun = reader["June"] != DBNull.Value ? reader["June"].ToString() : "default";
                        incomesummary.Jul = reader["July"] != DBNull.Value ? reader["July"].ToString() : "default";
                        incomesummary.Aug = reader["August"] != DBNull.Value ? reader["August"].ToString() : "default";

                        incomesummaryList.Add(incomesummary);
                    }
                }

                else if (pr == 9)
                {
                    while (reader.Read())
                    {
                        var incomesummary = new IncomeSummaryModel();

                        incomesummary.MainCaption = reader["MainCaption"] != DBNull.Value ? reader["MainCaption"].ToString() : "default";
                        incomesummary.Budget = reader["Budget"] != DBNull.Value ? reader["Budget"].ToString() : "default";
                        incomesummary.Jul = reader["July"] != DBNull.Value ? reader["July"].ToString() : "default";
                        incomesummary.Aug = reader["August"] != DBNull.Value ? reader["August"].ToString() : "default";
                        incomesummary.Sep = reader["September"] != DBNull.Value ? reader["September"].ToString() : "default";

                        incomesummaryList.Add(incomesummary);
                    }
                }

                else if (pr == 10)
                {
                    while (reader.Read())
                    {
                        var incomesummary = new IncomeSummaryModel();

                        incomesummary.MainCaption = reader["MainCaption"] != DBNull.Value ? reader["MainCaption"].ToString() : "default";
                        incomesummary.Budget = reader["Budget"] != DBNull.Value ? reader["Budget"].ToString() : "default";
                        incomesummary.Aug = reader["August"] != DBNull.Value ? reader["August"].ToString() : "default";
                        incomesummary.Sep = reader["September"] != DBNull.Value ? reader["September"].ToString() : "default";
                        incomesummary.Oct = reader["October"] != DBNull.Value ? reader["October"].ToString() : "default";

                        incomesummaryList.Add(incomesummary);
                    }
                }

                else if (pr == 11)
                {
                    while (reader.Read())
                    {
                        var incomesummary = new IncomeSummaryModel();

                        incomesummary.MainCaption = reader["MainCaption"] != DBNull.Value ? reader["MainCaption"].ToString() : "default";
                        incomesummary.Budget = reader["Budget"] != DBNull.Value ? reader["Budget"].ToString() : "default";
                        incomesummary.Sep = reader["September"] != DBNull.Value ? reader["September"].ToString() : "default";
                        incomesummary.Oct = reader["October"] != DBNull.Value ? reader["October"].ToString() : "default";
                        incomesummary.Nov = reader["November"] != DBNull.Value ? reader["November"].ToString() : "default";

                        incomesummaryList.Add(incomesummary);
                    }
                }

                else if (pr == 12)
                {
                    while (reader.Read())
                    {
                        var incomesummary = new IncomeSummaryModel();

                        incomesummary.MainCaption = reader["MainCaption"] != DBNull.Value ? reader["MainCaption"].ToString() : "default";
                        incomesummary.Budget = reader["Budget"] != DBNull.Value ? reader["Budget"].ToString() : "default";
                        incomesummary.Oct = reader["October"] != DBNull.Value ? reader["October"].ToString() : "default";
                        incomesummary.Nov = reader["November"] != DBNull.Value ? reader["November"].ToString() : "default";
                        incomesummary.Dec = reader["December"] != DBNull.Value ? reader["December"].ToString() : "default";

                        incomesummaryList.Add(incomesummary);
                    }
                }

                //while (reader.Read())
                //{
                //    var incomesummary = new IncomeSummaryModel();

                //    incomesummary.MainCaption = reader["MainCaption"] != DBNull.Value ? reader["MainCaption"].ToString() : "default";
                //    incomesummary.Budget = reader["Budget"] != DBNull.Value ? reader["Budget"].ToString() : "default";
                //    incomesummary.Jan = reader["January"] != DBNull.Value ? reader["January"].ToString() : "default";
                //    incomesummary.Feb = reader["February"] != DBNull.Value ? reader["February"].ToString() : "default";
                //    incomesummary.Mar = reader["March"] != DBNull.Value ? reader["March"].ToString() : "default";
                //    incomesummary.Apr = reader["April"] != DBNull.Value ? reader["April"].ToString() : "default";
                //    incomesummary.May = reader["May"] != DBNull.Value ? reader["May"].ToString() : "default";
                //    incomesummary.Jun = reader["June"] != DBNull.Value ? reader["June"].ToString() : "default";
                //    incomesummary.Jul = reader["July"] != DBNull.Value ? reader["July"].ToString() : "default";
                //    incomesummary.Aug = reader["August"] != DBNull.Value ? reader["August"].ToString() : "default";
                //    incomesummary.Sep = reader["September"] != DBNull.Value ? reader["September"].ToString() : "default";
                //    incomesummary.Oct = reader["October"] != DBNull.Value ? reader["October"].ToString() : "default";
                //    incomesummary.Nov = reader["November"] != DBNull.Value ? reader["November"].ToString() : "default";
                //    incomesummary.Dec = reader["December"] != DBNull.Value ? reader["December"].ToString() : "default";

                //    incomesummaryList.Add(incomesummary);
                //}
                con.Close();
            }
            return incomesummaryList;
        } //========== end of the mtd





    }
}