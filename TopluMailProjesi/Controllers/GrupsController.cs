using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TopluMailProjesi.Filter;
using TopluMailProjesi.Models;

namespace TopluMailProjesi.Controllers
{
    [UserFilter]
    public class GrupsController : Controller
    {
        private readonly TMGContext _context;

        public GrupsController(TMGContext context)
        {
            _context = context;
        }

        // GET: Grups
        public async Task<IActionResult> Index()
        {
            string? KullaniciGuid = HttpContext.Session.GetString("id");
            Guid guid = new Guid(KullaniciGuid);

            var tMGContext = _context.Grups.Include(g => g.Kullanici).Where(g => g.KullaniciId == guid);

            
            return View(await tMGContext.ToListAsync());
        }

        // GET: Grups/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Grups == null)
            {
                return NotFound();
            }

            var grup = await _context.Grups
                .Include(g => g.Kullanici)
                .FirstOrDefaultAsync(m => m.GrupId == id);
            if (grup == null)
            {
                return NotFound();
            }

            return View(grup);
        }

        // GET: Grups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Grups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GrupId,GrupAdi,KullaniciId")] Grup grup)
        {
            string? KullaniciGuid = HttpContext.Session.GetString("id");
            Guid guid2 = new Guid(KullaniciGuid);
            grup.KullaniciId = guid2;

            if (ModelState.IsValid)
            {
                grup.GrupId = Guid.NewGuid();
                _context.Add(grup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grup);
        }

        // GET: Grups/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Grups == null)
            {
                return NotFound();
            }

            var grup = await _context.Grups.FindAsync(id);
            if (grup == null)
            {
                return NotFound();
            }
            return View(grup);
        }

        // POST: Grups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("GrupId,GrupAdi,KullaniciId")] Grup grup)
        {
            string? KullaniciGuid = HttpContext.Session.GetString("id");
            Guid guid2 = new Guid(KullaniciGuid);
            grup.KullaniciId = guid2;
            if (id != grup.GrupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrupExists(grup.GrupId))
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
            return View(grup);
        }

        // GET: Grups/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Grups == null)
            {
                return NotFound();
            }

            var grup = await _context.Grups
                .Include(g => g.Kullanici)
                .FirstOrDefaultAsync(m => m.GrupId == id);
            if (grup == null)
            {
                return NotFound();
            }

            return View(grup);
        }

        // POST: Grups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var kisiGrup = _context.KisiGrups.Where(s => s.GrupId == id);

            foreach (var item in kisiGrup)
            {
                _context.KisiGrups.Remove(item);

            }
            await _context.SaveChangesAsync();


            if (_context.Grups == null)
            {
                return Problem("Entity set 'TMGContext.Grups'  is null.");
            }
            var grup = await _context.Grups.FindAsync(id);
            if (grup != null)
            {
                _context.Grups.Remove(grup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrupExists(Guid id)
        {
          return (_context.Grups?.Any(e => e.GrupId == id)).GetValueOrDefault();
        }
    }
}
