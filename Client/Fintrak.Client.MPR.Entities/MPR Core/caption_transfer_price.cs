
using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.MPR.Framework;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class caption_transfer_price : ObjectBase
    {
        int _caption_transfer_price_Id;
        string _Caption;
        string _Currency;
        Decimal _Rating;
        bool _Active;

       // ModuleOwnerType _ModuleOwnerType;

        public int caption_transfer_price_Id
        {
            get { return _caption_transfer_price_Id; }
            set
            {
                if (_caption_transfer_price_Id != value)
                {
                    _caption_transfer_price_Id = value;
                  OnPropertyChanged(() => caption_transfer_price_Id);
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

        public Decimal Rating
        {
            get { return _Rating; }
            set
            {
                if (_Rating != value)
                {
                    _Rating = value;
                   OnPropertyChanged(() => Rating);
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
