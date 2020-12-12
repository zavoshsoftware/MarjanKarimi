using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class ServiceTopic:BaseEntity
    {
        public int? Order { get; set; }
        public string Title { get; set; }
        public Guid  ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}