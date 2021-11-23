using System;
using System.Collections.Generic;

namespace E_Veterinar.Models
{
    public partial class Racun
    {
        public Racun()
        {
            Narocilos = new HashSet<Narocilo>();
        }

        public decimal IdRacuna { get; set; }
        public decimal IdNarocilo { get; set; }
        public DateTime DatumRacuna { get; set; }

        public virtual Narocilo IdNarociloNavigation { get; set; } = null!;
        public virtual ICollection<Narocilo> Narocilos { get; set; }
    }
}
