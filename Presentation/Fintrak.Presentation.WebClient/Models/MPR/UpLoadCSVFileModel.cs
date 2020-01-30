using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class UpLoadCSVFileModel
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public string Status { get; set; }
        public string Treated { get; set; }
        public string UploadUser { get; set; }
        public DateTime DateTimeUploaded { get; set; }
        public DateTime ReadStart { get; set; }
        public DateTime ReadEnd { get; set; }
    }

}  