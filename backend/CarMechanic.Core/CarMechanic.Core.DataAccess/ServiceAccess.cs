using CarMechanic.Core.DomainModel.Models;
using CarMechanic.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarMechanic.Core.DataAccess
{
    public class ServiceAccess
    {
        public List<Szolgaltatasok> GetServices()
        {
            using (var context = new CarMechanicContext())
            {
                return context.Szolgaltatasok.Include(x => x.RogzitetteNavigation).ToList();
            }
        }

        public void RemoveService(Szolgaltatas szolgaltatas)
        {
            using (var context = new CarMechanicContext())
            {
                var result = context.Szolgaltatasok.FirstOrDefault(x => x.Id == szolgaltatas.Id);
                if (result != null)
                {
                    context.Szolgaltatasok.Remove(result);
                    context.SaveChanges();
                }
            }
        }

        public void SetServicePrice(int serviceId, decimal price)
        {
            using (var context = new CarMechanicContext())
            {
                var result = context.Szolgaltatasok.FirstOrDefault(x => x.Id == serviceId);
                if (result != null)
                {
                    result.Egysegar = price;
                }
                context.SaveChanges();
            }
        }        

        public void SetService(Szolgaltatas szolgaltatas)
        {

            using (var context = new CarMechanicContext())
            {
                if (szolgaltatas.Id == 0)
                    context.Szolgaltatasok.Add(new Szolgaltatasok()
                    {
                        Nev = szolgaltatas.Nev,
                        Me = szolgaltatas.Me,
                        Egysegar = szolgaltatas.Egysegar,
                        Ismetlodo = szolgaltatas.Ismetlodo,
                        Ismetlodesiidoszak = szolgaltatas.Ismetlodesiidoszak,
                        Rogzitve = DateTime.Now,
                        Rogzitette = context.Felhasznalok.Where(x => x.Loginnev == szolgaltatas.Rogzitette).FirstOrDefault().Id
                    });
                else
                {
                    var result = context.Szolgaltatasok.FirstOrDefault(x => x.Id == szolgaltatas.Id);
                    if (result != null)
                    {
                        result.Nev = szolgaltatas.Nev;
                        result.Me = szolgaltatas.Me;
                        result.Egysegar = szolgaltatas.Egysegar;
                        result.Ismetlodo = szolgaltatas.Ismetlodo;
                        result.Ismetlodesiidoszak = szolgaltatas.Ismetlodesiidoszak;              
                        result.Rogzitve = DateTime.Now;
                        result.Rogzitette = context.Felhasznalok.Where(x => x.Loginnev == szolgaltatas.Rogzitette).FirstOrDefault().Id;
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
