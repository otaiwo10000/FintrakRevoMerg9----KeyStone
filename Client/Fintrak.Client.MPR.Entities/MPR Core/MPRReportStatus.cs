using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class MPRReportStatus : ObjectBase
    {
        int _MPRReportStatusId;
        int _Year;
        // int _Period;
        string _Period;
        string _Status;
        bool _Active;

        public int MPRReportStatusId
        {
            get { return _MPRReportStatusId; }
            set
            {
                if (_MPRReportStatusId != value)
                {
                    _MPRReportStatusId = value;
                    OnPropertyChanged(() => MPRReportStatusId);
                }
            }
        }


        public int Year
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


        //public int Period
        public string Period
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

        public string Status
        {
            get { return _Status; }
            set
            {
                if (_Status != value)
                {
                    _Status = value;
                    OnPropertyChanged(() => Status);
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


        class MPRReportStatusValidator : AbstractValidator<MPRReportStatus>
        {
            public MPRReportStatusValidator()
            {
                //RuleFor(obj => obj.Percentage).NotEmpty().WithMessage("Percentage is required.");
                //RuleFor(obj => obj.Branch).NotEmpty().WithMessage("Branch is required.");

            }
        }

        protected override IValidator GetValidator()
        {
            return new MPRReportStatusValidator();
        }

    }
}
