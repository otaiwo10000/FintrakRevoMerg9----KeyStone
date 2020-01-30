using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.MPR.Contracts
{
    public interface IScoreCardSetMetricTargetKBLRepository : IDataRepository<ScoreCardSetMetricTargetKBL>
    {
        
        IEnumerable<ScoreCardSetMetricTargetKBL> GetScoreCardSetMetricTargetKBLBySearchValue(string searchvalue);
        IEnumerable<ScoreCardSetMetricTargetKBL> GetScoreCardSetMetricTargetKBLByPeriodANDYear(int period, int year);

    }
}
