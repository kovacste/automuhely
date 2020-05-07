using CarMechanic.Core.DataAccess;
using CarMechanic.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMechanic.Core.BusinessLogic
{
    public class PartManager
    {
        private DbContextOptions<DomainModel.Models.CarMechanicContext> _options;
        private PartAccess _partAccess;
        public PartManager(DbContextOptions<DomainModel.Models.CarMechanicContext> options)
        {
            _options = options;
            _partAccess = new PartAccess(_options);
        }
        

        public List<Alkatresz> GetParts()
        {
            var alkatresz = new List<Alkatresz>();
            var result = _partAccess.GetParts();
            foreach (var row in result)
            {
                alkatresz.Add(new Alkatresz()
                {
                    Id = row.Id,
                    Nev = row.Nev,
                    Eladasiar = row.Eladasiar,
                    Beszerar = row.Beszerar,
                    Rogzitette = row.RogzitetteNavigation.Nev,
                    Rogzitve = row.Rogzitve
                });
            }
            return alkatresz;
        }

        public Alkatresz GetPart(int partId)
        {

            var result = _partAccess.GetPart(partId);

            var alkatresz = new Alkatresz()
            {
                Id = result.Id,
                Nev = result.Nev,
                Eladasiar = result.Eladasiar,
                Beszerar = result.Beszerar,
                Rogzitette = result.RogzitetteNavigation.Nev,
                Rogzitve = result.Rogzitve
            };

            return alkatresz;
        }

        public int SetPart(Alkatresz alkatresz)
        {
            return _partAccess.SetPart(alkatresz);

        }

        public void RemovePart(Alkatresz alkatresz)
        {
            _partAccess.RemovePart(alkatresz);
        }

        public void SetPartPrice(Alkatresz data)
        {
            _partAccess.SetPartPrice(data);
        }
    }
}
