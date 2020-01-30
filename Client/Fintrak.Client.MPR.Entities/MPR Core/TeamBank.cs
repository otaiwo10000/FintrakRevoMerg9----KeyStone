
using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class TeamBank : ObjectBase
    {
        public int TeamBankId { get; set; }
        public string Team_Code { get; set; }
        public string TeamName { get; set; }
        public string Branch_Code { get; set; }
        public string BranchName { get; set; }
        public string Zone_Code { get; set; }
        public string ZoneName { get; set; }
        public string Segment_Code { get; set; }
        public string SegmentName { get; set; }
        public string Group_Code { get; set; }
        public string GroupName { get; set; }
        public string Region_Code { get; set; }
        public string RegionName { get; set; }
        public string Division_Code { get; set; }
        public string DivisionName { get; set; }
        public string SBU_Code { get; set; }
        public string SBUName { get; set; }
        public string AccountOfficer_Code { get; set; }
        public string AccountOfficer_Name { get; set; }       
        public int Year { get; set; }
        public string StaffID { get; set; }


        //class TeamStructureValidator : AbstractValidator<TeamStructure>
        //{
        //    public TeamStructureValidator()
        //    {
        //        RuleFor(obj => obj.Accountofficer_Code).NotEmpty().WithMessage("Accountofficer code is required.");
        //        RuleFor(obj => obj.Team_Code).NotEmpty().WithMessage("Team Code is required.");
        //        RuleFor(obj => obj.Branch_Code).NotEmpty().WithMessage("Branch code is required.");
        //        RuleFor(obj => obj.Group_Code).NotEmpty().WithMessage("Group Code is required.");
        //        RuleFor(obj => obj.Region_Code).NotEmpty().WithMessage("Accountofficer code is required.");
        //        RuleFor(obj => obj.Division_Code).NotEmpty().WithMessage("Team Code is required.");
        //        RuleFor(obj => obj.DIRECTORATECODE).NotEmpty().WithMessage("Accountofficer code is required.");
        //        RuleFor(obj => obj.unit_code).NotEmpty().WithMessage("Team Code is required.");
        //        RuleFor(obj => obj.PPRCategory).NotEmpty().WithMessage("Accountofficer code is required.");
        //        RuleFor(obj => obj.staff_id).NotEmpty().WithMessage("Team Code is required.");
        //        RuleFor(obj => obj.Period).NotEmpty().WithMessage("Accountofficer code is required.");
        //        RuleFor(obj => obj.Year).NotEmpty().WithMessage("Team Code is required.");

        //    }
        //}

        //protected override IValidator GetValidator()
        //{
        //    return new TeamStructureValidator();
        //}
    }
}
