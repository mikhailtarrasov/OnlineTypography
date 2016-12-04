using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using Vera.Domain;
using Vera.Domain.Entity;

namespace Vera.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            DatabaseContext Context = new DatabaseContext();

            Context.Currencies.Add(new Currency() { Name = "RUB", Rate = 1m });
            Context.Currencies.Add(new Currency() { Name = "EUR", Rate = 68.47m });

            Context.SaveChanges();

            var rubCurrency = Context.Currencies.FirstOrDefault(x => x.Name == "RUB");

            /*--------------------------------Заполняем БД----------------------------------*/
            decimal cost = 10.24m;
            double area = 999949;
            for (int i = 0; i < 11; i++)    
            {
                Context.Prices.Add(new Price() { Cost = cost, Currency = rubCurrency });   
                Context.Formats.Add(new Format() { Name = "A" + i, Area = area });         
                Context.SaveChanges();

                Context.GluePrices.Add(new GluePrice()
                {
                    Format = Context.Formats.FirstOrDefault(x => x.Name == "A" + i),
                    Price = Context.Prices.FirstOrDefault(x => x.Cost == cost)
                });

                cost /= 2;
                area /= 2;
            }
            Context.SaveChanges();
            /*--------------------------------Заполняем БД----------------------------------*/


            //IEnumerable<Currency> currencies = Context.Currencies;
            //ViewBag.Currencies = currencies;

            return View();
        }

    }
}
