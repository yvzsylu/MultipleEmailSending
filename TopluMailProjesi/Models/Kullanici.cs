using System;
using System.Collections.Generic;

namespace TopluMailProjesi.Models
{
    public partial class Kullanici
    {
        public Kullanici()
        {
            Grups = new HashSet<Grup>();
            KisiGrups = new HashSet<KisiGrup>();
            Kisis = new HashSet<Kisi>();
            MailTaslaks = new HashSet<MailTaslak>();
        }

        public Guid KullaniciId { get; set; }
        public string Adi { get; set; } = null!;
        public string Soyadi { get; set; } = null!;
        public string KullaniciAdi { get; set; } = null!;
        public string Sifre { get; set; } = null!;

        public int IslemSayisi { get; set; }
        public virtual ICollection<Grup> Grups { get; set; }
        public virtual ICollection<KisiGrup> KisiGrups { get; set; }
        public virtual ICollection<Kisi> Kisis { get; set; }
        public virtual ICollection<MailTaslak> MailTaslaks { get; set; }
    }
}
