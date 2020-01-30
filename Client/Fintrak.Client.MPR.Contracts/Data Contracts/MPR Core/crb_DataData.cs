
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;

namespace Fintrak.Client.MPR.Contracts
{
    [DataContract]
    public class crb_DataData : DataContractBase
    {
        int _crb_Data_Id;
        DateTime _xDate;
        int _Count;
        Decimal _Volume;
        string _caption;

        //ModuleOwnerType _ModuleOwnerType;

        public int crb_Data_Id
        {
            get { return _crb_Data_Id; }
            set
            {
                if (_crb_Data_Id != value)
                {
                    _crb_Data_Id = value;
                   // OnPropertyChanged(() => crb_Data_Id);
                }
            }
        }

        public DateTime xDate
        {
            get { return _xDate; }
            set
            {
                if (_xDate != value)
                {
                    _xDate = value;
                   // OnPropertyChanged(() => Code);
                }
            }
        }

        public Int32 Count
        {
            get { return _Count; }
            set
            {
                if (_Count != value)
                {
                    _Count = value;
                   // OnPropertyChanged(() => Name);
                }
            }
        }

        public Decimal Volume
        {
            get { return _Volume; }
            set
            {
                if (_Volume != value)
                {
                    _Volume = value;
                   // OnPropertyChanged(() => ParentCode);
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
