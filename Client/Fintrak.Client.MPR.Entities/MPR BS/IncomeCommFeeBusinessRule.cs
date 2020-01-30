using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeCommFeeBusinessRule : ObjectBase
    {
        int _ID;
        string _GLCode;
        string _Bank;
        string _GL_Description;
        string _Channel;
        string _Related_Account;
        string _Branches;
        string _Basis_of_Allocation;
        string _rule;

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


        public string GLCode
        {
            get { return _GLCode; }
            set
            {
                if (_GLCode != value)
                {
                    _GLCode = value;
                    OnPropertyChanged(() => GLCode);
                }
            }
        }

        public string Bank
        {
            get { return _Bank; }
            set
            {
                if (_Bank != value)
                {
                    _Bank = value;
                    OnPropertyChanged(() => Bank);
                }
            }
        }

        public string GL_Description
        {
            get { return _GL_Description; }
            set
            {
                if (_GL_Description != value)
                {
                    _GL_Description = value;
                    OnPropertyChanged(() => GL_Description);
                }
            }
        }
     
        public string Channel
        {
            get { return _Channel; }
            set
            {
                if (_Channel != value)
                {
                    _Channel = value;
                    OnPropertyChanged(() => Channel);
                }
            }
        }

        public string Related_Account
        {
            get { return _Related_Account; }
            set
            {
                if (_Related_Account != value)
                {
                    _Related_Account = value;
                    OnPropertyChanged(() => Related_Account);
                }
            }
        }

        public string Branches
        {
            get { return _Branches; }
            set
            {
                if (_Branches != value)
                {
                    _Branches = value;
                    OnPropertyChanged(() => Branches);
                }
            }
        }

        public string Basis_of_Allocation
        {
            get { return _Basis_of_Allocation; }
            set
            {
                if (_Basis_of_Allocation != value)
                {
                    _Basis_of_Allocation = value;
                    OnPropertyChanged(() => Basis_of_Allocation);
                }
            }
        }

        public string rule
        {
            get { return _rule; }
            set
            {
                if (_rule != value)
                {
                    _rule = value;
                    OnPropertyChanged(() => rule);
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
