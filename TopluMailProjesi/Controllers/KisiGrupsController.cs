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
    public class KisiGrupsController : Controller
    {
        private readonly TMGContext _context;

        public KisiGrupsController(TMGContext context)
        {
            _context = context;
        }

        // GET: KisiGrups
        public async Task<IActionResult> Index()
        {
            string? KullaniciGuid = HttpContext.Session.GetString("id");
            Guid guid = new Guid(KullaniciGuid);

            var tMGContext = _context.KisiGrups.Include(k => k.Grup).Include(k => k.Kisi).Include(k => k.Kullanici).Where(k => k.KullaniciId == guid);

            
            return View(await tMGContext.ToListAsync());
        }

        // GET: KisiGrups/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.KisiGrups == null)
            {
                return NotFound();
            }

            var kisiGrup = await _context.KisiGrups
                .Include(k => k.Grup)
                .Include(k => k.Kisi)
                .Include(k => k.Kullanici)
                .FirstOrDefaultAsync(m => m.KisiGrupId == id);
            if (kisiGrup == null)
            {
                return NotFound();
            }

            return View(kisiGrup);
        }

        // GET: KisiGrups/Create
        public IActionResult Create()
        {
            string? KullaniciGuid = HttpContext.Session.GetString("id");
            Guid guid = new Guid(KullaniciGuid);
            

            ViewData["GrupId"] = new SelectList(_context.Grups.Include(g => g.Kullanici).Where(g => g.KullaniciId == guid), "GrupId", "GrupAdi");
            ViewData["KisiId"] = new SelectList(_context.Kisis.Include(k => k.Kullanici).Where(g => g.KullaniciId == guid), "KisiId", "AdSoyad");
            return View();
        }

        // POST: KisiGrups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KisiGrupId,KisiId,GrupId,KullaniciId")] KisiGrup kisiGrup)
        {
            string? KullaniciGuid = HttpContext.Session.GetString("id");
            Guid guid = new Guid(KullaniciGuid);
            kisiGrup.KullaniciId = guid;
            if (ModelState.IsValid)
            {
                
                kisiGrup.KisiGrupId = Guid.NewGuid();
                _context.Add(kisiGrup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GrupId"] = new SelectList(_context.Grups.Include(g => g.Kullanici).Where(g => g.KullaniciId == guid), "GrupId", "GrupAdi", kisiGrup.GrupId);
            ViewData["KisiId"] = new SelectList(_context.Kisis.Include(k => k.Kullanici).Where(g => g.KullaniciId == guid), "KisiId", "AdSoyad", kisiGrup.KisiId);
            return View(kisiGrup);
        }

        // GET: KisiGrups/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            string? KullaniciGuid = HttpContext.Session.GetString("id");
            Guid guid = new Guid(KullaniciGuid);
            if (id == null || _context.KisiGrups == null)
            {
                return NotFound();
            }

            var kisiGrup = await _context.KisiGrups.FindAsync(id);
            if (kisiGrup == null)
            {
                return NotFound();
            }
            ViewData["GrupId"] = new SelectList(_context.Grups.Where(g => g.KullaniciId == guid), "GrupId", "GrupAdi", kisiGrup.GrupId);
            ViewData["KisiId"] = new SelectList(_context.Kisis.Where(g => g.KullaniciId == guid), "KisiId", "AdSoyad", kisiGrup.KisiId);
            return View(kisiGrup);
        }

        // POST: KisiGrups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("KisiGrupId,KisiId,GrupId,KullaniciId")] KisiGrup kisiGrup)
        {

            string? KullaniciGuid = HttpContext.Session.GetString("id");
            Guid guid = new Guid(KullaniciGuid);
            

            if (id != kisiGrup.KisiGrupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kisiGrup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KisiGrupExists(kisiGrup.KisiGrupId))
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
            ViewData["GrupId"] = new SelectList(_context.Grups.Where(g => g.KullaniciId == guid), "GrupId", "GrupAdi", kisiGrup.GrupId);
            ViewData["KisiId"] = new SelectList(_context.Kisis.Where(g => g.KullaniciId == guid), "KisiId", "AdSoyad", kisiGrup.KisiId);
            return View(kisiGrup);
        }

        // GET: KisiGrups/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.KisiGrups == null)
            {
                return NotFound();
            }

            var kisiGrup = await _context.KisiGrups
                .Include(k => k.Grup)
                .Include(k => k.Kisi)
                .Include(k => k.Kullanici)
                .FirstOrDefaultAsync(m => m.KisiGrupId == id);
            if (kisiGrup == null)
            {
                return NotFound();
            }

            return View(kisiGrup);
        }

        // POST: KisiGrups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.KisiGrups == null)
            {
                return Problem("Entity set 'TMGContext.KisiGrups'  is null.");
            }
            var kisiGrup = await _context.KisiGrups.FindAsync(id);
            if (kisiGrup != null)
            {
                _context.KisiGrups.Remove(kisiGrup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KisiGrupExists(Guid id)
        {
          return (_context.KisiGrups?.Any(e => e.KisiGrupId == id)).GetValueOrDefault();
        }
    }
}
