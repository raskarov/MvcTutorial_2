using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedOffice.Models
{
    public class Patient
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
    }
}