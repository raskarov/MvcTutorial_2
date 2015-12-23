using MedOffice.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedOffice.Models
{
    public class LoginUserManager : UserManager<LoginUser>
    {
        public LoginUserManager(IUserStore<LoginUser> store) : base(store) { }

        public static LoginUserManager Create (IdentityFactoryOptions<LoginUserManager> options, IOwinContext context)
        {
            LoginContext ldb = context.Get<LoginContext>();
            LoginUserManager manager = new LoginUserManager(new UserStore<LoginUser>(ldb));
            return manager;
        }
    }
}