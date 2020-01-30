
using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class TeamStructureALL : ObjectBase
    {
        int _Team_StructureId;
       
        string _Accountofficer_Code;       
        string _AccountofficerName;      
        string _Team_Code;
        string _TeamName;
        string _Branch_Code;
        string _BranchName;
        string _Group_Code;
        string _GroupName;
        string _Zone_Code;
        string _ZoneName;
        string _Region_Code;
        string _RegionName;
        string _Division_Code;
        string _DivisionName;

        string _DIRECTORATECODE;
        string _DIRECTORATENAME;
        string _Segment_Code;
        string _SegmentName;
        string _SuperSegment_Code;
        string _SuperSegmentName;
        string _PPRCategory;
        string _Year;
        string _staff_id;
        string _unit_code;
        string _unitname;
        int _Period;

        string _ApprovalStatus;
        bool _Migrated;


        //ModuleOwnerType _ModuleOwnerType;

        public int Team_StructureId
        {
            get { return _Team_StructureId; }
            set
            {
                if (_Team_StructureId != value)
                {
                    _Team_StructureId = value;
                     OnPropertyChanged(() => Team_StructureId);
                }
            }
        }

        public string Accountofficer_Code
        {
            get { return _Accountofficer_Code; }
            set
            {
                if (_Accountofficer_Code != value)
                {
                    _Accountofficer_Code = value;
                    OnPropertyChanged(() => Accountofficer_Code);
                }
            }
        }

        public string AccountofficerName
        {
            get { return _AccountofficerName; }
            set
            {
                if (_AccountofficerName != value)
                {
                    _AccountofficerName = value;
                    OnPropertyChanged(() => AccountofficerName);
                }
            }
        }

        public string Team_Code
        {
            get { return _Team_Code; }
            set
            {
                if (_Team_Code != value)
                {
                    _Team_Code = value;
                    OnPropertyChanged(() => Team_Code);
                }
            }
        }

        public string TeamName
        {
            get { return _TeamName; }
            set
            {
                if (_TeamName != value)
                {
                    _TeamName = value;
                    OnPropertyChanged(() => TeamName);
                }
            }
        }

        public string Branch_Code
        {
            get { return _Branch_Code; }
            set
            {
                if (_Branch_Code != value)
                {
                    _Branch_Code = value;
                    OnPropertyChanged(() => Branch_Code);
                }
            }
        }

        public string BranchName
        {
            get { return _BranchName; }
            set
            {
                if (_BranchName != value)
                {
                    _BranchName = value;
                    OnPropertyChanged(() => BranchName);
                }
            }
        }

        public string Group_Code
        {
            get { return _Group_Code; }
            set
            {
                if (_Group_Code != value)
                {
                    _Group_Code = value;
                    OnPropertyChanged(() => Group_Code);
                }
            }
        }

        public string GroupName
        {
            get { return _GroupName; }
            set
            {
                if (_GroupName != value)
                {
                    _GroupName = value;
                    OnPropertyChanged(() => GroupName);
                }
            }
        }

        public string Zone_Code
        {
            get { return _Zone_Code; }
            set
            {
                if (_Zone_Code != value)
                {
                    _Zone_Code = value;
                    OnPropertyChanged(() => Zone_Code);
                }
            }
        }

        public string ZoneName
        {
            get { return _ZoneName; }
            set
            {
                if (_ZoneName != value)
                {
                    _ZoneName = value;
                    OnPropertyChanged(() => ZoneName);
                }
            }
        }

        public string Region_Code
        {
            get { return _Region_Code; }
            set
            {
                if (_Region_Code != value)
                {
                    _Region_Code = value;
                    OnPropertyChanged(() => Region_Code);
                }
            }
        }

        public string RegionName
        {
            get { return _RegionName; }
            set
            {
                if (_RegionName != value)
                {
                    _RegionName = value;
                    OnPropertyChanged(() => RegionName);
                }
            }
        }

        public string Division_Code
        {
            get { return _Division_Code; }
            set
            {
                if (_Division_Code != value)
                {
                    _Division_Code = value;
                    OnPropertyChanged(() => Division_Code);
                }
            }
        }

        public string DivisionName
        {
            get { return _DivisionName; }
            set
            {
                if (_DivisionName != value)
                {
                    _DivisionName = value;
                    OnPropertyChanged(() => DivisionName);
                }
            }
        }

        public string DIRECTORATECODE
        {
            get { return _DIRECTORATECODE; }
            set
            {
                if (_DIRECTORATECODE != value)
                {
                    _DIRECTORATECODE = value;
                    OnPropertyChanged(() => DIRECTORATECODE);
                }
            }
        }

        public string DIRECTORATENAME
        {
            get { return _DIRECTORATENAME; }
            set
            {
                if (_DIRECTORATENAME != value)
                {
                    _DIRECTORATENAME = value;
                    OnPropertyChanged(() => DIRECTORATENAME);
                }
            }
        }

        public string Segment_Code
        {
            get { return _Segment_Code; }
            set
            {
                if (_Segment_Code != value)
                {
                    _Segment_Code = value;
                    OnPropertyChanged(() => Segment_Code);
                }
            }
        }

        public string SegmentName
        {
            get { return _SegmentName; }
            set
            {
                if (_SegmentName != value)
                {
                    _SegmentName = value;
                    OnPropertyChanged(() => SegmentName);
                }
            }
        }

        public string SuperSegment_Code
        {
            get { return _SuperSegment_Code; }
            set
            {
                if (_SuperSegment_Code != value)
                {
                    _SuperSegment_Code = value;
                    OnPropertyChanged(() => SuperSegment_Code);
                }
            }
        }

        public string SuperSegmentName
        {
            get { return _SuperSegmentName; }
            set
            {
                if (_SuperSegmentName != value)
                {
                    _SuperSegmentName = value;
                    OnPropertyChanged(() => SuperSegmentName);
                }
            }
        }

        public string PPRCategory
        {
            get { return _PPRCategory; }
            set
            {
                if (_PPRCategory != value)
                {
                    _PPRCategory = value;
                    OnPropertyChanged(() => PPRCategory);
                }
            }
        }

        public string Year
        {
            get { return _Year; }
            set
            {
                if (_Year != value)
                {
                    _Year = value;
                    OnPropertyChanged(() => Year);
                }
            }
        }

        public string staff_id
        {
            get { return _staff_id; }
            set
            {
                if (_staff_id != value)
                {
                    _staff_id = value;
                    OnPropertyChanged(() => staff_id);
                }
            }
        }

        public string unit_code
        {
            get { return _unit_code; }
            set
            {
                if (_unit_code != value)
                {
                    _unit_code = value;
                    OnPropertyChanged(() => unit_code);
                }
            }
        }

        public string unitname
        {
            get { return _unitname; }
            set
            {
                if (_unitname != value)
                {
                    _unitname = value;
                    OnPropertyChanged(() => unitname);
                }
            }
        }

        public int Period
        {
            get { return _Period; }
            set
            {
                if (_Period != value)
                {
                    _Period = value;
                    OnPropertyChanged(() => Period);
                }
            }
        }

        public string ApprovalStatus
        {
            get { return _ApprovalStatus; }
            set
            {
                if (_ApprovalStatus != value)
                {
                    _ApprovalStatus = value;
                    OnPropertyChanged(() => ApprovalStatus);
                }
            }
        }

        public bool Migrated
        {
            get { return _Migrated; }
            set
            {
                if (_Migrated != value)
                {
                    _Migrated = value;
                    OnPropertyChanged(() => Migrated);
                }
            }
        }



        class TeamStructureALLValidator : AbstractValidator<TeamStructureALL>
        {
            public TeamStructureALLValidator()
            {
                //RuleFor(obj => obj.Accountofficer_Code).NotEmpty().WithMessage("Accountofficer code is required.");
                //RuleFor(obj => obj.Team_Code).NotEmpty().WithMessage("Team Code is required.");
                //RuleFor(obj => obj.Branch_Code).NotEmpty().WithMessage("Branch code is required.");
                //RuleFor(obj => obj.Group_Code).NotEmpty().WithMessage("Group Code is required.");
                //RuleFor(obj => obj.Region_Code).NotEmpty().WithMessage("Accountofficer code is required.");
                //RuleFor(obj => obj.Division_Code).NotEmpty().WithMessage("Team Code is required.");
                //RuleFor(obj => obj.DIRECTORATECODE).NotEmpty().WithMessage("Accountofficer code is required.");
                //RuleFor(obj => obj.unit_code).NotEmpty().WithMessage("Team Code is required.");
                //RuleFor(obj => obj.PPRCategory).NotEmpty().WithMessage("Accountofficer code is required.");
                //RuleFor(obj => obj.staff_id).NotEmpty().WithMessage("Team Code is required.");
                //RuleFor(obj => obj.Period).NotEmpty().WithMessage("Accountofficer code is required.");
                //RuleFor(obj => obj.Year).NotEmpty().WithMessage("Team Code is required.");

            }
        }

        protected override IValidator GetValidator()
        {
            return new TeamStructureALLValidator();
        }
    }
}
