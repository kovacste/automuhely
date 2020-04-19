using CarMechanic.Core.DataAccess;
using CarMechanic.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMechanic.Core.BusinessLogic
{
    public class ServiceManager
    {
        private ServiceAccess _serviceAccess = new ServiceAccess();

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
        public void SetService(Szolgaltatas szolgaltatas)
        {
            _serviceAccess.SetService(szolgaltatas);

        }
    }
}
