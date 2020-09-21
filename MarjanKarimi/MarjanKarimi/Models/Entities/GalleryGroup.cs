using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class GalleryGroup : BaseEntity
    {
        public GalleryGroup()
        {
            Galleries = new List<Gallery>();
        }
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "پارامتر url")]
        public string UrlParam { get; set; }

        [Display(Name = "اولویت نمایش")]
        public int? Order { get; set; }
        public virtual ICollection<Gallery> Galleries { get; set; }

    }
}