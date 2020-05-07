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
        private readonly DbContextOptions<CarMechanicContext> _options;
        public PartAccess(DbContextOptions<CarMechanicContext> options)
        {
            _options = options;
        }

        public List<Alkatreszek> GetParts()
        {
            using (var context = new CarMechanicContext(_options))
            {
                return context.Alkatreszek.Include(x => x.RogzitetteNavigation).ToList();
            }
        }

        public Alkatreszek GetPart(int partId)
        {
            using (var context = new CarMechanicContext(_options))
            {
                return context.Alkatreszek.Where(x => x.Id == partId).Include(x => x.RogzitetteNavigation).FirstOrDefault();
            }
        }

        public void RemovePart(Alkatresz alkatresz)
        {
            using (var context = new CarMechanicContext(_options))
            {
                var result = context.Alkatreszek.FirstOrDefault(x => x.Id == alkatresz.Id);
                if (result != null)
                {
                    context.Alkatreszek.Remove(result);
                    context.SaveChanges();
                }
            }
        }

        public int SetPart(Alkatresz alkatresz)
        {
            var result = 0;
            using (var context = new CarMechanicContext(_options))
            {
                var part = new Alkatreszek();
                if (alkatresz.Id == 0)
                {
                    part = new Alkatreszek()
                    {
                        Nev = alkatresz.Nev,
                        Beszerar = alkatresz.Beszerar,
                        Eladasiar = alkatresz.Eladasiar,
                        Rogzitve = DateTime.Now,
                        Rogzitette = context.Felhasznalok.Where(x => x.Loginnev == alkatresz.Rogzitette).FirstOrDefault().Id
                    };
                    context.Alkatreszek.Add(part);
                }
                else
                {
                    part = context.Alkatreszek.FirstOrDefault(x => x.Id == alkatresz.Id);
                    if (part != null)
                    {
                        part.Nev = alkatresz.Nev;
                        part.Beszerar = alkatresz.Beszerar;
                        part.Eladasiar = alkatresz.Eladasiar;
                        part.Rogzitve = DateTime.Now;
                        part.Rogzitette = context.Felhasznalok.Where(x => x.Loginnev == alkatresz.Rogzitette).FirstOrDefault().Id;
                    }
                }
                context.SaveChanges();
                result = part.Id;

            }
            return result;
        }

        public void SetPartPrice(Alkatresz data)
        {
            using (var context = new CarMechanicContext(_options))
            {
                var result = context.Alkatreszek.FirstOrDefault(x => x.Id == data.Id);
                if (result != null)
                {
                    result.Beszerar = data.Beszerar;
                    result.Eladasiar = data.Eladasiar;
                }
                context.SaveChanges();
            }
        }
    }
}