using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class MprInterestMapping : ObjectBase
    {
        int _ID;
        string _GLCode;
        string _GLName;
        string _GLClass;
        bool _Ispartitioned;
        bool _Active;

        public int ID
        {
            get { return _ID; }
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    OnPropertyChanged(() => ID);
                }
            }
        }


        public string GLCode
        {
            get { return _GLCode; }
            set
            {
                if (_GLCode != value)
                {
                    _GLCode = value;
                    OnPropertyChanged(() => GLCode);
                }
            }
        }

        public string GLName
        {
            get { return _GLName; }
            set
            {
                if (_GLName != value)
                {
                    _GLName = value;
                    OnPropertyChanged(() => GLName);
                }
            }
        }

        public string GLClass
        {
            get { return _GLClass; }
            set
            {
                if (_GLClass != value)
                {
                    _GLClass = value;
                    OnPropertyChanged(() => GLClass);
                }
            }
        }

        public bool Ispartitioned
        {
            get { return _Ispartitioned; }
            set
            {
                if (_Ispartitioned != value)
                {
                    _Ispartitioned = value;
                    OnPropertyChanged(() => Ispartitioned);
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


        class MprInterestMappingValidator : AbstractValidator<MprInterestMapping>
        {
            public MprInterestMappingValidator()
            {
                //(obj => obj.Account).NotEmpty().WithMessage("Account is required.");
                //RuleFor(obj => obj.MISCode).NotEmpty().WithMessage("MISCode is required.");
            


            }
        }

        protected override IValidator GetValidator()
        {
            return new MprInterestMappingValidator();
        }

    }
}
