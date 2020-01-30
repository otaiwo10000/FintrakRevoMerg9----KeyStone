using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class DimAuditModel
    {
        public int ID { get; set; }
        public string TableName { get; set; }
        public string PkgName { get; set; }
        public DateTime ExecStartDT { get; set; }
        public DateTime ExecStopDT { get; set; }
        public Int64 ExtractRowCnt { get; set; }
        public Int64 InsertRowCnt { get; set; }
        public Int64 UpdateRowCnt { get; set; }
        public Int64 ErrorRowCnt { get; set; }
        public Int64 TableInitialRowCnt { get; set; }
        public Int64 TableFinalRowCnt { get; set; }
        public string SuccessfulProcessingInd { get; set; }

        public string username { get; set; }

   
    }

   
}