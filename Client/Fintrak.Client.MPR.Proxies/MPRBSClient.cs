using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.ServiceModel;
using System.Collections.Generic;

namespace Fintrak.Client.MPR.Proxies
{
    [Export(typeof(IMPRBSService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MPRBSClient : UserClientBase<IMPRBSService>, IMPRBSService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }



        #region BalanceSheetThreshold

        public BalanceSheetThreshold UpdateBalanceSheetThreshold(BalanceSheetThreshold balanceSheetThreshold)
        {
            return Channel.UpdateBalanceSheetThreshold(balanceSheetThreshold);
        }

        public void DeleteBalanceSheetThreshold(int balanceSheetThresholdId)
        {
            Channel.DeleteBalanceSheetThreshold(balanceSheetThresholdId);
        }

        public BalanceSheetThreshold GetBalanceSheetThreshold(int balanceSheetThresholdId)
        {
            return Channel.GetBalanceSheetThreshold(balanceSheetThresholdId);
        }

        public BalanceSheetThresholdData[] GetAllBalanceSheetThresholds()
        {
            return Channel.GetAllBalanceSheetThresholds();
        }


        #endregion

        #region BSCaption

        public BSCaption UpdateBSCaption(BSCaption bsCaption)
        {
            return Channel.UpdateBSCaption(bsCaption);
        }

        public void DeleteBSCaption(int bsCaptionId)
        {
            Channel.DeleteBSCaption(bsCaptionId);
        }

        public BSCaption GetBSCaption(int bsCaptionId)
        {
            return Channel.GetBSCaption(bsCaptionId);
        }

        public BSCaption[] GetBSCaptions()
        {
            return Channel.GetBSCaptions();
        }

        public BSCaptionData[] GetAllBSCaptions()
        {
            var result = Channel.GetAllBSCaptions();
            return result;
        }

        public BSCaptionData[] GetAllMPRBSCaptions()
        {
            var result = Channel.GetAllMPRBSCaptions();
            return result;
        }

        public BSCaptionData[] GetAllBudgetBSCaptions()
        {
            var result = Channel.GetAllBudgetBSCaptions();
            return result;
        }


        public BSCaptionData[] GetAllMPRBSCaptionsByCaptionName(string CaptionName)
        {
            var result = Channel.GetAllMPRBSCaptionsByCaptionName(CaptionName);
            return result;
        }

        public BSCaptionData[] GetAllBudgetBSCaptionsByCaptionName(string CaptionName)
        {
            var result = Channel.GetAllBudgetBSCaptionsByCaptionName(CaptionName);
            return result;
        }

        public BSCaptionDataN[] GetAllBSCaptionsN()
        {
            return Channel.GetAllBSCaptionsN();
            //  return result;
        }

        #endregion

        #region MPRProduct

        public MPRProduct UpdateMPRProduct(MPRProduct product)
        {
            return Channel.UpdateMPRProduct(product);
        }

        public void DeleteMPRProduct(int productId)
        {
            Channel.DeleteMPRProduct(productId);
        }

        public MPRProduct GetMPRProduct(int productId)
        {
            return Channel.GetMPRProduct(productId);
        }

        public MPRProductData[] GetAllMPRProducts()
        {
            return Channel.GetAllMPRProducts();
        }

        public MPRProductData[] GetAllMPRProductsByProductCode(string productCode)
        {
            return Channel.GetAllMPRProductsByProductCode(productCode);
        }

        public MPRProductData[] GetMPRProductByType(BalanceSheetType type)
        {
            return Channel.GetMPRProductByType(type);
        }

        public MPRProductData[] GetMPRProductByNotional(bool notional)
        {
            return Channel.GetMPRProductByNotional(notional);
        }

        public KeyValueData[] GetUnMappedProducts()
        {
            return Channel.GetUnMappedProducts();
        }

        #endregion

        #region NonProductMap

        public NonProductMap UpdateNonProductMap(NonProductMap nonProductMap)
        {
            return Channel.UpdateNonProductMap(nonProductMap);
        }

        public void DeleteNonProductMap(int nonProductMapId)
        {
            Channel.DeleteNonProductMap(nonProductMapId);
        }

        public NonProductMap GetNonProductMap(int nonProductMapId)
        {
            return Channel.GetNonProductMap(nonProductMapId);
        }

        public NonProductMapData[] GetAllNonProductMaps()
        {
            return Channel.GetAllNonProductMaps();
        }


        #endregion

        #region NonProductRate

        public NonProductRate UpdateNonProductRate(NonProductRate nonProductRate)
        {
            return Channel.UpdateNonProductRate(nonProductRate);
        }

        public void DeleteNonProductRate(int nonProductRateId)
        {
            Channel.DeleteNonProductRate(nonProductRateId);
        }

        public NonProductRate GetNonProductRate(int nonProductRateId)
        {
            return Channel.GetNonProductRate(nonProductRateId);
        }

        public NonProductRate[] GetAllNonProductRates()
        {
            return Channel.GetAllNonProductRates();
        }


        #endregion

        #region ProductMIS

        public ProductMIS UpdateProductMIS(ProductMIS productMIS)
        {
            return Channel.UpdateProductMIS(productMIS);
        }

        public void DeleteProductMIS(int productMISId)
        {
            Channel.DeleteProductMIS(productMISId);
        }

        public ProductMIS GetProductMIS(int productMISId)
        {
            return Channel.GetProductMIS(productMISId);
        }

        public ProductMISData[] GetAllProductMISs()
        {
            return Channel.GetAllProductMISs();
        }



        #endregion

        #region MPRBalanceSheet

        public MPRBalanceSheet UpdateBalanceSheet(MPRBalanceSheet balanceSheet)
        {
            return Channel.UpdateBalanceSheet(balanceSheet);
        }

        public void DeleteBalanceSheet(int balanceSheetId)
        {
            Channel.DeleteBalanceSheet(balanceSheetId);
        }

        public MPRBalanceSheet GetBalanceSheet(int balanceSheetId)
        {
            return Channel.GetBalanceSheet(balanceSheetId);
        }

        public MPRBalanceSheet[] GetmprBalanceSheets(int number)
        {
            return Channel.GetmprBalanceSheets(number);
        }

        public MPRBalanceSheet[] GetAllBalanceSheets(string searchType, string searchValue, int number, DateTime fromDate)
        {
            return Channel.GetAllBalanceSheets(searchType, searchValue, number, fromDate);
        }

        public MPRBalanceSheet[] GetAllMPRBalanceSheets()
        {
            return Channel.GetAllMPRBalanceSheets();
        }

        public MPRBalanceSheet[] GetRunDate()
        {
            return Channel.GetRunDate();
        }


        #endregion

        #region BalanceSheetAdjustment

        public MPRBalanceSheetAdjustment UpdateBalanceSheetAdjustment(MPRBalanceSheetAdjustment balanceSheetAdjustment)
        {
            return Channel.UpdateBalanceSheetAdjustment(balanceSheetAdjustment);
        }

        public void DeleteBalanceSheetAdjustment(int balanceSheetAdjustmentId)
        {
            Channel.DeleteBalanceSheetAdjustment(balanceSheetAdjustmentId);
        }

        public MPRBalanceSheetAdjustment GetBalanceSheetAdjustment(int balanceSheetAdjustmentId)
        {
            return Channel.GetBalanceSheetAdjustment(balanceSheetAdjustmentId);
        }

        public MPRBalanceSheetAdjustment[] GetAllBalanceSheetAdjustments()
        {
            return Channel.GetAllBalanceSheetAdjustments();
        }


        public MPRBalanceSheetAdjustment[] GetBalanceSheetAdjustments(string searchType, string searchValue, int number)
        {
            return Channel.GetBalanceSheetAdjustments(searchType, searchValue, number);
        }

        public MPRBalanceSheetAdjustment[] GetCodebyUsers(string userName)
        {
            return Channel.GetCodebyUsers(userName);
        }

        public MPRBalanceSheetAdjustment[] GetBalanceSheetAdjustmentByCode(string code, string userName)
        {
            return Channel.GetBalanceSheetAdjustmentByCode(code, userName);
        }

        public void DeleteMPRBalanceSheetAdjustment(string code, string userName)
        {
            Channel.DeleteMPRBalanceSheetAdjustment(code, userName);
        }


        #endregion

        #region BalanceSheetBudgetOfficer

        public BalanceSheetBudgetOfficer UpdateBalanceSheetBudgetOfficer(BalanceSheetBudgetOfficer balanceSheetBudgetOfficer)
        {
            return Channel.UpdateBalanceSheetBudgetOfficer(balanceSheetBudgetOfficer);
        }

        public void DeleteBalanceSheetBudgetOfficer(int balanceSheetBudgetOffId)
        {
            Channel.DeleteBalanceSheetBudgetOfficer(balanceSheetBudgetOffId);
        }

        public BalanceSheetBudgetOfficer GetBalanceSheetBudgetOfficer(int balanceSheetBudgetOffId)
        {
            return Channel.GetBalanceSheetBudgetOfficer(balanceSheetBudgetOffId);
        }

        public BalanceSheetBudgetOfficer[] GetAllBalanceSheetBudgetOfficers(string year)
        {
            return Channel.GetAllBalanceSheetBudgetOfficers(year);
        }

        public BalanceSheetBudgetOfficer[] GetBalanceSheetBudgetOfficers(string searchValue)
        {
            return Channel.GetBalanceSheetBudgetOfficers(searchValue);
        }


        #endregion

        #region BalanceSheetBudget

        public BalanceSheetBudget UpdateBalanceSheetBudget(BalanceSheetBudget balanceSheetBudget)
        {
            return Channel.UpdateBalanceSheetBudget(balanceSheetBudget);
        }

        public void DeleteBalanceSheetBudget(int balanceSheetBudgetId)
        {
            Channel.DeleteBalanceSheetBudget(balanceSheetBudgetId);
        }

        public BalanceSheetBudget GetBalanceSheetBudget(int balanceSheetBudgetId)
        {
            return Channel.GetBalanceSheetBudget(balanceSheetBudgetId);
        }

        public BalanceSheetBudget[] GetAllBalanceSheetBudgets(string year)
        {
            return Channel.GetAllBalanceSheetBudgets(year);
        }

        public BalanceSheetBudget[] GetBalanceSheetBudgets(string searchValue)
        {
            return Channel.GetBalanceSheetBudgets(searchValue);
        }

        public void DeleteBSBSelectedIds(string selectedIds)
        {
            Channel.DeleteBSBSelectedIds(selectedIds);
        }



        #endregion

        #region BSGLMapping

        public BSGLMapping UpdateBSGLMapping(BSGLMapping bsGLMapping)
        {
            return Channel.UpdateBSGLMapping(bsGLMapping);
        }

        public void DeleteBSGLMapping(int bsGLMappingId)
        {
            Channel.DeleteBSGLMapping(bsGLMappingId);
        }

        public BSGLMapping GetBSGLMapping(int bsGLMappingId)
        {
            return Channel.GetBSGLMapping(bsGLMappingId);
        }

        public BSGLMappingData[] GetAllBSGLMappings()
        {
            var result = Channel.GetAllBSGLMappings();
            return result;
        }



        #endregion

        #region BSINOtherInformation

        public BSINOtherInformation UpdateBSINOtherInformation(BSINOtherInformation bSINOtherInformation)
        {
            return Channel.UpdateBSINOtherInformation(bSINOtherInformation);
        }

        public void DeleteBSINOtherInformation(int bSINOtherInformationId)
        {
            Channel.DeleteBSINOtherInformation(bSINOtherInformationId);
        }

        public BSINOtherInformation GetBSINOtherInformation(int bSINOtherInformationId)
        {
            return Channel.GetBSINOtherInformation(bSINOtherInformationId);
        }

        public BSINOtherInformation[] GetAllBSINOtherInformations()
        {
            var result = Channel.GetAllBSINOtherInformations();
            return result;
        }

        public IEnumerable<BSCaption> GetAllBsPlCaptions()
        {

            return Channel.GetAllBsPlCaptions();
        }

        #endregion

        #region BSINOtherInformationTotalLine

        public BSINOtherInformationTotalLine UpdateBSINOtherInformationTotalLine(BSINOtherInformationTotalLine bSINOtherInformationTotalLine)
        {
            return Channel.UpdateBSINOtherInformationTotalLine(bSINOtherInformationTotalLine);
        }

        public void DeleteBSINOtherInformationTotalLine(int bSINOtherInformationTotalLineId)
        {
            Channel.DeleteBSINOtherInformationTotalLine(bSINOtherInformationTotalLineId);
        }

        public BSINOtherInformationTotalLine GetBSINOtherInformationTotalLine(int bSINOtherInformationTotalLineId)
        {
            return Channel.GetBSINOtherInformationTotalLine(bSINOtherInformationTotalLineId);
        }

        public BSINOtherInformationTotalLine[] GetAllBSINOtherInformationTotalLines()
        {
            var result = Channel.GetAllBSINOtherInformationTotalLines();
            return result;
        }

        public IEnumerable<BSCaption> GetAllBsPlOtherInfoCaptions()
        {

            return Channel.GetAllBsPlOtherInfoCaptions();
        }

        #endregion

        #region NRFFCaption

        public NRFFCaption UpdateNRFFCaption(NRFFCaption nRFFCaption)
        {
            return Channel.UpdateNRFFCaption(nRFFCaption);
        }

        public void DeleteNRFFCaption(int NRFFCaption_Id)
        {
            Channel.DeleteNRFFCaption(NRFFCaption_Id);
        }

        public NRFFCaption GetNRFFCaption(int NRFFCaption_Id)
        {
            return Channel.GetNRFFCaption(NRFFCaption_Id);
        }

        public NRFFCaption[] GetAllNRFFCaptions()
        {
            var result = Channel.GetAllNRFFCaptions();
            return result;
        }

        #endregion

        #region CategoryTransferPrice

        public CategoryTransferPrice UpdateCategoryTransferPrice(CategoryTransferPrice categoryTransferPrice)
        {
            return Channel.UpdateCategoryTransferPrice(categoryTransferPrice);
        }

        public void DeleteCategoryTransferPrice(int CategoryTransferPriceId)
        {
            Channel.DeleteCategoryTransferPrice(CategoryTransferPriceId);
        }

        public CategoryTransferPrice GetCategoryTransferPrice(int CategoryTransferPriceId)
        {
            return Channel.GetCategoryTransferPrice(CategoryTransferPriceId);
        }

        public CategoryTransferPrice[] GetAllCategoryTransferPrices()
        {
            return Channel.GetAllCategoryTransferPrices();
        }

        public CategoryTransferPriceData[] GetCategoryTransferPriceUsingSetUp()
        {
            return Channel.GetCategoryTransferPriceUsingSetUp();
        }

        public CategoryTransferPriceData[] GetCategoryTransferPriceUsingsearch(string search)
        {
            return Channel.GetCategoryTransferPriceUsingsearch(search);
        }

        #endregion

        #region AcquirerMapping

        public AcquirerMapping UpdateAcquirerMapping(AcquirerMapping AcquirerMapping)
        {
            return Channel.UpdateAcquirerMapping(AcquirerMapping);
        }

        public void DeleteAcquirerMapping(int mpr_Acquirer_Mapping_Id)
        {
            Channel.DeleteAcquirerMapping(mpr_Acquirer_Mapping_Id);
        }

        public AcquirerMapping GetAcquirerMapping(int mpr_Acquirer_Mapping_Id)
        {
            return Channel.GetAcquirerMapping(mpr_Acquirer_Mapping_Id);
        }

        public AcquirerMapping[] GetAllAcquirerMappings()
        {
            return Channel.GetAllAcquirerMappings();
        }

        #endregion

        #region AcquirerSharing

        public AcquirerSharing UpdateAcquirerSharing(AcquirerSharing AcquirerSharing)
        {
            return Channel.UpdateAcquirerSharing(AcquirerSharing);
        }

        public void DeleteAcquirerSharing(int mpr_Acquirer_Sharing_Id)
        {
            Channel.DeleteAcquirerSharing(mpr_Acquirer_Sharing_Id);
        }

        public AcquirerSharing GetAcquirerSharing(int mpr_Acquirer_Sharing_Id)
        {
            return Channel.GetAcquirerSharing(mpr_Acquirer_Sharing_Id);
        }

        public AcquirerSharing[] GetAllAcquirerSharings()
        {
            return Channel.GetAllAcquirerSharings();
        }

        #endregion

        #region CustomerTransferPrice

        public CustomerTransferPrice UpdateCustomerTransferPrice(CustomerTransferPrice CustomerTransferPrice)
        {
            return Channel.UpdateCustomerTransferPrice(CustomerTransferPrice);
        }

        public void DeleteCustomerTransferPrice(int customertransferpriceId)
        {
            Channel.DeleteCustomerTransferPrice(customertransferpriceId);
        }

        public CustomerTransferPrice GetCustomerTransferPrice(int customertransferpriceId)
        {
            return Channel.GetCustomerTransferPrice(customertransferpriceId);
        }

        public CustomerTransferPrice[] GetAllCustomerTransferPrices()
        {
            return Channel.GetAllCustomerTransferPrices();
        }

        public CustomerTransferPriceData[] GetCustomerTransferPricebySetUp()
        {
            return Channel.GetCustomerTransferPricebySetUp();
        }

        public CustomerTransferPriceData[] GetCustomerTransferPricebysearch(string search)
        {
            return Channel.GetCustomerTransferPricebysearch(search);
        }

        #endregion

        #region TeamSector

        public TeamSector UpdateTeamSector(TeamSector teamsector)
        {
            return Channel.UpdateTeamSector(teamsector);
        }

        public void DeleteTeamSector(int Mpr_Team_Sector_ID)
        {
            Channel.DeleteTeamSector(Mpr_Team_Sector_ID);
        }

        public TeamSector GetTeamSector(int Mpr_Team_Sector_ID)
        {
            return Channel.GetTeamSector(Mpr_Team_Sector_ID);
        }

        public TeamSector[] GetAllTeamSectors()
        {
            return Channel.GetAllTeamSectors();
        }

        public TeamSector[] GetTeamSectorUsingsearch(string search)
        {
            return Channel.GetTeamSectorUsingsearch(search);
        }

        #endregion

        #region TeamSegment

        public TeamSegment UpdateTeamSegment(TeamSegment teamsegment)
        {
            return Channel.UpdateTeamSegment(teamsegment);
        }

        public void DeleteTeamSegment(int Mpr_Team_Segment_ID)
        {
            Channel.DeleteTeamSegment(Mpr_Team_Segment_ID);
        }

        public TeamSegment GetTeamSegment(int Mpr_Team_Segment_ID)
        {
            return Channel.GetTeamSegment(Mpr_Team_Segment_ID);
        }

        public TeamSegment[] GetAllTeamSegments()
        {
            return Channel.GetAllTeamSegments();
        }

        public TeamSegment[] GetTeamSegmentUsingsearch(string search)
        {
            return Channel.GetTeamSegmentUsingsearch(search);
        }

        #endregion

        #region IncomeBranches

        public IncomeBranches UpdateIncomeBranches(IncomeBranches Id)
        {
            return Channel.UpdateIncomeBranches(Id);
        }

        public void DeleteIncomeBranches(int Id)
        {
            Channel.DeleteIncomeBranches(Id);
        }

        public IncomeBranches GetIncomeBranches(int Id)
        {
            return Channel.GetIncomeBranches(Id);
        }

        public IncomeBranches[] GetAllIncomeBranches()
        {
            return Channel.GetAllIncomeBranches();
        }

        #endregion

        #region Income Split PoolsRates And Basis

        public IncomeSplitPoolsRatesAndBasis UpdateIncomeSplitPoolsRatesAndBasis(IncomeSplitPoolsRatesAndBasis Id)
        {
            return Channel.UpdateIncomeSplitPoolsRatesAndBasis(Id);
        }

        public void DeleteIncomeSplitPoolsRatesAndBasis(int Id)
        {
            Channel.DeleteIncomeSplitPoolsRatesAndBasis(Id);
        }

        public IncomeSplitPoolsRatesAndBasis GetIncomeSplitPoolsRatesAndBasis(int Id)
        {
            return Channel.GetIncomeSplitPoolsRatesAndBasis(Id);
        }

        public IncomeSplitPoolsRatesAndBasis[] GetAllIncomeSplitPoolsRatesAndBasis()
        {
            return Channel.GetAllIncomeSplitPoolsRatesAndBasis();
        }

        public IncomeSplitPoolsRatesAndBasis[] IncomeSplitUsingYearAndPeriod(int year, int period)
        {
            return Channel.IncomeSplitUsingYearAndPeriod(year, period);
        }

        #endregion

        #region Income Retail Product Override

        public IncomeRetailProductOverride UpdateIncomeRetailProductOverride(IncomeRetailProductOverride ic)
        {
            return Channel.UpdateIncomeRetailProductOverride(ic);
        }

        public void DeleteIncomeRetailProductOverride(int Id)
        {
            Channel.DeleteIncomeRetailProductOverride(Id);
        }

        public IncomeRetailProductOverride GetIncomeRetailProductOverride(int Id)
        {
            return Channel.GetIncomeRetailProductOverride(Id);
        }

        public IncomeRetailProductOverride[] GetAllIncomeRetailProductOverride()
        {
            return Channel.GetAllIncomeRetailProductOverride();
        }

        public IncomeRetailProductOverride[] OverrideUsingCustomerIdAndBank(int customerId, string bank)
        {
            return Channel.OverrideUsingCustomerIdAndBank(customerId, bank);
        }

        #endregion

        #region Income Retail Product Override TEMP

        public IncomeRetailProductOverrideTEMP UpdateIncomeRetailProductOverrideTEMP(IncomeRetailProductOverrideTEMP ic)
        {
            return Channel.UpdateIncomeRetailProductOverrideTEMP(ic);
        }

        public void DeleteIncomeRetailProductOverrideTEMP(int Id)
        {
            Channel.DeleteIncomeRetailProductOverrideTEMP(Id);
        }

        public IncomeRetailProductOverrideTEMP GetIncomeRetailProductOverrideTEMP(int Id)
        {
            return Channel.GetIncomeRetailProductOverrideTEMP(Id);
        }

        public IncomeRetailProductOverrideTEMP[] GetAllIncomeRetailProductOverrideTEMP()
        {
            return Channel.GetAllIncomeRetailProductOverrideTEMP();
        }

        public IncomeRetailProductOverrideTEMP[] OverrideUsingCustomerIdAndBankTEMP(int customerId, string bank)
        {
            return Channel.OverrideUsingCustomerIdAndBankTEMP(customerId, bank);
        }

        public IncomeRetailProductOverrideTEMP[] SearchByCustomerORMISORAcctOfficer(string search)
        {
            return Channel.SearchByCustomerORMISORAcctOfficer(search);
        }

        #endregion

        #region Income Account MIS Override TEMP

        public IncomeAccountMISOverrideTEMP UpdateIncomeAccountMISOverrideTEMP(IncomeAccountMISOverrideTEMP ic)
        {
            return Channel.UpdateIncomeAccountMISOverrideTEMP(ic);
        }

        public void DeleteIncomeAccountMISOverrideTEMP(int Id)
        {
            Channel.DeleteIncomeAccountMISOverrideTEMP(Id);
        }

        public IncomeAccountMISOverrideTEMP GetIncomeAccountMISOverrideTEMP(int Id)
        {
            return Channel.GetIncomeAccountMISOverrideTEMP(Id);
        }

        public IncomeAccountMISOverrideTEMP[] GetAllIncomeAccountMISOverrideTEMP()
        {
            return Channel.GetAllIncomeAccountMISOverrideTEMP();
        }

        public IncomeAccountMISOverrideTEMP[] OverrideUsingAccountNumberTEMP(string accountno)
        {
            return Channel.OverrideUsingAccountNumberTEMP(accountno);
        }

        public IncomeAccountMISOverrideTEMP[] SearchByAccountNoORMISORAcctOfficer(string search)
        {
            return Channel.SearchByAccountNoORMISORAcctOfficer(search);
        }

        #endregion

        #region IncomeCommFeeBusinessRule

        public IncomeCommFeeBusinessRule UpdateIncomeCommFeeBusinessRule(IncomeCommFeeBusinessRule incomeCommFeeBusinessRule)
        {
            return Channel.UpdateIncomeCommFeeBusinessRule(incomeCommFeeBusinessRule);
        }

        public void DeleteIncomeCommFeeBusinessRule(int ID)
        {
            Channel.DeleteIncomeCommFeeBusinessRule(ID);
        }

        public IncomeCommFeeBusinessRule GetIncomeCommFeeBusinessRule(int ID)
        {
            return Channel.GetIncomeCommFeeBusinessRule(ID);
        }

        public IncomeCommFeeBusinessRule[] GetAllIncomeCommFeeBusinessRule()
        {
            return Channel.GetAllIncomeCommFeeBusinessRule();
        }

        public IncomeCommFeeBusinessRule[] GetIncomeCommFeeBusinessRuleUsingSearchValue(string searchvalue)
        {
            return Channel.GetIncomeCommFeeBusinessRuleUsingSearchValue(searchvalue);
        }

        #endregion

        #region IncomeCustomerRatingOverride

        public IncomeCustomerRatingOverride UpdateIncomeCustomerRatingOverride(IncomeCustomerRatingOverride icro)
        {
            return Channel.UpdateIncomeCustomerRatingOverride(icro);
        }

        public void DeleteIncomeCustomerRatingOverride(int Id)
        {
            Channel.DeleteIncomeCustomerRatingOverride(Id);
        }

        public IncomeCustomerRatingOverride GetIncomeCustomerRatingOverride(int Id)
        {
            return Channel.GetIncomeCustomerRatingOverride(Id);
        }

        public IncomeCustomerRatingOverride[] GetAllIncomeCustomerRatingOverride()
        {
            return Channel.GetAllIncomeCustomerRatingOverride();
        }

        public IncomeCustomerRatingOverride[] GetOverrideByRefNumber(string refnumber)
        {
            return Channel.GetOverrideByRefNumber(refnumber);
        }

        #endregion

        #region IncomeAccountsListing

        public IncomeAccountsListing UpdateIncomeAccountsListing(IncomeAccountsListing ial)
        {
            return Channel.UpdateIncomeAccountsListing(ial);
        }

        public void DeleteIncomeAccountsListing(int Id)
        {
            Channel.DeleteIncomeAccountsListing(Id);
        }

        public IncomeAccountsListing GetIncomeAccountsListing(int Id)
        {
            return Channel.GetIncomeAccountsListing(Id);
        }

        public IncomeAccountsListing[] GetAllIncomeAccountsListing()
        {
            return Channel.GetAllIncomeAccountsListing();
        }

        public IncomeAccountsListing[] FilterByAccountNumber(string accountnumber)
        {
            return Channel.FilterByAccountNumber(accountnumber);
        }

        #endregion

        #region IncomeCustomerRatingOverride TEMP

        public IncomeCustomerRatingOverrideTEMP UpdateIncomeCustomerRatingOverrideTEMP(IncomeCustomerRatingOverrideTEMP icro)
        {
            return Channel.UpdateIncomeCustomerRatingOverrideTEMP(icro);
        }

        public void DeleteIncomeCustomerRatingOverrideTEMP(int Id)
        {
            Channel.DeleteIncomeCustomerRatingOverrideTEMP(Id);
        }

        public IncomeCustomerRatingOverrideTEMP GetIncomeCustomerRatingOverrideTEMP(int Id)
        {
            return Channel.GetIncomeCustomerRatingOverrideTEMP(Id);
        }

        public IncomeCustomerRatingOverrideTEMP[] GetAllIncomeCustomerRatingOverrideTEMP()
        {
            return Channel.GetAllIncomeCustomerRatingOverrideTEMP();
        }

        public IncomeCustomerRatingOverrideTEMP[] GetOverrideByRefNumberTEMP(string refnumber)
        {
            return Channel.GetOverrideByRefNumberTEMP(refnumber);
        }

        #endregion

        #region Volume Analysis Rundates

        public VolumeAnalysisRundates UpdateVolumeAnalysisRundates(VolumeAnalysisRundates vrd)
        {
            return Channel.UpdateVolumeAnalysisRundates(vrd);
        }

        public void DeleteVolumeAnalysisRundates(int Id)
        {
            Channel.DeleteVolumeAnalysisRundates(Id);
        }

        public VolumeAnalysisRundates GetVolumeAnalysisRundates(int Id)
        {
            return Channel.GetVolumeAnalysisRundates(Id);
        }

        public VolumeAnalysisRundates[] GetAllVolumeAnalysisRundates()
        {
            return Channel.GetAllVolumeAnalysisRundates();
        }

        #endregion

    }
}
