using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class LifeTimePDClassification : ObjectBase
    {
        int _LifeTimePDClassificationId;
        string _RefNo;
        string _Sector;
        double _BaseLifetimePD;
        string _BaseRating;
        double _CurrentLifetimePD;
        string _CurrentRating;
        int _NotchDiff;
        string _Classification;
        bool _Active;


        public int LifeTimePDClassificationId
        {
            get { return _LifeTimePDClassificationId; }
            set
            {
                if (_LifeTimePDClassificationId != value)
                {
                    _LifeTimePDClassificationId = value;
                    OnPropertyChanged(() => LifeTimePDClassificationId);
                }
            }
        }

        public int NotchDiff
        {
            get { return _NotchDiff; }
            set
            {
                if (_NotchDiff != value)
                {
                    _NotchDiff = value;
                    OnPropertyChanged(() => NotchDiff);
                }
            }
        }
        public string RefNo
        {
            get { return _RefNo; }
            set
            {
                if (_RefNo != value)
                {
                    _RefNo = value;
                    OnPropertyChanged(() => RefNo);
                }
            }
        }

        public string Sector
        {
            get { return _Sector; }
            set
            {
                if (_Sector != value)
                {
                    _Sector = value;
                    OnPropertyChanged(() => Sector);
                }
            }
        }


        public double BaseLifetimePD
        {
            get { return _BaseLifetimePD; }
            set
            {
                if (_BaseLifetimePD != value)
                {
                    _BaseLifetimePD = value;
                    OnPropertyChanged(() => BaseLifetimePD);
                }
            }
        }


        public string BaseRating
        {
            get { return _BaseRating; }
            set
            {
                if (_BaseRating != value)
                {
                    _BaseRating = value;
                    OnPropertyChanged(() => BaseRating);
                }
            }
        }

        public double CurrentLifetimePD
        {
            get { return _CurrentLifetimePD; }
            set
            {
                if (_CurrentLifetimePD != value)
                {
                    _CurrentLifetimePD = value;
                    OnPropertyChanged(() => CurrentLifetimePD);
                }
            }
        }


        public string CurrentRating
        {
            get { return _CurrentRating; }
            set
            {
                if (_CurrentRating != value)
                {
                    _CurrentRating = value;
                    OnPropertyChanged(() => CurrentRating);
                }
            }
        }

        public string Classification
        {
            get { return _Classification; }
            set
            {
                if (_Classification != value)
                {
                    _Classification = value;
                    OnPropertyChanged(() => Classification);
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
        class LifeTimePDClassificationValidator : AbstractValidator<LifeTimePDClassification>
        {
            public LifeTimePDClassificationValidator()
            {
                RuleFor(obj => obj._Classification).NotEmpty().WithMessage("Classification is required.");
                }
        }

        protected override IValidator GetValidator()
        {
            return new LifeTimePDClassificationValidator();
        }
    }
}
