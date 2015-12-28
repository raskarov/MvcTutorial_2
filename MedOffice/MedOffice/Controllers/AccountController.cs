using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MedOffice.DAL;
using MedOffice.Models;
using PagedList;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace MedOffice.Controllers
{
    public class AccountController : Controller
    {
        private LoginUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<LoginUserManager>();
            }
        }




        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                LoginUser user = await UserManager.FindAsync(model.Email, model.password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);

                    if (user.Roles.First().RoleId.ToString() == "1")
                    {
                        return RedirectToAction("AdminIndex", "Home");
                    }
                    if (user.Roles.First().RoleId.ToString() == "2")
                    {
                        return RedirectToAction("DoctorIndex", "Home");
                    }
                }
            }
            return View(model);
        }


        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }

    }
    }
