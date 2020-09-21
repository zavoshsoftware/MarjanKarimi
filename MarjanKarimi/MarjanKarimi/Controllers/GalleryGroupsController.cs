using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;

namespace MarjanKarimi.Controllers
{
    public class GalleryGroupsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: GalleryGroups
        public ActionResult Index()
        {
            return View(db.GalleryGroups.Where(a=>a.IsDeleted==false).OrderByDescending(a=>a.CreationDate).ToList());
        }

        // GET: GalleryGroups/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryGroup galleryGroup = db.GalleryGroups.Find(id);
            if (galleryGroup == null)
            {
                return HttpNotFound();
            }
            return View(galleryGroup);
        }

        // GET: GalleryGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GalleryGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,UrlParam,Order,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] GalleryGroup galleryGroup)
        {
            if (ModelState.IsValid)
            {
				galleryGroup.IsDeleted=false;
				galleryGroup.CreationDate= DateTime.Now; 
                galleryGroup.Id = Guid.NewGuid();
                db.GalleryGroups.Add(galleryGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(galleryGroup);
        }

        // GET: GalleryGroups/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryGroup galleryGroup = db.GalleryGroups.Find(id);
            if (galleryGroup == null)
            {
                return HttpNotFound();
            }
            return View(galleryGroup);
        }

        // POST: GalleryGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,UrlParam,Order,IsActive,CreationDate,LastModifiedDate,IsDeleted,DeletionDate,Description")] GalleryGroup galleryGroup)
        {
            if (ModelState.IsValid)
            {
				galleryGroup.IsDeleted = false;
				galleryGroup.LastModifiedDate = DateTime.Now;
                db.Entry(galleryGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(galleryGroup);
        }

        // GET: GalleryGroups/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryGroup galleryGroup = db.GalleryGroups.Find(id);
            if (galleryGroup == null)
            {
                return HttpNotFound();
            }
            return View(galleryGroup);
        }

        // POST: GalleryGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            GalleryGroup galleryGroup = db.GalleryGroups.Find(id);
			galleryGroup.IsDeleted=true;
			galleryGroup.DeletionDate=DateTime.Now;
 
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
