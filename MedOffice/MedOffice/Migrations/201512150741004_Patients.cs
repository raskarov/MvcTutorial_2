namespace MedOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Patients : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Patients", "Doctor_Id", "dbo.Doctors");
            DropIndex("dbo.Patients", new[] { "Doctor_Id" });
            RenameColumn(table: "dbo.Patients", name: "Doctor_Id", newName: "DoctorID");
            AlterColumn("dbo.Patients", "DoctorID", c => c.Int(nullable: false));
            CreateIndex("dbo.Patients", "DoctorID");
            AddForeignKey("dbo.Patients", "DoctorID", "dbo.Doctors", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "DoctorID", "dbo.Doctors");
            DropIndex("dbo.Patients", new[] { "DoctorID" });
            AlterColumn("dbo.Patients", "DoctorID", c => c.Int());
            RenameColumn(table: "dbo.Patients", name: "DoctorID", newName: "Doctor_Id");
            CreateIndex("dbo.Patients", "Doctor_Id");
            AddForeignKey("dbo.Patients", "Doctor_Id", "dbo.Doctors", "Id");
        }
    }
}
