using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MedOffice.Models;
using System.Web.Mvc;
using MedOffice.DAL;

namespace MedOffice.ViewModels
{
    public class DoctorVM
    {
        public Doctor doctor { get; set; }
        public string LoginEmail { get; set; }
        public PagedList.IPagedList<MedOffice.Models.Doctor> Doctors;
        public List<SelectListItem> list = new List<SelectListItem>();
        public PagedList.IPagedList<MedOffice.Models.Patient> Patients;

        public DoctorVM()
        {
            OfficeContext db = new OfficeContext();

            foreach (var spec in db.Specialization)
            {
                list.Add(new SelectListItem() { Text = spec.Name, Value = spec.Id.ToString() });
            }
        }
    }
}