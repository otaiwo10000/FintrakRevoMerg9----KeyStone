using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.MPR.Contracts
{
    public interface IIncomeRetailProductOverrideTEMPRepository : IDataRepository<IncomeRetailProductOverrideTEMP>
    {
        IEnumerable<IncomeRetailProductOverrideTEMP> OverrideByCustomerIdAndBank(int customerId, string bank);
        IEnumerable<IncomeRetailProductOverrideTEMP> ValidateByCustomerIdAndBank(int customerId, string bank);
        IEnumerable<IncomeRetailProductOverrideTEMP> SearchByCustomerORMISORAcctOfficer(string search);

    }
}
