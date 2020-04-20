using System;
using System.Collections.Generic;
using System.Text;

namespace CarMechanic.Core.Model
{
    public class Szolgaltatas
    {
        public int Id { get; set; }
        public string Nev { get; set; }
        public string Me { get; set; }
        public decimal Egysegar { get; set; }
        public bool Ismetlodo { get; set; }
        public int? Ismetlodesiidoszak { get; set; }
        public DateTime Rogzitve { get; set; }
        public string Rogzitette { get; set; }
    }
}
