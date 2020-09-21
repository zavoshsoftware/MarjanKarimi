using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Models
{
    public class Service : BaseEntity
    {
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "پارامتر صفحه")]
        public string UrlParam { get; set; }

        [Display(Name = "تصویر اصلی داخلی")]
        public string ImageUrl { get; set; }


        [Display(Name = "تصویر اصلی اصلی")]
        public string HomeImageUrl { get; set; }
        [Display(Name = "خلاصه")]
        public string Summery { get; set; }
        [Display(Name = "متن صفحه")]
        [DataType(DataType.Html)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        public string Body { get; set; }

        [Display(Name = "نمایش در صفحه اصلی")]
        public bool IsSpecial { get; set; }
        public Guid ServiceGroupId { get; set; }
        public virtual ServiceGroup ServiceGroup { get; set; }
        [Display(Name = "اولویت نمایش")]
        public int? Order { get; set; }

        internal class Configuration : EntityTypeConfiguration<Service>
        {
            public Configuration()
            {
                HasRequired(p => p.ServiceGroup)
                    .WithMany(j => j.Services)
                    .HasForeignKey(p => p.ServiceGroupId);
            }
        }
    }
}