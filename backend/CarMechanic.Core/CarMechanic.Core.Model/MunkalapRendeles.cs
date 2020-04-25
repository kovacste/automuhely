using System;
using System.Collections.Generic;
using System.Text;

namespace CarMechanic.Core.Model
{
    public class MunkalapRendeles
    {
        public int Id { get; set; }
        public int Munkalapid { get; set; }
        public Alkatresz Alkatresz { get; set; }
        public int Mennyiseg { get; set; }
        public DateTime Rogzitve { get; set; }
        public string Rogzitette { get; set; }
    }
}
