using System;
using System.Collections.Generic;

namespace CarMechanic.Core.DomainModel.Models
{
    public partial class Jogok
    {
        public Jogok()
        {
            FelhasznaloJogok = new HashSet<FelhasznaloJogok>();
        }

        public int Id { get; set; }
        public string Nev { get; set; }

        public virtual ICollection<FelhasznaloJogok> FelhasznaloJogok { get; set; }
    }
}
