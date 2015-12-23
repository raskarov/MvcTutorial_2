using MedOffice.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedOffice.DAL
{
    public class LoginContext : IdentityDbContext<LoginUser>
    {
        public LoginContext() : base("OfficeContext") { }

        public static LoginContext Create()
        {
            return new LoginContext();
        }
    }
}