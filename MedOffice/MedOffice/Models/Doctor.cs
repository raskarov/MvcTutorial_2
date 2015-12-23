using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace MedOffice.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }

        public int SpecID { get; set; }
        public Specialization Spec { get; set; }

        public ICollection<Patient> Patients { get; set; }

        public string FullName
        {
            get
            {
                return Surname + " " + Name;
            }
        }

    }
}