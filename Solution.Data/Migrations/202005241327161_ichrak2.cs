namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ichrak2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "qrCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "qrCode");
        }
    }
}
