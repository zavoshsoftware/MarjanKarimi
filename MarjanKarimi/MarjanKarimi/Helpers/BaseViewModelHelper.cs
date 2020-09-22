using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Models;
using static ViewModels._BaseViewModel;

//using ViewModels;

namespace Helpers
{
    public class BaseViewModelHelper
    {
        private DatabaseContext db = new DatabaseContext();
        public List<MegaMenuService> GetMenuSerivce()
        {
            List<ServiceGroup> groups = db.ServiceGroups.Where(current => current.IsActive && !current.IsDeleted).OrderBy(current=>current.Order).ToList();
            List<MegaMenuService> result = new List<MegaMenuService>();
            foreach (ServiceGroup group in groups)
            {
                result.Add(new MegaMenuService()
                {
                    ServiceGroup=group,
                    Services = db.Services.Where(current=>current.IsActive && !current.IsDeleted && current.ServiceGroupId == group.Id).OrderBy(current => current.Order).ToList()
                });
            }
            return result;
        }
        public TextItem GetFooterText()
        {
            return db.TextItems.Where(current => current.IsActive && !current.IsDeleted && current.TextItemType.UrlParam.ToLower() == "footerdesc").FirstOrDefault();
        }


    }
}