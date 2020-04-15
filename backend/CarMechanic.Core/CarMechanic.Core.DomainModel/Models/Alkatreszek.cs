using System;
using System.Collections.Generic;

namespace CarMechanic.Core.DomainModel.Models
{
    public partial class Alkatreszek
    {
        public Alkatreszek()
        {
            MunkalapRendelesek = new HashSet<MunkalapRendelesek>();
        }

        public int Id { get; set; }
        public string Nev { get; set; }
        public decimal Beszerar { get; set; }
        public decimal Eladasiar { get; set; }
        public DateTime Rogzitve { get; set; }
        public int Rogzitette { get; set; }

        public virtual Felhasznalok RogzitetteNavigation { get; set; }
        public virtual ICollection<MunkalapRendelesek> MunkalapRendelesek { get; set; }
    }
}
