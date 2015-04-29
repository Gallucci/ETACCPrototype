namespace MVCandEntityFrameworkPractice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStatusAsUnmapped : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Catalog", "StatusDisplayName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Catalog", "StatusDisplayName");
        }
    }
}
