using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;

namespace Fintrak.Business.IFRS.Contracts
{
    [ServiceContract]
    public interface IExtractedDataService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();
     
        #region IFRSBonds

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IFRSBonds UpdateIFRSBonds(IFRSBonds IFRSBonds);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIFRSBonds(int bondId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IFRSBonds GetIFRSBonds(int bondId);

        [OperationContract]
        IFRSBonds[] GetAllIFRSBonds();

        [OperationContract]
        IFRSBonds[] GetBondsByClassification(string classification);

        [OperationContract]
        IFRSBonds[] GetbondsByMaturityDate(DateTime matureDate);

        [OperationContract]
        void UpdatebondsByMaturityDate(DateTime matureDate, decimal cmprice);
    

        #endregion

        #region IFRSTbills

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IFRSTbills UpdateIFRSTbills(IFRSTbills IFRSTbills);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIFRSTbills(int tbillId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IFRSTbills GetIFRSTbills(int tbillId);

        [OperationContract]
        IFRSTbills[] GetAllIFRSTbills();

        [OperationContract]
        IFRSTbills[] GetTbillsByClassification(string classification);
        [OperationContract]
        IFRSTbills[] GetTbillsByMaturityDate(DateTime matureDate);

        [OperationContract]
        void UpdateTbillsByMaturityDate(DateTime matureDate, decimal cmprice);

        #endregion        

        #region LoanPry

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        LoanPry UpdateLoanPry(LoanPry loanPry);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteLoanPry(int pryId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LoanPry GetLoanPry(int pryId);

        [OperationContract]
        LoanPryData[] GetAllLoanPry();

        [OperationContract]
        LoanPry[] GetLoanPryByScheduleType(string schType);

        #endregion

        #region RawRawLoanDetails

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        RawLoanDetails UpdateRawLoanDetails(RawLoanDetails loanDetails);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteRawLoanDetails(int loanDetailId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        RawLoanDetails GetRawLoanDetails(int loanDetailId);


        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        RawLoanDetails[] GetAllRawLoanDetails();

        [OperationContract]
        void UpdateLoanClassNotch(string refNo, string rating, int stage);

        #endregion

        #region IntegralFee

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IntegralFee UpdateIntegralFee(IntegralFee integralFee);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIntegralFee(int integralFeeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IntegralFee GetIntegralFee(int integralFeeId);

        [OperationContract]
        IntegralFee[] GetAllIntegralFee();    

        #endregion

        #region IfrsCustomer

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IfrsCustomer UpdateIfrsCustomer(IfrsCustomer ifrsCustomer);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIfrsCustomer(int customerId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IfrsCustomer GetIfrsCustomer(int customerId);

        [OperationContract]
        IfrsCustomer[] GetAllIfrsCustomer();


        [OperationContract]
        IfrsCustomer[] GetIfrsCustomerByRating(string rating);
        #endregion

        #region IfrsCustomerAccount

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IfrsCustomerAccount UpdateIfrsCustomerAccount(IfrsCustomerAccount ifrsCustomerAccount);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIfrsCustomerAccount(int custAccountId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IfrsCustomerAccount GetIfrsCustomerAccount(int custAccountId);

        [OperationContract]
        IfrsCustomerAccount[] GetAllIfrsCustomerAccount();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        string[] GetDistinctSector();

        #endregion

        #region UnMappedProducts

        [OperationContract]
        UnMappedProduct[] GetAllUnMappedProducts();

        #endregion

        #region LoanPryMoratorium

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        LoanPryMoratorium UpdateLoanPryMoratorium(LoanPryMoratorium loanPryMoratorium);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteLoanPryMoratorium(int loanPryMoratoriumId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LoanPryMoratorium GetLoanPryMoratorium(int loanPryMoratoriumId);

        [OperationContract]
        LoanPryMoratorium[] GetAllLoanPryMoratorium();

        #endregion

        #region Borrowings

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Borrowings UpdateBorrowings(Borrowings borrowings);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBorrowings(int borrowingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Borrowings GetBorrowings(int borrowingId);

        [OperationContract]
        Borrowings[] GetAllBorrowings();

        
        #endregion

        #region OffBalanceSheetExposure

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        OffBalanceSheetExposure UpdateOffBalanceSheetExposure(OffBalanceSheetExposure offBalanceSheetExposure);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteOffBalanceSheetExposure(int obeId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        OffBalanceSheetExposure GetOffBalanceSheetExposure(int obeId);

        [OperationContract]
        OffBalanceSheetExposure[] GetAllOffBalanceSheetExposure();

        [OperationContract]
        OffBalanceSheetExposure[] GetOffBalanceSheetExposureByPortfolio(string portfolio);

        //[OperationContract]
        //OffBalanceSheetExposure[] GetTbillsByMaturityDate(DateTime matureDate);

        //[OperationContract]
        //void UpdateTbillsByMaturityDate(DateTime matureDate, decimal cmprice);

        #endregion

        #region Placement

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Placement UpdatePlacement(Placement placement);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePlacement(int Placement_Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Placement GetPlacement(int Placement_Id);

        [OperationContract]
        Placement[] GetAllPlacements();

        //[OperationContract]
        //Placement[] GetPlacementByRefNo(string RefNo);

        #endregion

        #region LoanInterestRate

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        LoanInterestRate UpdateLoanInterestRate(LoanInterestRate loanInterestRate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteLoanInterestRate(int LoanInterestRate_Id);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        LoanInterestRate GetLoanInterestRate(int LoanInterestRate_Id);

        [OperationContract]
        LoanInterestRate[] GetAllLoanInterestRates();

        //[OperationContract]
        //LoanInterestRate[] GetLoanInterestRateByRefNo(string RefNo);

        #endregion
       
    }
}
