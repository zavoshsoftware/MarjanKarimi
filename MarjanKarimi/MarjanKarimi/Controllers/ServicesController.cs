using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Helpers;
using Models;
using ViewModels;

namespace MarjanKarimi.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ServicesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            var services = db.Services.Include(s => s.ServiceGroup).Where(s => s.IsDeleted == false).OrderByDescending(s => s.CreationDate);
            return View(services.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.ServiceGroupId = new SelectList(db.ServiceGroups, "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Service service, HttpPostedFileBase fileUpload, HttpPostedFileBase homeFileUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed

                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/Service/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    service.ImageUrl = newFilenameUrl;
                }

                if (homeFileUpload != null)
                {
                    string filename = Path.GetFileName(homeFileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/Service/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    homeFileUpload.SaveAs(physicalFilename);

                    service.HomeImageUrl = newFilenameUrl;
                }
                #endregion
                service.IsDeleted = false;
                service.CreationDate = DateTime.Now;
                service.Id = Guid.NewGuid();
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ServiceGroupId = new SelectList(db.ServiceGroups, "Id", "Title", service.ServiceGroupId);
            return View(service);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceGroupId = new SelectList(db.ServiceGroups, "Id", "Title", service.ServiceGroupId);
            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Service service, HttpPostedFileBase fileUpload, HttpPostedFileBase homeFileUpload)
        {
            if (ModelState.IsValid)
            {
                #region Upload and resize image if needed

                if (fileUpload != null)
                {
                    string filename = Path.GetFileName(fileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/Service/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    service.ImageUrl = newFilenameUrl;
                }

                if (homeFileUpload != null)
                {
                    string filename = Path.GetFileName(homeFileUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = "/Uploads/Service/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    homeFileUpload.SaveAs(physicalFilename);

                    service.HomeImageUrl = newFilenameUrl;
                }
                #endregion
                service.IsDeleted = false;
                service.LastModifiedDate = DateTime.Now;
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServiceGroupId = new SelectList(db.ServiceGroups, "Id", "Title", service.ServiceGroupId);
            return View(service);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Service service = db.Services.Find(id);
            service.IsDeleted = true;
            service.DeletionDate = DateTime.Now;

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

        BaseViewModelHelper baseHelper = new BaseViewModelHelper();

        //[AllowAnonymous]
        //[Route("service/{serviceGroupUrlParam}")]
        //public ActionResult List(string serviceGroupUrlParam)
        //{
        //    ServiceListViewModel services = new ServiceListViewModel()
        //    {
        //        Services = db.Services.Include(s => s.ServiceGroup).Where(s => s.ServiceGroup.UrlParam == serviceGroupUrlParam && s.IsDeleted == false).OrderByDescending(s => s.CreationDate).ToList(),
        //        ServiceGroups = baseHelper.GetMenuSerivceGroup(),
        //        FooterTextItems = baseHelper.GetFooterTextItems(),
        //        HeaderTextItems = baseHelper.GetHeaderTextItems(),
        //        ServiceGroup = db.ServiceGroups.FirstOrDefault(c => c.UrlParam == serviceGroupUrlParam)
        //    };

        //    return View(services);
        //}



        [AllowAnonymous]
        [Route("service/{serviceUrlParam}")]
        public ActionResult Details(string serviceUrlParam)
        {
            Service service = db.Services.FirstOrDefault(c => c.UrlParam == serviceUrlParam);

            if (service == null)
                return RedirectPermanent("/");
            ServiceDetailViewModel serviceDetail = new ServiceDetailViewModel()
            {
               
                Service = service,

                SidebarServices = db.Services.Where(c => c.IsDeleted == false && c.IsActive && c.Id!=service.Id).OrderBy(c => c.Order).ToList(),

                

            };

            return View(serviceDetail);
        }
    }
}
