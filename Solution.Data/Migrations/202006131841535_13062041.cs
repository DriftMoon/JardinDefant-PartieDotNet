namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _13062041 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activites",
                c => new
                    {
                        ActiviteID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Outils = c.String(),
                        Type = c.String(),
                        Theme = c.String(),
                        AgeMin = c.Int(nullable: false),
                        AgeMax = c.Int(nullable: false),
                        ClassSize = c.Int(nullable: false),
                        Duration = c.Int(nullable: false),
                        Location = c.String(),
                        Affiche = c.String(),
                        Document = c.String(),
                        Start = c.DateTime(nullable: false),
                        Professor = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ActiviteID)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Enfants",
                c => new
                    {
                        EnfantId = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Prenom = c.String(),
                        Image = c.String(),
                        Age = c.Int(nullable: false),
                        Sexe = c.Int(nullable: false),
                        ParentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EnfantId)
                .ForeignKey("dbo.Users", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Role = c.String(),
                        image = c.String(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                        ParentId = c.Int(),
                        id_admin = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Publication_PublicationId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Publications", t => t.Publication_PublicationId)
                .Index(t => t.Publication_PublicationId);
            
            CreateTable(
                "dbo.CustomUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Formations",
                c => new
                    {
                        FormationID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        Theme = c.String(),
                        IsFullDay = c.Boolean(nullable: false),
                        NbrMax = c.Int(nullable: false),
                        Reserved = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Location = c.String(),
                        Affiche = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FormationID)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        idUser = c.Int(nullable: false),
                        idPub = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.idUser, t.idPub })
                .ForeignKey("dbo.Publications", t => t.idPub, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.idUser, cascadeDelete: true)
                .Index(t => t.idUser)
                .Index(t => t.idPub);
            
            CreateTable(
                "dbo.Publications",
                c => new
                    {
                        PublicationId = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        description = c.String(),
                        image = c.String(),
                        visibility = c.Int(nullable: false),
                        creationDate = c.DateTime(nullable: false),
                        ownerimg = c.String(),
                        nomuser = c.String(),
                        OwnerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PublicationId)
                .ForeignKey("dbo.Users", t => t.OwnerId)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Contenu = c.String(),
                        PublicationId = c.Int(),
                        ownerimg = c.String(),
                        nomuser = c.String(),
                        OwnerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Publications", t => t.PublicationId, cascadeDelete: true)
                .Index(t => t.PublicationId);
            
            CreateTable(
                "dbo.Replies",
                c => new
                    {
                        ReplyId = c.Int(nullable: false, identity: true),
                        Contenu = c.String(),
                        CommentId = c.Int(nullable: false),
                        ownerimg = c.String(),
                        nomuser = c.String(),
                        OwnerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReplyId)
                .ForeignKey("dbo.Comments", t => t.CommentId, cascadeDelete: true)
                .Index(t => t.CommentId);
            
            CreateTable(
                "dbo.CustomUserLogins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CustomUserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CustomRole_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.CustomRoles", t => t.CustomRole_Id)
                .Index(t => t.UserId)
                .Index(t => t.CustomRole_Id);
            
            CreateTable(
                "dbo.Participations",
                c => new
                    {
                        EnfantId = c.Int(nullable: false),
                        FormationID = c.Int(nullable: false),
                        Title = c.String(),
                    })
                .PrimaryKey(t => new { t.EnfantId, t.FormationID })
                .ForeignKey("dbo.Enfants", t => t.EnfantId)
                .ForeignKey("dbo.Formations", t => t.FormationID, cascadeDelete: true)
                .Index(t => t.EnfantId)
                .Index(t => t.FormationID);
            
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
            
            CreateTable(
                "dbo.CustomRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId });
            
            CreateTable(
                "dbo.EnfantActivites",
                c => new
                    {
                        Enfant_EnfantId = c.Int(nullable: false),
                        Activite_ActiviteID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Enfant_EnfantId, t.Activite_ActiviteID })
                .ForeignKey("dbo.Enfants", t => t.Enfant_EnfantId, cascadeDelete: true)
                .ForeignKey("dbo.Activites", t => t.Activite_ActiviteID, cascadeDelete: true)
                .Index(t => t.Enfant_EnfantId)
                .Index(t => t.Activite_ActiviteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomUserRoles", "CustomRole_Id", "dbo.CustomRoles");
            DropForeignKey("dbo.Questions", "IDQuiz", "dbo.Quizs");
            DropForeignKey("dbo.Activites", "UserId", "dbo.Users");
            DropForeignKey("dbo.Participations", "FormationID", "dbo.Formations");
            DropForeignKey("dbo.Participations", "EnfantId", "dbo.Enfants");
            DropForeignKey("dbo.Enfants", "ParentId", "dbo.Users");
            DropForeignKey("dbo.Formations", "UserId", "dbo.Users");
            DropForeignKey("dbo.CustomUserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.CustomUserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.Likes", "idUser", "dbo.Users");
            DropForeignKey("dbo.Likes", "idPub", "dbo.Publications");
            DropForeignKey("dbo.Publications", "OwnerId", "dbo.Users");
            DropForeignKey("dbo.Comments", "PublicationId", "dbo.Publications");
            DropForeignKey("dbo.Replies", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.Users", "Publication_PublicationId", "dbo.Publications");
            DropForeignKey("dbo.CustomUserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.EnfantActivites", "Activite_ActiviteID", "dbo.Activites");
            DropForeignKey("dbo.EnfantActivites", "Enfant_EnfantId", "dbo.Enfants");
            DropIndex("dbo.EnfantActivites", new[] { "Activite_ActiviteID" });
            DropIndex("dbo.EnfantActivites", new[] { "Enfant_EnfantId" });
            DropIndex("dbo.Questions", new[] { "IDQuiz" });
            DropIndex("dbo.Participations", new[] { "FormationID" });
            DropIndex("dbo.Participations", new[] { "EnfantId" });
            DropIndex("dbo.CustomUserRoles", new[] { "CustomRole_Id" });
            DropIndex("dbo.CustomUserRoles", new[] { "UserId" });
            DropIndex("dbo.CustomUserLogins", new[] { "UserId" });
            DropIndex("dbo.Replies", new[] { "CommentId" });
            DropIndex("dbo.Comments", new[] { "PublicationId" });
            DropIndex("dbo.Publications", new[] { "OwnerId" });
            DropIndex("dbo.Likes", new[] { "idPub" });
            DropIndex("dbo.Likes", new[] { "idUser" });
            DropIndex("dbo.Formations", new[] { "UserId" });
            DropIndex("dbo.CustomUserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "Publication_PublicationId" });
            DropIndex("dbo.Enfants", new[] { "ParentId" });
            DropIndex("dbo.Activites", new[] { "UserId" });
            DropTable("dbo.EnfantActivites");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.CustomRoles");
            DropTable("dbo.Quizs");
            DropTable("dbo.Questions");
            DropTable("dbo.Participations");
            DropTable("dbo.CustomUserRoles");
            DropTable("dbo.CustomUserLogins");
            DropTable("dbo.Replies");
            DropTable("dbo.Comments");
            DropTable("dbo.Publications");
            DropTable("dbo.Likes");
            DropTable("dbo.Formations");
            DropTable("dbo.CustomUserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Enfants");
            DropTable("dbo.Activites");
        }
    }
}
