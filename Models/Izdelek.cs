using System;
using System.Collections.Generic;

namespace E_Veterinar.Models
{
    public partial class Izdelek
    {
        public Izdelek()
        {
            Zalogas = new HashSet<Zaloga>();
        }

        public decimal IdIzdelek { get; set; }
        public string Ime { get; set; } = null!;
        public decimal Cena { get; set; }
        public string? Opis { get; set; }

        public virtual ICollection<Zaloga> Zalogas { get; set; }
    }
}
