using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MedOffice.Models;

namespace MedOffice.ViewModels
{
    public class AdminVM
    {
        public Administrator admin { get; set; }
        public string LoginEmail { get; set; }

        public IEnumerable<MedOffice.Models.Administrator> list { get; set; }
    }
}