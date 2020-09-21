using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;
using System.IO;
using ViewModels;

namespace MarjanKarimi.Controllers
{
    public class GalleriesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Galleries
        public ActionResult Index()
        {
            var galleries = db.Galleries.Include(g => g.GalleryGroup).Where(g => g.IsDeleted == false).OrderByDescending(g => g.CreationDate);
            return View(galleries.ToList());
        }



        // GET: Galleries/Create
        public ActionResult Create()
        {
            ViewBag.GalleryGroupId = new SelectList(db.GalleryGroups, "Id", "Title");
            return View();
        }

        // POST: Galleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Gallery gallery, HttpPostedFileBase fileUpload,HttpPostedFileBase thumUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed

                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/Gallery/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    gallery.ImageUrl = newFilenameUrl;
                }
                if (thumUpload != null)
                {
                    string filename = Path.GetFileName(thumUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/Gallery/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    thumUpload.SaveAs(physicalFilename);

                    gallery.ThumbImageUrl = newFilenameUrl;
                }
                #endregion
                gallery.IsDeleted = false;
                gallery.CreationDate = DateTime.Now;
                gallery.Id = Guid.NewGuid();
                db.Galleries.Add(gallery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GalleryGroupId = new SelectList(db.GalleryGroups, "Id", "Title", gallery.GalleryGroupId);
            return View(gallery);
        }

        // GET: Galleries/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            ViewBag.GalleryGroupId = new SelectList(db.GalleryGroups, "Id", "Title", gallery.GalleryGroupId);
            return View(gallery);
        }

        // POST: Galleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Gallery gallery, HttpPostedFileBase fileUpload,HttpPostedFileBase thumUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed

                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/Gallery/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    gallery.ImageUrl = newFilenameUrl;
                }
                if (thumUpload != null)
                {
                    string filename = Path.GetFileName(thumUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/Gallery/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    thumUpload.SaveAs(physicalFilename);

                    gallery.ThumbImageUrl = newFilenameUrl;
                }
                #endregion
                gallery.IsDeleted = false;
                gallery.LastModifiedDate = DateTime.Now;
                db.Entry(gallery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GalleryGroupId = new SelectList(db.GalleryGroups, "Id", "Title", gallery.GalleryGroupId);
            return View(gallery);
        }

        // GET: Galleries/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: Galleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Gallery gallery = db.Galleries.Find(id);
            gallery.IsDeleted = true;
            gallery.DeletionDate = DateTime.Now;

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

        [Route("gallery")]
        public ActionResult List()
        {

            GalleryListViewModel viewModel = new GalleryListViewModel()
            {
                FilterList = db.GalleryGroups.Where(current => current.IsDeleted == false && current.IsActive == true).ToList(),
                GelleryList = db.Galleries.Where(current => current.IsDeleted == false && current.IsActive == true).ToList()
            };
            return View(viewModel);
        }
    }
}
