using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Data.MPR.Contracts
{
    public interface IMISTransferPriceRepository : IDataRepository<MISTransferPrice>
    {
        IEnumerable<MISTransferPriceInfo> GetMISTransferPricebySetUp();

        IEnumerable<MISTransferPriceInfo> GetMISTransferPricebyParams(string defCode, string miscode, string category, string currency, int year, int period);
       
    }
}
