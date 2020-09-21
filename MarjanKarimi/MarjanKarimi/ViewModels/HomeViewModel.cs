using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class HomeViewModel:_BaseViewModel
    {
        public List<Slider> Sliders { get; set; }
        public List<Gallery> GalleryImages { get; set; }
        public List<Service> HomeServices { get; set; }
        public List<Blog> HomeBlogs { get; set; }
        public List<TextItem> WhyChooseus { get; set; }
        public List<TextItem> Numbers { get; set; }

    }
}