using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vera.Domain;
using Vera.Models;

namespace Vera.Controllers
{
    public class PrintingController : Controller
    {
        private DatabaseContext _db = new DatabaseContext();

        // GET: Printing
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Format = new SelectList(_db.Formats, "Id", "Name");
            ViewBag.FormingType = new SelectList(_db.FormingTypes, "Id", "Name");
            ViewBag.Colorfulness = new SelectList(_db.Colorfulnesses, "Id", "Name");

            return View();
        }

        public decimal GetPrintingPrice(int? formatId, int? colorfulnessId)
        {
            if (formatId.HasValue && colorfulnessId.HasValue)
            {
                var colorfulnessPricePerFormats = _db.ColorfulnessPricePerFormats.FirstOrDefault(
                    x => x.Format.Id == formatId.Value && x.Colorfulness.Id == colorfulnessId.Value);
                return colorfulnessPricePerFormats.Price.Cost * colorfulnessPricePerFormats.Price.Currency.Rate;
            }
            return 0;
        }

        [HttpPost]
        public ActionResult Index(PrintingPriceViewModel printingVM)
        {
            if (printingVM.FormatId.HasValue && printingVM.ColorfulnessId.HasValue &&
                printingVM.NewCost.HasValue)
            {
                var colorfulnessPricePerFormat = _db.ColorfulnessPricePerFormats.FirstOrDefault(
                    x => x.Format.Id == printingVM.FormatId && x.Colorfulness.Id == printingVM.ColorfulnessId);

                if (printingVM.NewCost.Value > 0)
                {
                    colorfulnessPricePerFormat.Price.Cost = printingVM.NewCost.Value;
                    _db.SaveChanges();    
                }
                
            }
            ViewBag.Format = new SelectList(_db.Formats, "Id", "Name");
            ViewBag.FormingType = new SelectList(_db.FormingTypes, "Id", "Name");
            ViewBag.Colorfulness = new SelectList(_db.Colorfulnesses, "Id", "Name");
            return View();
        }
    }
}