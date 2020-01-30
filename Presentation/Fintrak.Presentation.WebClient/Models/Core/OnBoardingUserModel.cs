using Fintrak.Client.Core.Contracts;
using Fintrak.Client.SystemCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class OnBoardingUserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StaffId { get; set; }
        public string TeamDefinitionCode { get; set; }
        public string MISCode { get; set; }
        public string Status { get; set; }
    }
}