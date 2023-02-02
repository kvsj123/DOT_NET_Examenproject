using DOT_NET_Examenproject.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace DOT_NET_Examenproject.Data
{
    public class SeedDataContext
    {
        public static void Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
            using (var context = new AppDbContext(serviceProvider.GetRequiredService
                                                              <DbContextOptions<AppDbContext>>()))
            {
                ApplicationUser user = null;

                context.Database.Migrate();
                context.Database.EnsureCreated();    // Zorg dat de databank bestaat

                if (!context.Users.Any())
                {
                    
                    user = new ApplicationUser
                    {
                        FirstName = "Admin",
                        LastName = "Administrator",
                        UserName = "AdminSys",
                        Email = "Aministrator@app.com",
                        EmailConfirmed = true
                    };
                    userManager.CreateAsync(user, "Abc!12345");
              
                    context.SaveChanges();
                }

                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(
                        new IdentityRole { Id = "User", Name = "User", NormalizedName = "USER" },
                        new IdentityRole { Id = "SystemAdministrator", Name = "SystemAdministrator", NormalizedName = "SystemAdministrator" });
                    context.SaveChanges();
                }

                if (!context.Bedrijf.Any())
                {
                    context.Bedrijf.AddRange(

                        new Models.Bedrijf { Name = "Jan", NrTva = 58976416, Adres = "fabriekstraat 15, 1080 Molenbeek", Email = "jan@gmail.com", NrTel = 124587 },
                        new Models.Bedrijf { Name = "Pol", NrTva = 78954634, Adres = "azarastraat 70, 1070 Anderlecht", Email = "pol@gmail.com", NrTel = 548976 },
                        new Models.Bedrijf { Name = "Jaak", NrTva = 74689856, Adres = "George Withoekstraat 31, 1600 Sint-Pieters-Leeuw", Email = "jaak@gmail.com", NrTel = 4897989 },
                        new Models.Bedrijf { Name = "Koen", NrTva = 48978997, Adres = "Veldstraat 52, 9000 Gent", Email = "koen@gmail.com", NrTel = 1564897 },
                        new Models.Bedrijf { Name = "Gert", NrTva = 36598798, Adres = "Spijkerstraat 44, 1050 Elsene", Email = "gert@gmail.com", NrTel = 489798 },
                        new Models.Bedrijf { Name = "Tim", NrTva = 44568965, Adres = "Klein-Bijgaardestraat 6, 1020 Laeken", Email = "tim@gmail.com", NrTel = 6878976 }


                    );
                    context.SaveChanges();
                }
                if (!context.Klant.Any())
                {
                    context.Klant.AddRange(

                        new Models.Klant { Name = "Pieter", NrTva = 1568789, Adres = "Bergensesteenweg 256, 1600 Sint-Pieters-Leeuw", Email = "pieter@gmail.com", NrTel = 124588 },
                        new Models.Klant { Name = "Thomas", NrTva = 5458894, Adres = "Vondelstraat 68, 1040 Etterbeek", Email = "thomas@gmail.com", NrTel = 489795 },
                        new Models.Klant { Name = "Philippe", NrTva = 4897986, Adres = "Bekerstraat 12, 1500 Halle", Email = "philippe@gmail.com", NrTel = 7896489 },
                        new Models.Klant { Name = "Karel", NrTva = 7894689, Adres = "Waversesteenweg 79, 1980 Zemst", Email = "karel@gmail.com", NrTel = 4779563 },
                        new Models.Klant { Name = "Jonas", NrTva = 4879568, Adres = "Landstraat 23, 2000 Antwerpen", Email = "jonas@gmail.com", NrTel = 4878924 },
                        new Models.Klant { Name = "Mathias", NrTva = 1234445, Adres = "Brusselbaan 336, 8000 Brugge", Email = "mathias@gmail.com", NrTel = 4893484 }

                    );
                    context.SaveChanges();
                }

                if (!context.Offerte.Any())
                {
                    context.Offerte.AddRange(

                        new Models.Offerte { TitelOfferte = "Werf Brugge Delhaize", TotaalBedrag = 1000, KlantId = 1, BedrijfId = 1 },
                        new Models.Offerte { TitelOfferte = "Delhaize Tubize", TotaalBedrag = 48796, KlantId = 3, BedrijfId = 4 }
                    );
                    context.SaveChanges();

                }

                if (user != null)
                {
                    context.UserRoles.AddRange(
                        new IdentityUserRole<string> { UserId = user.Id, RoleId = "SystemAdministrator" });
                    context.SaveChanges();
                }


            }
        }
    }
}