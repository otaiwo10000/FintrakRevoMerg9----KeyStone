using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class FinstatMapping : ObjectBase
    {
        int _FinstatMappingId;
        string _GLSH;
        string _Description;
        string _MainCaption;
        string _SubCaption;
        string _SubSubCaption;
        int _Class;
        string _RefNote;
        int _Position;
        string _PARENTGL;
        int _SubPosition;

        public int FinstatMappingId
        {
            get { return _FinstatMappingId; }
            set
            {
                if (_FinstatMappingId != value)
                {
                    _FinstatMappingId = value;
                    OnPropertyChanged(() => FinstatMappingId);
                }
            }
        }


        public string GLSH
        {
            get { return _GLSH; }
            set
            {
                if (_GLSH != value)
                {
                    _GLSH = value;
                    OnPropertyChanged(() => GLSH);
                }
            }
        }


        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged(() => Description);
                }
            }
        }


        public string MainCaption
        {
            get { return _MainCaption; }
            set
            {
                if (_MainCaption != value)
                {
                    _MainCaption = value;
                    OnPropertyChanged(() => MainCaption);
                }
            }
        }

        public string SubCaption
        {
            get { return _SubCaption; }
            set
            {
                if (_SubCaption != value)
                {
                    _SubCaption = value;
                    OnPropertyChanged(() => SubCaption);
                }
            }
        }

        public string SubSubCaption
        {
            get { return _SubSubCaption; }
            set
            {
                if (_SubSubCaption != value)
                {
                    _SubSubCaption = value;
                    OnPropertyChanged(() => SubSubCaption);
                }
            }
        }

        public int Class
        {
            get { return _Class; }
            set
            {
                if (_Class != value)
                {
                    _Class = value;
                    OnPropertyChanged(() => Class);
                }
            }
        }

        public string RefNote
        {
            get { return _RefNote; }
            set
            {
                if (_RefNote != value)
                {
                    _RefNote = value;
                    OnPropertyChanged(() => RefNote);
                }
            }
        }

        public int Position
        {
            get { return _Position; }
            set
            {
                if (_Position != value)
                {
                    _Position = value;
                    OnPropertyChanged(() => Position);
                }
            }
        }

        public string PARENTGL
        {
            get { return _PARENTGL; }
            set
            {
                if (_PARENTGL != value)
                {
                    _PARENTGL = value;
                    OnPropertyChanged(() => PARENTGL);
                }
            }
        }

        public int SubPosition
        {
            get { return _SubPosition; }
            set
            {
                if (_SubPosition != value)
                {
                    _SubPosition = value;
                    OnPropertyChanged(() => SubPosition);
                }
            }
        }

        class FinstatMappingValidator : AbstractValidator<FinstatMapping>
        {
            public FinstatMappingValidator()
            {
                RuleFor(obj => obj.Description).NotEmpty().WithMessage("Description is required.");
                RuleFor(obj => obj.MainCaption).NotEmpty().WithMessage("MainCaption is required.");
                RuleFor(obj => obj.SubCaption).NotEmpty().WithMessage("SubCaption is required.");
                RuleFor(obj => obj.SubSubCaption).NotEmpty().WithMessage("SubSubCaption is required.");
                RuleFor(obj => obj.Class).NotEmpty().WithMessage("Class is required.");
                RuleFor(obj => obj.RefNote).NotEmpty().WithMessage("RefNote is required.");
                RuleFor(obj => obj.Position).NotEmpty().WithMessage("Position is required.");
                RuleFor(obj => obj.PARENTGL).NotEmpty().WithMessage("PARENTGL is required.");
                RuleFor(obj => obj.SubPosition).NotEmpty().WithMessage("SubPosition is required.");

            }
        }

        protected override IValidator GetValidator()
        {
            return new FinstatMappingValidator();
        }

    }
}
