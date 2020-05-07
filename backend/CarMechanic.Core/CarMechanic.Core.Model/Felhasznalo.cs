using System;
using System.Collections.Generic;
using System.Text;

namespace CarMechanic.Core.Model
{
    public class Felhasznalo
    {
        public int Id { get; set; }
        public string LoginNev { get; set; }
        public string Nev { get; set; }
        public string Jelszo { get; set; }
        public List<string> Modul { get; set; }
    }
}
