using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class FTPRiskRatings : ObjectBase
    {
        int _ID;
        string _Ratings;
        string _Currency;
        string _Category;
        string _Caption;
        string _Levels;
        string _LevelCode;
        decimal _JAN;
        decimal _Feb;
        decimal _Mar;
        decimal _Apr;
        decimal _May;
        decimal _Jun;
        decimal _Jul;
        decimal _Aug;
        decimal _Sep;
        decimal _Oct;
        decimal _Nov;
        decimal _Dec;
        int _Year;
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

        public string Ratings
        {
            get { return _Ratings; }
            set
            {
                if (_Ratings != value)
                {
                    _Ratings = value;
                    OnPropertyChanged(() => Ratings);
                }
            }
        }

        public string Currency
        {
            get { return _Currency; }
            set
            {
                if (_Currency != value)
                {
                    _Currency = value;
                    OnPropertyChanged(() => Currency);
                }
            }
        }

        public string Category
        {
            get { return _Category; }
            set
            {
                if (_Category != value)
                {
                    _Category = value;
                    OnPropertyChanged(() => Category);
                }
            }
        }

        public string Caption
        {
            get { return _Caption; }
            set
            {
                if (_Caption != value)
                {
                    _Caption = value;
                    OnPropertyChanged(() => Caption);
                }
            }
        }

        public string Levels
        {
            get { return _Levels; }
            set
            {
                if (_Levels != value)
                {
                    _Levels = value;
                    OnPropertyChanged(() => Levels);
                }
            }
        }

        public string LevelCode
        {
            get { return _LevelCode; }
            set
            {
                if (_LevelCode != value)
                {
                    _LevelCode = value;
                    OnPropertyChanged(() => LevelCode);
                }
            }
        }

        public decimal JAN
        {
            get { return _JAN; }
            set
            {
                if (_JAN != value)
                {
                    _JAN = value;
                    OnPropertyChanged(() => JAN);
                }
            }
        }

        public decimal Feb
        {
            get { return _Feb; }
            set
            {
                if (_Feb != value)
                {
                    _Feb = value;
                    OnPropertyChanged(() => Feb);
                }
            }
        }

        public decimal Mar
        {
            get { return _Mar; }
            set
            {
                if (_Mar != value)
                {
                    _Mar = value;
                    OnPropertyChanged(() => Mar);
                }
            }
        }

        public decimal Apr
        {
            get { return _Apr; }
            set
            {
                if (_Apr != value)
                {
                    _Apr = value;
                    OnPropertyChanged(() => Apr);
                }
            }
        }

        public decimal May
        {
            get { return _May; }
            set
            {
                if (_May != value)
                {
                    _May = value;
                    OnPropertyChanged(() => May);
                }
            }
        }

        public decimal Jun
        {
            get { return _Jun; }
            set
            {
                if (_Jun != value)
                {
                    _Jun = value;
                    OnPropertyChanged(() => Jun);
                }
            }
        }

        public decimal Jul
        {
            get { return _Jul; }
            set
            {
                if (_Jul != value)
                {
                    _Jul = value;
                    OnPropertyChanged(() => Jul);
                }
            }
        }

        public decimal Aug
        {
            get { return _Aug; }
            set
            {
                if (_Aug != value)
                {
                    _Aug = value;
                    OnPropertyChanged(() => Aug);
                }
            }
        }

        public decimal Sep
        {
            get { return _Sep; }
            set
            {
                if (_Sep != value)
                {
                    _Sep = value;
                    OnPropertyChanged(() => Sep);
                }
            }
        }

        public decimal Oct
        {
            get { return _Oct; }
            set
            {
                if (_Oct != value)
                {
                    _Oct = value;
                    OnPropertyChanged(() => Oct);
                }
            }
        }

        public decimal Nov
        {
            get { return _Nov; }
            set
            {
                if (_Nov != value)
                {
                    _Nov = value;
                    OnPropertyChanged(() => Nov);
                }
            }
        }

        public decimal Dec
        {
            get { return _Dec; }
            set
            {
                if (_Dec != value)
                {
                    _Dec = value;
                    OnPropertyChanged(() => Dec);
                }
            }
        }

        public int Year
        {
            get { return _Year; }
            set
            {
                if (_Year != value)
                {
                    _Year = value;
                    OnPropertyChanged(() => Year);
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


        class FTPRiskRatingsValidator : AbstractValidator<FTPRiskRatings>
        {
            public FTPRiskRatingsValidator()
            {
                //RuleFor(obj => obj.OldMIS_Code).NotEmpty().WithMessage("OldMis_Code is required.");
                //RuleFor(obj => obj.NewMIS_Code).NotEmpty().WithMessage("NewMis_Code is required.");
            


            }
        }

        protected override IValidator GetValidator()
        {
            return new FTPRiskRatingsValidator();
        }

    }
}
