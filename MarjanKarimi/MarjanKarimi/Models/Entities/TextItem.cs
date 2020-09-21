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
    public class TextItem : BaseEntity
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "فیلد {0} اجباری می باشد.")]
        public string Title { get; set; }
       

        [Display(Name = "تصویر")]
        public string ImageUrl { get; set; }


        [Display(Name = "متن")]
        [DataType(DataType.Html)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        [Required(ErrorMessage = "فیلد {0} اجباری می باشد.")]
        public string Body { get; set; }

        [Display(Name = "پارامتر url")]
        public string UrlParam { get; set; }
        public Guid TextItemTypeId { get; set; }
        public virtual TextItemType TextItemType { get; set; }


        internal class Configuration : EntityTypeConfiguration<TextItem>
        {
            public Configuration()
            {
                HasRequired(p => p.TextItemType)
                    .WithMany(j => j.TextItems)
                    .HasForeignKey(p => p.TextItemTypeId);
            }
        }
    }
}