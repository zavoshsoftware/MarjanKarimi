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
        public List<ServiceGroup> SidebarServiceGroups { get; set; }
        public ServiceGroup ServiceGroup { get; set; }
       
    }
}