using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class OpexStaffcostDetail : ObjectBase
    {
        int _ID;
        string _EMP_NAME;
        double _AMOUNT;
        int _PERIOD;
        int _YEAR;
        string _TEAM_CODE;
        string _EMP_ID;
        string _ACCOUNTOFFICER_CODE;
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

        public string EMP_NAME
        {
            get { return _EMP_NAME; }
            set
            {
                if (_EMP_NAME != value)
                {
                    _EMP_NAME = value;
                    OnPropertyChanged(() => EMP_NAME);
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


        public int PERIOD
        {
            get { return _PERIOD; }
            set
            {
                if (_PERIOD != value)
                {
                    _PERIOD = value;
                    OnPropertyChanged(() => PERIOD);
                }
            }
        }

        public int YEAR
        {
            get { return _YEAR; }
            set
            {
                if (_YEAR != value)
                {
                    _YEAR = value;
                    OnPropertyChanged(() => YEAR);
                }
            }
        }

        public string TEAM_CODE
        {
            get { return _TEAM_CODE; }
            set
            {
                if (_TEAM_CODE != value)
                {
                    _TEAM_CODE = value;
                    OnPropertyChanged(() => TEAM_CODE);
                }
            }
        }

        public string EMP_ID
        {
            get { return _EMP_ID; }
            set
            {
                if (_EMP_ID != value)
                {
                    _EMP_ID = value;
                    OnPropertyChanged(() => EMP_ID);
                }
            }
        }

        public string ACCOUNTOFFICER_CODE
        {
            get { return _ACCOUNTOFFICER_CODE; }
            set
            {
                if (_ACCOUNTOFFICER_CODE != value)
                {
                    _ACCOUNTOFFICER_CODE = value;
                    OnPropertyChanged(() => ACCOUNTOFFICER_CODE);
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


        
        class OpexStaffcostDetailValidator : AbstractValidator<OpexStaffcostDetail>
        {
            public OpexStaffcostDetailValidator()
            {
                //RuleFor(obj => obj.ServiceCode).NotEmpty().WithMessage("ServiceCode is required.");
                //RuleFor(obj => obj.ServiceDescription).NotEmpty().WithMessage("ServiceDescription is required.");
             }
        }

        protected override IValidator GetValidator()
        {
            return new OpexStaffcostDetailValidator();
        }
    }
}
