using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class OpexMemounitPlmap : ObjectBase
    {
        int _ID;
        string _GL_CODE;
        string _MEMO_MIS_CODE;
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

        public string GL_CODE
        {
            get { return _GL_CODE; }
            set
            {
                if (_GL_CODE != value)
                {
                    _GL_CODE = value;
                    OnPropertyChanged(() => GL_CODE);
                }
            }
        }

        public string MEMO_MIS_CODE
        {
            get { return _MEMO_MIS_CODE; }
            set
            {
                if (_MEMO_MIS_CODE != value)
                {
                    _MEMO_MIS_CODE = value;
                    OnPropertyChanged(() => MEMO_MIS_CODE);
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


        
        class OpexMemounitPlmapValidator : AbstractValidator<OpexMemounitPlmap>
        {
            public OpexMemounitPlmapValidator()
            {
                //RuleFor(obj => obj.ServiceCode).NotEmpty().WithMessage("ServiceCode is required.");
                //RuleFor(obj => obj.ServiceDescription).NotEmpty().WithMessage("ServiceDescription is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new OpexMemounitPlmapValidator();
        }
    }
}
