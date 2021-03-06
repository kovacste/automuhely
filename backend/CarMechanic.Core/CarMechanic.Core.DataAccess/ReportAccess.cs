﻿using CarMechanic.Core.DomainModel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarMechanic.Core.DataAccess
{
    public class ReportAccess
    {
        private readonly DbContextOptions<CarMechanicContext> _options;
        public ReportAccess(DbContextOptions<CarMechanicContext> options)
        {
            _options = options;
        }

        public List<Munkalapok> GetWorkerStatistic(int year, int month)
        {
            using (var context = new CarMechanicContext(_options))
            {
                return context.Munkalapok.Where(x => x.Rogzitve.Year == year && x.Rogzitve.Month == month).Include(x => x.RogzitetteNavigation).ToList();
            }
        }

        public List<Munkalapok> GetServiceStatistic(int year, int month)
        {
            using (var context = new CarMechanicContext(_options))
            {
                return context.Munkalapok.Where(x => x.Lezarva.Value.Year == year && x.Lezarva.Value.Month == month).Include(x=>x.LezartaNavigation).ToList();
            }
        }
    }
}
