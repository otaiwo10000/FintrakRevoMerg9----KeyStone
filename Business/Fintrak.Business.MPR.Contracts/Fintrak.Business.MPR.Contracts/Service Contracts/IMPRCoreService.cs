using System;
using System.Linq;
using System.ServiceModel;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.MPR.Framework;
using System.Collections.Generic;

namespace Fintrak.Business.MPR.Contracts
{
    [ServiceContract]
    public interface IMPRCoreService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();

        #region UserMIS

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        UserMIS UpdateUserMIS(UserMIS userMIS);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteUserMIS(int userMISId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        UserMIS GetUserMIS(int userMISId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        UserMIS GetUserMISByLoginID(string loginID);

        [OperationContract]
        UserMIS[] GetAllUserMISs();

        #endregion

        #region UserClassificationMap

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        UserClassificationMap UpdateUserClassificationMap(UserClassificationMap userClassificationMap);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteUserClassificationMap(int userClassificationMapId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        UserClassificationMap GetUserClassificationMap(int userClassificationMapId);

        [OperationContract]
        UserClassificationMap[] GetAllUserClassificationMaps(string loginID);

        #endregion

        #region TeamDefinition

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        TeamDefinition UpdateTeamDefinition(TeamDefinition teamDefinition);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTeamDefinition(int teamDefinitionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TeamDefinition GetTeamDefinition(int teamDefinitionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TeamDefinition GetTeamDefinitionByCode(string code);

        [OperationContract]
        IEnumerable<TeamDefinition> GetAllTeamDefinitions();

        #endregion

        #region TeamClassificationType

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        TeamClassificationType UpdateTeamClassificationType(TeamClassificationType teamClassificationType);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTeamClassificationType(int teamClassificationTypeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TeamClassificationType GetTeamClassificationType(int teamClassificationTypeId);

        [OperationContract]
        TeamClassificationType[] GetAllTeamClassificationTypes();

        #endregion

        #region TeamClassification

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        TeamClassification UpdateTeamClassification(TeamClassification teamClassification);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTeamClassification(int teamClassificationId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TeamClassification GetTeamClassification(int teamClassificationId);

        [OperationContract]
        TeamClassification[] GetAllTeamClassifications();

        [OperationContract]
        TeamClassification[] GetTeamClassifications(string typeCode);

        #endregion

        #region Team

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Team UpdateTeam(Team team);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTeam(int teamId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Team GetTeam(int teamId);

        [OperationContract]
        Team[] GetParentTeams(string definitionCode);

        [OperationContract]
        Team[] GetTeamByLevel(int level);

        [OperationContract]
        IEnumerable<Team> GetTeamByDefinition(string definitionCode);

        [OperationContract]
        TeamData[] GetTeams();

        [OperationContract]
        TeamData[] GetTeamsBySearch(string SearchValue);

        #endregion

        #region TeamClassificationMap

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        TeamClassificationMap UpdateTeamClassificationMap(TeamClassificationMap teamClassificationMap);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTeamClassificationMap(int teamClassificationMapId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TeamClassificationMap GetTeamClassificationMap(int teamClassificationMapId);

        [OperationContract]
        TeamClassificationMap[] GetAllTeamClassificationMaps(string misCode, string definitionCode);

        #endregion

        #region AccountOfficerDetail

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        AccountOfficerDetail UpdateAccountOfficerDetail(AccountOfficerDetail accountOfficerDetail);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteAccountOfficerDetail(int accountOfficerDetailId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        AccountOfficerDetail GetAccountOfficerDetail(int accountOfficerDetailId);

        [OperationContract]
        AccountOfficerDetail[] GetAllAccountOfficerDetails();

        #endregion

        #region BranchDefaultMIS

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        BranchDefaultMIS UpdateBranchDefaultMIS(BranchDefaultMIS branchDefaultMIS);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBranchDefaultMIS(int branchDefaultMISId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BranchDefaultMIS GetBranchDefaultMIS(int branchDefaultMISId);

        [OperationContract]
        BranchDefaultMIS[] GetAllBranchDefaultMISs();

        #endregion

        #region AccountMIS

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        AccountMIS UpdateAccountMIS(AccountMIS accountMIS);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteAccountMIS(int accountMISId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        AccountMIS GetAccountMIS(int accountMISId);

        [OperationContract]
        AccountMISData[] GetAllAccountMISs();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSelectedIds(string selectedIds);

        #endregion

        #region ManagementTree

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ManagementTree UpdateManagementTree(ManagementTree managementTree);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteManagementTree(int managementTreeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ManagementTree GetManagementTree(int managementTreeId);

        [OperationContract]
        ManagementTreeData[] GetAllManagementTrees();

        #endregion

        #region MISReplacement

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MISReplacement UpdateMISReplacement(MISReplacement misReplacement);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMISReplacement(int misReplacementId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MISReplacement GetMISReplacement(int misReplacementId);

        [OperationContract]
        MISReplacement[] GetAllMISReplacements();

        #endregion

        #region SetUp

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SetUp UpdateMPRSetup(SetUp mprMPRSetup);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SetUp GetFirstMPRSetup();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MPRSetupData[] GetFirstMPRSetups();

        #endregion

        #region TransferPrice

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        TransferPrice UpdateTransferPrice(TransferPrice transferPrice);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTransferPrice(int transferPriceId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TransferPrice GetTransferPrice(int transferPriceId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TransferPriceData[] GetAllTransferPrices();

        #endregion

        #region AccountTransferPrice

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        AccountTransferPrice UpdateAccountTransferPrice(AccountTransferPrice accountTransferPrice);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteAccountTransferPrice(int accountTransferPriceId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        AccountTransferPrice GetAccountTransferPrice(int accountTransferPriceId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        AccountTransferPriceData[] GetAllAccountTransferPrices();

        #endregion

        #region GeneralTransferPrice

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        GeneralTransferPrice UpdateGeneralTransferPrice(GeneralTransferPrice generalTransferPrice);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteGeneralTransferPrice(int generalTransferPriceId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        GeneralTransferPrice GetGeneralTransferPrice(int generalTransferPriceId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        GeneralTransferPriceData[] GetAllGeneralTransferPrices();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteGTPSelectedIds(string selectedIds);

        #endregion

        #region CustAccount

        [OperationContract]
        CustAccount[] GetAllCustAccounts();

        [OperationContract]
        CustAccount[] GetCustAccounts(string searchType, string searchValue, int number);

        #endregion

        #region BSExemption

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        BSExemption UpdateBSExemption(BSExemption bsExemption);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBSExemption(int bsExemptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BSExemption GetBSExemption(int bsExemptionId);

        [OperationContract]
        BSExemption[] GetAllBSExemptions();

        #endregion

        #region MemoAccountMap

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MemoAccountMap UpdateMemoAccountMap(MemoAccountMap memoAccountMap);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMemoAccountMap(int memoAccountMapId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MemoAccountMap GetMemoAccountMap(int memoAccountMapId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MemoAccountMapData[] GetAllMemoAccountMaps();

        #endregion

        #region MemoGLMap

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MemoGLMap UpdateMemoGLMap(MemoGLMap memoGLMap);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMemoGLMap(int memoGLMapId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MemoGLMap GetMemoGLMap(int memoGLMapId);

        [OperationContract]
        MemoGLMapData[] GetAllMemoGLMaps();

        #endregion

        #region MemoProductMap

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MemoProductMap UpdateMemoProductMap(MemoProductMap memoProductMap);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMemoProductMap(int memoProductMapId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MemoProductMap GetMemoProductMap(int memoProductMapId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MemoProductMapData[] GetAllMemoProductMaps();

        #endregion

        #region CaptionMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CaptionMapping UpdateCaptionMapping(CaptionMapping captionMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCaptionMapping(int captionMappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CaptionMapping GetCaptionMapping(int captionMappingId);

        [OperationContract]
        CaptionMapping[] GetAllCaptionMappings();


        #endregion

        #region RatioCaptionMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        RatioCaptionMapping UpdateRatioCaptionMapping(RatioCaptionMapping ratioCaptionMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteRatioCaptionMapping(int ratioCaptionMappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        RatioCaptionMapping GetRatioCaptionMapping(int ratioCaptionMappingId);

        [OperationContract]
        RatioCaptionMapping[] GetAllRatioCaptionMappings();


        #endregion

        #region Ratios

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Ratios UpdateRatios(Ratios ratios);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteRatios(int ratiosId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Ratios GetRatios(int ratiosId);

        [OperationContract]
        Ratios[] GetAllRatios();


        #endregion

        #region MemoUnits

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MemoUnits UpdateMemoUnits(MemoUnits memoUnit);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMemoUnits(int memoUnitId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MemoUnits GetMemoUnits(int memoUnitId);

        [OperationContract]
        MemoUnits[] GetAllMemoUnits();

        #endregion

        #region Staffss

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Staffs UpdateStaffs(Staffs staffs);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteStaffs(int staffId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Staffs GetStaffs(int staffId);

        [OperationContract]
        Staffs[] GetAllStaffs();

        #endregion Staffs

        #region MessagingSubscription

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MessagingSubscription UpdateMessagingSubscription(MessagingSubscription messagingSubscription);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMessagingSubscription(int messagingSubscriptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MessagingSubscription GetMessagingSubscription(int messagingSubscriptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Revenue[] GetMessagingSubscriptionByRecipients(string recipients);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        DateTime[] GetRecipents();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        string[] GetReports();


        #endregion

        #region AbcRatio

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        AbcRatio UpdateAbcRatio(AbcRatio abcRatio);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteAbcRatio(int abcRatioId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        AbcRatio GetAbcRatio(int abcRatioId);

        [OperationContract]
        AbcRatio[] GetAllAbcRatio();


        #endregion

        #region Sbu

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Sbu UpdateSbu(Sbu sbu);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSbu(int sbuId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Sbu GetSbu(int sbuId);

        [OperationContract]
        Sbu[] GetAllSbu();


        #endregion

        #region SbuType

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SbuType UpdateSbuType(SbuType sbuType);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSbuType(int sbuTypeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SbuType GetSbuType(int sbuTypeId);

        [OperationContract]
        SbuType[] GetAllSbuType();


        #endregion

        #region Servicese

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Servicese UpdateServices(Servicese service);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteServices(int servicesId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Servicese GetServices(int serviceId);

        [OperationContract]
        Servicese[] GetAllServices();


        #endregion

        #region

        [OperationContract]
        crb_Data[] GetAllCrbData();

        [OperationContract]
        account_interest[] GellALlAccountInterest();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        product_interestData[] GetAllProductInterest();

        #endregion

        #region Team Structure 

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        TeamStructure UpdateTeamStructure(TeamStructure teamstructure);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTeamStructure(int Team_StructureId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TeamStructure GetTeamStructure(int Team_StructureId);       

        [OperationContract]
        TeamStructure[] GetAllTeamStructure();

        [OperationContract]
        TeamStructure[] GetTeamStructureUsingParams(string SearchValue, string year);

        [OperationContract]
        TeamStructure[] TeamstructureByParameters(string selectedDefinitionCode, string SearchValue, string year);

        [OperationContract]
        TeamStructure[] GetTeamstructureByParamsAndeSetUp(string code, string SearchValue);

        [OperationContract]
        TeamStructure[] GetTeamStructureUsingSetUp();

        [OperationContract]
        TeamStructure[] GetTeamStructureUsingDefinitionCode(string code);

        [OperationContract]
        TeamStructure[] GetTeamStructureUsingDefinitionCodeMonthly(string code);

        [OperationContract]
        TeamStructure[] GetTeamstructureByParamsAndeSetUpMonthly(string code, string SearchValue);
        [OperationContract]
        TeamStructure[] GetTeamStructureUsingSetUpMonthly();

        //[OperationContract]
        //TeamStructure GetTeamStructureTop1(string branch, string year);

        [OperationContract]
        TeamStructure GetTeamStructureTop1(string branch, string defcode, string year);


        #endregion


        [OperationContract]
        PublicSectorData[] GetAllPublicSectorData();

        #region CorporateAdjustment

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CorporateAdjustment UpdateCorporateAdjustment(CorporateAdjustment corporateadjustment);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCorporateAdjustment(int CorporateAdjustmentId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CorporateAdjustment GetCorporateAdjustment(int CorporateAdjustmentId);

        [OperationContract]
        CorporateAdjustment[] GetAllCorporateAdjustment();


        #endregion

        #region CaptionTransferPrice

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        caption_transfer_price UpdateCaptionTransferPrice(caption_transfer_price ctp);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCaptionTransferPrice(int caption_transfer_price_Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        caption_transfer_price GetCaptionTransferPrice(int caption_transfer_price_Id);

        [OperationContract]
        caption_transfer_price[] GetAllCaptionTransferPrice();

        #endregion

        #region AssetType

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        AssetType UpdateAssetType(AssetType assettype);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteAssetType(int AssetType_Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        AssetType GetAssetType(int AssetType_Id);

        [OperationContract]
        AssetType[] GetAllAssetType();


        #endregion

        #region Custormer MIS

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Customermis UpdateCustomermis(Customermis customermis);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCustomermis(int CustomermisId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Customermis GetCustomermis(int CustomermisId);

        [OperationContract]
        Customermis[] GetAllCustomermis();


        #endregion

        #region PPR

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PPR UpdatePPR(PPR ppr);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePPR(int PPRId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        PPR GetPPR(int PPRId);

        [OperationContract]
        PPR[] GetAllPPR();

        #endregion

        #region Risk Adjusted Charge

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        RiskAdjustedCharge UpdateRiskAdjustedCharge(RiskAdjustedCharge rac);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteRiskAdjustedCharge(int RiskAdjustedChargeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        RiskAdjustedCharge GetRiskAdjustedCharge(int RiskAdjustedChargeId);

        [OperationContract]
        RiskAdjustedCharge[] GetAllRiskAdjustedCharge();


        #endregion

        #region MPR ScoreCard Metrics 

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ScoreCardMetrics UpdateScoreCardMetric(ScoreCardMetrics scorecardmetric);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteScoreCardMetric(int MetricId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ScoreCardMetrics GetScoreCardMetric(int MetricId);

        [OperationContract]
        ScoreCardMetrics[] GetAllScoreCardMetrics();

        [OperationContract]
        ScoreCardMetricsData[] GetScoreCardMetricsUsingSearchValue(string searchvalue);

        [OperationContract]
        //ScoreCardMetrics[] GetScoreCardMetricsUsingSetUp();
        ScoreCardMetricsData[] GetScoreCardMetricsUsingSetUp();

        #endregion

        #region MPR ScoreCard Weight 

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ScoreCardWeight UpdateScoreCardWeight(ScoreCardWeight scorecardweight);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteScoreCardWeight(int WeightId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ScoreCardWeight GetScoreCardWeight(int WeightId);

        [OperationContract]
        ScoreCardWeight[] GetAllScoreCardWeight();

        [OperationContract]
        ScoreCardWeightData[] GetScoreCardWeightWITHMetrics();

        //[OperationContract]
        //ScoreCardMetrics[] GetScoreCardMetricsUsingSearchValue(string searchvalue);

        //[OperationContract]
        //ScoreCardMetrics[] GetScoreCardMetricsUsingSetUp();

        #endregion

        #region MPR Scorecard Perspective 

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ScoreCardPerspective UpdateScorecardPerspective(ScoreCardPerspective scPerspective);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteScorecardPerspective(int PerspectiveId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ScoreCardPerspective GetScorecardPerspective(int PerspectiveId);

        [OperationContract]
        ScoreCardPerspective[] GetAllScorecardPerspective();


        #endregion

        #region MPR ScoreCard Mapping 

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ScoreCardMapping UpdateScoreCardMapping(ScoreCardMapping scorecardmapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteScoreCardMapping(int MappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ScoreCardMapping GetScoreCardMapping(int MappingId);

        [OperationContract]
        ScoreCardMapping[] GetAllScoreCardMapping();

        [OperationContract]
        ScoreCardMappingData[] GetScoreCardMappingUsingSearchValue(string searchvalue);

        [OperationContract]
        ScoreCardMappingData[] GetScoreCardMappingUsingSetUp();

        #endregion


        #region MPR ScoreCard 

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ScoreCard UpdateScoreCard(ScoreCard scorecard);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteScoreCard(int mpr_scorecard_stgId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ScoreCard GetScoreCard(int mpr_scorecard_stgId);

        [OperationContract]
        ScoreCard[] GetAllScoreCard();

        [OperationContract]
        ScoreCard[] ScoreCardCaptions();

        #endregion

        #region MIS Transfer Price

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MISTransferPrice UpdateMISTransferPrice(MISTransferPrice mistransferprice);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMISTransferPrice(int mistransferpriceId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MISTransferPrice GetMISTransferPrice(int mistransferpriceId);

        [OperationContract]
        MISTransferPrice[] GetAllMISTransferPrice();

        [OperationContract]
        MISTransferPriceData[] GetMISTransferPriceUsingSetUp();

        [OperationContract]
        MISTransferPriceData[] GetMISTransferPricebyParams(string defCode, string miscode, string category, string currency, int year, int period);



        #endregion

        #region Product Transfer Price

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ProductTransferPrice Updateproducttransferprice(ProductTransferPrice producttransferprice);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void Deleteproducttransferprice(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ProductTransferPrice Getproducttransferprice(int ID);

        [OperationContract]
        ProductTransferPrice[] GetAllProductTransferPrice();

        [OperationContract]
        ProductTransferPriceData[] GetProductTransferPriceUsingSearchValue(string searchvalue);

        #endregion

        #region Team bank 

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        TeamBank UpdateTeamBank(TeamBank teambank);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTeamBank(int teambankId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TeamBank GetTeamBank(int teambankId);

        [OperationContract]
        TeamBank[] GetAllTeamBanks();

        [OperationContract]
        TeamBank[] GetTeamBanksUsingParams(string searchvalue, int year);

        [OperationContract]
        TeamBank[] GetTeamBankUsingDefinitionCode(string code);

        #endregion

        #region ScoreCardMetricsKBL

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ScoreCardMetricsKBL UpdateScoreCardMetricsKBL(ScoreCardMetricsKBL scorecardmetricsKBL);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteScoreCardMetricsKBL(int MetricID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ScoreCardMetricsKBL GetScoreCardMetricsKBL(int MetricID);

        [OperationContract]
        ScoreCardMetricsKBL[] GetAllScoreCardMetricsKBL();

        [OperationContract]
        ScoreCardMetricsKBL[] GetScoreCardMetricsKBLUsingSearchValue(string searchvalue);

        [OperationContract]
        ScoreCardMetricsKBL[] GetScoreCardMetricsKBLUsingYear(int year);

        #endregion

        #region ScoreCard KPI Types KBL

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ScoreCardKPITypesKBL UpdateScoreCardKPITypesKBL(ScoreCardKPITypesKBL scorecardKPItypesKBL);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteScoreCardKPITypesKBL(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ScoreCardKPITypesKBL GetScoreCardKPITypesKBL(int ID);

        [OperationContract]
        ScoreCardKPITypesKBL[] GetAllScoreCardKPITypesKBL();

        [OperationContract]
        ScoreCardKPITypesKBL[] GetScoreCardKPITypesKBLUsingSearchValue(string searchvalue);

        [OperationContract]
        ScoreCardKPITypesKBL[] GetScoreCardKPITypesKBLByPeriodYearKPIType(int period, int year, string searchvalue);

        #endregion

        #region ScoreCard Set Metric Target KBL

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ScoreCardSetMetricTargetKBL UpdateScoreCardSetMetricTargetKBL(ScoreCardSetMetricTargetKBL scoreCcardsetmetrictarget);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteScoreCardSetMetricTargetKBL(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ScoreCardSetMetricTargetKBL GetScoreCardSetMetricTargetKBL(int ID);

        [OperationContract]
        ScoreCardSetMetricTargetKBL[] GetAllScoreCardSetMetricTargetKBL();

        [OperationContract]
        ScoreCardSetMetricTargetKBL[] GetScoreCardSetMetricTargetKBLUsingSearchValue(string searchvalue);

        [OperationContract]
        ScoreCardSetMetricTargetKBL[] GetScoreCardSetMetricTargetKBLByPeriodANDYear(int period, int year);

        #endregion

        #region ScoreCard MIS Mapping KBL

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ScoreCardMISMappingKBL UpdateScoreCardMISMappingKBL(ScoreCardMISMappingKBL scorecardMISmapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteScoreCardMISMappingKBL(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ScoreCardMISMappingKBL GetScoreCardMISMappingKBL(int ID);

        [OperationContract]
        ScoreCardMISMappingKBL[] GetAllScoreCardMISMappingKBL();

        [OperationContract]
        ScoreCardMISMappingKBL[] GetScoreCardMISMappingKBLUsingSearchValue(string searchvalue);

        #endregion

        #region Income Cash Vault Schedule

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeCashVaultSchedule UpdateIncomeCashVaultSchedule(IncomeCashVaultSchedule incomecashvaultschedule);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeCashVaultSchedule(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeCashVaultSchedule GetIncomeCashVaultSchedule(int ID);

        [OperationContract]
        IncomeCashVaultSchedule[] GetAllIncomeCashVaultScheduleL();


        #endregion

        #region Income Setup

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeSetup UpdateIncomeSetup(IncomeSetup incomesetup);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeSetup(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeSetup GetIncomeSetup(int ID);

        [OperationContract]
        IncomeSetup[] GetAllIncomeSetup();

        [OperationContract]
        IncomeSetup GetLatestIncomeSetup();


        #endregion

        #region Slary Schedule

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SlarySchedule UpdateSlarySchedule(SlarySchedule slaryschedule);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSlarySchedule(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SlarySchedule GetSlarySchedule(int ID);

        [OperationContract]
        SlarySchedule[] GetAllSlarySchedule();


        #endregion

        #region Income Other Breakdown

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeOtherBreakdown UpdateIncomeOtherBreakdown(IncomeOtherBreakdown iob);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeOtherBreakdown(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeOtherBreakdown GetIncomeOtherBreakdown(int ID);

        [OperationContract]
        IncomeOtherBreakdown[] GetAllIncomeOtherBreakdown();


        #endregion

        #region Download Base Fintrak FinalManual

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        DownloadBaseFintrakFinalManual UpdateDDBaseFFM(DownloadBaseFintrakFinalManual ddb);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteDDBaseFFM(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        DownloadBaseFintrakFinalManual GetDDBaseFFM(int ID);

        [OperationContract]
        DownloadBaseFintrakFinalManual[] GetAllDDBaseFFM();


        #endregion

        #region MPRReportStatus

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MPRReportStatus UpdateMPRReportStatus(MPRReportStatus mprreportstatus);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMPRReportStatus(int MPRReportStatusId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MPRReportStatus GetMPRReportStatus(int MPRReportStatusId);

        [OperationContract]
        MPRReportStatus[] GetAllMPRReportStatus();


        #endregion

        #region FinstatMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FinstatMapping UpdateFinstatMapping(FinstatMapping finstatMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteFinstatMapping(int finstatMappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FinstatMapping GetFinstatMapping(int finstatMappingId);

        [OperationContract]
        FinstatMapping[] GetAllFinstatMapping();


        #endregion

        #region IncomeAccountsTreeMisCodes

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeAccountsTreeMisCodes UpdateIncomeAccountsTreeMisCodes(IncomeAccountsTreeMisCodes incomeAccountsTreeMisCodes);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeAccountsTreeMisCodes(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeAccountsTreeMisCodes GetIncomeAccountsTreeMisCodes(int ID);

        [OperationContract]
        IncomeAccountsTreeMisCodes[] GetAllIncomeAccountsTreeMisCodes();

        [OperationContract]
        IncomeAccountsTreeMisCodes[] GetByAccountNumber(string AccountNumber);

        #endregion

        #region IncomeAccountsTreeAccount

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeAccountsTreeAccount UpdateIncomeAccountsTreeAccount(IncomeAccountsTreeAccount incomeAccountsTreeAccount);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeAccountsTreeAccount(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeAccountsTreeAccount GetIncomeAccountsTreeAccount(int ID);

        [OperationContract]
        IncomeAccountsTreeAccount[] GetAllIncomeAccountsTreeAccount();

        [OperationContract]
        IncomeAccountsTreeAccount[] FilterByAccountNumber(string AccountNumber);

        #endregion

        #region IncomePoolRateSbu

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomePoolRateSbu UpdateIncomePoolRateSbu(IncomePoolRateSbu incomePoolRateSbu);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomePoolRateSbu(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomePoolRateSbu GetIncomePoolRateSbu(int ID);

        [OperationContract]
        IncomePoolRateSbu[] GetAllIncomePoolRateSbu();


        #endregion

        #region IncomePoolRateSbuYear

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomePoolRateSbuYear UpdateIncomePoolRateSbuYear(IncomePoolRateSbuYear incomePoolRateSbuYear);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomePoolRateSbuYear(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomePoolRateSbuYear GetIncomePoolRateSbuYear(int ID);

        [OperationContract]
        IncomePoolRateSbuYear[] GetAllIncomePoolRateSbuYear();


        #endregion

        #region IncomeAccountsFintrak

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeAccountsFintrak UpdateIncomeAccountsFintrak(IncomeAccountsFintrak incomeAccountsFintrak);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeAccountsFintrak(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeAccountsFintrak GetIncomeAccountsFintrak(int ID);

        [OperationContract]
        IncomeAccountsFintrak[] GetAllIncomeAccountsFintrak();


        #endregion

        #region IncomeAccountsNpl

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeAccountsNpl UpdateIncomeAccountsNpl(IncomeAccountsNpl incomeAccountsNpl);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeAccountsNpl(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeAccountsNpl GetIncomeAccountsNpl(int ID);

        [OperationContract]
        IncomeAccountsNpl[] GetAllIncomeAccountsNpl();

        [OperationContract]
        IncomeAccountsNplData[] GetAllIncomeAccountsCustomers();



        #endregion

        #region IncomeCommFeeMis

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeCommFeeMis UpdateIncomeCommFeeMis(IncomeCommFeeMis incomeCommFeeMis);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeCommFeeMis(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeCommFeeMis GetIncomeCommFeeMis(int ID);

        [OperationContract]
        IncomeCommFeeMis[] GetAllIncomeCommFeeMis();

        #endregion

        #region IncomeMisCodes

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeMisCodes UpdateIncomeMisCodes(IncomeMisCodes incomeMisCodes);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeMisCodes(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeMisCodes GetIncomeMisCodes(int ID);

        [OperationContract]
        IncomeMisCodes[] GetAllIncomeMisCodes();

        #endregion

        #region IncomeCurrency

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeCurrency UpdateIncomeCurrency(IncomeCurrency incomeCurrency);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeCurrency(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeCurrency GetIncomeCurrency(int ID);

        [OperationContract]
        IncomeCurrency[] GetAllIncomeCurrency();


        #endregion

        #region IncomeMemorep

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeMemorep UpdateIncomeMemorep(IncomeMemorep incomeMemorep);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeMemorep(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeMemorep GetIncomeMemorep(int ID);

        [OperationContract]
        IncomeMemorep[] GetAllIncomeMemorep();


        #endregion

        #region IncomeSplitPoolsRate

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeSplitPoolsRate UpdateIncomeSplitPoolsRate(IncomeSplitPoolsRate incomeSplitPoolsRate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeSplitPoolsRate(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeSplitPoolsRate GetIncomeSplitPoolsRate(int ID);

        [OperationContract]
        IncomeSplitPoolsRate[] GetAllIncomeSplitPoolsRate();


        #endregion

        #region IncomeAccountsUnit

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeAccountsUnit UpdateIncomeAccountsUnit(IncomeAccountsUnit incomeAccountsUnit);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeAccountsUnit(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeAccountsUnit GetIncomeAccountsUnit(int ID);

        [OperationContract]
        IncomeAccountsUnit[] GetAllIncomeAccountsUnit();


        #endregion

        #region Team Structure ALL

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        TeamStructureALL UpdateTeamStructureALL(TeamStructureALL TeamStructureALL);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTeamStructureALL(int Team_StructureId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        TeamStructureALL GetTeamStructureALL(int Team_StructureId);

        [OperationContract]
        TeamStructureALL[] GetAllTeamStructureALL();

        [OperationContract]
        TeamStructureALL[] GetTeamStructureALLUsingParams(string SearchValue, int year);

        [OperationContract]
        TeamStructureALL[] TeamStructureALLByParameters(string selectedDefinitionCode, string SearchValue, int year);

        [OperationContract]
        TeamStructureALL[] GetTeamStructureALLByParamsAndeSetUp(string code, string SearchValue);

        [OperationContract]
        TeamStructureALL[] GetTeamStructureALLUsingSetUp();

        [OperationContract]
        TeamStructureALL[] GetTeamStructureALLUsingDefinitionCode(string code);

        [OperationContract]
        TeamStructureALL[] GetTeamStructureALLUsingDefinitionCodeMonthly(string code);

        [OperationContract]
        TeamStructureALL[] GetTeamStructureALLByParamsAndeSetUpMonthly(string code, string SearchValue);
        [OperationContract]
        TeamStructureALL[] GetTeamStructureALLUsingSetUpMonthly();

        //[OperationContract]
        //TeamStructureALL GetTeamStructureALLTop1(string branch, string year);

        [OperationContract]
        TeamStructureALL GetTeamStructureALLTop1(string branch, string defcode, int year);


        #endregion

        #region IncomeFintrakAccountsSegment

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeFintrakAccountsSegment UpdateIncomeFintrakAccountsSegment(IncomeFintrakAccountsSegment ifas);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeFintrakAccountsSegment(int Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeFintrakAccountsSegment GetIncomeFintrakAccountsSegment(int Id);

        [OperationContract]
        IncomeFintrakAccountsSegment[] GetAllIncomeFintrakAccountsSegment();

        [OperationContract]
        IncomeFintrakAccountsSegment[] GetAccountsSegmentByCustomerIdBank(string customerid, string bank);


        #endregion

        #region FTPRiskRatings

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FTPRiskRatings UpdateFTPRiskRatings(FTPRiskRatings fTPRiskRatings);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteFTPRiskRatings(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FTPRiskRatings GetFTPRiskRatings(int ID);

        [OperationContract]
        FTPRiskRatings[] GetAllFTPRiskRatings();

        [OperationContract]
        FTPRiskRatings[] GetFTPRiskRatingsUsingSearchValue(string searchvalue);

        #endregion

        #region IncomeCaptionPoolRate

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeCaptionPoolRate UpdateIncomeCaptionPoolRate(IncomeCaptionPoolRate incomeCaptionPoolRate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeCaptionPoolRate(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeCaptionPoolRate GetIncomeCaptionPoolRate(int ID);

        [OperationContract]
        IncomeCaptionPoolRate[] GetAllIncomeCaptionPoolRate();

        [OperationContract]
        IncomeCaptionPoolRate[] GetIncomeCaptionPoolRateUsingSearchValue(string searchvalue);

        #endregion

        #region IncomeAccountsTreeAccountTEMP

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeAccountsTreeAccountTEMP UpdateIncomeAccountsTreeAccountTEMP(IncomeAccountsTreeAccountTEMP iatat);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeAccountsTreeAccountTEMP(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeAccountsTreeAccountTEMP GetIncomeAccountsTreeAccountTEMP(int ID);

        [OperationContract]
        IncomeAccountsTreeAccountTEMP[] GetAllIncomeAccountsTreeAccountTEMP();

        [OperationContract]
        IncomeAccountsTreeAccountTEMP[] GetIncomeAccountsTreeAccountTEMPBySearchVal(string search);
                                        
        #endregion

        #region IncomeAccountsTreeMisCodesTEMP

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeAccountsTreeMisCodesTEMP UpdateIncomeAccountsTreeMisCodesTEMP(IncomeAccountsTreeMisCodesTEMP iatat);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeAccountsTreeMisCodesTEMP(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeAccountsTreeMisCodesTEMP GetIncomeAccountsTreeMisCodesTEMP(int ID);

        [OperationContract]
        IncomeAccountsTreeMisCodesTEMP[] GetAllIncomeAccountsTreeMisCodesTEMP();

        [OperationContract]
        IncomeAccountsTreeMisCodesTEMP[] GetIncomeAccountsTreeMisCodesTEMPBySearchVal(string search);

        #endregion

        #region OneBankAO

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OneBankAO UpdateOneBankAO(OneBankAO onebank);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOneBankAO(int Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OneBankAO GetOneBankAO(int Id);

        [OperationContract]
        OneBankAO[] GetAllOneBankAO();

        [OperationContract]
        OneBankAO[] GetOneBankAOByParams(string SearchValue, int year, int period);

        #endregion

        #region OneBankBranch

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OneBankBranch UpdateOneBankBranch(OneBankBranch onebank);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOneBankBranch(int Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OneBankBranch GetOneBankBranch(int Id);

        [OperationContract]
        OneBankBranch[] GetAllOneBankBranch();

        [OperationContract]
        OneBankBranch[] GetOneBankBranchByParams(string SearchValue, int year, int period);

        #endregion

        #region OneBankRegionTD

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OneBankRegionTD UpdateOneBankRegionTD(OneBankRegionTD oneBankRegionTD);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOneBankRegionTD(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OneBankRegionTD GetOneBankRegionTD(int ID);

        [OperationContract]
        OneBankRegionTD[] GetAllOneBankRegionTD();

        #endregion

        #region OneBankTeamTable

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OneBankTeamTable UpdateOneBankTeamTable(OneBankTeamTable oneBankTeamTable);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOneBankTeamTable(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OneBankTeamTable GetOneBankTeamTable(int ID);

        [OperationContract]
        OneBankTeamTable[] GetAllOneBankTeamTable();

        #endregion

        #region MprInterestMappings

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MprInterestMapping UpdateMprInterestMapping(MprInterestMapping mprInterestMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMprInterestMapping(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MprInterestMapping GetMprInterestMapping(int ID);

        [OperationContract]
        MprInterestMapping[] GetAllMprInterestMapping();

        #endregion

        #region MISNewOthers TEMP

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MISNewOthersTEMP UpdateMISNewOthersTEMP(MISNewOthersTEMP iatat);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMISNewOthersTEMP(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MISNewOthersTEMP GetMISNewOthersTEMP(int ID);

        [OperationContract]
        MISNewOthersTEMP[] GetAllMISNewOthersTEMP();

        [OperationContract]
        MISNewOthersTEMP[] GetMISNewOthersTEMPBySearchVal(string search);

        #endregion

        #region IncomeCustomerPoolRate

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeCustomerPoolRate UpdateIncomeCustomerPoolRate(IncomeCustomerPoolRate incomecustomerpoolrate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeCustomerPoolRate(int Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeCustomerPoolRate GetIncomeCustomerPoolRate(int Id);

        [OperationContract]
        IncomeCustomerPoolRate[] GetAllIncomeCustomerPoolRate();

        [OperationContract]
        IncomeCustomerPoolRate[] GetIncomeCustomerPoolRateUsingSearchValue(string searchvalue);

        #endregion

        #region IncomeAccountPoolRate

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeAccountPoolRate UpdateIncomeAccountPoolRate(IncomeAccountPoolRate incomeAccountpoolrate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeAccountPoolRate(int Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeAccountPoolRate GetIncomeAccountPoolRate(int Id);

        [OperationContract]
        IncomeAccountPoolRate[] GetAllIncomeAccountPoolRate();

        [OperationContract]
        IncomeAccountPoolRate[] GetIncomeAccountPoolRateUsingSearchValue(string searchvalue);

        #endregion

    }
}
