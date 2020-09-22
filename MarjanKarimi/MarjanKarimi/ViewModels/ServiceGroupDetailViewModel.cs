using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class ServiceGroupDetailViewModel:_BaseViewModel
    {
        public List<ServiceGroup> SidebarServiceGroups { get; set; }
        public List<Service> Services { get; set; }
        public ServiceGroup ServiceGroup { get; set; }
    }
}