using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class RiskAdjustedCharge : ObjectBase
    {
        int _RiskAdjustedChargeId;
        string _AccountNo;
        Double _Amount;
        string _RiskDefinitionCode;
        string _AccountOfficerCode;
        string _TeamCode;
        string _BranchCode;
        string _MemoCode;
        string _SectorCode;
        string _SegmentCode;
        string _AccountTitle;
        string _CurrencyType;
        string _Productcode;

        bool _Active;

        public int RiskAdjustedChargeId
        {
            get { return _RiskAdjustedChargeId; }
            set
            {
                if (_RiskAdjustedChargeId != value)
                {
                    _RiskAdjustedChargeId = value;
                    OnPropertyChanged(() => RiskAdjustedChargeId);
                }
            }
        }

        public string AccountNo
        {
            get { return _AccountNo; }
            set
            {
                if (_AccountNo != value)
                {
                    _AccountNo = value;
                    OnPropertyChanged(() => AccountNo);
                }
            }
        }


        public Double Amount
        {
            get { return _Amount; }
            set
            {
                if (_Amount != value)
                {
                    _Amount = value;
                    OnPropertyChanged(() => Amount);
                }
            }
        }


        public string RiskDefinitionCode
        {
            get { return _RiskDefinitionCode; }
            set
            {
                if (_RiskDefinitionCode != value)
                {
                    _RiskDefinitionCode = value;
                    OnPropertyChanged(() => RiskDefinitionCode);
                }
            }
        }

        public string AccountOfficerCode
        {
            get { return _AccountOfficerCode; }
            set
            {
                if (_AccountOfficerCode != value)
                {
                    _AccountOfficerCode = value;
                    OnPropertyChanged(() => AccountOfficerCode);
                }
            }
        }


        public string TeamCode
        {
            get { return _TeamCode; }
            set
            {
                if (_TeamCode != value)
                {
                    _TeamCode = value;
                    OnPropertyChanged(() => TeamCode);
                }
            }
        }

        public string BranchCode
        {
            get { return _BranchCode; }
            set
            {
                if (_BranchCode != value)
                {
                    _BranchCode = value;
                    OnPropertyChanged(() => BranchCode);
                }
            }
        }


        public string MemoCode
        {
            get { return _MemoCode; }
            set
            {
                if (_MemoCode != value)
                {
                    _MemoCode = value;
                    OnPropertyChanged(() => MemoCode);
                }
            }
        }

        public string SectorCode
        {
            get { return _SectorCode; }
            set
            {
                if (_SectorCode != value)
                {
                    _SectorCode = value;
                    OnPropertyChanged(() => SectorCode);
                }
            }
        }


        public string SegmentCode
        {
            get { return _SegmentCode; }
            set
            {
                if (_SegmentCode != value)
                {
                    _SegmentCode = value;
                    OnPropertyChanged(() => SegmentCode);
                }
            }
        }

        public string AccountTitle
        {
            get { return _AccountTitle; }
            set
            {
                if (_AccountTitle != value)
                {
                    _AccountTitle = value;
                    OnPropertyChanged(() => AccountTitle);
                }
            }
        }


        public string CurrencyType
        {
            get { return _CurrencyType; }
            set
            {
                if (_CurrencyType != value)
                {
                    _CurrencyType = value;
                    OnPropertyChanged(() => CurrencyType);
                }
            }
        }

        public string Productcode
        {
            get { return _Productcode; }
            set
            {
                if (_Productcode != value)
                {
                    _Productcode = value;
                    OnPropertyChanged(() => Productcode);
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


    }
}
