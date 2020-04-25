using CarMechanic.Core.DataAccess;
using CarMechanic.Core.Model;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace CarMechanic.Core.BusinessLogic
{
    public class WorksheetManager
    {
        private WorksheetAccess _worksheetAccess = new WorksheetAccess();
        public List<Munkalap> GetWorkSheets()
        {
            var worksheets = new List<Munkalap>();
            var result = _worksheetAccess.GetWorkSheets();
            foreach (var row in result)
            {               
                worksheets.Add(new Munkalap() { Id = row.Id, Idopont = row.Idopont, Ugyfel = new Ugyfel() { Id = row.Ugyfel.Id, Nev = row.Ugyfel.Nev }, Lezarta = row.LezartaNavigation != null ? row.LezartaNavigation.Nev : null, Lezarva = row.Lezarva, Rogzitette = row.RogzitetteNavigation.Nev, Rogzitve = row.Rogzitve });
            }

            return worksheets;
        }

        public void SetWorkSheet(Munkalap worksheet)
        {
            _worksheetAccess.SetWorkSheet(worksheet);
        }

        public void SetWorkSheetDetails(MunkalapTetel[] details)
        {
            _worksheetAccess.SetWorkSheetDetails(details);
        }

        public void RemoveWorkSheetDetail(MunkalapTetel detail)
        {
            _worksheetAccess.RemoveWorkSheetDetail(detail);
        }

        public List<MunkalapRendeles> GetWorkSheetOrders(int worksheetId)
        {
            var orders = new List<MunkalapRendeles>();
            var result = _worksheetAccess.GetWorkSheetOrders(worksheetId);
            foreach (var row in orders)
            {                
                orders.Add(new MunkalapRendeles() { Id = row.Id, Alkatresz = new Alkatresz(), Mennyiseg = row.Mennyiseg, Munkalapid = row.Munkalapid, Rogzitve = row.Rogzitve, Rogzitette = row.Rogzitette});
            }

            return orders;
        }
    }
}
