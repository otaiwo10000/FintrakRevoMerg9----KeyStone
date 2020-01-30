using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeBranches : ObjectBase
    {
        int _ID;
        string _BranchCode;
        string _Description;
        string _MIS_Code;
        string _State;
        string _Address;
       
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


        public string BranchCode
        {
            get { return _BranchCode; }
            set
            {
                if (_BranchCode != value)
                {
                    _BranchCode = value;
                    OnPropertyChanged(() => BranchCode);
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

        public string MIS_Code
        {
            get { return _MIS_Code; }
            set
            {
                if (_MIS_Code != value)
                {
                    _MIS_Code = value;
                    OnPropertyChanged(() => MIS_Code);
                }
            }
        }
     
        public string State
        {
            get { return _State; }
            set
            {
                if (_State != value)
                {
                    _State = value;
                    OnPropertyChanged(() => State);
                }
            }
        }

        public string Address
        {
            get { return _Address; }
            set
            {
                if (_Address != value)
                {
                    _Address = value;
                    OnPropertyChanged(() => Address);
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

        
        //class ProductMISValidator : AbstractValidator<ProductMIS>
        //{
        //    public ProductMISValidator()
        //    {
        //        RuleFor(obj => obj.ProductCode).NotEmpty().WithMessage("Product Code is required.");
        //        RuleFor(obj => obj.TeamCode).NotEmpty().WithMessage("Team is required.");
        //        RuleFor(obj => obj.TeamDefinitionCode).NotEmpty().WithMessage("Team Definition is required.");
        //        //RuleFor(obj => obj.AccountOfficerCode).NotEmpty().WithMessage("Team is required.");
        //        //RuleFor(obj => obj.AccountOfficerDefinitionCode).NotEmpty().WithMessage("AccountOfficer Definition Code is required.");
        //        RuleFor(obj => obj.CaptionCode).NotEmpty().WithMessage("Caption is required.");
               
        //    }
        //}

        //protected override IValidator GetValidator()
        //{
        //    return new ProductMISValidator();
        //}
    }
}
