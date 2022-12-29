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

namespace DOT_NET_Examenproject.Controllers
{
    public class KlantsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;

        public KlantsController(AppDbContext context, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Klants
        [Authorize]
        public async Task<IActionResult> Index(string OpzoekVeld)
        {
            if (HttpContext.User.IsInRole("SystemAdministrator"))
            {
                var klanten = from g in _context.Klant
                                where g.IsDeleted == false
                                orderby g.Name
                                select g;


                if (!string.IsNullOrEmpty(OpzoekVeld))
                    klanten = from g in klanten
                                where g.Name.Contains(OpzoekVeld) && g.IsDeleted == false
                                orderby g.Name
                                select g;




                return View(await klanten.ToListAsync());
            }
            else if (HttpContext.User.IsInRole("User"))
            {
                string userIdd = _userManager.GetUserId(HttpContext.User);

                var FilKlanten = from g in _context.Klant
                                   where g.user_id == userIdd
                                   orderby g.Name
                                   select g;


                var klanten = from g in FilKlanten
                                where g.IsDeleted == false
                                orderby g.Name
                                select g;


                if (!string.IsNullOrEmpty(OpzoekVeld))
                    klanten = from g in klanten
                                where g.Name.Contains(OpzoekVeld) && g.IsDeleted == false
                                orderby g.Name
                                select g;
                return View(await klanten.ToListAsync());
            }
            else
            {
                return View(null);
            }
        }

        // GET: Klants/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Klant == null)
            {
                return NotFound();
            }

            var klant = await _context.Klant
                .FirstOrDefaultAsync(m => m.KlantId == id);
            if (klant == null)
            {
                return NotFound();
            }

            return View(klant);
        }

        // GET: Klants/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Klants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KlantId,Name,NrTva,Adres,Email,NrTel")] Klant klant)
        {
            string userIdd = _userManager.GetUserId(HttpContext.User);

            if (ModelState.IsValid)
            {
                _context.Add(klant);
                klant.user_id = userIdd;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(klant);
        }

        // GET: Klants/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Klant == null)
            {
                return NotFound();
            }

            var klant = await _context.Klant.FindAsync(id);
            if (klant == null)
            {
                return NotFound();
            }
            return View(klant);
        }

        // POST: Klants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KlantId,Name,NrTva,Adres,Email,NrTel")] Klant klant)
        {
            string userIdd = _userManager.GetUserId(HttpContext.User);

            if (id != klant.KlantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    klant.user_id = userIdd;
                    _context.Update(klant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KlantExists(klant.KlantId))
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
            return View(klant);
        }

        // GET: Klants/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Klant == null)
            {
                return NotFound();
            }

            var klant = await _context.Klant
                .FirstOrDefaultAsync(m => m.KlantId == id);
            if (klant == null)
            {
                return NotFound();
            }

            return View(klant);
        }

        // POST: Klants/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Klant == null)
            {
                return Problem("Entity set 'DOT_NET_ExamenprojectContext.Klant'  is null.");
            }
            var klant = await _context.Klant.FindAsync(id);
            if (klant != null)
            {
                klant.IsDeleted = true;
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KlantExists(int id)
        {
          return _context.Klant.Any(e => e.KlantId == id);
        }
    }
}
