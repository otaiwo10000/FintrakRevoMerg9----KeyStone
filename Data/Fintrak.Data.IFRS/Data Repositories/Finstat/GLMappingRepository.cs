using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;
using System.Data.SqlClient;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IGLMappingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GLMappingRepository : DataRepositoryBase<GLMapping>, IGLMappingRepository
    {

        protected override GLMapping AddEntity(IFRSContext entityContext, GLMapping entity)
        {
            return entityContext.Set<GLMapping>().Add(entity);
        }

        protected override GLMapping UpdateEntity(IFRSContext entityContext, GLMapping entity)
        {
            return (from e in entityContext.Set<GLMapping>() 
                    where e.GLMappingId == entity.GLMappingId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<GLMapping> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<GLMapping>()
                   select e;
        }

        protected override GLMapping GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<GLMapping>()
                         where e.GLMappingId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<GLMappingInfo> GetGLMappings()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.GLMappingSet
                            join b in entityContext.IFRSRegistrySet on a.CaptionCode equals b.Code 
                            select new GLMappingInfo()
                            {
                                GLMapping = a,
                                IFRSRegistry = b
                            };

                return query.ToFullyLoaded();
            }
        }

        public List<GLMapping> GetSubSubCaption(string caption)
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;
            var connectionString = IFRSContext.GetDataConnection();

            var glmappings = new List<GLMapping>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_getsubsubcaption", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "subcaption",
                    Value = caption,
                });


                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var glmapping = new GLMapping();

                    if (reader["SubCaption1"] != DBNull.Value)
                        glmapping.SubCaption1 = reader["SubCaption1"].ToString();

                    glmappings.Add(glmapping);
                }

                con.Close();
            }

            return glmappings;
        }

 
    }
}
