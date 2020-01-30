
using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class IncomeLineCapton : ObjectBase
    {      
        public int IncomeLineCaptonId { get; set; }

        public string Name { get; set; }
    }
}
