﻿using CarMechanic.Core.DomainModel.Models;
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
                return context.Ugyfelek.Include(x => x.Telepules).Include(x => x.Kozteruletjelleg).Include(x=>x.RogzitetteNavigation).ToList();
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

        public void SetClient(Ugyfel ugyfel)
        {

            using (var context = new CarMechanicContext())
            {
                if (ugyfel.Id == 0)
                    context.Ugyfelek.Add(new Ugyfelek() {
                        Nev = ugyfel.Nev,                        
                        Adoszam = ugyfel.Adoszam,
                        Telepulesid = ugyfel.Telepulesid,
                        Kozteruletneve = ugyfel.Kozteruletneve,
                        Kozteruletjellegid = ugyfel.Kozteruletjellegid,
                        Hazszam = ugyfel.Hazszam,
                        Telefonszam = ugyfel.Telefonszam,
                        Email = ugyfel.Email,
                        Rogzitve = DateTime.Now,
                        Rogzitette =  context.Felhasznalok.Where(x=>x.Loginnev == ugyfel.Rogzitette).FirstOrDefault().Id
                    });
                else
                {
                    var result = context.Ugyfelek.FirstOrDefault(x => x.Id == ugyfel.Id);
                    if(result != null)
                    {
                        result.Nev = ugyfel.Nev;
                        result.Adoszam = ugyfel.Adoszam;
                        result.Telepulesid = ugyfel.Telepulesid;
                        result.Kozteruletneve = ugyfel.Kozteruletneve;
                        result.Kozteruletjellegid = ugyfel.Kozteruletjellegid;
                        result.Hazszam = ugyfel.Hazszam;
                        result.Telefonszam = ugyfel.Telefonszam;
                        result.Email = ugyfel.Email;
                        result.Rogzitve = DateTime.Now;
                        result.Rogzitette = context.Felhasznalok.Where(x => x.Loginnev == ugyfel.Rogzitette).FirstOrDefault().Id;
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
