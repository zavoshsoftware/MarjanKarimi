using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class ServiceComment:BaseEntity
    {
        public int? Order { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime AnswerDate { get; set; }
        public Guid ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}