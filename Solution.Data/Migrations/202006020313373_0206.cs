namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0206 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Participations", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Participations", "Title");
        }
    }
}
