using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class OpexGLMap : ObjectBase
    {
        int _Id;
        string _ACCOUNTNUMBER;
        string _ACCOUNT_TITLE;
            
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


        public string ACCOUNTNUMBER
        {
            get { return _ACCOUNTNUMBER; }
            set
            {
                if (_ACCOUNTNUMBER != value)
                {
                    _ACCOUNTNUMBER = value;
                    OnPropertyChanged(() => ACCOUNTNUMBER);
                }
            }
        }

        public string ACCOUNT_TITLE
        {
            get { return _ACCOUNT_TITLE; }
            set
            {
                if (_ACCOUNT_TITLE != value)
                {
                    _ACCOUNT_TITLE = value;
                    OnPropertyChanged(() => ACCOUNT_TITLE);
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

      
        //class OpexBasisMappingValidator : AbstractValidator<OpexBasisMapping>
        //{
        //    public OpexBasisMappingValidator()
        //    {
        //        RuleFor(obj => obj.GLCode).NotEmpty().WithMessage("GLCode is required.");
        //        RuleFor(obj => obj.Description).NotEmpty().WithMessage("Description is required.");
        //    }
        //}

        //protected override IValidator GetValidator()
        //{
        //    return new OpexBasisMappingValidator();
        //}
    }
}
