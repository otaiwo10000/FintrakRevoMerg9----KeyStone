using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Exceptions;
using System.Collections.Generic;

namespace Fintrak.Business.MPR.Contracts
{
    [ServiceContract]
    public interface IMPRBSService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();


        #region BSCaption

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        BSCaption UpdateBSCaption(BSCaption bsCaption);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBSCaption(int bsCaptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BSCaption GetBSCaption(int bsCaptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BSCaption[] GetBSCaptions();

        [OperationContract]
        BSCaptionData[] GetAllBSCaptions();

        [OperationContract]
        BSCaptionData[] GetAllMPRBSCaptions();

        [OperationContract]
        BSCaptionData[] GetAllBudgetBSCaptions();

        [OperationContract]
        BSCaptionData[] GetAllMPRBSCaptionsByCaptionName(string CaptionName);

        [OperationContract]
        BSCaptionData[] GetAllBudgetBSCaptionsByCaptionName(string CaptionName);

        [OperationContract]
        BSCaptionDataN[] GetAllBSCaptionsN();


        #endregion

        #region MPRProduct

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MPRProduct UpdateMPRProduct(MPRProduct product);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMPRProduct(int productId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MPRProduct GetMPRProduct(int productId);

        [OperationContract]
        MPRProductData[] GetAllMPRProducts();

        [OperationContract]
        MPRProductData[] GetAllMPRProductsByProductCode(string productCode);

        [OperationContract]
        MPRProductData[] GetMPRProductByType(BalanceSheetType type);

        [OperationContract]
        MPRProductData[] GetMPRProductByNotional(bool notional);

        [OperationContract]
        KeyValueData[] GetUnMappedProducts();

        #endregion

        #region NonProductMap

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        NonProductMap UpdateNonProductMap(NonProductMap nonProductMap);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteNonProductMap(int nonProductMapId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        NonProductMap GetNonProductMap(int nonProductMapId);

        [OperationContract]
        NonProductMapData[] GetAllNonProductMaps();

        #endregion

        #region NonProductRate

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        NonProductRate UpdateNonProductRate(NonProductRate nonProductRate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteNonProductRate(int nonProductRateId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        NonProductRate GetNonProductRate(int nonProductRateId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        NonProductRate[] GetAllNonProductRates();

        #endregion

        #region ProductMIS

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ProductMIS UpdateProductMIS(ProductMIS productMIS);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteProductMIS(int productMISId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ProductMIS GetProductMIS(int productMISId);

        [OperationContract]
        ProductMISData[] GetAllProductMISs();

        #endregion

        #region BalanceSheetThreshold

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        BalanceSheetThreshold UpdateBalanceSheetThreshold(BalanceSheetThreshold balanceSheetThreshold);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBalanceSheetThreshold(int balanceSheetThresholdId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BalanceSheetThreshold GetBalanceSheetThreshold(int balanceSheetThresholdId);

        [OperationContract]
        BalanceSheetThresholdData[] GetAllBalanceSheetThresholds();


        #endregion

        #region BalanceSheetAdjustment

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MPRBalanceSheetAdjustment UpdateBalanceSheetAdjustment(MPRBalanceSheetAdjustment balanceSheetAdjustment);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBalanceSheetAdjustment(int balanceSheetAdjustmentId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MPRBalanceSheetAdjustment GetBalanceSheetAdjustment(int balanceSheetAdjustmentId);

        [OperationContract]
        MPRBalanceSheetAdjustment[] GetAllBalanceSheetAdjustments();

        [OperationContract]
        MPRBalanceSheetAdjustment[] GetBalanceSheetAdjustments(string searchType, string searchValue, int number);

        [OperationContract]
        MPRBalanceSheetAdjustment[] GetCodebyUsers(string userName);

        [OperationContract]
        MPRBalanceSheetAdjustment[] GetBalanceSheetAdjustmentByCode(string code, string userName);


        [OperationContract]
        void DeleteMPRBalanceSheetAdjustment(string code, string userName);


        #endregion

        #region BalanceSheet

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MPRBalanceSheet UpdateBalanceSheet(MPRBalanceSheet balanceSheet);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBalanceSheet(int balanceSheetId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MPRBalanceSheet GetBalanceSheet(int balanceSheetId);

        [OperationContract]
        MPRBalanceSheet[] GetmprBalanceSheets(int number);

        [OperationContract]
        MPRBalanceSheet[] GetRunDate();

        [OperationContract]
        MPRBalanceSheet[] GetAllBalanceSheets(string searchType, string searchValue, int number, DateTime fromDate);

        //[OperationContract]
        //MPRBalanceSheet[] GetAllBalanceSheets(DateTime fromDate);

        [OperationContract]
        MPRBalanceSheet[] GetAllMPRBalanceSheets();



        #endregion

        #region BalanceSheetBudget

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        BalanceSheetBudget UpdateBalanceSheetBudget(BalanceSheetBudget balanceSheetBudget);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBalanceSheetBudget(int balanceSheetBudgetId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BalanceSheetBudget GetBalanceSheetBudget(int balanceSheetBudgetId);

        [OperationContract]
        BalanceSheetBudget[] GetAllBalanceSheetBudgets(string year);

        [OperationContract]
        BalanceSheetBudget[] GetBalanceSheetBudgets(string searchValue);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBSBSelectedIds(string selectedIds);

        #endregion

        #region BalanceSheetBudgetOfficer

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        BalanceSheetBudgetOfficer UpdateBalanceSheetBudgetOfficer(BalanceSheetBudgetOfficer balanceSheetBudgetOfficer);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBalanceSheetBudgetOfficer(int balanceSheetBudgetOffId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BalanceSheetBudgetOfficer GetBalanceSheetBudgetOfficer(int balanceSheetBudgetOffId);

        [OperationContract]
        BalanceSheetBudgetOfficer[] GetAllBalanceSheetBudgetOfficers(string year);

        [OperationContract]
        BalanceSheetBudgetOfficer[] GetBalanceSheetBudgetOfficers(string searchValue);



        #endregion

        #region BSGLMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        BSGLMapping UpdateBSGLMapping(BSGLMapping bsGLMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBSGLMapping(int bsGLMappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BSGLMapping GetBSGLMapping(int bsGLMappingId);

        [OperationContract]
        BSGLMappingData[] GetAllBSGLMappings();


        #endregion

        #region BSINOtherInformation

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        BSINOtherInformation UpdateBSINOtherInformation(BSINOtherInformation bSINOtherInformation);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBSINOtherInformation(int bSINOtherInformationId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BSINOtherInformation GetBSINOtherInformation(int bSINOtherInformationId);

        [OperationContract]
        BSINOtherInformation[] GetAllBSINOtherInformations();

        [OperationContract]
        IEnumerable<BSCaption> GetAllBsPlCaptions();

        #endregion


        #region BSINOtherInformationTotalLine

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        BSINOtherInformationTotalLine UpdateBSINOtherInformationTotalLine(BSINOtherInformationTotalLine bSINOtherInformationTotalLine);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBSINOtherInformationTotalLine(int bSINOtherInformationTotalLineId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BSINOtherInformationTotalLine GetBSINOtherInformationTotalLine(int bSINOtherInformationTotalLineId);

        [OperationContract]
        BSINOtherInformationTotalLine[] GetAllBSINOtherInformationTotalLines();

        [OperationContract]
        IEnumerable<BSCaption> GetAllBsPlOtherInfoCaptions();

        #endregion

        #region NRFFCaption

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        NRFFCaption UpdateNRFFCaption(NRFFCaption nRFFCaption);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteNRFFCaption(int NRFFCaption_Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        NRFFCaption GetNRFFCaption(int NRFFCaption_Id);

        [OperationContract]
        NRFFCaption[] GetAllNRFFCaptions();

        #endregion

        #region CategoryTransferPrice

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CategoryTransferPrice UpdateCategoryTransferPrice(CategoryTransferPrice categoryTransferPrice);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCategoryTransferPrice(int CategoryTransferPriceId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CategoryTransferPrice GetCategoryTransferPrice(int CategoryTransferPriceId);

        [OperationContract]
        CategoryTransferPrice[] GetAllCategoryTransferPrices();

        [OperationContract]
        CategoryTransferPriceData[] GetCategoryTransferPriceUsingSetUp();

        [OperationContract]
        CategoryTransferPriceData[] GetCategoryTransferPriceUsingsearch(string search);


        #endregion

        #region AcquirerMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        AcquirerMapping UpdateAcquirerMapping(AcquirerMapping AcquirerMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteAcquirerMapping(int mpr_Acquirer_Mapping_Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        AcquirerMapping GetAcquirerMapping(int mpr_Acquirer_Mapping_Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        AcquirerMapping[] GetAllAcquirerMappings();


        #endregion

        #region AcquirerSharing

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        AcquirerSharing UpdateAcquirerSharing(AcquirerSharing AcquirerSharing);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteAcquirerSharing(int mpr_Acquirer_Sharing_Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        AcquirerSharing GetAcquirerSharing(int mpr_Acquirer_Sharing_Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        AcquirerSharing[] GetAllAcquirerSharings();


        #endregion

        #region CustomerTransferPrice

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CustomerTransferPrice UpdateCustomerTransferPrice(CustomerTransferPrice CustomerTransferPrice);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCustomerTransferPrice(int customertransferpriceId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CustomerTransferPrice GetCustomerTransferPrice(int customertransferpriceId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CustomerTransferPrice[] GetAllCustomerTransferPrices();

        [OperationContract]
        CustomerTransferPriceData[] GetCustomerTransferPricebySetUp();

        [OperationContract]
        CustomerTransferPriceData[] GetCustomerTransferPricebysearch(string search);

        #endregion

        #region TeamSector

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        TeamSector UpdateTeamSector(TeamSector teamsector);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTeamSector(int Mpr_Team_Sector_ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TeamSector GetTeamSector(int Mpr_Team_Sector_ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TeamSector[] GetAllTeamSectors();
        
        [OperationContract]
        TeamSector[] GetTeamSectorUsingsearch(string search);

        #endregion

        #region TeamSegment

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        TeamSegment UpdateTeamSegment(TeamSegment teamsegment);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTeamSegment(int Mpr_Team_Segment_ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TeamSegment GetTeamSegment(int Mpr_Team_Segment_ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TeamSegment[] GetAllTeamSegments();

        [OperationContract]
        TeamSegment[] GetTeamSegmentUsingsearch(string search);

        #endregion

        #region IncomeBranches

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeBranches UpdateIncomeBranches(IncomeBranches ib);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeBranches(int Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeBranches GetIncomeBranches(int Id);

        [OperationContract]
        IncomeBranches[] GetAllIncomeBranches();


        #endregion

        #region Income Split PoolsRates And Basis

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeSplitPoolsRatesAndBasis UpdateIncomeSplitPoolsRatesAndBasis(IncomeSplitPoolsRatesAndBasis ic);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeSplitPoolsRatesAndBasis(int Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeSplitPoolsRatesAndBasis GetIncomeSplitPoolsRatesAndBasis(int Id);

        [OperationContract]
        IncomeSplitPoolsRatesAndBasis[] GetAllIncomeSplitPoolsRatesAndBasis();

        [OperationContract]
        IncomeSplitPoolsRatesAndBasis[] IncomeSplitUsingYearAndPeriod(int year, int period);


        #endregion

        #region Income Retail Product Override

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeRetailProductOverride UpdateIncomeRetailProductOverride(IncomeRetailProductOverride ic);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeRetailProductOverride(int Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeRetailProductOverride GetIncomeRetailProductOverride(int Id);

        [OperationContract]
        IncomeRetailProductOverride[] GetAllIncomeRetailProductOverride();

        [OperationContract]
        IncomeRetailProductOverride[] OverrideUsingCustomerIdAndBank(int customerId, string bank);


        #endregion

        #region Income Retail Product Override TEMP

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeRetailProductOverrideTEMP UpdateIncomeRetailProductOverrideTEMP(IncomeRetailProductOverrideTEMP ic);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeRetailProductOverrideTEMP(int Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeRetailProductOverrideTEMP GetIncomeRetailProductOverrideTEMP(int Id);

        [OperationContract]
        IncomeRetailProductOverrideTEMP[] GetAllIncomeRetailProductOverrideTEMP();

        [OperationContract]
        IncomeRetailProductOverrideTEMP[] OverrideUsingCustomerIdAndBankTEMP(int customerId, string bank);

        [OperationContract]
        IncomeRetailProductOverrideTEMP[] SearchByCustomerORMISORAcctOfficer(string search);


        #endregion

        #region Income Account MIS Override TEMP

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeAccountMISOverrideTEMP UpdateIncomeAccountMISOverrideTEMP(IncomeAccountMISOverrideTEMP ic);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeAccountMISOverrideTEMP(int Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeAccountMISOverrideTEMP GetIncomeAccountMISOverrideTEMP(int Id);

        [OperationContract]
        IncomeAccountMISOverrideTEMP[] GetAllIncomeAccountMISOverrideTEMP();

        [OperationContract]
        IncomeAccountMISOverrideTEMP[] OverrideUsingAccountNumberTEMP(string accountno);

        [OperationContract]
        IncomeAccountMISOverrideTEMP[] SearchByAccountNoORMISORAcctOfficer(string search);


        #endregion

        #region IncomeCommFeeBusinessRule

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeCommFeeBusinessRule UpdateIncomeCommFeeBusinessRule(IncomeCommFeeBusinessRule incomeCommFeeBusinessRule);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeCommFeeBusinessRule(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeCommFeeBusinessRule GetIncomeCommFeeBusinessRule(int ID);

        [OperationContract]
        IncomeCommFeeBusinessRule[] GetAllIncomeCommFeeBusinessRule();

        [OperationContract]
        IncomeCommFeeBusinessRule[] GetIncomeCommFeeBusinessRuleUsingSearchValue(string searchvalue);

        #endregion

        #region IncomeCustomerRatingOverride

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeCustomerRatingOverride UpdateIncomeCustomerRatingOverride(IncomeCustomerRatingOverride icro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeCustomerRatingOverride(int Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeCustomerRatingOverride GetIncomeCustomerRatingOverride(int Id);

        [OperationContract]
        IncomeCustomerRatingOverride[] GetAllIncomeCustomerRatingOverride();

        [OperationContract]
        IncomeCustomerRatingOverride[] GetOverrideByRefNumber(string refnumber);

        #endregion

        #region IncomeAccountsListing

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeAccountsListing UpdateIncomeAccountsListing(IncomeAccountsListing ial);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeAccountsListing(int Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeAccountsListing GetIncomeAccountsListing(int Id);

        [OperationContract]
        IncomeAccountsListing[] GetAllIncomeAccountsListing();

        [OperationContract]
        IncomeAccountsListing[] FilterByAccountNumber(string accountnumber);

        #endregion

        #region IncomeCustomerRatingOverride TEMP

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeCustomerRatingOverrideTEMP UpdateIncomeCustomerRatingOverrideTEMP(IncomeCustomerRatingOverrideTEMP icro);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeCustomerRatingOverrideTEMP(int Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeCustomerRatingOverrideTEMP GetIncomeCustomerRatingOverrideTEMP(int Id);

        [OperationContract]
        IncomeCustomerRatingOverrideTEMP[] GetAllIncomeCustomerRatingOverrideTEMP();

        [OperationContract]
        IncomeCustomerRatingOverrideTEMP[] GetOverrideByRefNumberTEMP(string refnumber);

        #endregion

        #region VolumeAnalysisRundates

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        VolumeAnalysisRundates UpdateVolumeAnalysisRundates(VolumeAnalysisRundates vrd);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteVolumeAnalysisRundates(int Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        VolumeAnalysisRundates GetVolumeAnalysisRundates(int Id);

        [OperationContract]
        VolumeAnalysisRundates[] GetAllVolumeAnalysisRundates();


        #endregion


    }
}
