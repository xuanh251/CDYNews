using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDYNews.Web.Models
{
    public class FooterViewModel
    {
        public IEnumerable<PostViewModel> Posts { set; get; }
        public IEnumerable<PostCategoryViewModel> PostCategories { set; get; }
    }
}