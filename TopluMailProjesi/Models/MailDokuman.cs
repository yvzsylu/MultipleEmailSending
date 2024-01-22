using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TopluMailProjesi.Models
{
    public partial class MailDokuman
    {
        public Guid MailDokumanId { get; set; }
        [DisplayName("Taslak")]
        public Guid? MailTaslakId { get; set; }
        [DisplayName("Döküman")]
        public Guid? DokumanId { get; set; }
        [DisplayName("Döküman")]
        public virtual Dokumanlar? Dokuman { get; set; }
        [DisplayName("Taslak")]
        public virtual MailTaslak? MailTaslak { get; set; }
    }
}
