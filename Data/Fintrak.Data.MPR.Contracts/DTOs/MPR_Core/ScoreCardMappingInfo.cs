using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class ScoreCardMappingInfo
    {
        //public ScoreCardWeight ScoreCardWeight { get; set; }
        //// public ScoreCardMetrics ScoreCardMetrics { get; set; }
        //public string Metric { get; set; }
        //public int Metric_Code { get; set; }


        public int MappingId { get; set; }
        public int Metric_Code { get; set; }
        public string Actual_Caption { get; set; }
        public string Budget_Caption { get; set; }
        public int Period { get; set; }
        public int Year { get; set; }
        public int Mapping_code { get; set; }
        public string Metric { get; set; }

    }

    //public class ScoreCardWeightMetrics
    //{
    //    public int WeightId { get; set; }
    //    public int Metric_Code { get; set; }
    //    public decimal Weight { get; set; }
    //    public int Period { get; set; }
    //    public int Year { get; set; }
    //    public string Metric { get; set; }
    //}
}