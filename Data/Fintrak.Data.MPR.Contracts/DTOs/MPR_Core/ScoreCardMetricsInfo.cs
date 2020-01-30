using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class ScoreCardMetricsInfo
    {
        //public ScoreCardWeight ScoreCardWeight { get; set; }
        //// public ScoreCardMetrics ScoreCardMetrics { get; set; }
        //public string Metric { get; set; }
        //public int Metric_Code { get; set; }


        public int MetricId { get; set; }

        public string Metric { get; set; }

        public int Metric_Code { get; set; }

        public string Metric_Description { get; set; }

        public string MisCode { get; set; }

        public int Metric_Score_determinant { get; set; }

        public int Period { get; set; }

        public int Year { get; set; }

        public int PerspectiveId { get; set; }

        public int Metric_Position { get; set; }

        public int Mapping_Code { get; set; }

        public string perspective { get; set; }

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