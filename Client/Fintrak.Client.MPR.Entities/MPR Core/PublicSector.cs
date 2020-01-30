
using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class PublicSector : ObjectBase
    {
        string _Account_Number;
        string _CustomerName;
        string _ProductCode;
        string _PSEC_ProductCode;
        bool _Active;

        //ModuleOwnerType _ModuleOwnerType;

        public string Account_Number
        {
            get { return _Account_Number; }
            set
            {
                if (_Account_Number != value)
                {
                    _Account_Number = value;
                    // OnPropertyChanged(() => crb_Data_Id);
                }
            }
        }

        public string CustomerName
        {
            get { return _CustomerName; }
            set
            {
                if (_CustomerName != value)
                {
                    _CustomerName = value;
                    // OnPropertyChanged(() => Code);
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
                    // OnPropertyChanged(() => Name);
                }
            }
        }

        public string PSEC_ProductCode
        {
            get { return _PSEC_ProductCode; }
            set
            {
                if (_PSEC_ProductCode != value)
                {
                    _PSEC_ProductCode = value;
                    //OnPropertyChanged(() => DefinitionCode);
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
