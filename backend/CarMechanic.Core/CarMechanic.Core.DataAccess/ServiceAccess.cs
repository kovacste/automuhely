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
        private readonly DbContextOptions<CarMechanicContext> _options;
        public ServiceAccess(DbContextOptions<CarMechanicContext> options)
        {
            _options = options;
        }
        public List<Szolgaltatasok> GetServices()
        {
            using (var context = new CarMechanicContext(_options))
            {
                return context.Szolgaltatasok.Include(x => x.RogzitetteNavigation).ToList();
            }
        }

        public Szolgaltatasok GetService(int serviceId)
        {
            using (var context = new CarMechanicContext(_options))
            {
                return context.Szolgaltatasok.Where(x => x.Id == serviceId).Include(x => x.RogzitetteNavigation).FirstOrDefault();
            }
        }

        public void RemoveService(Szolgaltatas szolgaltatas)
        {
            using (var context = new CarMechanicContext(_options))
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
            using (var context = new CarMechanicContext(_options))
            {
                var result = context.Szolgaltatasok.FirstOrDefault(x => x.Id == serviceId);
                if (result != null)
                {
                    result.Egysegar = price;
                }
                context.SaveChanges();
            }
        }        

        public int SetService(Szolgaltatas szolgaltatas)
        {
            var result = 0;
            using (var context = new CarMechanicContext(_options))
            {
                var service = new Szolgaltatasok();
                if (szolgaltatas.Id == 0)
                {
                    service = new Szolgaltatasok()
                    {
                        Nev = szolgaltatas.Nev,
                        Me = szolgaltatas.Me,
                        Egysegar = szolgaltatas.Egysegar,
                        Ismetlodo = szolgaltatas.Ismetlodo,
                        Ismetlodesiidoszak = szolgaltatas.Ismetlodesiidoszak,
                        Rogzitve = DateTime.Now,
                        Rogzitette = context.Felhasznalok.Where(x => x.Loginnev == szolgaltatas.Rogzitette).FirstOrDefault().Id
                    };
                    context.Szolgaltatasok.Add(service);
                }
                else
                {
                    service = context.Szolgaltatasok.FirstOrDefault(x => x.Id == szolgaltatas.Id);                    
                    if (service != null)
                    {
                        service.Nev = szolgaltatas.Nev;
                        service.Me = szolgaltatas.Me;
                        service.Egysegar = szolgaltatas.Egysegar;
                        service.Ismetlodo = szolgaltatas.Ismetlodo;
                        service.Ismetlodesiidoszak = szolgaltatas.Ismetlodesiidoszak;
                        service.Rogzitve = DateTime.Now;
                        service.Rogzitette = context.Felhasznalok.Where(x => x.Loginnev == szolgaltatas.Rogzitette).FirstOrDefault().Id;

                    }
                }
                context.SaveChanges();
                result = service.Id;
            }
            return result;
        }
    }
}
