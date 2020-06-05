namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ichrak1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Category = c.Int(nullable: false),
                        Phone = c.Int(nullable: false),
                        DateEvent = c.DateTime(nullable: false),
                        Debut = c.DateTime(nullable: false),
                        Fin = c.DateTime(nullable: false),
                        Description = c.String(),
                        Image = c.String(),
                        DirecteurFk = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.Users", t => t.DirecteurFk)
                .Index(t => t.DirecteurFk);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "DirecteurFk", "dbo.Users");
            DropIndex("dbo.Events", new[] { "DirecteurFk" });
            DropTable("dbo.Events");
        }
    }
}
