using System;
using System.Linq;
using System.ServiceModel;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Business.IFRS.Contracts
{
    [ServiceContract]
    public interface IIFRS9Service : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();

        #region ExternalRating

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ExternalRating UpdateExternalRating(ExternalRating externalRating);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteExternalRating(int externalRatingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ExternalRating GetExternalRating(int externalRatingId);

        [OperationContract]
        ExternalRating[] GetAllExternalRatings();

        #endregion

        #region HistoricalSectorRating

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        HistoricalSectorRating UpdateHistoricalSectorRating(HistoricalSectorRating historicalSectorRating);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteHistoricalSectorRating(int historicalSectorRatingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        HistoricalSectorRating GetHistoricalSectorRating(int historicalSectorRatingId);

        [OperationContract]
        HistoricalSectorRating[] GetAllHistoricalSectorRatings();

        #endregion

        #region InternalRatingBased

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        InternalRatingBased UpdateInternalRatingBased(InternalRatingBased internalRatingBased);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteInternalRatingBased(int internalRatingBasedId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        InternalRatingBased GetInternalRatingBased(int internalRatingBasedId);

        [OperationContract]
        InternalRatingBased[] GetAllInternalRatingBaseds();

        #endregion

        #region MacroEconomic

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MacroEconomic UpdateMacroEconomic(MacroEconomic macroEconomic);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMacroEconomic(int macroEconomicId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MacroEconomic GetMacroEconomic(int macroEconomicId);

        [OperationContract]
        MacroEconomic[] GetAllMacroEconomics();


        #endregion

        #region RatingMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        RatingMapping UpdateRatingMapping(RatingMapping ratingMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteRatingMapping(int ratingMappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        RatingMapping GetRatingMapping(int ratingMappingId);

        [OperationContract]
        RatingMapping[] GetAllRatingMappings();

        [OperationContract]
        RatingMappingData[] GetRatingMappings();

        #endregion

        #region Transition

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Transition UpdateTransition(Transition transition);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteTransition(int transitionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Transition GetTransition(int transitionId);

        [OperationContract]
        Transition[] GetAllTransitions();

        #endregion

        #region Sector

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Sector UpdateSector(Sector sector);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSector(int sectorId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Sector GetSector(int sectorId);

        [OperationContract]
        Sector[] GetAllSectors();

        [OperationContract]
        Sector[] GetSectorBySource(string Source);

        #endregion

        #region HistoricalClassification

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        HistoricalClassification UpdateHistoricalClassification(HistoricalClassification historicalClassification);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteHistoricalClassification(int historicalClassificationId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        HistoricalClassification GetHistoricalClassification(int historicalClassificationId);

        [OperationContract]
        HistoricalClassification[] GetAllHistoricalClassifications();

        #endregion

        #region MacroEconomicHistorical

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MacroEconomicHistorical UpdateMacroEconomicHistorical(MacroEconomicHistorical macroEconomicHistorical);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMacroEconomicHistorical(int macroEconomicHistoricalId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MacroEconomicHistorical GetMacroEconomicHistorical(int macroEconomicHistoricalId);

        [OperationContract]
        MacroEconomicHistoricalData[] GetAllMacroEconomicHistoricals();

        #endregion

        #region NotchDifference

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        NotchDifference UpdateNotchDifference(NotchDifference notchDifference);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteNotchDifference(int notchDifferenceId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        NotchDifference GetNotchDifference(int notchDifferenceId);

        [OperationContract]
        NotchDifference[] GetAllNotchDifferences();

        #endregion

        #region SetUp

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SetUp UpdateSetUp(SetUp setUp);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSetUp(int setUpId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SetUp GetSetUp(int setUpId);

        [OperationContract]
        SetUp[] GetAllSetUps();

        #endregion

        #region HistoricalSectorialPD

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        HistoricalSectorialPD UpdateHistoricalSectorialPD(HistoricalSectorialPD historicalClassification);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteHistoricalSectorialPD(int historicalClassificationId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        HistoricalSectorialPD GetHistoricalSectorialPD(int historicalClassificationId);

        [OperationContract]
        HistoricalSectorialPD[] GetAllHistoricalSectorialPDs();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        string[] GetDistinctLYear();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        string[] GetDistinctLPeriod();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void ComputeHistoricalSectorialPD(int computationType, int curYear, int curPeriod, int prevYear, int prevPeriod);

        #endregion

        #region HistoricalSectorialLGD

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        HistoricalSectorialLGD UpdateHistoricalSectorialLGD(HistoricalSectorialLGD historicalClassification);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteHistoricalSectorialLGD(int historicalClassificationId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        HistoricalSectorialLGD GetHistoricalSectorialLGD(int historicalClassificationId);

        [OperationContract]
        HistoricalSectorialLGD[] GetAllHistoricalSectorialLGDs();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        string[] GetDistinctYear();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        string[] GetDistinctPeriod();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void ComputeHistoricalSectorialLGD(int computationType, int curYear, int curPeriod, int prevYear, int prevPeriod);

        #endregion

        //#region ComputedForcastedPDLGD

        //[OperationContract]
        //[TransactionFlow(TransactionFlowOption.Allowed)]
        //ComputedForcastedPDLGD UpdateComputedForcastedPDLGD(ComputedForcastedPDLGD computedForcastedPDLGD);

        //[OperationContract]
        //[TransactionFlow(TransactionFlowOption.Allowed)]
        //void DeleteComputedForcastedPDLGD(int computedPDId);

        //[OperationContract]
        //[FaultContract(typeof(NotFoundException))]
        //ComputedForcastedPDLGD GetComputedForcastedPDLGD(int computedPDId);

        //[OperationContract]
        //ComputedForcastedPDLGD[] GetAllComputedForcastedPDLGDs();

        //#endregion

        #region SectorialRegressedPD

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SectorialRegressedPD UpdateSectorialRegressedPD(SectorialRegressedPD sectorialRegressedPD);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSectorialRegressedPD(int sectorialRegressedPDId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SectorialRegressedPD GetSectorialRegressedPD(int sectorialRegressedPDId);

        [OperationContract]
        SectorialRegressedPD[] GetAllSectorialRegressedPDs();

        #endregion

        #region SectorialRegressedLGD

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SectorialRegressedLGD UpdateSectorialRegressedLGD(SectorialRegressedLGD sectorialRegressedLGD);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSectorialRegressedLGD(int sectorialRegressedLGDId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SectorialRegressedLGD GetSectorialRegressedLGD(int sectorialRegressedLGDId);

        [OperationContract]
        SectorialRegressedLGD[] GetAllSectorialRegressedLGDs();

        #endregion

        #region MacroEconomicVariable

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MacroEconomicVariable UpdateMacroEconomicVariable(MacroEconomicVariable macroEconomicVariable);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMacroEconomicVariable(int macroEconomicVariableId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MacroEconomicVariable GetMacroEconomicVariable(int macroEconomicVariableId);

        [OperationContract]
        MacroEconomicVariable[] GetAllMacroEconomicVariables();


        #endregion

        #region SectorVariableMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SectorVariableMapping UpdateSectorVariableMapping(SectorVariableMapping sectorVariableMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSectorVariableMapping(int sectorVariableMappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SectorVariableMapping GetSectorVariableMapping(int sectorVariableMappingId);

        [OperationContract]
        SectorVariableMappingData[] GetAllSectorVariableMappings();


        #endregion

        #region PitFormular

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PitFormular UpdatePitFormular(PitFormular pitFormular);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePitFormular(int pitFormularId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        PitFormular GetPitFormular(int pitFormularId);

        [OperationContract]
        PitFormular[] GetAllPitFormulars();

        #endregion

        #region PortfolioExposure

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PortfolioExposure UpdatePortfolioExposure(PortfolioExposure sector);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePortfolioExposure(int portfolioId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        PortfolioExposure GetPortfolioExposure(int portfolioId);

        [OperationContract]
        PortfolioExposure[] GetAllPortfolioExposures();

        [OperationContract]
        string GetAllPortfolioExposuresChart();

        #endregion

        #region SectorialExposure

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SectorialExposure UpdateSectorialExposure(SectorialExposure sectorialExposure);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSectorialExposure(int sectorialExposureId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SectorialExposure GetSectorialExposure(int sectorialExposureId);

        [OperationContract]
        SectorialExposure[] GetAllSectorialExposures();

        [OperationContract]
        string GetAllSectorialExposuresChart();
        #endregion

        #region PiTPD

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PiTPD UpdatePiTPD(PiTPD pitFormular);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePiTPD(int pitPdId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        PiTPD GetPiTPD(int pitPdId);

        [OperationContract]
        PiTPD[] GetAllPiTPDs();
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegressPD();

        #endregion

        #region EclCalculationModel

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        EclCalculationModel UpdateEclCalculationModel(EclCalculationModel eclCalculationModel);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteEclCalculationModel(int eclCalculationModelId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        EclCalculationModel GetEclCalculationModel(int eclCalculationModelId);

        [OperationContract]
        EclCalculationModel[] GetAllEclCalculationModels();

        #endregion

        #region LoanBucketDistribution

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        LoanBucketDistribution UpdateLoanBucketDistribution(LoanBucketDistribution sector);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteLoanBucketDistribution(int macroeconomicVDisplayId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LoanBucketDistribution GetLoanBucketDistribution(int macroeconomicVDisplayId);

        [OperationContract]
        LoanBucketDistribution[] GetAllLoanBucketDistributions();


        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void PDDistribution();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void PastDueDayDistribution();

        [OperationContract]
        LoanBucketDistribution[] GetLoanAssessments(string bucket);

        //[OperationContract]
        //LoanBucketDistribution[] GetAllUnderPerformingLoans();
        //[OperationContract]
        //LoanBucketDistribution[] GetAllNonPerformingLoans();

        #endregion

        #region MacroeconomicVDisplay

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MacroeconomicVDisplay UpdateMacroeconomicVDisplay(MacroeconomicVDisplay macroeconomicVDisplay);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMacroeconomicVDisplay(int macroeconomicVDisplayId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MacroeconomicVDisplay GetMacroeconomicVDisplay(int macroeconomicVDisplayId);

        [OperationContract]
        MacroeconomicVDisplay[] GetAllMacroeconomicVDisplays();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        string[] GetDistinctFHYear(string vType);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MacroeconomicVDisplay[] GetMacroeconomicVDisplayByYear(int yr);

        #endregion

        #region LifeTimePDClassification

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        LifeTimePDClassification UpdateLifeTimePDClassification(LifeTimePDClassification lifeTimePDClassification);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteLifeTimePDClassification(int lifeTimePDClassificationId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LifeTimePDClassification GetLifeTimePDClassification(int lifeTimePDClassificationId);

        [OperationContract]
        LifeTimePDClassification[] GetAllLifeTimePDClassifications();

        #endregion

        #region SummaryReport

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SummaryReport UpdateSummaryReport(SummaryReport summaryReport);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSummaryReport(int summaryReportId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SummaryReport GetSummaryReport(int summaryReportId);

        [OperationContract]
        SummaryReport[] GetAllSummaryReports();

        [OperationContract]
        string GetAllSummaryReportsChart();

        #endregion

        #region IfrsEquityUnqouted

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IfrsEquityUnqouted UpdateIfrsEquityUnqouted(IfrsEquityUnqouted ifrsEquityUnqouted);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIfrsEquityUnqouted(int ifrsEquityUnqoutedId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IfrsEquityUnqouted GetIfrsEquityUnqouted(int ifrsEquityUnqoutedId);

        [OperationContract]
        IfrsEquityUnqouted[] GetAllIfrsEquityUnqouteds();

        #endregion

        #region IfrsStocksPrimaryData

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IfrsStocksPrimaryData UpdateIfrsStocksPrimaryData(IfrsStocksPrimaryData ifrsStocksPrimaryData);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIfrsStocksPrimaryData(int ifrsStocksPrimaryDataId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IfrsStocksPrimaryData GetIfrsStocksPrimaryData(int ifrsStocksPrimaryDataId);

        [OperationContract]
        IfrsStocksPrimaryData[] GetAllIfrsStocksPrimaryDatas();

        #endregion

        #region IfrsStocksMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IfrsStocksMapping UpdateIfrsStocksMapping(IfrsStocksMapping ifrsStocksMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIfrsStocksMapping(int ifrsStocksMappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IfrsStocksMapping GetIfrsStocksMapping(int ifrsStocksMappingId);

        [OperationContract]
        IfrsStocksMappingData[] GetAllIfrsStocksMappings();

        #endregion

        #region Reconciliation

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Reconciliation UpdateReconciliation(Reconciliation reconciliation);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteReconciliation(int reconciliationId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Reconciliation GetReconciliation(int reconciliationId);

        [OperationContract]
        Reconciliation[] GetAllReconciliations();

        #endregion

        #region ForecastedMacroeconimcsSensitivity

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ForecastedMacroeconimcsSensitivity UpdateForecastedMacroeconimcsSensitivity(ForecastedMacroeconimcsSensitivity forecastedMacroeconimcsSensitivity);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteForecastedMacroeconimcsSensitivity(int forecastedMacroeconimcsSensitivityId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ForecastedMacroeconimcsSensitivity GetForecastedMacroeconimcsSensitivity(int forecastedMacroeconimcsSensitivityId);

        [OperationContract]
        ForecastedMacroeconimcsSensitivityData[] GetAllForecastedMacroeconimcsSensitivitys();

        [OperationContract]
        void InsertSensitivityData(string microeconomic, int year, int types, float values);

        [OperationContract]
        void ComputeSensitivity();


        #endregion

        #region FairValuationModel

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        FairValuationModel UpdateFairValuationModel(FairValuationModel fairValuationModel);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteFairValuationModel(int fairValuationModelId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        FairValuationModel GetFairValuationModel(int fairValuationModelId);

        [OperationContract]
        FairValuationModel[] GetAllFairValuationModels();

        #endregion

        #region BucketExposure

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        BucketExposure UpdateBucketExposure(BucketExposure sectorialExposure);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBucketExposure(int sectorialExposureId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BucketExposure GetBucketExposure(int sectorialExposureId);

        [OperationContract]
        BucketExposure[] GetAllBucketExposures();

        [OperationContract]
        string GetAllBucketExposuresChart();
        #endregion

        #region ForecastedMacroeconimcsScenario

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ForecastedMacroeconimcsScenario UpdateForecastedMacroeconimcsScenario(ForecastedMacroeconimcsScenario forecastedMacroeconimcsScenario);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteForecastedMacroeconimcsScenario(int forecastedMacroeconimcsScenarioId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ForecastedMacroeconimcsScenario GetForecastedMacroeconimcsScenario(int forecastedMacroeconimcsScenarioId);

        [OperationContract]
        ForecastedMacroeconimcsScenarioData[] GetAllForecastedMacroeconimcsScenarios();

        [OperationContract]
        void InsertScenarioData(string sector, string microeconomic, int year, int types, float values);

        [OperationContract]
        void ComputeScenario();


        #endregion

        #region LoanSpreadScenario

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        LoanSpreadScenario UpdateLoanSpreadScenario(LoanSpreadScenario loanSpreadScenario);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteLoanSpreadScenario(int loanSpreadScenarioId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LoanSpreadScenario GetLoanSpreadScenario(int loanSpreadScenarioId);

        [OperationContract]
        LoanSpreadScenario[] GetAllLoanSpreadScenarios();


        //[OperationContract]
        //LoanSpreadScenario[] GetLoanAssessments(string bucket);

        //[OperationContract]
        //LoanSpreadScenario[] GetAllUnderPerformingLoans();
        //[OperationContract]
        //LoanSpreadScenario[] GetAllNonPerformingLoans();

        #endregion

        #region LoanSpreadSensitivity

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        LoanSpreadSensitivity UpdateLoanSpreadSensitivity(LoanSpreadSensitivity loanSpreadSensitivity);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteLoanSpreadSensitivity(int loanSpreadSensitivityId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LoanSpreadSensitivity GetLoanSpreadSensitivity(int loanSpreadSensitivityId);

        [OperationContract]
        LoanSpreadSensitivity[] GetAllLoanSpreadSensitivitys();


        //[OperationContract]
        //LoanSpreadSensitivity[] GetLoanAssessments(string bucket);

        //[OperationContract]
        //LoanSpreadSensitivity[] GetAllUnderPerformingLoans();
        //[OperationContract]
        //LoanSpreadSensitivity[] GetAllNonPerformingLoans();

        #endregion

        #region UnquotedEquityFairvalueResult

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        UnquotedEquityFairvalueResult UpdateUnquotedEquityFairvalueResult(UnquotedEquityFairvalueResult unquotedEquityFairvalueResult);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteUnquotedEquityFairvalueResult(int unquotedEquityFairvalueResultId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        UnquotedEquityFairvalueResult GetUnquotedEquityFairvalueResult(int unquotedEquityFairvalueResultId);

        [OperationContract]
        UnquotedEquityFairvalueResult[] GetAllUnquotedEquityFairvalueResults();

        #endregion

        #region PiTPDComparism

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PiTPDComparism UpdatePiTPDComparism(PiTPDComparism piTPDComparism);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePiTPDComparism(int comparismPDId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        PiTPDComparism GetPiTPDComparism(int comparismPDId);

        [OperationContract]
        PiTPDComparism[] GetAllPiTPDComparisms();

        #endregion

        #region MarkovMatrix

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MarkovMatrix UpdateMarkovMatrix(MarkovMatrix markovMatrix);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMarkovMatrix(int markovMatrixId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MarkovMatrix GetMarkovMatrix(int markovMatrixId);

        [OperationContract]
        MarkovMatrix[] GetAllMarkovMatrixs();

        [OperationContract]
        MarkovMatrixData[] GetMarkovMatrixs();

        #endregion

        #region CCFModelling

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        CCFModelling UpdateCCFModelling(CCFModelling ccfModelling);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCCFModelling(int ccfModellingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        CCFModelling GetCCFModelling(int ccfModellingId);

        [OperationContract]
        CCFModelling[] GetAllCCFModellings();

        #endregion

       #region ECLComparism

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ECLComparism UpdateECLComparism(ECLComparism eclComparism);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteECLComparism(int eclComparismId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ECLComparism GetECLComparism(int eclComparismId);

        [OperationContract]
        ECLComparism[] GetAllECLComparisms();

        #endregion

       #region ForeignEADExchangeRate

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ForeignEADExchangeRate UpdateForeignEADExchangeRate(ForeignEADExchangeRate foreignEADexchangeRate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteForeignEADExchangeRate(int foreignEADExchangeRateId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ForeignEADExchangeRate GetForeignEADExchangeRate(int foreignEADExchangeRateId);

        [OperationContract]
        ForeignEADExchangeRate[] GetAllForeignEADExchangeRates();
 

        #endregion

        //Begin Victor Segment

        //GetBondEclComputationResults

        #region EuroBondSpread

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        EuroBondSpread UpdateEuroBondSpread(EuroBondSpread euroBondSpread);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteEuroBondSpread(int Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        EuroBondSpread GetEuroBondSpread(int Id);

        [OperationContract]
        EuroBondSpread[] GetAllEuroBondSpreads();

        #endregion

        #region LcBgEclComputationResult

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        LcBgEclComputationResult UpdateLcBgEclComputationResult(LcBgEclComputationResult lcBgEclComputationResult);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteLcBgEclComputationResult(int Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LcBgEclComputationResult GetLcBgEclComputationResult(int Id);

        [OperationContract]
        LcBgEclComputationResult[] GetAllLcBgEclComputationResults();

        #endregion

        #region PlacementEclComputationResult

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PlacementEclComputationResult UpdatePlacementEclComputationResult(PlacementEclComputationResult placementEclComputationResult);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePlacementEclComputationResult(int Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        PlacementEclComputationResult GetPlacementEclComputationResult(int Id);

        [OperationContract]
        PlacementEclComputationResult[] GetAllPlacementEclComputationResults();

        #endregion

        #region BondEclComputationResult

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        BondEclComputationResult UpdateBondEclComputationResult(BondEclComputationResult bondEclComputationResult);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBondEclComputationResult(int Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BondEclComputationResult GetBondEclComputationResult(int Id);

        [OperationContract]
        BondEclComputationResult[] GetAllBondEclComputationResults();

        #endregion

        #region EclComputationResult

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        EclComputationResult UpdateEclComputationResult(EclComputationResult eclComputationResult);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteEclComputationResult(int Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        EclComputationResult GetEclComputationResult(int Id);

        [OperationContract]
        EclComputationResult[] GetAllEclComputationResults();

        #endregion

        #region PlacementComputationResult

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PlacementComputationResult UpdatePlacementComputationResult(PlacementComputationResult placementComputationResult);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePlacementComputationResult(int Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        PlacementComputationResult GetPlacementComputationResult(int Id);

        [OperationContract]
        PlacementComputationResult[] GetAllPlacementComputationResults();

        #endregion

        #region LgdComputationResult

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        LgdComputationResult UpdateLgdComputationResult(LgdComputationResult lgdComputationResult);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteLgdComputationResult(int Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LgdComputationResult GetLgdComputationResult(int Id);

        [OperationContract]
        LgdComputationResult[] GetAllLgdComputationResults();

        #endregion

        #region MarginalPDDistributionPlacement

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MarginalPDDistributionPlacement UpdateMarginalPDDistributionPlacement(MarginalPDDistributionPlacement marginalPDDistributionPlacement);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMarginalPDDistributionPlacement(int Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MarginalPDDistributionPlacement GetMarginalPDDistributionPlacement(int Id);

        [OperationContract]
        MarginalPDDistributionPlacement[] GetAllMarginalPDDistributionPlacements();

        #endregion

        #region MarginalPDDistributionPlacement

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        BondMarginalPDDistribution UpdateBondMarginalPDDistribution(BondMarginalPDDistribution bondMarginalPDDistribution);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBondMarginalPDDistribution(int Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        BondMarginalPDDistribution GetBondMarginalPDDistribution(int Id);

        [OperationContract]
        BondMarginalPDDistribution[] GetAllBondMarginalPDDistributions();

        #endregion

        #region BondMarginalPDDistribution

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MarginalPDDistribution UpdateMarginalPDDistribution(MarginalPDDistribution marginalPDDistribution);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMarginalPDDistribution(int Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MarginalPDDistribution GetMarginalPDDistribution(int Id);

        [OperationContract]
        MarginalPDDistribution[] GetAllMarginalPDDistributions();

        #endregion

        #region LocalBondSpread

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        LocalBondSpread UpdateLocalBondSpread(LocalBondSpread localBondSpread);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteLocalBondSpread(int Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LocalBondSpread GetLocalBondSpread(int Id);

        [OperationContract]
        LocalBondSpread[] GetAllLocalBondSpreads();

        #endregion


        //End Victor Segment



        #region ComputedForcastedPDLGD

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ComputedForcastedPDLGD UpdateComputedForcastedPDLGD(ComputedForcastedPDLGD computedForcastedPDLGD);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteComputedForcastedPDLGD(int computedPDId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ComputedForcastedPDLGD GetComputedForcastedPDLGD(int computedPDId);

        [OperationContract]
        ComputedForcastedPDLGD[] GetAllComputedForcastedPDLGDs();

        [OperationContract]
        ComputedForcastedPDLGD[] GetListComputedForcastedPDLGDs();

        #endregion

        #region ComputedForcastedPDLGDForeign

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ComputedForcastedPDLGDForeign UpdateComputedForcastedPDLGDForeign(ComputedForcastedPDLGDForeign computedForcastedPDLGD);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteComputedForcastedPDLGDForeign(int computedPDId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ComputedForcastedPDLGDForeign GetComputedForcastedPDLGDForeign(int computedPDId);

        [OperationContract]
        ComputedForcastedPDLGDForeign[] GetAllComputedForcastedPDLGDForeigns();

        [OperationContract]
        ComputedForcastedPDLGDForeign[] GetListComputedForcastedPDLGDForeigns();

        #endregion

        #region MacroEconomicsNPL

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MacroEconomicsNPL UpdateMacroEconomicsNPL(MacroEconomicsNPL macroEconomicsNPL);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMacroEconomicsNPL(int macroeconomicnplId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MacroEconomicsNPL GetMacroEconomicsNPL(int macroeconomicnplId);

        [OperationContract]
        MacroEconomicsNPL[] GetAllMacroEconomicsNPLs();

        #endregion

        #region MonthlyDiscountFactor

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MonthlyDiscountFactor UpdateMonthlyDiscountFactor(MonthlyDiscountFactor monthlyDiscountFactor);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMonthlyDiscountFactor(int MonthlyDiscountFactor_Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MonthlyDiscountFactor GetMonthlyDiscountFactor(int MonthlyDiscountFactor_Id);

        [OperationContract]
        MonthlyDiscountFactor[] GetAllMonthlyDiscountFactors();

        [OperationContract]
        MonthlyDiscountFactor[] GetMonthlyDiscountFactorByRefNo(string RefNo);

        #endregion

        #region MonthlyDiscountFactorBond

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MonthlyDiscountFactorBond UpdateMonthlyDiscountFactorBond(MonthlyDiscountFactorBond monthlyDiscountFactorBond);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMonthlyDiscountFactorBond(int MonthlyDiscountFactorBond_Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MonthlyDiscountFactorBond GetMonthlyDiscountFactorBond(int MonthlyDiscountFactorBond_Id);

        [OperationContract]
        MonthlyDiscountFactorBond[] GetAllMonthlyDiscountFactorBonds();

        [OperationContract]
        MonthlyDiscountFactorBond[] GetMonthlyDiscountFactorBondByRefNo(string RefNo);

        #endregion

        #region MonthlyDiscountFactorPlacement

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MonthlyDiscountFactorPlacement UpdateMonthlyDiscountFactorPlacement(MonthlyDiscountFactorPlacement monthlyDiscountFactorPlacement);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMonthlyDiscountFactorPlacement(int MonthlyDiscountFactorPlacement_Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MonthlyDiscountFactorPlacement GetMonthlyDiscountFactorPlacement(int MonthlyDiscountFactorPlacement_Id);

        [OperationContract]
        MonthlyDiscountFactorPlacement[] GetAllMonthlyDiscountFactorPlacements();

        [OperationContract]
        MonthlyDiscountFactorPlacement[] GetMonthlyDiscountFactorPlacementByRefNo(string RefNo);

        #endregion

        #region ProbabilityWeighted

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ProbabilityWeighted UpdateProbabilityWeighted(ProbabilityWeighted probabilityWeighted);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteProbabilityWeighted(int ProbabilityWeighted_Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        ProbabilityWeighted GetProbabilityWeighted(int ProbabilityWeighted_Id);

        [OperationContract]
        ProbabilityWeighted[] GetAllProbabilityWeighteds();

        [OperationContract]
        ProbabilityWeighted[] GetProbabilityWeightedByInstrumentType(string InstrumentType);

        #endregion

        #region MacrovariableEstimate

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        MacrovariableEstimate UpdateMacrovariableEstimate(MacrovariableEstimate macrovariableEstimate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteMacrovariableEstimate(int MacrovariableEstimate_Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        MacrovariableEstimate GetMacrovariableEstimate(int MacrovariableEstimate_Id);

        [OperationContract]
        MacrovariableEstimate[] GetAllMacrovariableEstimates();

        [OperationContract]
        MacrovariableEstimate[] GetMacrovariableEstimateByCategory(string Category);

        #endregion

        #region SectorMapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        SectorMapping UpdateSectorMapping(SectorMapping sectorMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteSectorMapping(int SectorMapping_Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        SectorMapping GetSectorMapping(int SectorMapping_Id);

        [OperationContract]
        SectorMapping[] GetAllSectorMappings();

        //[OperationContract]
        //SectorMapping[] GetSectorMappingBySource(string Source);

        #endregion

        #region InvestmentOthersECL

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        InvestmentOthersECL UpdateInvestmentOthersECL(InvestmentOthersECL sectorMapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteInvestmentOthersECL(int ecl_Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        InvestmentOthersECL GetInvestmentOthersECL(int ecl_Id);

        [OperationContract]
        InvestmentOthersECL[] GetAllInvestmentOthersECLs();

        [OperationContract]
        InvestmentOthersECL[] GetInvestmentOthersECLByRefNo(string RefNo);

        #endregion

    }
}