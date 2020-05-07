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
        private readonly DbContextOptions<CarMechanicContext> _options;
        public WorksheetAccess(DbContextOptions<CarMechanicContext> options)
        {
            _options = options;
        }
        public List<Munkalapok> GetWorkSheets()
        {
            using (var context =  new CarMechanicContext(_options))
            {
                return context.Munkalapok.Include(x => x.RogzitetteNavigation).Include(x => x.LezartaNavigation).Include(x=>x.Ugyfel).ThenInclude(x=>x.Telepules).Include(x=>x.Ugyfel).ThenInclude(x=>x.Kozteruletjelleg).ToList();
            }
        }

        public Munkalapok GetWorkSheetWithClientId(int clientId)
        {
            using (var context = new CarMechanicContext(_options))
            {
                return context.Munkalapok.Where(x => x.Ugyfelid == clientId).Include(x => x.RogzitetteNavigation).Include(x => x.LezartaNavigation).Include(x => x.Ugyfel).ThenInclude(x => x.Telepules).Include(x => x.Ugyfel).ThenInclude(x => x.Kozteruletjelleg).FirstOrDefault();
            }
        }

        public Munkalapok GetWorkSheet(int workSheetId)
        {
            using (var context = new CarMechanicContext(_options))
            {
                return context.Munkalapok.Where(x => x.Id == workSheetId).Include(x => x.RogzitetteNavigation).Include(x => x.LezartaNavigation).Include(x => x.Ugyfel).ThenInclude(x => x.Telepules).Include(x => x.Ugyfel).ThenInclude(x => x.Kozteruletjelleg).FirstOrDefault();
            }
        }

        public List<MunkalapRendelesek> GetWorkSheetOrders(int worksheetId)
        {
            using (var context = new CarMechanicContext(_options))
            {
                return context.MunkalapRendelesek.Where(x => x.Munkalapid == worksheetId).Include(x => x.RogzitetteNavigation).Include(x => x.Alkatresz).ToList();
            }
        }

        public List<MunkalapTetelek> GetWorkSheetDetails(int worksheetId)
        {
            using (var context = new CarMechanicContext(_options))
            {
                return context.MunkalapTetelek.Where(x => x.Munkalapid == worksheetId).Include(x => x.RogzitetteNavigation).Include(x => x.Szolgaltatas).ToList();
            }
        }

        public MunkalapTetelek GetWorkSheetDetail(int detailId)
        {
            using (var context = new CarMechanicContext(_options))
            {
                return context.MunkalapTetelek.Where(x => x.Id == detailId).Include(x => x.RogzitetteNavigation).Include(x => x.Szolgaltatas).FirstOrDefault();
            }
        }

        public int SetWorkSheetOrders(MunkalapRendeles[] orders)
        {
            var result = 0;
            using (var context = new CarMechanicContext(_options))
            {
                var order = new MunkalapRendelesek();
                foreach (var row in orders)
                {
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
                result = order.Id;
            }
            return result;
        }

        public void RemoveWorkSheetOrder(MunkalapRendeles order)
        {
            using (var context = new CarMechanicContext(_options))
            {
                var result = context.MunkalapRendelesek.FirstOrDefault(x => x.Id == order.Id);
                if (result != null)
                {
                    context.MunkalapRendelesek.Remove(result);
                    context.SaveChanges();
                }
            }
        }

        public int SetWorkSheetDetails(MunkalapTetel[] details)
        {
            var result = 0;
            using (var context = new CarMechanicContext(_options))
            {
                var detail = new MunkalapTetelek();
                foreach (var row in details)
                {

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
                result = detail.Id;
            }
            return result;
        }

        public void RemoveWorkSheetDetail(MunkalapTetel detail)
        {
            using (var context = new CarMechanicContext(_options))
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
            using (var context = new CarMechanicContext(_options))
            {

                var ws = new Munkalapok();
                if (worksheet.Id == 0)
                {
                    ws.Ugyfelid = worksheet.Ugyfel.Id;
                    ws.Idopont = worksheet.Idopont;
                    ws.Rogzitve = DateTime.Now;
                    ws.Rogzitette = context.Felhasznalok.Where(x => x.Loginnev == worksheet.Rogzitette).FirstOrDefault().Id;
                    ws.MunkalapTetelek = new List<MunkalapTetelek>();
                    if (worksheet.Tetelek != null)
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

        public void RemoveWorkSheet(Munkalap worksheet)
        {
            using (var context = new CarMechanicContext(_options))
            {
                var oResult = context.MunkalapRendelesek.Where(x => x.Munkalapid == worksheet.Id);
                if (oResult != null)
                {
                    context.MunkalapRendelesek.RemoveRange(oResult);
                                    }
                var wsResult = context.MunkalapTetelek.Where(x => x.Munkalapid == worksheet.Id);
                if (wsResult != null)
                {
                    context.MunkalapTetelek.RemoveRange(wsResult);                    
                }
                var result = context.Munkalapok.FirstOrDefault(x => x.Id == worksheet.Id);
                if (result != null)
                {
                    context.Munkalapok.Remove(result);
                }
                context.SaveChanges();
            }
        }

        public void CloseWorkSheet(int worksheetId, string user)
        {
            using (var context = new CarMechanicContext(_options))
            {
                var ws = context.Munkalapok.FirstOrDefault(x => x.Id == worksheetId);
                ws.Lezarva = DateTime.Now;
                ws.Lezarta = context.Felhasznalok.Where(x => x.Loginnev == user).FirstOrDefault().Id;
                context.SaveChanges();
            }
        }
    }
}
