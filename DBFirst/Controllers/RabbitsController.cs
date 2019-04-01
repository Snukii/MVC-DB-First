using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DBFirst;

namespace DBFirst.Controllers
{
    public class RabbitsController : Controller
    {
        private RabbitsEntities db = new RabbitsEntities();

        // GET: Rabbits
        public ActionResult Index()
        {

            string searchtext = Request.Params["search"];

            List<Rabbit> result;

            if (String.IsNullOrEmpty(searchtext))
            {
                result = db.Rabbits.ToList();
            }
            else
            {
                result = db.Rabbits.Where(r => r.Owner1.Name.Contains(searchtext)).ToList();
            }


            return View(result);


        }

        // GET: Rabbits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rabbit rabbit = db.Rabbits.Find(id);
            if (rabbit == null)
            {
                return HttpNotFound();
            }
            return View(rabbit);
        }

        // GET: Rabbits/Create
        public ActionResult Create()
        {
            ViewBag.Owner = new SelectList(db.Owners, "OwnerID", "Name");
            return View();
        }

        // POST: Rabbits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RabbitID,Owner,Age")] Rabbit rabbit)
        {
            if (ModelState.IsValid)
            {
                db.Rabbits.Add(rabbit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Owner = new SelectList(db.Owners, "OwnerID", "Name", rabbit.Owner);
            return View(rabbit);
        }

        // GET: Rabbits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rabbit rabbit = db.Rabbits.Find(id);
            if (rabbit == null)
            {
                return HttpNotFound();
            }
            ViewBag.Owner = new SelectList(db.Owners, "OwnerID", "Name", rabbit.Owner);
            return View(rabbit);
        }

        // POST: Rabbits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RabbitID,Owner,Age")] Rabbit rabbit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rabbit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Owner = new SelectList(db.Owners, "OwnerID", "Name", rabbit.Owner);
            return View(rabbit);
        }

        // GET: Rabbits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rabbit rabbit = db.Rabbits.Find(id);
            if (rabbit == null)
            {
                return HttpNotFound();
            }
            return View(rabbit);
        }

        // POST: Rabbits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rabbit rabbit = db.Rabbits.Find(id);
            db.Rabbits.Remove(rabbit);
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
