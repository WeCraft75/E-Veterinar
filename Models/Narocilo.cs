using System;
using System.Collections.Generic;

namespace E_Veterinar.Models
{
    public partial class Narocilo
    {
        public Narocilo()
        {
            Evidencas = new HashSet<Evidenca>();
            Racuns = new HashSet<Racun>();
            Ids = new HashSet<Zaloga>();
        }

        public decimal IdNarocilo { get; set; }
        public decimal? IdRacuna { get; set; }
        public decimal? IdStranka { get; set; }
        public decimal ZahtevanaKolicina { get; set; }
        public DateTime? DatumNarocila { get; set; }

        public virtual Racun? IdRacunaNavigation { get; set; }
        public virtual Stranka? IdStrankaNavigation { get; set; }
        public virtual ICollection<Evidenca> Evidencas { get; set; }
        public virtual ICollection<Racun> Racuns { get; set; }

        public virtual ICollection<Zaloga> Ids { get; set; }
    }
}
