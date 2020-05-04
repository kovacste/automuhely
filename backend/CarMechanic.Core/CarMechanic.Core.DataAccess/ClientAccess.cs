using CarMechanic.Core.DomainModel.Models;
using CarMechanic.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CarMechanic.Core.DataAccess
{
    public class ClientAccess
    {
        public List<Ugyfelek> GetClientList()
        {
            using (var context = new CarMechanicContext())
            {
                return context.Ugyfelek.Include(x => x.Telepules).Include(x => x.Kozteruletjelleg).Include(x => x.RogzitetteNavigation).ToList();
            }
        }

        public Ugyfelek GetClient(int clientId)
        {
            using (var context = new CarMechanicContext())
            {
                return context.Ugyfelek.Where(x=>x.Id == clientId).Include(x => x.Telepules).Include(x => x.Kozteruletjelleg).Include(x => x.RogzitetteNavigation).FirstOrDefault();
            }
        }

        public List<Telepulesek> GetCities()
        {
            using (var context = new CarMechanicContext())
            {
                return context.Telepulesek.ToList();
            }
        }

        public List<KozteruletJellegek> GetStreetTypes()
        {
            using (var context = new CarMechanicContext())
            {
                return context.KozteruletJellegek.ToList();
            }
        }

        public void RemoveClient(Ugyfel ugyfel)
        {
            using (var context = new CarMechanicContext())
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
            using (var context = new CarMechanicContext())
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
                        client.Rogzitve = DateTime.Now;
                        client.Rogzitette = context.Felhasznalok.Where(x => x.Loginnev == ugyfel.Rogzitette).FirstOrDefault().Id;
                    }
                }
                context.SaveChanges();
                result = client.Id;
            }
            return result;
        }
    }
}
