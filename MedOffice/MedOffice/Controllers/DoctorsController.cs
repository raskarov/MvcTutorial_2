﻿using System;
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
            DoctorVM ViewModel = new DoctorVM();
            ViewModel.Doctors = Doctors.ToPagedList(PageNumber,PageSize);
            return View(ViewModel);
        }

        // GET: Doctors/Details/5


        // GET: Doctors/Create
        public ActionResult Create()
        {
            DoctorVM ViewModel = new DoctorVM();
            return PartialView("Create",ViewModel);
        }

        // POST: Doctors/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DoctorVM ViewModel, string password)
        {

            Doctor doctor = ViewModel.doctor;
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

            return View(ViewModel);
        }

        // GET: Doctors/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor Doctor = db.Doctors.Find(id);
            if (Doctor == null)
            {
                return HttpNotFound();
            }
            DoctorVM ViewModel = new DoctorVM();
            ViewModel.doctor = Doctor;
            ViewModel.LoginEmail = Doctor.Email;
            return PartialView("Edit",ViewModel);
        }

        // POST: Doctors/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DoctorVM ViewModel)
        {
            Doctor doctor = ViewModel.doctor;
            LoginUser user = UserManager.FindByEmail(ViewModel.LoginEmail);
            user.UserName = ViewModel.doctor.Email;
            user.Email = ViewModel.doctor.Email;
            UserManager.Update(user);
            if (ModelState.IsValid)
            {
                db.Entry(doctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ViewModel);
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
            DoctorVM ViewModel = new DoctorVM();
            ViewModel.doctor = doctor;
            return PartialView("Delete",ViewModel);
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
            DoctorVM ViewModel = new DoctorVM();
            ViewModel.Patients = Pat.ToPagedList(pageNumber, pageSize);
            return PartialView("~/Views/Patients/PatPartV.cshtml", ViewModel);

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
