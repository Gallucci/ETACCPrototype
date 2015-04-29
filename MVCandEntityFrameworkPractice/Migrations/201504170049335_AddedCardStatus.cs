namespace MVCandEntityFrameworkPractice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCardStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Card", "StatusType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Card", "StatusType");
        }
    }
}
