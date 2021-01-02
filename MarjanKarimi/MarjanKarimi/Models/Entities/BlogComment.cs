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
    public class BlogComment:BaseEntity
    {
        [Display(Name = "نام")]
        [StringLength(200, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string Name { get; set; }

        [Display(Name = "ایمیل")]
        [StringLength(256, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string Email { get; set; }

        [Display(Name = "نظر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string Message { get; set; }

        [Display(Name = "پاسخ")]
        [DataType(DataType.Html)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        public string Response { get; set; }

        public DateTime? ResponseDate { get; set; }
        public Guid BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        internal class Configuration : EntityTypeConfiguration<BlogComment>
        {
            public Configuration()
            {
                HasRequired(p => p.Blog)
                    .WithMany(j => j.BlogComments)
                    .HasForeignKey(p => p.BlogId);
            }
        }
    }
}