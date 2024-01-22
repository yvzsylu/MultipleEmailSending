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
    public class MailDokumenController : Controller
    {
        private readonly TMGContext _context;

        public MailDokumenController(TMGContext context)
        {
            _context = context;
        }

        // GET: MailDokumen
        public async Task<IActionResult> Index()
        {
            string? KullaniciGuid = HttpContext.Session.GetString("id");
            Guid guid = new Guid(KullaniciGuid);

            List<MailDokuman> list = _context.MailDokumen.Where(g => g.MailTaslak.KullaniciId == guid).ToList();
            foreach (var item in list)
            {
                _context.Entry(item).Reference(g => g.MailTaslak).Load();
                _context.Entry(item).Reference(g => g.Dokuman).Load();

            }
             //_context.MailDokumen.Include(m => m.Dokuman).Include(m => m.MailTaslak);
            return View(list);
        }

        // GET: MailDokumen/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.MailDokumen == null)
            {
                return NotFound();
            }

            var mailDokuman = await _context.MailDokumen
                .Include(m => m.Dokuman)
                .Include(m => m.MailTaslak)
                .FirstOrDefaultAsync(m => m.MailDokumanId == id);
            if (mailDokuman == null)
            {
                return NotFound();
            }

            return View(mailDokuman);
        }

        // GET: MailDokumen/Create
        public IActionResult Create()
        {
            string? KullaniciGuid = HttpContext.Session.GetString("id");
            Guid guid = new Guid(KullaniciGuid);

            ViewData["DokumanId"] = new SelectList(_context.Dokumanlars.Include(m => m.Kullanici).Where(m => m.KullaniciId == guid), "DokumanId", "DokumanYolu");
            ViewData["MailTaslakId"] = new SelectList(_context.MailTaslaks.Include(m => m.Kullanici).Where(m => m.KullaniciId == guid), "MailTaslakId", "Baslik");
            return View();
        }

        // POST: MailDokumen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MailDokumanId,MailTaslakId,DokumanId")] MailDokuman mailDokuman)
        {
            string? KullaniciGuid = HttpContext.Session.GetString("id");
            Guid guid = new Guid(KullaniciGuid);

            if (ModelState.IsValid)
            {
                mailDokuman.MailDokumanId = Guid.NewGuid();
                _context.Add(mailDokuman);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            ViewData["DokumanId"] = new SelectList(_context.Dokumanlars.Include(m => m.Kullanici).Where(m => m.KullaniciId == guid), "DokumanId", "DokumanYolu", mailDokuman.DokumanId);
            ViewData["MailTaslakId"] = new SelectList(_context.MailTaslaks.Include(m => m.Kullanici).Where(m => m.KullaniciId == guid), "MailTaslakId", "Baslik", mailDokuman.MailTaslakId);
            return View(mailDokuman);
        }

        // GET: MailDokumen/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            string? KullaniciGuid = HttpContext.Session.GetString("id");
            Guid guid = new Guid(KullaniciGuid);

            if (id == null || _context.MailDokumen == null)
            {
                return NotFound();
            }

            var mailDokuman = await _context.MailDokumen.FindAsync(id);
            if (mailDokuman == null)
            {
                return NotFound();
            }
            ViewData["DokumanId"] = new SelectList(_context.Dokumanlars, "DokumanId", "DokumanYolu", mailDokuman.DokumanId);
            ViewData["MailTaslakId"] = new SelectList(_context.MailTaslaks.Include(m => m.Kullanici).Where(m => m.KullaniciId == guid), "MailTaslakId", "Baslik", mailDokuman.MailTaslakId);
            return View(mailDokuman);
        }

        // POST: MailDokumen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MailDokumanId,MailTaslakId,DokumanId")] MailDokuman mailDokuman)
        {
            string? KullaniciGuid = HttpContext.Session.GetString("id");
            Guid guid = new Guid(KullaniciGuid);

            if (id != mailDokuman.MailDokumanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mailDokuman);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MailDokumanExists(mailDokuman.MailDokumanId))
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
            ViewData["DokumanId"] = new SelectList(_context.Dokumanlars, "DokumanId", "DokumanYolu", mailDokuman.DokumanId);
            ViewData["MailTaslakId"] = new SelectList(_context.MailTaslaks.Include(m => m.Kullanici).Where(m => m.KullaniciId == guid), "MailTaslakId", "Baslik", mailDokuman.MailTaslakId);
            return View(mailDokuman);
        }

        // GET: MailDokumen/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.MailDokumen == null)
            {
                return NotFound();
            }

            var mailDokuman = await _context.MailDokumen
                .Include(m => m.Dokuman)
                .Include(m => m.MailTaslak)
                .FirstOrDefaultAsync(m => m.MailDokumanId == id);
            if (mailDokuman == null)
            {
                return NotFound();
            }

            return View(mailDokuman);
        }

        // POST: MailDokumen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.MailDokumen == null)
            {
                return Problem("Entity set 'TMGContext.MailDokumen'  is null.");
            }
            var mailDokuman = await _context.MailDokumen.FindAsync(id);
            if (mailDokuman != null)
            {
                _context.MailDokumen.Remove(mailDokuman);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MailDokumanExists(Guid id)
        {
          return (_context.MailDokumen?.Any(e => e.MailDokumanId == id)).GetValueOrDefault();
        }
    }
}
