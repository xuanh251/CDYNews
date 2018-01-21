using CDYNews.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDYNews.Web.Models
{
    public class DetailViewModel
    {
        public PostViewModel PostDetail { get; set; }
        public IEnumerable<PostViewModel> PopularPost { get; set; }
        public PostCategoryViewModel CategoryName { get; set; }
        public IEnumerable<TagViewModel> ListTags { get; set; }
        public List<PostViewModel> RelativePost { get; set; }
        public IEnumerable<PostViewModel> SameCategoryPost { get; set; }
    }
}