using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Fintrak.Shared.AuditService;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Data;
using systemContract = Fintrak.Data.SystemCore.Contracts;
using systemCore = Fintrak.Data.SystemCore;
using Fintrak.Data.SystemCore.Contracts;

namespace Fintrak.Data.MPR
{
    public class MPRContext_2 : DbContext
    {

        public MPRContext_2()
        : base("name=FintrakDBDailyConnection")
        {
        }


        //const string SOLUTION_NAME = "FIN_MPR";

        //AuditManager _auditManager;

        //public MPRContext_2()
        //    : base(GetDataConnection())
        //{
        //    System.Data.Entity.Database.SetInitializer<MPRContext_2>(null);

        //    _auditManager = new AuditManager(GetDataConnection());
        //}

        //public MPRContext_2(string connectionString)
        //    : base(connectionString)
        //{
        //    System.Data.Entity.Database.SetInitializer<MPRContext_2>(null);
        //    _auditManager = new AuditManager(connectionString);
        //}



        //MPR Team Structure
        public DbSet<TeamStructure> TeamStructureSet { get; set; }
        public DbSet<TeamStructureALL> TeamStructureALLSet { get; set; }

        //Income
        //public DbSet<IncomeProductsTable> IncomeProductsTableSet { get; set; }

        ////PPR Caption
        //public DbSet<PPRCaption> PPRCaptionSet { get; set; }

        ////PL Caption
        //public DbSet<PLCaption2> PLCaption2Set { get; set; }

        ////Caption
        //public DbSet<Caption> CaptionSet { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Ignore<PropertyChangedEventHandler>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();




            //MPR
            //TeamDefinition
            modelBuilder.Entity<TeamStructure>().HasKey<int>(e => e.Team_StructureId).Ignore(e => e.EntityId);
            modelBuilder.Entity<TeamStructure>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TeamStructure>().ToTable("Mpr_Team_Structure");

            //TeamDefinitionALL
            modelBuilder.Entity<TeamStructureALL>().HasKey<int>(e => e.Team_StructureId).Ignore(e => e.EntityId);
            modelBuilder.Entity<TeamStructureALL>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TeamStructureALL>().ToTable("Mpr_Team_Structure_ALL");

            ////Income
            //modelBuilder.Entity<IncomeProductsTable>().HasKey<int>(e => e.ProductID).Ignore(e => e.EntityId);
            //modelBuilder.Entity<IncomeProductsTable>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<IncomeProductsTable>().ToTable("Income_ProductsTable");

            ////PPR Caption
            //modelBuilder.Entity<PPRCaption>().HasKey<int>(e => e.PPR_CaptionId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<PPRCaption>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<PPRCaption>().ToTable("PPR_Caption");

            ////PL Caption
            //modelBuilder.Entity<PLCaption2>().HasKey<int>(e => e.PL_CaptionId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<PLCaption2>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<PLCaption2>().ToTable("PL_Caption2");

            ////Caption
            //modelBuilder.Entity<Caption>().HasKey<int>(e => e.CaptionId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<Caption>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<Caption>().ToTable("Caption");

        }



        //public override int SaveChanges()
        //{
        //    try
        //    {
        //        if (ChangeTracker.HasChanges())
        //        {
        //            var entries = this.ChangeTracker.Entries();

        //            foreach (DbEntityEntry entry in entries)
        //            {
        //                if (entry.Entity != null)
        //                {
        //                    if (entry.State == EntityState.Added)
        //                    {
        //                        //entry is Added 

        //                        var model = (EntityBase)entry.Entity;
        //                        model.CreatedBy = DataConnector.LoginName;
        //                        model.CreatedOn = DateTime.Now;
        //                        model.UpdatedBy = DataConnector.LoginName;
        //                        model.UpdatedOn = DateTime.Now;
        //                    }
        //                    else if (entry.State == EntityState.Deleted)
        //                    {
        //                        //entry in deleted

        //                    }
        //                    else
        //                    {
        //                        //entry is modified
        //                        var model = (EntityBase)entry.Entity;
        //                        model.UpdatedBy = DataConnector.LoginName;
        //                        model.UpdatedOn = DateTime.Now;
        //                    }

        //                    _auditManager.AddAudit(entry, DataConnector.LoginName);
        //                }
        //            }
        //        }

        //        _auditManager.Save();

        //        return base.SaveChanges();
        //    }
        //    catch (DbUpdateException e)
        //    {
        //        var innerEx = e.InnerException;
        //        while (innerEx.InnerException != null)
        //            innerEx = innerEx.InnerException;

        //        throw new Exception(innerEx.Message);
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        var sb = new StringBuilder();

        //        foreach (var entry in e.EntityValidationErrors)
        //        {
        //            foreach (var error in entry.ValidationErrors)
        //            {
        //                sb.AppendLine(string.Format("{0}-{1}-{2}", entry.Entry.Entity, error.PropertyName, error.ErrorMessage));
        //            }
        //        }

        //        throw new Exception(sb.ToString());
        //    }
        //    catch (Exception e)
        //    {
        //        var innerEx = e.InnerException;
        //        while (innerEx.InnerException != null)
        //            innerEx = innerEx.InnerException;

        //        throw new Exception(innerEx.Message);
        //    }

        //}

        //public static string GetDataConnection()
        //{
        //    string connectionString = "";

        //    if (!string.IsNullOrEmpty(DataConnector.CompanyCode) && !string.IsNullOrEmpty(SOLUTION_NAME))
        //    {
        //        systemContract.IDatabaseRepository databaseRepository = new systemCore.DatabaseRepository();
        //        var companydbs = databaseRepository.GetDatabases().Where(c => c.Database.CompanyCode == DataConnector.CompanyCode && (c.Solution.Name == SOLUTION_NAME || c.Solution.Name == "CORE"));

        //        DatabaseInfo companydb = null;

        //        if (companydbs == null)
        //            throw new Exception("Unable to load database.");
        //        else
        //        {
        //            companydb = companydbs.Where(c => c.Solution.Name == SOLUTION_NAME).FirstOrDefault();

        //            if (companydb == null)
        //                companydb = companydbs.FirstOrDefault();
        //        }

        //        //connectionString="Data Source=10.0.0.18\FintrakSQL2014;Initial Catalog=FintrakDB;User =sa;Password=sqluser10$;Integrated Security=False"
        //        connectionString = string.Format("Data Source= {0};Initial Catalog={1};User ={2};Password={3};Integrated Security={4}", companydb.Database.ServerName, companydb.Database.DatabaseName, companydb.Database.UserName, companydb.Database.Password, companydb.Database.IntegratedSecurity);
        //    }

        //    return connectionString;
        //}



    }
}
