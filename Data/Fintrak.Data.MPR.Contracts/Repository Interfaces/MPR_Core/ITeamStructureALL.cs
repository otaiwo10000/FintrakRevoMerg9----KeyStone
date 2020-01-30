

using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Fintrak.Data.MPR.Contracts
{
    public interface ITeamStructureALLRepository : IDataRepository<TeamStructureALL>
    {
        IEnumerable<TeamStructureALL> GetTeamStructureALLByParams(string SearchValue, int year);

        IEnumerable<TeamStructureALL> GetTeamStructureALLBySetUp();

        IEnumerable<TeamStructureALL> GetTeamStructureALLByDefinitionCode(string code);

        IEnumerable<TeamStructureALL> GetTeamStructureALLByParamsAndeSetUp(string code, string SearchValue);
        IEnumerable<TeamStructureALL> GetTeamStructureALLByParameters(string selectedDefinitionCode, string SearchValue, int year);
        IEnumerable<TeamStructureALL> GetTeamStructureALLByDefinitionCodeMonthly(string code);
        IEnumerable<TeamStructureALL> GetTeamStructureALLByParamsAndeSetUpMonthly(string code, string SearchValue);
        IEnumerable<TeamStructureALL> GetTeamStructureALLBySetUpMonthly();
        // TeamStructure GetTeamStructureTop1(string branch, string year);
        TeamStructureALL GetTeamStructureALLTop1(string branch, string defcode, int year);

    }
}
