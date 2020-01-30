using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class IncomeProductMISCodeGetModel
    {
        //public int ID { get; set; }
        public string ProductCode { get; set; }
        public string CATEGORY_DESCRIPTION { get; set; }
        public string MIS_Code { get; set; }
        public string AccountOfficer_Code { get; set; }       
        
    }  
}