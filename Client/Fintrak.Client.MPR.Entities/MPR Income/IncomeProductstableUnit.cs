
using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeProductstableUnit : ObjectBase
    {       
        public int ID { get; set; }

        public string Product { get; set; }
      
        public string ProductName { get; set; }

        public string Unit { get; set; }
    }
}
