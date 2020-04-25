using System;
using System.Collections.Generic;

namespace CarMechanic.Core.DomainModel.Models
{
    public partial class Szolgaltatasok
    {
        public Szolgaltatasok()
        {
            MunkalapTetelek = new HashSet<MunkalapTetelek>();
        }

        public int Id { get; set; }
        public string Nev { get; set; }
        public string Me { get; set; }
        public decimal Egysegar { get; set; }
        public bool Ismetlodo { get; set; }
        public int? Ismetlodesiidoszak { get; set; }
        public DateTime Rogzitve { get; set; }
        public int Rogzitette { get; set; }

        public virtual Felhasznalok RogzitetteNavigation { get; set; }
        public virtual ICollection<MunkalapTetelek> MunkalapTetelek { get; set; }
    }
}
