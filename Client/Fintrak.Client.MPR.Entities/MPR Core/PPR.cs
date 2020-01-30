using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class PPR : ObjectBase
    {
        int _PPRId;
        string _ProductCode;
        string _ProductName;
        string _Caption;
        string _PPR_Caption;
        string _Category;
        string _Currency;
        bool _Visible;

        bool _Active;

        public int PPRId
        {
            get { return _PPRId; }
            set
            {
                if (_PPRId != value)
                {
                    _PPRId = value;
                    OnPropertyChanged(() => PPRId);
                }
            }
        }

        public string ProductCode
        {
            get { return _ProductCode; }
            set
            {
                if (_ProductCode != value)
                {
                    _ProductCode = value;
                    OnPropertyChanged(() => ProductCode);
                }
            }
        }


        public string ProductName
        {
            get { return _ProductName; }
            set
            {
                if (_ProductName != value)
                {
                    _ProductName = value;
                    OnPropertyChanged(() => ProductName);
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

        public string PPR_Caption
        {
            get { return _PPR_Caption; }
            set
            {
                if (_PPR_Caption != value)
                {
                    _PPR_Caption = value;
                    OnPropertyChanged(() => PPR_Caption);
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

        public bool Visible
        {
            get { return _Visible; }
            set
            {
                if (_Visible != value)
                {
                    _Visible = value;
                    OnPropertyChanged(() => Visible);
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
