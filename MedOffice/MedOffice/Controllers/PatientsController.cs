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
using MedOffice.ViewModels;

namespace MedOffice.Controllers
{
    public class PatientsController : Controller
    {
        private OfficeContext db = new OfficeContext();

        [Authorize]
        // GET: Patients
        public ActionResult Index(int? page, string sortOrder, string searchString, string currentFilter)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DoctSortParm = sortOrder == "Doct" ? "doct_desc" : "Doct";

            if(searchString!= null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var patients = db.Patients.Include(p => p.Doctor);
            if (!String.IsNullOrEmpty(searchString))
            {
                patients = patients.Where(x => x.Name.Contains(searchString) || x.Surname.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    patients = patients.OrderByDescending(x => x.Name);
                    break;
                case "Doct":
                    patients = patients.OrderBy(x => x.Doctor.Surname);
                    break;
                case "doct_desc":
                    patients = patients.OrderByDescending(x => x.Doctor.Surname);
                    break;
                default:
                    patients = patients.OrderBy(x => x.Name);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            PatientVM ViewModel = new PatientVM();
            ViewModel.Patients = patients.ToPagedList(pageNumber,pageSize);
            return View(ViewModel);
        }


        // GET: Patients/Create
        public ActionResult Create()
        {
            PatientVM ViewModel = new PatientVM();
            return PartialView("Create",ViewModel);
        }

        // POST: Patients/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PatientVM ViewModel)
        {
            Patient patient = ViewModel.patient;
            if (ModelState.IsValid)
            {
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ViewModel);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient Patient = db.Patients.Find(id);
            if (Patient == null)
            {
                return HttpNotFound();
            }
            PatientVM ViewModel = new PatientVM { patient = Patient };
            return PartialView("Edit",ViewModel);
        }

        // POST: Patients/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PatientVM ViewModel )
        {
            Patient patient = ViewModel.patient;
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return PartialView(ViewModel);
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient Patient = db.Patients.Find(id);
            if (Patient == null)
            {
                return HttpNotFound();
            }
            PatientVM ViewModel = new PatientVM { patient = Patient };
            return PartialView("Delete",ViewModel);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
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
