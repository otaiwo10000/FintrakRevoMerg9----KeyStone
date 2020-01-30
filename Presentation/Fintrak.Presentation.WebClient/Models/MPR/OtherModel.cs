using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class ExtractionProgressTableModel
    {
        public string PackageName { get; set; }
        public DateTime EstimatedTime { get; set; }
        public string Status { get; set; }
    }

    public class ExtractionProgressModel
    {
        public int TotalCount { get; set; }
        public int CompletedPkgCount { get; set; }
        public string PackageName { get; set; }
        //public DateTime EstimatedTime { get; set; }
        public string EstimatedTimeString { get; set; }
    }

    public class ProcessProgressTableModel
    {
        public string PackageName { get; set; }
        public DateTime EstimatedTime { get; set; }
        public string Status { get; set; }
    }

    public class ProcessProgressModel
    {
        public int TotalCount { get; set; }
        public int CompletedPkgCount { get; set; }
        public string PackageName { get; set; }
        //public DateTime EstimatedTime { get; set; }
        public string EstimatedTimeString { get; set; }
    }

    public class AccountCustomerModel
    {      
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }
    }

}