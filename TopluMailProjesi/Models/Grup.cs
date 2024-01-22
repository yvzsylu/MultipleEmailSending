using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TopluMailProjesi.Models
{
    public partial class Grup
    {
        public Grup()
        {
            KisiGrups = new HashSet<KisiGrup>();
        }

        public Guid GrupId { get; set; }

        [DisplayName("Grup Adı")]
        public string GrupAdi { get; set; } = null!;
        public Guid? KullaniciId { get; set; }

        public virtual Kullanici? Kullanici { get; set; }
        public virtual ICollection<KisiGrup> KisiGrups { get; set; }

        // List<Kisi> Kisiler
    }
}
