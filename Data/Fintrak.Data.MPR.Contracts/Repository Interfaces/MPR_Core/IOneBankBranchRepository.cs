using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.MPR.Contracts
{
    public interface IOneBankBranchRepository : IDataRepository<OneBankBranch>
    {
        IEnumerable<OneBankBranch> GetOneBankBranchByParams(string SearchValue, int year, int period);
    }
}
