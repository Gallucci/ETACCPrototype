namespace MVCandEntityFrameworkPractice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeStatusDisplayNameToStatusType2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Catalog", "StatusType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Catalog", "StatusType");
        }
    }
}
