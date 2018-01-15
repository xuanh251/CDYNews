using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDYNews.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<PostViewModel> BannerPost { get; set; }
        public IEnumerable<PostViewModel> Posts { set; get; }
        public IEnumerable<PostViewModel> LastedPosts { set; get; }
        public IEnumerable<PostCategoryViewModel> PostCategories { set; get; }
        public IEnumerable<PostCategoryViewModel> PostCategoriesList { get; set; }
    }
}