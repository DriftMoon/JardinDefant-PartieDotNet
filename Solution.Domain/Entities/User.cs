using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

public class CustomUserLogin : IdentityUserLogin<int>
{
    public int Id { get; set; }

}
public class CustomUserRole : IdentityUserRole<int>
{
    public int Id { get; set; }
}
public class CustomUserClaim : IdentityUserClaim<int>
{

}
public class CustomRole : IdentityRole<int, CustomUserRole>
{
    public CustomRole() { }
    public CustomRole(string name) { Name = name; }
}
namespace Solution.Domain.Entities
{
    public class User : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; }
        public string image { get; set; }

        public virtual ICollection<Publication> publications { get; set; }

        //public virtual ICollection<Reclamation> Complaintsendes { get; set; }

        //public virtual ICollection<Response> responsesSent { get; set; }

        //public virtual ICollection<Reclamation> Complaintrecevies { get; set; }

        public virtual ICollection<Formation> Formations { get; set; }

        public virtual ICollection<Activite> Activites { get; set; }

        public virtual ICollection<Reclamation> Complaintsendes { get; set; }

        public virtual ICollection<Response> responsesSent { get; set; }

        public virtual ICollection<Reclamation> Complaintrecevies { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {
            // Note the authenticationType must match the one defined in
            // CookieAuthenticationOptions.AuthenticationType 
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here 
            return userIdentity;
        }
    }
}
