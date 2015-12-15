using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MedOffice.Models;


namespace MedOffice.DAL
{
    public class OfficeContext : DbContext
    {
        
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Specialization> Specialization { get; set; }

    }
}