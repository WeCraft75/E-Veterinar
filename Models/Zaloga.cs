using System;
using System.Collections.Generic;

namespace E_Veterinar.Models
{
    public partial class Zaloga
    {
        public Zaloga()
        {
            IdNarocilos = new HashSet<Narocilo>();
        }

        public decimal IdIzdelek { get; set; }
        public decimal IdVeterinar { get; set; }
        public decimal Kolicina { get; set; }

        public virtual Izdelek IdIzdelekNavigation { get; set; } = null!;
        public virtual Veterinar IdVeterinarNavigation { get; set; } = null!;
        public virtual ICollection<Narocilo> IdNarocilos { get; set; }
    }
}
