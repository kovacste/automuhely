using CarMechanic.Core.DomainModel.Models;
using CarMechanic.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CarMechanic.Core.DataAccess
{
    public class ClientAccess
    {
        private readonly DbContextOptions<CarMechanicContext> _options;
        public ClientAccess(DbContextOptions<CarMechanicContext> options)
        {
            _options = options;
        }

        public List<Ugyfelek> GetClientList()
        {

            using (var context = new CarMechanicContext(_options))
            {
                return context.Ugyfelek.Include(x => x.Telepules).Include(x => x.Kozteruletjelleg).Include(x => x.RogzitetteNavigation).ToList();
            }
        }

        public Ugyfelek GetClient(int clientId)
        {
            using (var context = new CarMechanicContext(_options))
            {
                return context.Ugyfelek.Where(x=>x.Id == clientId).Include(x => x.Telepules).Include(x => x.Kozteruletjelleg).Include(x => x.RogzitetteNavigation).FirstOrDefault();
            }
        }

        public List<Telepulesek> GetCities()
        {
            using (var context = new CarMechanicContext(_options))
            {
                return context.Telepulesek.ToList();
            }
        }

        public List<KozteruletJellegek> GetStreetTypes()
        {
            using (var context = new CarMechanicContext(_options))
            {
                return context.KozteruletJellegek.ToList();
            }
        }

        public void RemoveClient(Ugyfel ugyfel)
        {
            using (var context = new CarMechanicContext(_options))
            {
                var result = context.Ugyfelek.FirstOrDefault(x => x.Id == ugyfel.Id);
                if (result != null)
                {
                    context.Ugyfelek.Remove(result);
                    context.SaveChanges();
                }
            }
        }

        public int SetClient(Ugyfel ugyfel)
        {
            var result = 0;
            using (var context = new CarMechanicContext(_options))
            {
                var client = new Ugyfelek();
                client = new Ugyfelek()
                {
                    Nev = ugyfel.Nev,
                    Adoszam = ugyfel.Adoszam,
                    Telepulesid = ugyfel.Telepulesid,
                    Kozteruletneve = ugyfel.Kozteruletneve,
                    Kozteruletjellegid = ugyfel.Kozteruletjellegid,
                    Hazszam = ugyfel.Hazszam,
                    Telefonszam = ugyfel.Telefonszam,
                    Email = ugyfel.Email,
                    Jelszo = ugyfel.Jelszo,
                    Rogzitve = DateTime.Now,
                    Rogzitette = context.Felhasznalok.Where(x => x.Loginnev == ugyfel.Rogzitette).FirstOrDefault().Id
                };
                if (ugyfel.Id == 0)                    
                    context.Ugyfelek.Add(client);
                else
                {
                    client = context.Ugyfelek.FirstOrDefault(x => x.Id == ugyfel.Id);
                    if(client != null)
                    {
                        client.Nev = ugyfel.Nev;
                        client.Adoszam = ugyfel.Adoszam;
                        client.Telepulesid = ugyfel.Telepulesid;
                        client.Kozteruletneve = ugyfel.Kozteruletneve;
                        client.Kozteruletjellegid = ugyfel.Kozteruletjellegid;
                        client.Hazszam = ugyfel.Hazszam;
                        client.Telefonszam = ugyfel.Telefonszam;
                        client.Email = ugyfel.Email;
                        client.Jelszo = ugyfel.Jelszo;
                        client.Rogzitve = DateTime.Now;
                        client.Rogzitette = context.Felhasznalok.Where(x => x.Loginnev == ugyfel.Rogzitette).FirstOrDefault().Id;
                    }
                }
                context.SaveChanges();
                result = client.Id;
            }
            return result;
        }

        public Ugyfelek AuthenticateClient(string loginName, string password)
        {
            using (var context = new CarMechanicContext(_options))
            {

                return context.Ugyfelek.Where(x => x.Email == loginName && x.Jelszo == password).FirstOrDefault();
            }
        }
    }
}
