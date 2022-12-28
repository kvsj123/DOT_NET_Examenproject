using DOT_NET_Examenproject.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DOT_NET_Examenproject.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<DOT_NET_Examenproject.Models.Bedrijf> Bedrijf { get; set; } = default!;

    public DbSet<DOT_NET_Examenproject.Models.Klant> Klant { get; set; }

    public DbSet<DOT_NET_Examenproject.Models.Offerte> Offerte { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<DOT_NET_Examenproject.Areas.Identity.Data.ApplicationUser> ApplicationUser { get; set; }
}
