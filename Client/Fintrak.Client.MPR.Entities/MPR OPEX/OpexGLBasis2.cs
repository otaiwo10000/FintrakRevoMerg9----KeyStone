using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class OpexGLBasis2 : ObjectBase
    {
        int _ID;
        string _CAPTION;
        string _MIS_CODE;
        double _BASIS;
        string _NARRATIVE;
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

        public string CAPTION
        {
            get { return _CAPTION; }
            set
            {
                if (_CAPTION != value)
                {
                    _CAPTION = value;
                    OnPropertyChanged(() => CAPTION);
                }
            }
        }

        public string MIS_CODE
        {
            get { return _MIS_CODE; }
            set
            {
                if (_MIS_CODE != value)
                {
                    _MIS_CODE = value;
                    OnPropertyChanged(() => MIS_CODE);
                }
            }
        }


        public double BASIS
        {
            get { return _BASIS; }
            set
            {
                if (_BASIS != value)
                {
                    _BASIS = value;
                    OnPropertyChanged(() => BASIS);
                }
            }
        }

        public string NARRATIVE
        {
            get { return _NARRATIVE; }
            set
            {
                if (_NARRATIVE != value)
                {
                    _NARRATIVE = value;
                    OnPropertyChanged(() => NARRATIVE);
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


        
        class OpexGLBasis2Validator : AbstractValidator<OpexGLBasis2>
        {
            public OpexGLBasis2Validator()
            {
                //RuleFor(obj => obj.ServiceCode).NotEmpty().WithMessage("ServiceCode is required.");
                //RuleFor(obj => obj.ServiceDescription).NotEmpty().WithMessage("ServiceDescription is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new OpexGLBasis2Validator();
        }
    }
}
