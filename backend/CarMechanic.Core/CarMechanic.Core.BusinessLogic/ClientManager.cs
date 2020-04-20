using CarMechanic.Core.DataAccess;
using CarMechanic.Core.DomainModel.Models;
using CarMechanic.Core.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMechanic.Core.BusinessLogic
{
    public class ClientManager
    {
        private ClientAccess _clientAccess = new ClientAccess();

        public List<Ugyfel> GetClientList()
        {
            var ugyfel = new List<Ugyfel>();
            var result = _clientAccess.GetClientList();
            result.ForEach(row =>
            {
                ugyfel.Add(new Ugyfel()
                {
                    Id = row.Id,
                    Nev = row.Nev,
                    Adoszam = row.Adoszam,
                    Telepulesid = row.Telepulesid,
                    Irszam = row.Telepules.Irszam,
                    Telepules = row.Telepules.Nev,
                    Kozteruletneve = row.Kozteruletneve,
                    Kozteruletjellegid = row.Kozteruletjellegid,
                    Kozteruletjelleg = row.Kozteruletjelleg.Nev,
                    Hazszam = row.Hazszam,
                    Telefonszam = row.Telefonszam,
                    Email = row.Email,
                    Rogzitve = row.Rogzitve,
                    Rogzitette = row.RogzitetteNavigation.Nev
                });
            });
            return ugyfel;
        }

        public List<Telepules> GetCities()
        {
            var telepules = new List<Telepules>();
            var result = _clientAccess.GetCities();
            result.ForEach(row =>
            {
                telepules.Add(new Telepules()
                {
                    Id = row.Id,
                    Irszam = row.Irszam,
                    Nev = row.Nev
                });
            });
            return telepules;
        }

        public List<KozteruletJelleg> GetStreetTypes()
        {
            var jelleg = new List<KozteruletJelleg>();
            var result = _clientAccess.GetStreetTypes();
            result.ForEach(row =>
            {
                jelleg.Add(new KozteruletJelleg()
                {
                    Id = row.Id,
                    Nev = row.Nev
                });
            });
            return jelleg;
        }

        public void SetClient(Ugyfel ugyfel)
        {
            _clientAccess.SetClient(ugyfel);

        }

        public void RemoveClient(Ugyfel ugyfel)
        {
            _clientAccess.RemoveClient(ugyfel);
        }
    }
}
