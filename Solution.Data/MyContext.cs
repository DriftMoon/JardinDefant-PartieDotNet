using Microsoft.AspNet.Identity.EntityFramework;
using Solution.Data.Configurations;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Data
{
    public class MyContext:IdentityDbContext<User>
    {
        public MyContext():base("KinderGarten")
        {

            Database.SetInitializer(new ContexInit());

        }
        public static MyContext Create()
        {
            return new MyContext();
        }


        //les dbsets
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Formation> Formations { get; set; }
        public DbSet<Activite> Activites { get; set; }
        public DbSet<Enfant> Enfants { get; set; }
        public DbSet<Participation> participations { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new IdentityUserLoginConfiguration());
            modelBuilder.Configurations.Add(new IdentityUserRoleConfiguration());
            modelBuilder.Configurations.Add(new PublicationConfiguration());
            modelBuilder.Configurations.Add(new LikeConfiguration());
            modelBuilder.Configurations.Add(new ReplyConfiguration());
            modelBuilder.Configurations.Add(new FormationConfiguration());
            modelBuilder.Configurations.Add(new ActiviteConfiguration());
            modelBuilder.Configurations.Add(new EnfantConfiguration());
            modelBuilder.Configurations.Add(new ParticipationConfiguration());
            //config + conventions
            //modelBuilder.Configurations.Add(...);
            //modelBuilder.Conventions.Add(...);
        }
        public class ContexInit : DropCreateDatabaseIfModelChanges<MyContext>
        {
            protected override void Seed(MyContext context)
            {
                /*   List<Patient> patients = new List<Patient>() {
                       new Patient {PatientId=1
                                   }

                   };
                   context.Patients.AddRange(patients);
                   context.SaveChanges();*/
                
            }
        }

    }
}
