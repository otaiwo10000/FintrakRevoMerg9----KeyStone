
using System;
using System.Linq;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class TeamSegment : ObjectBase
    {
        int _Mpr_Team_Segment_ID;
        string _TargetCode;
        string _TargetSegment;

        string _CustomerTypeCode;
        string _CustomerType;

        string _CustomerSegmentCode;
        string _CustomerSegment;

        bool _Active;


        public int Mpr_Team_Segment_ID
        {
            get { return _Mpr_Team_Segment_ID; }
            set
            {
                if (_Mpr_Team_Segment_ID != value)
                {
                    _Mpr_Team_Segment_ID = value;
                    OnPropertyChanged(() => Mpr_Team_Segment_ID);
                }
            }
        }

        public string TargetCode
        {
            get { return _TargetCode; }
            set
            {
                if (_TargetCode != value)
                {
                    _TargetCode = value;
                    OnPropertyChanged(() => TargetCode);
                }
            }
        }

        public string TargetSegment
        {
            get { return _TargetSegment; }
            set
            {
                if (_TargetSegment != value)
                {
                    _TargetSegment = value;
                    OnPropertyChanged(() => TargetSegment);
                }
            }
        }

        public string CustomerTypeCode
        {
            get { return _CustomerTypeCode; }
            set
            {
                if (_CustomerTypeCode != value)
                {
                    _CustomerTypeCode = value;
                    OnPropertyChanged(() => CustomerTypeCode);
                }
            }
        }
        public string CustomerType
        {
            get { return _CustomerType; }
            set
            {
                if (_CustomerType != value)
                {
                    _CustomerType = value;
                    OnPropertyChanged(() => CustomerType);
                }
            }
        }

        public string CustomerSegmentCode
        {
            get { return _CustomerSegmentCode; }
            set
            {
                if (_CustomerSegmentCode != value)
                {
                    _CustomerSegmentCode = value;
                    OnPropertyChanged(() => CustomerSegmentCode);
                }
            }
        }
        public string CustomerSegment
        {
            get { return _CustomerSegment; }
            set
            {
                if (_CustomerSegment != value)
                {
                    _CustomerSegment = value;
                    OnPropertyChanged(() => CustomerSegment);
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
