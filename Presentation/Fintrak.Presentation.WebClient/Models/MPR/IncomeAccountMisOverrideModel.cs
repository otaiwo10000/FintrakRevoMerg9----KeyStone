using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class IncomeAccountMisOverrideModel
    {
        public int Id { get; set; }
        public string accountnumber { get; set; }
        public string mis { get; set; }
        public string accountofficer_code { get; set; }
   
        public string username { get; set; }

   
    }

   
}