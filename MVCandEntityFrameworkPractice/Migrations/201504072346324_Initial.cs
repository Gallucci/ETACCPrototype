namespace MVCandEntityFrameworkPractice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccessLevel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                        InternalName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Card",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CatalogId = c.Int(),
                        HolderId = c.Int(),
                        Iso = c.Long(nullable: false),
                        Pin = c.Int(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        StatusDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Catalog", t => t.CatalogId)
                .ForeignKey("dbo.Person", t => t.HolderId)
                .Index(t => t.CatalogId)
                .Index(t => t.HolderId);
            
            CreateTable(
                "dbo.Catalog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        StatusDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        MiddleName = c.String(),
                        PreferredFirstName = c.String(),
                        NetId = c.String(),
                        StudentId = c.Int(),
                        DesigneeId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Person", t => t.DesigneeId)
                .Index(t => t.DesigneeId);
            
            CreateTable(
                "dbo.CardAccessLevel",
                c => new
                    {
                        CardId = c.Int(nullable: false),
                        AccessLevelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CardId, t.AccessLevelId })
                .ForeignKey("dbo.Card", t => t.CardId, cascadeDelete: true)
                .ForeignKey("dbo.AccessLevel", t => t.AccessLevelId, cascadeDelete: true)
                .Index(t => t.CardId)
                .Index(t => t.AccessLevelId);
            
            CreateTable(
                "dbo.CatalogAccessLevel",
                c => new
                    {
                        CatalogId = c.Int(nullable: false),
                        AccessLevelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CatalogId, t.AccessLevelId })
                .ForeignKey("dbo.Catalog", t => t.CatalogId, cascadeDelete: true)
                .ForeignKey("dbo.AccessLevel", t => t.AccessLevelId, cascadeDelete: true)
                .Index(t => t.CatalogId)
                .Index(t => t.AccessLevelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Person", "DesigneeId", "dbo.Person");
            DropForeignKey("dbo.Card", "HolderId", "dbo.Person");
            DropForeignKey("dbo.Card", "CatalogId", "dbo.Catalog");
            DropForeignKey("dbo.CatalogAccessLevel", "AccessLevelId", "dbo.AccessLevel");
            DropForeignKey("dbo.CatalogAccessLevel", "CatalogId", "dbo.Catalog");
            DropForeignKey("dbo.CardAccessLevel", "AccessLevelId", "dbo.AccessLevel");
            DropForeignKey("dbo.CardAccessLevel", "CardId", "dbo.Card");
            DropIndex("dbo.CatalogAccessLevel", new[] { "AccessLevelId" });
            DropIndex("dbo.CatalogAccessLevel", new[] { "CatalogId" });
            DropIndex("dbo.CardAccessLevel", new[] { "AccessLevelId" });
            DropIndex("dbo.CardAccessLevel", new[] { "CardId" });
            DropIndex("dbo.Person", new[] { "DesigneeId" });
            DropIndex("dbo.Card", new[] { "HolderId" });
            DropIndex("dbo.Card", new[] { "CatalogId" });
            DropTable("dbo.CatalogAccessLevel");
            DropTable("dbo.CardAccessLevel");
            DropTable("dbo.Person");
            DropTable("dbo.Catalog");
            DropTable("dbo.Card");
            DropTable("dbo.AccessLevel");
        }
    }
}
