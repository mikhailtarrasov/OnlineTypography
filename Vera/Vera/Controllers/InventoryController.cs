using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Vera.Domain;
using Vera.Domain.Entity;

namespace Vera.Controllers
{
    public class InventoryController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Inventory
        public ActionResult Index()
        {
            return View(db.Materials.ToList());
        }

        // GET: Inventory/Create
        public ActionResult Create()
        {
            ViewBag.MaterialTypes = new SelectList(db.MaterialTypes, "Id", "TypeName");
            ViewBag.Currencies = new SelectList(db.Currencies, "Id", "Name");
            return View();
        }

        // POST: Inventory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Material material, decimal cost, int materialTypeId, int currencyId)
        {
            if (ModelState.IsValid)
            {
                material.Type = db.MaterialTypes.Find(materialTypeId);
                var price = new Price()
                {
                    Cost = cost,
                    Currency = db.Currencies.Find(currencyId)
                };
                material.Price = price;
                db.Materials.Add(material);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(material);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
