using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Veterinar.Models
{
    public partial class Stranka
    {
        public Stranka()
        {
            Narocilos = new HashSet<Narocilo>();
            Termins = new HashSet<Termin>();
        }

        public decimal IdStranka { get; set; }
        public decimal Stevilka { get; set; }
        public string Ime { get; set; } = null!;
        public string Priimek { get; set; } = null!;
        public string Naslov { get; set; } = null!;
        public string Kraj { get; set; } = null!;
        public ApplicationUser? AspNetID { get; set; }
        public virtual Postum StevilkaNavigation { get; set; } = null!;
        public virtual ICollection<Narocilo> Narocilos { get; set; }
        public virtual ICollection<Termin> Termins { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return Ime + " " + Priimek;
            }
        }
    }
}
