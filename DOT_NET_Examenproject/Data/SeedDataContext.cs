using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace DOT_NET_Examenproject.Data
{
    public class SeedDataContext
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DOT_NET_ExamenprojectContext(serviceProvider.GetRequiredService
                                                              <DbContextOptions<DOT_NET_ExamenprojectContext>>()))
            {
                context.Database.Migrate();
                context.Database.EnsureCreated();    // Zorg dat de databank bestaat

                if (!context.Bedrijf.Any())
                {
                    context.Bedrijf.AddRange(
                        
                        new Models.Bedrijf { Name = "Jan", NrTva = 58976416, Adres = "fabriekstraat 15, 1080 Molenbeek", Email = "jan@gmail.com", NrTel = 124587},
                        new Models.Bedrijf { Name = "Pol", NrTva = 78954634, Adres = "azarastraat 70, 1070 Anderlecht", Email = "pol@gmail.com", NrTel = 548976 }


                    );
                    context.SaveChanges();
                }
                if (!context.Klant.Any())
                {
                    context.Klant.AddRange(

                        new Models.Klant { Name = "Pieter", NrTva = 1568789, Adres = "Bergensesteenweg 112, 1600 Sint-Pieters-Leeuw", Email = "pieter@gmail.com", NrTel = 12458 },
                        new Models.Klant { Name = "Thomas", NrTva = 5458894, Adres = "Vondelstraat 144, 1040 Etterbeek", Email = "thomas@gmail.com", NrTel = 12458 }

                    );
                    context.SaveChanges();
                }

                if (!context.Offerte.Any())
                {
                    context.Offerte.AddRange(

                        new Models.Offerte { TitelOfferte = "Werf Brugge Delhaize", TotaalBedrag = 1000, KlantId = 1, BedrijfId = 1 },
                        new Models.Offerte { TitelOfferte = "Werf Hasselt Carrefour", TotaalBedrag = 8560, KlantId = 2, BedrijfId = 2 }
                    );
                    context.SaveChanges();

                }
            }
        }
    }
}
