using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DeliveryApp.Models;

namespace DeliveryApp.Controllers
{
    public class EstablishmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Establishments
        public ActionResult Index()
        {
            var establishments = db.Establishments.Include(e => e.BusinessType);
            return View(establishments.ToList());
        }

        // GET: Establishments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Establishment establishment = db.Establishments.Find(id);
            if (establishment == null)
            {
                return HttpNotFound();
            }
            return View(establishment);
        }

        // GET: Establishments/Create
        public ActionResult Create()
        {
            ViewBag.BusinessTypeId = new SelectList(db.BusinessTypes, "BusinessTypeId", "BusinessTypeName");
            return View();
        }

        // POST: Establishments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EstablishmentId,EstablishmentName,EstablishmentWebsite,BusinessTypeId")] Establishment establishment)
        {
            if (ModelState.IsValid)
            {
                db.Establishments.Add(establishment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusinessTypeId = new SelectList(db.BusinessTypes, "BusinessTypeId", "BusinessTypeName", establishment.BusinessTypeId);
            return View(establishment);
        }

        // GET: Establishments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Establishment establishment = db.Establishments.Find(id);
            if (establishment == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessTypeId = new SelectList(db.BusinessTypes, "BusinessTypeId", "BusinessTypeName", establishment.BusinessTypeId);
            return View(establishment);
        }

        // POST: Establishments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EstablishmentId,EstablishmentName,EstablishmentWebsite,BusinessTypeId")] Establishment establishment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(establishment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusinessTypeId = new SelectList(db.BusinessTypes, "BusinessTypeId", "BusinessTypeName", establishment.BusinessTypeId);
            return View(establishment);
        }

        // GET: Establishments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Establishment establishment = db.Establishments.Find(id);
            if (establishment == null)
            {
                return HttpNotFound();
            }
            return View(establishment);
        }

        // POST: Establishments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Establishment establishment = db.Establishments.Find(id);
            db.Establishments.Remove(establishment);
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
