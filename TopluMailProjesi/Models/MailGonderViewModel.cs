using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopluMailProjesi.Models
{
    public class MailGonderViewModel
    {
        [DisplayName("Taslak")]
        public Guid? MailTaslakId { get; set; }
        [DisplayName("Grup Adı")]
        public Guid? GrupId { get; set; }
        
        public virtual Grup? Grup { get; set; }
        
        public virtual MailTaslak? MailTaslak { get; set; }

        public Guid? KullaniciId { get; set; }
        public virtual Kullanici? Kullanici { get; set; }
    }
}
