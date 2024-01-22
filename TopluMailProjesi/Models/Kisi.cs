using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopluMailProjesi.Models
{
    public partial class Kisi
    {
        public Kisi()
        {
            KisiGrups = new HashSet<KisiGrup>();
        }

        public Guid KisiId { get; set; }

        [DisplayName("Adı")]
        public string Adi { get; set; } = null!;
        [DisplayName("Soyadı")]
        public string Soyadi { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Cep { get; set; } = null!;
        public Guid? KullaniciId { get; set; }
        public string AdSoyad
        {
            get
            {
                return string.Format("{0} {1}", Adi, Soyadi);
            }
        }
        public virtual Kullanici? Kullanici { get; set; }
        public virtual ICollection<KisiGrup> KisiGrups { get; set; }

        // List<Grup> Gruplar
    }
}
