using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class AcquirerSharing : ObjectBase
    {
        int _mpr_Acquirer_Sharing_Id;
        string _AccountNumber;
        string _IntroducerCode;
        string _AccountOficerCode;
        double _Ratio;

        bool _Active;


        public int mpr_Acquirer_Sharing_Id
        {
            get { return _mpr_Acquirer_Sharing_Id; }
            set
            {
                if (_mpr_Acquirer_Sharing_Id != value)
                {
                    _mpr_Acquirer_Sharing_Id = value;
                    OnPropertyChanged(() => mpr_Acquirer_Sharing_Id);
                }
            }
        }

        public string AccountNumber
        {
            get { return _AccountNumber; }
            set
            {
                if (_AccountNumber != value)
                {
                    _AccountNumber = value;
                    OnPropertyChanged(() => AccountNumber);
                }
            }
        }



        public string IntroducerCode
        {
            get { return _IntroducerCode; }
            set
            {
                if (_IntroducerCode != value)
                {
                    _IntroducerCode = value;
                    OnPropertyChanged(() => IntroducerCode);
                }
            }
        }

        public string AccountOficerCode
        {
            get { return _AccountOficerCode; }
            set
            {
                if (_AccountOficerCode != value)
                {
                    _AccountOficerCode = value;
                    OnPropertyChanged(() => AccountOficerCode);
                }
            }
        }

        public double Ratio
        {
            get { return _Ratio; }
            set
            {
                if (_Ratio != value)
                {
                    _Ratio = value;
                    OnPropertyChanged(() => Ratio);
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



        class AcquirerSharingValidator : AbstractValidator<AcquirerSharing>
        {
            public AcquirerSharingValidator()
            {
                RuleFor(obj => obj.AccountNumber).NotEmpty().WithMessage("AccountNumber is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new AcquirerSharingValidator();
        }
    }
}
