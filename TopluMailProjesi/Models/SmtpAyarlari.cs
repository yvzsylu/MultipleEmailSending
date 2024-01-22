namespace TopluMailProjesi.Models
{
    public class SmtpAyarlari
    {


        public Guid Id { get; set; } 
        public string SunucuAdi { get; set; } = null!;

        public string Port { get; set; } = null!;
        public string KullaniciAdi { get; set; } = null!;
        public string Sifre { get; set; } = null!;


        public Guid? KullaniciId { get; set; }
        public virtual Kullanici? Kullanici { get; set; }




    }
}
