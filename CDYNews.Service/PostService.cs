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
        //List<Post> GetPostTaggedList();
        IEnumerable<Post> MostViewCountPost();
        IEnumerable<Post> GetHealthPost();
        IEnumerable<Post> GetEducationPost();
        IEnumerable<Post> GetSciencePost();
    }

    class PostService : IPostService
    {
        private IPostRepository _postRepository;
        private IUnitOfWork _unitOfWork;
        private ITagRepository _tagRepository;
        private IPostTagRepository _postTagRepository;
        private ICommonServices _commonServices;

        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork, ITagRepository tagRepository, IPostTagRepository postTagRepository, ICommonServices commonServices)
        {
            _postRepository = postRepository;
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
            var model = _postRepository.GetAll().OrderByDescending(s => s.CreatedDate).Take(3);
            return model;
        }

        public Post GetById(int id)
        {
            return _postRepository.GetSingleById(id);
        }

        public IEnumerable<Post> GetEducationPost()
        {
            return _postRepository.GetAll().Where(s => s.CategoryID == 6).OrderByDescending(s => s.CreatedDate).Take(5);
        }

        public IEnumerable<Post> GetHealthPost()
        {
            return _postRepository.GetAll().Where(s=>s.CategoryID==1003).OrderByDescending(s => s.CreatedDate).Take(5);
        }

        public IEnumerable<Post> GetLastedPost()
        {
            return _postRepository.GetAll().OrderByDescending(s => s.CreatedDate).Take(5);
        }

        public IEnumerable<Post> GetSciencePost()
        {
            return _postRepository.GetAll().Where(s => s.CategoryID == 2).OrderByDescending(s => s.CreatedDate).Take(5);
        }

        //public List<Post> GetPostTaggedList()
        //{

        //    List<Post> result = new List<Post>();
        //    foreach (var post in GetLastedPost())
        //    {
        //        IEnumerable<Post> _scannedPost = _postRepository.GetAll().OrderByDescending(s => s.CreatedDate).Skip(2);
        //        IEnumerable<PostTag> _gotTagList = _commonServices.getListPostTagOfSelectedPost(post.ID);
        //        int i = 0;
        //        foreach (var item in _scannedPost)
        //        {
        //            if (i == 2) break;
        //            IEnumerable<PostTag> _currentTagList = _commonServices.getListPostTagOfSelectedPost(item.ID);
        //            foreach (var currentTag in _currentTagList)
        //            {
        //                if (i == 2) break;
        //                foreach (var gotTag in _gotTagList)
        //                {
        //                    if (i == 2) break;
        //                    if (currentTag.TagID == gotTag.TagID)
        //                    {
        //                        result.Add(item);
        //                        i++;
        //                    }
        //                }
        //            }
        //        }

        //    }
        //    return result;
        //}

        public IEnumerable<Post> MostViewCountPost()
        {
            return _postRepository.GetAll().Where(s=>s.Status).OrderByDescending(s => s.ViewCount).Take(3);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
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
