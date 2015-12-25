using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MedOffice.Models;
using MedOffice.DAL;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MedOffice.ViewModels
{
    public class CreateDoctorVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public int SpecId { get; set; }

        public List<SelectListItem> list = new List<SelectListItem>();
    
        public CreateDoctorVM ()
        {
            OfficeContext db = new OfficeContext();

            foreach (var spec in db.Specialization)
            {
                list.Add(new SelectListItem() { Text = spec.Name, Value = spec.Id.ToString() });
            }
        }

}
}