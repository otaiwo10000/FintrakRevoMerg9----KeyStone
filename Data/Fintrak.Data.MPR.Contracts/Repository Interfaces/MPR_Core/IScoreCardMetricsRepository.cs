using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.MPR.Contracts
{
    public interface IScoreCardMetricsRepository : IDataRepository<ScoreCardMetrics>
    {
        //IEnumerable<ScoreCardMetrics> GetMetricsBySearchValue(string searchvalue);
        //IEnumerable<ScoreCardMetrics> GetMetricsBySetUp();

        IEnumerable<ScoreCardMetricsInfo> GetMetricsBySearchValue(string searchvalue);
        IEnumerable<ScoreCardMetricsInfo> GetMetricsBySetUp();
       // IEnumerable<ScoreCardMetricsInfo> GetMetricsByParams(string metric, string miscode, string perspective, int period, int year);
    }
}
