using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;

namespace MarjanKarimi.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class TextItemsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index(Guid id)
        {
            var textItems = db.TextItems.Include(t => t.TextItemType).Where(t=>t.TextItemTypeId==id&& t.IsDeleted==false).OrderByDescending(t=>t.CreationDate);
            return View(textItems.ToList());
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TextItem textItem = db.TextItems.Find(id);
            if (textItem == null)
            {
                return HttpNotFound();
            }
            return View(textItem);
        }

        public ActionResult Create(Guid id)
        {
            ViewBag.TextItemTypeId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TextItem textItem,Guid id, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = string.Empty;
                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/Text/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    textItem.ImageUrl = newFilenameUrl;
                }
                #endregion
                textItem.IsDeleted=false;
				textItem.CreationDate= DateTime.Now;
                textItem.TextItemTypeId = id;

                textItem.Id = Guid.NewGuid();
                db.TextItems.Add(textItem);
                db.SaveChanges();
                return RedirectToAction("Index",new{id=id});
            }

            ViewBag.TextItemTypeId = id;
            return View(textItem);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TextItem textItem = db.TextItems.Find(id);
            if (textItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.TextItemTypeId = textItem.TextItemTypeId;
            return View(textItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( TextItem textItem, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed
                string newFilenameUrl = string.Empty;
                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    newFilenameUrl = "/Uploads/Text/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    textItem.ImageUrl = newFilenameUrl;
                }
                #endregion

                textItem.IsDeleted = false;
				textItem.LastModifiedDate = DateTime.Now;
                db.Entry(textItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index",new{id=textItem.TextItemTypeId});
            }
            ViewBag.TextItemTypeId = textItem.TextItemTypeId;
            return View(textItem);
        }

        // GET: TextItems/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TextItem textItem = db.TextItems.Find(id);
            if (textItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.TextItemTypeId = textItem.TextItemTypeId;

            return View(textItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TextItem textItem = db.TextItems.Find(id);
			textItem.IsDeleted=true;
			textItem.DeletionDate=DateTime.Now;
 
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
