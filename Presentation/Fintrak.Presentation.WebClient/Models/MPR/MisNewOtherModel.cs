using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class MisNewOtherModel
    {
        public int Id { get; set; }
        public string Old_mis { get; set; }
        public string New_mis { get; set; }
        public string Teamname { get; set; }
        public string State { get; set; }
        public string Accountofficer_code { get; set; }

        public string username { get; set; }

   
    }

   
}