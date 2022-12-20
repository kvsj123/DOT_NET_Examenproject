﻿using Microsoft.AspNetCore.Identity;
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
                context.Database.EnsureCreated();    // Zorg dat de databank bestaat

                if (!context.Bedrijf.Any())
                {
                    context.Bedrijf.AddRange(
                        
                        new Models.Bedrijf { Name="Jan", NrTva = 123, Adres = "fdsfsd", Email = "fdsfsfds", NrTel = 12458}
                        

                    );
                    context.SaveChanges();
                }
                if (!context.Klant.Any())
                {
                    context.Klant.AddRange(

                        new Models.Klant { Name = "Pieter", NrTva = 123, Adres = "fdsfsd", Email = "fdsfsfds", NrTel = 12458 }
                        
                    );
                    context.SaveChanges();
                }
                if (!context.Offerte.Any())
                {
                    context.Offerte.AddRange(

                        new Models.Offerte { TitelOfferte = "Werf Brugge Delhaize", TotaalBedrag = 1000, KlantId = 1, BedrijfId = 1 }
                    );
                    context.SaveChanges();
                }
               




            }
        }
    }
}
