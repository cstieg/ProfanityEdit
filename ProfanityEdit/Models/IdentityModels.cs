using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProfanityEdit.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [StringLength(30)]
        public string DisplayName { get; set; }

        [ForeignKey("UserPreferenceSet")]
        public int? UserPreferenceSetId { get; set; }
        public virtual UserPreferenceSet UserPreferenceSet { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Movie> Movies { get; set; }

        public virtual DbSet<Rating> Ratings { get; set; }

        public virtual DbSet<Profanity> Profanities { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<EditList> EditLists { get; set; }

        public virtual DbSet<EditListItem> EditListItems { get; set; }

        public virtual DbSet<ObjectionableScene> ObjectionableScenes { get; set; }

        public virtual DbSet<UserPreferenceSet> UserPreferenceSets { get; set; }

        public virtual DbSet<UserPreferenceItem> UserPreferenceItems { get; set; }
    }
}