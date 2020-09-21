using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class BlogGroup : BaseEntity
    {
        [Display(Name = "گروه مطالب")]
        [Required(ErrorMessage = "فیلد {0} اجباری می باشد.")]
        public string Title { get; set; }
        [Display(Name = "خلاصه")]
        [Required(ErrorMessage = "فیلد {0} اجباری می باشد.")]
        public string Summery { get; set; }
        [Display(Name = "تصویر")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "فیلد {0} اجباری می باشد.")]
        [Display(Name = "پارامتر url")]
        public string UrlParam { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }

    }
}