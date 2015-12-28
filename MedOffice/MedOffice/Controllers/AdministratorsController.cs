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
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MedOffice.ViewModels;

namespace MedOffice.Controllers
{
    public class AdministratorsController : Controller
    {
        private OfficeContext db = new OfficeContext();

        private LoginUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<LoginUserManager>();
            }
        }

        // GET: Administrators
        [Authorize(Roles ="admin")]
        public ActionResult Index()
        {
            AdminVM Administrators = new AdminVM();
            Administrators.list = db.Administrators.ToList();
            return View(Administrators);
        }


        // GET: Administrators/Create
        public ActionResult Create()
        {
            AdminVM ViewModel = new AdminVM();
            return PartialView("Create",ViewModel);
        }

        // POST: Administrators/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( AdminVM ViewModel, string password)
        {

            Administrator administrator = ViewModel.admin;
            LoginUser user = new LoginUser { UserName = administrator.Email, Email = administrator.Email };
            var result = UserManager.Create(user, password);
            if(result.Succeeded)
            {
                UserManager.AddToRole(user.Id, "admin");
            }
            if (ModelState.IsValid)
            {
                db.Administrators.Add(administrator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ViewModel);
        }

        // GET: Administrators/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administrators.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            AdminVM ViewModel = new AdminVM { };
            ViewModel.admin = administrator;
            ViewModel.LoginEmail = administrator.Email;
            return PartialView("Edit",ViewModel);
        }

        // POST: Administrators/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdminVM ViewModel)
        {
            Administrator admin = ViewModel.admin; 
            LoginUser user = UserManager.FindByEmail(ViewModel.LoginEmail);
            user.Email = ViewModel.admin.Email;
            user.UserName = ViewModel.admin.Email;
            UserManager.Update(user);
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ViewModel);
        }

        //GET: Administrators/Delete/5
        public ActionResult Delete(int? id)
        {
                LoginUser user = UserManager.FindByName(User.Identity.Name.ToString());
                string Email = user.Email;
                Administrator admin = db.Administrators.Where(a => Email == a.Email).FirstOrDefault();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (admin.ID == id)
                {
                    return PartialView("Fail");
                }
                Administrator administrator = db.Administrators.Find(id);
                if (administrator == null)
                {
                    return HttpNotFound();
                }
                AdminVM Administrator = new AdminVM { };
                Administrator.admin = administrator;
                return PartialView("Delete",Administrator);
            
        }

        // POST: Administrators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            Administrator administrator = db.Administrators.Find(id);
            LoginUser user = UserManager.FindByEmail(administrator.Email);
            UserManager.Delete(user);
            db.Administrators.Remove(administrator);
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
