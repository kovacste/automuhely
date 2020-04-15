using System;
using System.Collections.Generic;
using System.Text;

namespace CarMechanic.Core.Model
{
    public class Felhasznalo
    {
        public string LoginNev { get; set; }
        public string Nev { get; set; }

        public List<string> Modul { get; set; }
    }
}
