using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopluMailProjesi.Models
{
    public partial class Dokumanlar
    {
        public Dokumanlar()
        {
            MailDokumen = new HashSet<MailDokuman>();
        }

        public Guid DokumanId { get; set; }
        public string? DokumanYolu { get; set; } = null!;
        public Guid? KullaniciId { get; set; }
        public virtual Kullanici? Kullanici { get; set; }
        public virtual ICollection<MailDokuman> MailDokumen { get; set; }

        [NotMapped]
        public IFormFile Dosya { get; set; }
    }
}
