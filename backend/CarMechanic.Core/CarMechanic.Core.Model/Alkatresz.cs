using System;
using System.Collections.Generic;
using System.Text;

namespace CarMechanic.Core.Model
{
    public class Alkatresz
    {
        public int Id { get; set; }
        public string Nev { get; set; }
        public decimal Beszerar { get; set; }
        public decimal Eladasiar { get; set; }
        public DateTime Rogzitve { get; set; }
        public string Rogzitette { get; set; }
    }
}
