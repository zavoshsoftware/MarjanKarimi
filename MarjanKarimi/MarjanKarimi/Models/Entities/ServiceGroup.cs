using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Models
{
    public class ServiceGroup:BaseEntity
    {
        public ServiceGroup()
        {
            Services=new List<Service>();
            ServiceGroups=new List<ServiceGroup>();
        }

        [Display(Name = "عنوان گروه سرویس")]
        public string Title { get; set; }

        [Display(Name = "پارامتر صفحه")]
        public string UrlParam { get; set; }

        public Guid? ParentId { get; set; }
        [Display(Name = "اولویت نمایش")]
        public int? Order { get; set; }

        [Display(Name = "تصویر")]
        public string ImageUrl { get; set; }

        [Display(Name = "خلاصه")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Summery { get; set; }

        [Display(Name = "متن صفحه")]
        [DataType(DataType.Html)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        public string Body { get; set; }

        public virtual ServiceGroup Parent { get; set; }

        public virtual ICollection<ServiceGroup> ServiceGroups { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}