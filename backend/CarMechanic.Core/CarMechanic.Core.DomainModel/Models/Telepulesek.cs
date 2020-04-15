using System;
using System.Collections.Generic;

namespace CarMechanic.Core.DomainModel.Models
{
    public partial class Telepulesek
    {
        public Telepulesek()
        {
            Ugyfelek = new HashSet<Ugyfelek>();
        }

        public int Id { get; set; }
        public string Irszam { get; set; }
        public string Nev { get; set; }

        public virtual ICollection<Ugyfelek> Ugyfelek { get; set; }
    }
}
