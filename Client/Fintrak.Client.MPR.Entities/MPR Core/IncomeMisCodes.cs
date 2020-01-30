using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeMisCodes : ObjectBase
    {
        int _ID;
        string _OldMIS_Code;
        string _NewMIS_Code;
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


        public string OldMIS_Code
        {
            get { return _OldMIS_Code; }
            set
            {
                if (_OldMIS_Code != value)
                {
                    _OldMIS_Code = value;
                    OnPropertyChanged(() => OldMIS_Code);
                }
            }
        }

        public string NewMIS_Code
        {
            get { return _NewMIS_Code; }
            set
            {
                if (_NewMIS_Code != value)
                {
                    _NewMIS_Code = value;
                    OnPropertyChanged(() => NewMIS_Code);
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


        class IncomeMisCodesValidator : AbstractValidator<IncomeMisCodes>
        {
            public IncomeMisCodesValidator()
            {
                RuleFor(obj => obj.OldMIS_Code).NotEmpty().WithMessage("OldMis_Code is required.");
                RuleFor(obj => obj.NewMIS_Code).NotEmpty().WithMessage("NewMis_Code is required.");
            


            }
        }

        protected override IValidator GetValidator()
        {
            return new IncomeMisCodesValidator();
        }

    }
}
