using DOT_NET_Examenproject.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace DOT_NET_Examenproject.Models
{
    public class Bedrijf
    {
        public int BedrijfId { get; set; }

        [Display(Name = "Bedrijf Naam")]
        public string Name { get; set; }
        [Display(Name = "TVA-Nummer")]
        public int NrTva { get; set; }
        [Display(Name = "Adres")]
        public string Adres { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "TelefoonNummer")]
        public int NrTel { get; set; }

        public bool IsDeleted { get; set; } = false;

        
        public string? user_id { get; set; }




    }
}
