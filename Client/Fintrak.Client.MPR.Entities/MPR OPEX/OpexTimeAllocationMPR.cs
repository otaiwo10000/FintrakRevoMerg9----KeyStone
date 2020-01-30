using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class OpexTimeAllocationMPR : ObjectBase
    {
        int _ID;
        string _SOURCE;
        string _BASISCAPTION;
        string _TARGET;
        string _DESCRIPTION;
        decimal _RATIO;
        string _TEMPLATE;
        int _SN;
        string _TOTAL;
        string _type;
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

        public string BASISCAPTION
        {
            get { return _BASISCAPTION; }
            set
            {
                if (_BASISCAPTION != value)
                {
                    _BASISCAPTION = value;
                    OnPropertyChanged(() => BASISCAPTION);
                }
            }
        }


        public string TARGET
        {
            get { return _TARGET; }
            set
            {
                if (_TARGET != value)
                {
                    _TARGET = value;
                    OnPropertyChanged(() => TARGET);
                }
            }
        }

        public string DESCRIPTION
        {
            get { return _DESCRIPTION; }
            set
            {
                if (_DESCRIPTION != value)
                {
                    _DESCRIPTION = value;
                    OnPropertyChanged(() => DESCRIPTION);
                }
            }
        }

        public decimal RATIO
        {
            get { return _RATIO; }
            set
            {
                if (_RATIO != value)
                {
                    _RATIO = value;
                    OnPropertyChanged(() => RATIO);
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

        public string TOTAL
        {
            get { return _TOTAL; }
            set
            {
                if (_TOTAL != value)
                {
                    _TOTAL = value;
                    OnPropertyChanged(() => TOTAL);
                }
            }
        }

        public string type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged(() => type);
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


        
        class OpexTimeAllocationMPRValidator : AbstractValidator<OpexTimeAllocationMPR>
        {
            public OpexTimeAllocationMPRValidator()
            {
                //RuleFor(obj => obj.ServiceCode).NotEmpty().WithMessage("ServiceCode is required.");
                //RuleFor(obj => obj.ServiceDescription).NotEmpty().WithMessage("ServiceDescription is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new OpexTimeAllocationMPRValidator();
        }
    }
}
