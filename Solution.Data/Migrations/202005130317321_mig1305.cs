namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1305 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionID = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Opt1 = c.String(),
                        Opt2 = c.String(),
                        Opt3 = c.String(),
                        Opt4 = c.String(),
                        CorrectOpt = c.Int(nullable: false),
                        IDQuiz = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionID)
                .ForeignKey("dbo.Quizs", t => t.IDQuiz, cascadeDelete: true)
                .Index(t => t.IDQuiz);
            
            CreateTable(
                "dbo.Quizs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Titre = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "IDQuiz", "dbo.Quizs");
            DropIndex("dbo.Questions", new[] { "IDQuiz" });
            DropTable("dbo.Quizs");
            DropTable("dbo.Questions");
        }
    }
}
