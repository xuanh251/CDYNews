using CDYNews.Common;
using CDYNews.Data.Infrastructure;
using CDYNews.Data.Repositories;
using CDYNews.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDYNews.Service
{
    public interface IPostService
    {
        Post Add(Post post);

        void Update(Post post);

        Post Delete(int id);

        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetAll(string keyword);

        IEnumerable<Post> GetAllPaging(int page, int pageSize, out int totalRow);

        IEnumerable<Post> GetAllByCategoryPaging(int categoryID, int page, int pageSize, out int totalRow);

        Post GetById(int id);

        IEnumerable<Post> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        void SaveChange();
        IEnumerable<Post> GetBanner();
        IEnumerable<Post> GetLastedPost();
        IEnumerable<Post> MostViewCountPost();
        IEnumerable<Post> GetHealthPost();
        IEnumerable<Post> GetEducationPost();
        IEnumerable<Post> GetSciencePost();
        List<Post> GetRelativePost(int postId);
        IEnumerable<Post> GetListPostByCategoryIdPaging(int categoryId, int page, int pageSize, out int totalRow);
        IEnumerable<string> GetListPostByKeyWord(string keyword);
        IEnumerable<Post> Search(string keyword, int page, int pageSize, out int totalRow);
        IEnumerable<Post> GetSameCategory(int postId);
    }

    class PostService : IPostService
    {
        private IPostRepository _postRepository;
        private IUnitOfWork _unitOfWork;
        private ITagRepository _tagRepository;
        private IPostTagRepository _postTagRepository;
        private IPostCategoryRepository _postCategoryRepository;
        private ICommonServices _commonServices;


        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork, ITagRepository tagRepository, IPostTagRepository postTagRepository, IPostCategoryRepository postCategoryRepository, ICommonServices commonServices)
        {
            _postRepository = postRepository;
            _postCategoryRepository = postCategoryRepository;
            _unitOfWork = unitOfWork;
            _tagRepository = tagRepository;
            _postTagRepository = postTagRepository;
            _commonServices = commonServices;
        }

        public Post Add(Post post)
        {
            var _post = _postRepository.Add(post);
            SaveChange();
            if (!string.IsNullOrEmpty(_post.Tags))
            {
                string[] ListTags = _post.Tags.Split(',');
                foreach (var item in ListTags)
                {
                    var TagID = StringHelper.ToUnsignString(item);
                    if (_tagRepository.Count(s => s.ID == TagID) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = TagID;
                        tag.Name = item;
                        tag.Type = CommonConstants.PostTag;
                        _tagRepository.Add(tag);
                    }
                    PostTag postTag = new PostTag();
                    postTag.PostID = _post.ID;
                    postTag.TagID = TagID;
                    _postTagRepository.Add(postTag);
                }
            }
            return _post;
        }

        public Post Delete(int id)
        {
            return _postRepository.Delete(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll();
        }
        public IEnumerable<Post> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _postRepository.GetMulti(s => s.Name.Contains(keyword) || s.Description.Contains(keyword) || s.Alias.Contains(keyword));
            }
            else
            {
                return _postRepository.GetAll();
            }

        }
        public IEnumerable<Post> GetAllByCategoryPaging(int categoryID, int page, int pageSize, out int totalRow)
        {
            return _postRepository.GetMultiPaging(s => s.Status && s.CategoryID == categoryID, out totalRow, page, pageSize, new string[] { "PostCatogory" });
        }

        public IEnumerable<Post> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            return _postRepository.GetAllByTag(tag, page, pageSize, out totalRow);
        }

        public IEnumerable<Post> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _postRepository.GetMultiPaging(s => s.Status, out totalRow, page, pageSize);
        }

        public IEnumerable<Post> GetBanner()
        {
            var model = _postRepository.GetMulti(s => s.Status).OrderByDescending(s => s.CreatedDate).Take(3);
            return model;
        }

        public Post GetById(int id)
        {
            return _postRepository.GetSingleById(id);
        }

        public IEnumerable<Post> GetEducationPost()
        {
            return _postRepository.GetMulti(s => s.CategoryID == 6).OrderByDescending(s => s.CreatedDate).Take(4);
        }

        public IEnumerable<Post> GetHealthPost()
        {
            return _postRepository.GetMulti(s => s.CategoryID == 1003).OrderByDescending(s => s.CreatedDate).Take(4);
        }

        public IEnumerable<Post> GetLastedPost()
        {
            return _postRepository.GetMulti(s => s.Status).OrderByDescending(s => s.CreatedDate).Take(5);
        }

        public IEnumerable<Post> GetListPostByCategoryIdPaging(int categoryId, int page, int pageSize, out int totalRow)
        {
            var query = _postRepository.GetMulti(x => x.Status && x.CategoryID == categoryId);

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<string> GetListPostByKeyWord(string keyword)
        {
            return _postRepository.GetMulti(s => s.Status && s.Name.Contains(keyword)).Select(s => s.Name);
        }

        public List<Post> GetRelativePost(int postId)
        {
            var postDetail = GetById(postId);
            //lấy ra list tag của bài viết hiện tại
            string[] ListTags = postDetail.Tags != null ? postDetail.Tags.Split(',') : null;
            var allPosts = GetAll().Where(s => s.Status).OrderByDescending(s => s.CreatedDate);
            var listRelativePosts = new List<Post>();
            //duyệt qua từng bài viết, nếu là bài viết hiện tại thì bỏ qua
            if (ListTags != null)
            {
                foreach (var item in allPosts)
                {
                    var isAddPost = false;
                    if (item.ID == postDetail.ID) continue;
                    //nếu bài viết đang duyệt có tag thì tạo mảng tag cho bài viết đó, ko thì để null
                    string[] currentListTags = item.Tags != null ? item.Tags.Split(',') : null;
                    //nếu list tag vừa tạo có phần tử thì duyệt qua nó
                    if (currentListTags != null)
                    {
                        foreach (var tag in currentListTags)
                        {
                            var result = Array.FindAll(ListTags, s => s.Equals(tag));
                            if (result.Length != 0)
                            {
                                isAddPost = true;
                                break;
                            }
                        }
                    }
                    if (isAddPost)
                    {
                        listRelativePosts.Add(item);
                    }
                }
            }
            
            return listRelativePosts;
        }

        public IEnumerable<Post> GetSameCategory(int postId)
        {
            var category = _postRepository.GetSingleById(postId);
            var data = _postRepository.GetMulti(s => s.Status && s.ID != postId && s.CategoryID == category.CategoryID);
            return data;
        }

        public IEnumerable<Post> GetSciencePost()
        {
            return _postRepository.GetAll().Where(s => s.CategoryID == 2).OrderByDescending(s => s.CreatedDate).Take(5);
        }

        public IEnumerable<Post> MostViewCountPost()
        {
            return _postRepository.GetAll().Where(s => s.Status).OrderByDescending(s => s.ViewCount);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Post> Search(string keyword, int page, int pageSize, out int totalRow)
        {
            var seoKeyWord = StringHelper.ToUnsignString(keyword);
            var query = _postRepository.GetMulti(x => x.Status && (x.Name.Contains(keyword) || x.Alias.Contains(seoKeyWord)));
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public void Update(Post post)
        {
            _postRepository.Update(post);
            if (!string.IsNullOrEmpty(post.Tags))
            {
                string[] ListTags = post.Tags.Split(',');
                foreach (var item in ListTags)
                {
                    var TagID = StringHelper.ToUnsignString(item);
                    if (_tagRepository.Count(s => s.ID == TagID) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = TagID;
                        tag.Name = item;
                        tag.Type = CommonConstants.PostTag;
                        _tagRepository.Add(tag);
                    }
                    _postTagRepository.DeleteMulti(s => s.PostID == post.ID);
                    PostTag postTag = new PostTag();
                    postTag.PostID = post.ID;
                    postTag.TagID = TagID;
                    _postTagRepository.Add(postTag);
                }
            }
        }

    }
}
