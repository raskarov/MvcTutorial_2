using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedOffice.Models;
using Newtonsoft.Json;
using MedOffice.DAL;
using System.Data.Entity;

namespace MedOffice.Controllers
{
    public class HomeController : Controller
    {
        private OfficeContext db = new OfficeContext();

        public ActionResult Index()
        {          
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public void Edit(Doctor Doctor)
        {
            db.Entry(Doctor).State = EntityState.Modified;
            db.SaveChanges();
        }

        [HttpPost]
        public void Create(Doctor Doctor)
        {
            //var SpecID = db.Specialization.Where(d => d.Name == Doctor.Spec.Name).Select(d => d.Id);
            //var List = SpecID.ToList();
            //var ID = List[0];
            //Doctor doctor = new Doctor { Name = Doctor.Name, Surname = Doctor.Surname, Email = Doctor.Email, DateOfBirth = Doctor.DateOfBirth };
            db.Doctors.Add(Doctor);
            db.SaveChanges();
        }
        [HttpPost]
        public void Delete(int id)
        {
            Doctor doctor = db.Doctors.Find(id);
            db.Doctors.Remove(doctor);
            db.SaveChanges(); 
        }

        public string GetData()
        {
            var doctors = db.Doctors.Include(d => d.Spec);
            return JsonConvert.SerializeObject(doctors);
        }
    }
}