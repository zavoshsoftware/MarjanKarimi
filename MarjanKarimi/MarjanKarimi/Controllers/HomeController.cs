using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace MarjanKarimi.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            HomeViewModel viewModel = new HomeViewModel()
            {
                GalleryImages = db.Galleries.Where(current=>current.IsActive && !current.IsDeleted).OrderByDescending(current=>current.CreationDate).Take(6).ToList(),
                HomeServices = db.Services.Where(current => current.IsActive && !current.IsDeleted).OrderByDescending(current => current.CreationDate).Take(6).ToList(),
                HomeBlogs = db.Blogs.Where(current => current.IsActive && !current.IsDeleted).OrderByDescending(current => current.CreationDate).Take(2).ToList(),
                Sliders = db.Sliders.Where(current => current.IsActive && !current.IsDeleted).OrderByDescending(current => current.CreationDate).ToList(),
                WhyChooseus = db.TextItems.Where(current => current.IsActive && !current.IsDeleted && current.TextItemType.UrlParam.ToLower() == "homewhymarjan").ToList(),
                Numbers = db.TextItems.Where(current => current.IsActive && !current.IsDeleted && current.TextItemType.UrlParam.ToLower() == "homenumbers").ToList(),

            };
            return View(viewModel);
        }
        public ActionResult Service()
        {
            return View();
        }
        [Route("contact")]
        public ActionResult Contact()
        {
            ContactViewModel viewModel = new ContactViewModel()
            {
                ContactTextItems = db.TextItems.Where(current => current.IsActive && !current.IsDeleted && current.TextItemType.UrlParam.ToLower() == "contactdesc").ToList(),
            };
            return View(viewModel);
        }
        [Route("about")]
        public ActionResult About()
        {
            AboutViewModel viewModel = new AboutViewModel()
            {
                AboutTextItem = db.TextItems.Where(current=>current.IsActive && ! current.IsDeleted &&  current.UrlParam.ToLower() == "aboutdesc").FirstOrDefault(),
                WhyChooseus = db.TextItems.Where(current => current.IsActive && !current.IsDeleted &&  current.TextItemType.UrlParam.ToLower() == "whymarjanabout").ToList(),
                Numbers = db.TextItems.Where(current => current.IsActive && !current.IsDeleted && current.TextItemType.UrlParam.ToLower() == "aboutnumbers").ToList(),
                Contact = db.TextItems.Where(current => current.IsActive && !current.IsDeleted && current.UrlParam.ToLower() == "aboutcontact").FirstOrDefault(),

            };
            return View(viewModel);
        }
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult Gallery()
        {
            return View();
        }
    }
}