using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Vera.Domain;
using Vera.Domain.Entity;

namespace Vera.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {

            /*--------------------------------Заполняем БД----------------------------------*/
            //FillInTheDatabase();
            /*--------------------------------Заполняем БД----------------------------------*/

            ViewBag.Format = new SelectList(db.Formats, "Id", "Name");
            ViewBag.FormingType = new SelectList(db.FormingTypes, "Id", "Name");

            return View();
        }

        public ActionResult SetGluePrice(int id)
        {
            return PartialView(db.GluePrices.FirstOrDefault(x => x.Format.Id == id));
        }

        public ActionResult FormingType(int id)
        {
            return PartialView(db.FormingTypes.FirstOrDefault(x => x.Id == id));
        }

        public decimal SetSewingPrice(int tetrCountInt)
        {
            if (tetrCountInt == null) return 0m;
            var idForming = 2;
            var sewing = db.Sewings.FirstOrDefault(x => x.FormingType.Id == idForming);
            if (sewing != null) return sewing.Price.Cost * sewing.Price.Currency.Rate * tetrCountInt;
            else MessageBox.Show("Не было найдено цены за шитьё, для такого идентификатора формата");
            
            return 0m;
        }

        public void FillInTheDatabase()
        {
            DatabaseContext context = new DatabaseContext();
            
            //context.
            //context.FormingTypes.Add(new FormingType() { Name = "Постраничный" });
            //context.FormingTypes.Add(new FormingType() { Name = "Потетрадный" });

            //context.Currencies.Add(new Currency() { Name = "RUB", Rate = 1m });
            //context.Currencies.Add(new Currency() { Name = "EUR", Rate = 68.47m });

            //context.SaveChanges();

            //var rubCurrency = context.Currencies.FirstOrDefault(x => x.Name == "RUB");

            //decimal cost = 10.24m;
            //double area = 999949;
            //for (int i = 0; i < 11; i++)
            //{
            //    context.Prices.Add(new Price() { Cost = cost, Currency = rubCurrency });
            //    context.Formats.Add(new Format() { Name = "A" + i, Area = area });
            //    context.SaveChanges();

            //    context.GluePrices.Add(new GluePrice()
            //    {
            //        Format = context.Formats.FirstOrDefault(x => x.Name == "A" + i),
            //        Price = context.Prices.FirstOrDefault(x => x.Cost == cost)
            //    });

            //    cost /= 2;
            //    area /= 2;
            //}
            context.SaveChanges();
            context.Dispose();
        }
    }
}
