
using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class PLCaption2 : ObjectBase
    {
        public int PL_CaptionId { get; set; }

        public string Name { get; set; }
    }
}
