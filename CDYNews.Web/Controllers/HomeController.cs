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
        public HomeController(IPostCategoryService postCategoryService, IPostService postService)
        {
            _postCategoryService = postCategoryService;
            _postService = postService;
        }
        // GET: Home
        [OutputCache(Duration = 60,Location =System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult Index()
        {
            //component
            var BannerPostView = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postService.GetBanner());
            var categoriesMenuView = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(_postCategoryService.GetAll());
            var categoriesListView = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(_postCategoryService.GetAllByParentId());
            var mostVisitedPostView = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postService.MostViewCountPost().Take(3));
            var healthPostView = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postService.GetHealthPost());
            var educationPostView = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postService.GetEducationPost());
            var sciencePostView = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postService.GetSciencePost());
            //lasted posts
            var lastedPostView = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postService.GetLastedPost());

            var homeViewModel = new HomeViewModel();
            homeViewModel.BannerPost = BannerPostView;
            homeViewModel.PostCategories = categoriesMenuView;
            homeViewModel.PostCategoriesList = categoriesListView;
            homeViewModel.LastedPosts = lastedPostView;
            homeViewModel.MostVisitedPost = mostVisitedPostView;
            homeViewModel.HealthPost = healthPostView;
            homeViewModel.EducationPost = educationPostView;
            homeViewModel.SciencePost = sciencePostView;

            return View(homeViewModel);
        }
        [ChildActionOnly]
        [OutputCache(Duration =3600)]
        public ActionResult Footer()
        {
            var categoriesView = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(_postCategoryService.GetAll());
            var postView= Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postService.GetAll());
            FooterViewModel footerViewModel = new FooterViewModel();
            footerViewModel.PostCategories = categoriesView;
            footerViewModel.Posts = postView;
            return PartialView(footerViewModel);
        }
        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Header()
        {
            var model = _postCategoryService.GetAll();
            var postCategories = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(model);
            return PartialView(postCategories.Where(s=>s.HomeFlag&&s.Status));
        }
        [ChildActionOnly]
        public ActionResult Scripts()
        {
            return PartialView();
        }
    }
}