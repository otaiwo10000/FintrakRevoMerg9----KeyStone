using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.MPR.Contracts
{
    public interface IIncomeCustomerRatingOverrideTEMPRepository : IDataRepository<IncomeCustomerRatingOverrideTEMP>
    {
        IEnumerable<IncomeCustomerRatingOverrideTEMP> GetOverrideByRefNumber(string refnumber);
        IEnumerable<IncomeCustomerRatingOverrideTEMP> ValidateByRefNumber(string refnumber);
    }
}
