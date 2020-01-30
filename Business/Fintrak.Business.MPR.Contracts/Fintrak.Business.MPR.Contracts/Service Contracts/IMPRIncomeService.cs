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
    public interface IMPRIncomeService : IServiceContract
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void RegisterModule();


        

        #region Income Product

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeProductsTable Updateincomeproducttable(IncomeProductsTable incomeproducttable);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void Deleteincomeproducttable(int productid);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeProductsTable Getincomeproducttable(int productid);

        [OperationContract]
        IncomeProductsTable[] GetAllincomeproducttable();

        [OperationContract]
        IncomeProductsTable[] GetincomeproducttableUsingSearchValue(string searchvalue);

        #endregion


        #region Caption

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Caption UpdateCaption(Caption caption);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteCaption(int CaptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Caption GetCaption(int CaptionId);

        [OperationContract]
        Caption[] GetAllCaptions();

        #endregion

        #region PLCaption2

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PLCaption2 UpdatePLCaption2(PLCaption2 plcaption);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePLCaption2(int PLCaptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        PLCaption2 GetPLCaption2(int PLCaptionId);

        [OperationContract]
        PLCaption2[] GetAllPLCaption2();

        #endregion

        #region PPRCaption

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        PPRCaption UpdatePPRCaption(PPRCaption pprcaption);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeletePPRCaption(int PPRCaptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        PPRCaption GetPPRCaption(int PPRCaptionId);

        [OperationContract]
        PPRCaption[] GetAllPPRCaption();

        #endregion

        #region Income CommFee Line Caption

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeCommFeeLineCaption UpdateIncomeCommFeeLineCaption(IncomeCommFeeLineCaption ICFLcaption);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeCommFeeLineCaption(int ICFLcaptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeCommFeeLineCaption GetIncomeCommFeeLineCaption(int ICFLcaptionId);

        [OperationContract]
        IncomeCommFeeLineCaption[] GetAllIncomeCommFeeLineCaption();

        [OperationContract]
        IncomeCommFeeLineCaption[] GetIncomeCommFeeLineCaptionUsingSearchValue(string searchvalue);

        #endregion

        #region IncomeLineCapton

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeLineCapton UpdateIncomeLineCapton(IncomeLineCapton ilcaption);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeLineCapton(int ilCaptionId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeLineCapton GetIncomeLineCapton(int ilCaptionId);

        [OperationContract]
        IncomeLineCapton[] GetAllIncomeLineCaptons();

        #endregion

        #region Income Products table Unit

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeProductstableUnit UpdateIncomeProductstableUnit(IncomeProductstableUnit iptUnit);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeProductstableUnit(int iptUnitId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeProductstableUnit GetIncomeProductstableUnit(int iptUnitId);

        [OperationContract]
        IncomeProductstableUnit[] GetAllIncomeProductstableUnits();

        [OperationContract]
        IncomeProductstableUnit[] GetincomeproducttableunitUsingSearchValue(string searchvalue);

        #endregion

        #region Income Products Table Treasury

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeProductsTableTreasury UpdateIncomeProductsTableTreasury(IncomeProductsTableTreasury iptTreasury);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeProductsTableTreasury(int iptTreasuryId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeProductsTableTreasury GetIncomeProductsTableTreasury(int iptTreasuryId);

        [OperationContract]
        IncomeProductsTableTreasury[] GetAllIncomeProductsTableTreasury();

        [OperationContract]
        IncomeProductsTableTreasury[] GetIncomeProductsTableTreasuryUsingSearchValue(string searchvalue);

        #endregion

        #region Income NEA Mapping

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeNEAMapping UpdateIncomeNEAMapping(IncomeNEAMapping incomeNEAmapping);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeNEAMapping(int incomeNEAmappingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeNEAMapping GetIncomeNEAMapping(int incomeNEAmappingId);

        [OperationContract]
        IncomeNEAMapping[] GetAllIncomeNEAMapping();

        [OperationContract]
        IncomeNEAMappingData[] GetIncomeNEAMappingUsingSearchValue(string searchvalue);
        [OperationContract]
        IncomeNEAMappingData[] GetFullIncomeNEAMapping();

        #endregion

        #region KBL MIS Product Category Info

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        KBL_MISProductCategoryInfo UpdateKBL_MISProductCategoryInfo(KBL_MISProductCategoryInfo misproductcategory);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteKBL_MISProductCategoryInfo(int misproductcategoryId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        KBL_MISProductCategoryInfo GetKBL_MISProductCategoryInfo(int misproductcategoryId);

        [OperationContract]
        KBL_MISProductCategoryInfo[] GetAllKBL_MISProductCategoryInfo();


        #endregion

        #region IncomeCaptionPosition

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeCaptionPosition UpdateIncomeCaptionPosition(IncomeCaptionPosition incomecaption);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteIncomeCaptionPosition(int ID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeCaptionPosition GetIncomeCaptionPosition(int ID);

        [OperationContract]
        IncomeCaptionPosition[] GetAllIncomeCaptionPosition();

        #endregion

        #region GroupCaptions

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        GroupCaptions UpdateGroupCaptions(GroupCaptions groupCaptions);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteGroupCaptions(int GroupCaptionID);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        GroupCaptions GetGroupCaptions(int GroupCaptionID);

        [OperationContract]
        GroupCaptions[] GetAllGroupCaptions();

        #endregion

        #region Income Product ALT

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        IncomeProductsTableALT Updateincomeproducttablealt(IncomeProductsTableALT incomeproducttablealt);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void Deleteincomeproducttablealt(int productid);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        IncomeProductsTableALT Getincomeproducttablealt(int productid);

        [OperationContract]
        IncomeProductsTableALT[] GetAllincomeproducttablealt();

        [OperationContract]
        IncomeProductsTableALT[] GetincomeproducttablealtUsingSearchValue(string searchvalue);

        #endregion

    }
}
