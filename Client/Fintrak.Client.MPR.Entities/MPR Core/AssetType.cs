using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Client.MPR.Entities
{
    public class AssetType : ObjectBase
    {
        int _AssetTypeId;
        string _AssetTypeCol;
        
        bool _Active;

        public int AssetTypeId
        {
            get { return _AssetTypeId; }
            set
            {
                if (_AssetTypeId != value)
                {
                    _AssetTypeId = value;
                    OnPropertyChanged(() => AssetTypeId);
                }
            }
        }

        public string AssetTypeCol
        {
            get { return _AssetTypeCol; }
            set
            {
                if (_AssetTypeCol != value)
                {
                    _AssetTypeCol = value;
                    OnPropertyChanged(() => AssetTypeCol);
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
