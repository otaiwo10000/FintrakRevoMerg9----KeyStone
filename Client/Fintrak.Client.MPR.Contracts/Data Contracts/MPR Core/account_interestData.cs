
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.MPR.Contracts
{
    [DataContract]
    public class account_interestData : DataContractBase
    {
        int _account_interest_Id;
        string _AccountNo;
        Double _InterestRate;
        string _Productcode;
        string _Period;
        string _Year;
        string _caption;
        

        //ModuleOwnerType _ModuleOwnerType;

        public int account_interest_Id
        {
            get { return _account_interest_Id; }
            set
            {
                if (_account_interest_Id != value)
                {
                    _account_interest_Id = value;
                    // OnPropertyChanged(() => crb_Data_Id);
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
                    //OnPropertyChanged(() => DefinitionCode);
                }
            }
        }

        public Double InterestRate
        {
            get { return _InterestRate; }
            set
            {
                if (_InterestRate != value)
                {
                    _InterestRate = value;
                    // OnPropertyChanged(() => ParentCode);
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
                    //OnPropertyChanged(() => DefinitionCode);
                }
            }
        }

        public string Period
        {
            get { return _Period; }
            set
            {
                if (_Period != value)
                {
                    _Period = value;
                    // OnPropertyChanged(() => Code);
                }
            }
        }

        public string Year
        {
            get { return _Year; }
            set
            {
                if (_Year != value)
                {
                    _Year = value;
                    // OnPropertyChanged(() => Code);
                }
            }
        }

        
        public string caption
        {
            get { return _caption; }
            set
            {
                if (_caption != value)
                {
                    _caption = value;
                    //OnPropertyChanged(() => DefinitionCode);
                }
            }
        }

    }
}
