using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.MPR.Contracts
{
    public interface IIncomeCommFeeLineCaptionRepository : IDataRepository<IncomeCommFeeLineCaption>
    {
        IEnumerable<IncomeCommFeeLineCaption> GetIncomeCommFeeLineCaptionBySearchValue(string searchvalue);
        //IEnumerable<ScoreCardMetrics> GetMetricsBySetUp();


        //IEnumerable<ScoreCardMetricsInfo> GetMetricsBySetUp();
       // IEnumerable<ScoreCardMetricsInfo> GetMetricsByParams(string metric, string miscode, string perspective, int period, int year);
    }
}
