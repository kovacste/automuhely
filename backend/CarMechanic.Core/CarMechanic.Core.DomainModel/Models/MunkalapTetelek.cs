using System;
using System.Collections.Generic;

namespace CarMechanic.Core.DomainModel.Models
{
    public partial class MunkalapTetelek
    {
        public int Id { get; set; }
        public int Munkalapid { get; set; }
        public int? Szolgaltatasid { get; set; }
        public int? Alkatreszid { get; set; }
        public int? Mennyiseg { get; set; }
        public decimal? Ar { get; set; }
        public DateTime Rogzitve { get; set; }
        public int Rogzitette { get; set; }

        public virtual Alkatreszek Alkatresz { get; set; }
        public virtual Munkalapok Munkalap { get; set; }
        public virtual Felhasznalok RogzitetteNavigation { get; set; }
        public virtual Szolgaltatasok Szolgaltatas { get; set; }
    }
}
