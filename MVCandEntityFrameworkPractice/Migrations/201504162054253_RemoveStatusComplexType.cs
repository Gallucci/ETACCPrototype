namespace MVCandEntityFrameworkPractice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveStatusComplexType : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Catalog", "Status_DisplayName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Catalog", "Status_DisplayName", c => c.String());
        }
    }
}
