using System;
using System.Collections.Generic;

namespace E_Veterinar.Models
{
    public partial class Postum
    {
        public Postum()
        {
            Strankas = new HashSet<Stranka>();
            Veterinars = new HashSet<Veterinar>();
        }

        public decimal Stevilka { get; set; }
        public string Naziv { get; set; } = null!;

        public virtual ICollection<Stranka> Strankas { get; set; }
        public virtual ICollection<Veterinar> Veterinars { get; set; }
    }
}
