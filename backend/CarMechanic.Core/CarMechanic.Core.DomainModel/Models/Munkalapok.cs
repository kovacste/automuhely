using System;
using System.Collections.Generic;

namespace CarMechanic.Core.DomainModel.Models
{
    public partial class Munkalapok
    {
        public Munkalapok()
        {
            MunkalapRendelesek = new HashSet<MunkalapRendelesek>();
        }

        public int Id { get; set; }
        public int Ugyfelid { get; set; }
        public DateTime Idopont { get; set; }
        public DateTime Rogzitve { get; set; }
        public int Rogzitette { get; set; }
        public DateTime? Lezarva { get; set; }
        public int? Lezarta { get; set; }

        public virtual Felhasznalok LezartaNavigation { get; set; }
        public virtual Felhasznalok RogzitetteNavigation { get; set; }
        public virtual Ugyfelek Ugyfel { get; set; }
        public virtual ICollection<MunkalapRendelesek> MunkalapRendelesek { get; set; }
    }
}
