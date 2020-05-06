using System;
using System.Collections.Generic;

namespace CarMechanic.Core.DomainModel.Models
{
    public partial class Ugyfelek
    {
        public Ugyfelek()
        {
            Munkalapok = new HashSet<Munkalapok>();
        }

        public int Id { get; set; }
        public string Nev { get; set; }
        public string Adoszam { get; set; }
        public int Telepulesid { get; set; }
        public string Kozteruletneve { get; set; }
        public int Kozteruletjellegid { get; set; }
        public string Hazszam { get; set; }
        public string Telefonszam { get; set; }
        public string Email { get; set; }
        public string Jelszo { get; set; }
        public DateTime Rogzitve { get; set; }
        public int Rogzitette { get; set; }

        public virtual KozteruletJellegek Kozteruletjelleg { get; set; }
        public virtual Felhasznalok RogzitetteNavigation { get; set; }
        public virtual Telepulesek Telepules { get; set; }
        public virtual ICollection<Munkalapok> Munkalapok { get; set; }
    }
}
