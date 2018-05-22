using System.Data.Entity;
using VirtualTour.Models;
using Microsoft.AspNet.Identity.EntityFramework;

public class ApplicationContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationContext() : base("IdentityDb") { }

    public DbSet<Image> Images { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public static ApplicationContext Create()
    {
        return new ApplicationContext();
    }
}