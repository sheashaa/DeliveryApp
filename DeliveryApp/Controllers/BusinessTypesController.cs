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
    public class BusinessTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BusinessTypes
        public ActionResult Index()
        {
            return View(db.BusinessTypes.ToList());
        }

        // GET: BusinessTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessType businessType = db.BusinessTypes.Find(id);
            if (businessType == null)
            {
                return HttpNotFound();
            }
            return View(businessType);
        }

        // GET: BusinessTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BusinessTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusinessTypeId,BusinessTypeName")] BusinessType businessType)
        {
            if (ModelState.IsValid)
            {
                db.BusinessTypes.Add(businessType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(businessType);
        }

        // GET: BusinessTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessType businessType = db.BusinessTypes.Find(id);
            if (businessType == null)
            {
                return HttpNotFound();
            }
            return View(businessType);
        }

        // POST: BusinessTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusinessTypeId,BusinessTypeName")] BusinessType businessType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(businessType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(businessType);
        }

        // GET: BusinessTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessType businessType = db.BusinessTypes.Find(id);
            if (businessType == null)
            {
                return HttpNotFound();
            }
            return View(businessType);
        }

        // POST: BusinessTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BusinessType businessType = db.BusinessTypes.Find(id);
            db.BusinessTypes.Remove(businessType);
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
