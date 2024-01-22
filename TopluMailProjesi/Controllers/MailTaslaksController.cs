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
    public class MailTaslaksController : Controller
    {
        private readonly TMGContext _context;

        public MailTaslaksController(TMGContext context)
        {
            _context = context;
        }

        // GET: MailTaslaks
        public async Task<IActionResult> Index()
        {
            string? KullaniciGuid = HttpContext.Session.GetString("id");
            Guid guid = new Guid(KullaniciGuid);

            var tMGContext = _context.MailTaslaks.Include(m => m.Kullanici).Where(m=> m.KullaniciId == guid);
            return View(await tMGContext.ToListAsync());
        }

        // GET: MailTaslaks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.MailTaslaks == null)
            {
                return NotFound();
            }

            var mailTaslak = await _context.MailTaslaks
                .Include(m => m.Kullanici)
                .FirstOrDefaultAsync(m => m.MailTaslakId == id);
            if (mailTaslak == null)
            {
                return NotFound();
            }

            return View(mailTaslak);
        }

        // GET: MailTaslaks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MailTaslaks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MailTaslakId,Baslik,Aciklama,GonderimTarihi,SorguNo,KullaniciId")] MailTaslak mailTaslak)
        {
            string? KullaniciGuid = HttpContext.Session.GetString("id");
            Guid guid2 = new Guid(KullaniciGuid);
            mailTaslak.KullaniciId = guid2;
            if (ModelState.IsValid)
            {
                mailTaslak.MailTaslakId = Guid.NewGuid();
                _context.Add(mailTaslak);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mailTaslak);
        }

        // GET: MailTaslaks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.MailTaslaks == null)
            {
                return NotFound();
            }

            var mailTaslak = await _context.MailTaslaks.FindAsync(id);
            if (mailTaslak == null)
            {
                return NotFound();
            }
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicis, "KullaniciId", "KullaniciAdi", mailTaslak.KullaniciId);
            return View(mailTaslak);
        }

        // POST: MailTaslaks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MailTaslakId,Baslik,Aciklama,GonderimTarihi,SorguNo,KullaniciId")] MailTaslak mailTaslak)
        {
            string? KullaniciGuid = HttpContext.Session.GetString("id");
            Guid guid2 = new Guid(KullaniciGuid);
            mailTaslak.KullaniciId = guid2;
            if (id != mailTaslak.MailTaslakId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mailTaslak);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MailTaslakExists(mailTaslak.MailTaslakId))
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
            return View(mailTaslak);
        }

        // GET: MailTaslaks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.MailTaslaks == null)
            {
                return NotFound();
            }

            var mailTaslak = await _context.MailTaslaks
                .Include(m => m.Kullanici)
                .FirstOrDefaultAsync(m => m.MailTaslakId == id);
            if (mailTaslak == null)
            {
                return NotFound();
            }

            return View(mailTaslak);
        }

        // POST: MailTaslaks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var mdList = _context.MailDokumen.Where(s => s.MailTaslakId == id);

            foreach (var item in mdList)
            {
                _context.MailDokumen.Remove(item);

            }
            await _context.SaveChangesAsync();

            if (_context.MailTaslaks == null)
            {
                return Problem("Entity set 'TMGContext.MailTaslaks'  is null.");
            }
            var mailTaslak = await _context.MailTaslaks.FindAsync(id);
            if (mailTaslak != null)
            {
                _context.MailTaslaks.Remove(mailTaslak);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MailTaslakExists(Guid id)
        {
          return (_context.MailTaslaks?.Any(e => e.MailTaslakId == id)).GetValueOrDefault();
        }
    }
}
