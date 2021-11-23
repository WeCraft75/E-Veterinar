using System;
using System.Collections.Generic;

namespace E_Veterinar.Models
{
    public partial class Storitev
    {
        public Storitev()
        {
            IdEvidences = new HashSet<Evidenca>();
        }

        public decimal IdStoritev { get; set; }
        public string? OpisStoritve { get; set; }

        public virtual ICollection<Evidenca> IdEvidences { get; set; }
    }
}
