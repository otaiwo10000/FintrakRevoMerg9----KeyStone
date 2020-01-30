using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class PLUploadedDataTEMP : ObjectBase
    {
        public int ID { get; set; }
        public string MIS_Code { get; set; }
        public string Maincaption { get; set; }
        public string Accountnumber { get; set; }
        public string Narrative { get; set; }
        public int Period { get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; }
        public string AcctOfficer { get; set; }
        public decimal Volume { get; set; }
        public string ApprovalStatus { get; set; }
        public bool _Migrated { get; set; }

        bool _Active;


        //public int Id
        //{
        //    get { return _Id; }
        //    set
        //    {
        //        if (_Id != value)
        //        {
        //            _Id = value;
        //            OnPropertyChanged(() => Id);
        //        }
        //    }
        //}

        //public string accountnumber
        //{
        //    get { return _accountnumber; }
        //    set
        //    {
        //        if (_accountnumber != value)
        //        {
        //            _accountnumber = value;
        //            OnPropertyChanged(() => accountnumber);
        //        }
        //    }
        //}

        //public string mis
        //{
        //    get { return _mis; }
        //    set
        //    {
        //        if (_mis != value)
        //        {
        //            _mis = value;
        //            OnPropertyChanged(() => mis);
        //        }
        //    }
        //}

        //public string AccountOfficer_Code
        //{
        //    get { return _AccountOfficer_Code; }
        //    set
        //    {
        //        if (_AccountOfficer_Code != value)
        //        {
        //            _AccountOfficer_Code = value;
        //            OnPropertyChanged(() => AccountOfficer_Code);
        //        }
        //    }
        //}

        //public string ApprovalStatus
        //{
        //    get { return _ApprovalStatus; }
        //    set
        //    {
        //        if (_ApprovalStatus != value)
        //        {
        //            _ApprovalStatus = value;
        //            OnPropertyChanged(() => ApprovalStatus);
        //        }
        //    }
        //}

        //public bool Migrated
        //{
        //    get { return _Migrated; }
        //    set
        //    {
        //        if (_Migrated != value)
        //        {
        //            _Migrated = value;
        //            OnPropertyChanged(() => Migrated);
        //        }
        //    }
        //}



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



        //class IncomeSplitPoolsRatesAndBasisValidator : AbstractValidator<IncomeSplitPoolsRatesAndBasis>
        //{
        //    public IncomeSplitPoolsRatesAndBasisValidator()
        //    {
        //        RuleFor(obj => obj._PoolRateFCYAsset).NotEmpty().WithMessage("Pool rate FCY asset is required.");
        //    }
        //}

        //protected override IValidator GetValidator()
        //{
        //    return new IncomeSplitPoolsRatesAndBasisValidator();
        //}
    }
}
