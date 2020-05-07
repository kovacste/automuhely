using CarMechanic.Core.DataAccess;
using CarMechanic.Core.DomainModel.Models;
using CarMechanic.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace CarMechanic.Core.BusinessLogic
{
    public class WorksheetManager
    {
        private DbContextOptions<DomainModel.Models.CarMechanicContext> _options;
        private WorksheetAccess _worksheetAccess;
        public WorksheetManager(DbContextOptions<DomainModel.Models.CarMechanicContext> options)
        {
            _options = options;
            _worksheetAccess = new WorksheetAccess(_options);
        }

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

        public Munkalap GetWorkSheet(int workSheetId)
        {
            var worksheet = new Munkalap();
            var result = _worksheetAccess.GetWorkSheet(workSheetId);

            worksheet.Id = result.Id;
            worksheet.Idopont = result.Idopont;
            worksheet.Ugyfel = new Ugyfel() { Id = result.Ugyfel.Id, Nev = result.Ugyfel.Nev };
            worksheet.Lezarta = result.LezartaNavigation != null ? result.LezartaNavigation.Nev : null;
            worksheet.Lezarva = result.Lezarva;
            worksheet.Rogzitette = result.RogzitetteNavigation.Nev;
            worksheet.Rogzitve = result.Rogzitve;

            return worksheet;
        }

        public List<Munkalap> GetWorkSheetWithClientId(int clientId)
        {
            var worksheets = new List<Munkalap>();
            var result = _worksheetAccess.GetWorkSheetWithClientId(clientId);

            foreach (var row in result)
            {
                worksheets.Add(new Munkalap() { Id = row.Id, Idopont = row.Idopont, Ugyfel = new Ugyfel() { Id = row.Ugyfel.Id, Nev = row.Ugyfel.Nev }, Lezarta = row.LezartaNavigation != null ? row.LezartaNavigation.Nev : null, Lezarva = row.Lezarva, Rogzitette = row.RogzitetteNavigation.Nev, Rogzitve = row.Rogzitve });
            }

            return worksheets;
        }

        public int SetWorkSheet(Munkalap worksheet)
        {
            return _worksheetAccess.SetWorkSheet(worksheet);
        }

        public void RemoveWorkSheet(Munkalap worksheet)
        {
            _worksheetAccess.RemoveWorkSheet(worksheet);
        }

        public int SetWorkSheetDetails(MunkalapTetel[] details)
        {
            return _worksheetAccess.SetWorkSheetDetails(details);
        }

        public void RemoveWorkSheetDetail(MunkalapTetel detail)
        {
            _worksheetAccess.RemoveWorkSheetDetail(detail);
        }

        public List<MunkalapTetel> GetWorkSheetDetails(int worksheetId)
        {
            var details = new List<MunkalapTetel>();

            var result = _worksheetAccess.GetWorkSheetDetails(worksheetId);
            foreach (var row in result)
            {
                details.Add(new MunkalapTetel() { Id = row.Id, Szolgaltatas = new Szolgaltatas() { Id = row.Szolgaltatas.Id, Nev = row.Szolgaltatas.Nev }, Mennyiseg = row.Mennyiseg, Ar = row.Ar, Munkalapid = row.Munkalapid, Rogzitve = row.Rogzitve, Rogzitette = row.RogzitetteNavigation.Nev });
            }
            return details;
        }

        public MunkalapTetel GetWorkSheetDetail(int detailId)
        {
            var detail = new MunkalapTetel();

            var result = _worksheetAccess.GetWorkSheetDetail(detailId);
            detail.Id = result.Id;
            detail.Szolgaltatas = new Szolgaltatas() { Id = result.Szolgaltatas.Id, Nev = result.Szolgaltatas.Nev };
            detail.Mennyiseg = result.Mennyiseg;
            detail.Ar = result.Ar;
            detail.Munkalapid = result.Munkalapid;
            detail.Rogzitve = result.Rogzitve;
            detail.Rogzitette = result.RogzitetteNavigation.Nev;

            return detail;
        }

        public List<MunkalapRendeles> GetWorkSheetOrders(int worksheetId)
        {
            var orders = new List<MunkalapRendeles>();
            var result = _worksheetAccess.GetWorkSheetOrders(worksheetId);
            foreach (var row in result)
            {
                orders.Add(new MunkalapRendeles() { Id = row.Id, Alkatresz = new Alkatresz() { Id = row.Alkatresz.Id, Nev = row.Alkatresz.Nev, Beszerar = row.Alkatresz.Beszerar, Eladasiar = row.Alkatresz.Eladasiar }, Mennyiseg = row.Mennyiseg, Munkalapid = row.Munkalapid, Rogzitve = row.Rogzitve, Rogzitette = row.RogzitetteNavigation.Nev });
            }

            return orders;
        }

        public int SetWorkSheetOrders(MunkalapRendeles[] orders)
        {
            return _worksheetAccess.SetWorkSheetOrders(orders);
        }

        public void RemoveWorkSheetOrder(MunkalapRendeles order)
        {
            _worksheetAccess.RemoveWorkSheetOrder(order);
        }

        public void CloseWorkSheet(int worksheetId, string user)
        {

            _worksheetAccess.CloseWorkSheet(worksheetId, user);
        }
    }
}
