using CarMechanic.Core.DomainModel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarMechanic.Core.DataAccess
{
    public class UserAccess
    {
        public Felhasznalok AuthenticateUser(string loginName, string password)
        {
            using (var context = new CarMechanicContext())
            {
              
                return context.Felhasznalok.Include(x=>x.FelhasznaloJogok).ThenInclude(x=>x.Jog).Where(x => x.Loginnev == loginName && x.Jelszo == password).FirstOrDefault();
            }
        }

    }
}
