using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class IncomeAdjustmentCommFeesSearchModel
    {
        public int ID { get; set; }
        public string MIS_Code { get; set; }
        public string BranchCode { get; set; }
        public string Inc_Exp { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyType { get; set; }
        public string GL_Code { get; set; }
        public string RelatedAccount { get; set; }
        public string Narrative { get; set; }

        public int Period { get; set; }
        public int Year { get; set; }
        public string CustomerCode { get; set; }
        public string AccountOfficer_Code { get; set; }
        public string CustomerName { get; set; }
        public DateTime P_Date { get; set; }
        public string Caption { get; set; }
        public string Tran_ID { get; set; }
        public DateTime Tran_Date { get; set; }
        public string GLName { get; set; }
        public string EntryStatus { get; set; }
        public string GroupCaption { get; set; }
        public decimal Rate { get; set; }
        public decimal Raw_Amt { get; set; }
        public string Sub_Head_GL_Code { get; set; }
        public string ProductCode { get; set; }
        public string Trans_Code { get; set; }
        public string Ref_Num { get; set; }
        public string rcre_user_id { get; set; }
        public string entry_user_id { get; set; }
        public string Co_Dode { get; set; }
        public string Co_AO { get; set; }
        public int TranIDLEN { get; set; }
        public string T24Key { get; set; }

        public string username { get; set; }

   
    }

   
}