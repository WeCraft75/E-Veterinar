using System;
using System.Collections.Generic;

namespace E_Veterinar.Models
{
    public partial class Veterinar
    {
        public Veterinar()
        {
            Termins = new HashSet<Termin>();
            Zalogas = new HashSet<Zaloga>();
        }

        public decimal IdVeterinar { get; set; }
        public decimal Stevilka { get; set; }
        public string Ime { get; set; } = null!;
        public string Priimek { get; set; } = null!;
        public string Kraj { get; set; } = null!;
        public bool NaDom { get; set; }

        public virtual Postum StevilkaNavigation { get; set; } = null!;
        public virtual ICollection<Termin> Termins { get; set; }
        public virtual ICollection<Zaloga> Zalogas { get; set; }
    }
}
