using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;
using Fintrak.Shared.Core.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeFintrakAccountsSegment : ObjectBase
    {
        int _Id;
        string _Description;
        string _SegmentCode;
        string _AccoutofficerCode;
        string _CUSTOMERID;
        string _ACCOUNTNUMBER;
        string _CUSTOMERNAME;
        string _TEAMNAME;
        string _Bank;
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

        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged(() => Description);
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


        public string AccoutofficerCode
        {
            get { return _AccoutofficerCode; }
            set
            {
                if (_AccoutofficerCode != value)
                {
                    _AccoutofficerCode = value;
                    OnPropertyChanged(() => AccoutofficerCode);
                }
            }
        }

        public string CUSTOMERID
        {
            get { return _CUSTOMERID; }
            set
            {
                if (_CUSTOMERID != value)
                {
                    _CUSTOMERID = value;
                    OnPropertyChanged(() => CUSTOMERID);
                }
            }
        }

        public string ACCOUNTNUMBER
        {
            get { return _ACCOUNTNUMBER; }
            set
            {
                if (_ACCOUNTNUMBER != value)
                {
                    _ACCOUNTNUMBER = value;
                    OnPropertyChanged(() => ACCOUNTNUMBER);
                }
            }
        }

        public string CUSTOMERNAME
        {
            get { return _CUSTOMERNAME; }
            set
            {
                if (_CUSTOMERNAME != value)
                {
                    _CUSTOMERNAME = value;
                    OnPropertyChanged(() => CUSTOMERNAME);
                }
            }
        }

        public string TEAMNAME
        {
            get { return _TEAMNAME; }
            set
            {
                if (_TEAMNAME != value)
                {
                    _TEAMNAME = value;
                    OnPropertyChanged(() => TEAMNAME);
                }
            }
        }

        public string Bank
        {
            get { return _Bank; }
            set
            {
                if (_Bank != value)
                {
                    _Bank = value;
                    OnPropertyChanged(() => Bank);
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


        class IncomeFintrakAccountsSegmentValidator : AbstractValidator<IncomeFintrakAccountsSegment>
        {
            public IncomeFintrakAccountsSegmentValidator()
            {
                RuleFor(obj => obj.CUSTOMERID).NotEmpty().WithMessage("Customer Id is required.");
                RuleFor(obj => obj.Bank).NotEmpty().WithMessage("Bank is required.");

            }
        }

        protected override IValidator GetValidator()
        {
            return new IncomeFintrakAccountsSegmentValidator();
        }

    }
}
