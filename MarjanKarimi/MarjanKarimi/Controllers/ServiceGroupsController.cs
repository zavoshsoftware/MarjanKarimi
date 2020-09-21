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
    [Authorize(Roles = "Administrator")]
    public class ServiceGroupsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index(Guid? id)
        {
            ViewBag.parent = null;

            List<ServiceGroup> serviceGroups;

            if (id == null)
            {
                serviceGroups = db.ServiceGroups.Include(t => t.Parent).Where(t => t.IsDeleted == false && t.ParentId == null).OrderByDescending(t => t.CreationDate).ToList();

            }
            else
            {
                serviceGroups = db.ServiceGroups.Include(t => t.Parent).Where(t => t.IsDeleted == false && t.ParentId == id).OrderByDescending(t => t.CreationDate).ToList();
                ViewBag.parent = id;
            }

            return View(serviceGroups);
        }

        public ActionResult Create(Guid? id)
        {
            if (id != null)
            {
                ViewBag.parent = id;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ServiceGroup serviceGroup, Guid? id)
        {
            if (ModelState.IsValid)
            {
				serviceGroup.IsDeleted=false;
				serviceGroup.CreationDate= DateTime.Now; 
                serviceGroup.Id = Guid.NewGuid();

                if (id != null)
                    serviceGroup.ParentId = id.Value;

                db.ServiceGroups.Add(serviceGroup);
                db.SaveChanges();

                if (id != null)
                    return RedirectToAction("Index", new { id = id });

                return RedirectToAction("Index");
            }

            return View(serviceGroup);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceGroup serviceGroup = db.ServiceGroups.Find(id);
            if (serviceGroup == null)
            {
                return HttpNotFound();
            }
            if (serviceGroup.ParentId != null)
                ViewBag.parent = serviceGroup.ParentId;
            return View(serviceGroup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( ServiceGroup serviceGroup)
        {
            if (ModelState.IsValid)
            {
				serviceGroup.IsDeleted = false;
				serviceGroup.LastModifiedDate = DateTime.Now;
                db.Entry(serviceGroup).State = EntityState.Modified;
                db.SaveChanges();
                if (serviceGroup.ParentId != null)
                    return RedirectToAction("Index", new { id = serviceGroup.ParentId });

                return RedirectToAction("Index");
            }
            return View(serviceGroup);
        }

        // GET: ServiceGroups/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceGroup serviceGroup = db.ServiceGroups.Find(id);
            if (serviceGroup == null)
            {
                return HttpNotFound();
            }

            if (serviceGroup.ParentId != null)
                ViewBag.parent = serviceGroup.ParentId;

            return View(serviceGroup);
        }

        // POST: ServiceGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ServiceGroup serviceGroup = db.ServiceGroups.Find(id);
			serviceGroup.IsDeleted=true;
			serviceGroup.DeletionDate=DateTime.Now;
 
            db.SaveChanges();

            if (serviceGroup.ParentId != null)
                return RedirectToAction("Index", new { id = serviceGroup.ParentId });

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
