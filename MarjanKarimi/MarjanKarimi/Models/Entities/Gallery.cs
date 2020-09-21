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
    public class Gallery:BaseEntity
    {
        [Display(Name="عنوان")]
        public string Title { get; set; }

        [Display(Name="تصویر")]
        public string ImageUrl { get; set; }

        [Display(Name= "تصویر thumbnail")]
        public string ThumbImageUrl { get; set; }

        [Required(ErrorMessage = "فیلد {0} اجباری می باشد.")]
        public Guid GalleryGroupId { get; set; }

        public virtual GalleryGroup GalleryGroup { get; set; }

        internal class Configuration : EntityTypeConfiguration<Gallery>
        {
            public Configuration()
            {
                HasRequired(p => p.GalleryGroup)
                    .WithMany(j => j.Galleries)
                    .HasForeignKey(p => p.GalleryGroupId);
            }
        }
    }
}