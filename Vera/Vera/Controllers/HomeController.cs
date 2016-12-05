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

            /*--------------------------------Заполняем БД----------------------------------*/
            //FillInTheDatabase();
            /*--------------------------------Заполняем БД----------------------------------*/

            var context = new DatabaseContext();

            ViewBag.Format = new SelectList(context.Formats, "Id", "Name");
            
            return View();
        }



        public void FillInTheDatabase()
        {
            DatabaseContext context = new DatabaseContext();

            context.Currencies.Add(new Currency() { Name = "RUB", Rate = 1m });
            context.Currencies.Add(new Currency() { Name = "EUR", Rate = 68.47m });

            context.SaveChanges();

            var rubCurrency = context.Currencies.FirstOrDefault(x => x.Name == "RUB");

            decimal cost = 10.24m;
            double area = 999949;
            for (int i = 0; i < 11; i++)
            {
                context.Prices.Add(new Price() { Cost = cost, Currency = rubCurrency });
                context.Formats.Add(new Format() { Name = "A" + i, Area = area });
                context.SaveChanges();

                context.GluePrices.Add(new GluePrice()
                {
                    Format = context.Formats.FirstOrDefault(x => x.Name == "A" + i),
                    Price = context.Prices.FirstOrDefault(x => x.Cost == cost)
                });

                cost /= 2;
                area /= 2;
            }
            context.SaveChanges();
        }
    }

   
}
