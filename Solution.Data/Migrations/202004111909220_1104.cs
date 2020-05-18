namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1104 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activites", "Document", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Activites", "Document");
        }
    }
}
