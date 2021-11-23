using System;
using System.Collections.Generic;

namespace E_Veterinar.Models
{
    public partial class Termin
    {
        public Termin()
        {
            Evidencas = new HashSet<Evidenca>();
        }

        public decimal IdVeterinar { get; set; }
        public DateTime DatumZacetka { get; set; }
        public DateTime DatumKonca { get; set; }
        public decimal IdStranka { get; set; }
        public bool JeZaseden { get; set; }
        public bool JePotrjen { get; set; }

        public virtual Stranka IdStrankaNavigation { get; set; } = null!;
        public virtual Veterinar IdVeterinarNavigation { get; set; } = null!;
        public virtual ICollection<Evidenca> Evidencas { get; set; }
    }
}
