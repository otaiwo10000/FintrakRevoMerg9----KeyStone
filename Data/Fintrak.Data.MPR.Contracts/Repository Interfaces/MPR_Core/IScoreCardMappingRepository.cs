using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.MPR.Contracts
{
    public interface IScoreCardMappingRepository : IDataRepository<ScoreCardMapping>
    {
        //IEnumerable<ScoreCardMapping> GetMappingBySearchValue(string searchvalue);
        //IEnumerable<ScoreCardMapping> GetMappingBySetUp();

        IEnumerable<ScoreCardMappingInfo> GetMappingBySearchValue(string searchvalue);
        IEnumerable<ScoreCardMappingInfo> GetMappingBySetUp();
    }
}
