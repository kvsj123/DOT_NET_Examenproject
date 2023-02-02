using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DOT_NET_Examenproject.Data;
using DOT_NET_Examenproject.Models;
using Microsoft.AspNetCore.Authorization;
using DOT_NET_Examenproject.Areas.Identity.Data;
using Microsoft.Exchange.WebServices.Data;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Bcpg.Sig;
using static Microsoft.Exchange.WebServices.Data.SearchFilter;
using static System.Net.Mime.MediaTypeNames;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using iTextSharp.text.pdf.draw;
using NuGet.Protocol;

namespace DOT_NET_Examenproject.Controllers
{
    public class OffertesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;

        public OffertesController(AppDbContext context, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Offertes
        [Authorize]
        public async Task<IActionResult> Index(string OpzoekVeld)
        {
            if (HttpContext.User.IsInRole("SystemAdministrator"))
            {
                var dOT_NET_ExamenprojectContext = _context.Offerte.Include(o => o.Bedrijf).Include(o => o.Klant);

                var offerten = from g in _context.Offerte.Include(o => o.Bedrijf).Include(o => o.Klant)
                               where g.IsDeleted == false
                               orderby g.TitelOfferte
                               select g;

                if (!string.IsNullOrEmpty(OpzoekVeld))
                    offerten = from g in offerten
                               where g.TitelOfferte.Contains(OpzoekVeld) && g.IsDeleted == false
                               orderby g.TitelOfferte
                               select g;
                return View(await offerten.ToListAsync());
            }
            else if (HttpContext.User.IsInRole("User"))
            {
                string userIdd = _userManager.GetUserId(HttpContext.User);

                var dOT_NET_ExamenprojectContext = _context.Offerte.Include(o => o.Bedrijf).Include(o => o.Klant);

                var FilOfferten = from g in dOT_NET_ExamenprojectContext
                                   where g.user_id == userIdd
                                   orderby g.TitelOfferte
                                   select g;

                

                var offerten = from g in FilOfferten
                               where g.IsDeleted == false
                               orderby g.TitelOfferte
                               select g;

                if (!string.IsNullOrEmpty(OpzoekVeld))
                    offerten = from g in offerten
                               where g.TitelOfferte.Contains(OpzoekVeld) && g.IsDeleted == false
                               orderby g.TitelOfferte
                               select g;
                return View(await offerten.ToListAsync());



            }
            else
            {
                return View(null);
            }

            
        }

        // GET: Offertes/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Offerte == null)
            {
                return NotFound();
            }

            var offerte = await _context.Offerte
                .Include(o => o.Bedrijf)
                .Include(o => o.Klant)
                .FirstOrDefaultAsync(m => m.OfferteId == id);
            if (offerte == null)
            {
                return NotFound();
            }

            return View(offerte);
        }

      
        





        // GET: Offertes/Create
                [Authorize]
        public IActionResult Create()
        {
            string userIdd = _userManager.GetUserId(HttpContext.User);

            var FilBedrijven = from g in _context.Bedrijf
                               where g.user_id == userIdd
                               orderby g.Name
                               select g;

            var FilKlanten = from g in _context.Klant
                              where g.user_id == userIdd
                              orderby g.Name
                              select g;

               if (HttpContext.User.IsInRole("User"))
            {
                ViewData["BedrijfId"] = new SelectList(FilBedrijven, "BedrijfId", "Name");
                ViewData["KlantId"] = new SelectList(FilKlanten, "KlantId", "Name");
            }
            else if (HttpContext.User.IsInRole("SystemAdministrator"))
            {
                ViewData["BedrijfId"] = new SelectList(_context.Bedrijf, "BedrijfId", "Name");
                ViewData["KlantId"] = new SelectList(_context.Klant, "KlantId", "Name");
            }
            
            return View();
        }

        // POST: Offertes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("OfferteId,TitelOfferte,TotaalBedrag,IsDeleted,KlantId,BedrijfId")] Offerte offerte)
        {
            string userIdd = _userManager.GetUserId(HttpContext.User);

            var FilBedrijven = from g in _context.Bedrijf
                               where g.user_id == userIdd
                               orderby g.Name
                               select g;

            var FilKlanten = from g in _context.Klant
                               where g.user_id == userIdd
                               orderby g.Name
                               select g;

       

            if (ModelState.IsValid)
            {

                var klant = await _context.Klant.FindAsync(offerte.KlantId);
                var bedrijf = await _context.Bedrijf.FindAsync(offerte.BedrijfId);

                string fileName = $"Offerte_{offerte.TitelOfferte}.pdf";
                string outFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
                Document document = new Document();

                FileStream stream = new FileStream(outFile, FileMode.Create);

                PdfWriter.GetInstance(document, stream);
                document.Open();

                // Add the client and provider information to the PDF
                var top = FontFactory.GetFont("Arial", 30, Font.BOLD, BaseColor.Black);
                var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD, BaseColor.Black);
                var bottom = FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.DarkGray);
                var info_style = FontFactory.GetFont("Helvetica", 14, Font.BOLD, BaseColor.Black);

                Paragraph p14 = new Paragraph("Offerte: " + offerte.TitelOfferte + "\n\n", top);
                p14.Alignment = Element.ALIGN_CENTER;
                document.Add(p14);

                // Add a horizontal rule to separate the title from the content
                var rule = new Chunk(new LineSeparator());


                BaseColor blue = new BaseColor(0, 75, 155);
                BaseColor gris = new BaseColor(240, 240, 240);
                BaseColor blanc = new BaseColor(0, 75, 155);

                Font title_style = new Font(iTextSharp.text.Font.HELVETICA, 15f, iTextSharp.text.Font.BOLD, BaseColor.Black);


                Paragraph p13 = new Paragraph("Klant: " + "\n", titleFont);
                p13.Alignment = Element.ALIGN_LEFT;
                document.Add(p13);
                Paragraph p1 = new Paragraph(klant.Name, info_style);
                p1.Alignment = Element.ALIGN_LEFT;
                document.Add(p1);
                Paragraph p2 = new Paragraph("BE " + klant.NrTva, info_style);
                p2.Alignment = Element.ALIGN_LEFT;
                document.Add(p2);
                Paragraph p3 = new Paragraph(klant.Adres, info_style);
                p3.Alignment = Element.ALIGN_LEFT;
                document.Add(p3);
                Paragraph p4 = new Paragraph(klant.Email, info_style);
                p4.Alignment = Element.ALIGN_LEFT;
                document.Add(p4);
                Paragraph p5 = new Paragraph(klant.NrTel + "\n\n", info_style);
                p5.Alignment = Element.ALIGN_LEFT;
                document.Add(p5);

                Paragraph p12 = new Paragraph("Bedrijf: " + "\n", titleFont);
                p12.Alignment = Element.ALIGN_RIGHT;
                document.Add(p12);
                Paragraph p6 = new Paragraph(bedrijf.Name, info_style);
                p6.Alignment = Element.ALIGN_RIGHT;
                document.Add(p6);
                Paragraph p7 = new Paragraph("BE " + bedrijf.NrTva, info_style);
                p7.Alignment = Element.ALIGN_RIGHT;
                document.Add(p7);
                Paragraph p8 = new Paragraph(bedrijf.Adres, info_style);
                p8.Alignment = Element.ALIGN_RIGHT;
                document.Add(p8);
                Paragraph p9 = new Paragraph(bedrijf.Email, info_style);
                p9.Alignment = Element.ALIGN_RIGHT;
                document.Add(p9);
                Paragraph p10 = new Paragraph(bedrijf.NrTel + "\n\n\n\n\n\n\n\n\n\n\n\n", info_style);
                p10.Alignment = Element.ALIGN_RIGHT;
                document.Add(p10);

                Paragraph p11 = new Paragraph("TOTAAL BEDRAG: " + offerte.TotaalBedrag + "€\n\n\n", titleFont);
                p11.Alignment = Element.ALIGN_CENTER;
                document.Add(p11);

                

                // Add a horizontal rule to separate the company information from the invoice details
                document.Add(rule);

                // Add the invoice details to the document
                Paragraph p15 = new Paragraph("Bedankt om zo snel mogelijk te betalen!", bottom);
                p15.Alignment = Element.ALIGN_CENTER;
                document.Add(p15);

                document.Close();
               
                byte[] fileBytes = new byte[stream.Length];
                stream.Dispose();
                stream.Close();
                
                
                Process.Start(@"cmd.exe", @"/c" + outFile);
                document.Close();
                offerte.user_id = userIdd;

                _context.Add(offerte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                return File(fileBytes, "application/pdf", fileName);
            }
           

            

            if (HttpContext.User.IsInRole("User"))
            {
                ViewData["BedrijfId"] = new SelectList(FilBedrijven, "BedrijfId", "Name", offerte.BedrijfId);
                ViewData["KlantId"] = new SelectList(FilKlanten, "KlantId", "Name", offerte.KlantId);
            }
            else if (HttpContext.User.IsInRole("SystemAdministrator"))
            {
                ViewData["BedrijfId"] = new SelectList(_context.Bedrijf, "BedrijfId", "Name", offerte.BedrijfId);
                ViewData["KlantId"] = new SelectList(_context.Klant, "KlantId", "Name", offerte.KlantId);
            }
            return View(offerte);
        }

        // GET: Offertes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            string userIdd = _userManager.GetUserId(HttpContext.User);

            var FilBedrijven = from g in _context.Bedrijf
                               where g.user_id == userIdd
                               orderby g.Name
                               select g;

            var FilKlanten = from g in _context.Klant
                             where g.user_id == userIdd
                             orderby g.Name
                             select g;

            if (id == null || _context.Offerte == null)
            {
                return NotFound();
            }

            var offerte = await _context.Offerte.FindAsync(id);
            if (offerte == null)
            {
                return NotFound();
            }
            if (HttpContext.User.IsInRole("User"))
            {
                ViewData["BedrijfId"] = new SelectList(FilBedrijven, "BedrijfId", "Name", offerte.BedrijfId);
                ViewData["KlantId"] = new SelectList(FilKlanten, "KlantId", "Name", offerte.KlantId);
            }
            else if (HttpContext.User.IsInRole("SystemAdministrator"))
            {
                ViewData["BedrijfId"] = new SelectList(_context.Bedrijf, "BedrijfId", "Name", offerte.BedrijfId);
                ViewData["KlantId"] = new SelectList(_context.Klant, "KlantId", "Name", offerte.KlantId);
            }
            return View(offerte);
        }

        // POST: Offertes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OfferteId,TitelOfferte,TotaalBedrag,IsDeleted,KlantId,BedrijfId")] Offerte offerte)
        {
            string userIdd = _userManager.GetUserId(HttpContext.User);

            var FilBedrijven = from g in _context.Bedrijf
                               where g.user_id == userIdd
                               orderby g.Name
                               select g;

            var FilKlanten = from g in _context.Klant
                             where g.user_id == userIdd
                             orderby g.Name
                             select g;

            if (id != offerte.OfferteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    offerte.user_id = userIdd;
                    _context.Update(offerte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfferteExists(offerte.OfferteId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            if (HttpContext.User.IsInRole("User"))
            {
                ViewData["BedrijfId"] = new SelectList(FilBedrijven, "BedrijfId", "Name", offerte.BedrijfId);
                ViewData["KlantId"] = new SelectList(FilKlanten, "KlantId", "Name", offerte.KlantId);
            }
            else if (HttpContext.User.IsInRole("SystemAdministrator"))
            {
                ViewData["BedrijfId"] = new SelectList(_context.Bedrijf, "BedrijfId", "Name", offerte.BedrijfId);
                ViewData["KlantId"] = new SelectList(_context.Klant, "KlantId", "Name", offerte.KlantId);
            }
            return View(offerte);
        }

        // GET: Offertes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Offerte == null)
            {
                return NotFound();
            }

            var offerte = await _context.Offerte
                .Include(o => o.Bedrijf)
                .Include(o => o.Klant)
                .FirstOrDefaultAsync(m => m.OfferteId == id);
            if (offerte == null)
            {
                return NotFound();
            }

            return View(offerte);
        }

        // POST: Offertes/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Offerte == null)
            {
                return Problem("Entity set 'DOT_NET_ExamenprojectContext.Offerte'  is null.");
            }
            var offerte = await _context.Offerte.FindAsync(id);
            if (offerte != null)
            {
               offerte.IsDeleted = true;
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfferteExists(int id)
        {
          return _context.Offerte.Any(e => e.OfferteId == id);
        }
    }
}
