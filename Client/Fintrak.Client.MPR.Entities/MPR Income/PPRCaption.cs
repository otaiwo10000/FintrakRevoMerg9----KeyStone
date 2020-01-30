
using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class PPRCaption : ObjectBase
    {
        public int PPR_CaptionId { get; set; }

        public string Name { get; set; }
    }
}
