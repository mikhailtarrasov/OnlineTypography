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
        // Фальцовка. Только для потетрадного типа формировки блока, умножается на кол-во тетрадей
        private decimal fillisterPrice = 0.5m;   
                                            

        public ActionResult Index()
        {

            /*--------------------------------Заполняем БД----------------------------------*/
            //FillInTheDatabase();
            /*--------------------------------Заполняем БД----------------------------------*/

            ViewBag.Format = new SelectList(db.Formats, "Id", "Name");
            ViewBag.FormingType = new SelectList(db.FormingTypes, "Id", "Name");

            return View();
        }

        public ActionResult Inventory()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public decimal SetGluePrice(int id)
        {
            var model = db.GluePrices.FirstOrDefault(x => x.Format.Id == id);
            return model.Price.Cost*model.Price.Currency.Rate;
        }

        public ActionResult FormingType(int id)
        {
            return PartialView(db.FormingTypes.FirstOrDefault(x => x.Id == id));
        }

        public decimal SetSewingPrice(string id)
        {
            int response = Int32.Parse(id);

            int idForming = response%10;
            int tetrCountInt = response/10;

            var sewing = db.Sewings.FirstOrDefault(x => x.FormingType.Id == idForming);
            if (sewing != null) return sewing.Price.Cost*sewing.Price.Currency.Rate*tetrCountInt;
            //else MessageBox.Show("Не было найдено цены за шитьё, для такого идентификатора формата");
            
            return 0m;
        }

        public decimal SetPageCountPrice(int? id)
        {
            int pageCount;
            if (id == null) pageCount = 0;
            else pageCount = (int)id;
            var job = db.Jobs.FirstOrDefault(x => x.JobTitle == "Подбор блока");
            return job.Pay.Cost * job.Pay.Currency.Rate * pageCount; 
        }

        public decimal SetFillisterPrice(int id)
        {
            return fillisterPrice*id;
        }

        public void FillInTheDatabase()
        {
            //var sewingTetr = db.Sewings.FirstOrDefault(x => x.FormingType.Name == "Потетрадный");
            //sewingTetr.Price.Cost = 2.50m;
            //var rubCurrency = db.Currencies.FirstOrDefault(x => x.Name == "RUB");
            //var postrFormingType = db.FormingTypes.FirstOrDefault(x => x.Name == "Постраничный");
            //db.Sewings.Add(new Sewing()
            //{
            //    FormingType = postrFormingType,
            //    Price = new Price()
            //    {
            //        Cost = 50m,
            //        Currency = rubCurrency
            //    }
            //});
            //db.FormingTypes.Add(new FormingType() { Name = "Постраничный" });
            //db.FormingTypes.Add(new FormingType() { Name = "Потетрадный" });

            //db.Currencies.Add(new Currency() { Name = "RUB", Rate = 1m });
            //db.Currencies.Add(new Currency() { Name = "EUR", Rate = 68.47m });

            //db.SaveChanges();

            //var rubCurrency = db.Currencies.FirstOrDefault(x => x.Name == "RUB");

            //decimal cost = 10.24m;
            //double area = 999949;
            //for (int i = 0; i < 11; i++)
            //{
            //    db.Prices.Add(new Price() { Cost = cost, Currency = rubCurrency });
            //    db.Formats.Add(new Format() { Name = "A" + i, Area = area });
            //    db.SaveChanges();

            //    db.GluePrices.Add(new GluePrice()
            //    {
            //        Format = db.Formats.FirstOrDefault(x => x.Name == "A" + i),
            //        Price = db.Prices.FirstOrDefault(x => x.Cost == cost)
            //    });

            //    cost /= 2;
            //    area /= 2;
            //}

            //db.Jobs.Add(new Job()
            //{
            //    JobTitle = "Подбор блока",
            //    Pay = new Price()
            //    {
            //        Cost = 0.02m,
            //        Currency = db.Currencies.FirstOrDefault(x => x.Name == "RUB")
            //    }
            //});
            //db.SaveChanges();

            // TO DO HERE

            //db.Dispose();
        }
    }
}
