using System.Collections.Generic;
using Models;

namespace ViewModels
{
    public class GalleryListViewModel : _BaseViewModel
    {
        public List<Gallery> GelleryList { get; set; }
        public List<GalleryGroup> FilterList { get; set; }

    }
}