using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopluMailProjesi.Models;

namespace TopluMailProjesi.Controllers
{
    public class AccountController : Controller
    {
        private readonly TMGContext _context;

        public AccountController(TMGContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            
            
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("id")))
            {
                return Redirect("/Home/Dashboard");
            }
            return View();
        }

        public IActionResult Login(string username, string pass)
        {
            pass = Sifrele.MD5Olustur(pass);


            var user = _context.Kullanicis.FirstOrDefault(k => k.KullaniciAdi.Equals(username) && k.Sifre.Equals(pass)); 

            if (user != null)
            {
                HttpContext.Session.SetString("id", user.KullaniciId.ToString());
                HttpContext.Session.SetString("username", user.KullaniciAdi.ToString());
                HttpContext.Session.SetInt32("islemSayisi", user.IslemSayisi);
                return Redirect("/Home/Dashboard");
            }


            return Redirect("Index");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return Redirect("Index");
        }

        public IActionResult SignUp()
        {


            return View();
        }

        
        public async Task<IActionResult> Register(Kullanici model)
        {

            

            if (ModelState.IsValid)
            {
                
                model.KullaniciId = Guid.NewGuid();
                model.Sifre = Sifrele.MD5Olustur(model.Sifre);
                _context.Add(model);
                await _context.SaveChangesAsync();
                
            }
            return Redirect("Index");
        }
    }
}
