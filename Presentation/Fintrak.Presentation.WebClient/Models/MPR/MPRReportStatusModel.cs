using Fintrak.Client.Core.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.SystemCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class MPRReportStatusModel
    {
        public int MPRReportStatusId { get; set; }
        public int Year { get; set; }
        public string Period { get; set; }
        public string Status { get; set; }

    }
}