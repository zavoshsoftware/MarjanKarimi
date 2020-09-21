using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class ServiceListViewModel:_BaseViewModel
    {
        public List<Service> Services { get; set; }
        public ServiceGroup ServiceGroup { get; set; }
    }
}