using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Models;

namespace Models
{
    public class TextItemType : BaseEntity
    {
        public TextItemType()
        {
            TextItems = new List<TextItem>();
        }
        [Display(Name ="عنوان")]
        [MaxLength(200, ErrorMessage = "تعداد کاراکتر {0} نباید بیشتر از {1} باشد.")]
        public string Title { get; set; }

        [Display(Name = "پارامتر url")]
        public string UrlParam { get; set; }

        public ICollection<TextItem> TextItems { get; set; }
    }
}