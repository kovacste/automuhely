using CarMechanic.Core.DataAccess;
using CarMechanic.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CarMechanic.Core.BusinessLogic
{
    public class ReportManager
    {
        private ReportAccess _reportAccess = new ReportAccess();
        public List<DolgozoStatisztika> GetWorkerStatistic(int year, int month)
        {
            var result = _reportAccess.GetWorkerStatistic(year, month);
            var statistic = new List<DolgozoStatisztika>();      
            foreach (var row in result)
            {
                statistic.Add(new DolgozoStatisztika() { Nev = row.RogzitetteNavigation.Nev, MunkalapDb = 1 });
            }
            return statistic.GroupBy(x => x.Nev).Select(grp=> new DolgozoStatisztika() { Nev = grp.Key, MunkalapDb = grp.Sum(x=>x.MunkalapDb)}).ToList();
        }

        public List<DolgozoStatisztika> GetServiceStatistic(int year, int month)
        {
            var result = _reportAccess.GetServiceStatistic(year, month);
            var statistic = new List<DolgozoStatisztika>();
            foreach (var row in result)
            {
                statistic.Add(new DolgozoStatisztika() { Nev = row.LezartaNavigation.Nev, MunkalapDb = 1 });
            }
            return statistic.GroupBy(x => x.Nev).Select(grp => new DolgozoStatisztika() { Nev = grp.Key, MunkalapDb = grp.Sum(x => x.MunkalapDb) }).ToList();
        }
    }
}
