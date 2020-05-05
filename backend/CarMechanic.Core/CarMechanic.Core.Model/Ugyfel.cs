using System;
using System.Collections.Generic;
using System.Text;

namespace CarMechanic.Core.Model
{
    public class Ugyfel
    {
        public int Id { get; set; }
        public string Nev { get; set; }
        public string Adoszam { get; set; }
        public int Telepulesid { get; set; }
        public string Irszam { get; set; }
        public string Telepules { get; set; }
        public string Kozteruletneve { get; set; }
        public int Kozteruletjellegid { get; set; }
        public string Kozteruletjelleg { get; set; }
        public string Hazszam { get; set; }
        public string Telefonszam { get; set; }
        public string Email { get; set; }
        public string Jelszo { get; set; }
        public DateTime Rogzitve { get; set; }
        public string Rogzitette { get; set; }

    }
}
