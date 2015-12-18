namespace MedOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDoctors : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Doctors", "Spec_Id", "dbo.Specializations");
            DropIndex("dbo.Doctors", new[] { "Spec_Id" });
            RenameColumn(table: "dbo.Doctors", name: "Spec_Id", newName: "SpecID");
            AlterColumn("dbo.Doctors", "SpecID", c => c.Int(nullable: false));
            CreateIndex("dbo.Doctors", "SpecID");
            AddForeignKey("dbo.Doctors", "SpecID", "dbo.Specializations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Doctors", "SpecID", "dbo.Specializations");
            DropIndex("dbo.Doctors", new[] { "SpecID" });
            AlterColumn("dbo.Doctors", "SpecID", c => c.Int());
            RenameColumn(table: "dbo.Doctors", name: "SpecID", newName: "Spec_Id");
            CreateIndex("dbo.Doctors", "Spec_Id");
            AddForeignKey("dbo.Doctors", "Spec_Id", "dbo.Specializations", "Id");
        }
    }
}
