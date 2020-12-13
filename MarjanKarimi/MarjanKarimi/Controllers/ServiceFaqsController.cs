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
    public class ServiceFaqsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: ServiceFaqs
        public ActionResult Index(Guid id)
        {
            var serviceFaqs = db.ServiceFaqs.Include(s => s.Service).Where(s=>s.ServiceId==id&& s.IsDeleted==false).OrderByDescending(s=>s.CreationDate);
            return View(serviceFaqs.ToList());
        }
 
        public ActionResult Create(Guid id)
        {
            ViewBag.ServiceId = id;
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ServiceFaq serviceFaq, Guid id)
        {
            if (ModelState.IsValid)
            {
                serviceFaq.ServiceId = id;
				serviceFaq.IsDeleted=false;
				serviceFaq.CreationDate= DateTime.Now; 
                serviceFaq.Id = Guid.NewGuid();
                db.ServiceFaqs.Add(serviceFaq);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = id });
            }

            ViewBag.ServiceId = id;
            return View(serviceFaq);
        }

        // GET: ServiceFaqs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceFaq serviceFaq = db.ServiceFaqs.Find(id);
            if (serviceFaq == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServiceId = serviceFaq.ServiceId;
            return View(serviceFaq);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ServiceFaq serviceFaq)
        {
            if (ModelState.IsValid)
            {
				serviceFaq.IsDeleted = false;
				serviceFaq.LastModifiedDate = DateTime.Now;
                db.Entry(serviceFaq).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index",new{id=serviceFaq.ServiceId});
            }
            ViewBag.ServiceId = serviceFaq.ServiceId;
            return View(serviceFaq);
        }

        // GET: ServiceFaqs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceFaq serviceFaq = db.ServiceFaqs.Find(id);
            if (serviceFaq == null)
            {
                return HttpNotFound();
            }
            return View(serviceFaq);
        }

        // POST: ServiceFaqs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ServiceFaq serviceFaq = db.ServiceFaqs.Find(id);
			serviceFaq.IsDeleted=true;
			serviceFaq.DeletionDate=DateTime.Now;
 
            db.SaveChanges();
            return RedirectToAction("Index", new { id = serviceFaq.ServiceId });
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
