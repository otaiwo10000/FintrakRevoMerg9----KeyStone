using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class IncomeAccountsListingModel
    {
        public int Id { get; set; }
        public string ACCOUNTNUMBER { get; set; }
        public string CustomerName { get; set; }
        public string MIS_CODE { get; set; }
        public string BranchCode { get; set; }
        public string accountofficer_code { get; set; }
        public string Team_branch { get; set; }
        public DateTime Date_Open { get; set; }

        public string username { get; set; }

   
    }

   
}