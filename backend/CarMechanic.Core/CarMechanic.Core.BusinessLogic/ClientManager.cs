﻿using CarMechanic.Core.DataAccess;
using CarMechanic.Core.DomainModel.Models;
using CarMechanic.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CarMechanic.Core.BusinessLogic
{
    public class ClientManager
    {
        private DbContextOptions<CarMechanicContext> _options;
        private ClientAccess _clientAccess;
        public ClientManager(DbContextOptions<CarMechanicContext> options)
        {
            _options = options;
            _clientAccess =  new ClientAccess(_options);
        }  

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
                    Jelszo = row.Jelszo,
                    Rogzitve = row.Rogzitve,
                    Rogzitette = row.RogzitetteNavigation.Nev
                });
            });
            return ugyfel;
        }

        public Ugyfel GetClient(int clientId)
        {

            var result = _clientAccess.GetClient(clientId);

            var ugyfel = new Ugyfel()
            {
                Id = result.Id,
                Nev = result.Nev,
                Adoszam = result.Adoszam,
                Telepulesid = result.Telepulesid,
                Irszam = result.Telepules.Irszam,
                Telepules = result.Telepules.Nev,
                Kozteruletneve = result.Kozteruletneve,
                Kozteruletjellegid = result.Kozteruletjellegid,
                Kozteruletjelleg = result.Kozteruletjelleg.Nev,
                Hazszam = result.Hazszam,
                Telefonszam = result.Telefonszam,
                Email = result.Email,
                Jelszo = result.Jelszo,
                Rogzitve = result.Rogzitve,
                Rogzitette = result.RogzitetteNavigation.Nev
            };

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

        public int SetClient(Ugyfel ugyfel)
        {
            return _clientAccess.SetClient(ugyfel);

        }

        public void RemoveClient(Ugyfel ugyfel)
        {
            _clientAccess.RemoveClient(ugyfel);
        }

        public Felhasznalo AuthenticateUser(string loginName, string password)
        {
            var result = _clientAccess.AuthenticateClient(loginName, password);
            if (result != null)
            {
                var currentUser = new Felhasznalo();
                currentUser.Id = result.Id;
                currentUser.LoginNev = result.Email;
                currentUser.Nev = result.Nev;
                var modul = new List<string>();
                modul.Add("Ügyfél");              
                currentUser.Modul = modul;
                return currentUser;
            }
            else
                throw new Exception("NOT_AUTHENTICATED");
        }
    }
}
