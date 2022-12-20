using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DOT_NET_Examenproject.Data;
using DOT_NET_Examenproject.Models;

namespace DOT_NET_Examenproject.Controllers
{
    public class OffertesController : Controller
    {
        private readonly DOT_NET_ExamenprojectContext _context;

        public OffertesController(DOT_NET_ExamenprojectContext context)
        {
            _context = context;
        }

        // GET: Offertes
        public async Task<IActionResult> Index(string OpzoekVeld)
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

        // GET: Offertes/Details/5
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
        public IActionResult Create()
        {
            ViewData["BedrijfId"] = new SelectList(_context.Bedrijf, "BedrijfId", "Name");
            ViewData["KlantId"] = new SelectList(_context.Klant, "KlantId", "Name");

            
            return View();
        }

        // POST: Offertes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OfferteId,TitelOfferte,TotaalBedrag,IsDeleted,KlantId,BedrijfId")] Offerte offerte)
        {
            if (ModelState.IsValid)
            {
                _context.Add(offerte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BedrijfId"] = new SelectList(_context.Bedrijf, "BedrijfId", "Name", offerte.BedrijfId);
            ViewData["KlantId"] = new SelectList(_context.Klant, "KlantId", "Name", offerte.KlantId);
            return View(offerte);
        }

        // GET: Offertes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Offerte == null)
            {
                return NotFound();
            }

            var offerte = await _context.Offerte.FindAsync(id);
            if (offerte == null)
            {
                return NotFound();
            }
            ViewData["BedrijfId"] = new SelectList(_context.Bedrijf, "BedrijfId", "Name", offerte.BedrijfId);
            ViewData["KlantId"] = new SelectList(_context.Klant, "KlantId", "Name", offerte.KlantId);
            return View(offerte);
        }

        // POST: Offertes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OfferteId,TitelOfferte,TotaalBedrag,IsDeleted,KlantId,BedrijfId")] Offerte offerte)
        {
            if (id != offerte.OfferteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            ViewData["BedrijfId"] = new SelectList(_context.Bedrijf, "BedrijfId", "Name", offerte.BedrijfId);
            ViewData["KlantId"] = new SelectList(_context.Klant, "KlantId", "Name", offerte.KlantId);
            return View(offerte);
        }

        // GET: Offertes/Delete/5
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
