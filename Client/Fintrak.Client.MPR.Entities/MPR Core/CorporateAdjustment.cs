using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
//using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class CorporateAdjustment : ObjectBase
    {
        int _CorporateAdjustmentId;
        string _TeamCode;
        string _AccountOfficerCode;
        string _Narrative;
        string _BranchCode;
        string _GLCode;

        string _Caption;
        string _RelatedAccount;
        string _AccountName;
        double _Amount;
        DateTime _RunDate;

        string _CompanyCode;
        string _AdjustmentCode;
        string _BrokerCode;
        string _GLDescription;
        string _Code;

        bool _Active;

        public int CorporateAdjustmentId
        {
            get { return _CorporateAdjustmentId; }
            set
            {
                if (_CorporateAdjustmentId != value)
                {
                    _CorporateAdjustmentId = value;
                    OnPropertyChanged(() => CorporateAdjustmentId);
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

        public string Narrative
        {
            get { return _Narrative; }
            set
            {
                if (_Narrative != value)
                {
                    _Narrative = value;
                    OnPropertyChanged(() => Narrative);
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

        public string GLCode
        {
            get { return _GLCode; }
            set
            {
                if (_GLCode != value)
                {
                    _GLCode = value;
                    OnPropertyChanged(() => GLCode);
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


        public string RelatedAccount
        {
            get { return _RelatedAccount; }
            set
            {
                if (_RelatedAccount != value)
                {
                    _RelatedAccount = value;
                    OnPropertyChanged(() => RelatedAccount);
                }
            }
        }

        public string AccountName
        {
            get { return _AccountName; }
            set
            {
                if (_AccountName != value)
                {
                    _AccountName = value;
                    OnPropertyChanged(() => AccountName);
                }
            }
        }

        public double Amount
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

        public DateTime RunDate
        {
            get { return _RunDate; }
            set
            {
                if (_RunDate != value)
                {
                    _RunDate = value;
                    OnPropertyChanged(() => RunDate);
                }
            }
        }

        public string CompanyCode
        {
            get { return _CompanyCode; }
            set
            {
                if (_CompanyCode != value)
                {
                    _CompanyCode = value;
                    OnPropertyChanged(() => CompanyCode);
                }
            }
        }

        public string AdjustmentCode
        {
            get { return _AdjustmentCode; }
            set
            {
                if (_AdjustmentCode != value)
                {
                    _AdjustmentCode = value;
                    OnPropertyChanged(() => AdjustmentCode);
                }
            }
        }

        public string BrokerCode
        {
            get { return _BrokerCode; }
            set
            {
                if (_BrokerCode != value)
                {
                    _BrokerCode = value;
                    OnPropertyChanged(() => BrokerCode);
                }
            }
        }

        public string GLDescription
        {
            get { return _GLDescription; }
            set
            {
                if (_GLDescription != value)
                {
                    _GLDescription = value;
                    OnPropertyChanged(() => GLDescription);
                }
            }
        }

        public string Code
        {
            get { return _Code; }
            set
            {
                if (_Code != value)
                {
                    _Code = value;
                    OnPropertyChanged(() => Code);
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
