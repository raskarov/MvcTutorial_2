using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedOffice.ViewModels
{
    public class AdminEditVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string LoginEmail { get; set; }
    }
}