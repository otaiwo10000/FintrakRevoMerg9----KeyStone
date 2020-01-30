using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.IFRS.Entities
{
    public class SetUp : ObjectBase
    {
        int _SetUpId;
        double _Threshold;
        int _Deteroriation_Level;
        int _Classification_Type;
        int _Historical_PD_Year_Count;
        string _GroupBased;
        double _CCF;
        int _Ltpdapproach;
        bool _PDBasis;
        bool _Active;

        public int SetUpId
        {
            get { return _SetUpId; }
            set
            {
                if (_SetUpId != value)
                {
                    _SetUpId = value;
                    OnPropertyChanged(() => SetUpId);
                }
            }
        }
        public string GroupBased
        {
            get { return _GroupBased; }
            set
            {
                if (_GroupBased != value)
                {
                    _GroupBased = value;
                    OnPropertyChanged(() => GroupBased);
                }
            }
        }
        public double Threshold
        {
            get { return _Threshold; }
            set
            {
                if (_Threshold != value)
                {
                    _Threshold = value;
                    OnPropertyChanged(() => Threshold);
                }
            }
        }

        public double CCF
        {
            get { return _CCF; }
            set
            {
                if (_CCF != value)
                {
                    _CCF = value;
                    OnPropertyChanged(() => CCF);
                }
            }
        }
        public int Historical_PD_Year_Count
        {
            get { return _Historical_PD_Year_Count; }
            set
            {
                if (_Historical_PD_Year_Count != value)
                {
                    _Historical_PD_Year_Count = value;
                    OnPropertyChanged(() => Historical_PD_Year_Count);
                }
            }
        }
        public int Deteroriation_Level
        {
            get { return _Deteroriation_Level; }
            set
            {
                if (_Deteroriation_Level != value)
                {
                    _Deteroriation_Level = value;
                    OnPropertyChanged(() => Deteroriation_Level);
                }
            }
        }

        public int Ltpdapproach
        {
            get { return _Ltpdapproach; }
            set
            {
                if (_Ltpdapproach != value)
                {
                    _Ltpdapproach = value;
                    OnPropertyChanged(() => Ltpdapproach);
                }
            }
        }
        
        public bool PDBasis
        {
            get { return _PDBasis; }
            set
            {
                if (_PDBasis != value)
                {
                    _PDBasis = value;
                    OnPropertyChanged(() => PDBasis);
                }
            }
        }
        public int Classification_Type
        {
            get { return _Classification_Type; }
            set
            {
                if (_Classification_Type != value)
                {
                    _Classification_Type = value;
                    OnPropertyChanged(() => Classification_Type);
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


        class SetUpValidator : AbstractValidator<SetUp>
        {
            public SetUpValidator()
            {
                RuleFor(obj => obj.Threshold).NotEmpty().WithMessage("Threshold is required.");
                RuleFor(obj => obj.Classification_Type).NotEmpty().WithMessage("Classification Type is required.");
            }
        }

        protected override IValidator GetValidator()
        {
            return new SetUpValidator();
        }
    }
}
