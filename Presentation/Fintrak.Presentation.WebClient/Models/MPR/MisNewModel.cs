using Fintrak.Client.MPR.Contracts;
using Fintrak.Client.MPR.Entities;
using Fintrak.Client.Core.Contracts;
using Fintrak.Client.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Presentation.WebClient.Models
{
    public class MisNewModel
    {
        public int Id { get; set; }
        public string old_mis { get; set; }
        public string new_mis { get; set; }
        public string teamname { get; set; }
        public string State { get; set; }

        public string username { get; set; }

   
    }

   
}