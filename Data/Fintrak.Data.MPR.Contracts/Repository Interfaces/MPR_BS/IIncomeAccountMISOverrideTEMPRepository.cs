using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.MPR.Contracts
{
    public interface IIncomeAccountMISOverrideTEMPRepository : IDataRepository<IncomeAccountMISOverrideTEMP>
    {
        IEnumerable<IncomeAccountMISOverrideTEMP> OverrideByAccountNumber(string accountno);
        IEnumerable<IncomeAccountMISOverrideTEMP> ValidateByAccountNumber2(string accountno);
        IEnumerable<IncomeAccountMISOverrideTEMP> SearchByAccountNoORMISORAcctOfficer(string search);
    }
}
