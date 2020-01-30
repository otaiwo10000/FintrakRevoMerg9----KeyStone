using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.IFRS.Proxies
{
    [Export(typeof(IIFRS9Service))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IFRS9Client : UserClientBase<IIFRS9Service>, IIFRS9Service
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }

        #region ExternalRating

        public ExternalRating UpdateExternalRating(ExternalRating externalRating)
        {
            return Channel.UpdateExternalRating(externalRating);
        }

        public void DeleteExternalRating(int externalRatingId)
        {
            Channel.DeleteExternalRating(externalRatingId);
        }

        public ExternalRating GetExternalRating(int externalRatingId)
        {
            return Channel.GetExternalRating(externalRatingId);
        }

        public ExternalRating[] GetAllExternalRatings()
        {
            return Channel.GetAllExternalRatings();
        }


        #endregion

        #region HistoricalSectorRating

        public HistoricalSectorRating UpdateHistoricalSectorRating(HistoricalSectorRating historicalSectorRating)
        {
            return Channel.UpdateHistoricalSectorRating(historicalSectorRating);
        }

        public void DeleteHistoricalSectorRating(int historicalSectorRatingId)
        {
            Channel.DeleteHistoricalSectorRating(historicalSectorRatingId);
        }

        public HistoricalSectorRating GetHistoricalSectorRating(int historicalSectorRatingId)
        {
            return Channel.GetHistoricalSectorRating(historicalSectorRatingId);
        }

        public HistoricalSectorRating[] GetAllHistoricalSectorRatings()
        {
            return Channel.GetAllHistoricalSectorRatings();
        }


        #endregion

        #region InternalRatingBased

        public InternalRatingBased UpdateInternalRatingBased(InternalRatingBased internalRatingBased)
        {
            return Channel.UpdateInternalRatingBased(internalRatingBased);
        }

        public void DeleteInternalRatingBased(int internalRatingBasedId)
        {
            Channel.DeleteInternalRatingBased(internalRatingBasedId);
        }

        public InternalRatingBased GetInternalRatingBased(int internalRatingBasedId)
        {
            return Channel.GetInternalRatingBased(internalRatingBasedId);
        }

        public InternalRatingBased[] GetAllInternalRatingBaseds()
        {
            return Channel.GetAllInternalRatingBaseds();
        }


        #endregion

        #region MacroEconomic

        public MacroEconomic UpdateMacroEconomic(MacroEconomic macroEconomic)
        {
            return Channel.UpdateMacroEconomic(macroEconomic);
        }

        public void DeleteMacroEconomic(int macroEconomicId)
        {
            Channel.DeleteMacroEconomic(macroEconomicId);
        }

        public MacroEconomic GetMacroEconomic(int macroEconomicId)
        {
            return Channel.GetMacroEconomic(macroEconomicId);
        }

        public MacroEconomic[] GetAllMacroEconomics()
        {
            return Channel.GetAllMacroEconomics();
        }


        #endregion

        #region RatingMapping

        public RatingMapping UpdateRatingMapping(RatingMapping ratingMapping)
        {
            return Channel.UpdateRatingMapping(ratingMapping);
        }

        public void DeleteRatingMapping(int ratingMappingId)
        {
            Channel.DeleteRatingMapping(ratingMappingId);
        }

        public RatingMapping GetRatingMapping(int ratingMappingId)
        {
            return Channel.GetRatingMapping(ratingMappingId);
        }

        public RatingMapping[] GetAllRatingMappings()
        {
            return Channel.GetAllRatingMappings();
        }

        public RatingMappingData[] GetRatingMappings()
        {
            return Channel.GetRatingMappings();
        }
        #endregion

        #region Transition

        public Transition UpdateTransition(Transition transition)
        {
            return Channel.UpdateTransition(transition);
        }

        public void DeleteTransition(int transitionId)
        {
            Channel.DeleteTransition(transitionId);
        }

        public Transition GetTransition(int transitionId)
        {
            return Channel.GetTransition(transitionId);
        }

        public Transition[] GetAllTransitions()
        {
            return Channel.GetAllTransitions();
        }


        #endregion

        #region Sector

        public Sector UpdateSector(Sector sector)
        {
            return Channel.UpdateSector(sector);
        }

        public void DeleteSector(int sectorId)
        {
            Channel.DeleteSector(sectorId);
        }

        public Sector GetSector(int sectorId)
        {
            return Channel.GetSector(sectorId);
        }

        public Sector[] GetAllSectors()
        {
            return Channel.GetAllSectors();
        }

        public Sector[] GetSectorBySource(string Source)
        {
            return Channel.GetSectorBySource(Source);
        }


        #endregion

        #region HistoricalClassification

        public HistoricalClassification UpdateHistoricalClassification(HistoricalClassification historicalClassification)
        {
            return Channel.UpdateHistoricalClassification(historicalClassification);
        }

        public void DeleteHistoricalClassification(int historicalClassificationId)
        {
            Channel.DeleteHistoricalClassification(historicalClassificationId);
        }

        public HistoricalClassification GetHistoricalClassification(int historicalClassificationId)
        {
            return Channel.GetHistoricalClassification(historicalClassificationId);
        }

        public HistoricalClassification[] GetAllHistoricalClassifications()
        {
            return Channel.GetAllHistoricalClassifications();
        }


        #endregion

        #region MacroEconomicHistorical

        public MacroEconomicHistorical UpdateMacroEconomicHistorical(MacroEconomicHistorical macroEconomicHistorical)
        {
            return Channel.UpdateMacroEconomicHistorical(macroEconomicHistorical);
        }

        public void DeleteMacroEconomicHistorical(int macroEconomicHistoricalId)
        {
            Channel.DeleteMacroEconomicHistorical(macroEconomicHistoricalId);
        }

        public MacroEconomicHistorical GetMacroEconomicHistorical(int macroEconomicHistoricalId)
        {
            return Channel.GetMacroEconomicHistorical(macroEconomicHistoricalId);
        }

        public MacroEconomicHistoricalData[] GetAllMacroEconomicHistoricals()
        {
            return Channel.GetAllMacroEconomicHistoricals();
        }


        #endregion

        #region NotchDifference

        public NotchDifference UpdateNotchDifference(NotchDifference notchDifference)
        {
            return Channel.UpdateNotchDifference(notchDifference);
        }

        public void DeleteNotchDifference(int notchDifferenceId)
        {
            Channel.DeleteNotchDifference(notchDifferenceId);
        }

        public NotchDifference GetNotchDifference(int notchDifferenceId)
        {
            return Channel.GetNotchDifference(notchDifferenceId);
        }

        public NotchDifference[] GetAllNotchDifferences()
        {
            return Channel.GetAllNotchDifferences();
        }


        #endregion

        #region SetUp

        public SetUp UpdateSetUp(SetUp setUp)
        {
            return Channel.UpdateSetUp(setUp);
        }

        public void DeleteSetUp(int setUpId)
        {
            Channel.DeleteSetUp(setUpId);
        }

        public SetUp GetSetUp(int setUpId)
        {
            return Channel.GetSetUp(setUpId);
        }

        public SetUp[] GetAllSetUps()
        {
            return Channel.GetAllSetUps();
        }


        #endregion

        #region HistoricalSectorialPD

        public HistoricalSectorialPD UpdateHistoricalSectorialPD(HistoricalSectorialPD historicalSectorialPD)
        {
            return Channel.UpdateHistoricalSectorialPD(historicalSectorialPD);
        }

        public void DeleteHistoricalSectorialPD(int historicalSectorialPDId)
        {
            Channel.DeleteHistoricalSectorialPD(historicalSectorialPDId);
        }

        public HistoricalSectorialPD GetHistoricalSectorialPD(int historicalSectorialPDId)
        {
            return Channel.GetHistoricalSectorialPD(historicalSectorialPDId);
        }

        public HistoricalSectorialPD[] GetAllHistoricalSectorialPDs()
        {
            return Channel.GetAllHistoricalSectorialPDs();
        }

        public string[] GetDistinctYear()
        {
            return Channel.GetDistinctYear();
        }
        public string[] GetDistinctPeriod()
        {
            return Channel.GetDistinctPeriod();
        }

        public void ComputeHistoricalSectorialPD(int computationType,int curYear, int curPeriod, int prevYear, int prevPeriod)
        {
            Channel.ComputeHistoricalSectorialPD(computationType,curYear, curPeriod, prevYear, prevPeriod);
        }
        #endregion

        #region HistoricalSectorialLGD

        public HistoricalSectorialLGD UpdateHistoricalSectorialLGD(HistoricalSectorialLGD historicalSectorialLGD)
        {
            return Channel.UpdateHistoricalSectorialLGD(historicalSectorialLGD);
        }

        public void DeleteHistoricalSectorialLGD(int historicalSectorialLGDId)
        {
            Channel.DeleteHistoricalSectorialLGD(historicalSectorialLGDId);
        }

        public HistoricalSectorialLGD GetHistoricalSectorialLGD(int historicalSectorialLGDId)
        {
            return Channel.GetHistoricalSectorialLGD(historicalSectorialLGDId);
        }

        public HistoricalSectorialLGD[] GetAllHistoricalSectorialLGDs()
        {
            return Channel.GetAllHistoricalSectorialLGDs();
        }

        public string[] GetDistinctLYear()
        {
            return Channel.GetDistinctYear();
        }
        public string[] GetDistinctLPeriod()
        {
            return Channel.GetDistinctPeriod();
        }

        public void ComputeHistoricalSectorialLGD(int computationType, int curYear, int curPeriod, int prevYear, int prevPeriod)
        {
            Channel.ComputeHistoricalSectorialLGD(computationType, curYear, curPeriod, prevYear, prevPeriod);
        }
        #endregion

        //#region ComputedForcastedPDLGD

        //public ComputedForcastedPDLGD UpdateComputedForcastedPDLGD(ComputedForcastedPDLGD computedForcastedPDLGD)
        //{
        //    return Channel.UpdateComputedForcastedPDLGD(computedForcastedPDLGD);
        //}

        //public void DeleteComputedForcastedPDLGD(int computedPDId)
        //{
        //    Channel.DeleteComputedForcastedPDLGD(computedPDId);
        //}

        //public ComputedForcastedPDLGD GetComputedForcastedPDLGD(int computedPDId)
        //{
        //    return Channel.GetComputedForcastedPDLGD(computedPDId);
        //}

        //public ComputedForcastedPDLGD[] GetAllComputedForcastedPDLGDs()
        //{
        //    return Channel.GetAllComputedForcastedPDLGDs();
        //}


        //#endregion

        #region SectorialRegressedPD

        public SectorialRegressedPD UpdateSectorialRegressedPD(SectorialRegressedPD sectorialRegressedPD)
        {
            return Channel.UpdateSectorialRegressedPD(sectorialRegressedPD);
        }

        public void DeleteSectorialRegressedPD(int sectorialRegressedPDId)
        {
            Channel.DeleteSectorialRegressedPD(sectorialRegressedPDId);
        }

        public SectorialRegressedPD GetSectorialRegressedPD(int sectorialRegressedPDId)
        {
            return Channel.GetSectorialRegressedPD(sectorialRegressedPDId);
        }

        public SectorialRegressedPD[] GetAllSectorialRegressedPDs()
        {
            return Channel.GetAllSectorialRegressedPDs();
        }


        #endregion

        #region SectorialRegressedLGD

        public SectorialRegressedLGD UpdateSectorialRegressedLGD(SectorialRegressedLGD sectorialRegressedLGD)
        {
            return Channel.UpdateSectorialRegressedLGD(sectorialRegressedLGD);
        }

        public void DeleteSectorialRegressedLGD(int sectorialRegressedLGDId)
        {
            Channel.DeleteSectorialRegressedLGD(sectorialRegressedLGDId);
        }

        public SectorialRegressedLGD GetSectorialRegressedLGD(int sectorialRegressedLGDId)
        {
            return Channel.GetSectorialRegressedLGD(sectorialRegressedLGDId);
        }

        public SectorialRegressedLGD[] GetAllSectorialRegressedLGDs()
        {
            return Channel.GetAllSectorialRegressedLGDs();
        }


        #endregion

        #region MacroEconomicVariable

        public MacroEconomicVariable UpdateMacroEconomicVariable(MacroEconomicVariable macroEconomicVariable)
        {
            return Channel.UpdateMacroEconomicVariable(macroEconomicVariable);
        }

        public void DeleteMacroEconomicVariable(int macroEconomicVariableId)
        {
            Channel.DeleteMacroEconomicVariable(macroEconomicVariableId);
        }

        public MacroEconomicVariable GetMacroEconomicVariable(int macroEconomicVariableId)
        {
            return Channel.GetMacroEconomicVariable(macroEconomicVariableId);
        }

        public MacroEconomicVariable[] GetAllMacroEconomicVariables()
        {
            return Channel.GetAllMacroEconomicVariables();
        }


        #endregion

        #region SectorVariableMapping

        public SectorVariableMapping UpdateSectorVariableMapping(SectorVariableMapping sectorVariableMapping)
        {
            return Channel.UpdateSectorVariableMapping(sectorVariableMapping);
        }

        public void DeleteSectorVariableMapping(int sectorVariableMappingId)
        {
            Channel.DeleteSectorVariableMapping(sectorVariableMappingId);
        }

        public SectorVariableMapping GetSectorVariableMapping(int sectorVariableMappingId)
        {
            return Channel.GetSectorVariableMapping(sectorVariableMappingId);
        }

        public SectorVariableMappingData[] GetAllSectorVariableMappings()
        {
            return Channel.GetAllSectorVariableMappings();
        }


        #endregion

        #region PitFormular

        public PitFormular UpdatePitFormular(PitFormular pitFormular)
        {
            return Channel.UpdatePitFormular(pitFormular);
        }

        public void DeletePitFormular(int pitFormularId)
        {
            Channel.DeletePitFormular(pitFormularId);
        }

        public PitFormular GetPitFormular(int pitFormularId)
        {
            return Channel.GetPitFormular(pitFormularId);
        }

        public PitFormular[] GetAllPitFormulars()
        {
            return Channel.GetAllPitFormulars();
        }


        #endregion

        #region PortfolioExposure

        public PortfolioExposure UpdatePortfolioExposure(PortfolioExposure portfolioExposure)
        {
            return Channel.UpdatePortfolioExposure(portfolioExposure);
        }

        public void DeletePortfolioExposure(int portfolioExposureId)
        {
            Channel.DeletePortfolioExposure(portfolioExposureId);
        }

        public PortfolioExposure GetPortfolioExposure(int portfolioExposureId)
        {
            return Channel.GetPortfolioExposure(portfolioExposureId);
        }

        public PortfolioExposure[] GetAllPortfolioExposures()
        {
            return Channel.GetAllPortfolioExposures();
        }

        public string GetAllPortfolioExposuresChart()
        {
            return Channel.GetAllPortfolioExposuresChart();
        }

        #endregion

        #region SectorialExposure

        public SectorialExposure UpdateSectorialExposure(SectorialExposure sectorialExposure)
        {
            return Channel.UpdateSectorialExposure(sectorialExposure);
        }

        public void DeleteSectorialExposure(int sectorialExposureId)
        {
            Channel.DeleteSectorialExposure(sectorialExposureId);
        }

        public SectorialExposure GetSectorialExposure(int sectorialExposureId)
        {
            return Channel.GetSectorialExposure(sectorialExposureId);
        }

        public SectorialExposure[] GetAllSectorialExposures()
        {
            return Channel.GetAllSectorialExposures();
        }

        public string GetAllSectorialExposuresChart()
        {
            return Channel.GetAllSectorialExposuresChart();
        }

        #endregion

        #region PiTPD

        public PiTPD UpdatePiTPD(PiTPD piTPD)
        {
            return Channel.UpdatePiTPD(piTPD);
        }

        public void DeletePiTPD(int piTPDId)
        {
            Channel.DeletePiTPD(piTPDId);
        }

        public PiTPD GetPiTPD(int piTPDId)
        {
            return Channel.GetPiTPD(piTPDId);
        }

        public PiTPD[] GetAllPiTPDs()
        {
            return Channel.GetAllPiTPDs();
        }

        public void RegressPD()
        {
            Channel.RegressPD();
        }

        #endregion

        #region EclCalculationModel

        public EclCalculationModel UpdateEclCalculationModel(EclCalculationModel loanBucketDistribution)
        {
            return Channel.UpdateEclCalculationModel(loanBucketDistribution);
        }

        public void DeleteEclCalculationModel(int loanBucketDistributionId)
        {
            Channel.DeleteEclCalculationModel(loanBucketDistributionId);
        }

        public EclCalculationModel GetEclCalculationModel(int loanBucketDistributionId)
        {
            return Channel.GetEclCalculationModel(loanBucketDistributionId);
        }

        public EclCalculationModel[] GetAllEclCalculationModels()
        {
            return Channel.GetAllEclCalculationModels();
        }


        #endregion

        #region LoanBucketDistribution

        public LoanBucketDistribution UpdateLoanBucketDistribution(LoanBucketDistribution loanBucketDistribution)
        {
            return Channel.UpdateLoanBucketDistribution(loanBucketDistribution);
        }

        public void DeleteLoanBucketDistribution(int loanBucketDistributionId)
        {
            Channel.DeleteLoanBucketDistribution(loanBucketDistributionId);
        }

        public LoanBucketDistribution GetLoanBucketDistribution(int loanBucketDistributionId)
        {
            return Channel.GetLoanBucketDistribution(loanBucketDistributionId);
        }

        public LoanBucketDistribution[] GetAllLoanBucketDistributions()
        {
            return Channel.GetAllLoanBucketDistributions();
        }

        public void PDDistribution()
        {
            Channel.PDDistribution();
        }

        public void PastDueDayDistribution()
        {
            Channel.PastDueDayDistribution();
        }

        public LoanBucketDistribution[] GetLoanAssessments(string bucket)
        {
            return Channel.GetLoanAssessments(bucket);
        }
        #endregion

        #region MacroeconomicVDisplay

        public MacroeconomicVDisplay UpdateMacroeconomicVDisplay(MacroeconomicVDisplay macroeconomicVDisplay)
        {
            return Channel.UpdateMacroeconomicVDisplay(macroeconomicVDisplay);
        }

        public void DeleteMacroeconomicVDisplay(int macroeconomicVDisplayId)
        {
            Channel.DeleteMacroeconomicVDisplay(macroeconomicVDisplayId);
        }

        public MacroeconomicVDisplay GetMacroeconomicVDisplay(int macroeconomicVDisplayId)
        {
            return Channel.GetMacroeconomicVDisplay(macroeconomicVDisplayId);
        }

        public MacroeconomicVDisplay[] GetAllMacroeconomicVDisplays()
        {
            return Channel.GetAllMacroeconomicVDisplays();
        }

        public string[] GetDistinctFHYear(string vType)
        {
            return Channel.GetDistinctFHYear(vType);
        }


        public MacroeconomicVDisplay[] GetMacroeconomicVDisplayByYear(int yr)
        {
            return Channel.GetMacroeconomicVDisplayByYear(yr);
        }
       
        #endregion

        #region LifeTimePDClassification

        public LifeTimePDClassification UpdateLifeTimePDClassification(LifeTimePDClassification lifeTimePDClassification)
        {
            return Channel.UpdateLifeTimePDClassification(lifeTimePDClassification);
        }

        public void DeleteLifeTimePDClassification(int lifeTimePDClassificationId)
        {
            Channel.DeleteLifeTimePDClassification(lifeTimePDClassificationId);
        }

        public LifeTimePDClassification GetLifeTimePDClassification(int lifeTimePDClassificationId)
        {
            return Channel.GetLifeTimePDClassification(lifeTimePDClassificationId);
        }

        public LifeTimePDClassification[] GetAllLifeTimePDClassifications()
        {
            return Channel.GetAllLifeTimePDClassifications();
        }


        #endregion


        #region IfrsEquityUnqouted

        public IfrsEquityUnqouted UpdateIfrsEquityUnqouted(IfrsEquityUnqouted ifrsEquityUnqouted)
        {
            return Channel.UpdateIfrsEquityUnqouted(ifrsEquityUnqouted);
        }

        public void DeleteIfrsEquityUnqouted(int ifrsEquityUnqoutedId)
        {
            Channel.DeleteIfrsEquityUnqouted(ifrsEquityUnqoutedId);
        }

        public IfrsEquityUnqouted GetIfrsEquityUnqouted(int ifrsEquityUnqoutedId)
        {
            return Channel.GetIfrsEquityUnqouted(ifrsEquityUnqoutedId);
        }

        public IfrsEquityUnqouted[] GetAllIfrsEquityUnqouteds()
        {
            return Channel.GetAllIfrsEquityUnqouteds();
        }


        #endregion

        #region IfrsStocksPrimaryData

        public IfrsStocksPrimaryData UpdateIfrsStocksPrimaryData(IfrsStocksPrimaryData ifrsStocksPrimaryData)
        {
            return Channel.UpdateIfrsStocksPrimaryData(ifrsStocksPrimaryData);
        }

        public void DeleteIfrsStocksPrimaryData(int ifrsStocksPrimaryDataId)
        {
            Channel.DeleteIfrsStocksPrimaryData(ifrsStocksPrimaryDataId);
        }

        public IfrsStocksPrimaryData GetIfrsStocksPrimaryData(int ifrsStocksPrimaryDataId)
        {
            return Channel.GetIfrsStocksPrimaryData(ifrsStocksPrimaryDataId);
        }

        public IfrsStocksPrimaryData[] GetAllIfrsStocksPrimaryDatas()
        {
            return Channel.GetAllIfrsStocksPrimaryDatas();
        }


        #endregion

        #region IfrsStocksMapping

        public IfrsStocksMapping UpdateIfrsStocksMapping(IfrsStocksMapping ifrsStocksMapping)
        {
            return Channel.UpdateIfrsStocksMapping(ifrsStocksMapping);
        }

        public void DeleteIfrsStocksMapping(int ifrsStocksMappingId)
        {
            Channel.DeleteIfrsStocksMapping(ifrsStocksMappingId);
        }

        public IfrsStocksMapping GetIfrsStocksMapping(int ifrsStocksMappingId)
        {
            return Channel.GetIfrsStocksMapping(ifrsStocksMappingId);
        }

        public IfrsStocksMappingData[] GetAllIfrsStocksMappings()
        {
            return Channel.GetAllIfrsStocksMappings();
        }


        #endregion

        #region Reconciliation

        public Reconciliation UpdateReconciliation(Reconciliation reconciliation)
        {
            return Channel.UpdateReconciliation(reconciliation);
        }

        public void DeleteReconciliation(int reconciliationId)
        {
            Channel.DeleteReconciliation(reconciliationId);
        }

        public Reconciliation GetReconciliation(int reconciliationId)
        {
            return Channel.GetReconciliation(reconciliationId);
        }

        public Reconciliation[] GetAllReconciliations()
        {
            return Channel.GetAllReconciliations();
        }


        #endregion

        #region ForecastedMacroeconimcsSensitivity

        public ForecastedMacroeconimcsSensitivity UpdateForecastedMacroeconimcsSensitivity(ForecastedMacroeconimcsSensitivity forecastedMacroeconimcsSensitivity)
        {
            return Channel.UpdateForecastedMacroeconimcsSensitivity(forecastedMacroeconimcsSensitivity);
        }

        public void DeleteForecastedMacroeconimcsSensitivity(int forecastedMacroeconimcsSensitivityId)
        {
            Channel.DeleteForecastedMacroeconimcsSensitivity(forecastedMacroeconimcsSensitivityId);
        }

        public ForecastedMacroeconimcsSensitivity GetForecastedMacroeconimcsSensitivity(int forecastedMacroeconimcsSensitivityId)
        {
            return Channel.GetForecastedMacroeconimcsSensitivity(forecastedMacroeconimcsSensitivityId);
        }

        public ForecastedMacroeconimcsSensitivityData[] GetAllForecastedMacroeconimcsSensitivitys()
        {
            return Channel.GetAllForecastedMacroeconimcsSensitivitys();
        }

        public void InsertSensitivityData(string microeconomic, int year, int types, float values)
        {
            Channel.InsertSensitivityData(microeconomic, year, types, values);
        }

        public void ComputeSensitivity()
        {
            Channel.ComputeSensitivity();
        }


        #endregion

        #region SummaryReport

        public SummaryReport UpdateSummaryReport(SummaryReport summaryReport)
        {
            return Channel.UpdateSummaryReport(summaryReport);
        }

        public void DeleteSummaryReport(int summaryReportId)
        {
            Channel.DeleteSummaryReport(summaryReportId);
        }

        public SummaryReport GetSummaryReport(int summaryReportId)
        {
            return Channel.GetSummaryReport(summaryReportId);
        }

        public SummaryReport[] GetAllSummaryReports()
        {
            return Channel.GetAllSummaryReports();
        }

        public string GetAllSummaryReportsChart()
        {
            return Channel.GetAllSummaryReportsChart();
        }

        #endregion

        #region FairValuationModel

        public FairValuationModel UpdateFairValuationModel(FairValuationModel fairValuationModel)
        {
            return Channel.UpdateFairValuationModel(fairValuationModel);
        }

        public void DeleteFairValuationModel(int fairValuationModelId)
        {
            Channel.DeleteFairValuationModel(fairValuationModelId);
        }

        public FairValuationModel GetFairValuationModel(int fairValuationModelId)
        {
            return Channel.GetFairValuationModel(fairValuationModelId);
        }

        public FairValuationModel[] GetAllFairValuationModels()
        {
            return Channel.GetAllFairValuationModels();
        }


        #endregion

        #region BucketExposure

        public BucketExposure UpdateBucketExposure(BucketExposure bucketExposure)
        {
            return Channel.UpdateBucketExposure(bucketExposure);
        }

        public void DeleteBucketExposure(int bucketExposureId)
        {
            Channel.DeleteBucketExposure(bucketExposureId);
        }

        public BucketExposure GetBucketExposure(int bucketExposureId)
        {
            return Channel.GetBucketExposure(bucketExposureId);
        }

        public BucketExposure[] GetAllBucketExposures()
        {
            return Channel.GetAllBucketExposures();
        }

        public string GetAllBucketExposuresChart()
        {
            return Channel.GetAllBucketExposuresChart();
        }

        #endregion

        #region ForecastedMacroeconimcsScenario

        public ForecastedMacroeconimcsScenario UpdateForecastedMacroeconimcsScenario(ForecastedMacroeconimcsScenario forecastedMacroeconimcsScenario)
        {
            return Channel.UpdateForecastedMacroeconimcsScenario(forecastedMacroeconimcsScenario);
        }

        public void DeleteForecastedMacroeconimcsScenario(int forecastedMacroeconimcsScenarioId)
        {
            Channel.DeleteForecastedMacroeconimcsScenario(forecastedMacroeconimcsScenarioId);
        }

        public ForecastedMacroeconimcsScenario GetForecastedMacroeconimcsScenario(int forecastedMacroeconimcsScenarioId)
        {
            return Channel.GetForecastedMacroeconimcsScenario(forecastedMacroeconimcsScenarioId);
        }

        public ForecastedMacroeconimcsScenarioData[] GetAllForecastedMacroeconimcsScenarios()
        {
            return Channel.GetAllForecastedMacroeconimcsScenarios();
        }

        public void InsertScenarioData(string sector, string microeconomic, int year, int types, float values)
        {
            Channel.InsertScenarioData(sector, microeconomic, year, types, values);
        }

        public void ComputeScenario()
        {
            Channel.ComputeScenario();
        }


        #endregion

        #region LoanSpreadScenario

        public LoanSpreadScenario UpdateLoanSpreadScenario(LoanSpreadScenario loanSpreadScenario)
        {
            return Channel.UpdateLoanSpreadScenario(loanSpreadScenario);
        }

        public void DeleteLoanSpreadScenario(int loanSpreadScenarioId)
        {
            Channel.DeleteLoanSpreadScenario(loanSpreadScenarioId);
        }

        public LoanSpreadScenario GetLoanSpreadScenario(int loanSpreadScenarioId)
        {
            return Channel.GetLoanSpreadScenario(loanSpreadScenarioId);
        }

        public LoanSpreadScenario[] GetAllLoanSpreadScenarios()
        {
            return Channel.GetAllLoanSpreadScenarios();
        }


        //public LoanSpreadScenario[] GetLoanAssessments(string bucket)
        //{
        //    return Channel.GetLoanAssessments(bucket);
        //}
        #endregion

        #region LoanSpreadSensitivity

        public LoanSpreadSensitivity UpdateLoanSpreadSensitivity(LoanSpreadSensitivity loanSpreadSensitivity)
        {
            return Channel.UpdateLoanSpreadSensitivity(loanSpreadSensitivity);
        }

        public void DeleteLoanSpreadSensitivity(int loanSpreadSensitivityId)
        {
            Channel.DeleteLoanSpreadSensitivity(loanSpreadSensitivityId);
        }

        public LoanSpreadSensitivity GetLoanSpreadSensitivity(int loanSpreadSensitivityId)
        {
            return Channel.GetLoanSpreadSensitivity(loanSpreadSensitivityId);
        }

        public LoanSpreadSensitivity[] GetAllLoanSpreadSensitivitys()
        {
            return Channel.GetAllLoanSpreadSensitivitys();
        }

        //public LoanSpreadSensitivity[] GetLoanAssessments(string bucket)
        //{
        //    return Channel.GetLoanAssessments(bucket);
        //}
        #endregion

        #region UnquotedEquityFairvalueResult

        public UnquotedEquityFairvalueResult UpdateUnquotedEquityFairvalueResult(UnquotedEquityFairvalueResult unquotedEquityFairvalueResult)
        {
            return Channel.UpdateUnquotedEquityFairvalueResult(unquotedEquityFairvalueResult);
        }

        public void DeleteUnquotedEquityFairvalueResult(int unquotedEquityFairvalueResultId)
        {
            Channel.DeleteUnquotedEquityFairvalueResult(unquotedEquityFairvalueResultId);
        }

        public UnquotedEquityFairvalueResult GetUnquotedEquityFairvalueResult(int unquotedEquityFairvalueResultId)
        {
            return Channel.GetUnquotedEquityFairvalueResult(unquotedEquityFairvalueResultId);
        }

        public UnquotedEquityFairvalueResult[] GetAllUnquotedEquityFairvalueResults()
        {
            return Channel.GetAllUnquotedEquityFairvalueResults();
        }

        #region PiTPDComparism

        public PiTPDComparism UpdatePiTPDComparism(PiTPDComparism piTPDComparism)
        {
            return Channel.UpdatePiTPDComparism(piTPDComparism);
        }

        public void DeletePiTPDComparism(int comparismPDId)
        {
            Channel.DeletePiTPDComparism(comparismPDId);
        }

        public PiTPDComparism GetPiTPDComparism(int comparismPDId)
        {
            return Channel.GetPiTPDComparism(comparismPDId);
        }

        public PiTPDComparism[] GetAllPiTPDComparisms()
        {
            return Channel.GetAllPiTPDComparisms();
        }

        //public string GetAllPiTPDComparismsChart()
        //{
        //    return Channel.GetAllPiTPDComparismsChart();
        //}

        #endregion

        #endregion

        #region MarkovMatrix

        public MarkovMatrix UpdateMarkovMatrix(MarkovMatrix markovMatrix)
        {
            return Channel.UpdateMarkovMatrix(markovMatrix);
        }

        public void DeleteMarkovMatrix(int markovMatrixId)
        {
            Channel.DeleteMarkovMatrix(markovMatrixId);
        }

        public MarkovMatrix GetMarkovMatrix(int markovMatrixId)
        {
            return Channel.GetMarkovMatrix(markovMatrixId);
        }

        public MarkovMatrix[] GetAllMarkovMatrixs()
        {
            return Channel.GetAllMarkovMatrixs();
        }

        public MarkovMatrixData[] GetMarkovMatrixs()
        {
            return Channel.GetMarkovMatrixs();
        }
        #endregion

        #region CCFModelling

        public CCFModelling UpdateCCFModelling(CCFModelling ccfModelling)
        {
            return Channel.UpdateCCFModelling(ccfModelling);
        }

        public void DeleteCCFModelling(int ccfModellingId)
        {
            Channel.DeleteCCFModelling(ccfModellingId);
        }

        public CCFModelling GetCCFModelling(int ccfModellingId)
        {
            return Channel.GetCCFModelling(ccfModellingId);
        }

        public CCFModelling[] GetAllCCFModellings()
        {
            return Channel.GetAllCCFModellings();
        }

        #endregion

        #region ECLComparism

        public ECLComparism UpdateECLComparism(ECLComparism eclComparism)
        {
            return Channel.UpdateECLComparism(eclComparism);
        }

        public void DeleteECLComparism(int eclComparismId)
        {
            Channel.DeleteECLComparism(eclComparismId);
        }

        public ECLComparism GetECLComparism(int eclComparismId)
        {
            return Channel.GetECLComparism(eclComparismId);
        }

        public ECLComparism[] GetAllECLComparisms()
        {
            return Channel.GetAllECLComparisms();
        }


        #endregion

        #region ForeignEADExchnageRAte

        public ForeignEADExchangeRate UpdateForeignEADExchangeRate(ForeignEADExchangeRate foreignEADExchangeRate)
        {
            return Channel.UpdateForeignEADExchangeRate(foreignEADExchangeRate);
        }

        public void DeleteForeignEADExchangeRate(int foreignEADExchangeRateId)
        {
            Channel.DeleteForeignEADExchangeRate(foreignEADExchangeRateId);
        }

        public ForeignEADExchangeRate GetForeignEADExchangeRate(int foreignEADExchnageRateId)
        {
            return Channel.GetForeignEADExchangeRate(foreignEADExchnageRateId);
        }

        public ForeignEADExchangeRate[] GetAllForeignEADExchangeRates()
        {
            return Channel.GetAllForeignEADExchangeRates();
        }


        #endregion

        //Begin Victor Segment

        #region EuroBondSpread

        public EuroBondSpread UpdateEuroBondSpread(EuroBondSpread euroBondSpread)
        {
            return Channel.UpdateEuroBondSpread(euroBondSpread);
        }

        public void DeleteEuroBondSpread(int euroBondSpreadId)
        {
            Channel.DeleteEuroBondSpread(euroBondSpreadId);
        }

        public EuroBondSpread GetEuroBondSpread(int euroBondSpreadId)
        {
            return Channel.GetEuroBondSpread(euroBondSpreadId);
        }

        public EuroBondSpread[] GetAllEuroBondSpreads()
        {
            return Channel.GetAllEuroBondSpreads();
        }


        #endregion

        #region PlacementComputationResult

        public PlacementComputationResult UpdatePlacementComputationResult(PlacementComputationResult placementComputationResult)
        {
            return Channel.UpdatePlacementComputationResult(placementComputationResult);
        }

        public void DeletePlacementComputationResult(int placementComputationResultId)
        {
            Channel.DeletePlacementComputationResult(placementComputationResultId);
        }

        public PlacementComputationResult GetPlacementComputationResult(int placementComputationResultId)
        {
            return Channel.GetPlacementComputationResult(placementComputationResultId);
        }

        public PlacementComputationResult[] GetAllPlacementComputationResults()
        {
            return Channel.GetAllPlacementComputationResults();
        }


        #endregion

        #region LgdComputationResult

        public LgdComputationResult UpdateLgdComputationResult(LgdComputationResult lgdComputationResult)
        {
            return Channel.UpdateLgdComputationResult(lgdComputationResult);
        }

        public void DeleteLgdComputationResult(int lgdComputationResultId)
        {
            Channel.DeleteLgdComputationResult(lgdComputationResultId);
        }

        public LgdComputationResult GetLgdComputationResult(int lgdComputationResultId)
        {
            return Channel.GetLgdComputationResult(lgdComputationResultId);
        }

        public LgdComputationResult[] GetAllLgdComputationResults()
        {
            return Channel.GetAllLgdComputationResults();
        }


        #endregion

        #region LocalBondSpread

        public LocalBondSpread UpdateLocalBondSpread(LocalBondSpread localBondSpread)
        {
            return Channel.UpdateLocalBondSpread(localBondSpread);
        }

        public void DeleteLocalBondSpread(int localBondSpreadId)
        {
            Channel.DeleteLocalBondSpread(localBondSpreadId);
        }

        public LocalBondSpread GetLocalBondSpread(int localBondSpreadId)
        {
            return Channel.GetLocalBondSpread(localBondSpreadId);
        }

        public LocalBondSpread[] GetAllLocalBondSpreads()
        {
            return Channel.GetAllLocalBondSpreads();
        }


        #endregion

        #region MarginalPDDistribution

        public MarginalPDDistribution UpdateMarginalPDDistribution(MarginalPDDistribution marginalPDDistribution)
        {
            return Channel.UpdateMarginalPDDistribution(marginalPDDistribution);
        }

        public void DeleteMarginalPDDistribution(int marginalPDDistributionId)
        {
            Channel.DeleteMarginalPDDistribution(marginalPDDistributionId);
        }

        public MarginalPDDistribution GetMarginalPDDistribution(int marginalPDDistributionId)
        {
            return Channel.GetMarginalPDDistribution(marginalPDDistributionId);
        }

        public MarginalPDDistribution[] GetAllMarginalPDDistributions()
        {
            return Channel.GetAllMarginalPDDistributions();
        }


        #endregion

        #region BondMarginalPDDistribution

        public BondMarginalPDDistribution UpdateBondMarginalPDDistribution(BondMarginalPDDistribution bondMarginalPDDistribution)
        {
            return Channel.UpdateBondMarginalPDDistribution(bondMarginalPDDistribution);
        }

        public void DeleteBondMarginalPDDistribution(int bondMarginalPDDistributionId)
        {
            Channel.DeleteBondMarginalPDDistribution(bondMarginalPDDistributionId);
        }

        public BondMarginalPDDistribution GetBondMarginalPDDistribution(int bondMarginalPDDistributionId)
        {
            return Channel.GetBondMarginalPDDistribution(bondMarginalPDDistributionId);
        }

        public BondMarginalPDDistribution[] GetAllBondMarginalPDDistributions()
        {
            return Channel.GetAllBondMarginalPDDistributions();
        }


        #endregion

        #region MarginalPDDistributionPlacement

        public MarginalPDDistributionPlacement UpdateMarginalPDDistributionPlacement(MarginalPDDistributionPlacement marginalPDDistributionPlacement)
        {
            return Channel.UpdateMarginalPDDistributionPlacement(marginalPDDistributionPlacement);
        }

        public void DeleteMarginalPDDistributionPlacement(int marginalPDDistributionPlacementId)
        {
            Channel.DeleteMarginalPDDistributionPlacement(marginalPDDistributionPlacementId);
        }

        public MarginalPDDistributionPlacement GetMarginalPDDistributionPlacement(int marginalPDDistributionPlacementId)
        {
            return Channel.GetMarginalPDDistributionPlacement(marginalPDDistributionPlacementId);
        }

        public MarginalPDDistributionPlacement[] GetAllMarginalPDDistributionPlacements()
        {
            return Channel.GetAllMarginalPDDistributionPlacements();
        }


        #endregion

        #region EclComputationResult

        public EclComputationResult UpdateEclComputationResult(EclComputationResult eclComputationResult)
        {
            return Channel.UpdateEclComputationResult(eclComputationResult);
        }

        public void DeleteEclComputationResult(int eclComputationResultId)
        {
            Channel.DeleteEclComputationResult(eclComputationResultId);
        }

        public EclComputationResult GetEclComputationResult(int eclComputationResultId)
        {
            return Channel.GetEclComputationResult(eclComputationResultId);
        }

        public EclComputationResult[] GetAllEclComputationResults()
        {
            return Channel.GetAllEclComputationResults();
        }


        #endregion

        #region BondEclComputationResult

        public BondEclComputationResult UpdateBondEclComputationResult(BondEclComputationResult bondEclComputationResult)
        {
            return Channel.UpdateBondEclComputationResult(bondEclComputationResult);
        }

        public void DeleteBondEclComputationResult(int bondEclComputationResultId)
        {
            Channel.DeleteBondEclComputationResult(bondEclComputationResultId);
        }

        public BondEclComputationResult GetBondEclComputationResult(int bondEclComputationResultId)
        {
            return Channel.GetBondEclComputationResult(bondEclComputationResultId);
        }

        public BondEclComputationResult[] GetAllBondEclComputationResults()
        {
            return Channel.GetAllBondEclComputationResults();
        }


        #endregion

        #region PlacementEclComputationResult

        public PlacementEclComputationResult UpdatePlacementEclComputationResult(PlacementEclComputationResult placementEclComputationResult)
        {
            return Channel.UpdatePlacementEclComputationResult(placementEclComputationResult);
        }

        public void DeletePlacementEclComputationResult(int placementEclComputationResultId)
        {
            Channel.DeletePlacementEclComputationResult(placementEclComputationResultId);
        }

        public PlacementEclComputationResult GetPlacementEclComputationResult(int placementEclComputationResultId)
        {
            return Channel.GetPlacementEclComputationResult(placementEclComputationResultId);
        }

        public PlacementEclComputationResult[] GetAllPlacementEclComputationResults()
        {
            return Channel.GetAllPlacementEclComputationResults();
        }


        #endregion

        #region LcBgEclComputationResult

        public LcBgEclComputationResult UpdateLcBgEclComputationResult(LcBgEclComputationResult lcBgEclComputationResult)
        {
            return Channel.UpdateLcBgEclComputationResult(lcBgEclComputationResult);
        }

        public void DeleteLcBgEclComputationResult(int lcBgEclComputationResultId)
        {
            Channel.DeleteLcBgEclComputationResult(lcBgEclComputationResultId);
        }

        public LcBgEclComputationResult GetLcBgEclComputationResult(int lcBgEclComputationResultId)
        {
            return Channel.GetLcBgEclComputationResult(lcBgEclComputationResultId);
        }

        public LcBgEclComputationResult[] GetAllLcBgEclComputationResults()
        {
            return Channel.GetAllLcBgEclComputationResults();
        }


        #endregion
        
        //End Victor Segment

        #region ComputedForcastedPDLGD

        public ComputedForcastedPDLGD UpdateComputedForcastedPDLGD(ComputedForcastedPDLGD computedForcastedPDLGD)
        {
            return Channel.UpdateComputedForcastedPDLGD(computedForcastedPDLGD);
        }

        public void DeleteComputedForcastedPDLGD(int computedPDId)
        {
            Channel.DeleteComputedForcastedPDLGD(computedPDId);
        }

        public ComputedForcastedPDLGD GetComputedForcastedPDLGD(int computedPDId)
        {
            return Channel.GetComputedForcastedPDLGD(computedPDId);
        }

        public ComputedForcastedPDLGD[] GetAllComputedForcastedPDLGDs()
        {
            return Channel.GetAllComputedForcastedPDLGDs();
        }

        public ComputedForcastedPDLGD[] GetListComputedForcastedPDLGDs()
        {
            return Channel.GetListComputedForcastedPDLGDs();
        }


        #endregion

        #region ComputedForcastedPDLGDForeign

        public ComputedForcastedPDLGDForeign UpdateComputedForcastedPDLGDForeign(ComputedForcastedPDLGDForeign computedForcastedPDLGDForeign)
        {
            return Channel.UpdateComputedForcastedPDLGDForeign(computedForcastedPDLGDForeign);
        }

        public void DeleteComputedForcastedPDLGDForeign(int computedPDId)
        {
            Channel.DeleteComputedForcastedPDLGDForeign(computedPDId);
        }

        public ComputedForcastedPDLGDForeign GetComputedForcastedPDLGDForeign(int computedPDId)
        {
            return Channel.GetComputedForcastedPDLGDForeign(computedPDId);
        }

        public ComputedForcastedPDLGDForeign[] GetAllComputedForcastedPDLGDForeigns()
        {
            return Channel.GetAllComputedForcastedPDLGDForeigns();
        }

        public ComputedForcastedPDLGDForeign[] GetListComputedForcastedPDLGDForeigns()
        {
            return Channel.GetListComputedForcastedPDLGDForeigns();
        }


        #endregion

        #region MacroEconomicsNPL

        public MacroEconomicsNPL UpdateMacroEconomicsNPL(MacroEconomicsNPL macroEconomicsNPL)
        {
            return Channel.UpdateMacroEconomicsNPL(macroEconomicsNPL);
        }

        public void DeleteMacroEconomicsNPL(int macroeconomicnplId)
        {
            Channel.DeleteMacroEconomicsNPL(macroeconomicnplId);
        }

        public MacroEconomicsNPL GetMacroEconomicsNPL(int macroeconomicnplId)
        {
            return Channel.GetMacroEconomicsNPL(macroeconomicnplId);
        }

        public MacroEconomicsNPL[] GetAllMacroEconomicsNPLs()
        {
            return Channel.GetAllMacroEconomicsNPLs();
        }

        #endregion

        #region MonthlyDiscountFactor

        public MonthlyDiscountFactor UpdateMonthlyDiscountFactor(MonthlyDiscountFactor monthlyDiscountFactor)
        {
            return Channel.UpdateMonthlyDiscountFactor(monthlyDiscountFactor);
        }

        public void DeleteMonthlyDiscountFactor(int MonthlyDiscountFactor_Id)
        {
            Channel.DeleteMonthlyDiscountFactor(MonthlyDiscountFactor_Id);
        }

        public MonthlyDiscountFactor GetMonthlyDiscountFactor(int MonthlyDiscountFactor_Id)
        {
            return Channel.GetMonthlyDiscountFactor(MonthlyDiscountFactor_Id);
        }

        public MonthlyDiscountFactor[] GetAllMonthlyDiscountFactors()
        {
            return Channel.GetAllMonthlyDiscountFactors();
        }

        public MonthlyDiscountFactor[] GetMonthlyDiscountFactorByRefNo(string RefNo)
        {
            return Channel.GetMonthlyDiscountFactorByRefNo(RefNo);
        }


        #endregion

        #region MonthlyDiscountFactorBond

        public MonthlyDiscountFactorBond UpdateMonthlyDiscountFactorBond(MonthlyDiscountFactorBond monthlyDiscountFactorBond)
        {
            return Channel.UpdateMonthlyDiscountFactorBond(monthlyDiscountFactorBond);
        }

        public void DeleteMonthlyDiscountFactorBond(int MonthlyDiscountFactorBond_Id)
        {
            Channel.DeleteMonthlyDiscountFactorBond(MonthlyDiscountFactorBond_Id);
        }

        public MonthlyDiscountFactorBond GetMonthlyDiscountFactorBond(int MonthlyDiscountFactorBond_Id)
        {
            return Channel.GetMonthlyDiscountFactorBond(MonthlyDiscountFactorBond_Id);
        }

        public MonthlyDiscountFactorBond[] GetAllMonthlyDiscountFactorBonds()
        {
            return Channel.GetAllMonthlyDiscountFactorBonds();
        }

        public MonthlyDiscountFactorBond[] GetMonthlyDiscountFactorBondByRefNo(string RefNo)
        {
            return Channel.GetMonthlyDiscountFactorBondByRefNo(RefNo);
        }


        #endregion

        #region MonthlyDiscountFactorPlacement

        public MonthlyDiscountFactorPlacement UpdateMonthlyDiscountFactorPlacement(MonthlyDiscountFactorPlacement monthlyDiscountFactor)
        {
            return Channel.UpdateMonthlyDiscountFactorPlacement(monthlyDiscountFactor);
        }

        public void DeleteMonthlyDiscountFactorPlacement(int MonthlyDiscountFactorPlacement_Id)
        {
            Channel.DeleteMonthlyDiscountFactorPlacement(MonthlyDiscountFactorPlacement_Id);
        }

        public MonthlyDiscountFactorPlacement GetMonthlyDiscountFactorPlacement(int MonthlyDiscountFactorPlacement_Id)
        {
            return Channel.GetMonthlyDiscountFactorPlacement(MonthlyDiscountFactorPlacement_Id);
        }

        public MonthlyDiscountFactorPlacement[] GetAllMonthlyDiscountFactorPlacements()
        {
            return Channel.GetAllMonthlyDiscountFactorPlacements();
        }

        public MonthlyDiscountFactorPlacement[] GetMonthlyDiscountFactorPlacementByRefNo(string RefNo)
        {
            return Channel.GetMonthlyDiscountFactorPlacementByRefNo(RefNo);
        }


        #endregion

        #region ProbabilityWeighted

        public ProbabilityWeighted UpdateProbabilityWeighted(ProbabilityWeighted probabilityWeighted)
        {
            return Channel.UpdateProbabilityWeighted(probabilityWeighted);
        }

        public void DeleteProbabilityWeighted(int ProbabilityWeighted_Id)
        {
            Channel.DeleteProbabilityWeighted(ProbabilityWeighted_Id);
        }

        public ProbabilityWeighted GetProbabilityWeighted(int ProbabilityWeighted_Id)
        {
            return Channel.GetProbabilityWeighted(ProbabilityWeighted_Id);
        }

        public ProbabilityWeighted[] GetAllProbabilityWeighteds()
        {
            return Channel.GetAllProbabilityWeighteds();
        }

        public ProbabilityWeighted[] GetProbabilityWeightedByInstrumentType(string InstrumentType)
        {
            return Channel.GetProbabilityWeightedByInstrumentType(InstrumentType);
        }


        #endregion

        #region MacrovariableEstimate

        public MacrovariableEstimate UpdateMacrovariableEstimate(MacrovariableEstimate macrovariableEstimate)
        {
            return Channel.UpdateMacrovariableEstimate(macrovariableEstimate);
        }

        public void DeleteMacrovariableEstimate(int MacrovariableEstimate_Id)
        {
            Channel.DeleteMacrovariableEstimate(MacrovariableEstimate_Id);
        }

        public MacrovariableEstimate GetMacrovariableEstimate(int MacrovariableEstimate_Id)
        {
            return Channel.GetMacrovariableEstimate(MacrovariableEstimate_Id);
        }

        public MacrovariableEstimate[] GetAllMacrovariableEstimates()
        {
            return Channel.GetAllMacrovariableEstimates();
        }

        public MacrovariableEstimate[] GetMacrovariableEstimateByCategory(string Category)
        {
            return Channel.GetMacrovariableEstimateByCategory(Category);
        }


        #endregion

        #region SectorMapping

        public SectorMapping UpdateSectorMapping(SectorMapping sectorMapping)
        {
            return Channel.UpdateSectorMapping(sectorMapping);
        }

        public void DeleteSectorMapping(int SectorMapping_Id)
        {
            Channel.DeleteSectorMapping(SectorMapping_Id);
        }

        public SectorMapping GetSectorMapping(int SectorMapping_Id)
        {
            return Channel.GetSectorMapping(SectorMapping_Id);
        }

        public SectorMapping[] GetAllSectorMappings()
        {
            return Channel.GetAllSectorMappings();
        }

        //public SectorMapping[] GetSectorMappingBySource(string Source)
        //{
        //    return Channel.GetSectorMappingBySource(Source);
        //}


        #endregion

        #region InvestmentOthersECL

        public InvestmentOthersECL UpdateInvestmentOthersECL(InvestmentOthersECL investmentOthersECL)
        {
            return Channel.UpdateInvestmentOthersECL(investmentOthersECL);
        }

        public void DeleteInvestmentOthersECL(int ecl_Id)
        {
            Channel.DeleteInvestmentOthersECL(ecl_Id);
        }

        public InvestmentOthersECL GetInvestmentOthersECL(int ecl_Id)
        {
            return Channel.GetInvestmentOthersECL(ecl_Id);
        }

        public InvestmentOthersECL[] GetAllInvestmentOthersECLs()
        {
            return Channel.GetAllInvestmentOthersECLs();
        }

        public InvestmentOthersECL[] GetInvestmentOthersECLByRefNo(string RefNo)
        {
            return Channel.GetInvestmentOthersECLByRefNo(RefNo);
        }


        #endregion
  
    }
}
