using System;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Shared.Common.ServiceModel;
using System.Collections.Generic;

namespace Fintrak.Client.MPR.Proxies
{
    [Export(typeof(IMPRIncomeService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MPRIncomeClient : UserClientBase<IMPRIncomeService>, IMPRIncomeService
    {
        public void RegisterModule()
        {
            Channel.RegisterModule();
        }


        #region Income Product

        public IncomeProductsTable Updateincomeproducttable(IncomeProductsTable incomeproducttable)
        {
            return Channel.Updateincomeproducttable(incomeproducttable);
        }

        public void Deleteincomeproducttable(int productid)
        {
            Channel.Deleteincomeproducttable(productid);
        }

        public IncomeProductsTable Getincomeproducttable(int productid)
        {
            return Channel.Getincomeproducttable(productid);
        }

        public IncomeProductsTable[] GetAllincomeproducttable()
        {
            return Channel.GetAllincomeproducttable();
        }

        public IncomeProductsTable[] GetincomeproducttableUsingSearchValue(string searchvalue)
        {
            return Channel.GetincomeproducttableUsingSearchValue(searchvalue);
        }

        #endregion

        #region Caption

        public Caption UpdateCaption(Caption caption)
        {
            return Channel.UpdateCaption(caption);
        }

        public void DeleteCaption(int CaptionId)
        {
            Channel.DeleteCaption(CaptionId);
        }

        public Caption GetCaption(int CaptionId)
        {
            return Channel.GetCaption(CaptionId);
        }

        public Caption[] GetAllCaptions()
        {
            return Channel.GetAllCaptions();
        }

        #endregion

        #region PLCaption2

        public PLCaption2 UpdatePLCaption2(PLCaption2 plcaption)
        {
            return Channel.UpdatePLCaption2(plcaption);
        }

        public void DeletePLCaption2(int PLCaptionId)
        {
            Channel.DeletePLCaption2(PLCaptionId);
        }

        public PLCaption2 GetPLCaption2(int PLCaptionId)
        {
            return Channel.GetPLCaption2(PLCaptionId);
        }

        public PLCaption2[] GetAllPLCaption2()
        {
            return Channel.GetAllPLCaption2();
        }

        #endregion

        #region PPRCaption

        public PPRCaption UpdatePPRCaption(PPRCaption pprcaption)
        {
            return Channel.UpdatePPRCaption(pprcaption);
        }

        public void DeletePPRCaption(int PPRCaptionId)
        {
            Channel.DeletePPRCaption(PPRCaptionId);
        }

        public PPRCaption GetPPRCaption(int PPRCaptionId)
        {
            return Channel.GetPPRCaption(PPRCaptionId);
        }

        public PPRCaption[] GetAllPPRCaption()
        {
            return Channel.GetAllPPRCaption();
        }

        #endregion

        #region Income CommFee Line Caption

        public IncomeCommFeeLineCaption UpdateIncomeCommFeeLineCaption(IncomeCommFeeLineCaption ICFLcaption)
        {
            return Channel.UpdateIncomeCommFeeLineCaption(ICFLcaption);
        }

        public void DeleteIncomeCommFeeLineCaption(int ICFLcaptionId)
        {
            Channel.DeleteIncomeCommFeeLineCaption(ICFLcaptionId);
        }

        public IncomeCommFeeLineCaption GetIncomeCommFeeLineCaption(int ICFLcaptionId)
        {
            return Channel.GetIncomeCommFeeLineCaption(ICFLcaptionId);
        }

        public IncomeCommFeeLineCaption[] GetAllIncomeCommFeeLineCaption()
        {
            return Channel.GetAllIncomeCommFeeLineCaption();
        }

        public IncomeCommFeeLineCaption[] GetIncomeCommFeeLineCaptionUsingSearchValue(string searchvalue)
        {
            return Channel.GetIncomeCommFeeLineCaptionUsingSearchValue(searchvalue);
        }

        #endregion

        #region Income CommFee Line Caption

        public IncomeLineCapton UpdateIncomeLineCapton(IncomeLineCapton ilcaption)
        {
            return Channel.UpdateIncomeLineCapton(ilcaption);
        }

        public void DeleteIncomeLineCapton(int ilCaptionId)
        {
            Channel.DeleteIncomeLineCapton(ilCaptionId);
        }

        public IncomeLineCapton GetIncomeLineCapton(int ilCaptionId)
        {
            return Channel.GetIncomeLineCapton(ilCaptionId);
        }

        public IncomeLineCapton[] GetAllIncomeLineCaptons()
        {
            return Channel.GetAllIncomeLineCaptons();
        }

        #endregion

        #region Income Products table Unit

        public IncomeProductstableUnit UpdateIncomeProductstableUnit(IncomeProductstableUnit iptUnit)
        {
            return Channel.UpdateIncomeProductstableUnit(iptUnit);
        }

        public void DeleteIncomeProductstableUnit(int iptUnitId)
        {
            Channel.DeleteIncomeProductstableUnit(iptUnitId);
        }

        public IncomeProductstableUnit GetIncomeProductstableUnit(int iptUnitId)
        {
            return Channel.GetIncomeProductstableUnit(iptUnitId);
        }

        public IncomeProductstableUnit[] GetAllIncomeProductstableUnits()
        {
            return Channel.GetAllIncomeProductstableUnits();
        }

        public IncomeProductstableUnit[] GetincomeproducttableunitUsingSearchValue(string searchvalue)
        {
            return Channel.GetincomeproducttableunitUsingSearchValue(searchvalue);
        }

        #endregion

        #region Income Products Table Treasury

        public IncomeProductsTableTreasury UpdateIncomeProductsTableTreasury(IncomeProductsTableTreasury iptTreasury)
        {
            return Channel.UpdateIncomeProductsTableTreasury(iptTreasury);
        }

        public void DeleteIncomeProductsTableTreasury(int iptTreasuryId)
        {
            Channel.DeleteIncomeProductsTableTreasury(iptTreasuryId);
        }

        public IncomeProductsTableTreasury GetIncomeProductsTableTreasury(int iptTreasuryId)
        {
            return Channel.GetIncomeProductsTableTreasury(iptTreasuryId);
        }

        public IncomeProductsTableTreasury[] GetAllIncomeProductsTableTreasury()
        {
            return Channel.GetAllIncomeProductsTableTreasury();
        }

        public IncomeProductsTableTreasury[] GetIncomeProductsTableTreasuryUsingSearchValue(string searchvalue)
        {
            return Channel.GetIncomeProductsTableTreasuryUsingSearchValue(searchvalue);
        }

        #endregion

        #region Income NEA Mapping
        public IncomeNEAMapping UpdateIncomeNEAMapping(IncomeNEAMapping incomeNEAmapping)
        {
            return Channel.UpdateIncomeNEAMapping(incomeNEAmapping);
        }

        public void DeleteIncomeNEAMapping(int incomeNEAmappingId)
        {
            Channel.DeleteIncomeNEAMapping(incomeNEAmappingId);
        }

        public IncomeNEAMapping GetIncomeNEAMapping(int incomeNEAmappingId)
        {
            return Channel.GetIncomeNEAMapping(incomeNEAmappingId);
        }

        public IncomeNEAMapping[] GetAllIncomeNEAMapping()
        {
            return Channel.GetAllIncomeNEAMapping();
        }

        public IncomeNEAMappingData[] GetIncomeNEAMappingUsingSearchValue(string searchvalue)
        {
            return Channel.GetIncomeNEAMappingUsingSearchValue(searchvalue);
        }

        public IncomeNEAMappingData[] GetFullIncomeNEAMapping()
        {
            return Channel.GetFullIncomeNEAMapping();
        }

        #endregion

        #region KBL MIS Product Category Info
        public KBL_MISProductCategoryInfo UpdateKBL_MISProductCategoryInfo(KBL_MISProductCategoryInfo misproductcategory)
        {
            return Channel.UpdateKBL_MISProductCategoryInfo(misproductcategory);
        }

        public void DeleteKBL_MISProductCategoryInfo(int misproductcategoryId)
        {
            Channel.DeleteKBL_MISProductCategoryInfo(misproductcategoryId);
        }

        public KBL_MISProductCategoryInfo GetKBL_MISProductCategoryInfo(int misproductcategoryId)
        {
            return Channel.GetKBL_MISProductCategoryInfo(misproductcategoryId);
        }

        public KBL_MISProductCategoryInfo[] GetAllKBL_MISProductCategoryInfo()
        {
            return Channel.GetAllKBL_MISProductCategoryInfo();
        }

        #endregion

        #region IncomeCaptionPosition

        public IncomeCaptionPosition UpdateIncomeCaptionPosition(IncomeCaptionPosition incomecaption)
        {
            return Channel.UpdateIncomeCaptionPosition(incomecaption);
        }

        public void DeleteIncomeCaptionPosition(int ID)
        {
            Channel.DeleteIncomeCaptionPosition(ID);
        }

        public IncomeCaptionPosition GetIncomeCaptionPosition(int ID)
        {
            return Channel.GetIncomeCaptionPosition(ID);
        }

        public IncomeCaptionPosition[] GetAllIncomeCaptionPosition()
        {
            return Channel.GetAllIncomeCaptionPosition();
        }

        #endregion

        #region GroupCaptions

        public GroupCaptions UpdateGroupCaptions(GroupCaptions groupCaptions)
        {
            return Channel.UpdateGroupCaptions(groupCaptions);
        }

        public void DeleteGroupCaptions(int GroupCaptionID)
        {
            Channel.DeleteGroupCaptions(GroupCaptionID);
        }

        public GroupCaptions GetGroupCaptions(int GroupCaptionID)
        {
            return Channel.GetGroupCaptions(GroupCaptionID);
        }

        public GroupCaptions[] GetAllGroupCaptions()
        {
            return Channel.GetAllGroupCaptions();
        }

        #endregion

        #region Income Product ALT

        public IncomeProductsTableALT Updateincomeproducttablealt(IncomeProductsTableALT incomeproducttablealt)
        {
            return Channel.Updateincomeproducttablealt(incomeproducttablealt);
        }

        public void Deleteincomeproducttablealt(int productid)
        {
            Channel.Deleteincomeproducttablealt(productid);
        }

        public IncomeProductsTableALT Getincomeproducttablealt(int productid)
        {
            return Channel.Getincomeproducttablealt(productid);
        }

        public IncomeProductsTableALT[] GetAllincomeproducttablealt()
        {
            return Channel.GetAllincomeproducttablealt();
        }

        public IncomeProductsTableALT[] GetincomeproducttablealtUsingSearchValue(string searchvalue)
        {
            return Channel.GetincomeproducttablealtUsingSearchValue(searchvalue);
        }

        #endregion

    }
}
