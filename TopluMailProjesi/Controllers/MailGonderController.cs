using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using TopluMailProjesi.Filter;
using TopluMailProjesi.Models;


namespace TopluMailProjesi.Controllers
{
    [UserFilter]
    public class MailGonderController : Controller
    {
        private readonly TMGContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public MailGonderController(TMGContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }


        // GET: MailGonderController
        public ActionResult Index()
        {
            string? KullaniciGuid = HttpContext.Session.GetString("id");
            Guid guid = new Guid(KullaniciGuid);


            ViewData["GrupId"] = new SelectList(_context.Grups.Include(g => g.Kullanici).Where(g => g.KullaniciId == guid), "GrupId", "GrupAdi");
            ViewData["MailTaslakId"] = new SelectList(_context.MailTaslaks.Include(g => g.Kullanici).Where(g => g.KullaniciId == guid), "MailTaslakId", "Baslik");



            return View();
        }
        
       
        // POST: MailGonderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("GrupId,MailTaslakId")] MailGonderViewModel model)
        {
            string? KullaniciGuid = HttpContext.Session.GetString("id");
            Guid guid = new Guid(KullaniciGuid);
            
            
            List<KisiGrup> kgList = _context.KisiGrups.Where(g => g.GrupId == model.GrupId).ToList();
            foreach (var item in kgList)
            {
                _context.Entry(item).Reference(g => g.Kisi).Load();
            }

            if (kgList.Count() == 0)
            {
                TempData["Message"] = "Lütfen gruba kişi ekledikten sonra tekrar deneyiniz";
                return Redirect("/MailGonder/Index");
            }
            List<MailDokuman> md = _context.MailDokumen.Where(g => g.MailTaslakId == model.MailTaslakId).ToList();
            foreach (var item in md)
            {
                _context.Entry(item).Reference(g => g.Dokuman).Load();
                _context.Entry(item).Reference(g => g.MailTaslak).Load();
            }


            var _smtpayarlari = _context.SmtpAyarlaris.FirstOrDefault(s => s.KullaniciId == guid);

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential(_smtpayarlari.KullaniciAdi, _smtpayarlari.Sifre);
            smtp.Port = Convert.ToInt32(_smtpayarlari.Port);
            smtp.Host = _smtpayarlari.SunucuAdi;
            smtp.EnableSsl = true;
            MailAddress smtpMail = new MailAddress(_smtpayarlari.KullaniciAdi);
            try
            {
                


                    foreach (var item in kgList)
                    {
                        MailMessage mail = new MailMessage();
                        mail.To.Add(item.Kisi.Email);
                        


                        foreach (var item2 in md)
                        {
                            Attachment a = new Attachment(_hostEnvironment.WebRootPath + "/files/" + item2.Dokuman.DokumanYolu);
                            mail.Attachments.Add(a);
                        }
                        mail.From = smtpMail;
                        mail.Subject = md[0].MailTaslak.Baslik;
                        mail.Body = "Mail Gönderme Servisi <br>" + md[0].MailTaslak.Aciklama;
                        mail.IsBodyHtml = true;
                        smtp.Send(mail);
                    }
                    TempData["Message"] = "Tüm Mailler Gönderilmiştir :)";
                var kullanıcı = _context.Kullanicis.FirstOrDefault(g => g.KullaniciId == guid);
                kullanıcı.IslemSayisi += kgList.Count();
                _context.Update(kullanıcı);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Mailler Gönderilemedi" + ex.Message;
            }
            ViewData["GrupId"] = new SelectList(_context.Grups.Include(g => g.Kullanici).Where(g => g.KullaniciId == guid), "GrupId", "GrupAdi", model.GrupId);
            ViewData["KisiId"] = new SelectList(_context.MailTaslaks.Include(k => k.Kullanici).Where(g => g.KullaniciId == guid), "KisiId", "Adi", model.MailTaslakId);
            return Redirect("/MailGonder/Index");
        }


        //MimeMessage mail = new MimeMessage();
        //MailboxAddress smtpMail = new MailboxAddress("mailgonderme","mailgondermeservisi@gmail.com");
        //mail.From.Add(smtpMail);
        //MailboxAddress to = new MailboxAddress("tomailgonderme", item.Kisi.Email);
        //mail.To.Add(to);

        //mail.Subject = model.MailTaslak.Baslik;

        //BodyBuilder bodyBuilder = new BodyBuilder();
        //bodyBuilder.HtmlBody = "<h1>Mail Gönderme Servisi</h1>";
        //bodyBuilder.TextBody = model.MailTaslak.Aciklama;
        //foreach (var item2 in md)
        //{
        //    bodyBuilder.Attachments.Add(_hostEnvironment.WebRootPath + "//" + item2.Dokuman.DokumanYolu);
        //}
        // GET: MailGonderController/Edit/5

    }
}
