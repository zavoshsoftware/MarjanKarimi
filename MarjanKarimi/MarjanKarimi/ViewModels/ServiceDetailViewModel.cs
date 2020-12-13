using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class ServiceDetailViewModel : _BaseViewModel
    {
        public Service Service { get; set; }
        public List<ServiceTopic> ServiceTopics { get; set; }
        public List<ServiceFaq> ServiceFaqs { get; set; }
        public ServiceGroup ServiceGroup { get; set; }
       
    }
}