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
    public class MPRContext : DbContext
    {
        const string SOLUTION_NAME = "FIN_MPR";

        AuditManager _auditManager;

        public MPRContext()
            : base(GetDataConnection())
        {
            System.Data.Entity.Database.SetInitializer<MPRContext>(null);

            _auditManager = new AuditManager(GetDataConnection());

            //var objectContext = (this as IObjectContextAdapter).ObjectContext;
            //objectContext.CommandTimeout = 120;
        }

        public MPRContext(string connectionString)
            : base(connectionString)
        {
            System.Data.Entity.Database.SetInitializer<MPRContext>(null);
            _auditManager = new AuditManager(connectionString);

            //var objectContext = (this as IObjectContextAdapter).ObjectContext;
            //objectContext.CommandTimeout = 120;
        }

       

        //MPR
        public DbSet<UserMIS> UserMISSet { get; set; }
        public DbSet<UserClassificationMap> UserClassificationMapSet { get; set; }
        public DbSet<TeamDefinition> TeamDefinitionSet { get; set; }
        public DbSet<TeamClassificationType> TeamClassificationTypeSet { get; set; }
        public DbSet<TeamClassification> TeamClassificationSet { get; set; }
        public DbSet<Team> TeamSet { get; set; }
        public DbSet<TeamClassificationMap> TeamClassificationMapSet { get; set; }
        public DbSet<AccountOfficerDetail> AccountOfficerDetailSet { get; set; }
        public DbSet<BranchDefaultMIS> BranchDefaultMISSet { get; set; }
        public DbSet<ManagementTree> ManagementTreeSet { get; set; }
        public DbSet<AccountMIS> AccountMISSet { get; set; }
        public DbSet<MISReplacement> MISReplacementSet { get; set; }
        public DbSet<SetUp> SetUpSet { get; set; }
        public DbSet<Staffs> StaffSet { get; set; }
        public DbSet<BSCaption> BSCaptionSet { get; set; }
        public DbSet<MPRProduct> MPRProductSet { get; set; }
        public DbSet<NonProductMap> NonProductMapSet { get; set; }
        public DbSet<NonProductRate> NonProductRateSet { get; set; }
        public DbSet<ProductMIS> ProductMISSet { get; set; }
        public DbSet<BalanceSheetThreshold> BalanceSheetThresholdSet { get; set; }
        public DbSet<AccountTransferPrice> AccountTransferPriceSet { get; set; }
        public DbSet<TransferPrice> TransferPriceSet { get; set; }
        public DbSet<GeneralTransferPrice> GeneralTransferPriceSet { get; set; }
        public DbSet<BalanceSheetBudget> BalanceSheetBudgetSet { get; set; }
        public DbSet<BalanceSheetBudgetOfficer> BalanceSheetBudgetOfficerSet { get; set; }
        public DbSet<BSGLMapping> BSGLMappingSet { get; set; }
        public DbSet<MPRBalanceSheetAdjustment> MPRBalanceSheetAdjustmentSet { get; set; }
        public DbSet<MPRBalanceSheet> MPRBalanceSheetSet { get; set; }
        public DbSet<CustAccount> CustAccountSet { get; set; }
        public DbSet<MemoAccountMap> MemoAccountMapSet { get; set; }
        public DbSet<MemoGLMap> MemoGLMapSet { get; set; }
        public DbSet<MemoProductMap> MemoProductMapSet { get; set; }
        public DbSet<MemoUnits> MemoUnitsSet { get; set; }
        public DbSet<BSExemption> BSExemptionSet { get; set; }
        public DbSet<CaptionMapping> CaptionMappingSet { get; set; }
        public DbSet<MessagingSubscription> MessagingSubscriptionSet { get; set; }
        public DbSet<RatioCaptionMapping> RatioCaptionMappingSet { get; set; }
        public DbSet<Ratios> RatiosSet { get; set; }
        public DbSet<BSINOtherInformation> BSINOtherInformationSet { get; set; }
        public DbSet<BSINOtherInformationTotalLine> BSINOtherInformationTotalLineSet { get; set; }
        public DbSet<AbcRatio> AbcRatioSet { get; set; }
        public DbSet<Sbu> SbuSet { get; set; }
        public DbSet<SbuType> SbuTypeSet { get; set; }
        public DbSet<Servicese> ServicesSet { get; set; }
        public DbSet<NRFFCaption> NRFFCaptionSet { get; set; }


        //MPR_PL
        public DbSet<PLCaption> PLCaptionSet { get; set; }
        public DbSet<MPRGLMapping> MPRGLMappingSet { get; set; }
        public DbSet<GLReclassification> GLReclassificationSet { get; set; }
        public DbSet<GLException> GLExceptionSet { get; set; }
        public DbSet<MPRTotalLine> MPRTotalLineSet { get; set; }
        public DbSet<MPRTotalLineMakeUp> MPRTotalLineMakeUpSet { get; set; }
        public DbSet<GLMIS> GLMISSet { get; set; }
        public DbSet<PLIncomeReport> PLIncomeReportSet { get; set; }
        public DbSet<PLIncomeReportAdjustment> PLIncomeReportAdjustmentSet { get; set; }
        public DbSet<RevenueBudget> RevenueBudgetSet { get; set; }
        public DbSet<RevenueBudgetOfficer> RevenueBudgetOfficerSet { get; set; }
        public DbSet<Revenue> RevenueSet { get; set; }
        public DbSet<MPRPLDerivedCaption> MPRPLDerivedCaptionSet { get; set; }
        public DbSet<ProcessData> ProcessDataSet { get; set; }
        public DbSet<IncomeCentralVaultSchedule> IncomeCentralVaultScheduleSet { get; set; }
        public DbSet<MPRCommFee> MPRCommFeeSet { get; set; }

        //MPR_OPEX
        public DbSet<CostCentre> CostCentreSet { get; set; }
        public DbSet<CostCentreDefinition> CostCentreDefinitionSet { get; set; }
        public DbSet<ExpenseBasis> ExpenseBasisSet { get; set; }
        public DbSet<ExpenseMapping> ExpenseMappingSet { get; set; }
        public DbSet<ExpenseProductMapping> ExpenseProductMappingSet { get; set; }
        public DbSet<ExpenseGLMapping> ExpenseGLMappingSet { get; set; }
        public DbSet<ExpenseRawBasis> ExpenseRawBasisSet { get; set; }
        public DbSet<StaffCost> StaffCostSet { get; set; }
        public DbSet<OpexManagementTree> OpexManagementTreeSet { get; set; }
        public DbSet<ActivityBase> ActivityBaseSet { get; set; }
        public DbSet<ActivityBaseRatio> ActivityBaseRatioSet { get; set; }
        public DbSet<OpexMISReplacement> OpexMISReplacementSet { get; set; }
        public DbSet<OpexBusinessRule> OpexBusinessRuleSet { get; set; }
        public DbSet<OpexAbcExemption> OpexAbcExemptionSet { get; set; }
        public DbSet<OpexRawExpense> OpexRawExpenseSet { get; set; }
        public DbSet<OpexGLMapping> OpexGLMappingSet { get; set; }
        public DbSet<OpexReport> OpexReportSet { get; set; }
        public DbSet<OpexGLBasis> OpexGLBasisSet { get; set; }
        public DbSet<OpexBasisMapping> OpexBasisMappingSet { get; set; }
        public DbSet<CheckList> CheckList { get; set; }
        public DbSet<HoExemptionMISCode> HoExemptionMISCode { get; set; }
        public DbSet<FixedAssetSharingRatio> FixedAssetSharingRatioSet { get; set; }
        public DbSet<IncomeCashCentreCode> IncomeCashCentreCodeSet { get; set; }
        public DbSet<IncomeCentralVaultAccounts> IncomeCentralVaultAccountsSet { get; set; }
        public DbSet<IncomeNEAGLSBU> IncomeNEAGLSBUSet { get; set; }
        public DbSet<CategoryTransferPrice> CategoryTransferPriceSet { get; set; }
        public DbSet<LowCostRemap> LowCostRemapSet { get; set; }
        public DbSet<NEABranchSBUShares> NEABranchSBUSharesSet { get; set; }
        public DbSet<NEABranchSharingRatio> NEABranchSharingRatioSet { get; set; }
        public DbSet<NEASharingRatio> NEASharingRatioSet { get; set; }
        public DbSet<NEASharingRatioFcy> NEASharingRatioFcySet { get; set; }
        public DbSet<OpexBranchMapping> OpexBranchMappingSet { get; set; }


        public DbSet<ChartOfAccount> ChartOfAccountSet { get; set; }
        public DbSet<Currency> CurrencySet { get; set; }
        public DbSet<FiscalYear> FiscalYearSet { get; set; }
        public DbSet<Branch> BranchSet { get; set; }
        public DbSet<Product> ProductSet { get; set; }
        public DbSet<FiscalPeriod> FiscalPeriodSet { get; set; }
        public DbSet<FinancialType> FinancialTypeSet { get; set; }
        public DbSet<GLDefinition> GLDefinitionSet { get; set; }

        public DbSet<crb_Data> crb_Data { get; set; }
        public DbSet<account_interest> account_interestSet { get; set; }
        public DbSet<product_interest> product_interestSet { get; set; }
        public DbSet<TeamStructure> TeamStructureSet { get; set; }
        public DbSet<CorporateAdjustment> CorporateAdjustmentSet { get; set; }
        public DbSet<caption_transfer_price> caption_transfer_priceSet { get; set; }
        public DbSet<AssetType> AssetTypeSet { get; set; }
        public DbSet<Customermis> CustomermisSet { get; set; }
        public DbSet<PPR> PPRSet { get; set; }
        public DbSet<RiskAdjustedCharge> RiskAdjustedChargeSet { get; set; }
        public DbSet<ScoreCardMetrics> ScoreCardMetricsSet { get; set; }
        public DbSet<ScoreCardWeight> ScoreCardWeightSet { get; set; }
        public DbSet<ScoreCardPerspective> ScorecardPerspectiveSet { get; set; }
        public DbSet<ScoreCardMapping> ScoreCardMappingSet { get; set; }
        public DbSet<ScoreCard> ScoreCardSet { get; set; }
        public DbSet<MISTransferPrice> MISTransferPriceSet { get; set; }
        public DbSet<AcquirerMapping> AcquirerMappingSet { get; set; }
        public DbSet<AcquirerSharing> AcquirerSharingSet { get; set; }
        public DbSet<CustomerTransferPrice> CustomerTransferPriceSet { get; set; }
        public DbSet<TeamSector> TeamSectorSet { get; set; }
        public DbSet<TeamSegment> TeamSegmentSet { get; set; }
        public DbSet<ProductTransferPrice> ProductTransferPriceSet { get; set; }

        public DbSet<IncomeProductsTable> IncomeProductsTableSet { get; set; }
        public DbSet<PPRCaption> PPRCaptionSet { get; set; }
        public DbSet<PLCaption2> PLCaption2Set { get; set; }
        public DbSet<Caption> CaptionSet { get; set; }
        public DbSet<IncomeCommFeeLineCaption> IncomeCommFeeLineCaptionSet { get; set; }
        public DbSet<IncomeLineCapton> IncomeLineCaptonSet { get; set; }
        public DbSet<IncomeProductstableUnit> IncomeProductstableUnitSet { get; set; }
        public DbSet<IncomeProductsTableTreasury> IncomeProductsTableTreasurySet { get; set; }
        public DbSet<IncomeNEAMapping> IncomeNEAMappingSet { get; set; }
        public DbSet<KBL_MISProductCategoryInfo> KBL_MISProductCategoryInfoSet { get; set; }
        public DbSet<IncomeCaptionPosition> IncomeCaptionPositionSet { get; set; }
        public DbSet<TeamBank> TeamBankSet { get; set; }
        public DbSet<ScoreCardMetricsKBL> ScoreCardMetricsKBLSet { get; set; }
        public DbSet<ScoreCardKPITypesKBL> ScoreCardKPITypesKBLSet { get; set; }
        public DbSet<ScoreCardSetMetricTargetKBL> ScoreCardSetMetricTargetKBLSet { get; set; }
        public DbSet<ScoreCardMISMappingKBL> ScoreCardMISMappingKBLSet { get; set; }
        public DbSet<IncomeCashVaultSchedule> IncomeCashVaultScheduleSet { get; set; }
        public DbSet<IncomeSetup> IncomeSetupSet { get; set; }
        public DbSet<SlarySchedule> SlaryScheduleSet { get; set; }
        public DbSet<IncomeOtherBreakdown> IncomeOtherBreakdownSet { get; set; }
        public DbSet<DownloadBaseFintrakFinalManual> DownloadBaseFintrakFinalManualSet { get; set; }
        public DbSet<OpexGLBasis2> OpexGLBasis2Set { get; set; }
        public DbSet<MPRReportStatus> MPRReportStatusSet { get; set; }
        public DbSet<FinstatMapping> FinstatMappingSet { get; set; }
        public DbSet<IncomeAccountsTreeMisCodes> IncomeAccountsTreeMisCodesSet { get; set; }
        public DbSet<IncomeAccountsTreeAccount> IncomeAccountsTreeAccountSet { get; set; }
        public DbSet<IncomePoolRateSbu> IncomePoolRateSbuSet { get; set; }
        public DbSet<IncomePoolRateSbuYear> IncomePoolRateSbuYearSet { get; set; }
        public DbSet<IncomeAccountsFintrak> IncomeAccountsFintrakSet { get; set; }
        public DbSet<IncomeAccountsNpl> IncomeAccountNplSet { get; set; }
        public DbSet<IncomeCommFeeMis> incomeCommFeeMisSet { get; set; }
        public DbSet<IncomeMisCodes> IncomeMisCodesSet { get; set; }
        public DbSet<OpexStaffcostDetail> OpexStaffcostDetailSet { get; set; }
        public DbSet<OpexTimeAllocationMPR> OpexTimeAllocationMPRSet { get; set; }
        public DbSet<IncomeProductShare> IncomeProductShareSet { get; set; }
        public DbSet<OpexMaintenance> OpexMaintenanceSet { get; set; }
        public DbSet<OpexMemounits> OpexMemounitsSet { get; set; }
        public DbSet<OpexMemounitPlmap> OpexMemounitPlmapSet { get; set; }
        public DbSet<OpexSBUBaseCost> OpexSBUBaseCostSet { get; set; }
        public DbSet<IncomeAdjustmentCommFeeSearchManual> IncomeAdjustmentCommFeeSearchManualSet { get; set; }
        public DbSet<IncomeAccountsPart> IncomeAccountsPartSet { get; set; }
        public DbSet<IncomeBranches> IncomeBranchesSet { get; set; }
        public DbSet<IncomeCurrency> IncomeCurrencySet { get; set; }
        public DbSet<IncomeMemorep> IncomeMemorepSet { get; set; }
        public DbSet<OpexUpdate> OpexUpdateSet { get; set; }
        public DbSet<IncomeSplitPoolsRate> IncomeSplitPoolsRateSet { get; set; }
        public DbSet<IncomeAccountsUnit> IncomeAccountsUnitSet { get; set; }
        public DbSet<OpexGLMap> OpexGLMapSet { get; set; }
        public DbSet<GroupCaptions> GroupCaptionsSet { get; set; }
        public DbSet<IncomeProductsTableALT> IncomeProductsTableALTSet { get; set; }
        public DbSet<TeamStructureALL> TeamStructureALLSet { get; set; }
        public DbSet<IncomeFintrakAccountsSegment> IncomeFintrakAccountsSegmentSet { get; set; }
        public DbSet<IncomeSplitPoolsRatesAndBasis> IncomeSplitPoolsRatesAndBasisSet { get; set; }
        public DbSet<IncomeRetailProductOverride> IncomeRetailProductOverrideSet { get; set; }
        public DbSet<IncomeRetailProductOverrideTEMP> IncomeRetailProductOverrideTEMPSet { get; set; }
        public DbSet<IncomeAccountMISOverrideTEMP> IncomeAccountMISOverrideTEMPSet { get; set; }
        public DbSet<IncomeCommFeeBusinessRule> IncomeCommFeeBusinessRuleSet { get; set; }
        public DbSet<IncomeCustomerRatingOverride> IncomeCustomerRatingOverrideSet { get; set; }
        public DbSet<IncomeAccountsListing> IncomeAccountsListingSet { get; set; }
        public DbSet<FTPRiskRatings> FTPRiskRatingsSet { get; set; }
        public DbSet<IncomeCaptionPoolRate> IncomeCaptionPoolRateSet { get; set; }
        public DbSet<IncomeCustomerRatingOverrideTEMP> IncomeCustomerRatingOverrideTEMPSet { get; set; }
        public DbSet<IncomeAccountsTreeAccountTEMP> IncomeAccountsTreeAccountTEMPSet { get; set; }
        public DbSet<IncomeAccountsTreeMisCodesTEMP> IncomeAccountsTreeMisCodesTEMPSet { get; set; }
        public DbSet<OneBankAO> OneBankAOSet { get; set; }
        public DbSet<OneBankBranch> OneBankBranchSet { get; set; }
        public DbSet<OneBankRegionTD> OneBankRegionTDSet { get; set; }
        public DbSet<OneBankTeamTable> OneBankTeamTableSet { get; set; }
        public DbSet<VolumeAnalysisRundates> VolumeAnalysisRundatesSet { get; set; }
        public DbSet<TeamStructureALLTEMP> TeamStructureALLTEMPSet { get; set; }
        public DbSet<MprInterestMapping> MprInterestMappingSet { get; set; }
        public DbSet<MISNewOthersTEMP> MISNewOthersTEMPSet { get; set; }
        public DbSet<IncomeCustomerPoolRate> IncomeCustomerPoolRateSet { get; set; }
        public DbSet<IncomeAccountPoolRate> IncomeAccountPoolRateSet { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Ignore<PropertyChangedEventHandler>();
            modelBuilder.Ignore<ExtensionDataObject>();
            modelBuilder.Ignore<IIdentifiableEntity>();


            //MPR
            //TeamDefinition
            modelBuilder.Entity<UserMIS>().HasKey<int>(e => e.UserMISId).Ignore(e => e.EntityId);
            modelBuilder.Entity<UserMIS>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<UserMIS>().ToTable("mpr_usermis");

            modelBuilder.Entity<UserClassificationMap>().HasKey<int>(e => e.UserClassificationMapId).Ignore(e => e.EntityId);
            modelBuilder.Entity<UserClassificationMap>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<UserClassificationMap>().ToTable("mpr_user_classification_map");

            //TeamDefinition
            modelBuilder.Entity<TeamDefinition>().HasKey<int>(e => e.TeamDefinitionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<TeamDefinition>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TeamDefinition>().ToTable("mpr_team_definition");

            //TeamClassificationType
            modelBuilder.Entity<TeamClassificationType>().HasKey<int>(e => e.TeamClassificationTypeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<TeamClassificationType>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TeamClassificationType>().ToTable("mpr_team_classification_type");

            //TeamClassification
            modelBuilder.Entity<TeamClassification>().HasKey<int>(e => e.TeamClassificationId).Ignore(e => e.EntityId);
            modelBuilder.Entity<TeamClassification>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TeamClassification>().ToTable("mpr_team_classification");

            //Team
            modelBuilder.Entity<Team>().HasKey<int>(e => e.TeamId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Team>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Team>().ToTable("mpr_team");

            //TeamClassificationMap
            modelBuilder.Entity<TeamClassificationMap>().HasKey<int>(e => e.TeamClassificationMapId).Ignore(e => e.EntityId);
            modelBuilder.Entity<TeamClassificationMap>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TeamClassificationMap>().ToTable("mpr_team_classification_map");

            //AccountOfficerDetail
            modelBuilder.Entity<AccountOfficerDetail>().HasKey<int>(e => e.AccountOfficerDetailId).Ignore(e => e.EntityId);
            modelBuilder.Entity<AccountOfficerDetail>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<AccountOfficerDetail>().ToTable("mpr_accountofficer_detail");

            //BranchDefaultMIS
            modelBuilder.Entity<BranchDefaultMIS>().HasKey<int>(e => e.BranchDefaultMISId).Ignore(e => e.EntityId);
            modelBuilder.Entity<BranchDefaultMIS>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BranchDefaultMIS>().ToTable("mpr_branchdefaultmis");

            //ManagementTree
            modelBuilder.Entity<ManagementTree>().HasKey<int>(e => e.ManagementTreeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ManagementTree>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ManagementTree>().ToTable("mpr_managementtree");

            //AccountMIS
            modelBuilder.Entity<AccountMIS>().HasKey<int>(e => e.AccountMISId).Ignore(e => e.EntityId);
            modelBuilder.Entity<AccountMIS>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<AccountMIS>().ToTable("mpr_accountmis");

            //MISReplacement
            modelBuilder.Entity<MISReplacement>().HasKey<int>(e => e.MISReplacementId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MISReplacement>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MISReplacement>().ToTable("mpr_misreplacement");

            //SetUp
            modelBuilder.Entity<SetUp>().HasKey<int>(e => e.SetupId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SetUp>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SetUp>().ToTable("mpr_setup");

            //Staff
            modelBuilder.Entity<Staffs>().HasKey<int>(e => e.StaffId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Staffs>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Staffs>().ToTable("cor_staff");

            ////SetUp
            //modelBuilder.Entity<MPRSetUp>().HasKey<int>(e => e.SetupId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<MPRSetUp>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<MPRSetUp>().ToTable("mpr_setup");

            //BSCaption
            modelBuilder.Entity<BSCaption>().HasKey<int>(e => e.CaptionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<BSCaption>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BSCaption>().ToTable("mpr_bs_caption");

            //MPRProduct
            modelBuilder.Entity<MPRProduct>().HasKey<int>(e => e.ProductId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MPRProduct>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MPRProduct>().ToTable("mpr_product");

            //NonProductMap
            modelBuilder.Entity<NonProductMap>().HasKey<int>(e => e.NonProductMapId).Ignore(e => e.EntityId);
            modelBuilder.Entity<NonProductMap>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<NonProductMap>().ToTable("mpr_non_product_map");

            //NonProductRate
            modelBuilder.Entity<NonProductRate>().HasKey<int>(e => e.NonProductRateId).Ignore(e => e.EntityId);
            modelBuilder.Entity<NonProductRate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<NonProductRate>().ToTable("mpr_non_product_rate");

            //ProductMIS
            modelBuilder.Entity<ProductMIS>().HasKey<int>(e => e.ProductMISId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ProductMIS>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ProductMIS>().ToTable("mpr_productmis");

            //BalanceSheetThreshold
            modelBuilder.Entity<BalanceSheetThreshold>().HasKey<int>(e => e.BalanceSheetThresholdId).Ignore(e => e.EntityId);
            modelBuilder.Entity<BalanceSheetThreshold>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BalanceSheetThreshold>().ToTable("mpr_balancesheet_threshold");

            //AccountTransferPrice
            modelBuilder.Entity<AccountTransferPrice>().HasKey<int>(e => e.AccountTransferPriceId).Ignore(e => e.EntityId);
            modelBuilder.Entity<AccountTransferPrice>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<AccountTransferPrice>().ToTable("mpr_account_transfer_price");

            //TransferPrice
            modelBuilder.Entity<TransferPrice>().HasKey<int>(e => e.TransferPriceId).Ignore(e => e.EntityId);
            modelBuilder.Entity<TransferPrice>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TransferPrice>().ToTable("mpr_transfer_price");

            //GeneralTransferPrice
            modelBuilder.Entity<GeneralTransferPrice>().HasKey<int>(e => e.GeneralTransferPriceId).Ignore(e => e.EntityId);
            modelBuilder.Entity<GeneralTransferPrice>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<GeneralTransferPrice>().ToTable("mpr_general_transfer_price");

            //BalanceSheetBudget
            modelBuilder.Entity<BalanceSheetBudget>().HasKey<int>(e => e.BudgetId).Ignore(e => e.EntityId);
            modelBuilder.Entity<BalanceSheetBudget>().Property(c => c.BudgetId).HasColumnName("BalanceSheetBudgetId");
            modelBuilder.Entity<BalanceSheetBudget>().Property(c => c.MisCode).HasColumnName("TeamCode");
            modelBuilder.Entity<BalanceSheetBudget>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BalanceSheetBudget>().ToTable("mpr_balancesheet_budget");

            //BalanceSheetBudgetOfficer
            modelBuilder.Entity<BalanceSheetBudgetOfficer>().HasKey<int>(e => e.BudgetId).Ignore(e => e.EntityId);
            modelBuilder.Entity<BalanceSheetBudgetOfficer>().Property(c => c.BudgetId).HasColumnName("BalanceSheetBudgetOffId");
            modelBuilder.Entity<BalanceSheetBudgetOfficer>().Property(c => c.MisCode).HasColumnName("AccountOfficerCode");
            modelBuilder.Entity<BalanceSheetBudgetOfficer>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BalanceSheetBudgetOfficer>().ToTable("mpr_balancesheet_budget_officer");

            //BSGLMapping
            modelBuilder.Entity<BSGLMapping>().HasKey<int>(e => e.BSGLMappingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<BSGLMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BSGLMapping>().ToTable("mpr_bs_gl_mapping");

            //MPRBalanceSheetAdjustment
            modelBuilder.Entity<MPRBalanceSheetAdjustment>().HasKey<int>(e => e.BalancesheetAdjustmentId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MPRBalanceSheetAdjustment>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MPRBalanceSheetAdjustment>().ToTable("mpr_balancesheet_adjustment");

            //MPRBalanceSheet
            modelBuilder.Entity<MPRBalanceSheet>().HasKey<int>(e => e.BalanceSheetId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MPRBalanceSheet>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MPRBalanceSheet>().ToTable("mpr_balancesheet");

            //CustAccount
            modelBuilder.Entity<CustAccount>().HasKey<int>(e => e.CustAccountId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CustAccount>().Property(c => c.RowVersion).IsRowVersion();
            // modelBuilder.Entity<CustAccount>().ToTable("cor_cust_account");
            modelBuilder.Entity<CustAccount>().ToTable("mpr_account_stg");

            //MemoAccountMap
            modelBuilder.Entity<MemoAccountMap>().HasKey<int>(e => e.MemoAccountMapId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MemoAccountMap>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MemoAccountMap>().ToTable("mpr_memo_account_map");

            //MemoGLMap
            modelBuilder.Entity<MemoGLMap>().HasKey<int>(e => e.MemoGLMapId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MemoGLMap>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MemoGLMap>().ToTable("mpr_memo_gl_map");

            //MemoProductMap
            modelBuilder.Entity<MemoProductMap>().HasKey<int>(e => e.MemoProductMapId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MemoProductMap>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MemoProductMap>().ToTable("mpr_memo_product_map");

            //MemoUnits
            modelBuilder.Entity<MemoUnits>().HasKey<int>(e => e.MemoUnitsId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MemoUnits>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MemoUnits>().ToTable("mpr_memo_units");

            //BSExemption
            modelBuilder.Entity<BSExemption>().HasKey<int>(e => e.BSExemptionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<BSExemption>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BSExemption>().ToTable("mpr_bs_exemption");

            //CaptionMapping
            modelBuilder.Entity<CaptionMapping>().HasKey<int>(e => e.CaptionMappingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CaptionMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CaptionMapping>().ToTable("mpr_caption_mapping");

            //RatioCaptionMapping
            modelBuilder.Entity<RatioCaptionMapping>().HasKey<int>(e => e.RatioCaptionMappingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<RatioCaptionMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<RatioCaptionMapping>().ToTable("mpr_ratio_caption_mapping");

            //Ratios
            modelBuilder.Entity<Ratios>().HasKey<int>(e => e.RatiosId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Ratios>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Ratios>().ToTable("mpr_ratios");

            //BSINOtherInformation
            modelBuilder.Entity<BSINOtherInformation>().HasKey<int>(e => e.BSINOtherInformationId).Ignore(e => e.EntityId);
            modelBuilder.Entity<BSINOtherInformation>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BSINOtherInformation>().ToTable("mpr_bs_in_other_information");

            //BSINOtherInformationTotalLine
            modelBuilder.Entity<BSINOtherInformationTotalLine>().HasKey<int>(e => e.BSINOtherInformationTotalLineId).Ignore(e => e.EntityId);
            modelBuilder.Entity<BSINOtherInformationTotalLine>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<BSINOtherInformationTotalLine>().ToTable("mpr_bs_in_other_information_total_line");

            //AbcRatio
            modelBuilder.Entity<AbcRatio>().HasKey<int>(e => e.AbcRatioId).Ignore(e => e.EntityId);
            modelBuilder.Entity<AbcRatio>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<AbcRatio>().ToTable("mpr_abcratio");

            //Sbu
            modelBuilder.Entity<Sbu>().HasKey<int>(e => e.SbuId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Sbu>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Sbu>().ToTable("mpr_sbu");

            //SbuType
            modelBuilder.Entity<SbuType>().HasKey<int>(e => e.SbuTypeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<SbuType>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SbuType>().ToTable("mpr_sbutype");

            //Services
            modelBuilder.Entity<Servicese>().HasKey<int>(e => e.ServicesId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Servicese>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Servicese>().ToTable("mpr_services");

            //NRFFCaption
            modelBuilder.Entity<NRFFCaption>().HasKey<int>(e => e.NRFFCaption_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<NRFFCaption>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<NRFFCaption>().ToTable("mpr_nrff_caption");


            //MPR_PL
            //PLCaption
            modelBuilder.Entity<PLCaption>().HasKey<int>(e => e.PLCaptionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<PLCaption>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PLCaption>().ToTable("mpr_pl_caption");

            //MPRGLMapping
            modelBuilder.Entity<MPRGLMapping>().HasKey<int>(e => e.MPRGLMappingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MPRGLMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MPRGLMapping>().ToTable("mpr_gl_mapping");

            //GLReclassification
            modelBuilder.Entity<GLReclassification>().HasKey<int>(e => e.GLReclassificationId).Ignore(e => e.EntityId);
            modelBuilder.Entity<GLReclassification>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<GLReclassification>().ToTable("mpr_gl_reclassification");

            //GLException
            modelBuilder.Entity<GLException>().HasKey<int>(e => e.GLExceptionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<GLException>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<GLException>().ToTable("mpr_gl_exception");

            //MPRTotalLine
            modelBuilder.Entity<MPRTotalLine>().HasKey<int>(e => e.TotallineId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MPRTotalLine>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MPRTotalLine>().ToTable("mpr_totalline");

            //MPRTotalLineMakeUp
            modelBuilder.Entity<MPRTotalLineMakeUp>().HasKey<int>(e => e.TotalLineMakeUpId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MPRTotalLineMakeUp>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MPRTotalLineMakeUp>().ToTable("mpr_totalline_makeup");

            //GLMIS
            modelBuilder.Entity<GLMIS>().HasKey<int>(e => e.GlMisId).Ignore(e => e.EntityId);
            modelBuilder.Entity<GLMIS>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<GLMIS>().ToTable("mpr_glmis");

            //PLIncomeReport
            modelBuilder.Entity<PLIncomeReport>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<PLIncomeReport>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PLIncomeReport>().ToTable("mpr_pl_income_report");

            //PLIncomeReportAdjustment
            modelBuilder.Entity<PLIncomeReportAdjustment>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<PLIncomeReportAdjustment>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PLIncomeReportAdjustment>().ToTable("mpr_pl_income_report_adjustment");

            //RevenueBudget
            modelBuilder.Entity<RevenueBudget>().HasKey<int>(e => e.BudgetId).Ignore(e => e.EntityId);
            modelBuilder.Entity<RevenueBudget>().Property(c => c.BudgetId).HasColumnName("RevenueBudgetId");
            modelBuilder.Entity<RevenueBudget>().Property(c => c.MisCode).HasColumnName("TeamCode");
            modelBuilder.Entity<RevenueBudget>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<RevenueBudget>().ToTable("mpr_revenue_budget");

            //RevenueBudgetOfficer
            modelBuilder.Entity<RevenueBudgetOfficer>().HasKey<int>(e => e.BudgetId).Ignore(e => e.EntityId);
            modelBuilder.Entity<RevenueBudgetOfficer>().Property(c => c.BudgetId).HasColumnName("RevenueBudgetOffId");
            modelBuilder.Entity<RevenueBudgetOfficer>().Property(c => c.MisCode).HasColumnName("AccountOfficerCode");
            modelBuilder.Entity<RevenueBudgetOfficer>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<RevenueBudgetOfficer>().ToTable("mpr_revenue_budget_officer");

            //Revenue
            modelBuilder.Entity<Revenue>().HasKey<int>(e => e.RevenueId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Revenue>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Revenue>().ToTable("mpr_revenue");

            //IncomeCentralVaultSchedule
            modelBuilder.Entity<IncomeCentralVaultSchedule>().HasKey<int>(e => e.IncomeCentralVaultScheduleId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeCentralVaultSchedule>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeCentralVaultSchedule>().ToTable("mpr_income_central_vault_schedule");


            //MPRPLDerivedCaption
            modelBuilder.Entity<MPRPLDerivedCaption>().HasKey<int>(e => e.PLDerivedCaptionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MPRPLDerivedCaption>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MPRPLDerivedCaption>().ToTable("mpr_pl_derived_caption");

            //ProcessData
            modelBuilder.Entity<ProcessData>().HasKey<int>(e => e.ProcessDataId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ProcessData>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ProcessData>().ToTable("mpr_process_data");

            //MPRCommFee
            modelBuilder.Entity<MPRCommFee>().HasKey<int>(e => e.CommFee_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<MPRCommFee>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MPRCommFee>().ToTable("mpr_commfee_stg");

            //MPR_OPEX
            //CostCentre
            modelBuilder.Entity<CostCentre>().HasKey<int>(e => e.CostCentreId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CostCentre>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CostCentre>().ToTable("mpr_costcentre");

            //CostCentreDefinition
            modelBuilder.Entity<CostCentreDefinition>().HasKey<int>(e => e.CCDefinitionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CostCentreDefinition>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CostCentreDefinition>().ToTable("mpr_costcentre_definition");

            //ExpenseBasis
            modelBuilder.Entity<ExpenseBasis>().HasKey<int>(e => e.ExpenseBasisId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ExpenseBasis>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ExpenseBasis>().ToTable("mpr_expense_basis");

            //ExpenseMapping
            modelBuilder.Entity<ExpenseMapping>().HasKey<int>(e => e.ExpenseMappingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ExpenseMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ExpenseMapping>().ToTable("mpr_expense_map");

            //ExpenseProductMapping
            modelBuilder.Entity<ExpenseProductMapping>().HasKey<int>(e => e.ExpenseProductId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ExpenseProductMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ExpenseProductMapping>().ToTable("mpr_expense_product_mapping");

            //ExpenseGLMapping
            modelBuilder.Entity<ExpenseGLMapping>().HasKey<int>(e => e.ExpenseGLId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ExpenseGLMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ExpenseGLMapping>().ToTable("mpr_expense_gl_mapping");

            //ExpenseRawBasis
            modelBuilder.Entity<ExpenseRawBasis>().HasKey<int>(e => e.ExpenseRawBasisId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ExpenseRawBasis>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ExpenseRawBasis>().ToTable("mpr_expense_raw_basis");

            //StaffCost
            modelBuilder.Entity<StaffCost>().HasKey<int>(e => e.StaffCostId).Ignore(e => e.EntityId);
            modelBuilder.Entity<StaffCost>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<StaffCost>().ToTable("mpr_staffcost");

            //OpexManagementTree
            modelBuilder.Entity<OpexManagementTree>().HasKey<int>(e => e.OpexMgtTreeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<OpexManagementTree>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OpexManagementTree>().ToTable("mpr_opex_management_tree");

            //ActivityBase
            modelBuilder.Entity<ActivityBase>().HasKey<int>(e => e.ActivityBaseId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ActivityBase>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ActivityBase>().ToTable("mpr_activity_base");

            //ActivityBaseRatio
            modelBuilder.Entity<ActivityBaseRatio>().HasKey<int>(e => e.ActivityBaseRatioId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ActivityBaseRatio>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ActivityBaseRatio>().ToTable("mpr_activity_base_ratio");

            //OpexMISReplacement
            modelBuilder.Entity<OpexMISReplacement>().HasKey<int>(e => e.OpexMISReplacementId).Ignore(e => e.EntityId);
            modelBuilder.Entity<OpexMISReplacement>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OpexMISReplacement>().ToTable("mpr_opex_mis_replacement");

            //OpexBusinessRule
            modelBuilder.Entity<OpexBusinessRule>().HasKey<int>(e => e.OpexBusinessRuleId).Ignore(e => e.EntityId);
            modelBuilder.Entity<OpexBusinessRule>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OpexBusinessRule>().ToTable("mpr_opex_business_rule");

            //OpexAbcExemption
            modelBuilder.Entity<OpexAbcExemption>().HasKey<int>(e => e.OpexAbcExemptionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<OpexAbcExemption>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OpexAbcExemption>().ToTable("mpr_opex_abc_exemption");

            //OpexRawExpense
            modelBuilder.Entity<OpexRawExpense>().HasKey<int>(e => e.OpexRawExpenseId).Ignore(e => e.EntityId);
            modelBuilder.Entity<OpexRawExpense>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OpexRawExpense>().ToTable("mpr_opex_raw_expense");

            //OpexGLMapping
            modelBuilder.Entity<OpexGLMapping>().HasKey<int>(e => e.GLMappingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<OpexGLMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OpexGLMapping>().ToTable("mpr_opex_gl_mapping");

            //OpexRreport
            modelBuilder.Entity<OpexReport>().HasKey<int>(e => e.ReportId).Ignore(e => e.EntityId);
            modelBuilder.Entity<OpexReport>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OpexReport>().ToTable("mpr_opex_report");

            //OpexGLBasis
            modelBuilder.Entity<OpexGLBasis>().HasKey<int>(e => e.OpexGLBasisId).Ignore(e => e.EntityId);
            modelBuilder.Entity<OpexGLBasis>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OpexGLBasis>().ToTable("mpr_opex_gl_basis");

            //OpexBasisMapping
            modelBuilder.Entity<OpexBasisMapping>().HasKey<int>(e => e.OpexBasisMappingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<OpexBasisMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OpexBasisMapping>().ToTable("mpr_opex_basis_mapping");


            //CheckList
            modelBuilder.Entity<CheckList>().HasKey<int>(e => e.CheckListId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CheckList>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CheckList>().ToTable("mpr_opex_checklist");

            //HoExemptionMISCode
            modelBuilder.Entity<HoExemptionMISCode>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<HoExemptionMISCode>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<HoExemptionMISCode>().ToTable("mpr_opex_ho_exemption_miscodes");




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

            //FixedAssetSharingRatio
            modelBuilder.Entity<FixedAssetSharingRatio>().HasKey<int>(e => e.FixedAssetSharingRatioId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FixedAssetSharingRatio>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FixedAssetSharingRatio>().ToTable("mpr_fixedasset_sharing_ratio");

            //IncomeCashCentreCode
            modelBuilder.Entity<IncomeCentralVaultAccounts>().HasKey<int>(e => e.IncomeCentralVaultAccountsId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeCentralVaultAccounts>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeCentralVaultAccounts>().ToTable("mpr_income_central_vault_accounts");

            //IncomeCashCentreCode
            modelBuilder.Entity<IncomeCashCentreCode>().HasKey<int>(e => e.IncomeCashCentreCodeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeCashCentreCode>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeCashCentreCode>().ToTable("mpr_income_cash_centrecode");

            //IncomeNEAGLSBU
            modelBuilder.Entity<IncomeNEAGLSBU>().HasKey<int>(e => e.IncomeNEAGLSBUId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeNEAGLSBU>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeNEAGLSBU>().ToTable("mpr_income_nea_gl_sbu");

            //CategoryTransferPrice
            modelBuilder.Entity<CategoryTransferPrice>().HasKey<int>(e => e.CategoryTransferPriceId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CategoryTransferPrice>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CategoryTransferPrice>().ToTable("mpr_category_transfer_price");

            //LowCostRemap
            modelBuilder.Entity<LowCostRemap>().HasKey<int>(e => e.LowCostRemapId).Ignore(e => e.EntityId);
            modelBuilder.Entity<LowCostRemap>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<LowCostRemap>().ToTable("mpr_low_cost_remap");

            //NEABranchSBUShares
            modelBuilder.Entity<NEABranchSBUShares>().HasKey<int>(e => e.NEABranchSBUSharesId).Ignore(e => e.EntityId);
            modelBuilder.Entity<NEABranchSBUShares>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<NEABranchSBUShares>().ToTable("mpr_nea_branch_sbu_shares");

            //NEABranchSharingRatio
            modelBuilder.Entity<NEABranchSharingRatio>().HasKey<int>(e => e.NEABranchSharingRatioId).Ignore(e => e.EntityId);
            modelBuilder.Entity<NEABranchSharingRatio>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<NEABranchSharingRatio>().ToTable("mpr_nea_branch_sharing_ratio");

            //NEASharingRatio
            modelBuilder.Entity<NEASharingRatio>().HasKey<int>(e => e.NEASharingRatioId).Ignore(e => e.EntityId);
            modelBuilder.Entity<NEASharingRatio>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<NEASharingRatio>().ToTable("mpr_nea_sharing_ratio");

            //NEASharingRatioFcy
            modelBuilder.Entity<NEASharingRatioFcy>().HasKey<int>(e => e.NEASharingRatioFcyId).Ignore(e => e.EntityId);
            modelBuilder.Entity<NEASharingRatioFcy>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<NEASharingRatioFcy>().ToTable("mpr_nea_sharing_ratio_fcy");

            //OpexBranchMapping
            modelBuilder.Entity<OpexBranchMapping>().HasKey<int>(e => e.OpexBranchMappingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<OpexBranchMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OpexBranchMapping>().ToTable("mpr_opexbranchmapping");

            //MessagingSubscription
            modelBuilder.Entity<MessagingSubscription>().HasKey<int>(e => e.MessagingSubscriptionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MessagingSubscription>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MessagingSubscription>().ToTable("mpr_messaging_subscription");

            //crb_Data
            modelBuilder.Entity<crb_Data>().HasKey<int>(e => e.crb_Data_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<crb_Data>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<crb_Data>().ToTable("mpr_crb_Data");

            //AccountInterest
            modelBuilder.Entity<account_interest>().HasKey<int>(e => e.account_interest_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<account_interest>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<account_interest>().ToTable("mpr_account_interest");

            //ProductInterest
            modelBuilder.Entity<product_interest>().HasKey<int>(e => e.product_interestId).Ignore(e => e.EntityId);
            modelBuilder.Entity<product_interest>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<product_interest>().ToTable("mpr_product_interest");

            //Team Structure
            modelBuilder.Entity<TeamStructure>().HasKey<int>(e => e.Team_StructureId).Ignore(e => e.EntityId);
            modelBuilder.Entity<TeamStructure>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TeamStructure>().ToTable("Mpr_Team_Structure");

            //Corporate Adjustment
            modelBuilder.Entity<CorporateAdjustment>().HasKey<int>(e => e.CorporateAdjustmentId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CorporateAdjustment>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CorporateAdjustment>().ToTable("mpr_Corporate_adjustment");

            //Caption Transfer Price
            modelBuilder.Entity<caption_transfer_price>().HasKey<int>(e => e.caption_transfer_price_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<caption_transfer_price>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<caption_transfer_price>().ToTable("mpr_caption_transfer_price");

            //Asset Type
            modelBuilder.Entity<AssetType>().HasKey<int>(e => e.AssetType_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<AssetType>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<AssetType>().ToTable("Mpr_AssetType");

            //Customer MIS
            modelBuilder.Entity<Customermis>().HasKey<int>(e => e.CustomermisId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Customermis>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Customermis>().ToTable("mpr_Customermis");

            //PPR
            modelBuilder.Entity<PPR>().HasKey<int>(e => e.PPRId).Ignore(e => e.EntityId);
            modelBuilder.Entity<PPR>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PPR>().ToTable("mpr_PPR");

            //RiskAdjustedCharge
            modelBuilder.Entity<RiskAdjustedCharge>().HasKey<int>(e => e.RiskAdjustedChargeId).Ignore(e => e.EntityId);
            modelBuilder.Entity<RiskAdjustedCharge>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<RiskAdjustedCharge>().ToTable("mpr_Risk_Adjusted_Charge");

            //ScoreCardMetrics
            modelBuilder.Entity<ScoreCardMetrics>().HasKey<int>(e => e.MetricId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ScoreCardMetrics>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ScoreCardMetrics>().ToTable("mpr_Scorecard_metrics");

            //ScoreCardWeight
            modelBuilder.Entity<ScoreCardWeight>().HasKey<int>(e => e.WeightId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ScoreCardWeight>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ScoreCardWeight>().ToTable("mpr_Scorecard_weight");

            //ScoreCardPerspective
            modelBuilder.Entity<ScoreCardPerspective>().HasKey<int>(e => e.PerspectiveId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ScoreCardPerspective>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ScoreCardPerspective>().ToTable("mpr_Scorecard_Perspective");

            //ScoreCardMapping
            modelBuilder.Entity<ScoreCardMapping>().HasKey<int>(e => e.MappingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ScoreCardMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ScoreCardMapping>().ToTable("mpr_scorecard_mapping");

            //ScoreCard
            modelBuilder.Entity<ScoreCard>().HasKey<int>(e => e.mpr_scorecard_stgId).Ignore(e => e.EntityId);
            modelBuilder.Entity<ScoreCard>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ScoreCard>().ToTable("mpr_scorecard");

            //MISTransferPricing
            modelBuilder.Entity<MISTransferPrice>().HasKey<int>(e => e.mistransferpriceId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MISTransferPrice>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MISTransferPrice>().ToTable("mpr_mis_transfer_price");

            //AcquirerMapping
            modelBuilder.Entity<AcquirerMapping>().HasKey<int>(e => e.mpr_Acquirer_Mapping_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<AcquirerMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<AcquirerMapping>().ToTable("mpr_Acquirer_Mapping");

            //AcquirerSharing
            modelBuilder.Entity<AcquirerSharing>().HasKey<int>(e => e.mpr_Acquirer_Sharing_Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<AcquirerSharing>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<AcquirerSharing>().ToTable("mpr_Acquirer_Sharing");

            //CustomerTransferPrice
            modelBuilder.Entity<CustomerTransferPrice>().HasKey<int>(e => e.customertransferpriceId).Ignore(e => e.EntityId);
            modelBuilder.Entity<CustomerTransferPrice>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<CustomerTransferPrice>().ToTable("mpr_customer_transfer_price");

            //TeamSector
            modelBuilder.Entity<TeamSector>().HasKey<int>(e => e.Mpr_Team_Sector_ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<TeamSector>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TeamSector>().ToTable("Mpr_Team_Sector");

            //TeamSegment
            modelBuilder.Entity<TeamSegment>().HasKey<int>(e => e.Mpr_Team_Segment_ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<TeamSegment>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TeamSegment>().ToTable("Mpr_Team_Segment");

            //ProductTransferPrice
            modelBuilder.Entity<ProductTransferPrice>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<ProductTransferPrice>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ProductTransferPrice>().ToTable("mpr_product_transfer_price");

            //Income
            modelBuilder.Entity<IncomeProductsTable>().HasKey<int>(e => e.ProductID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeProductsTable>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeProductsTable>().ToTable("Income_ProductsTable");

            //PPR Caption
            modelBuilder.Entity<PPRCaption>().HasKey<int>(e => e.PPR_CaptionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<PPRCaption>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PPRCaption>().ToTable("PPR_Caption");

            ////PL Caption
            //modelBuilder.Entity<PLCaption2>().HasKey<int>(e => e.PL_CaptionId).Ignore(e => e.EntityId);
            //modelBuilder.Entity<PLCaption2>().Property(c => c.RowVersion).IsRowVersion();
            //modelBuilder.Entity<PLCaption2>().ToTable("PL_Caption2");

            //Income Caption Position
            modelBuilder.Entity<PLCaption2>().HasKey<int>(e => e.PL_CaptionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<PLCaption2>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<PLCaption2>().ToTable("PL_Caption2");

            //Caption
            modelBuilder.Entity<Caption>().HasKey<int>(e => e.CaptionId).Ignore(e => e.EntityId);
            modelBuilder.Entity<Caption>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<Caption>().ToTable("Caption");

            //Income CommFee Line Caption
            modelBuilder.Entity<IncomeCommFeeLineCaption>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeCommFeeLineCaption>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeCommFeeLineCaption>().ToTable("Income_CommFeeLineCaption");

            //IncomeLine Caption
            modelBuilder.Entity<IncomeLineCapton>().HasKey<int>(e => e.IncomeLineCaptonId).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeLineCapton>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeLineCapton>().ToTable("IncomeLineCapton");

            //Income Products table Unit
            modelBuilder.Entity<IncomeProductstableUnit>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeProductstableUnit>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeProductstableUnit>().ToTable("Income_Productstable_Unit");

            //Income Products Table Treasury
            modelBuilder.Entity<IncomeProductsTableTreasury>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeProductsTableTreasury>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeProductsTableTreasury>().ToTable("Income_ProductsTable_Treasury");

            //Income NEA Mapping
            modelBuilder.Entity<IncomeNEAMapping>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeNEAMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeNEAMapping>().ToTable("Income_NEA_Mapping");

            //KBL MISProduct Category Info
            modelBuilder.Entity<KBL_MISProductCategoryInfo>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<KBL_MISProductCategoryInfo>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<KBL_MISProductCategoryInfo>().ToTable("KBL_MIS_PRODUCT_CATEGORY_INFO");


            //Income Caption Position
            modelBuilder.Entity<IncomeCaptionPosition>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeCaptionPosition>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeCaptionPosition>().ToTable("Income_IncomeCaption_Position");

            //Team Bank
            modelBuilder.Entity<TeamBank>().HasKey<int>(e => e.TeamBankId).Ignore(e => e.EntityId);
            modelBuilder.Entity<TeamBank>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TeamBank>().ToTable("Team_Bank");

            //ScoreCard Metrics KBL
            modelBuilder.Entity<ScoreCardMetricsKBL>().HasKey<int>(e => e.MetricID).Ignore(e => e.EntityId);
            modelBuilder.Entity<ScoreCardMetricsKBL>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ScoreCardMetricsKBL>().ToTable("ScoreCard_Metrics");

            //ScoreCard KPI Types KBL
            modelBuilder.Entity<ScoreCardKPITypesKBL>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<ScoreCardKPITypesKBL>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ScoreCardKPITypesKBL>().ToTable("ScoreCard_KPI_Types");

            //ScoreCard Set Metric Target KBL
            modelBuilder.Entity<ScoreCardSetMetricTargetKBL>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<ScoreCardSetMetricTargetKBL>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ScoreCardSetMetricTargetKBL>().ToTable("ScoreCard_SetMetricTarget");

            //ScoreCard MIS Mapping KBL
            modelBuilder.Entity<ScoreCardMISMappingKBL>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<ScoreCardMISMappingKBL>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<ScoreCardMISMappingKBL>().ToTable("SCORECARD_MIS_MAPPING");

            //Income Cash Vault Schedule
            modelBuilder.Entity<IncomeCashVaultSchedule>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeCashVaultSchedule>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeCashVaultSchedule>().ToTable("Income_Cash_Vault_Schedule");

            //Income Setup
            modelBuilder.Entity<IncomeSetup>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeSetup>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeSetup>().ToTable("Income_Setup");

            //Slary Schedule
            modelBuilder.Entity<SlarySchedule>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<SlarySchedule>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<SlarySchedule>().ToTable("Slary_Schedule");

            //Income Other Breakdown
            modelBuilder.Entity<IncomeOtherBreakdown>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeOtherBreakdown>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeOtherBreakdown>().ToTable("Income_Income_OtherBreakdown");

            //Download Base Fintrak Final Manual
            modelBuilder.Entity<DownloadBaseFintrakFinalManual>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<DownloadBaseFintrakFinalManual>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<DownloadBaseFintrakFinalManual>().ToTable("mpr_downloadBase_FintrakFinalManual");

            //Download Base Fintrak Final Manual
            modelBuilder.Entity<OpexGLBasis2>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<OpexGLBasis2>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OpexGLBasis2>().ToTable("OPEX_GL_BASIS");

            //MPRReportStatus
            modelBuilder.Entity<MPRReportStatus>().HasKey<int>(e => e.MPRReportStatusId).Ignore(e => e.EntityId);
            modelBuilder.Entity<MPRReportStatus>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MPRReportStatus>().ToTable("MPRReportStatus");

            //FinstatMapping
            modelBuilder.Entity<FinstatMapping>().HasKey<int>(e => e.FinstatMappingId).Ignore(e => e.EntityId);
            modelBuilder.Entity<FinstatMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FinstatMapping>().ToTable("finstat_Mapping");

            //IncomeAccountsTreeMisCodes
            modelBuilder.Entity<IncomeAccountsTreeMisCodes>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeAccountsTreeMisCodes>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeAccountsTreeMisCodes>().ToTable("Income_AccountsTree_MISCodes");

            //IncomeAccountsTreeAccount
            modelBuilder.Entity<IncomeAccountsTreeAccount>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeAccountsTreeAccount>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeAccountsTreeAccount>().ToTable("Income_AccountsTree_Accounts");

            //IncomePoolRateSbu
            modelBuilder.Entity<IncomePoolRateSbu>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomePoolRateSbu>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomePoolRateSbu>().ToTable("Income_PoolRate_SBU");

            //IncomePoolRateSbuYear
            modelBuilder.Entity<IncomePoolRateSbuYear>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomePoolRateSbuYear>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomePoolRateSbuYear>().ToTable("Income_PoolRate_Sbu_Year");

            //IncomeAccountsFintrak
            modelBuilder.Entity<IncomeAccountsFintrak>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeAccountsFintrak>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeAccountsFintrak>().ToTable("Income_AccountsFintrak");

            //IncomeAccountsNpl
            modelBuilder.Entity<IncomeAccountsNpl>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeAccountsNpl>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeAccountsNpl>().ToTable("Income_Accounts_NPL");
                
            //Income commfee MIS
            modelBuilder.Entity<IncomeCommFeeMis>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeCommFeeMis>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeCommFeeMis>().ToTable("Income_CommFee_MIS");

            //Income MIS codes
            modelBuilder.Entity<IncomeMisCodes>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeMisCodes>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeMisCodes>().ToTable("Income_MisCodes");
          
            //Opex Staffs Cost Detail
            modelBuilder.Entity<OpexStaffcostDetail>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<OpexStaffcostDetail>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OpexStaffcostDetail>().ToTable("OPEX_STAFFCOST_DETAIL");

            //Opex Time Allocation
            modelBuilder.Entity<OpexTimeAllocationMPR>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<OpexTimeAllocationMPR>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OpexTimeAllocationMPR>().ToTable("OPEX_TIME_ALLOCATION_MPR");

            //IncomeProductShare
            modelBuilder.Entity<IncomeProductShare>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeProductShare>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeProductShare>().ToTable("Income_Product_Share");

            //OpexMaintenance
            modelBuilder.Entity<OpexMaintenance>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<OpexMaintenance>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OpexMaintenance>().ToTable("OPEX_VOLUME_CAPTIONS");

            //OpexMemounits
            modelBuilder.Entity<OpexMemounits>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<OpexMemounits>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OpexMemounits>().ToTable("OPEX_MEMOUNITS");

            //OpexMemounitPlmap
            modelBuilder.Entity<OpexMemounitPlmap>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<OpexMemounitPlmap>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OpexMemounitPlmap>().ToTable("OPEX_MEMOUNIT_PLMAP");

            //OpexSBUBaseCost
            modelBuilder.Entity<OpexSBUBaseCost>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<OpexSBUBaseCost>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OpexSBUBaseCost>().ToTable("OPEX_TEMPLATE");

            //commfeemanual
            modelBuilder.Entity<IncomeAdjustmentCommFeeSearchManual>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeAdjustmentCommFeeSearchManual>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeAdjustmentCommFeeSearchManual>().ToTable("Income_CommFeesManual");

            //IncomeAccountsPart
            modelBuilder.Entity<IncomeAccountsPart>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeAccountsPart>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeAccountsPart>().ToTable("Income_Accounts");

            //IncomeBranches
            modelBuilder.Entity<IncomeBranches>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeBranches>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeBranches>().ToTable("Income_Branches");

            //IncomeCurrency
            modelBuilder.Entity<IncomeCurrency>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeCurrency>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeCurrency>().ToTable("Income_Currency");

            //IncomeMemorep
            modelBuilder.Entity<IncomeMemorep>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeMemorep>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeMemorep>().ToTable("Income_Memo_rep");

            //OpexUpdate
            modelBuilder.Entity<OpexUpdate>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<OpexUpdate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OpexUpdate>().ToTable("OPEX_UPDATE");

            //IncomeSplitPoolsRate
            modelBuilder.Entity<IncomeSplitPoolsRate>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeSplitPoolsRate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeSplitPoolsRate>().ToTable("Income_SplitPoolsRates");

            //IncomeAccountsUnit
            modelBuilder.Entity<IncomeAccountsUnit>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeAccountsUnit>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeAccountsUnit>().ToTable("Income_Accounts_Units");

            //OpexGlMap
            modelBuilder.Entity<OpexGLMap>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<OpexGLMap>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OpexGLMap>().ToTable("Opex_GL_Map");

            //GroupCaptions
            modelBuilder.Entity<GroupCaptions>().HasKey<int>(e => e.GroupCaptionID).Ignore(e => e.EntityId);
            modelBuilder.Entity<GroupCaptions>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<GroupCaptions>().ToTable("GroupCaption");

            //Income
            modelBuilder.Entity<IncomeProductsTableALT>().HasKey<int>(e => e.ProductID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeProductsTableALT>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeProductsTableALT>().ToTable("Income_ProductsTablealt");

            //Team structure ALL
            modelBuilder.Entity<TeamStructureALL>().HasKey<int>(e => e.Team_StructureId).Ignore(e => e.EntityId);
            modelBuilder.Entity<TeamStructureALL>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TeamStructureALL>().ToTable("Mpr_Team_Structure_ALL");

            //Income Fintrak Account Segement
            modelBuilder.Entity<IncomeFintrakAccountsSegment>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeFintrakAccountsSegment>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeFintrakAccountsSegment>().ToTable("Income_FintrakAccounts_Segment");

            //Income SplitPools Rates And Basis
            modelBuilder.Entity<IncomeSplitPoolsRatesAndBasis>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeSplitPoolsRatesAndBasis>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeSplitPoolsRatesAndBasis>().ToTable("Income_SplitPoolsRatesAndBasis");

            //Income Retail Product Override
            modelBuilder.Entity<IncomeRetailProductOverride>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeRetailProductOverride>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeRetailProductOverride>().ToTable("Income_RetailProduct_Override");

            //Income Retail Product Override Temp
            modelBuilder.Entity<IncomeRetailProductOverrideTEMP>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeRetailProductOverrideTEMP>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeRetailProductOverrideTEMP>().ToTable("Income_RetailProduct_Override_TEMP");

            //Income Account MIS Override Temp
            modelBuilder.Entity<IncomeAccountMISOverrideTEMP>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeAccountMISOverrideTEMP>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeAccountMISOverrideTEMP>().ToTable("Income_accountMIS_Override_TEMP");

            //IncomeCommFeeBusinessRule
            modelBuilder.Entity<IncomeCommFeeBusinessRule>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeCommFeeBusinessRule>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeCommFeeBusinessRule>().ToTable("Income_COMMFEES_BusinessRule");

            //IncomeCustomer override
            modelBuilder.Entity<IncomeCustomerRatingOverride>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeCustomerRatingOverride>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeCustomerRatingOverride>().ToTable("Income_CustomerRating_Override");

            //IncomeAccountsListing
            modelBuilder.Entity<IncomeAccountsListing>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeAccountsListing>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeAccountsListing>().ToTable("Income_AccountsListing");

            //FTPRiskRatings
            modelBuilder.Entity<FTPRiskRatings>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<FTPRiskRatings>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<FTPRiskRatings>().ToTable("FTPRiskRatings");

            //IncomeCaptionPoolRate
            modelBuilder.Entity<IncomeCaptionPoolRate>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeCaptionPoolRate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeCaptionPoolRate>().ToTable("Income_CaptionPoolRate");

            //IncomeCustomer override Temp
            modelBuilder.Entity<IncomeCustomerRatingOverrideTEMP>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeCustomerRatingOverrideTEMP>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeCustomerRatingOverrideTEMP>().ToTable("Income_CustomerRating_Override_TEMP");

            //Income Accounts Tree Account Temp
            modelBuilder.Entity<IncomeAccountsTreeAccountTEMP>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeAccountsTreeAccountTEMP>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeAccountsTreeAccountTEMP>().ToTable("Income_AccountsTree_Accounts_TEMP");

            //IncomeAccountsTreeMisCodes Temp
            modelBuilder.Entity<IncomeAccountsTreeMisCodesTEMP>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeAccountsTreeMisCodesTEMP>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeAccountsTreeMisCodesTEMP>().ToTable("Income_AccountsTree_MISCodes_TEMP");

            //OneBankAO
            modelBuilder.Entity<OneBankAO>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<OneBankAO>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OneBankAO>().ToTable("oneBankAOTable");

            //OneBankBranch
            modelBuilder.Entity<OneBankBranch>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<OneBankBranch>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OneBankBranch>().ToTable("oneBankBranchTable");

            //OneBankRegionTD
            modelBuilder.Entity<OneBankRegionTD>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<OneBankRegionTD>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OneBankRegionTD>().ToTable("oneBankRegionTD");

            //OneBankTeamTable
            modelBuilder.Entity<OneBankTeamTable>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<OneBankTeamTable>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<OneBankTeamTable>().ToTable("oneBankTeamTable");

            //VolumeAnalysisRundates
            modelBuilder.Entity<VolumeAnalysisRundates>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<VolumeAnalysisRundates>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<VolumeAnalysisRundates>().ToTable("volume_analysis_rundates");

            //TeamStructureALLTEMP
            modelBuilder.Entity<TeamStructureALLTEMP>().HasKey<int>(e => e.Team_StructureId).Ignore(e => e.EntityId);
            modelBuilder.Entity<TeamStructureALLTEMP>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<TeamStructureALLTEMP>().ToTable("Team_TEMP");

            //MprInterestMapping
            modelBuilder.Entity<MprInterestMapping>().HasKey<int>(e => e.ID).Ignore(e => e.EntityId);
            modelBuilder.Entity<MprInterestMapping>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MprInterestMapping>().ToTable("mpr_interest_mapping");

            //MISNewOthersTEMP
            modelBuilder.Entity<MISNewOthersTEMP>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<MISNewOthersTEMP>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<MISNewOthersTEMP>().ToTable("mis_new_others_TEMPTEMP");

            //IncomeCustomerPoolRate
            modelBuilder.Entity<IncomeCustomerPoolRate>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeCustomerPoolRate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeCustomerPoolRate>().ToTable("Income_CustomerPoolRate");

            //IncomeAccountPoolRate
            modelBuilder.Entity<IncomeAccountPoolRate>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
            modelBuilder.Entity<IncomeAccountPoolRate>().Property(c => c.RowVersion).IsRowVersion();
            modelBuilder.Entity<IncomeAccountPoolRate>().ToTable("vw_Income_specialaccounts");

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

                //connectionString="Data Source=10.0.0.18\FintrakSQL2014;Initial Catalog=FintrakDB;User =sa;Password=sqluser10$;Integrated Security=False"
                connectionString = string.Format("Data Source= {0};Initial Catalog={1};User ={2};Password={3};Integrated Security={4}", companydb.Database.ServerName, companydb.Database.DatabaseName, companydb.Database.UserName, companydb.Database.Password, companydb.Database.IntegratedSecurity);
            }

            return connectionString;
        }

    }
}
