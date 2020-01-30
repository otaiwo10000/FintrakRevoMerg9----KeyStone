
using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeCaptionPosition : ObjectBase
    {      
        public int ID { get; set; }    
        public string Caption { get; set; }
        public int position { get; set; }       
        public int Class { get; set; }    
        public string ReportColour { get; set; }
        public bool Visible { get; set; }
        public bool IsBreakdown { get; set; }
    }
}
