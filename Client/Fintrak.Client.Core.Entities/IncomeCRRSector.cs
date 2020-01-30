using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.Core.Entities
{
    public class IncomeCRRSector : ObjectBase
    {
        int _ID;
        string _SECTOR_CODE;
        string _SECTOR;
        decimal _CRR;
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

        public string SECTOR_CODE
        {
            get { return _SECTOR_CODE; }
            set
            {
                if (_SECTOR_CODE != value)
                {
                    _SECTOR_CODE = value;
                    OnPropertyChanged(() => SECTOR_CODE);
                }
            }
        }

        public string SECTOR
        {
            get { return _SECTOR; }
            set
            {
                if (_SECTOR != value)
                {
                    _SECTOR = value;
                    OnPropertyChanged(() => SECTOR);
                }
            }
        }

        public decimal CRR
        {
            get { return _CRR; }
            set
            {
                if (_CRR != value)
                {
                    _CRR = value;
                    OnPropertyChanged(() => CRR);
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

      
        //class IncomeCRRSectorValidator : AbstractValidator<Staff>
        //{
        //    public IncomeCRRSectorValidator()
        //    {
        //        RuleFor(obj => obj.StaffCode).NotEmpty().WithMessage("StaffCode must not be empty.");
        //        RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name must not be empty.");
               
        //    }
        //}

        //protected override IValidator GetValidator()
        //{
        //    return new IncomeCRRSectorValidator();
        //}
    }
}
