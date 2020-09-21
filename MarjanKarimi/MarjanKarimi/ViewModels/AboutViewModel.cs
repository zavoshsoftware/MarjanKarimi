using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class AboutViewModel : _BaseViewModel
    {
      
        public TextItem AboutTextItem { get; set; }
        public List<TextItem> WhyChooseus { get; set; }
        public List<TextItem> Numbers { get; set; }
        public TextItem Contact { get; set; }
     
    }
}