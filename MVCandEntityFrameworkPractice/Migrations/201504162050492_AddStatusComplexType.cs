namespace MVCandEntityFrameworkPractice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusComplexType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Catalog", "Status_DisplayName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Catalog", "Status_DisplayName");
        }
    }
}
