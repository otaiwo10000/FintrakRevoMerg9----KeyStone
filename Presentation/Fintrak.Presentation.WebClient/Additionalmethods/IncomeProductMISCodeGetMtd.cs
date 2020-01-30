using Fintrak.Presentation.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fintrak.Presentation.WebClient.Additionalmethods
{
    public class IncomeProductMISCodeGetMtd
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;      

        public IEnumerable<Models.IncomeProductMISCodeGetModel> GetProductMISCode()
        {
            List<IncomeProductMISCodeGetModel> prodmisList = new List<IncomeProductMISCodeGetModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("Income_Product_MISCode_Get", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                //cmd.Parameters.Add("@result", System.Data.SqlDbType.TinyInt).Direction = System.Data.ParameterDirection.Output;

                con.Open();
                //cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var prodmis = new IncomeProductMISCodeGetModel();

                    //iob.ID = reader["ID"] != DBNull.Value ? int.Parse(reader["ID"].ToString()) : 0;
                    prodmis.ProductCode = reader["ProductCode"] != DBNull.Value ? reader["ProductCode"].ToString() : "default";
                    prodmis.CATEGORY_DESCRIPTION = reader["CATEGORY_DESCRIPTION"] != DBNull.Value ? reader["CATEGORY_DESCRIPTION"].ToString() : "default";
                    prodmis.MIS_Code = reader["MIS_Code"] != DBNull.Value ? reader["MIS_Code"].ToString() : "default";
                    prodmis.AccountOfficer_Code = reader["AccountOfficer_Code"] != DBNull.Value ? reader["AccountOfficer_Code"].ToString() : "default";

                    prodmisList.Add(prodmis);
                }
                con.Close();
            }
            return prodmisList;
        } //========== end of the mtd

       

    }
}