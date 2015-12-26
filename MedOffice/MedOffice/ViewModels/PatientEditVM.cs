using MedOffice.DAL;
using MedOffice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedOffice.ViewModels
{
    public class PatientEditVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string Comment { get; set; }
        public int DoctorId { get; set; }

        public List<SelectListItem> list = new List<SelectListItem>();

        public PatientEditVM()
        {
            OfficeContext db = new OfficeContext();

            foreach  (var doctor in db.Doctors)
            {
                list.Add(new SelectListItem() { Text = doctor.FullName, Value = doctor.Id.ToString() });
            }
        }
    }
}