using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.MPR.Contracts
{
    public interface IIncomeCustomerPoolRateRepository : IDataRepository<IncomeCustomerPoolRate>
    {
        IEnumerable<IncomeCustomerPoolRate> GetIncomeCustomerPoolRateBySearchValue(string searchvalue);
        IncomeCustomerPoolRate ValidateIncomeCustomerPoolRate(string customernumber, int year);
    }
}
