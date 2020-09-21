using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Models;
 
//using ViewModels;

namespace Helpers
{
    public class BaseViewModelHelper
    {
        private DatabaseContext db = new DatabaseContext();
        public List<Service> GetMenuSerivce()
        {
            return db.Services.Where(current =>current.IsDeleted == false && current.IsActive).OrderBy(current => current.Order).ToList();
        }
        public TextItem GetFooterText()
        {
            return db.TextItems.Where(current => current.IsActive && !current.IsDeleted && current.TextItemType.UrlParam.ToLower() == "footerdesc").FirstOrDefault();
        }


    }
}