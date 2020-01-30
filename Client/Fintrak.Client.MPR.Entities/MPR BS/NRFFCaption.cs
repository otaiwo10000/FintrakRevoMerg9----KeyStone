using System;
using System.Linq;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class NRFFCaption : ObjectBase
    {
        int _NRFFCaption_Id;
        string _Description;
        bool _Active;

        public int NRFFCaption_Id
        {
            get { return _NRFFCaption_Id; }
            set
            {
                if (_NRFFCaption_Id != value)
                {
                    _NRFFCaption_Id = value;
                    OnPropertyChanged(() => NRFFCaption_Id);
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

        
        class NRFFCaptionValidator : AbstractValidator<NRFFCaption>
        {
            public NRFFCaptionValidator()
            {
                RuleFor(obj => obj.Description).NotEmpty().WithMessage("Description is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new NRFFCaptionValidator();
        }
    }
}
