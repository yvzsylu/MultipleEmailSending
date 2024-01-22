using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TopluMailProjesi.Models
{
    public partial class MailTaslak
    {
        public MailTaslak()
        {
            MailDokumen = new HashSet<MailDokuman>();
        }

        public Guid MailTaslakId { get; set; }
        [DisplayName("Başlık")]
        public string Baslik { get; set; } = null!;
        [DisplayName("Açıklama")]
        public string Aciklama { get; set; } = null!;
        [DataType(DataType.Date)]
        [DisplayName("Gönderim Tarihi")]
        public DateTime GonderimTarihi { get; set; }
        [DisplayName("Sorgu No")]
        public string? SorguNo { get; set; }
        public Guid? KullaniciId { get; set; }
        public virtual Kullanici? Kullanici { get; set; }
        public virtual ICollection<MailDokuman> MailDokumen { get; set; }
    }
}
