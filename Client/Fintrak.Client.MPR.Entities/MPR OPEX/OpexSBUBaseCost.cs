using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class OpexSBUBaseCost : ObjectBase
    {
        int _ID;
        string _MIS_CODE;
        double _AMOUNT;
        string _TEMPLATE;
        string _SOURCE;
        int _SN;
        string _TRANS_TYPE;
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

        public double AMOUNT
        {
            get { return _AMOUNT; }
            set
            {
                if (_AMOUNT != value)
                {
                    _AMOUNT = value;
                    OnPropertyChanged(() => AMOUNT);
                }
            }
        }

        public string TEMPLATE
        {
            get { return _TEMPLATE; }
            set
            {
                if (_TEMPLATE != value)
                {
                    _TEMPLATE = value;
                    OnPropertyChanged(() => TEMPLATE);
                }
            }
        }

        public string SOURCE
        {
            get { return _SOURCE; }
            set
            {
                if (_SOURCE != value)
                {
                    _SOURCE = value;
                    OnPropertyChanged(() => SOURCE);
                }
            }
        }


        public int SN
        {
            get { return _SN; }
            set
            {
                if (_SN != value)
                {
                    _SN = value;
                    OnPropertyChanged(() => SN);
                }
            }
        }

        public string TRANS_TYPE
        {
            get { return _TRANS_TYPE; }
            set
            {
                if (_TRANS_TYPE != value)
                {
                    _TRANS_TYPE = value;
                    OnPropertyChanged(() => TRANS_TYPE);
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


        
        class OpexSBUBaseCostValidator : AbstractValidator<OpexSBUBaseCost>
        {
            public OpexSBUBaseCostValidator()
            {
                //RuleFor(obj => obj.ServiceCode).NotEmpty().WithMessage("ServiceCode is required.");
                //RuleFor(obj => obj.ServiceDescription).NotEmpty().WithMessage("ServiceDescription is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new OpexSBUBaseCostValidator();
        }
    }
}
