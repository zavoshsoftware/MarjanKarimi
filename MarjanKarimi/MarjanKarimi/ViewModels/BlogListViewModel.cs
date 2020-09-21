using System.Collections.Generic;
using Models;

namespace ViewModels
{
    public class BlogListViewModel : _BaseViewModel
    {
        public List<Blog> Blogs { get; set; }
    }
}