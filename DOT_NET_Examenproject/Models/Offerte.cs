using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DOT_NET_Examenproject.Models
{
    public class Offerte
    {
        public int OfferteId { get; set; }

        [Display(Name = "Titel Offerte")]
        public string TitelOfferte { get; set; }
        [Display(Name = "Totaal Bedrag")]
        public float TotaalBedrag { get; set; }

        public bool IsDeleted { get; set; } = false;

        [Display(Name = "Klant")]
        public int KlantId { get; set; }
        [ForeignKey("KlantId")]
        public Klant Klant { get; set; }

        [Display(Name = "Bedrijf")]
        public int BedrijfId { get; set; }

        [ForeignKey("BedrijfId")]
        public Bedrijf Bedrijf { get; set; }


    }

  
}
