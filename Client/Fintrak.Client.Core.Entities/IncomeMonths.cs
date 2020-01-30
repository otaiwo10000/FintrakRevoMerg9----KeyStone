using System;
using System.Linq;
using FluentValidation;
using Fintrak.Shared.Common.Core;

namespace Fintrak.Client.Core.Entities
{
    public class IncomeMonths : ObjectBase
    {
       
        public int ID { get; set; }

        public int Period { get; set; }

        public string Month { get; set; }

        public int NumberOfDaysInMonth { get; set; }

        public int Year { get; set; }

        public int LastDay { get; set; }

        public bool Active { get; set; }
       

       
        //class StaffValidator : AbstractValidator<Staff>
        //{
        //    public StaffValidator()
        //    {
        //        RuleFor(obj => obj.StaffCode).NotEmpty().WithMessage("StaffCode must not be empty.");
        //        RuleFor(obj => obj.Name).NotEmpty().WithMessage("Name must not be empty.");
               
        //    }
        //}

        //protected override IValidator GetValidator()
        //{
        //    return new StaffValidator();
        //}
    }
}
