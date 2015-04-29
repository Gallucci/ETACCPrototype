namespace MVCandEntityFrameworkPractice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeStatusDisplayNameToStatusType : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Catalog", "StatusDisplayName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Catalog", "StatusDisplayName", c => c.String());
        }
    }
}
