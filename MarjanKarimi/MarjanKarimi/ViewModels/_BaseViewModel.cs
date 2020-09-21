 

using System.Collections.Generic;
using Models;
using Telerik.Windows.Documents.Fixed.Model.Editing.Lists;
using Helpers;

namespace ViewModels
{
    public class _BaseViewModel
    {
        readonly BaseViewModelHelper _baseViewModelHelper = new BaseViewModelHelper();
        public List<Service> MenuServices
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
      
    }
}