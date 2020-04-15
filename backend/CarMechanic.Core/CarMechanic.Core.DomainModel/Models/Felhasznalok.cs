using System;
using System.Collections.Generic;

namespace CarMechanic.Core.DomainModel.Models
{
    public partial class Felhasznalok
    {
        public Felhasznalok()
        {
            Alkatreszek = new HashSet<Alkatreszek>();
            FelhasznaloJogok = new HashSet<FelhasznaloJogok>();
            MunkalapRendelesek = new HashSet<MunkalapRendelesek>();
            MunkalapokLezartaNavigation = new HashSet<Munkalapok>();
            MunkalapokRogzitetteNavigation = new HashSet<Munkalapok>();
            Szolgaltatasok = new HashSet<Szolgaltatasok>();
            Ugyfelek = new HashSet<Ugyfelek>();
        }

        public int Id { get; set; }
        public string Loginnev { get; set; }
        public string Nev { get; set; }
        public string Jelszo { get; set; }

        public virtual ICollection<Alkatreszek> Alkatreszek { get; set; }
        public virtual ICollection<FelhasznaloJogok> FelhasznaloJogok { get; set; }
        public virtual ICollection<MunkalapRendelesek> MunkalapRendelesek { get; set; }
        public virtual ICollection<Munkalapok> MunkalapokLezartaNavigation { get; set; }
        public virtual ICollection<Munkalapok> MunkalapokRogzitetteNavigation { get; set; }
        public virtual ICollection<Szolgaltatasok> Szolgaltatasok { get; set; }
        public virtual ICollection<Ugyfelek> Ugyfelek { get; set; }
    }
}
