using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedOffice.Models
{
    public class Administrator
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }

    }
}