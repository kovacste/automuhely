using CarMechanic.Core.DomainModel.Models;
using CarMechanic.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Markup;

namespace CarMechanic.Core.DataAccess
{
   
    public class WorksheetAccess
    {
        public List<Munkalapok> GetWorkSheets()
        {
            using (var context =  new CarMechanicContext())
            {
                return context.Munkalapok.Include(x => x.RogzitetteNavigation).Include(x => x.LezartaNavigation).Include(x=>x.Ugyfel).ThenInclude(x=>x.Telepules).Include(x=>x.Ugyfel).ThenInclude(x=>x.Kozteruletjelleg).ToList();
            }
        }

        public Munkalapok GetWorkSheet(int workSheetId)
        {
            using (var context = new CarMechanicContext())
            {
                return context.Munkalapok.Where(x => x.Id == workSheetId).Include(x => x.RogzitetteNavigation).Include(x => x.LezartaNavigation).Include(x => x.Ugyfel).ThenInclude(x => x.Telepules).Include(x => x.Ugyfel).ThenInclude(x => x.Kozteruletjelleg).FirstOrDefault();
            }
        }

        public List<MunkalapRendelesek> GetWorkSheetOrders(int worksheetId)
        {
            using (var context = new CarMechanicContext())
            {
                return context.MunkalapRendelesek.Where(x => x.Munkalapid == worksheetId).Include(x => x.RogzitetteNavigation).Include(x => x.Alkatresz).ToList();
            }
        }

        public List<MunkalapTetelek> GetWorkSheetDetails(int worksheetId)
        {
            using (var context = new CarMechanicContext())
            {
                return context.MunkalapTetelek.Where(x => x.Munkalapid == worksheetId).Include(x => x.RogzitetteNavigation).Include(x => x.Szolgaltatas).ToList();
            }
        }

        public MunkalapTetelek GetWorkSheetDetail(int detailId)
        {
            using (var context = new CarMechanicContext())
            {
                return context.MunkalapTetelek.Where(x => x.Id == detailId).Include(x => x.RogzitetteNavigation).Include(x => x.Szolgaltatas).FirstOrDefault();
            }
        }

        public void SetWorkSheetOrders(MunkalapRendeles[] orders)
        {
            using (var context = new CarMechanicContext())
            {
                foreach (var row in orders)
                {
                    var order = new MunkalapRendelesek();
                    if (row.Id == 0)
                    {
                        order.Munkalapid = row.Munkalapid;
                        order.Alkatreszid = row.Alkatresz.Id;
                        order.Mennyiseg = row.Mennyiseg;
                        order.Rogzitve = DateTime.Now;
                        order.Rogzitette = context.Felhasznalok.Where(x => x.Loginnev == row.Rogzitette).FirstOrDefault().Id;
                        context.MunkalapRendelesek.Add(order);
                    }
                    else
                    {
                        order = context.MunkalapRendelesek.FirstOrDefault(x => x.Id == row.Id);
                        order.Alkatreszid = row.Alkatresz.Id;
                        order.Mennyiseg = row.Mennyiseg;
                        order.Rogzitve = DateTime.Now;
                        order.Rogzitette = context.Felhasznalok.Where(x => x.Loginnev == row.Rogzitette).FirstOrDefault().Id;
                    }
                }
                context.SaveChanges();
            }
        }

        public void RemoveWorkSheetOrder(MunkalapRendeles order)
        {
            using (var context = new CarMechanicContext())
            {
                var result = context.MunkalapRendelesek.FirstOrDefault(x => x.Id == order.Id);
                if (result != null)
                {
                    context.MunkalapRendelesek.Remove(result);
                    context.SaveChanges();
                }
            }
        }

        public void SetWorkSheetDetails(MunkalapTetel[] details)
        {
            using (var context = new CarMechanicContext())
            {
                foreach (var row in details)
                {
                    var detail = new MunkalapTetelek();
                    if (row.Id == 0)
                    {
                        detail.Munkalapid = row.Munkalapid;
                        detail.Szolgaltatasid = row.Szolgaltatas.Id;
                        detail.Mennyiseg = row.Mennyiseg;
                        detail.Ar = row.Ar;
                        detail.Rogzitve = DateTime.Now;
                        detail.Rogzitette = context.Felhasznalok.Where(x => x.Loginnev == row.Rogzitette).FirstOrDefault().Id;
                        context.MunkalapTetelek.Add(detail);
                    }
                    else
                    {
                        detail = context.MunkalapTetelek.FirstOrDefault(x => x.Id == row.Id);
                        detail.Szolgaltatasid = row.Szolgaltatas.Id;
                        detail.Mennyiseg = row.Mennyiseg;
                        detail.Ar = row.Ar; 
                        detail.Rogzitve = DateTime.Now;                        
                        detail.Rogzitette = context.Felhasznalok.Where(x => x.Loginnev == row.Rogzitette).FirstOrDefault().Id;
                    }
                }
                context.SaveChanges();
            }
        }

        public void RemoveWorkSheetDetail(MunkalapTetel detail)
        {
            using (var context = new CarMechanicContext())
            {
                var result = context.MunkalapTetelek.FirstOrDefault(x => x.Id == detail.Id);
                if (result != null)
                {
                    context.MunkalapTetelek.Remove(result);
                    context.SaveChanges();
                }
            }
        }


        public int SetWorkSheet(Munkalap worksheet)
        {
            using (var context = new CarMechanicContext())
            {

                var ws = new Munkalapok();
                if (ws.Id == 0)
                {
                    ws.Ugyfelid = worksheet.Ugyfel.Id;
                    ws.Idopont = worksheet.Idopont;
                    ws.Rogzitve = DateTime.Now;
                    ws.Rogzitette = context.Felhasznalok.Where(x => x.Loginnev == worksheet.Rogzitette).FirstOrDefault().Id;
                    ws.MunkalapTetelek = new List<MunkalapTetelek>();
                    foreach (var row in worksheet.Tetelek)
                    {
                        ws.MunkalapTetelek.Add(new MunkalapTetelek()
                        {                            
                            Ar = row.Ar,
                            Szolgaltatasid = row.Szolgaltatas.Id,
                            Mennyiseg = row.Mennyiseg,
                            Rogzitve = ws.Rogzitve,
                            Rogzitette = ws.Rogzitette

                        });
                    }                            
                    context.Munkalapok.Add(ws);
                }
                else
                {
                    ws = context.Munkalapok.FirstOrDefault(x => x.Id == worksheet.Id);
                    ws.Idopont = worksheet.Idopont;
                    ws.Rogzitve = DateTime.Now;
                    ws.Rogzitette = context.Felhasznalok.Where(x => x.Loginnev == worksheet.Rogzitette).FirstOrDefault().Id;
                }                
                context.SaveChanges();

                return ws.Id;
            }
        }

        public void CloseWorkSheet(int worksheetId, string user)
        {
            using (var context = new CarMechanicContext())
            {
                var ws = context.Munkalapok.FirstOrDefault(x => x.Id == worksheetId);
                ws.Lezarva = DateTime.Now;
                ws.Lezarta = context.Felhasznalok.Where(x => x.Loginnev == user).FirstOrDefault().Id;
            }
        }
    }
}
