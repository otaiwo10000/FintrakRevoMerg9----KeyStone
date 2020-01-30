using Fintrak.Client.Core.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.SystemCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class IncomeAccountsUnitModel
    {
        public int ID { get; set; }
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public string MIS_Code { get; set; }
   
    }

}