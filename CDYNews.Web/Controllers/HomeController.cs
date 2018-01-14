using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using CDYNews.Model.Models;
using CDYNews.Service;
using CDYNews.Web.Models;

namespace CDYNews.Web.Controllers
{
    public class HomeController : Controller
    {
        IPostCategoryService _postCategoryService;
        IPostService _postService;
        public HomeController(IPostCategoryService postCategoryService,IPostService postService)
        {
            _postCategoryService = postCategoryService;
            _postService = postService;
        }
        // GET: Home
        public ActionResult Index()
        {
            var BannerPostView = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postService.GetBanner());
            var categoriesView = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(_postCategoryService.GetAll());
            var homeViewModel = new HomeViewModel();
            homeViewModel.BannerPost = BannerPostView;
            homeViewModel.PostCategories = categoriesView;
            return View(homeViewModel);
        }
        [ChildActionOnly]
        public ActionResult Footer()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult Header()
        {
            var model = _postCategoryService.GetAll();
            var postCategories = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(model);
            return PartialView(postCategories);
        }
        [ChildActionOnly]
        public ActionResult PostCategorieList()
        {
            var model = _postCategoryService.GetAllByParentId();
            var postCategories = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(model);
            return PartialView(postCategories);
        }
    }
}