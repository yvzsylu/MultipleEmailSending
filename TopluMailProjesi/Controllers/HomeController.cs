using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TopluMailProjesi.Filter;
using TopluMailProjesi.Models;

namespace TopluMailProjesi.Controllers
{
    [UserFilter]
    public class HomeController : Controller
    {
        private readonly TMGContext _context;

        public HomeController(TMGContext context)
        {
            _context = context;
        }

        

        public IActionResult Dashboard()
        {
            string? KullaniciGuid = HttpContext.Session.GetString("id");
            Guid guid = new Guid(KullaniciGuid);

            var kisiListesi = _context.Kisis.Include(k => k.Kullanici).Where(g => g.KullaniciId == guid);
            var grupListesi = _context.Grups.Include(g => g.Kullanici).Where(g => g.KullaniciId == guid);
            var taslakListesi = _context.MailTaslaks.Include(m => m.Kullanici).Where(m => m.KullaniciId == guid);
            var kullanıcı = _context.Kullanicis.FirstOrDefault(g => g.KullaniciId == guid);

            ViewBag.KisiSayisi = kisiListesi.Count();
            ViewBag.GrupSayisi = grupListesi.Count();
            ViewBag.TaslakSayisi = taslakListesi.Count();
            ViewBag.IslemSayisi = kullanıcı.IslemSayisi;
            return View();
        }

        
    }
}