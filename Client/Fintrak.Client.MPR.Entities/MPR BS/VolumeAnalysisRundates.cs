using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class VolumeAnalysisRundates : ObjectBase
    {
        int _Id;
        DateTime _rundate;
        string _visible;
        
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

        public DateTime rundate
        {
            get { return _rundate; }
            set
            {
                if (_rundate != value)
                {
                    _rundate = value;
                    OnPropertyChanged(() => rundate);
                }
            }
        }

        public string visible
        {
            get { return _visible; }
            set
            {
                if (_visible != value)
                {
                    _visible = value;
                    OnPropertyChanged(() => visible);
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



        //class AcquirerMappingValidator : AbstractValidator<AcquirerMapping>
        //{
        //    public AcquirerMappingValidator()
        //    {
        //        RuleFor(obj => obj.AccountNumber).NotEmpty().WithMessage("AccountNumber is required.");
        //    }
        //}

        //protected override IValidator GetValidator()
        //{
        //    return new AcquirerMappingValidator();
        //}

    }
}
