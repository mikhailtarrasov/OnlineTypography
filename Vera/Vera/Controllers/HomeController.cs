using System;
using System.Linq;
using System.Web.Mvc;
using Vera.Domain;
using Vera.Domain.Entity;

namespace Vera.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        private DatabaseContext db = new DatabaseContext();
        // Фальцовка. Только для потетрадного типа формировки блока, умножается на кол-во тетрадей
        private decimal fillisterPrice = 0.5m;      // Фальцовка
        private decimal trimmingBlock = 10m;        // Подрезка блока
                                            

        public ActionResult Index()
        {

            /*--------------------------------Заполняем БД----------------------------------*/
            //FillInTheDatabase();
            /*--------------------------------Заполняем БД----------------------------------*/

            ViewBag.Format = new SelectList(db.Formats, "Id", "Name");
            ViewBag.FormingType = new SelectList(db.FormingTypes, "Id", "Name");
            ViewBag.Cardboard = new SelectList(db.Materials.Where(x => x.Type.TypeName == "Картон"), "Id", "Name");
            ViewBag.BindingMaterials = new SelectList(db.Materials.Where(x => x.Type.TypeName == "Переплетный материал"), "Id", "Name");

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public decimal SetGluePrice(int id)
        {
            var model = db.GluePrices.FirstOrDefault(x => x.Format.Id == id);
            return model.Price.Cost * model.Price.Currency.Rate;
        }

        public ActionResult FormingType(int id)
        {
            ViewBag.Colorfulness = new SelectList(db.Colorfulnesses, "Id", "Name");
            return PartialView(db.FormingTypes.FirstOrDefault(x => x.Id == id));
        }

        public decimal SetSewingPrice(string id)
        {
            int response = Int32.Parse(id);

            int idForming = response%10;
            int tetrCountInt = response/10;

            var sewing = db.Sewings.FirstOrDefault(x => x.FormingType.Id == idForming);
            if (sewing != null) return sewing.Price.Cost*sewing.Price.Currency.Rate*tetrCountInt;
            
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

        public decimal SetTrimmingBlockPrice()
        {
            return trimmingBlock;
        }

        public decimal SetMaterialPrice(int id)
        {
            int formatId = id % 100;
            id /= 100;
            var material = db.Materials.FirstOrDefault(x => x.Id == id);
            var format = db.Formats.Find(formatId);
            return Decimal.Round(material.Price.Cost * material.Price.Currency.Rate * 2 * format.Area, 2);    // Стоимость за кв.м. * две площади формата
        }

        public void FillInTheDatabase()
        {
            //db.Currencies.Add(new Currency() { Name = "RUB", Rate = 1m });
            ////db.Currencies.Add(new Currency() { Name = "EUR", Rate = 68.47m });
            //db.Currencies.Add(new Currency() { Name = "EUR", Rate = 58.7m });

            //db.SaveChanges();

            //var rubCurrency = db.Currencies.FirstOrDefault(x => x.Name == "RUB");

            //db.FormingTypes.Add(new FormingType() { Name = "Постраничный" });
            //db.FormingTypes.Add(new FormingType() { Name = "Потетрадный" });

            //db.SaveChanges();

            //var postrFormingType = db.FormingTypes.FirstOrDefault(x => x.Name == "Постраничный");
            //var potetrFormingType = db.FormingTypes.FirstOrDefault(x => x.Name == "Потетрадный");
            //db.Sewings.Add(new Sewing()
            //{
            //    FormingType = postrFormingType,
            //    Price = new Price()
            //    {
            //        Cost = 50m,
            //        Currency = rubCurrency
            //    }
            //});
            //db.Sewings.Add(new Sewing()
            //{
            //    FormingType = potetrFormingType,
            //    Price = new Price()
            //    {
            //        Cost = 2.50m,
            //        Currency = rubCurrency
            //    }
            //});

            //decimal cost = 10.24m;
            //decimal area = 0.999949m;
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
            //db.MaterialTypes.Add(new MaterialType() { TypeName = "Картон" });
            //db.MaterialTypes.Add(new MaterialType() { TypeName = "Переплетный материал" });

            //db.SaveChanges();

            //var cardboard = db.MaterialTypes.FirstOrDefault(x => x.TypeName == "Картон");
            //var bindingMaterial = db.MaterialTypes.FirstOrDefault(x => x.TypeName == "Переплетный материал");

            //db.Materials.Add(new Material()
            //{
            //    Name = "Обычный картон",
            //    Price = new Price()
            //    {
            //        Cost = 30m,
            //        Currency = rubCurrency
            //    },
            //    Type = cardboard
            //});
            //db.Materials.Add(new Material()
            //{
            //    Name = "Картон на поролоне",
            //    Price = new Price()
            //    {
            //        Cost = 50m,
            //        Currency = rubCurrency
            //    },
            //    Type = cardboard
            //});

            //db.Materials.Add(new Material()
            //{
            //    Name = "Кожа",
            //    Price = new Price()
            //    {
            //        Cost = 500m,
            //        Currency = rubCurrency
            //    },
            //    Type = bindingMaterial
            //});
            //db.Materials.Add(new Material()
            //{
            //    Name = "Кож. зам.",
            //    Price = new Price()
            //    {
            //        Cost = 300m,
            //        Currency = rubCurrency
            //    },
            //    Type = bindingMaterial
            //});

            //db.SaveChanges();

            //var materials = db.Materials;
            //foreach (var material in materials)
            //{
            //    material.Balance = 0;
            //}
            //db.Colorfulnesses.Add(new Colorfulness() { Name = "1+0" });
            //db.Colorfulnesses.Add(new Colorfulness() { Name = "1+1" });
            //db.Colorfulnesses.Add(new Colorfulness() { Name = "4+0" });
            //db.Colorfulnesses.Add(new Colorfulness() { Name = "4+1" });
            //db.Colorfulnesses.Add(new Colorfulness() { Name = "4+4" });
            //db.SaveChanges();
            //var cost = 115;

            //var rubCurr = db.Currencies.FirstOrDefault(x => x.Name == "RUB");

            //for (int i = 0; i <= 10; i++)
            //{
            //    var currentFormat = db.Formats.FirstOrDefault(x => x.Name == "A" + i);

            //    db.ColorfulnessPricePerFormats.Add(new ColorfulnessPricePerFormat()
            //    {
            //        Colorfulness = db.Colorfulnesses.First(x => x.Name == "1+0"),
            //        Format = currentFormat,
            //        Price = new Price()
            //        {
            //            Cost = cost,
            //            Currency = rubCurr
            //        }
            //    });
            //    db.ColorfulnessPricePerFormats.Add(new ColorfulnessPricePerFormat()
            //    {
            //        Colorfulness = db.Colorfulnesses.First(x => x.Name == "1+1"),
            //        Format = currentFormat,
            //        Price = new Price()
            //        {
            //            Cost = cost * 2,
            //            Currency = rubCurr
            //        }
            //    });
            //    db.ColorfulnessPricePerFormats.Add(new ColorfulnessPricePerFormat()
            //    {
            //        Colorfulness = db.Colorfulnesses.First(x => x.Name == "4+0"),
            //        Format = currentFormat,
            //        Price = new Price()
            //        {
            //            Cost = cost * 4,
            //            Currency = rubCurr
            //        }
            //    });
            //    db.ColorfulnessPricePerFormats.Add(new ColorfulnessPricePerFormat()
            //    {
            //        Colorfulness = db.Colorfulnesses.First(x => x.Name == "4+1"),
            //        Format = currentFormat,
            //        Price = new Price()
            //        {
            //            Cost = cost * 5,
            //            Currency = rubCurr
            //        }
            //    });
            //    db.ColorfulnessPricePerFormats.Add(new ColorfulnessPricePerFormat()
            //    {
            //        Colorfulness = db.Colorfulnesses.First(x => x.Name == "4+4"),
            //        Format = currentFormat,
            //        Price = new Price()
            //        {
            //            Cost = cost * 8,
            //            Currency = rubCurr
            //        }
            //    });
            //    cost /= 2;
            //}
            //db.SaveChanges();

            //db.Dispose();
        }
    }
}
