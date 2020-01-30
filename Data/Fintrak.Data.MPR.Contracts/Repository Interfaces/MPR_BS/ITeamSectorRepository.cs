using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.MPR.Contracts
{
    public interface ITeamSectorRepository : IDataRepository<TeamSector>
    {
        IEnumerable<TeamSector> GetTeamSectorbysearch(string search);

    }
}
