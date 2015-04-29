using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCandEntityFrameworkPractice.DataAccess;
using MVCandEntityFrameworkPractice.Models.Domain;

namespace MVCandEntityFrameworkPractice.Views
{
    public class CardsController : Controller
    {
        private ElectronicAccessContext db = new ElectronicAccessContext();

        // GET: Cards
        public ActionResult Index()
        {
            var cards = db.Cards.Include(c => c.Catalog).Include(c => c.Holder);
            return View(cards.ToList());
        }

        // GET: Cards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = db.Cards.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        // GET: Cards/Create
        public ActionResult Create()
        {
            ViewBag.CatalogId = new SelectList(db.Catalogs, "Id", "DisplayName");
            ViewBag.HolderId = new SelectList(db.Designees, "Id", "LastName");
            return View();
        }

        // POST: Cards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CatalogId,HolderId,Iso,Pin,StartDate,EndDate,CreatedDate,ModifiedDate,StatusType,StatusDate")] Card card)
        {
            if (ModelState.IsValid)
            {
                db.Cards.Add(card);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CatalogId = new SelectList(db.Catalogs, "Id", "DisplayName", card.CatalogId);
            ViewBag.HolderId = new SelectList(db.Designees, "Id", "LastName", card.HolderId);
            return View(card);
        }

        // GET: Cards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = db.Cards.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatalogId = new SelectList(db.Catalogs, "Id", "DisplayName", card.CatalogId);
            ViewBag.HolderId = new SelectList(db.Designees, "Id", "LastName", card.HolderId);
            return View(card);
        }

        // POST: Cards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CatalogId,HolderId,Iso,Pin,StartDate,EndDate,CreatedDate,ModifiedDate,StatusType,StatusDate")] Card card)
        {
            if (ModelState.IsValid)
            {
                db.Entry(card).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CatalogId = new SelectList(db.Catalogs, "Id", "DisplayName", card.CatalogId);
            ViewBag.HolderId = new SelectList(db.Designees, "Id", "LastName", card.HolderId);
            return View(card);
        }

        // GET: Cards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = db.Cards.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Card card = db.Cards.Find(id);
            db.Cards.Remove(card);
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
