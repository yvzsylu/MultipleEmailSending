using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using TopluMailProjesi.Filter;
using TopluMailProjesi.Models;

namespace TopluMailProjesi.Controllers
{
    [UserFilter]
    public class KisisController : Controller
    {
        private readonly TMGContext _context;

        public KisisController(TMGContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Import (IFormFile file)
        {

            string? KullaniciGuid = HttpContext.Session.GetString("id");
            Guid guid = new Guid(KullaniciGuid);
            
            var list = new List<Kisi>();
            using (var stream = new MemoryStream())
            {
                if (file == null)
                {
                    TempData["mesaj"] = "Excel Dosyası Ekleyiniz";
                    return Redirect("/Kisis/Create");
                }
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowcount = worksheet.Dimension.Rows;
                    for (int row = 2; row < rowcount; row++)
                    {
                        list.Add(new Kisi
                        {
                            KisiId = Guid.NewGuid(),
                            Adi = worksheet.Cells[row,1].Value.ToString().Trim(),
                            Soyadi = worksheet.Cells[row, 2].Value.ToString().Trim(),
                            Email = worksheet.Cells[row, 3].Value.ToString().Trim(),
                            Cep = worksheet.Cells[row, 4].Value.ToString().Trim(),
                            KullaniciId = guid
                    });
                    }
                }
                try
                {
                    foreach (var item in list)
                    {
                        _context.Add(item);

                    }
                    await _context.SaveChangesAsync();
                    TempData["mesaj"] = "Kişiler eklenmiştir.";
                }
                catch (Exception ex)
                {
                    TempData["mesaj"] = "Kişiler eklenemedi" + ex.Message;
                }
            }

            return Redirect("/Kisis/Create");
        }


        // GET: Kisis
        public async Task<IActionResult> Index()
        {
            string? KullaniciGuid = HttpContext.Session.GetString("id");
            Guid guid = new Guid(KullaniciGuid);

            var tMGContext = _context.Kisis.Include(k => k.Kullanici).Where(g => g.KullaniciId == guid);
            return View(await tMGContext.ToListAsync());
        }

        // GET: Kisis/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Kisis == null)
            {
                return NotFound();
            }

            var kisi = await _context.Kisis
                .Include(k => k.Kullanici)
                .FirstOrDefaultAsync(m => m.KisiId == id);
            if (kisi == null)
            {
                return NotFound();
            }

            return View(kisi);
        }

        // GET: Kisis/Create
        public IActionResult Create()
        {
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicis, "KullaniciId", "KullaniciAdi");
            return View();
        }

        // POST: Kisis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KisiId,Adi,Soyadi,Email,Cep,KullaniciId")] Kisi kisi)
        {
            string? KullaniciGuid = HttpContext.Session.GetString("id");
            Guid guid = new Guid(KullaniciGuid);
            kisi.KullaniciId = guid;

            if (ModelState.IsValid)
            {
                kisi.KisiId = Guid.NewGuid();
                _context.Add(kisi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kisi);
        }

        // GET: Kisis/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Kisis == null)
            {
                return NotFound();
            }

            var kisi = await _context.Kisis.FindAsync(id);
            if (kisi == null)
            {
                return NotFound();
            }
            return View(kisi);
        }

        // POST: Kisis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("KisiId,Adi,Soyadi,Email,Cep,KullaniciId")] Kisi kisi)
        {
            string? KullaniciGuid = HttpContext.Session.GetString("id");
            Guid guid2 = new Guid(KullaniciGuid);
            kisi.KullaniciId = guid2;

            if (id != kisi.KisiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kisi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KisiExists(kisi.KisiId))
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
            return View(kisi);
        }

        // GET: Kisis/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Kisis == null)
            {
                return NotFound();
            }

            var kisi = await _context.Kisis
                .Include(k => k.Kullanici)
                .FirstOrDefaultAsync(m => m.KisiId == id);
            if (kisi == null)
            {
                return NotFound();
            }

            return View(kisi);
        }

        // POST: Kisis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var kisiGrup = _context.KisiGrups.Where(s => s.KisiId == id);

            foreach (var item in kisiGrup)
            {
                _context.KisiGrups.Remove(item);

            }
            await _context.SaveChangesAsync();


            if (_context.Kisis == null)
            {
                return Problem("Entity set 'TMGContext.Kisis'  is null.");
            }
            var kisi = await _context.Kisis.FindAsync(id);
            if (kisi != null)
            {
                _context.Kisis.Remove(kisi);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KisiExists(Guid id)
        {
            

            return (_context.Kisis?.Any(e => e.KisiId == id)).GetValueOrDefault();
        }
    }
}
