using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using CDYNews.Model.Models;
using CDYNews.Web.Models;
using CDYNews.Service;
using CDYNews.Common;
using CDYNews.Web.Infrastructure.Core;
using CDYNews.Web.Infrastructure.Extensions;

namespace CDYNews.Web.Controllers
{
    public class PostController : Controller
    {
        IPostService _postService;
        IPostCategoryService _postCategoryService;
        public PostController(IPostService postService, IPostCategoryService postCategoryService)
        {
            _postService = postService;
            _postCategoryService = postCategoryService;
        }

        // GET: Post
        public ActionResult Detail(int postId)
        {
            Post post = _postService.GetById(postId);
            if (post.ViewCount.HasValue) post.ViewCount++;
            else post.ViewCount = 1;
            _postService.Update(post);
            _postService.SaveChange();

            DetailViewModel detailViewModel = new DetailViewModel();
            var postDetail = _postService.GetById(postId);
            var postDetailContentView = Mapper.Map<Post, PostViewModel>(postDetail);
            var listRelativePosts = _postService.GetRelativePost(postId);
            detailViewModel.PostDetail = postDetailContentView;
            detailViewModel.CategoryName = Mapper.Map<PostCategory, PostCategoryViewModel>(_postCategoryService.GetById(postDetail.CategoryID));
            detailViewModel.ListTags =Mapper.Map<IEnumerable<Tag>,IEnumerable<TagViewModel>>(_postService.GetListTagByPostId(postId));
            detailViewModel.RelativePost = listRelativePosts.Count != 0 ? Mapper.Map<List<Post>, List<PostViewModel>>(listRelativePosts) : null;
            detailViewModel.PopularPost = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postService.MostViewCountPost().Take(5));
            detailViewModel.SameCategoryPost = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(_postService.GetSameCategory(postId).OrderByDescending(s => s.CreatedDate).Take(5));
            return View(detailViewModel);
        }
        public ActionResult ListPostCategory(int id, int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var postModel = _postService.GetListPostByCategoryIdPaging(id, page, pageSize, out totalRow);
            var postViewModel = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(postModel);
            var category = _postCategoryService.GetById(id);
            ViewBag.Category = Mapper.Map<PostCategory, PostCategoryViewModel>(category);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<PostViewModel>()
            {
                Items = postViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPage = totalPage
            };
            return View(paginationSet);
        }
        public JsonResult GetListProductByKeyWord(string keyword)
        {
            var model = _postService.GetListPostByKeyWord(keyword);
            return Json(new
            {
                data = model
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string keyword, int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var postModel = _postService.Search(keyword, page, pageSize, out totalRow);
            var PostViewModel = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(postModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            ViewBag.Keyword = keyword;
            ViewBag.SearchTitle = keyword;
            var paginationSet = new PaginationSet<PostViewModel>()
            {
                Items = PostViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPage = totalPage
            };

            return View(paginationSet);
        }
        public ActionResult ListPostByTag(string tagId, int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var postModel = _postService.GetListPostsByTag(tagId, page, pageSize, out totalRow);
            var PostViewModel = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(postModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            ViewBag.Tag = Mapper.Map<Tag,TagViewModel>(_postService.GetTag(tagId));
            var paginationSet = new PaginationSet<PostViewModel>()
            {
                Items = PostViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPage = totalPage
            };

            return View(paginationSet);
        }
    }
}