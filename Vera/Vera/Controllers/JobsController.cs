using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Vera.Domain;
using Vera.Domain.Entity;
using Vera.Models;
using WebGrease;

namespace Vera.Controllers
{
    [Authorize(Roles = "admin")]
    public class JobsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Jobs
        public ActionResult Index()
        {
            var dbItems = db.Jobs.ToList();
            var jobsList = new List<JobViewModel>();
            foreach (var job in dbItems)
            {
                jobsList.Add(MappingJobToJobViewModel(job));
            }
            return View(jobsList);
        }

        //// GET: Jobs/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Job job = db.Jobs.Find(id);
        //    if (job == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(job);
        //}

        // GET: Jobs/Create
        public ActionResult Create()
        {
            ViewBag.Dependencies = new SelectList(db.JobDependencies, "Id", "Name");
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,JobTitle")] Job job, decimal? cost, int dependencyId)
        {
            if (ModelState.IsValid && cost != null)
            {
                var price = new Price()
                {
                    Cost = cost.Value,
                    Currency = db.Currencies.FirstOrDefault(x => x.Name == "RUB")
                };
                job.Pay = price;
                job.Dependency = db.JobDependencies.FirstOrDefault(x => x.Id == dependencyId);
                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(job);
        }

        // GET: Jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(MappingJobToJobViewModel(job));
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,JobTitle")] Job job)
        public ActionResult Edit(JobViewModel jobViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dbJob = db.Jobs.Find(jobViewModel.Id);
                    dbJob.Pay.Cost = jobViewModel.Cost;

                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    //ignored
                }
                return RedirectToAction("Index");
            }
            return View(jobViewModel);
        }

        //// GET: Jobs/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Job job = db.Jobs.Find(id);
        //    if (job == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(job);
        //}

        //// POST: Jobs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Job job = db.Jobs.Find(id);
        //    db.Jobs.Remove(job);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private JobViewModel MappingJobToJobViewModel(Job job)
        {
            return Mapper.Map<Job, JobViewModel>(job);
        }
    }
}
