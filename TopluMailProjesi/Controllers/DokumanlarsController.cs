using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TopluMailProjesi.Filter;
using TopluMailProjesi.Models;

namespace TopluMailProjesi.Controllers
{
    [UserFilter]
    public class DokumanlarsController : Controller
    {
        private readonly TMGContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public DokumanlarsController(TMGContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dokumanlars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DokumanId,DokumanYolu,Dosya")] Dokumanlar dokumanlar)
        {
            string? KullaniciGuid = HttpContext.Session.GetString("id");
            Guid guid = new Guid(KullaniciGuid);




            if (ModelState.IsValid)
            {

                var dosyaYolu = Path.Combine(_hostEnvironment.WebRootPath, "files"); // birleştirme
                if (!Directory.Exists(dosyaYolu)) //yoksa
                {
                    Directory.CreateDirectory(dosyaYolu); // oluştur
                }

                var tamDosyaAdi = Path.Combine(dosyaYolu, dokumanlar.Dosya.FileName);
                //fileupload                                                                                       
                using (var dosyaAkisi = new FileStream(tamDosyaAdi,FileMode.Create)) //garbage collection beklemeden kaldırılır
                {
                    await dokumanlar.Dosya.CopyToAsync(dosyaAkisi);
                }

                dokumanlar.DokumanYolu = dokumanlar.Dosya.FileName;
                dokumanlar.DokumanId = Guid.NewGuid();
                dokumanlar.KullaniciId = guid;
                _context.Add(dokumanlar);
                await _context.SaveChangesAsync();
                TempData["mesaj"] = "Dosya Eklenmiştir :)";
                
            }
            return View();
        }

        
    }
}
