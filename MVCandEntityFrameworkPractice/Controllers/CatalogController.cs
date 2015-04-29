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
using PagedList;

namespace MVCandEntityFrameworkPractice.Controllers
{
    public class CatalogController : Controller
    {
        private ElectronicAccessContext db = new ElectronicAccessContext();

        // GET: Catalog
        //public ActionResult Index()
        //{
        //    return View(db.Catalogs.ToList());
        //}
        [Route("Catalogs/All")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DisplayNameSortParm = string.IsNullOrEmpty(sortOrder) ? "DisplayName_Desc" : "";
            ViewBag.StatusNameSortParm = sortOrder == "StatusName" ? "StatusName_Desc" : "StatusName";
            ViewBag.CreatedDateSortParm = sortOrder == "CreatedDate" ? "CreatedDate_Desc" : "CreatedDate";
            ViewBag.ModifiedDateSortParm = sortOrder == "ModifiedDate" ? "ModifiedDate_Desc" : "ModifiedDate";

            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var catalogs = from catalog in db.Catalogs
                           select catalog;

            if(!string.IsNullOrEmpty(searchString))            
                catalogs = catalogs.Where(catalog => catalog.DisplayName.Contains(searchString));            

            switch (sortOrder)
            {
                case "DisplayName_Desc":
                    catalogs = catalogs.OrderByDescending(catalog => catalog.DisplayName);
                    break;
                case "StatusName":
                    catalogs = catalogs.OrderBy(catalog => catalog.Status.DisplayName);
                    break;
                case "StatusName_Desc":
                    catalogs = catalogs.OrderByDescending(catalog => catalog.Status.DisplayName);
                    break;
                case "CreatedDate":
                    catalogs = catalogs.OrderBy(catalog => catalog.CreatedDate);
                    break;
                case "CreatedDate_Desc":
                    catalogs = catalogs.OrderByDescending(catalog => catalog.CreatedDate);
                    break;
                case "ModifiedDate":
                    catalogs = catalogs.OrderBy(catalog => catalog.ModifiedDate);
                    break;
                case "ModifiedDate_Desc":
                    catalogs = catalogs.OrderByDescending(catalog => catalog.ModifiedDate);
                    break;
                default:
                    catalogs = catalogs.OrderBy(catalog => catalog.DisplayName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(catalogs.ToPagedList(pageNumber, pageSize));

            //return View(catalogs.ToList());
        }

        // GET: Catalog/Details/5
        [Route("Catalog/{id:int}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalog catalog = db.Catalogs.Find(id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            return View(catalog);
        }

        // GET: Catalog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Catalog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DisplayName")] Catalog catalog)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Catalogs.Add(catalog);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes.  Try again, and if the problem persists see you system administrator.");
            }

            return View(catalog);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,DisplayName")] Catalog catalog)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Catalogs.Add(catalog);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(catalog);
        //}

        // GET: Catalog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalog catalog = db.Catalogs.Find(id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            return View(catalog);
        }

        // POST: Catalog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,DisplayName")] Catalog catalog)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(catalog).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(catalog);
        //}

        // POST: Catalog/Edit/5
        // Action modified to protect against overposting attacks.  For information sorce
        // see https://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/implementing-basic-crud-functionality-with-the-entity-framework-in-asp-net-mvc-application
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditCatalog(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var catalogToUpdate = db.Catalogs.Find(id);
            if (TryUpdateModel(catalogToUpdate, "",
               new string[] { "DisplayName" }))
            {
                try
                {
                    // IF changes were made then update the Modified Date property
                    if(db.Entry(catalogToUpdate).State == EntityState.Modified)                    
                        catalogToUpdate.ModifiedDate = System.DateTime.Now;
                    
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(catalogToUpdate);
        }

        // GET: Catalog/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if(saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed.  Try again, and if the problem persists see your system administrator.";
            }
            Catalog catalog = db.Catalogs.Find(id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            return View(catalog);
        }

        // POST: Catalog/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Catalog catalog = db.Catalogs.Find(id);
        //    db.Catalogs.Remove(catalog);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Catalog catalog = db.Catalogs.Find(id);
                db.Catalogs.Remove(catalog);
                db.SaveChanges();
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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

        // GET: Catalog/Deactivate/5
        public ActionResult Deactivate(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Deactivation failed.  Try again, and if the problem persists see your system administrator.";
            }
            Catalog catalog = db.Catalogs.Find(id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            return View(catalog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deactivate(int id)
        {
            try
            {
                Catalog catalog = db.Catalogs.Find(id);
                catalog.Status.Deactivate();
                db.SaveChanges();
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Deactivate", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
    }
}
