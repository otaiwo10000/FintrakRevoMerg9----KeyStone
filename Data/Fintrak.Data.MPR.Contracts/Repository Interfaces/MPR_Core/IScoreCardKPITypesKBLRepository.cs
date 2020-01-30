
using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.MPR.Contracts
{
    public interface IScoreCardKPITypesKBLRepository : IDataRepository<ScoreCardKPITypesKBL>
    {
        IEnumerable<ScoreCardKPITypesKBL> GetScoreCardKPITypesKBLBySearchValue(string searchvalue);
        IEnumerable<ScoreCardKPITypesKBL> GetScoreCardKPITypesKBLByPeriodYearKPIType(int period, int year, string searchvalue);

    }
}