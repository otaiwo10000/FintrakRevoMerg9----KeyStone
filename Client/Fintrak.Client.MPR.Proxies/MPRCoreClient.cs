using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Shared.Common.ServiceModel;
using System.Collections.Generic;

namespace Fintrak.Client.MPR.Proxies
{
    [Export(typeof(IMPRCoreService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MPRCoreClient : UserClientBase<IMPRCoreService>, IMPRCoreService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }

        #region UserMIS

        public UserMIS UpdateUserMIS(UserMIS userMIS)
        {
            return Channel.UpdateUserMIS(userMIS);
        }

        public void DeleteUserMIS(int userMISId)
        {
            Channel.DeleteUserMIS(userMISId);
        }

        public UserMIS GetUserMIS(int userMISId)
        {
            return Channel.GetUserMIS(userMISId);
        }

        public UserMIS GetUserMISByLoginID(string loginID)
        {
            return Channel.GetUserMISByLoginID(loginID);
        }

        public UserMIS[] GetAllUserMISs()
        {
            return Channel.GetAllUserMISs();
        }



        #endregion

        #region UserClassificationMap

        public UserClassificationMap UpdateUserClassificationMap(UserClassificationMap userClassificationMap)
        {
            return Channel.UpdateUserClassificationMap(userClassificationMap);
        }

        public void DeleteUserClassificationMap(int userClassificationMapId)
        {
            Channel.DeleteUserClassificationMap(userClassificationMapId);
        }

        public UserClassificationMap GetUserClassificationMap(int userClassificationMapId)
        {
            return Channel.GetUserClassificationMap(userClassificationMapId);
        }

        public UserClassificationMap[] GetAllUserClassificationMaps(string loginID)
        {
            return Channel.GetAllUserClassificationMaps(loginID);
        }



        #endregion

        #region TeamDefinition

        public TeamDefinition UpdateTeamDefinition(TeamDefinition teamDefinition)
        {
            return Channel.UpdateTeamDefinition(teamDefinition);
        }

        public void DeleteTeamDefinition(int teamDefinitionId)
        {
            Channel.DeleteTeamDefinition(teamDefinitionId);
        }

        public TeamDefinition GetTeamDefinition(int teamDefinitionId)
        {
            return Channel.GetTeamDefinition(teamDefinitionId);
        }

        public TeamDefinition GetTeamDefinitionByCode(string code)
        {
            return Channel.GetTeamDefinitionByCode(code);
        }

        public IEnumerable<TeamDefinition> GetAllTeamDefinitions()
        {
            return Channel.GetAllTeamDefinitions();
        }



        #endregion

        #region AccountMIS

        public AccountMIS UpdateAccountMIS(AccountMIS accountMIS)
        {
            return Channel.UpdateAccountMIS(accountMIS);
        }

        public void DeleteAccountMIS(int accountMISId)
        {
            Channel.DeleteAccountMIS(accountMISId);
        }

        public AccountMIS GetAccountMIS(int accountMISId)
        {
            return Channel.GetAccountMIS(accountMISId);
        }

        public AccountMISData[] GetAllAccountMISs()
        {
            return Channel.GetAllAccountMISs();
        }

        public void DeleteSelectedIds(string selectedIds)
        {
            Channel.DeleteSelectedIds(selectedIds);
        }

        #endregion

        #region AccountOfficerDetail

        public AccountOfficerDetail UpdateAccountOfficerDetail(AccountOfficerDetail accountOfficerDetail)
        {
            return Channel.UpdateAccountOfficerDetail(accountOfficerDetail);
        }

        public void DeleteAccountOfficerDetail(int accountOfficerDetailId)
        {
            Channel.DeleteAccountOfficerDetail(accountOfficerDetailId);
        }

        public AccountOfficerDetail GetAccountOfficerDetail(int accountOfficerDetailId)
        {
            return Channel.GetAccountOfficerDetail(accountOfficerDetailId);
        }

        public AccountOfficerDetail[] GetAllAccountOfficerDetails()
        {
            return Channel.GetAllAccountOfficerDetails();
        }

        #endregion

        #region BranchDefaultMIS

        public BranchDefaultMIS UpdateBranchDefaultMIS(BranchDefaultMIS branchDefaultMIS)
        {
            return Channel.UpdateBranchDefaultMIS(branchDefaultMIS);
        }

        public void DeleteBranchDefaultMIS(int branchDefaultMISId)
        {
            Channel.DeleteBranchDefaultMIS(branchDefaultMISId);
        }

        public BranchDefaultMIS GetBranchDefaultMIS(int branchDefaultMISId)
        {
            return Channel.GetBranchDefaultMIS(branchDefaultMISId);
        }

        public BranchDefaultMIS[] GetAllBranchDefaultMISs()
        {
            return Channel.GetAllBranchDefaultMISs();
        }



        #endregion

        #region ManagementTree

        public ManagementTree UpdateManagementTree(ManagementTree managementTree)
        {
            return Channel.UpdateManagementTree(managementTree);
        }

        public void DeleteManagementTree(int managementTreeId)
        {
            Channel.DeleteManagementTree(managementTreeId);
        }

        public ManagementTree GetManagementTree(int managementTreeId)
        {
            return Channel.GetManagementTree(managementTreeId);
        }

        public ManagementTreeData[] GetAllManagementTrees()
        {
            return Channel.GetAllManagementTrees();
        }


        #endregion

        #region MISReplacement

        public MISReplacement UpdateMISReplacement(MISReplacement misReplacement)
        {
            return Channel.UpdateMISReplacement(misReplacement);
        }

        public void DeleteMISReplacement(int misReplacementId)
        {
            Channel.DeleteMISReplacement(misReplacementId);
        }

        public MISReplacement GetMISReplacement(int misReplacementId)
        {
            return Channel.GetMISReplacement(misReplacementId);
        }

        public MISReplacement[] GetAllMISReplacements()
        {
            return Channel.GetAllMISReplacements();
        }



        #endregion

        #region MPRSetup

        public Setup UpdateMPRSetup(Setup mprMPRSetup)
        {
            return Channel.UpdateMPRSetup(mprMPRSetup);
        }

        public Setup GetFirstMPRSetup()
        {
            return Channel.GetFirstMPRSetup();
        }

        public MPRSetupData[] GetFirstMPRSetups()
        {
            return Channel.GetFirstMPRSetups();
        }

        #endregion

        #region TeamClassification

        public TeamClassification UpdateTeamClassification(TeamClassification teamClassification)
        {
            return Channel.UpdateTeamClassification(teamClassification);
        }

        public void DeleteTeamClassification(int teamClassificationId)
        {
            Channel.DeleteTeamClassification(teamClassificationId);
        }

        public TeamClassification GetTeamClassification(int teamClassificationId)
        {
            return Channel.GetTeamClassification(teamClassificationId);
        }

        public TeamClassification[] GetAllTeamClassifications()
        {
            return Channel.GetAllTeamClassifications();
        }

        public TeamClassification[] GetTeamClassifications(string typeCode)
        {
            return Channel.GetTeamClassifications(typeCode);
        }


        #endregion

        #region TeamClassificationType

        public TeamClassificationType UpdateTeamClassificationType(TeamClassificationType teamClassificationType)
        {
            return Channel.UpdateTeamClassificationType(teamClassificationType);
        }

        public void DeleteTeamClassificationType(int teamClassificationTypeId)
        {
            Channel.DeleteTeamClassificationType(teamClassificationTypeId);
        }

        public TeamClassificationType GetTeamClassificationType(int teamClassificationTypeId)
        {
            return Channel.GetTeamClassificationType(teamClassificationTypeId);
        }

        public TeamClassificationType[] GetAllTeamClassificationTypes()
        {
            return Channel.GetAllTeamClassificationTypes();
        }


        #endregion

        #region Team

        public Team UpdateTeam(Team team)
        {
            return Channel.UpdateTeam(team);
        }

        public void DeleteTeam(int teamId)
        {
            Channel.DeleteTeam(teamId);
        }

        public Team GetTeam(int teamId)
        {
            return Channel.GetTeam(teamId);
        }

        public TeamData[] GetTeams()
        {
            return Channel.GetTeams();
        }

        public Team[] GetParentTeams(string definitionCode)
        {
            return Channel.GetParentTeams(definitionCode);
        }

        public Team[] GetTeamByLevel(int level)
        {
            return Channel.GetTeamByLevel(level);
        }

        public IEnumerable<Team> GetTeamByDefinition(string definitionCode)
        {
            return Channel.GetTeamByDefinition(definitionCode);
        }

        public TeamData[] GetTeamsBySearch(string SearchValue)
        {
            return Channel.GetTeamsBySearch(SearchValue);
        }

        #endregion

        #region TeamClassificationMap

        public TeamClassificationMap UpdateTeamClassificationMap(TeamClassificationMap teamClassificationMap)
        {
            return Channel.UpdateTeamClassificationMap(teamClassificationMap);
        }

        public void DeleteTeamClassificationMap(int teamClassificationMapId)
        {
            Channel.DeleteTeamClassificationMap(teamClassificationMapId);
        }

        public TeamClassificationMap GetTeamClassificationMap(int teamClassificationMapId)
        {
            return Channel.GetTeamClassificationMap(teamClassificationMapId);
        }

        public TeamClassificationMap[] GetAllTeamClassificationMaps(string misCode, string definitionCode)
        {
            return Channel.GetAllTeamClassificationMaps(misCode, definitionCode);
        }

        #endregion

        #region TransferPrice

        public TransferPrice UpdateTransferPrice(TransferPrice transferPrice)
        {
            return Channel.UpdateTransferPrice(transferPrice);
        }

        public void DeleteTransferPrice(int transferPriceId)
        {
            Channel.DeleteTransferPrice(transferPriceId);
        }

        public TransferPrice GetTransferPrice(int transferPriceId)
        {
            return Channel.GetTransferPrice(transferPriceId);
        }

        public TransferPriceData[] GetAllTransferPrices()
        {
            return Channel.GetAllTransferPrices();
        }


        #endregion

        #region AccountTransferPrice

        public AccountTransferPrice UpdateAccountTransferPrice(AccountTransferPrice accountTransferPrice)
        {
            return Channel.UpdateAccountTransferPrice(accountTransferPrice);
        }

        public void DeleteAccountTransferPrice(int accountTransferPriceId)
        {
            Channel.DeleteAccountTransferPrice(accountTransferPriceId);
        }

        public AccountTransferPrice GetAccountTransferPrice(int accountTransferPriceId)
        {
            return Channel.GetAccountTransferPrice(accountTransferPriceId);
        }

        public AccountTransferPriceData[] GetAllAccountTransferPrices()
        {
            return Channel.GetAllAccountTransferPrices();
        }


        #endregion

        #region GeneralTransferPrice

        public GeneralTransferPrice UpdateGeneralTransferPrice(GeneralTransferPrice generalTransferPrice)
        {
            return Channel.UpdateGeneralTransferPrice(generalTransferPrice);
        }

        public void DeleteGeneralTransferPrice(int generalTransferPriceId)
        {
            Channel.DeleteGeneralTransferPrice(generalTransferPriceId);
        }

        public GeneralTransferPrice GetGeneralTransferPrice(int generalTransferPriceId)
        {
            return Channel.GetGeneralTransferPrice(generalTransferPriceId);
        }

        public GeneralTransferPriceData[] GetAllGeneralTransferPrices()
        {
            return Channel.GetAllGeneralTransferPrices();
        }

        public void DeleteGTPSelectedIds(string selectedIds)
        {
            Channel.DeleteGTPSelectedIds(selectedIds);
        }
        #endregion

        #region CustAccount

        public CustAccount[] GetAllCustAccounts()
        {
            return Channel.GetAllCustAccounts();
        }

        public CustAccount[] GetCustAccounts(string searchType, string searchValue, int number)
        {
            return Channel.GetCustAccounts(searchType, searchValue, number);
        }


        #endregion

        #region MemoAccountMap

        public MemoAccountMap UpdateMemoAccountMap(MemoAccountMap memoAccountMap)
        {
            return Channel.UpdateMemoAccountMap(memoAccountMap);
        }

        public void DeleteMemoAccountMap(int memoAccountMapId)
        {
            Channel.DeleteMemoAccountMap(memoAccountMapId);
        }

        public MemoAccountMap GetMemoAccountMap(int memoAccountMapId)
        {
            return Channel.GetMemoAccountMap(memoAccountMapId);
        }

        public MemoAccountMapData[] GetAllMemoAccountMaps()
        {
            return Channel.GetAllMemoAccountMaps();
        }


        #endregion

        #region MemoGLMap

        public MemoGLMap UpdateMemoGLMap(MemoGLMap memoGLMap)
        {
            return Channel.UpdateMemoGLMap(memoGLMap);
        }

        public void DeleteMemoGLMap(int memoGLMapId)
        {
            Channel.DeleteMemoGLMap(memoGLMapId);
        }

        public MemoGLMap GetMemoGLMap(int memoGLMapId)
        {
            return Channel.GetMemoGLMap(memoGLMapId);
        }

        public MemoGLMapData[] GetAllMemoGLMaps()
        {
            return Channel.GetAllMemoGLMaps();
        }


        #endregion

        #region MemoProductMap

        public MemoProductMap UpdateMemoProductMap(MemoProductMap memoProductMap)
        {
            return Channel.UpdateMemoProductMap(memoProductMap);
        }

        public void DeleteMemoProductMap(int memoProductMapId)
        {
            Channel.DeleteMemoProductMap(memoProductMapId);
        }

        public MemoProductMap GetMemoProductMap(int memoProductMapId)
        {
            return Channel.GetMemoProductMap(memoProductMapId);
        }

        public MemoProductMapData[] GetAllMemoProductMaps()
        {
            return Channel.GetAllMemoProductMaps();
        }


        #endregion

        #region MemoUnits

        public MemoUnits UpdateMemoUnits(MemoUnits memoUnits)
        {
            return Channel.UpdateMemoUnits(memoUnits);
        }

        public void DeleteMemoUnits(int memoUnitsId)
        {
            Channel.DeleteMemoUnits(memoUnitsId);
        }

        public MemoUnits GetMemoUnits(int memoUnitsId)
        {
            return Channel.GetMemoUnits(memoUnitsId);
        }

        public MemoUnits[] GetAllMemoUnits()
        {
            return Channel.GetAllMemoUnits();
        }


        #endregion

        #region BSExemption

        public BSExemption UpdateBSExemption(BSExemption bsExemption)
        {
            return Channel.UpdateBSExemption(bsExemption);
        }

        public void DeleteBSExemption(int bsExemptionId)
        {
            Channel.DeleteBSExemption(bsExemptionId);
        }

        public BSExemption GetBSExemption(int bsExemptionId)
        {
            return Channel.GetBSExemption(bsExemptionId);
        }

        public BSExemption[] GetAllBSExemptions()
        {
            return Channel.GetAllBSExemptions();
        }



        #endregion

        #region CaptionMapping

        public CaptionMapping UpdateCaptionMapping(CaptionMapping captionMapping)
        {
            return Channel.UpdateCaptionMapping(captionMapping);
        }

        public void DeleteCaptionMapping(int captionMappingId)
        {
            Channel.DeleteCaptionMapping(captionMappingId);
        }

        public CaptionMapping GetCaptionMapping(int captionMappingId)
        {
            return Channel.GetCaptionMapping(captionMappingId);
        }

        public CaptionMapping[] GetAllCaptionMappings()
        {
            return Channel.GetAllCaptionMappings();
        }

        #endregion

        #region RatioCaptionMapping

        public RatioCaptionMapping UpdateRatioCaptionMapping(RatioCaptionMapping ratioCaptionMapping)
        {
            return Channel.UpdateRatioCaptionMapping(ratioCaptionMapping);
        }

        public void DeleteRatioCaptionMapping(int ratioCaptionMappingId)
        {
            Channel.DeleteRatioCaptionMapping(ratioCaptionMappingId);
        }

        public RatioCaptionMapping GetRatioCaptionMapping(int ratioCaptionMappingId)
        {
            return Channel.GetRatioCaptionMapping(ratioCaptionMappingId);
        }

        public RatioCaptionMapping[] GetAllRatioCaptionMappings()
        {
            return Channel.GetAllRatioCaptionMappings();
        }

        #endregion

        #region Ratios

        public Ratios UpdateRatios(Ratios ratios)
        {
            return Channel.UpdateRatios(ratios);
        }

        public void DeleteRatios(int ratiosId)
        {
            Channel.DeleteRatios(ratiosId);
        }

        public Ratios GetRatios(int ratiosId)
        {
            return Channel.GetRatios(ratiosId);
        }

        public Ratios[] GetAllRatios()
        {
            return Channel.GetAllRatios();
        }

        #endregion

        #region AbcRatio

        public AbcRatio UpdateAbcRatio(AbcRatio abcRatio)
        {
            return Channel.UpdateAbcRatio(abcRatio);
        }

        public void DeleteAbcRatio(int abcRatioId)
        {
            Channel.DeleteAbcRatio(abcRatioId);
        }

        public AbcRatio GetAbcRatio(int abcRatioId)
        {
            return Channel.GetAbcRatio(abcRatioId);
        }

        public AbcRatio[] GetAllAbcRatio()
        {
            return Channel.GetAllAbcRatio();
        }

        #endregion

        #region Sbu

        public Sbu UpdateSbu(Sbu sbu)
        {
            return Channel.UpdateSbu(sbu);
        }

        public void DeleteSbu(int sbuId)
        {
            Channel.DeleteSbu(sbuId);
        }

        public Sbu GetSbu(int sbuId)
        {
            return Channel.GetSbu(sbuId);
        }

        public Sbu[] GetAllSbu()
        {
            return Channel.GetAllSbu();
        }

        #endregion

        #region SbuType

        public SbuType UpdateSbuType(SbuType sbuType)
        {
            return Channel.UpdateSbuType(sbuType);
        }

        public void DeleteSbuType(int sbuTypeId)
        {
            Channel.DeleteSbuType(sbuTypeId);
        }

        public SbuType GetSbuType(int sbuTypeId)
        {
            return Channel.GetSbuType(sbuTypeId);
        }

        public SbuType[] GetAllSbuType()
        {
            return Channel.GetAllSbuType();
        }

        #endregion

        #region Servicese

        public Servicese UpdateServices(Servicese services)
        {
            return Channel.UpdateServices(services);
        }

        public void DeleteServices(int servicesId)
        {
            Channel.DeleteServices(servicesId);
        }

        public Servicese GetServices(int servicesId)
        {
            return Channel.GetServices(servicesId);
        }

        public Servicese[] GetAllServices()
        {
            return Channel.GetAllServices();
        }

        #endregion

        #region Staffs

        public Staffs UpdateStaffs(Staffs staffs)
        {
            return Channel.UpdateStaffs(staffs);
        }

        public void DeleteStaffs(int staffId)
        {
            Channel.DeleteStaffs(staffId);
        }

        public Staffs GetStaffs(int staffId)
        {
            return Channel.GetStaffs(staffId);
        }

        public Staffs[] GetAllStaffs()
        {
            return Channel.GetAllStaffs();
        }


        #endregion

        #region MessagingSubscription

        public MessagingSubscription UpdateMessagingSubscription(MessagingSubscription messagingSubscription)
        {
            return Channel.UpdateMessagingSubscription(messagingSubscription);
        }

        public void DeleteMessagingSubscription(int messagingSubscriptionId)
        {
            Channel.DeleteMessagingSubscription(messagingSubscriptionId);
        }

        public MessagingSubscription GetMessagingSubscription(int messagingSubscriptionId)
        {
            return Channel.GetMessagingSubscription(messagingSubscriptionId);
        }


        public Revenue[] GetMessagingSubscriptionByRecipients(string recipients)
        {
            return Channel.GetMessagingSubscriptionByRecipients(recipients);
        }


        public DateTime[] GetRecipents()
        {
            return Channel.GetRecipents();
        }


        public string[] GetReports()
        {
            return Channel.GetReports();
        }

        #endregion

        public crb_Data[] GetAllCrbData()
        {
            return Channel.GetAllCrbData();
        }

        #region Account Interest
        public account_interest[] GellALlAccountInterest()
        {
            return Channel.GellALlAccountInterest();
        }

        public product_interestData[] GetAllProductInterest()
        {
            return Channel.GetAllProductInterest();
        }

        #endregion

        #region Team Structure
        public TeamStructure UpdateTeamStructure(TeamStructure teamstructure)
        {
            return Channel.UpdateTeamStructure(teamstructure);
        }

        public void DeleteTeamStructure(int Team_StructureId)
        {
            Channel.DeleteTeamStructure(Team_StructureId);
        }

        public TeamStructure GetTeamStructure(int Team_StructureId)
        {
            return Channel.GetTeamStructure(Team_StructureId);
        }

        public TeamStructure[] GetAllTeamStructure()
        {
            return Channel.GetAllTeamStructure();
        }

        public TeamStructure[] GetTeamStructureUsingParams(string SearchValue, string year)
        {
            return Channel.GetTeamStructureUsingParams(SearchValue, year);
        }

        public TeamStructure[] TeamstructureByParameters(string selectedDefinitionCode, string SearchValue, string year)
        {
            return Channel.TeamstructureByParameters(selectedDefinitionCode, SearchValue, year);
        }

        public TeamStructure[] GetTeamstructureByParamsAndeSetUp(string code, string SearchValue)
        {
            return Channel.GetTeamstructureByParamsAndeSetUp(code, SearchValue);
        }
        public TeamStructure[] GetTeamStructureUsingSetUp()
        {
            return Channel.GetTeamStructureUsingSetUp();
        }

        public TeamStructure[] GetTeamStructureUsingDefinitionCode(string code)
        {
            return Channel.GetTeamStructureUsingDefinitionCode(code);
        }

        public TeamStructure[] GetTeamStructureUsingDefinitionCodeMonthly(string code)
        {
            return Channel.GetTeamStructureUsingDefinitionCodeMonthly(code);
        }

        public TeamStructure[] GetTeamstructureByParamsAndeSetUpMonthly(string code, string SearchValue)
        {
            return Channel.GetTeamstructureByParamsAndeSetUpMonthly(code, SearchValue);
        }

        public TeamStructure[] GetTeamStructureUsingSetUpMonthly()
        {
            return Channel.GetTeamStructureUsingSetUpMonthly();
        }

        //public TeamStructure GetTeamStructureTop1(string branch, string year)
        //{
        //    return Channel.GetTeamStructureTop1(branch, year);
        //}

        public TeamStructure GetTeamStructureTop1(string branch, string defcode, string year)
        {
            return Channel.GetTeamStructureTop1(branch, defcode, year);
        }


        #endregion


        public PublicSectorData[] GetAllPublicSectorData()
        {
            return Channel.GetAllPublicSectorData();
        }

        #region Corporate Adjustment

        public CorporateAdjustment UpdateCorporateAdjustment(CorporateAdjustment corporateadjustment)
        {
            return Channel.UpdateCorporateAdjustment(corporateadjustment);
        }

        public void DeleteCorporateAdjustment(int CorporateAdjustmentId)
        {
            Channel.DeleteCorporateAdjustment(CorporateAdjustmentId);
        }

        public CorporateAdjustment GetCorporateAdjustment(int CorporateAdjustmentId)
        {
            return Channel.GetCorporateAdjustment(CorporateAdjustmentId);
        }

        public CorporateAdjustment[] GetAllCorporateAdjustment()
        {
            return Channel.GetAllCorporateAdjustment();
        }

        #endregion

        #region Caption Transfer Price

        public caption_transfer_price UpdateCaptionTransferPrice(caption_transfer_price ctp)
        {
            return Channel.UpdateCaptionTransferPrice(ctp);
        }

        public void DeleteCaptionTransferPrice(int caption_transfer_price_Id)
        {
            Channel.DeleteCaptionTransferPrice(caption_transfer_price_Id);
        }

        public caption_transfer_price GetCaptionTransferPrice(int caption_transfer_price_Id)
        {
            return Channel.GetCaptionTransferPrice(caption_transfer_price_Id);
        }

        public caption_transfer_price[] GetAllCaptionTransferPrice()
        {
            return Channel.GetAllCaptionTransferPrice();
        }

        #endregion

        #region Asset Type

        public AssetType UpdateAssetType(AssetType assettype)
        {
            return Channel.UpdateAssetType(assettype);
        }

        public void DeleteAssetType(int AssetType_Id)
        {
            Channel.DeleteAssetType(AssetType_Id);
        }

        public AssetType GetAssetType(int AssetType_Id)
        {
            return Channel.GetAssetType(AssetType_Id);
        }

        public AssetType[] GetAllAssetType()
        {
            return Channel.GetAllAssetType();
        }

        #endregion

        #region Customer MIS

        public Customermis UpdateCustomermis(Customermis customermis)
        {
            return Channel.UpdateCustomermis(customermis);
        }

        public void DeleteCustomermis(int CustomermisId)
        {
            Channel.DeleteCustomermis(CustomermisId);
        }

        public Customermis GetCustomermis(int CustomermisId)
        {
            return Channel.GetCustomermis(CustomermisId);
        }

        public Customermis[] GetAllCustomermis()
        {
            return Channel.GetAllCustomermis();
        }

        #endregion

        #region PPR

        public PPR UpdatePPR(PPR ppr)
        {
            return Channel.UpdatePPR(ppr);
        }

        public void DeletePPR(int PPRId)
        {
            Channel.DeletePPR(PPRId);
        }

        public PPR GetPPR(int PPRId)
        {
            return Channel.GetPPR(PPRId);
        }

        public PPR[] GetAllPPR()
        {
            return Channel.GetAllPPR();
        }

        #endregion

        #region Risk Adjusted Charge

        public RiskAdjustedCharge UpdateRiskAdjustedCharge(RiskAdjustedCharge rac)
        {
            return Channel.UpdateRiskAdjustedCharge(rac);
        }

        public void DeleteRiskAdjustedCharge(int RiskAdjustedChargeId)
        {
            Channel.DeleteRiskAdjustedCharge(RiskAdjustedChargeId);
        }

        public RiskAdjustedCharge GetRiskAdjustedCharge(int RiskAdjustedChargeId)
        {
            return Channel.GetRiskAdjustedCharge(RiskAdjustedChargeId);
        }

        public RiskAdjustedCharge[] GetAllRiskAdjustedCharge()
        {
            return Channel.GetAllRiskAdjustedCharge();
        }

        #endregion

        #region MPR ScoreCard Metrics

        public ScoreCardMetrics UpdateScoreCardMetric(ScoreCardMetrics scorecardmetric)
        {
            return Channel.UpdateScoreCardMetric(scorecardmetric);
        }

        public void DeleteScoreCardMetric(int MetricId)
        {
            Channel.DeleteScoreCardMetric(MetricId);
        }

        public ScoreCardMetrics GetScoreCardMetric(int MetricId)
        {
            return Channel.GetScoreCardMetric(MetricId);
        }

        public ScoreCardMetrics[] GetAllScoreCardMetrics()
        {
            return Channel.GetAllScoreCardMetrics();
        }

        public ScoreCardMetricsData[] GetScoreCardMetricsUsingSearchValue(string searchvalue)
        {
            return Channel.GetScoreCardMetricsUsingSearchValue(searchvalue);
        }

        public ScoreCardMetricsData[] GetScoreCardMetricsUsingSetUp()
        {
            return Channel.GetScoreCardMetricsUsingSetUp();
        }

        #endregion

        #region MPR ScoreCard Weight

        public ScoreCardWeight UpdateScoreCardWeight(ScoreCardWeight scorecardweight)
        {
            return Channel.UpdateScoreCardWeight(scorecardweight);
        }

        public void DeleteScoreCardWeight(int WeightId)
        {
            Channel.DeleteScoreCardWeight(WeightId);
        }

        public ScoreCardWeight GetScoreCardWeight(int WeightId)
        {
            return Channel.GetScoreCardWeight(WeightId);
        }

        public ScoreCardWeight[] GetAllScoreCardWeight()
        {
            return Channel.GetAllScoreCardWeight();
        }

        public ScoreCardWeightData[] GetScoreCardWeightWITHMetrics()
        {
            return Channel.GetScoreCardWeightWITHMetrics();
        }

        //public ScoreCardMetrics[] GetScoreCardMetricsUsingSearchValue(string searchvalue)
        //{
        //    return Channel.GetScoreCardMetricsUsingSearchValue(searchvalue);
        //}

        //public ScoreCardMetrics[] GetScoreCardMetricsUsingSetUp()
        //{
        //    return Channel.GetScoreCardMetricsUsingSetUp();
        //}

        #endregion

        #region MPR ScoreCard Perspective

        public ScoreCardPerspective UpdateScorecardPerspective(ScoreCardPerspective scPerspective)
        {
            return Channel.UpdateScorecardPerspective(scPerspective);
        }

        public void DeleteScorecardPerspective(int PerspectiveId)
        {
            Channel.DeleteScorecardPerspective(PerspectiveId);
        }

        public ScoreCardPerspective GetScorecardPerspective(int PerspectiveId)
        {
            return Channel.GetScorecardPerspective(PerspectiveId);
        }

        public ScoreCardPerspective[] GetAllScorecardPerspective()
        {
            return Channel.GetAllScorecardPerspective();
        }

        #endregion

        #region MPR ScoreCard Mapping

        public ScoreCardMapping UpdateScoreCardMapping(ScoreCardMapping scorecardmapping)
        {
            return Channel.UpdateScoreCardMapping(scorecardmapping);
        }

        public void DeleteScoreCardMapping(int MappingId)
        {
            Channel.DeleteScoreCardMapping(MappingId);
        }

        public ScoreCardMapping GetScoreCardMapping(int MappingId)
        {
            return Channel.GetScoreCardMapping(MappingId);
        }

        public ScoreCardMapping[] GetAllScoreCardMapping()
        {
            return Channel.GetAllScoreCardMapping();
        }

        public ScoreCardMappingData[] GetScoreCardMappingUsingSearchValue(string searchvalue)
        {
            return Channel.GetScoreCardMappingUsingSearchValue(searchvalue);
        }

        public ScoreCardMappingData[] GetScoreCardMappingUsingSetUp()
        {
            return Channel.GetScoreCardMappingUsingSetUp();
        }

        #endregion

        #region MPR ScoreCard

        public ScoreCard UpdateScoreCard(ScoreCard scorecard)
        {
            return Channel.UpdateScoreCard(scorecard);
        }

        public void DeleteScoreCard(int mpr_scorecard_stgId)
        {
            Channel.DeleteScoreCard(mpr_scorecard_stgId);
        }

        public ScoreCard GetScoreCard(int mpr_scorecard_stgId)
        {
            return Channel.GetScoreCard(mpr_scorecard_stgId);
        }

        public ScoreCard[] GetAllScoreCard()
        {
            return Channel.GetAllScoreCard();
        }

        public ScoreCard[] ScoreCardCaptions()
        {
            return Channel.ScoreCardCaptions();
        }

        #endregion

        #region MIS Transfer Price

        public MISTransferPrice UpdateMISTransferPrice(MISTransferPrice mistransferprice)
        {
            return Channel.UpdateMISTransferPrice(mistransferprice);
        }

        public void DeleteMISTransferPrice(int mistransferpriceId)
        {
            Channel.DeleteMISTransferPrice(mistransferpriceId);
        }

        public MISTransferPrice GetMISTransferPrice(int mistransferpriceId)
        {
            return Channel.GetMISTransferPrice(mistransferpriceId);
        }

        public MISTransferPrice[] GetAllMISTransferPrice()
        {
            return Channel.GetAllMISTransferPrice();
        }

        public MISTransferPriceData[] GetMISTransferPriceUsingSetUp()
        {
            return Channel.GetMISTransferPriceUsingSetUp();
        }
        public MISTransferPriceData[] GetMISTransferPricebyParams(string defCode, string miscode, string category, string currency, int year, int period)
        {
            return Channel.GetMISTransferPricebyParams(defCode, miscode, category, currency, year, period);
        }



        #endregion

        #region Product Transfer Price

        public ProductTransferPrice Updateproducttransferprice(ProductTransferPrice producttransferprice)
        {
            return Channel.Updateproducttransferprice(producttransferprice);
        }

        public void Deleteproducttransferprice(int ID)
        {
            Channel.Deleteproducttransferprice(ID);
        }

        public ProductTransferPrice Getproducttransferprice(int ID)
        {
            return Channel.Getproducttransferprice(ID);
        }

        public ProductTransferPrice[] GetAllProductTransferPrice()
        {
            return Channel.GetAllProductTransferPrice();
        }

        public ProductTransferPriceData[] GetProductTransferPriceUsingSearchValue(string searchvalue)
        {
            return Channel.GetProductTransferPriceUsingSearchValue(searchvalue);
        }

        #endregion

        #region Team Bank
        public TeamBank UpdateTeamBank(TeamBank teambank)
        {
            return Channel.UpdateTeamBank(teambank);
        }

        public void DeleteTeamBank(int teambankId)
        {
            Channel.DeleteTeamBank(teambankId);
        }

        public TeamBank GetTeamBank(int teambankId)
        {
            return Channel.GetTeamBank(teambankId);
        }

        public TeamBank[] GetAllTeamBanks()
        {
            return Channel.GetAllTeamBanks();
        }

        public TeamBank[] GetTeamBanksUsingParams(string searchvalue, int year)
        {
            return Channel.GetTeamBanksUsingParams(searchvalue, year);
        }

        public TeamBank[] GetTeamBankUsingDefinitionCode(string code)
        {
            return Channel.GetTeamBankUsingDefinitionCode(code);
        }

        #endregion

        #region ScoreCardMetricsKBL

        public ScoreCardMetricsKBL UpdateScoreCardMetricsKBL(ScoreCardMetricsKBL scorecardmetricsKBL)
        {
            return Channel.UpdateScoreCardMetricsKBL(scorecardmetricsKBL);
        }

        public void DeleteScoreCardMetricsKBL(int MetricID)
        {
            Channel.DeleteScoreCardMetricsKBL(MetricID);
        }

        public ScoreCardMetricsKBL GetScoreCardMetricsKBL(int MetricID)
        {
            return Channel.GetScoreCardMetricsKBL(MetricID);
        }

        public ScoreCardMetricsKBL[] GetAllScoreCardMetricsKBL()
        {
            return Channel.GetAllScoreCardMetricsKBL();
        }

        public ScoreCardMetricsKBL[] GetScoreCardMetricsKBLUsingSearchValue(string searchvalue)
        {
            return Channel.GetScoreCardMetricsKBLUsingSearchValue(searchvalue);
        }

        public ScoreCardMetricsKBL[] GetScoreCardMetricsKBLUsingYear(int year)
        {
            return Channel.GetScoreCardMetricsKBLUsingYear(year);
        }

        #endregion

        #region ScoreCard KPI Types KBL

        public ScoreCardKPITypesKBL UpdateScoreCardKPITypesKBL(ScoreCardKPITypesKBL scorecardKPItypesKBL)
        {
            return Channel.UpdateScoreCardKPITypesKBL(scorecardKPItypesKBL);
        }

        public void DeleteScoreCardKPITypesKBL(int ID)
        {
            Channel.DeleteScoreCardKPITypesKBL(ID);
        }

        public ScoreCardKPITypesKBL GetScoreCardKPITypesKBL(int ID)
        {
            return Channel.GetScoreCardKPITypesKBL(ID);
        }

        public ScoreCardKPITypesKBL[] GetAllScoreCardKPITypesKBL()
        {
            return Channel.GetAllScoreCardKPITypesKBL();
        }

        public ScoreCardKPITypesKBL[] GetScoreCardKPITypesKBLUsingSearchValue(string searchvalue)
        {
            return Channel.GetScoreCardKPITypesKBLUsingSearchValue(searchvalue);
        }

        public ScoreCardKPITypesKBL[] GetScoreCardKPITypesKBLByPeriodYearKPIType(int period, int year, string searchvalue)
        {
            return Channel.GetScoreCardKPITypesKBLByPeriodYearKPIType(period, year, searchvalue);
        }

        #endregion

        #region ScoreCard Set Metric Target KBL

        public ScoreCardSetMetricTargetKBL UpdateScoreCardSetMetricTargetKBL(ScoreCardSetMetricTargetKBL scoreCcardsetmetrictarget)
        {
            return Channel.UpdateScoreCardSetMetricTargetKBL(scoreCcardsetmetrictarget);
        }

        public void DeleteScoreCardSetMetricTargetKBL(int ID)
        {
            Channel.DeleteScoreCardSetMetricTargetKBL(ID);
        }

        public ScoreCardSetMetricTargetKBL GetScoreCardSetMetricTargetKBL(int ID)
        {
            return Channel.GetScoreCardSetMetricTargetKBL(ID);
        }

        public ScoreCardSetMetricTargetKBL[] GetAllScoreCardSetMetricTargetKBL()
        {
            return Channel.GetAllScoreCardSetMetricTargetKBL();
        }

        public ScoreCardSetMetricTargetKBL[] GetScoreCardSetMetricTargetKBLUsingSearchValue(string searchvalue)
        {
            return Channel.GetScoreCardSetMetricTargetKBLUsingSearchValue(searchvalue);
        }

        public ScoreCardSetMetricTargetKBL[] GetScoreCardSetMetricTargetKBLByPeriodANDYear(int period, int year)
        {
            return Channel.GetScoreCardSetMetricTargetKBLByPeriodANDYear(period, year);
        }

        #endregion

        #region ScoreCard MIS Mapping KBL

        public ScoreCardMISMappingKBL UpdateScoreCardMISMappingKBL(ScoreCardMISMappingKBL scorecardMISmapping)
        {
            return Channel.UpdateScoreCardMISMappingKBL(scorecardMISmapping);
        }

        public void DeleteScoreCardMISMappingKBL(int ID)
        {
            Channel.DeleteScoreCardMISMappingKBL(ID);
        }

        public ScoreCardMISMappingKBL GetScoreCardMISMappingKBL(int ID)
        {
            return Channel.GetScoreCardMISMappingKBL(ID);
        }

        public ScoreCardMISMappingKBL[] GetAllScoreCardMISMappingKBL()
        {
            return Channel.GetAllScoreCardMISMappingKBL();
        }

        public ScoreCardMISMappingKBL[] GetScoreCardMISMappingKBLUsingSearchValue(string searchvalue)
        {
            return Channel.GetScoreCardMISMappingKBLUsingSearchValue(searchvalue);
        }

        #endregion

        #region Income Cash Vault Schedule

        public IncomeCashVaultSchedule UpdateIncomeCashVaultSchedule(IncomeCashVaultSchedule incomecashvaultschedule)
        {
            return Channel.UpdateIncomeCashVaultSchedule(incomecashvaultschedule);
        }

        public void DeleteIncomeCashVaultSchedule(int ID)
        {
            Channel.DeleteIncomeCashVaultSchedule(ID);
        }

        public IncomeCashVaultSchedule GetIncomeCashVaultSchedule(int ID)
        {
            return Channel.GetIncomeCashVaultSchedule(ID);
        }

        public IncomeCashVaultSchedule[] GetAllIncomeCashVaultScheduleL()
        {
            return Channel.GetAllIncomeCashVaultScheduleL();
        }


        #endregion

        #region Slary Schedule

        public SlarySchedule UpdateSlarySchedule(SlarySchedule slaryschedule)
        {
            return Channel.UpdateSlarySchedule(slaryschedule);
        }

        public void DeleteSlarySchedule(int ID)
        {
            Channel.DeleteSlarySchedule(ID);
        }

        public SlarySchedule GetSlarySchedule(int ID)
        {
            return Channel.GetSlarySchedule(ID);
        }

        public SlarySchedule[] GetAllSlarySchedule()
        {
            return Channel.GetAllSlarySchedule();
        }


        #endregion

        #region Income Other Breakdown

        public IncomeOtherBreakdown UpdateIncomeOtherBreakdown(IncomeOtherBreakdown iob)
        {
            return Channel.UpdateIncomeOtherBreakdown(iob);
        }

        public void DeleteIncomeOtherBreakdown(int ID)
        {
            Channel.DeleteIncomeOtherBreakdown(ID);
        }

        public IncomeOtherBreakdown GetIncomeOtherBreakdown(int ID)
        {
            return Channel.GetIncomeOtherBreakdown(ID);
        }

        public IncomeOtherBreakdown[] GetAllIncomeOtherBreakdown()
        {
            return Channel.GetAllIncomeOtherBreakdown();
        }


        #endregion

        #region Download Base Fintrak FinalManual

        public DownloadBaseFintrakFinalManual UpdateDDBaseFFM(DownloadBaseFintrakFinalManual ddb)
        {
            return Channel.UpdateDDBaseFFM(ddb);
        }

        public void DeleteDDBaseFFM(int ID)
        {
            Channel.DeleteDDBaseFFM(ID);
        }

        public DownloadBaseFintrakFinalManual GetDDBaseFFM(int ID)
        {
            return Channel.GetDDBaseFFM(ID);
        }

        public DownloadBaseFintrakFinalManual[] GetAllDDBaseFFM()
        {
            return Channel.GetAllDDBaseFFM();
        }


        #endregion

        #region MPRReportStatus

        public MPRReportStatus UpdateMPRReportStatus(MPRReportStatus mprreportstatus)
        {
            return Channel.UpdateMPRReportStatus(mprreportstatus);
        }

        public void DeleteMPRReportStatus(int MPRReportStatusId)
        {
            Channel.DeleteMPRReportStatus(MPRReportStatusId);
        }

        public MPRReportStatus GetMPRReportStatus(int MPRReportStatusId)
        {
            return Channel.GetMPRReportStatus(MPRReportStatusId);
        }

        public MPRReportStatus[] GetAllMPRReportStatus()
        {
            return Channel.GetAllMPRReportStatus();
        }


        #endregion

        #region FinstatMapping

        public FinstatMapping UpdateFinstatMapping(FinstatMapping finstatMapping)
        {
            return Channel.UpdateFinstatMapping(finstatMapping);
        }

        public void DeleteFinstatMapping(int finstatMappingId)
        {
            Channel.DeleteFinstatMapping(finstatMappingId);
        }

        public FinstatMapping GetFinstatMapping(int finstatMappingId)
        {
            return Channel.GetFinstatMapping(finstatMappingId);
        }

        public FinstatMapping[] GetAllFinstatMapping()
        {
            return Channel.GetAllFinstatMapping();
        }

        #endregion

        #region IncomeSetup

        public IncomeSetup UpdateIncomeSetup(IncomeSetup incomesetup)
        {
            return Channel.UpdateIncomeSetup(incomesetup);
        }

        public void DeleteIncomeSetup(int ID)
        {
            Channel.DeleteIncomeSetup(ID);
        }

        public IncomeSetup GetIncomeSetup(int ID)
        {
            return Channel.GetIncomeSetup(ID);
        }

        public IncomeSetup[] GetAllIncomeSetup()
        {
            return Channel.GetAllIncomeSetup();
        }

        public IncomeSetup GetLatestIncomeSetup()
        {
            return Channel.GetLatestIncomeSetup();
        }

        #endregion

        #region IncomeAccountsTreeMisCodes

        public IncomeAccountsTreeMisCodes UpdateIncomeAccountsTreeMisCodes(IncomeAccountsTreeMisCodes incomeAccountsTreeMisCodes)
        {
            return Channel.UpdateIncomeAccountsTreeMisCodes(incomeAccountsTreeMisCodes);
        }

        public void DeleteIncomeAccountsTreeMisCodes(int ID)
        {
            Channel.DeleteIncomeAccountsTreeMisCodes(ID);
        }

        public IncomeAccountsTreeMisCodes GetIncomeAccountsTreeMisCodes(int ID)
        {
            return Channel.GetIncomeAccountsTreeMisCodes(ID);
        }

        public IncomeAccountsTreeMisCodes[] GetAllIncomeAccountsTreeMisCodes()
        {
            return Channel.GetAllIncomeAccountsTreeMisCodes();
        }

        public IncomeAccountsTreeMisCodes[] GetByAccountNumber(string AccountNumber)
        {
            return Channel.GetByAccountNumber(AccountNumber);
        }

        #endregion

        #region IncomeAccountsTreeAccount

        public IncomeAccountsTreeAccount UpdateIncomeAccountsTreeAccount(IncomeAccountsTreeAccount incomeAccountsTreeAccount)
        {
            return Channel.UpdateIncomeAccountsTreeAccount(incomeAccountsTreeAccount);
        }

        public void DeleteIncomeAccountsTreeAccount(int ID)
        {
            Channel.DeleteIncomeAccountsTreeAccount(ID);
        }

        public IncomeAccountsTreeAccount GetIncomeAccountsTreeAccount(int ID)
        {
            return Channel.GetIncomeAccountsTreeAccount(ID);
        }

        public IncomeAccountsTreeAccount[] GetAllIncomeAccountsTreeAccount()
        {
            return Channel.GetAllIncomeAccountsTreeAccount();
        }

        public IncomeAccountsTreeAccount[] FilterByAccountNumber(string AccountNumber)
        {
            return Channel.FilterByAccountNumber(AccountNumber);
        }

        #endregion

        #region IncomePoolRateSbu

        public IncomePoolRateSbu UpdateIncomePoolRateSbu(IncomePoolRateSbu incomePoolRateSbu)
        {
            return Channel.UpdateIncomePoolRateSbu(incomePoolRateSbu);
        }

        public void DeleteIncomePoolRateSbu(int ID)
        {
            Channel.DeleteIncomePoolRateSbu(ID);
        }

        public IncomePoolRateSbu GetIncomePoolRateSbu(int ID)
        {
            return Channel.GetIncomePoolRateSbu(ID);
        }

        public IncomePoolRateSbu[] GetAllIncomePoolRateSbu()
        {
            return Channel.GetAllIncomePoolRateSbu();
        }

        #endregion

        #region IncomePoolRateSbuYear

        public IncomePoolRateSbuYear UpdateIncomePoolRateSbuYear(IncomePoolRateSbuYear incomePoolRateSbuYear)
        {
            return Channel.UpdateIncomePoolRateSbuYear(incomePoolRateSbuYear);
        }

        public void DeleteIncomePoolRateSbuYear(int ID)
        {
            Channel.DeleteIncomePoolRateSbuYear(ID);
        }

        public IncomePoolRateSbuYear GetIncomePoolRateSbuYear(int ID)
        {
            return Channel.GetIncomePoolRateSbuYear(ID);
        }

        public IncomePoolRateSbuYear[] GetAllIncomePoolRateSbuYear()
        {
            return Channel.GetAllIncomePoolRateSbuYear();
        }

        #endregion

        #region IncomeAccountsFintrak

        public IncomeAccountsFintrak UpdateIncomeAccountsFintrak(IncomeAccountsFintrak incomeAccountsFintrak)
        {
            return Channel.UpdateIncomeAccountsFintrak(incomeAccountsFintrak);
        }

        public void DeleteIncomeAccountsFintrak(int ID)
        {
            Channel.DeleteIncomeAccountsFintrak(ID);
        }

        public IncomeAccountsFintrak GetIncomeAccountsFintrak(int ID)
        {
            return Channel.GetIncomeAccountsFintrak(ID);
        }

        public IncomeAccountsFintrak[] GetAllIncomeAccountsFintrak()
        {
            return Channel.GetAllIncomeAccountsFintrak();
        }

        #endregion


        #region IncomeAccountsNpl

        public IncomeAccountsNpl UpdateIncomeAccountsNpl(IncomeAccountsNpl incomeAccountsNpl)
        {
            return Channel.UpdateIncomeAccountsNpl(incomeAccountsNpl);
        }

        public void DeleteIncomeAccountsNpl(int ID)
        {
            Channel.DeleteIncomeAccountsNpl(ID);
        }

        public IncomeAccountsNpl GetIncomeAccountsNpl(int ID)
        {
            return Channel.GetIncomeAccountsNpl(ID);
        }

        public IncomeAccountsNpl[] GetAllIncomeAccountsNpl()
        {
            return Channel.GetAllIncomeAccountsNpl();
        }

        public IncomeAccountsNplData[] GetAllIncomeAccountsCustomers()
        {
            return Channel.GetAllIncomeAccountsCustomers();
        }

        #endregion

        #region IncomeCommFeeMis

        public IncomeCommFeeMis UpdateIncomeCommFeeMis(IncomeCommFeeMis incomeCommFeeMis)
        {
            return Channel.UpdateIncomeCommFeeMis(incomeCommFeeMis);
        }

        public void DeleteIncomeCommFeeMis(int ID)
        {
            Channel.DeleteIncomeCommFeeMis(ID);
        }

        public IncomeCommFeeMis GetIncomeCommFeeMis(int ID)
        {
            return Channel.GetIncomeCommFeeMis(ID);
        }

        public IncomeCommFeeMis[] GetAllIncomeCommFeeMis()
        {
            return Channel.GetAllIncomeCommFeeMis();
        }

        #endregion

        #region IncomeMisCodes

        public IncomeMisCodes UpdateIncomeMisCodes(IncomeMisCodes incomeMisCodes)
        {
            return Channel.UpdateIncomeMisCodes(incomeMisCodes);
        }

        public void DeleteIncomeMisCodes(int ID)
        {
            Channel.DeleteIncomeMisCodes(ID);
        }

        public IncomeMisCodes GetIncomeMisCodes(int ID)
        {
            return Channel.GetIncomeMisCodes(ID);
        }

        public IncomeMisCodes[] GetAllIncomeMisCodes()
        {
            return Channel.GetAllIncomeMisCodes();
        }

        #endregion

        #region IncomeCurrency

        public IncomeCurrency UpdateIncomeCurrency(IncomeCurrency incomeCurrency)
        {
            return Channel.UpdateIncomeCurrency(incomeCurrency);
        }

        public void DeleteIncomeCurrency(int ID)
        {
            Channel.DeleteIncomeCurrency(ID);
        }

        public IncomeCurrency GetIncomeCurrency(int ID)
        {
            return Channel.GetIncomeCurrency(ID);
        }

        public IncomeCurrency[] GetAllIncomeCurrency()
        {
            return Channel.GetAllIncomeCurrency();
        }

        #endregion

        #region IncomeMemorep

        public IncomeMemorep UpdateIncomeMemorep(IncomeMemorep incomeMemorep)
        {
            return Channel.UpdateIncomeMemorep(incomeMemorep);
        }

        public void DeleteIncomeMemorep(int ID)
        {
            Channel.DeleteIncomeMemorep(ID);
        }

        public IncomeMemorep GetIncomeMemorep(int ID)
        {
            return Channel.GetIncomeMemorep(ID);
        }

        public IncomeMemorep[] GetAllIncomeMemorep()
        {
            return Channel.GetAllIncomeMemorep();
        }

        #endregion

        #region IncomeSplitPoolsRate

        public IncomeSplitPoolsRate UpdateIncomeSplitPoolsRate(IncomeSplitPoolsRate incomeSplitPoolsRate)
        {
            return Channel.UpdateIncomeSplitPoolsRate(incomeSplitPoolsRate);
        }

        public void DeleteIncomeSplitPoolsRate(int ID)
        {
            Channel.DeleteIncomeSplitPoolsRate(ID);
        }

        public IncomeSplitPoolsRate GetIncomeSplitPoolsRate(int ID)
        {
            return Channel.GetIncomeSplitPoolsRate(ID);
        }

        public IncomeSplitPoolsRate[] GetAllIncomeSplitPoolsRate()
        {
            return Channel.GetAllIncomeSplitPoolsRate();
        }

        #endregion

        #region IncomeAccountsUnit

        public IncomeAccountsUnit UpdateIncomeAccountsUnit(IncomeAccountsUnit incomeAccountsUnit)
        {
            return Channel.UpdateIncomeAccountsUnit(incomeAccountsUnit);
        }

        public void DeleteIncomeAccountsUnit(int ID)
        {
            Channel.DeleteIncomeAccountsUnit(ID);
        }

        public IncomeAccountsUnit GetIncomeAccountsUnit(int ID)
        {
            return Channel.GetIncomeAccountsUnit(ID);
        }

        public IncomeAccountsUnit[] GetAllIncomeAccountsUnit()
        {
            return Channel.GetAllIncomeAccountsUnit();
        }

        #endregion

        #region Team Structure ALL
        public TeamStructureALL UpdateTeamStructureALL(TeamStructureALL TeamStructureALL)
        {
            return Channel.UpdateTeamStructureALL(TeamStructureALL);
        }

        public void DeleteTeamStructureALL(int Team_StructureId)
        {
            Channel.DeleteTeamStructureALL(Team_StructureId);
        }

        public TeamStructureALL GetTeamStructureALL(int Team_StructureId)
        {
            return Channel.GetTeamStructureALL(Team_StructureId);
        }

        public TeamStructureALL[] GetAllTeamStructureALL()
        {
            return Channel.GetAllTeamStructureALL();
        }

        public TeamStructureALL[] GetTeamStructureALLUsingParams(string SearchValue, int year)
        {
            return Channel.GetTeamStructureALLUsingParams(SearchValue, year);
        }

        public TeamStructureALL[] TeamStructureALLByParameters(string selectedDefinitionCode, string SearchValue, int year)
        {
            return Channel.TeamStructureALLByParameters(selectedDefinitionCode, SearchValue, year);
        }

        public TeamStructureALL[] GetTeamStructureALLByParamsAndeSetUp(string code, string SearchValue)
        {
            return Channel.GetTeamStructureALLByParamsAndeSetUp(code, SearchValue);
        }
        public TeamStructureALL[] GetTeamStructureALLUsingSetUp()
        {
            return Channel.GetTeamStructureALLUsingSetUp();
        }

        public TeamStructureALL[] GetTeamStructureALLUsingDefinitionCode(string code)
        {
            return Channel.GetTeamStructureALLUsingDefinitionCode(code);
        }

        public TeamStructureALL[] GetTeamStructureALLUsingDefinitionCodeMonthly(string code)
        {
            return Channel.GetTeamStructureALLUsingDefinitionCodeMonthly(code);
        }

        public TeamStructureALL[] GetTeamStructureALLByParamsAndeSetUpMonthly(string code, string SearchValue)
        {
            return Channel.GetTeamStructureALLByParamsAndeSetUpMonthly(code, SearchValue);
        }

        public TeamStructureALL[] GetTeamStructureALLUsingSetUpMonthly()
        {
            return Channel.GetTeamStructureALLUsingSetUpMonthly();
        }

        public TeamStructureALL GetTeamStructureALLTop1(string branch, string defcode, int year)
        {
            return Channel.GetTeamStructureALLTop1(branch, defcode, year);
        }


        #endregion

        #region IncomeFintrakAccountsSegment

        public IncomeFintrakAccountsSegment UpdateIncomeFintrakAccountsSegment(IncomeFintrakAccountsSegment ifas)
        {
            return Channel.UpdateIncomeFintrakAccountsSegment(ifas);
        }

        public void DeleteIncomeFintrakAccountsSegment(int Id)
        {
            Channel.DeleteIncomeFintrakAccountsSegment(Id);
        }

        public IncomeFintrakAccountsSegment GetIncomeFintrakAccountsSegment(int Id)
        {
            return Channel.GetIncomeFintrakAccountsSegment(Id);
        }

        public IncomeFintrakAccountsSegment[] GetAllIncomeFintrakAccountsSegment()
        {
            return Channel.GetAllIncomeFintrakAccountsSegment();
        }

        public IncomeFintrakAccountsSegment[] GetAccountsSegmentByCustomerIdBank(string customerid, string bank)
        {
            return Channel.GetAccountsSegmentByCustomerIdBank(customerid, bank);
        }

        #endregion

        #region FTPRiskRatings

        public FTPRiskRatings UpdateFTPRiskRatings(FTPRiskRatings fTPRiskRatings)
        {
            return Channel.UpdateFTPRiskRatings(fTPRiskRatings);
        }

        public void DeleteFTPRiskRatings(int ID)
        {
            Channel.DeleteFTPRiskRatings(ID);
        }

        public FTPRiskRatings GetFTPRiskRatings(int ID)
        {
            return Channel.GetFTPRiskRatings(ID);
        }

        public FTPRiskRatings[] GetAllFTPRiskRatings()
        {
            return Channel.GetAllFTPRiskRatings();
        }

        public FTPRiskRatings[] GetFTPRiskRatingsUsingSearchValue(string searchvalue)
        {
            return Channel.GetFTPRiskRatingsUsingSearchValue(searchvalue);
        }
        #endregion

        #region IncomeCaptionPoolRate

        public IncomeCaptionPoolRate UpdateIncomeCaptionPoolRate(IncomeCaptionPoolRate incomeCaptionPoolRate)
        {
            return Channel.UpdateIncomeCaptionPoolRate(incomeCaptionPoolRate);
        }

        public void DeleteIncomeCaptionPoolRate(int ID)
        {
            Channel.DeleteIncomeCaptionPoolRate(ID);
        }

        public IncomeCaptionPoolRate GetIncomeCaptionPoolRate(int ID)
        {
            return Channel.GetIncomeCaptionPoolRate(ID);
        }

        public IncomeCaptionPoolRate[] GetAllIncomeCaptionPoolRate()
        {
            return Channel.GetAllIncomeCaptionPoolRate();
        }

        public IncomeCaptionPoolRate[] GetIncomeCaptionPoolRateUsingSearchValue(string searchvalue)
        {
            return Channel.GetIncomeCaptionPoolRateUsingSearchValue(searchvalue);
        }

        #endregion

        #region IncomeAccountsTreeAccountTEMP

        public IncomeAccountsTreeAccountTEMP UpdateIncomeAccountsTreeAccountTEMP(IncomeAccountsTreeAccountTEMP iatat)
        {
            return Channel.UpdateIncomeAccountsTreeAccountTEMP(iatat);
        }

        public void DeleteIncomeAccountsTreeAccountTEMP(int ID)
        {
            Channel.DeleteIncomeAccountsTreeAccountTEMP(ID);
        }

        public IncomeAccountsTreeAccountTEMP GetIncomeAccountsTreeAccountTEMP(int ID)
        {
            return Channel.GetIncomeAccountsTreeAccountTEMP(ID);
        }

        public IncomeAccountsTreeAccountTEMP[] GetAllIncomeAccountsTreeAccountTEMP()
        {
            return Channel.GetAllIncomeAccountsTreeAccountTEMP();
        }

        public IncomeAccountsTreeAccountTEMP[] GetIncomeAccountsTreeAccountTEMPBySearchVal(string search)
        {
            return Channel.GetIncomeAccountsTreeAccountTEMPBySearchVal(search);
        }

        #endregion


        #region IncomeAccountsTreeMisCodesTEMP

        public IncomeAccountsTreeMisCodesTEMP UpdateIncomeAccountsTreeMisCodesTEMP(IncomeAccountsTreeMisCodesTEMP iatat)
        {
            return Channel.UpdateIncomeAccountsTreeMisCodesTEMP(iatat);
        }

        public void DeleteIncomeAccountsTreeMisCodesTEMP(int ID)
        {
            Channel.DeleteIncomeAccountsTreeMisCodesTEMP(ID);
        }

        public IncomeAccountsTreeMisCodesTEMP GetIncomeAccountsTreeMisCodesTEMP(int ID)
        {
            return Channel.GetIncomeAccountsTreeMisCodesTEMP(ID);
        }

        public IncomeAccountsTreeMisCodesTEMP[] GetAllIncomeAccountsTreeMisCodesTEMP()
        {
            return Channel.GetAllIncomeAccountsTreeMisCodesTEMP();
        }

        public IncomeAccountsTreeMisCodesTEMP[] GetIncomeAccountsTreeMisCodesTEMPBySearchVal(string search)
        {
            return Channel.GetIncomeAccountsTreeMisCodesTEMPBySearchVal(search);
        }

        #endregion

        #region OneBankAO

        public OneBankAO UpdateOneBankAO(OneBankAO onebank)
        {
            return Channel.UpdateOneBankAO(onebank);
        }

        public void DeleteOneBankAO(int Id)
        {
            Channel.DeleteOneBankAO(Id);
        }

        public OneBankAO GetOneBankAO(int Id)
        {
            return Channel.GetOneBankAO(Id);
        }

        public OneBankAO[] GetAllOneBankAO()
        {
            return Channel.GetAllOneBankAO();
        }

        public OneBankAO[] GetOneBankAOByParams(string SearchValue, int year, int period)
        {
            return Channel.GetOneBankAOByParams(SearchValue, year, period);
        }

        #endregion

        #region OneBankBranch

        public OneBankBranch UpdateOneBankBranch(OneBankBranch onebank)
        {
            return Channel.UpdateOneBankBranch(onebank);
        }

        public void DeleteOneBankBranch(int Id)
        {
            Channel.DeleteOneBankBranch(Id);
        }

        public OneBankBranch GetOneBankBranch(int Id)
        {
            return Channel.GetOneBankBranch(Id);
        }

        public OneBankBranch[] GetAllOneBankBranch()
        {
            return Channel.GetAllOneBankBranch();
        }

        public OneBankBranch[] GetOneBankBranchByParams(string SearchValue, int year, int period)
        {
            return Channel.GetOneBankBranchByParams(SearchValue, year, period);
        }

        #endregion

        #region OneBankRegionTD

        public OneBankRegionTD UpdateOneBankRegionTD(OneBankRegionTD oneBankRegionTD)
        {
            return Channel.UpdateOneBankRegionTD(oneBankRegionTD);
        }

        public void DeleteOneBankRegionTD(int ID)
        {
            Channel.DeleteOneBankRegionTD(ID);
        }

        public OneBankRegionTD GetOneBankRegionTD(int ID)
        {
            return Channel.GetOneBankRegionTD(ID);
        }

        public OneBankRegionTD[] GetAllOneBankRegionTD()
        {
            return Channel.GetAllOneBankRegionTD();
        }

        #endregion


        #region OneBankTeamTable

        public OneBankTeamTable UpdateOneBankTeamTable(OneBankTeamTable oneBankTeamTable)
        {
            return Channel.UpdateOneBankTeamTable(oneBankTeamTable);
        }

        public void DeleteOneBankTeamTable(int ID)
        {
            Channel.DeleteOneBankTeamTable(ID);
        }

        public OneBankTeamTable GetOneBankTeamTable(int ID)
        {
            return Channel.GetOneBankTeamTable(ID);
        }

        public OneBankTeamTable[] GetAllOneBankTeamTable()
        {
            return Channel.GetAllOneBankTeamTable();
        }

        #endregion

        #region MprInterestMapping

        public MprInterestMapping UpdateMprInterestMapping(MprInterestMapping mprInterestMapping)
        {
            return Channel.UpdateMprInterestMapping(mprInterestMapping);
        }

        public void DeleteMprInterestMapping(int ID)
        {
            Channel.DeleteMprInterestMapping(ID);
        }

        public MprInterestMapping GetMprInterestMapping(int ID)
        {
            return Channel.GetMprInterestMapping(ID);
        }

        public MprInterestMapping[] GetAllMprInterestMapping()
        {
            return Channel.GetAllMprInterestMapping();
        }

        #endregion

        #region MISNewOthers TEMP

        public MISNewOthersTEMP UpdateMISNewOthersTEMP(MISNewOthersTEMP iatat)
        {
            return Channel.UpdateMISNewOthersTEMP(iatat);
        }

        public void DeleteMISNewOthersTEMP(int Id)
        {
            Channel.DeleteMISNewOthersTEMP(Id);
        }

        public MISNewOthersTEMP GetMISNewOthersTEMP(int Id)
        {
            return Channel.GetMISNewOthersTEMP(Id);
        }

        public MISNewOthersTEMP[] GetAllMISNewOthersTEMP()
        {
            return Channel.GetAllMISNewOthersTEMP();
        }

        public MISNewOthersTEMP[] GetMISNewOthersTEMPBySearchVal(string search)
        {
            return Channel.GetMISNewOthersTEMPBySearchVal(search);
        }

        #endregion

        #region IncomeCustomerPoolRate

        public IncomeCustomerPoolRate UpdateIncomeCustomerPoolRate(IncomeCustomerPoolRate incomeCustomerPoolRate)
        {
            return Channel.UpdateIncomeCustomerPoolRate(incomeCustomerPoolRate);
        }

        public void DeleteIncomeCustomerPoolRate(int Id)
        {
            Channel.DeleteIncomeCustomerPoolRate(Id);
        }

        public IncomeCustomerPoolRate GetIncomeCustomerPoolRate(int Id)
        {
            return Channel.GetIncomeCustomerPoolRate(Id);
        }

        public IncomeCustomerPoolRate[] GetAllIncomeCustomerPoolRate()
        {
            return Channel.GetAllIncomeCustomerPoolRate();
        }

        public IncomeCustomerPoolRate[] GetIncomeCustomerPoolRateUsingSearchValue(string searchvalue)
        {
            return Channel.GetIncomeCustomerPoolRateUsingSearchValue(searchvalue);
        }

        #endregion

        #region IncomeAccountPoolRate

        public IncomeAccountPoolRate UpdateIncomeAccountPoolRate(IncomeAccountPoolRate incomeAccountPoolRate)
        {
            return Channel.UpdateIncomeAccountPoolRate(incomeAccountPoolRate);
        }

        public void DeleteIncomeAccountPoolRate(int Id)
        {
            Channel.DeleteIncomeAccountPoolRate(Id);
        }

        public IncomeAccountPoolRate GetIncomeAccountPoolRate(int Id)
        {
            return Channel.GetIncomeAccountPoolRate(Id);
        }

        public IncomeAccountPoolRate[] GetAllIncomeAccountPoolRate()
        {
            return Channel.GetAllIncomeAccountPoolRate();
        }

        public IncomeAccountPoolRate[] GetIncomeAccountPoolRateUsingSearchValue(string searchvalue)
        {
            return Channel.GetIncomeAccountPoolRateUsingSearchValue(searchvalue);
        }

        #endregion



    }
}
