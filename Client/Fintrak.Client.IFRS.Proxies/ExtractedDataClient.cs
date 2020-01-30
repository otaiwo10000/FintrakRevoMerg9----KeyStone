using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.IFRS.Contracts;
using Fintrak.Client.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.IFRS.Proxies
{
    [Export(typeof(IExtractedDataService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ExtratedDataClient : UserClientBase<IExtractedDataService>, IExtractedDataService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }



        #region IFRSBonds

        public IFRSBonds UpdateIFRSBonds(IFRSBonds IFRSBonds)
        {
            return Channel.UpdateIFRSBonds(IFRSBonds);
        }

        public void DeleteIFRSBonds(int bondId)
        {
            Channel.DeleteIFRSBonds(bondId);
        }

        public IFRSBonds GetIFRSBonds(int bondId)
        {
            return Channel.GetIFRSBonds(bondId);
        }

        public IFRSBonds[] GetAllIFRSBonds()
        {
            return Channel.GetAllIFRSBonds();
        }

        public IFRSBonds[] GetBondsByClassification(string classification)
        {
            return Channel.GetBondsByClassification(classification);
        }

        public IFRSBonds[] GetbondsByMaturityDate(DateTime matureDate)
        {
            return Channel.GetbondsByMaturityDate(matureDate);
        }

       public  void UpdatebondsByMaturityDate(DateTime matureDate, decimal cmprice)
        {
             Channel.UpdatebondsByMaturityDate(matureDate, cmprice);
        }
        
        
        #endregion

        #region IFRSTbills

        public IFRSTbills UpdateIFRSTbills(IFRSTbills IFRSTbills)
        {
            return Channel.UpdateIFRSTbills(IFRSTbills);
        }

        public void DeleteIFRSTbills(int tbillId)
        {
            Channel.DeleteIFRSTbills(tbillId);
        }

        public IFRSTbills GetIFRSTbills(int tbillId)
        {
            return Channel.GetIFRSTbills(tbillId);
        }

        public IFRSTbills[] GetAllIFRSTbills()
        {
            return Channel.GetAllIFRSTbills();
        }

        public IFRSTbills[] GetTbillsByClassification(string classification)
        {
            return Channel.GetTbillsByClassification(classification);
        }

        public IFRSTbills[] GetTbillsByMaturityDate(DateTime matureDate)
        {
            return Channel.GetTbillsByMaturityDate(matureDate);
        }

        public void UpdateTbillsByMaturityDate(DateTime matureDate, decimal cmprice)
        {
            Channel.UpdateTbillsByMaturityDate(matureDate, cmprice);
        }

        #endregion

        #region LoanPry

        public LoanPry UpdateLoanPry(LoanPry loanPryMoratorium)
        { 
            return Channel.UpdateLoanPry(loanPryMoratorium);
        }

        public void DeleteLoanPry(int pryId)
        {
            Channel.DeleteLoanPry(pryId);
        }

        public LoanPry GetLoanPry(int pryId)
        {
            return Channel.GetLoanPry(pryId);
        }

        public LoanPryData[] GetAllLoanPry()
        {
            return Channel.GetAllLoanPry();
        }

        public LoanPry[] GetLoanPryByScheduleType(string schType)
        {
            return Channel.GetLoanPryByScheduleType(schType);
        }

        #endregion

        #region RawLoanDetails

        public RawLoanDetails UpdateRawLoanDetails(RawLoanDetails loanDetails)
        {
            return Channel.UpdateRawLoanDetails(loanDetails);
        }

        public void DeleteRawLoanDetails(int loanDetailld)
        {
            Channel.DeleteRawLoanDetails(loanDetailld);
        }

        public RawLoanDetails GetRawLoanDetails(int loanDetailld)
        {
            return Channel.GetRawLoanDetails(loanDetailld);
        }

        public RawLoanDetails[] GetAllRawLoanDetails()
        {
            return Channel.GetAllRawLoanDetails();
        }

        public void UpdateLoanClassNotch(string refNo, string rating, int stage)
        {
            Channel.UpdateLoanClassNotch(refNo, rating, stage);
        }

        #endregion

        #region IntegralFee

        public IntegralFee UpdateIntegralFee(IntegralFee integralFee)
        {
            return Channel.UpdateIntegralFee(integralFee);
        }

        public void DeleteIntegralFee(int integralFeeId)
        {
            Channel.DeleteIntegralFee(integralFeeId);
        }

        public IntegralFee GetIntegralFee(int integralFeeId)
        {
            return Channel.GetIntegralFee(integralFeeId);
        }

        public IntegralFee[] GetAllIntegralFee()
        {
            return Channel.GetAllIntegralFee();
        }

        #endregion

        #region IfrsCustomer

        public IfrsCustomer UpdateIfrsCustomer(IfrsCustomer ifrsCustomer)
        {
            return Channel.UpdateIfrsCustomer(ifrsCustomer);
        }

        public void DeleteIfrsCustomer(int customerId)
        {
            Channel.DeleteIfrsCustomer(customerId);
        }

        public IfrsCustomer GetIfrsCustomer(int customerId)
        {
            return Channel.GetIfrsCustomer(customerId);
        }

        public IfrsCustomer[] GetAllIfrsCustomer()
        {
            return Channel.GetAllIfrsCustomer();
        }

        public IfrsCustomer[] GetIfrsCustomerByRating(string rating)
        {
            return Channel.GetIfrsCustomerByRating(rating);
        }
        #endregion

        #region IfrsCustomerAccount

        public IfrsCustomerAccount UpdateIfrsCustomerAccount(IfrsCustomerAccount ifrsCustomerAccount)
        {
            return Channel.UpdateIfrsCustomerAccount(ifrsCustomerAccount);
        }

        public void DeleteIfrsCustomerAccount(int custAccountId)
        {
            Channel.DeleteIfrsCustomerAccount(custAccountId);
        }

        public IfrsCustomerAccount GetIfrsCustomerAccount(int custAccountId)
        {
            return Channel.GetIfrsCustomerAccount(custAccountId);
        }

        public IfrsCustomerAccount[] GetAllIfrsCustomerAccount()
        {
            return Channel.GetAllIfrsCustomerAccount();
        }
        public string[] GetDistinctSector()
        {
            return Channel.GetDistinctSector();
        }

        #endregion

        #region UnMappedProduct

        public UnMappedProduct[] GetAllUnMappedProducts()
        {
            return Channel.GetAllUnMappedProducts();
        }



        #endregion

        #region LoanPryMoratorium

        public LoanPryMoratorium UpdateLoanPryMoratorium(LoanPryMoratorium loanPryMoratorium)
        {
            return Channel.UpdateLoanPryMoratorium(loanPryMoratorium);
        }

        public void DeleteLoanPryMoratorium(int loanPryMoratoriumId)
        {
            Channel.DeleteLoanPryMoratorium(loanPryMoratoriumId);
        }

        public LoanPryMoratorium GetLoanPryMoratorium(int loanPryMoratoriumId)
        {
            return Channel.GetLoanPryMoratorium(loanPryMoratoriumId);
        }

        public LoanPryMoratorium[] GetAllLoanPryMoratorium()
        {
            return Channel.GetAllLoanPryMoratorium();
        }

       

        #endregion
        
        #region Borrowings

        public Borrowings UpdateBorrowings(Borrowings borrowing)
        {
            return Channel.UpdateBorrowings(borrowing);
        }

        public void DeleteBorrowings(int borrowingId)
        {
            Channel.DeleteBorrowings(borrowingId);
        }

        public Borrowings GetBorrowings(int borrowingId)
        {
            return Channel.GetBorrowings(borrowingId);
        }

        public Borrowings[] GetAllBorrowings()
        {
            return Channel.GetAllBorrowings();
        }
        
        #endregion

        #region OffBalanceSheetExposure

        public OffBalanceSheetExposure UpdateOffBalanceSheetExposure(OffBalanceSheetExposure offBalanceSheetExposure)
        {
            return Channel.UpdateOffBalanceSheetExposure(offBalanceSheetExposure);
        }

        public void DeleteOffBalanceSheetExposure(int obeId)
        {
            Channel.DeleteOffBalanceSheetExposure(obeId);
        }

        public OffBalanceSheetExposure GetOffBalanceSheetExposure(int obeId)
        {
            return Channel.GetOffBalanceSheetExposure(obeId);
        }

        public OffBalanceSheetExposure[] GetAllOffBalanceSheetExposure()
        {
            return Channel.GetAllOffBalanceSheetExposure();
        }

        public OffBalanceSheetExposure[] GetOffBalanceSheetExposureByPortfolio(string portfolio)
        {
            return Channel.GetOffBalanceSheetExposureByPortfolio(portfolio);
        }

        #endregion

        #region Placement

        public Placement UpdatePlacement(Placement placement)
        {
            return Channel.UpdatePlacement(placement);
        }

        public void DeletePlacement(int Placement_Id)
        {
            Channel.DeletePlacement(Placement_Id);
        }

        public Placement GetPlacement(int Placement_Id)
        {
            return Channel.GetPlacement(Placement_Id);
        }

        public Placement[] GetAllPlacements()
        {
            return Channel.GetAllPlacements();
        }

        //public Placement[] GetPlacementByRefNo(string RefNo)
        //{
        //    return Channel.GetPlacementByRefNo(RefNo);
        //}


        #endregion

        #region LoanInterestRate

        public LoanInterestRate UpdateLoanInterestRate(LoanInterestRate loanInterestRate)
        {
            return Channel.UpdateLoanInterestRate(loanInterestRate);
        }

        public void DeleteLoanInterestRate(int LoanInterestRate_Id)
        {
            Channel.DeleteLoanInterestRate(LoanInterestRate_Id);
        }

        public LoanInterestRate GetLoanInterestRate(int LoanInterestRate_Id)
        {
            return Channel.GetLoanInterestRate(LoanInterestRate_Id);
        }

        public LoanInterestRate[] GetAllLoanInterestRates()
        {
            return Channel.GetAllLoanInterestRates();
        }

        //public LoanInterestRate[] GetLoanInterestRateByRefNo(string RefNo)
        //{
        //    return Channel.GetLoanInterestRateByRefNo(RefNo);
        //}


        #endregion
    }
}
