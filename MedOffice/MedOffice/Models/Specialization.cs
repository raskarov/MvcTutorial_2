using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MedOffice.Models
{
    public class Specialization
    {
        public int Id { get; set; }
        [Display(Name = "Specialization")]
        public string Name { get; set; }
    }
}