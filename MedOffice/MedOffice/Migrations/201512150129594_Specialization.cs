namespace MedOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Specialization : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Specializations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Doctors", "Spec_Id", c => c.Int());
            CreateIndex("dbo.Doctors", "Spec_Id");
            AddForeignKey("dbo.Doctors", "Spec_Id", "dbo.Specializations", "Id");
            DropColumn("dbo.Doctors", "Spec");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Doctors", "Spec", c => c.String());
            DropForeignKey("dbo.Doctors", "Spec_Id", "dbo.Specializations");
            DropIndex("dbo.Doctors", new[] { "Spec_Id" });
            DropColumn("dbo.Doctors", "Spec_Id");
            DropTable("dbo.Specializations");
        }
    }
}
