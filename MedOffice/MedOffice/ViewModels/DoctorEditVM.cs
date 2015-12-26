using MedOffice.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedOffice.ViewModels
{
    public class DoctorEditVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string LoginEmail { get; set; }
        public int SpecID { get; set; }

        public List<SelectListItem> list = new List<SelectListItem>();

        OfficeContext db = new OfficeContext();

        public DoctorEditVM()
        {
            foreach (var spec in db.Specialization)
            {
                list.Add(new SelectListItem() { Text = spec.Name, Value = spec.Id.ToString() });
            }
        }
}
}