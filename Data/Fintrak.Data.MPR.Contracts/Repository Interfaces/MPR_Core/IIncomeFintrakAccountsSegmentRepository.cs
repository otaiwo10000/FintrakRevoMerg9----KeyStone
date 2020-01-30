using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.MPR.Contracts
{
    public interface IIncomeFintrakAccountsSegmentRepository : IDataRepository<IncomeFintrakAccountsSegment>
    {
        IEnumerable<IncomeFintrakAccountsSegment> GetAccountsSegmentByCustomerIdBank(string customerId, string bank);
    }
}
