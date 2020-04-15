using System;
using System.Collections.Generic;

namespace CarMechanic.Core.DomainModel.Models
{
    public partial class FelhasznaloJogok
    {
        public int Id { get; set; }
        public int Felhasznaloid { get; set; }
        public int Jogid { get; set; }

        public virtual Felhasznalok Felhasznalo { get; set; }
        public virtual Jogok Jog { get; set; }
    }
}
