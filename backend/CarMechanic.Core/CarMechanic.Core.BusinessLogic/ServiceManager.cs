using CarMechanic.Core.DataAccess;
using CarMechanic.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMechanic.Core.BusinessLogic
{
    public class ServiceManager
    {
        private DbContextOptions<DomainModel.Models.CarMechanicContext> _options;
        private ServiceAccess _serviceAccess;
        public ServiceManager(DbContextOptions<DomainModel.Models.CarMechanicContext> options)
        {
            _options = options;
            _serviceAccess = new ServiceAccess(_options);
        }        

        public List<Szolgaltatas> GetServices()
        {
            var szolgaltatas = new List<Szolgaltatas>();
            var result = _serviceAccess.GetServices();
            foreach (var row in result)
            {
                szolgaltatas.Add(new Szolgaltatas()
                {
                    Id = row.Id,
                    Nev = row.Nev,
                    Me = row.Me,
                    Egysegar = row.Egysegar,
                    Ismetlodesiidoszak = row.Ismetlodesiidoszak,
                    Ismetlodo = row.Ismetlodo,
                    Rogzitette = row.RogzitetteNavigation.Nev,
                    Rogzitve = row.Rogzitve
                });
            }
            return szolgaltatas;
        }

        public Szolgaltatas GetService(int serviceId)
        {
            var result = _serviceAccess.GetService(serviceId);
            var szolgaltatas = new Szolgaltatas()
            {
                Id = result.Id,
                Nev = result.Nev,
                Me = result.Me,
                Egysegar = result.Egysegar,
                Ismetlodesiidoszak = result.Ismetlodesiidoszak,
                Ismetlodo = result.Ismetlodo,
                Rogzitette = result.RogzitetteNavigation.Nev,
                Rogzitve = result.Rogzitve
            };

            return szolgaltatas;
        }

        public int SetService(Szolgaltatas szolgaltatas)
        {
            return _serviceAccess.SetService(szolgaltatas);

        }

        public void RemoveService(Szolgaltatas szolgaltatas)
        {
            _serviceAccess.RemoveService(szolgaltatas);
        }

        public void SetServicePrice(int serviceId, decimal price)
        {
            _serviceAccess.SetServicePrice(serviceId, price);
        }
    }
}
