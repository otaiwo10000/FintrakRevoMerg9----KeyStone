
using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class ScoreCardPerspective : ObjectBase
    {
        Int32 _PerspectiveId;
        string _Perspective;
        string _PerspectiveSub;
        int _Year;
        int _Position;
        bool _Active;

        //ModuleOwnerType _ModuleOwnerType;

        public Int32 PerspectiveId
        {
            get { return _PerspectiveId; }
            set
            {
                if (_PerspectiveId != value)
                {
                    _PerspectiveId = value;
                    OnPropertyChanged(() => PerspectiveId);
                }
            }
        }

        public string Perspective
        {
            get { return _Perspective; }
            set
            {
                if (_Perspective != value)
                {
                    _Perspective = value;
                    OnPropertyChanged(() => Perspective);
                }
            }
        }

        public string PerspectiveSub
        {
            get { return _PerspectiveSub; }
            set
            {
                if (_PerspectiveSub != value)
                {
                    _PerspectiveSub = value;
                    OnPropertyChanged(() => PerspectiveSub);
                }
            }
        }

        public int Year
        {
            get { return _Year; }
            set
            {
                if (_Year != value)
                {
                    _Year= value;
                     OnPropertyChanged(() => Year);
                }
            }
        }

        public int Position
        {
            get { return _Position; }
            set
            {
                if (_Position != value)
                {
                    _Position = value;
                    OnPropertyChanged(() => Position);
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
