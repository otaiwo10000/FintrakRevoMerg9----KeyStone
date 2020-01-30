using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class MISNewTEMP : ObjectBase
    {
        int _Id;
        string _State;
        string _Teamname;
        string _new_mis;
        string _old_mis;
        string _ApprovalStatus;
        bool _Active;

        public int Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    OnPropertyChanged(() => Id);
                }
            }
        }


        public string State
        {
            get { return _State; }
            set
            {
                if (_State != value)
                {
                    _State = value;
                    OnPropertyChanged(() => State);
                }
            }
        }


        public string Teamname
        {
            get { return _Teamname; }
            set
            {
                if (_Teamname != value)
                {
                    _Teamname = value;
                    OnPropertyChanged(() => Teamname);
                }
            }
        }

        public string new_mis
        {
            get { return _new_mis; }
            set
            {
                if (_new_mis != value)
                {
                    _new_mis = value;
                    OnPropertyChanged(() => new_mis);
                }
            }
        }

        public string old_mis
        {
            get { return _old_mis; }
            set
            {
                if (_old_mis != value)
                {
                    _old_mis = value;
                    OnPropertyChanged(() => old_mis);
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


        class MISNewTEMPValidator : AbstractValidator<MISNewTEMP>
        {
            public MISNewTEMPValidator()
            {
                //RuleFor(obj => obj.Percentage).NotEmpty().WithMessage("Percentage is required.");
                //RuleFor(obj => obj.Branch).NotEmpty().WithMessage("Branch is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new MISNewTEMPValidator();
        }

    }
}
