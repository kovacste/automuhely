using CarMechanic.Core.DomainModel.Models;
using CarMechanic.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarMechanic.Core.DataAccess
{
    public class PartAccess
    {
        public List<Alkatreszek> GetParts()
        {
            using (var context = new CarMechanicContext())
            {
                return context.Alkatreszek.Include(x => x.RogzitetteNavigation).ToList();
            }
        }

        public void RemovePart(Alkatresz alkatresz)
        {
            using (var context = new CarMechanicContext())
            {
                var result = context.Alkatreszek.FirstOrDefault(x => x.Id == alkatresz.Id);
                if (result != null)
                {
                    context.Alkatreszek.Remove(result);
                    context.SaveChanges();
                }
            }
        }

        public void SetPart(Alkatresz alkatresz)
        {

            using (var context = new CarMechanicContext())
            {
                if (alkatresz.Id == 0)
                    context.Alkatreszek.Add(new Alkatreszek()
                    {
                        Nev = alkatresz.Nev,
                        Beszerar = alkatresz.Beszerar,
                        Eladasiar = alkatresz.Eladasiar,
                        Rogzitve = DateTime.Now,
                        Rogzitette = context.Felhasznalok.Where(x => x.Loginnev == alkatresz.Rogzitette).FirstOrDefault().Id
                    });
                else
                {
                    var result = context.Alkatreszek.FirstOrDefault(x => x.Id == alkatresz.Id);
                    if (result != null)
                    {
                        result.Nev = alkatresz.Nev;
                        result.Beszerar = alkatresz.Beszerar;
                        result.Eladasiar = alkatresz.Eladasiar;
                        result.Rogzitve = DateTime.Now;
                        result.Rogzitette = context.Felhasznalok.Where(x => x.Loginnev == alkatresz.Rogzitette).FirstOrDefault().Id;
                    }
                }
                context.SaveChanges();
            }
        }
    }
}