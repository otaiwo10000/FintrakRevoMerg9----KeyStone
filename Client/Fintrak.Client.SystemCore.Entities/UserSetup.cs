using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.SystemCore.Entities
{
    public class UserSetup : ObjectBase
    {
        int _UserSetupId;
        string _LoginID;
        string _Name;
        string _Email;
        string _StaffID;
        string _PhotoUrl;
        byte[] _Photo;
        bool _IsApplicationUser;
        bool _IsReportUser;
        bool _MultiCompanyAccess;
        DateTime _LatestConnection;
        string _CompanyCode;

        string _Mis_Code;
        string _Grade;
        string _ManagerID;
        string _Segment;
        DateTime _DateEmployed;
        bool _Active;

        public int UserSetupId
        {
            get { return _UserSetupId; }
            set
            {
                if (_UserSetupId != value)
                {
                    _UserSetupId = value;
                    OnPropertyChanged(() => UserSetupId);
                }
            }
        }

        public string LoginID
        {
            get { return _LoginID; }
            set
            {
                if (_LoginID != value)
                {
                    _LoginID = value;
                    OnPropertyChanged(() => LoginID);
                }
            }
        }

        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }

        public string Email
        {
            get { return _Email; }
            set
            {
                if (_Email != value)
                {
                    _Email = value;
                    OnPropertyChanged(() => Email);
                }
            }
        }

        public string StaffID
        {
            get { return _StaffID; }
            set
            {
                if (_StaffID != value)
                {
                    _StaffID = value;
                    OnPropertyChanged(() => StaffID);
                }
            }
        }

        public string PhotoUrl
        {
            get { return _PhotoUrl; }
            set
            {
                if (_PhotoUrl != value)
                {
                    _PhotoUrl = value;
                    OnPropertyChanged(() => PhotoUrl);
                }
            }
        }

        public byte[] Photo
        {
            get { return _Photo; }
            set
            {
                if (_Photo != value)
                {
                    _Photo = value;
                    OnPropertyChanged(() => Photo);
                }
            }
        }

        public bool MultiCompanyAccess
        {
            get { return _MultiCompanyAccess; }
            set
            {
                if (_MultiCompanyAccess != value)
                {
                    _MultiCompanyAccess = value;
                    OnPropertyChanged(() => MultiCompanyAccess);
                }
            }
        }

        public bool IsApplicationUser
        {
            get { return _IsApplicationUser; }
            set
            {
                if (_IsApplicationUser != value)
                {
                    _IsApplicationUser = value;
                    OnPropertyChanged(() => IsApplicationUser);
                }
            }
        }

        public bool IsReportUser
        {
            get { return _IsReportUser; }
            set
            {
                if (_IsReportUser != value)
                {
                    _IsReportUser = value;
                    OnPropertyChanged(() => IsReportUser);
                }
            }
        }

        public DateTime LatestConnection
        {
            get { return _LatestConnection; }
            set
            {
                if (_LatestConnection != value)
                {
                    _LatestConnection = value;
                    OnPropertyChanged(() => LatestConnection);
                }
            }
        }

        public string CompanyCode
        {
            get { return _CompanyCode; }
            set
            {
                if (_CompanyCode != value)
                {
                    _CompanyCode = value;
                    OnPropertyChanged(() => CompanyCode);
                }
            }
        }

        public string Mis_Code
        {
            get { return _Mis_Code; }
            set
            {
                if (_Mis_Code != value)
                {
                    _Mis_Code = value;
                    OnPropertyChanged(() => Mis_Code);
                }
            }
        }

        public string Grade
        {
            get { return _Grade; }
            set
            {
                if (_Grade != value)
                {
                    _Grade = value;
                    OnPropertyChanged(() => Grade);
                }
            }
        }

        public string ManagerID
        {
            get { return _ManagerID; }
            set
            {
                if (_ManagerID != value)
                {
                    _ManagerID = value;
                    OnPropertyChanged(() => ManagerID);
                }
            }
        }

        public string Segment
        {
            get { return _Segment; }
            set
            {
                if (_Segment != value)
                {
                    _Segment = value;
                    OnPropertyChanged(() => Segment);
                }
            }
        }

        public DateTime DateEmployed
        {
            get { return _DateEmployed; }
            set
            {
                if (_DateEmployed != value)
                {
                    _DateEmployed = value;
                    OnPropertyChanged(() => DateEmployed);
                }
            }
        }

        public bool Active
        {
            get { return _Active; }
            set
            {
                if (_Active != value)
                {
                    _Active = value;
                    OnPropertyChanged(() => Active);
                }
            }
        }

        public string LongDescription
        {
            get
            {
                return string.Format("{0} - {1}", _LoginID, _Name );
            }
        }

        class UserSetupValidator : AbstractValidator<UserSetup>
        {
            public UserSetupValidator()
            {
                RuleFor(obj => obj.LoginID).NotEmpty().WithMessage("LoginID must not be empty.");
                RuleFor(obj => obj.Name).NotEmpty().WithMessage("The Name must not be empty.");
                RuleFor(obj => obj.Email).NotEmpty().WithMessage("Email must not be empty.")
                                         .EmailAddress().WithMessage("Enter a valid email.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new UserSetupValidator();
        }
    }
}
