namespace Taskv2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deneme1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Gorev",
                c => new
                    {
                        GorevID = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 50),
                        Aciklama = c.String(nullable: false, maxLength: 100),
                        Durum = c.String(nullable: false, maxLength: 50),
                        ProjeID = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.GorevID)
                .ForeignKey("dbo.Proje", t => t.ProjeID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.ProjeID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Proje",
                c => new
                    {
                        ProjeID = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 50),
                        Aciklama = c.String(nullable: false, maxLength: 100),
                        Durum = c.String(nullable: false, maxLength: 50),
                        isVisible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProjeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Gorev", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Gorev", "ProjeID", "dbo.Proje");
            DropIndex("dbo.Gorev", new[] { "UserID" });
            DropIndex("dbo.Gorev", new[] { "ProjeID" });
            DropTable("dbo.Proje");
            DropTable("dbo.Gorev");
        }
    }
}
