using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class ServiceFaq:BaseEntity
    {
        public int? Order { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public Guid ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}