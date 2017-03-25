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

        // GET: Inventory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material material = db.Materials.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }
            return View(material);
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

        // GET: Inventory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material material = db.Materials.Find(id);
            ViewBag.Currencies = new SelectList(db.Currencies, "Id", "Name");
            if (material == null)
            {
                return HttpNotFound();
            }
            return View(material);
        }

        // POST: Inventory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Type,Balance")] Material material, int currencyId)
        {
            if (ModelState.IsValid)
            {
                var price = db.Prices.Find(material.Price.Id);
                price.Cost = material.Price.Cost;
                price.Currency = db.Currencies.Find(currencyId);

                material.Price = price;
                db.Entry(material).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(material);
        }

        // GET: Inventory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material material = db.Materials.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }
            return View(material);
        }

        // POST: Inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Material material = db.Materials.Find(id);
            Price materialPrice = db.Prices.Find(material.Price.Id);
            db.Prices.Remove(materialPrice);
            db.Materials.Remove(material);
            db.SaveChanges();
            return RedirectToAction("Index");
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
