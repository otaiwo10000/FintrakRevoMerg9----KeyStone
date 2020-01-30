using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeCustomerRatingOverride : ObjectBase
    {
        int _Id;
        string _Cust_ID;
        string _Ref_No;
        string _Settlement_Account;
        string _Customer_Name;
        decimal _Limit;
        decimal _PrincipalOutstandingBal;
        DateTime _Value_Date;
        DateTime _Maturity_Date;
        string _Risk_Rating;

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

        public string Cust_ID
        {
            get { return _Cust_ID; }
            set
            {
                if (_Cust_ID != value)
                {
                    _Cust_ID = value;
                    OnPropertyChanged(() => Cust_ID);
                }
            }
        }

        public string Ref_No
        {
            get { return _Ref_No; }
            set
            {
                if (_Ref_No != value)
                {
                    _Ref_No = value;
                    OnPropertyChanged(() => Ref_No);
                }
            }
        }

        public string Settlement_Account
        {
            get { return _Settlement_Account; }
            set
            {
                if (_Settlement_Account != value)
                {
                    _Settlement_Account = value;
                    OnPropertyChanged(() => Settlement_Account);
                }
            }
        }

        public string Customer_Name
        {
            get { return _Customer_Name; }
            set
            {
                if (_Customer_Name != value)
                {
                    _Customer_Name = value;
                    OnPropertyChanged(() => Customer_Name);
                }
            }
        }
        
        public decimal Limit
        {
            get { return _Limit; }
            set
            {
                if (_Limit != value)
                {
                    _Limit = value;
                    OnPropertyChanged(() => Limit);
                }
            }
        }

        public decimal PrincipalOutstandingBal
        {
            get { return _PrincipalOutstandingBal; }
            set
            {
                if (_PrincipalOutstandingBal != value)
                {
                    _PrincipalOutstandingBal = value;
                    OnPropertyChanged(() => PrincipalOutstandingBal);
                }
            }
        }

        public DateTime Value_Date
        {
            get { return _Value_Date; }
            set
            {
                if (_Value_Date != value)
                {
                    _Value_Date = value;
                    OnPropertyChanged(() => Value_Date);
                }
            }
        }

        public DateTime Maturity_Date
        {
            get { return _Maturity_Date; }
            set
            {
                if (_Maturity_Date != value)
                {
                    _Maturity_Date = value;
                    OnPropertyChanged(() => Maturity_Date);
                }
            }
        }

        public string Risk_Rating
        {
            get { return _Risk_Rating; }
            set
            {
                if (_Risk_Rating != value)
                {
                    _Risk_Rating = value;
                    OnPropertyChanged(() => Risk_Rating);
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
