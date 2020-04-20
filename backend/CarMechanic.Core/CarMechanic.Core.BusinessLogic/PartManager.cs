using CarMechanic.Core.DataAccess;
using CarMechanic.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMechanic.Core.BusinessLogic
{
    public class PartManager
    {
        private PartAccess _partAccess = new PartAccess();

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
        public void SetPart(Alkatresz alkatresz)
        {
            _partAccess.SetPart(alkatresz);

        }

        public void RemovePart(Alkatresz alkatresz)
        {
            _partAccess.RemovePart(alkatresz);
        }

        public void SetPartPrice(int partId, decimal purchasePrice, decimal salesPrice)
        {
            _partAccess.SetPartPrice(partId, purchasePrice, salesPrice);
        }
    }
}
