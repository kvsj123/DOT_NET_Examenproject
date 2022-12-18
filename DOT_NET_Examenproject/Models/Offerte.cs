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

       

        [ForeignKey("Klant")]
        public int KlantId { get; set; }

        public Klant? Klant { get; set; }


    }

  
}
