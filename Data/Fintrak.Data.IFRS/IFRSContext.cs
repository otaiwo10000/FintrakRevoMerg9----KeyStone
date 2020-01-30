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
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Data;
using systemContract = Fintrak.Data.SystemCore.Contracts;
using systemCore = Fintrak.Data.SystemCore;
using Fintrak.Data.SystemCore.Contracts;

namespace Fintrak.Data.IFRS
{
    public class IFRSContext : DbContext
    {
        const string SOLUTION_NAME = "FIN_IFRS";

        AuditManager _auditManager;

        public IFRSContext()
            : base(GetDataConnection())
        {
            System.Data.Entity.Database.SetInitializer<IFRSContext>(null);

            _auditManager = new AuditManager(GetDataConnection());
        }

        public IFRSContext(string connectionString)
            : base(connectionString)
        {
            System.Data.Entity.Database.SetInitializer<IFRSContext>(null);
            _auditManager = new AuditManager(connectionString);
        }

        //IFRS 
        public DbSet<DerivedCaption> DerivedCaptionSet { get; set; }
        public DbSet<GLMapping> GLMappingSet { get; set; }
        public DbSet<InstrumentType> InstrumentTypeSet { get; set; }
        public DbSet<GLType> GLTypeSet { get; set; }
        public DbSet<InstrumentTypeGLMap> InstrumentTypeGLMapSet { get; set; }
        public DbSet<AutoPostingTemplate> AutoPostingTemplateSet { get; set; }
        public DbSet<TrialBalanceGap> TrialBalanceGapSet { get; set; }
        public DbSet<GLAdjustment> GLAdjustmentSet { get; set; }
        public DbSet<PostingDetail> PostingDetailSet { get; set; }
        public DbSet<TrialBalance> TrialBalanceSet { get; set; }
        public DbSet<IFRSReport> IFRSReportSet { get; set; }
        public DbSet<TransactionDetail> TransactionDetailSet { get; set; }
        public DbSet<IFRSRegistry> IFRSRegistrySet { get; set; }
        public DbSet<LoanSetup> LoanSetupSet { get; set; }
        public DbSet<ScheduleType> ScheduleTypeSet { get; set; }
        public DbSet<IFRSProduct> IFRSProductSet { get; set; }
        public DbSet<CreditRiskRating> CreditRiskRatingSet { get; set; }
        public DbSet<CollateralCategory> CollateralCategorySet { get; set; }
        public DbSet<CollateralType> CollateralTypeSet { get; set; }
        public DbSet<CollateralRealizationPeriod> CollateralRealizationPeriodSet { get; set; }
        public DbSet<CollateralInformation> CollateralInformationSet { get; set; }
        public DbSet<WatchListedLoan> WatchListedLoanSet { get; set; }
        public DbSet<ImpairmentOverride> ImpairmentOverrideSet { get; set; }
        public DbSet<FairValueBasisMap> FairValueBasisMapSet { get; set; }
        public DbSet<FairValueBasisExemption> FairValueBasisExemptionSet { get; set; }
        public DbSet<BondComputation> BondComputationSet { get; set; }
        public DbSet<BondComputationResultZero> BondComputationResultZeroSet { get; set; }
        public DbSet<BondPeriodicSchedule> BondPeriodicScheduleSet { get; set; }
        public DbSet<EquityStockComputationResult> EquityStockComputationResultSet { get; set; }
        public DbSet<LoanPeriodicSchedule> LoanPeriodicScheduleSet { get; set; }
        public DbSet<LoanSchedule> LoanScheduleSet { get; set; }
        public DbSet<LoansImpairmentResult> LoansImpairmentResultSet { get; set; }
        public DbSet<TBillsComputationResult> TBillsComputationResultSet { get; set; }
        public DbSet<IFRSBudget> IFRSBudgetSet { get; set; }
        public DbSet<LoanPry> LoanPryDataSet { get; set; }
        public DbSet<RawLoanDetails> LoanDetailsSet { get; set; }
        public DbSet<IndividualImpairment> IndividualImpairmentSet { get; set; }
        public DbSet<IndividualSchedule> IndividualScheduleSet { get; set; }
        public DbSet<IntegralFee> IntegralFeeSet { get; set; }
        public DbSet<LoanIRRData> LoanIRRDataSet { get; set; }
        public DbSet<IfrsCustomer> IfrsCustomerSet { get; set; }
        public DbSet<IfrsCustomerAccount> IfrsCustomerAccountSet { get; set; }
        public DbSet<LoanPryMoratorium> LoanPryMoratoriumDataSet { get; set; }
        public DbSet<Borrowings> BorrowingsDataSet { get; set; }
        public DbSet<BorrowingPeriodicSchedule> BorrowingPeriodicScheduleSet { get; set; }
        public DbSet<BorrowingSchedule> BorrowingScheduleSet { get; set; }

        public DbSet<ChartOfAccount> ChartOfAccountSet { get; set; }
        public DbSet<Currency> CurrencySet { get; set; }
        public DbSet<FiscalYear> FiscalYearSet { get; set; }
        public DbSet<Branch> BranchSet { get; set; }
        public DbSet<Product> ProductSet { get; set; }
        public DbSet<FiscalPeriod> FiscalPeriodSet { get; set; }
        public DbSet<FinancialType> FinancialTypeSet { get; set; }
        public DbSet<GLDefinition> GLDefinitionSet { get; set; }
        public DbSet<RevenueGLMapping> RevenueGLMappingSet { get; set; }

        public DbSet<QualitativeNote> QualitativeNoteSet { get; set; }

        public DbSet<IFRSReportPack> IFRSReportPackSet { get; set; }
        public DbSet<IFRSRevacctRegistry> IFRSRevacctRegistrySet { get; set; }
        public DbSet<Placement> PlacementSet { get; set; }
        public DbSet<LoanInterestRate> LoanInterestRateSet { get; set; }


        //IFRS9

        public DbSet<ExternalRating> ExternalRatingSet { get; set; }
        public DbSet<HistoricalSectorRating> HistoricalSectorRatingSet { get; set; }
        public DbSet<InternalRatingBased> InternalRatingBasedSet { get; set; }
        public DbSet<MacroEconomic> MacroEconomicSet { get; set; }
        public DbSet<RatingMapping> RatingMappingSet { get; set; }
        public DbSet<Transition> TransitionSet { get; set; }
        public DbSet<Sector> SectorSet { get; set; }
        public DbSet<HistoricalClassification> HistoricalClassificationSet { get; set; }
        public DbSet<MacroEconomicHistorical> MacroEconomicHistoricalSet { get; set; }
        public DbSet<NotchDifference> NotchDifferenceSet { get; set; }
        public DbSet<SectorialRegressedPD> SectorialRegressedPDSet { get; set; }
        public DbSet<SectorialRegressedLGD> SectorialRegressedLGDSet { get; set; }
        //public DbSet<ComputedForcastedPDLGD> ComputedForcastedPDLGDSet { get; set; }
        public DbSet<HistoricalSectorialPD> HistoricalSectorialPDSet { get; set; }
        public DbSet<MacroEconomicVariable> MacroEconomicVariableSet { get; set; }
        public DbSet<SectorVariableMapping> SectorVariableMappingSet { get; set; }

        public DbSet<PitFormular> PitFormularSet { get; set; }

        public DbSet<PortfolioExposure> PortfolioExposureSet { get; set; }

        public DbSet<SectorialExposure> SectorialExposureSet { get; set; }

        public DbSet<PiTPD> PiTPDSet { get; set; }
        public DbSet<EclCalculationModel> EclCalculationModelSet { get; set; }

        public DbSet<LoanBucketDistribution> LoanBucketDistributionSet { get; set; }

        public DbSet<MacroeconomicVDisplay> MacroeconomicVDisplaySet { get; set; }

        public DbSet<LifeTimePDClassification> LifeTimePDClassificationSet { get; set; }


        public DbSet<FairValuationModel> FairValuationModelSet { get; set; }

        public DbSet<SummaryReport> SummaryReportSet { get; set; }

        public DbSet<IfrsStocksPrimaryData> IfrsStocksPrimaryDataSet { get; set; }

        public DbSet<IfrsStocksMapping> IfrsStocksMappingSet { get; set; }

        public DbSet<IfrsEquityUnqouted> IfrsEquityUnqoutedSet { get; set; }

        public DbSet<Reconciliation> ReconciliationSet { get; set; }

        public DbSet<ForecastedMacroeconimcsSensitivity> ForecastedMacroeconimcsSensitivitySet { get; set; }

        public DbSet<BucketExposure> BucketExposureSet { get; set; }

        public DbSet<ForecastedMacroeconimcsScenario> ForecastedMacroeconimcsScenarioSet { get; set; }

        public DbSet<LoanSpreadSensitivity> LoanSpreadSensitivitySet { get; set; }

        public DbSet<LoanSpreadScenario> LoanSpreadScenarioSet { get; set; }

        public DbSet<UnquotedEquityFairvalueResult> UnquotedEquityFairvalueResultSet { get; set; }
        public DbSet<PiTPDComparism> PiTPDComparismSet { get; set; }

        public DbSet<MarkovMatrix> MarkovMatrixSet { get; set; }

        public DbSet<CCFModelling> CCFModellingSet { get; set; }

        public DbSet<HistoricalSectorialLGD> HistoricalSectorialLGDSet { get; set; }

        public DbSet<ECLComparism> ECLComparismSet { get; set; }

        public DbSet<ForeignEADExchangeRate> ForeignEADExchangeRateSet { get; set; }
        public DbSet<OffBalanceSheetExposure> OffBalanceSheetExposureSet { get; set; }

        //Begin Victor IFRS9 DataSet Segment
        public DbSet<LocalBondSpread> LocalBondSpreadSet { get; set; }
        public DbSet<MarginalPDDistribution> MarginalPDDistributionSet { get; set; }
        public DbSet<BondMarginalPDDistribution> BondMarginalPDDistributionSet { get; set; }
        public DbSet<MarginalPDDistributionPlacement> MarginalPDDistributionPlacementSet { get; set; }
        public DbSet<LgdComputationResult> LgdComputationResultSet { get; set; }
        public DbSet<PlacementComputationResult> PlacementComputationResultSet { get; set; }
        public DbSet<EclComputationResult> FinalEclOutputSet { get; set; }
        public DbSet<BondEclComputationResult> BondEclComputationResultSet { get; set; }
        public DbSet<PlacementEclComputationResult> PlacementEclComputationResultSet { get; set; }
        public DbSet<LcBgEclComputationResult> LcBgEclComputationResultSet { get; set; }
        public DbSet<EuroBondSpread> EuroBondSpreadSet { get; set; }
        //End Victor IFRS9 DataSet Segment

        public DbSet<ComputedForcastedPDLGD> ComputedForcastedPDLGDSet { get; set; }
        public DbSet<ComputedForcastedPDLGDForeign> ComputedForcastedPDLGDForeignSet { get; set; }
        public DbSet<MacroEconomicsNPL> MacroEconomicsNPLSet { get; set; }
        public DbSet<MonthlyDiscountFactor> MonthlyDiscountFactorSet { get; set; }
        public DbSet<MonthlyDiscountFactorPlacement> MonthlyDiscountFactorPlacementSet { get; set; }
        public DbSet<MonthlyDiscountFactorBond> MonthlyDiscountFactorBondSet { get; set; }
        public DbSet<ProbabilityWeighted> ProbabilityWeightedSet { get; set; }
        public DbSet<MacrovariableEstimate> MacrovariableEstimateSet { get; set; }
        public DbSet<SectorMapping> SectorMappingSet { get; set; }
        public DbSet<InvestmentOthersECL> InvestmentOthersECLSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Ignore<PropertyChangedEventHandler>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();

            //IFRSContext


            //IFRS
            //DerivedCaption
            modelBuilder.Entity<DerivedCaption>().HasKey<int>(e => e.DerivedCaptionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<DerivedCaption>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<DerivedCaption>().ToTable("ifrs_derivedcaption");

            //GLMapping
            modelBuilder.Entity<GLMapping>().HasKey<int>(e => e.GLMappingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<GLMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<GLMapping>().ToTable("ifrs_glmapping");

            //InstrumentType
            modelBuilder.Entity<InstrumentType>().HasKey<int>(e => e.InstrumentTypeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<InstrumentType>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<InstrumentType>().ToTable("ifrs_instrumentType");

            //GLType
            modelBuilder.Entity<GLType>().HasKey<int>(e => e.GLTypeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<GLType>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<GLType>().ToTable("ifrs_gltype");

            //InstrumentTypeGLMap
            modelBuilder.Entity<InstrumentTypeGLMap>().HasKey<int>(e => e.InstrumentTypeGLMapId).Ignore(e => e.EntityId);
            modelBuilder.Entity<InstrumentTypeGLMap>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<InstrumentTypeGLMap>().ToTable("ifrs_instrumenttypeglmap");

            //AutoPostingTemplate
            modelBuilder.Entity<AutoPostingTemplate>().HasKey<int>(e => e.AutoPostingTemplateId).Ignore(e => e.EntityId);
            modelBuilder.Entity<AutoPostingTemplate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<AutoPostingTemplate>().ToTable("ifrs_autopostingtemplate");

            //TrialBalanceGap
            modelBuilder.Entity<TrialBalanceGap>().HasKey<int>(e => e.TrialBalanceGAPId).Ignore(e => e.EntityId);
            modelBuilder.Entity<TrialBalanceGap>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TrialBalanceGap>().ToTable("ifrs_trialbalancegap");

            //GLAdjustment
            modelBuilder.Entity<GLAdjustment>().HasKey<int>(e => e.GLAdjustmentId).Ignore(e => e.EntityId);
            modelBuilder.Entity<GLAdjustment>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<GLAdjustment>().ToTable("ifrs_gladjustment");

            //PostingDetail
            modelBuilder.Entity<PostingDetail>().HasKey<int>(e => e.PostingDetailId).Ignore(e => e.EntityId);
            modelBuilder.Entity<PostingDetail>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PostingDetail>().ToTable("ifrs_postingdetail");

            //TrialBalance
            modelBuilder.Entity<TrialBalance>().HasKey<int>(e => e.TrialBalanceId).Ignore(e => e.EntityId);
            modelBuilder.Entity<TrialBalance>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TrialBalance>().ToTable("ifrs_trialbalance");

            //IFRSReport
            modelBuilder.Entity<IFRSReport>().HasKey<int>(e => e.IFRSReportId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IFRSReport>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IFRSReport>().ToTable("ifrs_report");

            //TransactionDetail
            modelBuilder.Entity<TransactionDetail>().HasKey<int>(e => e.TransactionDetailId).Ignore(e => e.EntityId);
            modelBuilder.Entity<TransactionDetail>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TransactionDetail>().ToTable("ifrs_transactiondetail");

            //IFRSRegistry
            modelBuilder.Entity<IFRSRegistry>().HasKey<int>(e => e.RegistryId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IFRSRegistry>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IFRSRegistry>().ToTable("ifrs_registry");

            //LoanSetup
            modelBuilder.Entity<LoanSetup>().HasKey<int>(e => e.LoanSetupId).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanSetup>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanSetup>().ToTable("ifrs_loan_setup");

            //ScheduleType
            modelBuilder.Entity<ScheduleType>().HasKey<int>(e => e.ScheduleTypeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ScheduleType>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ScheduleType>().ToTable("ifrs_schedule_type");

            //IFRSProduct
            modelBuilder.Entity<IFRSProduct>().HasKey<int>(e => e.ProductId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IFRSProduct>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IFRSProduct>().ToTable("ifrs_product");

            //CreditRiskRating
            modelBuilder.Entity<CreditRiskRating>().HasKey<int>(e => e.CreditRiskRatingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CreditRiskRating>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CreditRiskRating>().ToTable("ifrs_credit_risk_rating");

            // CollateralCategory
            modelBuilder.Entity<CollateralCategory>().HasKey<int>(e => e.CollateralCategoryId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CollateralCategory>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CollateralCategory>().ToTable("ifrs_collateral_category");

            //CollateralType
            modelBuilder.Entity<CollateralType>().HasKey<int>(e => e.CollateralTypeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CollateralType>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CollateralType>().ToTable("ifrs_collateral_type");

            //CollateralRealizationPeriod
            modelBuilder.Entity<CollateralRealizationPeriod>().HasKey<int>(e => e.CollateralRealizationPeriodId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CollateralRealizationPeriod>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CollateralRealizationPeriod>().ToTable("ifrs_collateral_realization_period");

            //CollateralInformation
            modelBuilder.Entity<CollateralInformation>().HasKey<int>(e => e.CollateralInformationId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CollateralInformation>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CollateralInformation>().ToTable("ifrs_collateral_information");

            //WatchlistedLoan
            modelBuilder.Entity<WatchListedLoan>().HasKey<int>(e => e.WatchListedLoanId).Ignore(e => e.EntityId);
            modelBuilder.Entity<WatchListedLoan>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<WatchListedLoan>().ToTable("ifrs_watchlisted_loan");

            //ImpairmentOverride
            modelBuilder.Entity<ImpairmentOverride>().HasKey<int>(e => e.ImpairmentOverrideId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ImpairmentOverride>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ImpairmentOverride>().ToTable("ifrs_impairment_override");

            //FairValueBasisMap
            modelBuilder.Entity<FairValueBasisMap>().HasKey<int>(e => e.FairValueBasisMapId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FairValueBasisMap>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FairValueBasisMap>().ToTable("ifrs_fair_value_basis_map");

            //FairValueBasisExemption
            modelBuilder.Entity<FairValueBasisExemption>().HasKey<int>(e => e.FairValueBasisExemptionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FairValueBasisExemption>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FairValueBasisExemption>().ToTable("ifrs_fair_value_basis_exemption");

            //BondComputation
            modelBuilder.Entity<BondComputation>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<BondComputation>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BondComputation>().ToTable("ifrs_bond_computation_result");

            //BondComputationResultZero
            modelBuilder.Entity<BondComputationResultZero>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<BondComputationResultZero>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BondComputationResultZero>().ToTable("ifrs_bond_computation_result_zero");

            //BondPeriodicSchedule
            modelBuilder.Entity<BondPeriodicSchedule>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<BondPeriodicSchedule>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BondPeriodicSchedule>().ToTable("ifrs_bond_periodic_schedule");

            //EquityStockComputationResult
            modelBuilder.Entity<EquityStockComputationResult>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<EquityStockComputationResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<EquityStockComputationResult>().ToTable("ifrs_equity_stock_computation_result");

            //LoanPeriodicSchedule
            modelBuilder.Entity<LoanPeriodicSchedule>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanPeriodicSchedule>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanPeriodicSchedule>().ToTable("ifrs_loan_periodic_schedule");

            //LoanSchedule
            modelBuilder.Entity<LoanSchedule>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanSchedule>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanSchedule>().ToTable("ifrs_loan_schedule");

            //LoansImpairmentResult
            modelBuilder.Entity<LoansImpairmentResult>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoansImpairmentResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoansImpairmentResult>().ToTable("ifrs_loans_impairment_result");

            //TBillsComputationResult
            modelBuilder.Entity<TBillsComputationResult>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<TBillsComputationResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TBillsComputationResult>().ToTable("ifrs_tbills_computation_result");

            //IFRSBudget
            modelBuilder.Entity<IFRSBudget>().HasKey<int>(e => e.IFRSBudgetId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IFRSBudget>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IFRSBudget>().ToTable("ifrs_budget");

            //LoanPrimaryData
            //modelBuilder.Entity<LoanPrimaryData>().HasKey<int>(e => e.PryId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<LoanPrimaryData>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<LoanPrimaryData>().ToTable("ifrs_loan_primary_data");

            //LoanDetails
            //modelBuilder.Entity<LoanDetails>().HasKey<int>(e => e.LoanDetailId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<LoanDetails>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<LoanDetails>().ToTable("ifrs_loans_details");

            //IndividualImpairment
            modelBuilder.Entity<IndividualImpairment>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IndividualImpairment>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IndividualImpairment>().ToTable("ifrs_individual_impairment");

            //IndividualSchedule
            modelBuilder.Entity<IndividualSchedule>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IndividualSchedule>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IndividualSchedule>().ToTable("ifrs_individual_schedule");

            //IntegralFee
            modelBuilder.Entity<IntegralFee>().HasKey<int>(e => e.IntegralFeeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IntegralFee>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IntegralFee>().ToTable("ifrs_integral_fee");

            //LoanIRRData
            modelBuilder.Entity<LoanIRRData>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanIRRData>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanIRRData>().ToTable("ifrs_loan_irr_data");

            //IfrsCustomer
            modelBuilder.Entity<IfrsCustomer>().HasKey<int>(e => e.CustomerId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsCustomer>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsCustomer>().ToTable("ifrs_customer");

            //IfrsCustomerAccount
            modelBuilder.Entity<IfrsCustomerAccount>().HasKey<int>(e => e.CustAccountId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsCustomerAccount>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsCustomerAccount>().ToTable("ifrs_customer_account");

            //BorrowingPeriodicSchedule
            modelBuilder.Entity<BorrowingPeriodicSchedule>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<BorrowingPeriodicSchedule>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BorrowingPeriodicSchedule>().ToTable("ifrs_borrowings_periodic_schedule");

            //BorrowingSchedule
            modelBuilder.Entity<BorrowingSchedule>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<BorrowingSchedule>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BorrowingSchedule>().ToTable("ifrs_borrowings_schedule");

            //BorrowingSchedule
            modelBuilder.Entity<Borrowings>().HasKey<int>(e => e.BorrowingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Borrowings>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Borrowings>().ToTable("ifrs_borrowings_primary_data");

            //ChartOfAccount
            modelBuilder.Entity<ChartOfAccount>().HasKey<int>(e => e.ChartOfAccountId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ChartOfAccount>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ChartOfAccount>().ToTable("cor_chartofacct");

            //Currency
            modelBuilder.Entity<Currency>().HasKey<int>(e => e.CurrencyId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Currency>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Currency>().ToTable("cor_currency");

            //FiscalYear
            modelBuilder.Entity<FiscalYear>().HasKey<int>(e => e.FiscalYearId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FiscalYear>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FiscalYear>().ToTable("cor_fiscalyear");

            //Branch
            modelBuilder.Entity<Branch>().HasKey<int>(e => e.BranchId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Branch>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Branch>().ToTable("cor_branch");

            //Product
            modelBuilder.Entity<Product>().HasKey<int>(e => e.ProductId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Product>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Product>().ToTable("cor_product");

            //FiscalPeriod
            modelBuilder.Entity<FiscalPeriod>().HasKey<int>(e => e.FiscalPeriodId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FiscalPeriod>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FiscalPeriod>().ToTable("cor_fiscalperiod");

            //FinancialType
            modelBuilder.Entity<FinancialType>().HasKey<int>(e => e.FinancialTypeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FinancialType>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FinancialType>().ToTable("cor_financial_type");

            //GLDefinition
            modelBuilder.Entity<GLDefinition>().HasKey<int>(e => e.GLDefinitionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<GLDefinition>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<GLDefinition>().ToTable("cor_gl_definition");


            //ifrs_bonds
            modelBuilder.Entity<IFRSBonds>().HasKey<int>(e => e.BondId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IFRSBonds>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IFRSBonds>().ToTable("ifrs_bonds");

            //ifrs_tbills
            modelBuilder.Entity<IFRSTbills>().HasKey<int>(e => e.TbillId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IFRSTbills>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IFRSTbills>().ToTable("ifrs_tbills");

            //LoanPrimaryData
            modelBuilder.Entity<LoanPry>().HasKey<int>(e => e.PryId).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanPry>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanPry>().ToTable("ifrs_loan_primary_data");

            //LoanPrimaryMoratoriumData
            modelBuilder.Entity<LoanPryMoratorium>().HasKey<int>(e => e.LoanPryMoratoriumId).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanPryMoratorium>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanPryMoratorium>().ToTable("ifrs_loan_primary_moratorium");

            //RawLoanDetails
            modelBuilder.Entity<RawLoanDetails>().HasKey<int>(e => e.LoanDetailId).Ignore(e => e.EntityId);
            modelBuilder.Entity<RawLoanDetails>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<RawLoanDetails>().ToTable("ifrs_loans_details");

            //RevenueGLMapping
            modelBuilder.Entity<RevenueGLMapping>().HasKey<int>(e => e.GLMappingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<RevenueGLMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<RevenueGLMapping>().ToTable("ifrs_revacct_glmapping");

            //QualitativeNote
            modelBuilder.Entity<QualitativeNote>().HasKey<int>(e => e.QualitativeNoteId).Ignore(e => e.EntityId);
            modelBuilder.Entity<QualitativeNote>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<QualitativeNote>().ToTable("ifrs_captions_qualitative");

            //IFRSReportPack
            modelBuilder.Entity<IFRSReportPack>().HasKey<int>(e => e.ReportPackId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IFRSReportPack>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IFRSReportPack>().ToTable("ifrs_report_pack");


            //IFRSReportPack
            modelBuilder.Entity<IFRSReportPack>().HasKey<int>(e => e.ReportPackId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IFRSReportPack>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IFRSReportPack>().ToTable("ifrs_report_pack");


            //IFRSRevacctRegistry
            modelBuilder.Entity<IFRSRevacctRegistry>().HasKey<int>(e => e.RevenueId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IFRSRevacctRegistry>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IFRSRevacctRegistry>().ToTable("ifrs_revacct_registry");


            //Placement
            modelBuilder.Entity<Placement>().HasKey<int>(e => e.Placement_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<Placement>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Placement>().ToTable("ifrs_placement");


            //LoanInterestRate
            modelBuilder.Entity<LoanInterestRate>().HasKey<int>(e => e.LoanInterestRate_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanInterestRate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanInterestRate>().ToTable("ifrs_loan_interest_rate");


            //IFRS9
            //ExternalRating
            modelBuilder.Entity<ExternalRating>().HasKey<int>(e => e.ExternalRatingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ExternalRating>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ExternalRating>().ToTable("ifrs_external_rating");

            //HistoricalSectorRating
            modelBuilder.Entity<HistoricalSectorRating>().HasKey<int>(e => e.HistoricalSectorRatingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<HistoricalSectorRating>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<HistoricalSectorRating>().ToTable("ifrs_sectorial_last_reporting_rating");

            //InternalRatingBased
            modelBuilder.Entity<InternalRatingBased>().HasKey<int>(e => e.InternalRatingBasedId).Ignore(e => e.EntityId);
            modelBuilder.Entity<InternalRatingBased>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<InternalRatingBased>().ToTable("ifrs_internal_rating_based");

            ////MacroEconomic
            //modelBuilder.Entity<MacroEconomic>().HasKey<int>(e => e.MacroEconomicId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<MacroEconomic>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<MacroEconomic>().ToTable("Ifrs_forecasted_macroeconimcs");

            //RatingMapping
            modelBuilder.Entity<RatingMapping>().HasKey<int>(e => e.RatingMappingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<RatingMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<RatingMapping>().ToTable("ifrs_rating_mapping");

            //Transition
            modelBuilder.Entity<Transition>().HasKey<int>(e => e.TransitionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Transition>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Transition>().ToTable("ifrs_transition_matrix");

            //Sector
            modelBuilder.Entity<Sector>().HasKey<int>(e => e.SectorId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Sector>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Sector>().ToTable("ifrs_sector_listing");


            //HistoricalClassification
            modelBuilder.Entity<HistoricalClassification>().HasKey<int>(e => e.HistoricalClassificationId).Ignore(e => e.EntityId);
            modelBuilder.Entity<HistoricalClassification>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<HistoricalClassification>().ToTable("ifrs_loan_classification_historical_data");

            //MacroEconomicHistorical
            modelBuilder.Entity<MacroEconomicHistorical>().HasKey<int>(e => e.MacroEconomicHistoricalId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MacroEconomicHistorical>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MacroEconomicHistorical>().ToTable("Ifrs_forecasted_macroeconimcs");

            //NotchDifference
            modelBuilder.Entity<NotchDifference>().HasKey<int>(e => e.NotchDifferenceId).Ignore(e => e.EntityId);
            modelBuilder.Entity<NotchDifference>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<NotchDifference>().ToTable("ifrs_notch_differences");


            //SetUp
            modelBuilder.Entity<SetUp>().HasKey<int>(e => e.SetUpId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SetUp>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SetUp>().ToTable("ifrs_setup_data");

            //HistoricalSectorialPD
            modelBuilder.Entity<HistoricalSectorialPD>().HasKey<int>(e => e.HistoricalSectorialPDId).Ignore(e => e.EntityId);
            modelBuilder.Entity<HistoricalSectorialPD>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<HistoricalSectorialPD>().ToTable("ifrs_historical_sectorial_pd");

            //ComputedForcastedPDLGD
            modelBuilder.Entity<ComputedForcastedPDLGD>().HasKey<int>(e => e.ComputedPDId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ComputedForcastedPDLGD>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ComputedForcastedPDLGD>().ToTable("ifrs_computed_forcasted_pd_lgd");

            //SectorialRegressedLGD
            modelBuilder.Entity<SectorialRegressedLGD>().HasKey<int>(e => e.SectorialRegressedLGDId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SectorialRegressedLGD>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SectorialRegressedLGD>().ToTable("ifrs_sectorial_regressed_lgd");

            //SectorialRegressedPD
            modelBuilder.Entity<SectorialRegressedPD>().HasKey<int>(e => e.SectorialRegressedPDId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SectorialRegressedPD>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SectorialRegressedPD>().ToTable("ifrs_sectorial_regressed_pd");

            //MacroEconomicVariable
            modelBuilder.Entity<MacroEconomicVariable>().HasKey<int>(e => e.MacroEconomicVariableId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MacroEconomicVariable>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MacroEconomicVariable>().ToTable("ifrs_macroeconomic_variables");

            //SectorVariableMapping
            modelBuilder.Entity<SectorVariableMapping>().HasKey<int>(e => e.SectorVariableMappingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SectorVariableMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SectorVariableMapping>().ToTable("ifrs_sector_variable_mapping");


            //PitFormular
            modelBuilder.Entity<PitFormular>().HasKey<int>(e => e.PitFormularId).Ignore(e => e.EntityId);
            modelBuilder.Entity<PitFormular>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PitFormular>().ToTable("ifrs_pit_formula");

            //PortfolioExposure
            modelBuilder.Entity<PortfolioExposure>().HasKey<int>(e => e.PortfolioId).Ignore(e => e.EntityId);
            modelBuilder.Entity<PortfolioExposure>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PortfolioExposure>().ToTable("ifrs_dashboard_portfolio_exposure");

            //PortfolioExposure
            modelBuilder.Entity<SectorialExposure>().HasKey<int>(e => e.SectorialExposureId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SectorialExposure>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SectorialExposure>().ToTable("ifrs_dashboard_sectorial_exposure");

            modelBuilder.Entity<EclCalculationModel>().HasKey<int>(e => e.EclModelId).Ignore(e => e.EntityId);
            modelBuilder.Entity<EclCalculationModel>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<EclCalculationModel>().ToTable("ifrs_ecl_models");

            //PiTPD
            modelBuilder.Entity<PiTPD>().HasKey<int>(e => e.PiTPDId).Ignore(e => e.EntityId);
            modelBuilder.Entity<PiTPD>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PiTPD>().ToTable("ifrs_pitpds");

            //LoanBucketDistribution
            modelBuilder.Entity<LoanBucketDistribution>().HasKey<int>(e => e.LoanBucketDistributionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanBucketDistribution>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanBucketDistribution>().ToTable("ifrs_loan_classification_spread");

            //ifrs_macro_variable_display
            modelBuilder.Entity<MacroeconomicVDisplay>().HasKey<int>(e => e.MacroVariableDisplayId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MacroeconomicVDisplay>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MacroeconomicVDisplay>().ToTable("ifrs_macro_variable_display");

            //ifrs_macro_variable_display
            modelBuilder.Entity<LifeTimePDClassification>().HasKey<int>(e => e.LifeTimePDClassificationId).Ignore(e => e.EntityId);
            modelBuilder.Entity<LifeTimePDClassification>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LifeTimePDClassification>().ToTable("ifrs_loan_classification_notch_diff");

            //ifrs_macro_variable_display
            modelBuilder.Entity<SummaryReport>().HasKey<int>(e => e.SummaryReportId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SummaryReport>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SummaryReport>().ToTable("ifrs_assessment_summary_report");

            //ifrs_macro_variable_display
            modelBuilder.Entity<FairValuationModel>().HasKey<int>(e => e.FairValueModelId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FairValuationModel>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FairValuationModel>().ToTable("ifrs_fairvaluation_models");


            //IfrsStocksPrimaryData
            modelBuilder.Entity<IfrsStocksPrimaryData>().HasKey<int>(e => e.IfrsStocksPrimaryDataId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsStocksPrimaryData>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsStocksPrimaryData>().ToTable("ifrs_stocks_primary_data");

            //IfrsStocksMapping
            modelBuilder.Entity<IfrsStocksMapping>().HasKey<int>(e => e.IfrsStocksMappingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsStocksMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsStocksMapping>().ToTable("ifrs_stocks_mapping");

            //IfrsEquityUnqouted
            modelBuilder.Entity<IfrsEquityUnqouted>().HasKey<int>(e => e.IfrsEquityUnqoutedId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IfrsEquityUnqouted>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IfrsEquityUnqouted>().ToTable("ifrs_equity_unqouted");

            //Reconciliation
            modelBuilder.Entity<Reconciliation>().HasKey<int>(e => e.ReconciliationId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Reconciliation>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Reconciliation>().ToTable("ifrs_reconciliation");

            //ForecastedMacroeconimcsSensitivity
            modelBuilder.Entity<ForecastedMacroeconimcsSensitivity>().HasKey<int>(e => e.ForecastedMacroeconimcsSensitivityId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ForecastedMacroeconimcsSensitivity>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ForecastedMacroeconimcsSensitivity>().ToTable("Ifrs_forecasted_macroeconimcs_Sensitivity");

            //BucketExposure
            modelBuilder.Entity<BucketExposure>().HasKey<int>(e => e.BucketExposureId).Ignore(e => e.EntityId);
            modelBuilder.Entity<BucketExposure>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BucketExposure>().ToTable("ifrs_dashboard_bucket_exposure");


            //ForecastedMacroeconimcsScenario
            modelBuilder.Entity<ForecastedMacroeconimcsScenario>().HasKey<int>(e => e.ForecastedMacroeconimcsScenarioId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ForecastedMacroeconimcsScenario>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ForecastedMacroeconimcsScenario>().ToTable("Ifrs_forecasted_macroeconimcs_Scenario");

            //LoanSpreadSensitivity
            modelBuilder.Entity<LoanSpreadSensitivity>().HasKey<int>(e => e.LoanSpreadSensitivityId).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanSpreadSensitivity>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanSpreadSensitivity>().ToTable("ifrs_loan_classification_spread_Sensitivity");


            //LoanSpreadScenario
            modelBuilder.Entity<LoanSpreadScenario>().HasKey<int>(e => e.LoanSpreadScenarioId).Ignore(e => e.EntityId);
            modelBuilder.Entity<LoanSpreadScenario>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LoanSpreadScenario>().ToTable("ifrs_loan_classification_spread_Scenario");

            //UnquotedEquityFairvalueResult
            modelBuilder.Entity<UnquotedEquityFairvalueResult>().HasKey<int>(e => e.UnquotedEquityFairvalueResultId).Ignore(e => e.EntityId);
            modelBuilder.Entity<UnquotedEquityFairvalueResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<UnquotedEquityFairvalueResult>().ToTable("ifrs_unquotedequity_fairvalue_result");

            //PiTPDComparism
            modelBuilder.Entity<PiTPDComparism>().HasKey<int>(e => e.ComparismPDId).Ignore(e => e.EntityId);
            modelBuilder.Entity<PiTPDComparism>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PiTPDComparism>().ToTable("ifrs_pitpd_comparism");


            //MarkovMatrix
            modelBuilder.Entity<MarkovMatrix>().HasKey<int>(e => e.MarkovMatrixId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MarkovMatrix>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MarkovMatrix>().ToTable("ifrs_pit_markov_matrix");

            //MarkovMatrix
            modelBuilder.Entity<CCFModelling>().HasKey<int>(e => e.CCFModellingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CCFModelling>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CCFModelling>().ToTable("ifrs_ccf_modelling");

            //MarkovMatrix
            modelBuilder.Entity<HistoricalSectorialLGD>().HasKey<int>(e => e.HistoricalSectorialLGDId).Ignore(e => e.EntityId);
            modelBuilder.Entity<HistoricalSectorialLGD>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<HistoricalSectorialLGD>().ToTable("ifrs_historical_sectorial_lgd");


            //ECLComparism
            modelBuilder.Entity<ECLComparism>().HasKey<int>(e => e.ECLComparismId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ECLComparism>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ECLComparism>().ToTable("ifrs_ecl_comparism");

            //OffBalanceSheetExposure
            modelBuilder.Entity<OffBalanceSheetExposure>().HasKey<int>(e => e.ObeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<OffBalanceSheetExposure>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OffBalanceSheetExposure>().ToTable("ifrs_lc_bg");
            //OffBalanceSheetExposure
            modelBuilder.Entity<ForeignEADExchangeRate>().HasKey<int>(e => e.ForeignEADExchangeRateId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ForeignEADExchangeRate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ForeignEADExchangeRate>().ToTable("Ifrs_forecasted_macroeconimcs_InterestRate");

            //Begin Victor IFRS9 ORM Segment

            //LocalBondSpread
            modelBuilder.Entity<LocalBondSpread>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<LocalBondSpread>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LocalBondSpread>().ToTable("ifrs_Local_bond_spread");

            //MarginalPDDistribution
            modelBuilder.Entity<MarginalPDDistribution>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<MarginalPDDistribution>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MarginalPDDistribution>().ToTable("ifrs_MarginalPD_distribution");

            //BondMarginalPDDistribution
            modelBuilder.Entity<BondMarginalPDDistribution>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<BondMarginalPDDistribution>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BondMarginalPDDistribution>().ToTable("ifrs_Bond_MarginalPD_distribution");

            //MarginalPDDistributionPlacement
            modelBuilder.Entity<MarginalPDDistributionPlacement>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<MarginalPDDistributionPlacement>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MarginalPDDistributionPlacement>().ToTable("ifrs_MarginalPD_distribution_placement");

            //LgdComputationResult
            modelBuilder.Entity<LgdComputationResult>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<LgdComputationResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LgdComputationResult>().ToTable("ifrs_lgd_computation_result");

            //PlacementComputationResult
            modelBuilder.Entity<PlacementComputationResult>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<PlacementComputationResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PlacementComputationResult>().ToTable("ifrs_placement_computation_result");

            //TrialBalanceGap
            modelBuilder.Entity<EclComputationResult>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<EclComputationResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<EclComputationResult>().ToTable("ifrs_ecl_computation_result");

            //FinalEclOutputBonds
            modelBuilder.Entity<BondEclComputationResult>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<BondEclComputationResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BondEclComputationResult>().ToTable("ifrs_bond_ecl_computation_result");

            //FinalEclOutputPlacement
            modelBuilder.Entity<PlacementEclComputationResult>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<PlacementEclComputationResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PlacementEclComputationResult>().ToTable("ifrs_placement_ecl_computation_result");

            //FinalEclOutputLcBg
            modelBuilder.Entity<LcBgEclComputationResult>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<LcBgEclComputationResult>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LcBgEclComputationResult>().ToTable("ifrs_lc_bg_ecl_computation_result");

            //EuroBondSpread
            modelBuilder.Entity<EuroBondSpread>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<EuroBondSpread>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<EuroBondSpread>().ToTable("ifrs_Euro_bond_spread");

            //End Victor IFRS9 ORM Segment
             //ComputedForcastedPDLGD
            modelBuilder.Entity<ComputedForcastedPDLGD>().HasKey<int>(e => e.ComputedPDId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ComputedForcastedPDLGD>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ComputedForcastedPDLGD>().ToTable("Ifrs_computed_forcasted_pd_lgd_local");

            //ComputedForcastedPDLGDForeign
            modelBuilder.Entity<ComputedForcastedPDLGDForeign>().HasKey<int>(e => e.ComputedPDId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ComputedForcastedPDLGDForeign>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ComputedForcastedPDLGDForeign>().ToTable("Ifrs_computed_forcasted_pd_lgd_foreign");


            //MacroEconomicsNPL
            modelBuilder.Entity<MacroEconomicsNPL>().HasKey<int>(e => e.macroeconomicnplId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MacroEconomicsNPL>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MacroEconomicsNPL>().ToTable("ifrs_MacroEconomics_NPL");


            //MonthlyDiscountFactorBond
            modelBuilder.Entity<MonthlyDiscountFactorBond>().HasKey<int>(e => e.MonthlyDiscountFactorBond_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<MonthlyDiscountFactorBond>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MonthlyDiscountFactorBond>().ToTable("ifrs_monthly_discount_factor_bond");


            //MonthlyDiscountFactorPlacement
            modelBuilder.Entity<MonthlyDiscountFactorPlacement>().HasKey<int>(e => e.MonthlyDiscountFactorPlacement_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<MonthlyDiscountFactorPlacement>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MonthlyDiscountFactorPlacement>().ToTable("ifrs_monthly_discount_factor_placement");


            //MonthlyDiscountFactor
            modelBuilder.Entity<MonthlyDiscountFactor>().HasKey<int>(e => e.MonthlyDiscountFactor_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<MonthlyDiscountFactor>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MonthlyDiscountFactor>().ToTable("ifrs_monthly_discount_factor");


            //ProbabilityWeighted
            modelBuilder.Entity<ProbabilityWeighted>().HasKey<int>(e => e.ProbabilityWeighted_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<ProbabilityWeighted>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ProbabilityWeighted>().ToTable("ifrs_probability_Weighted");


            //MacrovariableEstimate
            modelBuilder.Entity<MacrovariableEstimate>().HasKey<int>(e => e.MacrovariableEstimate_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<MacrovariableEstimate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MacrovariableEstimate>().ToTable("ifrs_macrovariable_estimates");


            //SectorMapping
            modelBuilder.Entity<SectorMapping>().HasKey<int>(e => e.SectorMapping_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<SectorMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SectorMapping>().ToTable("ifrs_sector_mapping");


            //InvestmentOthersECL
            modelBuilder.Entity<InvestmentOthersECL>().HasKey<int>(e => e.ecl_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<InvestmentOthersECL>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<InvestmentOthersECL>().ToTable("ifrs_investment_and_others_ecl");
       
        }



        public override int SaveChanges()
        {
            try
            {
                if (ChangeTracker.HasChanges())
                {
                    var entries = this.ChangeTracker.Entries();

                    foreach (DbEntityEntry entry in entries)
                    {
                        if (entry.Entity != null)
                        {
                            if (entry.State == EntityState.Added)
                            {
                                //entry is Added 

                                var model = (EntityBase)entry.Entity;
                                model.CreatedBy = DataConnector.LoginName;
                                model.CreatedOn = DateTime.Now;
                                model.UpdatedBy = DataConnector.LoginName;
                                model.UpdatedOn = DateTime.Now;
                            }
                            else if (entry.State == EntityState.Deleted)
                            {
                                //entry in deleted

                            }
                            else
                            {
                                //entry is modified
                                var model = (EntityBase)entry.Entity;
                                model.UpdatedBy = DataConnector.LoginName;
                                model.UpdatedOn = DateTime.Now;
                            }

                            _auditManager.AddAudit(entry, DataConnector.LoginName);
                        }
                    }
                }

                _auditManager.Save();

                return base.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                var innerEx = e.InnerException;
                while (innerEx.InnerException != null)
                    innerEx = innerEx.InnerException;

                throw new Exception(innerEx.Message);
            }
            catch (DbEntityValidationException e)
            {
                var sb = new StringBuilder();

                foreach (var entry in e.EntityValidationErrors)
                {
                    foreach (var error in entry.ValidationErrors)
                    {
                        sb.AppendLine(string.Format("{0}-{1}-{2}", entry.Entry.Entity, error.PropertyName, error.ErrorMessage));
                    }
                }

                throw new Exception(sb.ToString());
            }
            catch (Exception e)
            {
                var innerEx = e.InnerException;
                while (innerEx.InnerException != null)
                    innerEx = innerEx.InnerException;

                throw new Exception(innerEx.Message);
            }

        }

        public static string GetDataConnection()
        {
           
            string connectionString = "";
            if (!string.IsNullOrEmpty(DataConnector.CompanyCode) && !string.IsNullOrEmpty(SOLUTION_NAME))
            {
                systemContract.IDatabaseRepository databaseRepository = new systemCore.DatabaseRepository();
                var companydbs = databaseRepository.GetDatabases().Where(c => c.Database.CompanyCode == DataConnector.CompanyCode && (c.Solution.Name == SOLUTION_NAME || c.Solution.Name == "CORE"));

                DatabaseInfo companydb = null;

                if (companydbs == null)
                    throw new Exception("Unable to load database.");
                else
                {
                    companydb = companydbs.Where(c => c.Solution.Name == SOLUTION_NAME).FirstOrDefault();

                    if (companydb == null)
                        companydb = companydbs.FirstOrDefault();
                }

               
                connectionString = string.Format("Data Source= {0};Initial Catalog={1};User ={2};Password={3};Integrated Security={4}", companydb.Database.ServerName, companydb.Database.DatabaseName, companydb.Database.UserName, companydb.Database.Password, companydb.Database.IntegratedSecurity);
            }

            return connectionString;
        }

    }
}
