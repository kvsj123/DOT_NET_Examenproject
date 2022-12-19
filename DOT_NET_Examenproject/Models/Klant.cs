using System.ComponentModel.DataAnnotations;

namespace DOT_NET_Examenproject.Models
{
    public class Klant
    {
        public int KlantId { get; set; }

        [Display(Name = "Klant Bedrijf Naam")]
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

    }
}
