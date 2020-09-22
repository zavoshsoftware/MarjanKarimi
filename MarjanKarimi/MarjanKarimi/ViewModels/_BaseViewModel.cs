 

using System.Collections.Generic;
using Models;
using Telerik.Windows.Documents.Fixed.Model.Editing.Lists;
using Helpers;

namespace ViewModels
{
    public class _BaseViewModel
    {
        readonly BaseViewModelHelper _baseViewModelHelper = new BaseViewModelHelper();
        public List<MegaMenuService> MenuServices
        {
            get
            {
                return _baseViewModelHelper.GetMenuSerivce();

            }
        }
        public TextItem FooterDesc
        {
            get
            {
                return _baseViewModelHelper.GetFooterText();
            }
        }

        public class MegaMenuService
        {
            public ServiceGroup ServiceGroup { get; set; }
            public List<Service> Services { get; set; }
        }
      
    }
}