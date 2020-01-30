
using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class Caption : ObjectBase
    {

        public int CaptionId { get; set; }

        public string Name { get; set; }

    }
}
