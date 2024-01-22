using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TopluMailProjesi.Models
{
    public partial class KisiGrup
    {
        public Guid KisiGrupId { get; set; }
        [DisplayName("Adı Soyadı")]
        public Guid? KisiId { get; set; } 
        [DisplayName("Grup Adı")]
        public Guid? GrupId { get; set; } 
        public Guid? KullaniciId { get; set; }

        public virtual Grup? Grup { get; set; } 
        [DisplayName("Kişi")]
        public virtual Kisi? Kisi { get; set; } 
        public virtual Kullanici? Kullanici { get; set; }
    }
}
