using CarMechanic.Core.DataAccess;
using CarMechanic.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMechanic.Core.BusinessLogic
{
    public class UserManager
    {
        private UserAccess _userAccess = new UserAccess();

        public Felhasznalo AuthenticateUser(string loginName, string password)
        {
            var result = _userAccess.AuthenticateUser(loginName, password);
            if (result != null)
            {
                var currentUser = new Felhasznalo();
                currentUser.LoginNev = result.Loginnev;
                currentUser.Nev = result.Nev;
                var modul = new List<string>();
                foreach (var row in result.FelhasznaloJogok)
                {
                    modul.Add(row.Jog.Nev);
                }
                currentUser.Modul = modul;
                return currentUser;
            }
            else
                throw new Exception("NOT_AUTHENTICATED");
        }
    }
}
