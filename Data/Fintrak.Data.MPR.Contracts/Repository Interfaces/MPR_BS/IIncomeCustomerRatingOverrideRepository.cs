using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.MPR.Contracts
{
    public interface IIncomeCustomerRatingOverrideRepository : IDataRepository<IncomeCustomerRatingOverride>
    {
        IEnumerable<IncomeCustomerRatingOverride> GetOverrideByRefNumber(string refnumber);
        IEnumerable<IncomeCustomerRatingOverride> ValidateByRefNumber(string refnumber);
    }
}
