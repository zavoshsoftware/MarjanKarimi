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
    public class ServiceTopicsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: ServiceTopics
        public ActionResult Index(Guid id)
        {
            var serviceTopics = db.ServiceTopics.Include(s => s.Service).Where(s=>s.ServiceId==id&& s.IsDeleted==false).OrderByDescending(s=>s.CreationDate);
            return View(serviceTopics.ToList());
        }
       
        public ActionResult Create(Guid id)
        {
            ViewBag.ServiceId = id;
            return View();
        }

         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ServiceTopic serviceTopic, Guid id)
        {
            if (ModelState.IsValid)
            {
                serviceTopic.ServiceId = id;
				serviceTopic.IsDeleted=false;
				serviceTopic.CreationDate= DateTime.Now; 
                serviceTopic.Id = Guid.NewGuid();
                db.ServiceTopics.Add(serviceTopic);
                db.SaveChanges();
                return RedirectToAction("Index",new{id=id});
            }

            ViewBag.ServiceId =id;
            return View(serviceTopic);
        }

        // GET: ServiceTopics/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceTopic serviceTopic = db.ServiceTopics.Find(id);
            if (serviceTopic == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceId = serviceTopic.ServiceId;
            return View(serviceTopic);
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ServiceTopic serviceTopic)
        {
            if (ModelState.IsValid)
            {
				serviceTopic.IsDeleted = false;
				serviceTopic.LastModifiedDate = DateTime.Now;
                db.Entry(serviceTopic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServiceId = serviceTopic.ServiceId;
            return View(serviceTopic);
        }

         
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceTopic serviceTopic = db.ServiceTopics.Find(id);
            if (serviceTopic == null)
            {
                return HttpNotFound();
            }
            return View(serviceTopic);
        }

        // POST: ServiceTopics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ServiceTopic serviceTopic = db.ServiceTopics.Find(id);
			serviceTopic.IsDeleted=true;
			serviceTopic.DeletionDate=DateTime.Now;
 
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
