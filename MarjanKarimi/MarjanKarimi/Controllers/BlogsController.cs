using System;
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
    public class BlogsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            var blogs = db.Blogs.Include(b => b.BlogGroup).Where(b=>b.IsDeleted==false).OrderByDescending(b=>b.CreationDate);
            return View(blogs.ToList());
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        public ActionResult Create()
        {
            ViewBag.BlogGroupId = new SelectList(db.BlogGroups, "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Blog blog, HttpPostedFileBase fileUpload)
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

                    newFilenameUrl = "/Uploads/Blog/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    blog.ImageUrl = newFilenameUrl;
                }
                #endregion
                blog.IsDeleted=false;
				blog.CreationDate= DateTime.Now; 
                blog.Id = Guid.NewGuid();
                db.Blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BlogGroupId = new SelectList(db.BlogGroups, "Id", "Title", blog.BlogGroupId);
            return View(blog);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogGroupId = new SelectList(db.BlogGroups, "Id", "Title", blog.BlogGroupId);
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Blog blog, HttpPostedFileBase fileUpload)
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

                    newFilenameUrl = "/Uploads/Blog/" + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);

                    fileUpload.SaveAs(physicalFilename);

                    blog.ImageUrl = newFilenameUrl;
                }
                #endregion
                blog.IsDeleted = false;
				blog.LastModifiedDate = DateTime.Now;
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlogGroupId = new SelectList(db.BlogGroups, "Id", "Title", blog.BlogGroupId);
            return View(blog);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Blog blog = db.Blogs.Find(id);
			blog.IsDeleted=true;
			blog.DeletionDate=DateTime.Now;
 
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

        [AllowAnonymous]
        [Route("blog")]
        public ActionResult List()
        {
            BlogListViewModel blogs = new BlogListViewModel()
            {
                Blogs = db.Blogs .Where(s => s.IsDeleted == false).OrderByDescending(s => s.CreationDate).ToList(),
                
            };

            return View(blogs);
        }



        [AllowAnonymous]
        [Route("blog/{blogUrlParam}")]
        public ActionResult Details(string blogUrlParam)
        {
            Blog blog = db.Blogs.FirstOrDefault(c => c.UrlParam == blogUrlParam);

            if (blog == null)
                return RedirectPermanent("/");

         

            BlogDetailViewModel blogDetail = new BlogDetailViewModel()
            {
                
                Blog = blog,

                RecentBlogs = db.Blogs.Where(c => c.IsDeleted == false && c.IsActive)
                    .OrderByDescending(c => c.CreationDate).Take(4).ToList(),

                PopularBlogs = db.Blogs.Where(c => c.IsDeleted == false && c.IsActive).OrderByDescending(c => c.Visit).Take(4).ToList(),
                Comments = db.BlogComments.Where(c => c.IsDeleted == false && c.IsActive && c.BlogId == blog.Id).ToList()

            };

            return View(blogDetail);
        }
    }
}
