using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TopluMailProjesi.Models;

namespace TopluMailProjesi.Controllers
{
    public class SmtpAyarlarisController : Controller
    {
        private readonly TMGContext _context;

        public SmtpAyarlarisController(TMGContext context)
        {
            _context = context;
        }

        // GET: SmtpAyarlaris
        public async Task<IActionResult> Index()
        {

            string? KullaniciGuid = HttpContext.Session.GetString("id");
            Guid guid = new Guid(KullaniciGuid);


            Kullanici k = new Kullanici();
            var kullanici = _context.Kullanicis.FirstOrDefault(k => k.KullaniciId == guid);
            //if (id == null)
            //{
            //    return NotFound();
            //}

            SmtpAyarlari sa = new SmtpAyarlari();
            sa.KullaniciId = guid;
            
            var smtpAyarlari = _context.SmtpAyarlaris.FirstOrDefault(x => x.KullaniciId == guid);
            if (smtpAyarlari != null)
            {
                sa = smtpAyarlari;
            }

            return View(sa);


        }



        // GET: SmtpAyarlaris/Create
        //public IActionResult Create()
        //{
        //    ViewData["KullaniciId"] = new SelectList(_context.Kullanicis, "KullaniciId", "KullaniciId");
        //    return View();
        //}

        // POST: SmtpAyarlaris/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,SunucuAdi,Port,KullaniciAdi,Sifre,KullaniciId")] SmtpAyarlari smtpAyarlari)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        smtpAyarlari.Id = Guid.NewGuid();
        //        _context.Add(smtpAyarlari);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["KullaniciId"] = new SelectList(_context.Kullanicis, "KullaniciId", "KullaniciId", smtpAyarlari.KullaniciId);
        //    return View(smtpAyarlari);
        //}

        // GET: SmtpAyarlaris/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var smtpAyarlari = await _context.SmtpAyarlaris.FindAsync(id);
        //    if (smtpAyarlari == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["KullaniciId"] = new SelectList(_context.Kullanicis, "KullaniciId", "KullaniciId", smtpAyarlari.KullaniciId);
        //    return View(smtpAyarlari);
        //}

        // POST: SmtpAyarlaris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Guid id, [Bind("Id,SunucuAdi,Port,KullaniciAdi,Sifre,KullaniciId")] SmtpAyarlari smtpAyarlari)
        {

            if (id == default(Guid)) {

                //create

                if (ModelState.IsValid)
                {
                    smtpAyarlari.Id = Guid.NewGuid();
                    _context.Add(smtpAyarlari);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                //edit
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(smtpAyarlari);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!SmtpAyarlariExists(smtpAyarlari.Id))
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
            }
            return View(smtpAyarlari);
        }



        private bool SmtpAyarlariExists(Guid id)
        {
            return _context.SmtpAyarlaris.Any(e => e.Id == id);
        }
    }
}
