using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class AcquirerMapping : ObjectBase
    {
        int _mpr_Acquirer_Mapping_Id;
        string _AccountNumber;
        string _IntroducerCode;
        
        bool _Active;


        public int mpr_Acquirer_Mapping_Id
        {
            get { return _mpr_Acquirer_Mapping_Id; }
            set
            {
                if (_mpr_Acquirer_Mapping_Id != value)
                {
                    _mpr_Acquirer_Mapping_Id = value;
                    OnPropertyChanged(() => mpr_Acquirer_Mapping_Id);
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



        class AcquirerMappingValidator : AbstractValidator<AcquirerMapping>
        {
            public AcquirerMappingValidator()
            {
                RuleFor(obj => obj.AccountNumber).NotEmpty().WithMessage("AccountNumber is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new AcquirerMappingValidator();
        }
    }
}
