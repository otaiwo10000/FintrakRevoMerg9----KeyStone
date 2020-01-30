
using System;
using System.Linq;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class TeamSector : ObjectBase
    {
        int _Mpr_Team_Sector_ID;
        string _Level1Code;
        string _Level1Name;

        string _Level2Code;
        string _Level2Name;

        string _Level3Code;
        string _Level3Name;

        string _Level4Code;
        string _Level4Name;

        bool _Active;


        public int Mpr_Team_Sector_ID
        {
            get { return _Mpr_Team_Sector_ID; }
            set
            {
                if (_Mpr_Team_Sector_ID != value)
                {
                    _Mpr_Team_Sector_ID = value;
                    OnPropertyChanged(() => Mpr_Team_Sector_ID);
                }
            }
        }

        public string Level1Code
        {
            get { return _Level1Code; }
            set
            {
                if (_Level1Code != value)
                {
                    _Level1Code = value;
                    OnPropertyChanged(() => Level1Code);
                }
            }
        }

        public string Level1Name
        {
            get { return _Level1Name; }
            set
            {
                if (_Level1Name != value)
                {
                    _Level1Name = value;
                    OnPropertyChanged(() => Level1Name);
                }
            }
        }

        public string Level2Code
        {
            get { return _Level2Code; }
            set
            {
                if (_Level2Code != value)
                {
                    _Level2Code = value;
                    OnPropertyChanged(() => Level2Code);
                }
            }
        }
        public string Level2Name
        {
            get { return _Level2Name; }
            set
            {
                if (_Level2Name != value)
                {
                    _Level2Name = value;
                    OnPropertyChanged(() => Level2Name);
                }
            }
        }

        public string Level3Code
        {
            get { return _Level3Code; }
            set
            {
                if (_Level3Code != value)
                {
                    _Level3Code = value;
                    OnPropertyChanged(() => Level3Code);
                }
            }
        }
        public string Level3Name
        {
            get { return _Level3Name; }
            set
            {
                if (_Level3Name != value)
                {
                    _Level3Name = value;
                    OnPropertyChanged(() => Level3Name);
                }
            }
        }

        public string Level4Code
        {
            get { return _Level4Code; }
            set
            {
                if (_Level4Code != value)
                {
                    _Level4Code = value;
                    OnPropertyChanged(() => Level4Code);
                }
            }
        }

        public string Level4Name
        {
            get { return _Level4Name; }
            set
            {
                if (_Level4Name != value)
                {
                    _Level4Name = value;
                    OnPropertyChanged(() => Level4Name);
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



        //class CategoryTransferPriceValidator : AbstractValidator<CategoryTransferPrice>
        //{
        //    public CategoryTransferPriceValidator()
        //    {
        //        RuleFor(obj => obj.BalanceSheetCategory).NotEmpty().WithMessage("BalanceSheetCategory is required.");
        //    }
        //}

        //protected override IValidator GetValidator()
        //{
        //    return new CategoryTransferPriceValidator();
        //}
    }
}
