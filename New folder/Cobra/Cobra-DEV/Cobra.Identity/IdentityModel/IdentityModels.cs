using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Cobra.Identity.IdentityModel
{
    #region data member
    public class ApplicationUserLogin : IdentityUserLogin<int> { }
    public class ApplicationUserRole : IdentityUserRole<int> { }
    public class ApplicationUserClaim : IdentityUserClaim<int> { }
    public class ApplicationRole : IdentityRole<int, ApplicationUserRole> { }
    #endregion

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationDbContext() : base("CobraIdentityConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("Login");
            modelBuilder.Entity<ApplicationRole>().ToTable("Role");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("LoginRole");
            modelBuilder.Entity<ApplicationUserLogin>().ToTable("LoginSocialMedia");
            modelBuilder.Entity<ApplicationUserClaim>().ToTable("LoginClaim");

            //modelBuilder.Entity<ApplicationUser>()
            //    .Property(r => r.Id)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //modelBuilder.Entity<ApplicationRole>()
            //    .Property(r => r.Id)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //modelBuilder.Entity<ApplicationUserClaim>()
            //    .Property(r => r.Id)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //map columns 
            modelBuilder.Entity<ApplicationRole>().Property(r => r.Name).HasColumnName("RoleName");
            modelBuilder.Entity<ApplicationUserLogin>().Property(r => r.UserId).HasColumnName("LoginId");
            modelBuilder.Entity<ApplicationUserClaim>().Property(x => x.UserId).HasColumnName("LoginId");
            modelBuilder.Entity<ApplicationUserRole>().Property(x => x.UserId).HasColumnName("LoginId");
        }
    }

    public class ApplicationUser : IdentityUser<int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public bool IsActive { get; set; }
        public string EmailConfirmationToken { get; set; }
        public DateTime? EmailConfirmationTokenExpiryDate { get; set; }
        public string ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordTokenExpiryDate { get; set; }
    }
}

