using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DOT_NET_Examenproject.Models;

namespace DOT_NET_Examenproject.Data
{
    public class DOT_NET_ExamenprojectContext : DbContext
    {
        public DOT_NET_ExamenprojectContext (DbContextOptions<DOT_NET_ExamenprojectContext> options)
            : base(options)
        {
        }

        public DbSet<DOT_NET_Examenproject.Models.Bedrijf> Bedrijf { get; set; } = default!;

        public DbSet<DOT_NET_Examenproject.Models.Klant> Klant { get; set; }

        public DbSet<DOT_NET_Examenproject.Models.Offerte> Offerte { get; set; }

        

        

        

        
    }

}
