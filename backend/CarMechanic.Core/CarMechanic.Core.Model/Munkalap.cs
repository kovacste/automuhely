using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CarMechanic.Core.Model
{
    public class Munkalap
    {
        public int Id { get; set; }
        public Ugyfel Ugyfel { get; set; }
        public DateTime Idopont { get; set; }
        public DateTime Rogzitve { get; set; }
        public string Rogzitette { get; set; }
        public DateTime? Lezarva { get; set; }
        public string? Lezarta { get; set; }

        public List<MunkalapTetel> Tetelek { get; set; }
    }
}
