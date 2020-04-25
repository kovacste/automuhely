using System;
using System.Collections.Generic;
using System.Text;

namespace CarMechanic.Core.Model
{
    public class MunkalapTetel
    {
        public int Id { get; set; }
        public int Munkalapid { get; set; }
        public Szolgaltatas Szolgaltatas { get; set; }
        public int? Mennyiseg { get; set; }
        public decimal? Ar { get; set; }
        public DateTime Rogzitve { get; set; }
        public string Rogzitette { get; set; }
    }
}
