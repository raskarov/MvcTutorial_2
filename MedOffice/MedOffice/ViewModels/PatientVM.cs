using MedOffice.DAL;
using MedOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList.Mvc;
using System.Web.Mvc;
using PagedList;

namespace MedOffice.ViewModels
{
    public class PatientVM
    {
        public Patient patient { get; set; }

        public List<SelectListItem> list = new List<SelectListItem>();
        public PagedList.IPagedList<MedOffice.Models.Patient> Patients;
        public PatientVM()
        {
            OfficeContext db = new OfficeContext();

            foreach (var doctor in db.Doctors)
            {
                list.Add(new SelectListItem() { Text = doctor.FullName, Value = doctor.Id.ToString() });
            }
        }
    }
}