using System;
using System.Collections.Generic;

namespace E_Veterinar.Models
{
    public partial class Evidenca
    {
        public Evidenca()
        {
            IdStoritevs = new HashSet<Storitev>();
        }

        public decimal IdEvidence { get; set; }
        public decimal? IdVeterinar { get; set; }
        public DateTime? DatumZacetka { get; set; }
        public DateTime? DatumKonca { get; set; }
        public decimal IdNarocilo { get; set; }
        public decimal Cena { get; set; }

        public virtual Narocilo IdNarociloNavigation { get; set; } = null!;
        public virtual Termin? Termin { get; set; }

        public virtual ICollection<Storitev> IdStoritevs { get; set; }
    }
}
