
using System;
using System.Linq;
using Fintrak.Shared.Common.Core;
using FluentValidation;

namespace Fintrak.Client.MPR.Entities
{
    public class GroupCaptions : ObjectBase
    {

        public int GroupCaptionID { get; set; }

        public string GroupCaption { get; set; }

    }
}
