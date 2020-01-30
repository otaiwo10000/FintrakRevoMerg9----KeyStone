

using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Fintrak.Data.MPR.Contracts
{
    public interface ITeamStructureRepository : IDataRepository<TeamStructure>
    {
        IEnumerable<TeamStructure> GetTeamstructureByParams(string SearchValue, string year);

        IEnumerable<TeamStructure> GetTeamstructureBySetUp();

        IEnumerable<TeamStructure> GetTeamstructureByDefinitionCode(string code);

        IEnumerable<TeamStructure> GetTeamstructureByParamsAndeSetUp(string code, string SearchValue);
        IEnumerable<TeamStructure> GetTeamstructureByParameters(string selectedDefinitionCode, string SearchValue, string year);
        IEnumerable<TeamStructure> GetTeamstructureByDefinitionCodeMonthly(string code);
        IEnumerable<TeamStructure> GetTeamstructureByParamsAndeSetUpMonthly(string code, string SearchValue);
        IEnumerable<TeamStructure> GetTeamstructureBySetUpMonthly();
       // TeamStructure GetTeamStructureTop1(string branch, string year);
        TeamStructure GetTeamStructureTop1(string branch, string defcode, string year);
        
    }
}
