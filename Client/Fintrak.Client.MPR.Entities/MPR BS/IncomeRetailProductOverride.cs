using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeRetailProductOverride : ObjectBase
    {
        int _Id;
        int _Customerid;
        string _Bank;
        string _Mis_code;
        string _AccountOfficer_Code;
       
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

        public int Customerid
        {
            get { return _Customerid; }
            set
            {
                if (_Customerid != value)
                {
                    _Customerid = value;
                    OnPropertyChanged(() => Customerid);
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

        public string Mis_code
        {
            get { return _Mis_code; }
            set
            {
                if (_Mis_code != value)
                {
                    _Mis_code = value;
                    OnPropertyChanged(() => Mis_code);
                }
            }
        }

        public string AccountOfficer_Code
        {
            get { return _AccountOfficer_Code; }
            set
            {
                if (_AccountOfficer_Code != value)
                {
                    _AccountOfficer_Code = value;
                    OnPropertyChanged(() => AccountOfficer_Code);
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



        //class IncomeSplitPoolsRatesAndBasisValidator : AbstractValidator<IncomeSplitPoolsRatesAndBasis>
        //{
        //    public IncomeSplitPoolsRatesAndBasisValidator()
        //    {
        //        RuleFor(obj => obj._PoolRateFCYAsset).NotEmpty().WithMessage("Pool rate FCY asset is required.");
        //    }
        //}

        //protected override IValidator GetValidator()
        //{
        //    return new IncomeSplitPoolsRatesAndBasisValidator();
        //}
    }
}
