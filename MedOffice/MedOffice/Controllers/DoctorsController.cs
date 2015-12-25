using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedOffice.DAL;
using MedOffice.Models;
using PagedList;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using MedOffice.ViewModels;

namespace MedOffice.Controllers
{
    public class DoctorsController : Controller
    {
        private OfficeContext db = new OfficeContext();

        private LoginUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<LoginUserManager>();
            }
        }

        // GET: Doctors
        [Authorize(Roles ="admin")]
        public ActionResult Index(int? page, string sortOrder, string searchString, string currentFilter)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SpecSortParm = sortOrder == "Spec" ? "spec_desc" : "Spec";
            var Doctors = db.Doctors.Include(d => d.Spec);
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                Doctors = Doctors.Where(d => d.Surname.Contains(searchString) || d.Name.Contains(searchString));
            }
            
            switch(sortOrder)
            {
                case "name_desc":
                    Doctors = Doctors.OrderByDescending(d => d.Name);
                    break;
                case "Spec":
                    Doctors = Doctors.OrderBy(d => d.Spec.Name);
                    break;
                case "spec_desc":
                    Doctors = Doctors.OrderByDescending(d => d.Spec.Name);
                    break;
                default:
                    Doctors = Doctors.OrderBy(d => d.Name);
                    break;
                    
            }
            int PageSize = 5;
            int PageNumber = (page ?? 1);
            
            return View(Doctors.ToPagedList(PageNumber,PageSize));
        }

        // GET: Doctors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Doct = db.Doctors.Include(d => d.Spec).ToList();
            var doctor = Doct.Find(x => x.Id == id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // GET: Doctors/Create
        public ActionResult Create()
        {
            CreateDoctorVM CreateDoctor = new CreateDoctorVM();
            return View(CreateDoctor);
        }

        // POST: Doctors/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateDoctorVM ViewModel, string password)
        {

            Doctor doctor = new Doctor { Name = ViewModel.Name, Surname = ViewModel.Surname, DateOfBirth = ViewModel.DateOfBirth, Email = ViewModel.Email, SpecID = ViewModel.SpecId };
            LoginUser user = new LoginUser { UserName = doctor.Email, Email = doctor.Email };
            var result = UserManager.Create(user, password);
            if(result.Succeeded)
            {
                UserManager.AddToRole(user.Id, "doctor");
            }
            if (ModelState.IsValid)
            {
                db.Doctors.Add(doctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }            

            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Surname,DateOfBirth,Email,SpecID")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Doct = db.Doctors.Include(d => d.Spec).ToList();
            var doctor = Doct.Find(x => x.Id == id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        public ActionResult Patients(int? id,int? page,string sortOrder, string searchString, string currentFilter)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Pat = db.Patients.Include(p => p.Doctor);
            Pat = Pat.Where(p => p.DoctorID == id);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SpecSortParm = sortOrder == "Spec" ? "spec_desc" : "Spec";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                Pat = Pat.Where(x => x.Name.Contains(searchString) || x.Surname.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    Pat =Pat.OrderByDescending(x => x.Name);
                    break;
                case "Doct":
                    Pat = Pat.OrderBy(x => x.Doctor.Surname);
                    break;
                case "doct_desc":
                    Pat = Pat.OrderByDescending(x => x.Doctor.Surname);
                    break;
                default:
                    Pat = Pat.OrderBy(x => x.Name);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);


            return View(Pat.ToPagedList(pageNumber,pageSize));

        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Doctor doctor = db.Doctors.Find(id);
            LoginUser user = UserManager.FindByEmail(doctor.Email);
            UserManager.Delete(user);
            db.Doctors.Remove(doctor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
