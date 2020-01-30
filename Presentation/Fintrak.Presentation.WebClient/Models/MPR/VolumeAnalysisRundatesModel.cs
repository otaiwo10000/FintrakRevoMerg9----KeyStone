using Fintrak.Client.Core.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.SystemCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class VolumeAnalysisRundatesModel
    {
        public int Id { get; set; }
        public DateTime rundate { get; set; }
        public string visible { get; set; }
   
    }
}